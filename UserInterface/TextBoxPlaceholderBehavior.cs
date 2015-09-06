using System;
using System.Drawing;
using System.Windows.Forms;

namespace MessageEncrypter.UserInterface
{
    class TextBoxPlaceholderBehavior : IPlaceholderBehavior
    {
        private static readonly Color PlaceholderForecolor = SystemColors.GrayText;

        private readonly object eventLock = new object();
        private readonly TextBox textBox;
        private readonly Color originalForeColor;
        private readonly string placeholder;

        public bool IsPlaceholderActive { get; private set; }
        public bool IgnoreEvents { private get; set; }
        public bool HasLastEventBeenTriggeredByThis { get; private set; }

        public TextBoxPlaceholderBehavior(TextBox textBox, string placeholder)
        {
            this.textBox = textBox;
            this.originalForeColor = textBox.ForeColor;
            this.placeholder = placeholder;
            this.IsPlaceholderActive = false;
            this.IgnoreEvents = false;
        }

        public void Attach()
        {
            InsertPlaceholderIfNecessary();
            AttachTextBoxEvents();
        }

        private void AttachTextBoxEvents()
        {
            textBox.GotFocus += textBox_GotFocus;
            textBox.LostFocus += textBox_LostFocus;
            if (textBox.Focused)
            {
                textBox.TextChanged += textBox_TextChanged;
            }
        }

        public void RestorePlaceholder()
        {
            if (!textBox.Focused)
            {
                this.IsPlaceholderActive = true;
                ChangeTextBoxText(placeholder);
            }
        }

        private void textBox_GotFocus(object sender, EventArgs e)
        {
            RemovePlaceholder();
            textBox.TextChanged += textBox_TextChanged;
        }

        private void textBox_LostFocus(object sender, EventArgs e)
        {
            textBox.TextChanged -= textBox_TextChanged;
            InsertPlaceholderIfNecessary();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (!IgnoreEvents)
            {
                this.IsPlaceholderActive = false;
            }
        }

        private void InsertPlaceholderIfNecessary()
        {
            var textBoxIsEmpty = string.IsNullOrEmpty(textBox.Text);
            if (textBoxIsEmpty)
            {
                ChangeTextBoxText(placeholder);
                this.IsPlaceholderActive = true;
                textBox.ForeColor = PlaceholderForecolor;
            }
        }

        private void RemovePlaceholder()
        {
            if (IsPlaceholderActive)
            {
                ChangeTextBoxText(string.Empty);
                this.IsPlaceholderActive = false;
            }
            textBox.ForeColor = originalForeColor;
        }

        private void ChangeTextBoxText(string text)
        {
            this.HasLastEventBeenTriggeredByThis = true;
            textBox.Text = text;
            this.HasLastEventBeenTriggeredByThis = false;
        }
    }
}
