using System;
using System.Collections.Generic;

namespace MessageEncrypter.Crypto
{
    interface IPublicKeyRepository<TPublicKey>
        where TPublicKey : IPublicKey
    {
        IEnumerable<TPublicKey> PublicKeys { get; }

        TPublicKey GetPublicKey(Guid identifier);
        void SavePublicKey(TPublicKey publicKey);
    }
}
