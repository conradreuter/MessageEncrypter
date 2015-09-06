namespace MessageEncrypter.Crypto
{
    interface ICryptoImplementation<TPrivateKey, TPublicKey>
        where TPrivateKey : IPrivateKey
        where TPublicKey : IPublicKey
    {
        IKeyPairGenerator<TPrivateKey, TPublicKey> KeyPairGenerator { get; }
        IKeyPairSerializer<TPrivateKey, TPublicKey> KeyPairSerializer { get; }
        IPublicKeySerializer<TPublicKey> PublicKeySerializer { get; }
    }
}
