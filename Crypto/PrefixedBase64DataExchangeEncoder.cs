using System;
using System.Text;
using System.Text.RegularExpressions;

namespace MessageEncrypter.Crypto
{
    class PrefixedBase64DataExchangeEncoder<TPublicKey> : IDataExchangeEncoder<TPublicKey>
        where TPublicKey : IPublicKey
    {
        private static class Prefixes
        {
            public const string PublicKey = "###MESSAGEENCRYPTER_PUBLICKEY_{0}###";
            public const string EncryptedBytes = "###MESSAGEENCRYPTER_ENCRYPTEDMESSAGE_{0}###";
        }

        private static class Regexes
        {
            private const string GuidPattern = "([a-f0-9]{8}(?:-[a-f0-9]{4}){3}-[a-f0-9]{12})";

            public static readonly Regex PublicKey = GenerateRegexFromPrefix(Prefixes.PublicKey);
            public static readonly Regex EncryptedBytes = GenerateRegexFromPrefix(Prefixes.EncryptedBytes);

            private static Regex GenerateRegexFromPrefix(string prefix)
            {
                var prefixPattern = string.Format(prefix, GuidPattern);
                var pattern = string.Format("^{0}(.*)$", prefixPattern);
                return new Regex(pattern, RegexOptions.IgnoreCase);
            }
        }

        private readonly Encoding characterEncoding;
        private readonly IPublicKeySerializer<TPublicKey> publicKeySerializer;

        public PrefixedBase64DataExchangeEncoder(Encoding characterEncoding,
                                                 IPublicKeySerializer<TPublicKey> publicKeySerializer)
        {
            this.characterEncoding = characterEncoding;
            this.publicKeySerializer = publicKeySerializer;
        }

        public string EncodePublicKey(TPublicKey publicKey)
        {
            var serializedPublicKey = publicKeySerializer.SerializePublicKey(publicKey);
            var bytes = characterEncoding.GetBytes(serializedPublicKey);
            return Base64Encode(Prefixes.PublicKey, publicKey.Identifier, bytes);
        }

        public TPublicKey DecodePublicKey(string encodedPublicKey)
        {
            var tuple = Base64Decode(Regexes.PublicKey, encodedPublicKey);
            var serializedPublicKey = characterEncoding.GetString(tuple.Item1);
            var identifier = tuple.Item2;
            return publicKeySerializer.DeserializePublicKey(serializedPublicKey, identifier);
        }

        public bool CouldItBeAPublicKey(string encodedString)
        {
            return CouldDecodingBePossible(Regexes.PublicKey, encodedString);
        }

        public string EncodeEncryptedBytes(byte[] encryptedBytes, Guid publicKeyIdentifier)
        {
            return Base64Encode(Prefixes.EncryptedBytes, publicKeyIdentifier, encryptedBytes);
        }

        public Tuple<byte[], Guid> DecodeEncryptedBytes(string encodedEncryptedBytes)
        {
            return Base64Decode(Regexes.EncryptedBytes, encodedEncryptedBytes);
        }

        public bool CouldItBeEncryptedBytes(string encodedString)
        {
            return CouldDecodingBePossible(Regexes.EncryptedBytes, encodedString);
        }

        private string Base64Encode(string prefixFormat, Guid identifier, byte[] bytes)
        {
            var encodedContent = Convert.ToBase64String(bytes);
            var prefix = string.Format(prefixFormat, identifier.ToString());
            return prefix + encodedContent;
        }

        private Tuple<byte[], Guid> Base64Decode(Regex regex, string prefixPlusEncodedContent)
        {
            var match = regex.Match(prefixPlusEncodedContent);
            var identifierAsString = match.Groups[1].Value;
            var identifier = Guid.Parse(identifierAsString);
            var encodedContent = match.Groups[2].Value;
            var bytes = Convert.FromBase64String(encodedContent);
            return Tuple.Create(bytes, identifier);
        }

        private bool CouldDecodingBePossible(Regex regex, string encodedString)
        {
            return regex.IsMatch(encodedString);
        }
    }
}
