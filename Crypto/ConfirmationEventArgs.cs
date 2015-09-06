using System;

namespace MessageEncrypter.Crypto
{
    class ConfirmationEventArgs : EventArgs
    {
        public string Message { get; private set; }
        public bool Confirmed { get; set; }

        public ConfirmationEventArgs(string format, params object[] args)
        {
            this.Message = string.Format(format, args);
            this.Confirmed = false;
        }
    }
}
