using System;
using System.IO;
using System.Linq;
using System.Text;

namespace MessageEncrypter.Crypto
{
    static class CryptoFacade
    {
        public static readonly Encoding CharacterEncoding = Encoding.UTF8;

        private static class Directories
        {
            public static readonly string KeyPair;
            public static readonly string PublicKeys;

            static Directories()
            {
                var localAppDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var baseDirectory = Path.Combine(localAppDataDirectory, "MessageEncrypter");
                KeyPair = Path.Combine(baseDirectory, "keypair");
                PublicKeys = Path.Combine(baseDirectory, "publickeys");
                Directory.CreateDirectory(KeyPair);
                Directory.CreateDirectory(PublicKeys);
            }
        }

        public static ICryptoFacade Create<TPrivateKey, TPublicKey>(ICryptoImplementation<TPrivateKey, TPublicKey> cryptoImplementation)
            where TPrivateKey : IPrivateKey
            where TPublicKey : IPublicKey
        {
            return new Impl<TPrivateKey, TPublicKey>
            {
                KeyPairProvider = new FileSystemKeyPairProvider<TPrivateKey, TPublicKey>(Directories.KeyPair, cryptoImplementation.KeyPairSerializer)
                {
                    KeyPairGenerator = cryptoImplementation.KeyPairGenerator
                },
                PublicKeyRepository = new FileSystemPublicKeyRepository<TPublicKey>(Directories.PublicKeys, cryptoImplementation.PublicKeySerializer),
                DataExchangeEncoder = new PrefixedBase64DataExchangeEncoder<TPublicKey>(CharacterEncoding, cryptoImplementation.PublicKeySerializer)
            };
        }

        private class Impl<TPrivateKey, TPublicKey> : ICryptoFacade
            where TPrivateKey : IPrivateKey
            where TPublicKey : IPublicKey
        {
            public IKeyPairProvider<TPrivateKey, TPublicKey> KeyPairProvider { private get; set; }
            public IPublicKeyRepository<TPublicKey> PublicKeyRepository { private get; set; }
            public IDataExchangeEncoder<TPublicKey> DataExchangeEncoder { private get; set; }

            public event EventHandler<PublicKeyEventArgs> NewPublicKey;
            public event EventHandler<PublicKeyEventArgs> RemovePublicKey;
            public event EventHandler<OutputEventArgs> Output;
            public event EventHandler<ConfirmationEventArgs> NeedConfirmation;

            public void Initialise()
            {
                var publicKeys = this.PublicKeyRepository.PublicKeys.ToList();
                publicKeys.ForEach(FireNewPublicKey);
            }

            public InputType DetermineInputType(string input)
            {
                if (string.IsNullOrEmpty(input))
                {
                    return InputType.Empty;
                }
                else if (DataExchangeEncoder.CouldItBeAPublicKey(input))
                {
                    return InputType.PublicKey;
                }
                else if (DataExchangeEncoder.CouldItBeEncryptedBytes(input))
                {
                    return InputType.EncryptedMessage;
                }
                else
                {
                    return InputType.Message;
                }
            }

            public void CopyMyPublicKeyToClipboard()
            {
                var myPublicKey = KeyPairProvider.KeyPair.PublicKey;
                var encodedPublicKey = DataExchangeEncoder.EncodePublicKey(myPublicKey);
                FireOutput(OutputType.MyPublicKey, encodedPublicKey);
            }

            public void ImportPublicKey(string publicKeyInput, string description)
            {
                var publicKey = DataExchangeEncoder.DecodePublicKey(publicKeyInput);
                publicKey.Description = description;
                if (NoPublicKeyWithTheGivenIdentifierExistsOrItCanBeOverwritten(publicKey.Identifier))
                {
                    PublicKeyRepository.SavePublicKey(publicKey);
                    FireNewPublicKey(publicKey);
                    FireOutput(OutputType.ImportedPublicKey, publicKey.Identifier.ToString());
                }
            }

            private bool NoPublicKeyWithTheGivenIdentifierExistsOrItCanBeOverwritten(Guid identifier)
            {
                var alreadyExistingPublicKey = PublicKeyRepository.GetPublicKey(identifier);
                if (alreadyExistingPublicKey != null)
                {
                    if (!RequestConfirmation(Confirmation.PublicKeyOverwrite()))
                    {
                        return false;
                    }
                    FireRemovePublicKey(alreadyExistingPublicKey);
                }
                return true;
            }

            public void EncryptMessage(string message, IPublicKey publicKey)
            {
                var bytes = CharacterEncoding.GetBytes(message);
                var encryptedBytes = publicKey.Encrypt(bytes);
                var encryptedMessage = DataExchangeEncoder.EncodeEncryptedBytes(encryptedBytes, publicKey.Identifier);
                FireOutput(OutputType.EncryptedMessage, encryptedMessage);
            }

            public void DecryptMessage(string encryptedMessageInput)
            {
                var tuple = DataExchangeEncoder.DecodeEncryptedBytes(encryptedMessageInput);
                var encryptedBytes = tuple.Item1;
                var identifier = tuple.Item2;
                var privateKey = KeyPairProvider.KeyPair.PrivateKey;
                if (!identifier.Equals(privateKey.Identifier))
                {
                    throw ApplicationException.KeyPairIdentifierMismatch();
                }
                var bytes = privateKey.Decrypt(encryptedBytes);
                var message = CharacterEncoding.GetString(bytes);
                FireOutput(OutputType.DecryptedMessage, message);
            }

            private void FireNewPublicKey(TPublicKey publicKey)
            {
                if (NewPublicKey != null)
                {
                    var args = new PublicKeyEventArgs(publicKey);
                    NewPublicKey(this, args);
                }
            }

            private void FireRemovePublicKey(TPublicKey publicKey)
            {
                if (RemovePublicKey != null)
                {
                    var args = new PublicKeyEventArgs(publicKey);
                    RemovePublicKey(this, args);
                }
            }

            private void FireOutput(OutputType type, string output)
            {
                if (Output != null)
                {
                    var args = new OutputEventArgs(type, output);
                    Output(this, args);
                }
            }

            private bool RequestConfirmation(ConfirmationEventArgs args)
            {
                NeedConfirmation(this, args);
                return args.Confirmed;
            }
        }
    }
}