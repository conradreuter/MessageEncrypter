namespace MessageEncrypter.Crypto
{
    interface IKeyPairProvider<TPrivateKey, TPublicKey>
        where TPrivateKey : IPrivateKey
        where TPublicKey : IPublicKey
    {
        IKeyPairGenerator<TPrivateKey, TPublicKey> KeyPairGenerator { set; }
        IKeyPair<TPrivateKey, TPublicKey> KeyPair { get; }
    }
}
