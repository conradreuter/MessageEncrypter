namespace MessageEncrypter.UserInterface
{
    partial class MessageEncrypterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TableLayoutPanel layoutPanel;
            System.Windows.Forms.Panel actionPanel;
            System.Windows.Forms.Panel inputAndNotificationPanel;
            this.importPublicKeyDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.decryptMessageButton = new System.Windows.Forms.Button();
            this.publicKeyForEncryptionComboBox = new System.Windows.Forms.ComboBox();
            this.copyMyPublicKeyButton = new System.Windows.Forms.Button();
            this.emptyLabel = new System.Windows.Forms.Label();
            this.notificationPopup = new MessageEncrypter.UserInterface.NotificationPopup();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            layoutPanel = new System.Windows.Forms.TableLayoutPanel();
            actionPanel = new System.Windows.Forms.Panel();
            inputAndNotificationPanel = new System.Windows.Forms.Panel();
            layoutPanel.SuspendLayout();
            actionPanel.SuspendLayout();
            inputAndNotificationPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutPanel
            // 
            layoutPanel.ColumnCount = 1;
            layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            layoutPanel.Controls.Add(actionPanel, 0, 2);
            layoutPanel.Controls.Add(this.emptyLabel, 0, 0);
            layoutPanel.Controls.Add(inputAndNotificationPanel, 0, 1);
            layoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            layoutPanel.Location = new System.Drawing.Point(0, 0);
            layoutPanel.Name = "layoutPanel";
            layoutPanel.RowCount = 3;
            layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            layoutPanel.Size = new System.Drawing.Size(298, 243);
            layoutPanel.TabIndex = 0;
            // 
            // actionPanel
            // 
            actionPanel.Controls.Add(this.importPublicKeyDescriptionTextBox);
            actionPanel.Controls.Add(this.decryptMessageButton);
            actionPanel.Controls.Add(this.publicKeyForEncryptionComboBox);
            actionPanel.Controls.Add(this.copyMyPublicKeyButton);
            actionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            actionPanel.Location = new System.Drawing.Point(3, 214);
            actionPanel.Name = "actionPanel";
            actionPanel.Size = new System.Drawing.Size(292, 26);
            actionPanel.TabIndex = 3;
            // 
            // importPublicKeyDescriptionTextBox
            // 
            this.importPublicKeyDescriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.importPublicKeyDescriptionTextBox.Location = new System.Drawing.Point(0, 0);
            this.importPublicKeyDescriptionTextBox.Name = "importPublicKeyDescriptionTextBox";
            this.importPublicKeyDescriptionTextBox.Size = new System.Drawing.Size(292, 20);
            this.importPublicKeyDescriptionTextBox.TabIndex = 1;
            this.importPublicKeyDescriptionTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.publicKeyDescriptionTextBox_KeyUp);
            // 
            // decryptMessageButton
            // 
            this.decryptMessageButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.decryptMessageButton.Location = new System.Drawing.Point(0, 0);
            this.decryptMessageButton.Name = "decryptMessageButton";
            this.decryptMessageButton.Size = new System.Drawing.Size(292, 26);
            this.decryptMessageButton.TabIndex = 5;
            this.decryptMessageButton.Text = "Decrypt the message";
            this.decryptMessageButton.UseVisualStyleBackColor = true;
            this.decryptMessageButton.Visible = false;
            this.decryptMessageButton.Click += new System.EventHandler(this.decryptMessageButton_Click);
            // 
            // publicKeyForEncryptionComboBox
            // 
            this.publicKeyForEncryptionComboBox.DisplayMember = "Description";
            this.publicKeyForEncryptionComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.publicKeyForEncryptionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.publicKeyForEncryptionComboBox.FormattingEnabled = true;
            this.publicKeyForEncryptionComboBox.Location = new System.Drawing.Point(0, 0);
            this.publicKeyForEncryptionComboBox.Name = "publicKeyForEncryptionComboBox";
            this.publicKeyForEncryptionComboBox.Size = new System.Drawing.Size(292, 21);
            this.publicKeyForEncryptionComboBox.TabIndex = 3;
            this.publicKeyForEncryptionComboBox.ValueMember = "Description";
            this.publicKeyForEncryptionComboBox.Visible = false;
            this.publicKeyForEncryptionComboBox.SelectedValueChanged += new System.EventHandler(this.publicKeyForEncryptionComboBox_SelectedValueChanged);
            // 
            // copyMyPublicKeyButton
            // 
            this.copyMyPublicKeyButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.copyMyPublicKeyButton.Location = new System.Drawing.Point(0, 0);
            this.copyMyPublicKeyButton.Name = "copyMyPublicKeyButton";
            this.copyMyPublicKeyButton.Size = new System.Drawing.Size(292, 26);
            this.copyMyPublicKeyButton.TabIndex = 1;
            this.copyMyPublicKeyButton.Text = "Copy my public key to clipboard";
            this.copyMyPublicKeyButton.UseVisualStyleBackColor = true;
            this.copyMyPublicKeyButton.Click += new System.EventHandler(this.copyMyPublicKeyButton_Click);
            // 
            // emptyLabel
            // 
            this.emptyLabel.AutoSize = true;
            this.emptyLabel.Location = new System.Drawing.Point(3, 0);
            this.emptyLabel.Name = "emptyLabel";
            this.emptyLabel.Size = new System.Drawing.Size(0, 1);
            this.emptyLabel.TabIndex = 4;
            // 
            // inputAndNotificationPanel
            // 
            inputAndNotificationPanel.Controls.Add(this.notificationPopup);
            inputAndNotificationPanel.Controls.Add(this.inputTextBox);
            inputAndNotificationPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            inputAndNotificationPanel.Location = new System.Drawing.Point(3, 3);
            inputAndNotificationPanel.Name = "inputAndNotificationPanel";
            inputAndNotificationPanel.Size = new System.Drawing.Size(292, 205);
            inputAndNotificationPanel.TabIndex = 5;
            // 
            // notificationPopup
            // 
            this.notificationPopup.AutoSize = true;
            this.notificationPopup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.notificationPopup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.notificationPopup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notificationPopup.ForeColor = System.Drawing.Color.White;
            this.notificationPopup.Location = new System.Drawing.Point(0, 0);
            this.notificationPopup.Name = "notificationPopup";
            this.notificationPopup.Size = new System.Drawing.Size(82, 21);
            this.notificationPopup.TabIndex = 1;
            this.notificationPopup.Visible = false;
            // 
            // inputTextBox
            // 
            this.inputTextBox.AcceptsReturn = true;
            this.inputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputTextBox.Location = new System.Drawing.Point(0, 0);
            this.inputTextBox.Multiline = true;
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(292, 205);
            this.inputTextBox.TabIndex = 7;
            this.inputTextBox.TextChanged += new System.EventHandler(this.inputTextBox_TextChanged);
            this.inputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputTextBox_KeyDown);
            // 
            // MessageEncrypterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 243);
            this.Controls.Add(layoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageEncrypterForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Message Encrypter";
            this.Load += new System.EventHandler(this.MessageEncrypterForm_Load);
            layoutPanel.ResumeLayout(false);
            layoutPanel.PerformLayout();
            actionPanel.ResumeLayout(false);
            actionPanel.PerformLayout();
            inputAndNotificationPanel.ResumeLayout(false);
            inputAndNotificationPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button copyMyPublicKeyButton;
        private System.Windows.Forms.TextBox importPublicKeyDescriptionTextBox;
        private System.Windows.Forms.ComboBox publicKeyForEncryptionComboBox;
        private System.Windows.Forms.Button decryptMessageButton;
        private System.Windows.Forms.Label emptyLabel;
        private System.Windows.Forms.TextBox inputTextBox;
        private NotificationPopup notificationPopup;
    }
}

