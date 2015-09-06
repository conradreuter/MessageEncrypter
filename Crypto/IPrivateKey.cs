using System;

namespace MessageEncrypter.Crypto
{
    interface IPrivateKey
    {
        Guid Identifier { get; }
        
        byte[] Decrypt(byte[] encryptedBytes);
    }
}
