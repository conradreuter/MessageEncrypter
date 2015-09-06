namespace MessageEncrypter.Crypto.RSA
{
    sealed class RsaCryptoImplementation : ICryptoImplementation<RsaDualKey, RsaDualKey>
    {
        public IKeyPairGenerator<RsaDualKey, RsaDualKey> KeyPairGenerator { get; private set; }
        public IKeyPairSerializer<RsaDualKey, RsaDualKey> KeyPairSerializer { get; private set; }
        public IPublicKeySerializer<RsaDualKey> PublicKeySerializer { get; private set; }

        public RsaCryptoImplementation()
        {
            this.KeyPairGenerator = new RsaKeyPairGenerator();
            var serializer = new RsaDualKeySerializer();
            this.KeyPairSerializer = serializer;
            this.PublicKeySerializer = serializer;
        }
    }
}