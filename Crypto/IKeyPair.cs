namespace MessageEncrypter.Crypto
{
    interface IKeyPair<TPrivateKey, TPublicKey>
        where TPrivateKey : IPrivateKey
        where TPublicKey : IPublicKey
    {
        TPrivateKey PrivateKey { get; }
        TPublicKey PublicKey { get; }
    }
}
