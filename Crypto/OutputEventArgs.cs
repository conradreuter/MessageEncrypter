using System;

namespace MessageEncrypter.Crypto
{
    class OutputEventArgs : EventArgs
    {
        public OutputType Type { get; private set; }
        public string Output { get; private set; }

        public OutputEventArgs(OutputType type,
                               string output)
        {
            this.Type = type;
            this.Output = output;
        }
    }

    enum OutputType
    {
        MyPublicKey,
        ImportedPublicKey,
        EncryptedMessage,
        DecryptedMessage
    }
}
