using System;

namespace MessageEncrypter
{
    class ApplicationException : Exception
    {
        private static class MessageFormats
        {
            public const string KeyPairIdentifierMismatch = "Your identifier does not match the one given in the " + 
                "encrypted message. You do not seem to be the intended recipient.";
        }

        private ApplicationException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public static ApplicationException KeyPairIdentifierMismatch()
        {
            return new ApplicationException(MessageFormats.KeyPairIdentifierMismatch);
        }
    }
}
