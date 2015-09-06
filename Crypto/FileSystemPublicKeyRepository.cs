using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MessageEncrypter.Crypto
{
    class FileSystemPublicKeyRepository<TPublicKey> : IPublicKeyRepository<TPublicKey>
        where TPublicKey : IPublicKey
    {
        private static class Files
        {
            public const string Description = "description";
            public const string PublicKey = "publickey";
        }

        private readonly string directory;
        private readonly IPublicKeySerializer<TPublicKey> publicKeySerializer;
        private readonly Lazy<IDictionary<Guid, TPublicKey>> publicKeysByIdentifier;

        public IEnumerable<TPublicKey> PublicKeys { get { return publicKeysByIdentifier.Value.Values; } }

        public FileSystemPublicKeyRepository(string directory,
                                             IPublicKeySerializer<TPublicKey> publicKeySerializer)
        {
            this.directory = directory;
            this.publicKeySerializer = publicKeySerializer;
            this.publicKeysByIdentifier = new Lazy<IDictionary<Guid, TPublicKey>>(LoadPublicKeysByIdentifier);
        }

        private IDictionary<Guid, TPublicKey> LoadPublicKeysByIdentifier()
        {
            return LoadPublicKeys().ToDictionary(pk => pk.Identifier);
        }

        private IEnumerable<TPublicKey> LoadPublicKeys()
        {
            return GetAvailablePublicKeyDirectories().Select(LoadPublicKeyFromDirectory);
        }

        private IEnumerable<string> GetAvailablePublicKeyDirectories()
        {
            return Directory.EnumerateDirectories(directory);
        }

        private TPublicKey LoadPublicKeyFromDirectory(string publicKeyDirectory)
        {
            var serializedPublicKey = ReadAllTextFrom(publicKeyDirectory, Files.PublicKey);
            var identifierString = new DirectoryInfo(publicKeyDirectory).Name;
            var identifier = Guid.Parse(identifierString);
            var description = ReadAllTextFrom(publicKeyDirectory, Files.Description);
            var publicKey = publicKeySerializer.DeserializePublicKey(serializedPublicKey, identifier);
            publicKey.Description = description;
            return publicKey;
        }

        private string ReadAllTextFrom(string publicKeyDirectory, string fileName)
        {
            return File.ReadAllText(GetFilePath(publicKeyDirectory, fileName));
        }

        public TPublicKey GetPublicKey(Guid identifier)
        {
            TPublicKey publicKey;
            publicKeysByIdentifier.Value.TryGetValue(identifier, out publicKey);
            return publicKey;
        }

        public void SavePublicKey(TPublicKey publicKey)
        {
            var publicKeyDirectory = publicKey.Identifier.ToString();
            var serializedPublicKey = publicKeySerializer.SerializePublicKey(publicKey);
            Directory.CreateDirectory(GetDirectoryPath(publicKeyDirectory));
            WriteAllTextTo(publicKeyDirectory, Files.PublicKey, serializedPublicKey);
            WriteAllTextTo(publicKeyDirectory, Files.Description, publicKey.Description);
            publicKeysByIdentifier.Value[publicKey.Identifier] = publicKey;
        }

        private void WriteAllTextTo(string publicKeyDirectory, string fileName, string text)
        {
            File.WriteAllText(GetFilePath(publicKeyDirectory, fileName), text);
        }

        private string GetFilePath(string publicKeyDirectory, string fileName)
        {
            return Path.Combine(GetDirectoryPath(publicKeyDirectory), fileName);
        }

        private string GetDirectoryPath(string publicKeyDirectory)
        {
            return Path.Combine(directory, publicKeyDirectory);
        }
    }
}
