using System;

namespace MessageEncrypter.Crypto
{
    class PublicKeyEventArgs : EventArgs
    {
        public IPublicKey PublicKey { get; private set; }

        public PublicKeyEventArgs(IPublicKey publicKey)
        {
            this.PublicKey = publicKey;
        }
    }
}
