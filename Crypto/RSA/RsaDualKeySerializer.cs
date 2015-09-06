using System;

namespace MessageEncrypter.Crypto.RSA
{
    sealed class RsaDualKeySerializer : IKeyPairSerializer<RsaDualKey, RsaDualKey>,
                                        IPublicKeySerializer<RsaDualKey>
    {
        public string SerializeKeyPair(IKeyPair<RsaDualKey, RsaDualKey> keyPair)
        {
            return keyPair.PrivateKey.ToXml(true);
        }

        public string SerializePublicKey(RsaDualKey publicKey)
        {
            return publicKey.ToXml(false);
        }

        public IKeyPair<RsaDualKey, RsaDualKey> DeserializeKeyPair(string serializedKeyPair, Guid identifier)
        {
            return RsaDualKey.FromXml(serializedKeyPair, identifier);
        }

        public RsaDualKey DeserializePublicKey(string serializedPublicKey, Guid identifier)
        {
            return RsaDualKey.FromXml(serializedPublicKey, identifier);
        }
    }
}
