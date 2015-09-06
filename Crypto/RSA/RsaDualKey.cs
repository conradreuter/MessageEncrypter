using System;
using System.Security.Cryptography;

namespace MessageEncrypter.Crypto.RSA
{
    sealed partial class RsaDualKey : IKeyPair<RsaDualKey, RsaDualKey>, IPublicKey, IPrivateKey
    {
        private const int KeySize = 4096;

        private readonly RSACryptoServiceProvider rsaCsp;

        public RsaDualKey PrivateKey { get { return this; } }
        public RsaDualKey PublicKey { get { return ExtractPublicKey(this); } }

        public Guid Identifier { get; private set; }
        public string Description { get; set; }

        private RsaDualKey()
        {
            this.rsaCsp = new RSACryptoServiceProvider(KeySize) { PersistKeyInCsp = false };
        }

        public byte[] Encrypt(byte[] bytes)
        {
            return rsaCsp.Encrypt(bytes, true);
        }

        public byte[] Decrypt(byte[] encryptedBytes)
        {
            return rsaCsp.Decrypt(encryptedBytes, true);
        }

        public string ToXml(bool isKeyPair)
        {
            return rsaCsp.ToXmlString(isKeyPair);
        }
    }
}
