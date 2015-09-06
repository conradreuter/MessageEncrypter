using System;

namespace MessageEncrypter.Crypto
{
    interface IPublicKeySerializer<TPublicKey>
        where TPublicKey : IPublicKey
    {
        string SerializePublicKey(TPublicKey publicKey);
        TPublicKey DeserializePublicKey(string serializedPublicKey, Guid identifier);
    }
}
