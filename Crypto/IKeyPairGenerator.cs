namespace MessageEncrypter.Crypto
{
    interface IKeyPairGenerator<TPrivateKey, TPublicKey>
        where TPrivateKey : IPrivateKey
        where TPublicKey : IPublicKey
    {
        IKeyPair<TPrivateKey, TPublicKey> GenerateKeyPair();
    }
}
