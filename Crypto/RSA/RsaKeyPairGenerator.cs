namespace MessageEncrypter.Crypto.RSA
{
    sealed class RsaKeyPairGenerator : IKeyPairGenerator<RsaDualKey, RsaDualKey>
    {
        public IKeyPair<RsaDualKey, RsaDualKey> GenerateKeyPair()
        {
            return RsaDualKey.Generate();
        }
    }
}
