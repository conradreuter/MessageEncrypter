using System;

namespace MessageEncrypter.Crypto.RSA
{
    partial class RsaDualKey
    {
        public static RsaDualKey Generate()
        {
            return new RsaDualKey
            {
                Identifier = Guid.NewGuid()
            };
        }

        public static RsaDualKey FromXml(string xml, Guid identifier)
        {
            var rsaKey = new RsaDualKey
            {
                Identifier = identifier
            };
            rsaKey.rsaCsp.FromXmlString(xml);
            return rsaKey;
        }

        private static RsaDualKey ExtractPublicKey(RsaDualKey rsaKey)
        {
            var rsaKeyWithoutPrivateKey = new RsaDualKey
            {
                Identifier = rsaKey.Identifier,
                Description = rsaKey.Description
            };
            var publicRsaParameters = rsaKey.rsaCsp.ExportParameters(false);
            rsaKeyWithoutPrivateKey.rsaCsp.ImportParameters(publicRsaParameters);
            return rsaKeyWithoutPrivateKey;
        }
    }
}
