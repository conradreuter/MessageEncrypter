using System;

namespace MessageEncrypter.Crypto
{
    interface IKeyPairSerializer<TPrivateKey, TPublicKey>
        where TPrivateKey : IPrivateKey
        where TPublicKey : IPublicKey
    {
        string SerializeKeyPair(IKeyPair<TPrivateKey, TPublicKey> keyPair);
        IKeyPair<TPrivateKey, TPublicKey> DeserializeKeyPair(string serializedKeyPair, Guid identifier);
    }
}
