using System;

namespace MessageEncrypter.Crypto
{
    interface IDataExchangeEncoder<TPublicKey>
        where TPublicKey : IPublicKey
    {
        string EncodePublicKey(TPublicKey publicKey);
        TPublicKey DecodePublicKey(string encodedPublicKey);
        bool CouldItBeAPublicKey(string encodedString);

        string EncodeEncryptedBytes(byte[] encryptedBytes, Guid publicKeyIdentifier);
        Tuple<byte[], Guid> DecodeEncryptedBytes(string encodedEncryptedBytes);
        bool CouldItBeEncryptedBytes(string encodedString);
    }
}
