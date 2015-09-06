using System;

namespace MessageEncrypter.Crypto
{
    interface IPublicKey
    {
        Guid Identifier { get; }
        string Description { get; set; }

        byte[] Encrypt(byte[] bytes);
    }
}
