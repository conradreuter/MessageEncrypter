using System;
using System.IO;

namespace MessageEncrypter.Crypto
{
    class FileSystemKeyPairProvider<TPrivateKey, TPublicKey> : IKeyPairProvider<TPrivateKey, TPublicKey>
        where TPrivateKey : IPrivateKey
        where TPublicKey : IPublicKey
    {
        private const string KeyPairPublicKeyDescription = "<<<my own key pair>>>";
        private static class Files
        {
            public const string Identifier = "identifier";
            public const string KeyPair = "keypair";
        }

        private readonly string directory;
        private readonly IKeyPairSerializer<TPrivateKey, TPublicKey> keyPairSerializer;
        private readonly Lazy<IKeyPair<TPrivateKey, TPublicKey>> keyPair;

        public IKeyPairGenerator<TPrivateKey, TPublicKey> KeyPairGenerator { private get; set; }
        public IKeyPair<TPrivateKey, TPublicKey> KeyPair { get { return keyPair.Value; } }

        public FileSystemKeyPairProvider(string directory,
                                         IKeyPairSerializer<TPrivateKey, TPublicKey> keyPairSerializer)
        {
            this.directory = directory;
            this.keyPairSerializer = keyPairSerializer;
            this.keyPair = new Lazy<IKeyPair<TPrivateKey, TPublicKey>>(LoadKeyPair);
        }

        private IKeyPair<TPrivateKey, TPublicKey> LoadKeyPair()
        {
            try
            {
                return LoadKeyPairFromDirectory();
            }
            catch (FileNotFoundException)
            {
                var keyPair = KeyPairGenerator.GenerateKeyPair();
                SaveKeyPairInDirectory(keyPair);
                return keyPair;
            }
        }

        private IKeyPair<TPrivateKey, TPublicKey> LoadKeyPairFromDirectory()
        {
            var serializedKeyPair = ReadAllTextFrom(Files.KeyPair);
            var identifierString = ReadAllTextFrom(Files.Identifier);
            var identifier = Guid.Parse(identifierString);
            var keyPair = keyPairSerializer.DeserializeKeyPair(serializedKeyPair, identifier);
            keyPair.PublicKey.Description = KeyPairPublicKeyDescription;
            return keyPair;
        }

        private string ReadAllTextFrom(string fileName)
        {
            return File.ReadAllText(GetFilePath(fileName));
        }

        private void SaveKeyPairInDirectory(IKeyPair<TPrivateKey, TPublicKey> keyPair)
        {
            keyPair.PublicKey.Description = KeyPairPublicKeyDescription;
            var serializedKeyPair = keyPairSerializer.SerializeKeyPair(keyPair);
            var identifierString = keyPair.PrivateKey.Identifier.ToString();
            WriteAllTextTo(Files.KeyPair, serializedKeyPair);
            WriteAllTextTo(Files.Identifier, identifierString);
        }

        private void WriteAllTextTo(string fileName, string text)
        {
            File.WriteAllText(GetFilePath(fileName), text);
        }

        private string GetFilePath(string fileName)
        {
            return Path.Combine(directory, fileName);
        }
    }
}
