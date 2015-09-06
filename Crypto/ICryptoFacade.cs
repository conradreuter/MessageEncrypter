using System;

namespace MessageEncrypter.Crypto
{
    interface ICryptoFacade
    {
        event EventHandler<PublicKeyEventArgs> NewPublicKey;
        event EventHandler<PublicKeyEventArgs> RemovePublicKey;
        event EventHandler<OutputEventArgs> Output;
        event EventHandler<ConfirmationEventArgs> NeedConfirmation;

        void Initialise();
        InputType DetermineInputType(string input);

        void CopyMyPublicKeyToClipboard();
        void ImportPublicKey(string publicKeyInput, string description);
        void EncryptMessage(string message, IPublicKey publicKey);
        void DecryptMessage(string encryptedMessageInput);
    }
}
