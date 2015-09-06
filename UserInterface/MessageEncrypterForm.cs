using MessageEncrypter.Crypto;
using System;
using System.Windows.Forms;

namespace MessageEncrypter.UserInterface
{
    partial class MessageEncrypterForm : Form
    {
        private static class Placeholders
        {
            public const string Input = "Enter a message for encryption or paste an encrypted message or a public key...";
            public const string PublicKeyDescription = "Enter a description for the public key and press Enter...";
            public const string PublicKeyForEncryption = "Select a public key for encryption...";
        }

        private static class Messages
        {
            public const string PublicKeyCopiedToClipboard = "Public key copied to clipboard";
            public const string PublicKeyImported = "Public key imported";
            public const string MessageEncrypted = "Message encrypted and copied to clipboard";
            public const string MessageDecrypted = "Message decrypted and copied to clipboard";
        }

        private readonly ICryptoFacade cryptoFacade;
        private readonly Control[] controlsShownBasedOnInputType;
        private InputType currentInputType;
        private bool hasLastInputTextBoxEventBeenTriggeredByThis;

        public MessageEncrypterForm(ICryptoFacade cryptoFacade)
        {
            InitializeComponent();

            this.cryptoFacade = cryptoFacade;
            this.currentInputType = InputType.Empty;
            this.controlsShownBasedOnInputType = new Control[]
            {
                copyMyPublicKeyButton,
                importPublicKeyDescriptionTextBox,
                publicKeyForEncryptionComboBox,
                decryptMessageButton
            };
            this.hasLastInputTextBoxEventBeenTriggeredByThis = false;

            AttachCryptoFacadeEventHandlers();
            AttachControlBehaviors();
            AdjustUserInterface();
        }

        private void AttachCryptoFacadeEventHandlers()
        {
            cryptoFacade.NewPublicKey += cryptoFacade_NewPublicKey;
            cryptoFacade.RemovePublicKey += cryptoFacade_RemovePublicKey;
            cryptoFacade.Output += cryptoFacade_Output;
            cryptoFacade.NeedConfirmation += cryptoFacade_NeedConfirmation;
        }

        private void cryptoFacade_NewPublicKey(object sender, PublicKeyEventArgs e)
        {
            publicKeyForEncryptionComboBox.Items.Add(e.PublicKey);
        }

        private void cryptoFacade_RemovePublicKey(object sender, PublicKeyEventArgs e)
        {
            publicKeyForEncryptionComboBox.Items.Remove(e.PublicKey);
        }

        private void cryptoFacade_Output(object sender, OutputEventArgs e)
        {
            switch (e.Type)
            {
                case OutputType.MyPublicKey:
                    Clipboard.SetText(e.Output);
                    ShowNotification(Messages.PublicKeyCopiedToClipboard);
                    RestoreUserInterfaceDefaults();
                    break;

                case OutputType.ImportedPublicKey:
                    ShowNotification(Messages.PublicKeyImported);
                    RestoreUserInterfaceDefaults();
                    break;

                case OutputType.EncryptedMessage:
                    Clipboard.SetText(e.Output);
                    ShowNotification(Messages.MessageEncrypted);
                    RestoreUserInterfaceDefaults();
                    WriteOutputToInputTextBox(e.Output);
                    break;

                case OutputType.DecryptedMessage:
                    Clipboard.SetText(e.Output);
                    ShowNotification(Messages.MessageDecrypted);
                    RestoreUserInterfaceDefaults();
                    WriteOutputToInputTextBox(e.Output);
                    break;
            }
        }

        private void ShowNotification(string message)
        {
            notificationPopup.Show(message);
        }

        private void WriteOutputToInputTextBox(string output)
        {
            this.hasLastInputTextBoxEventBeenTriggeredByThis = true;
            inputTextBox.IgnoreEvents(() =>
            {
                inputTextBox.Select();
                inputTextBox.Text = output;
                inputTextBox.SelectAll();
            });
            this.hasLastInputTextBoxEventBeenTriggeredByThis = false;
        }

        private void cryptoFacade_NeedConfirmation(object sender, ConfirmationEventArgs e)
        {
            var result = MessageBox.Show(e.Message, string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            e.Confirmed = result == DialogResult.Yes;
        }

        private void AttachControlBehaviors()
        {
            inputTextBox.AttachPlaceholderBehavior(Placeholders.Input);
            importPublicKeyDescriptionTextBox.AttachPlaceholderBehavior(Placeholders.PublicKeyDescription);
            publicKeyForEncryptionComboBox.AttachPlaceholderBehavior(Placeholders.PublicKeyForEncryption);
            notificationPopup.AttachCenteringBehavior(inputTextBox);
        }

        private void MessageEncrypterForm_Load(object sender, EventArgs e)
        {
            cryptoFacade.Initialise();
            RestoreUserInterfaceDefaults();
        }

        private void inputTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!hasLastInputTextBoxEventBeenTriggeredByThis && !inputTextBox.HasLastEventBeenTriggeredByPlaceholderBehavior())
            {
                UpdateInputType();
            }
        }

        private void inputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Control)
            {
                inputTextBox.SelectAll();
                e.Handled = true;
            }
        }

        private void UpdateInputType()
        {
            var inputOrNull = inputTextBox.GetTextOrNullInCaseOfPlaceholder();
            var inputType = cryptoFacade.DetermineInputType(inputOrNull);
            if (inputType != currentInputType)
            {
                this.currentInputType = inputType;
                AdjustUserInterface();
            }
        }

        private void AdjustUserInterface()
        {
            switch (currentInputType)
            {
                case InputType.Empty:
                    AdjustUserInterfaceForPublicKeyCopying();
                    break;

                case InputType.PublicKey:
                    AdjustUserInterfaceForPublicKeyImport();
                    break;

                case InputType.Message:
                    AdjustUserInterfaceForEncryption();
                    break;

                case InputType.EncryptedMessage:
                    AdjustUserInterfaceForDecryption();
                    break;

                default:
                    RestoreUserInterfaceDefaults();
                    break;
            }
        }

        private void AdjustUserInterfaceForPublicKeyCopying()
        {
            HideEverythingExceptFor(copyMyPublicKeyButton);
        }

        private void AdjustUserInterfaceForPublicKeyImport()
        {
            HideEverythingExceptFor(importPublicKeyDescriptionTextBox);
        }

        private void AdjustUserInterfaceForEncryption()
        {
            publicKeyForEncryptionComboBox.RestorePlaceholder();
            HideEverythingExceptFor(publicKeyForEncryptionComboBox);
        }

        private void AdjustUserInterfaceForDecryption()
        {
            HideEverythingExceptFor(decryptMessageButton);
        }

        private void RestoreUserInterfaceDefaults()
        {
            AdjustUserInterfaceForPublicKeyCopying();
            RemoveFocusFromAllElements();
            inputTextBox.RestorePlaceholder();
        }

        private void RemoveFocusFromAllElements()
        {
            emptyLabel.Select();
        }

        private void HideEverythingExceptFor(Control visibleControl)
        {
            foreach (var control in controlsShownBasedOnInputType)
            {
                control.Visible = control == visibleControl;
            }
        }

        private void copyMyPublicKeyButton_Click(object sender, EventArgs e)
        {
            cryptoFacade.CopyMyPublicKeyToClipboard();
        }

        private void publicKeyDescriptionTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var publicKeyInput = inputTextBox.Text;
                var descriptionOrNull = importPublicKeyDescriptionTextBox.GetTextOrNullInCaseOfPlaceholder();
                var description = (descriptionOrNull ?? string.Empty).Trim();
                if (!string.IsNullOrEmpty(description))
                {
                    cryptoFacade.ImportPublicKey(publicKeyInput, description);
                    importPublicKeyDescriptionTextBox.Clear();
                    RestoreUserInterfaceDefaults();
                }
                else
                {
                    importPublicKeyDescriptionTextBox.Text = description;
                    importPublicKeyDescriptionTextBox.Select();
                }
                e.Handled = true;
            }
        }

        private void publicKeyForEncryptionComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            var message = inputTextBox.Text;
            var publicKey = publicKeyForEncryptionComboBox.SelectedItem as IPublicKey;
            if (publicKey != null)
            {
                cryptoFacade.EncryptMessage(message, publicKey);
            }
        }

        private void decryptMessageButton_Click(object sender, EventArgs e)
        {
            var encryptedMessageInput = inputTextBox.Text;
            cryptoFacade.DecryptMessage(encryptedMessageInput);
        }
    }
}
