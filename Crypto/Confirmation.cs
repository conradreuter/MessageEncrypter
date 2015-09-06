using System;

namespace MessageEncrypter.Crypto
{
    static class Confirmation
    {
        private static class MessageFormats
        {
            public const string PublicKeyOverwrite = "A public key with the given identifier already exists. Do you want to overwrite it?";
        }

        public static ConfirmationEventArgs PublicKeyOverwrite()
        {
            return new ConfirmationEventArgs(MessageFormats.PublicKeyOverwrite);
        }
    }
}
