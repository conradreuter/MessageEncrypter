using System;
using System.Windows.Forms;

namespace MessageEncrypter.UserInterface
{
    class ComboBoxPlaceholderBehavior : IPlaceholderBehavior
    {
        private readonly ComboBox comboBox;
        private readonly string placeholder;

        public bool IsPlaceholderActive { get; private set; }
        public bool IgnoreEvents { private get; set; }
        public bool HasLastEventBeenTriggeredByThis { get;  private set; }

        public ComboBoxPlaceholderBehavior(ComboBox comboBox, string placeholder)
        {
            this.comboBox = comboBox;
            this.placeholder = placeholder;
            this.IgnoreEvents = false;
        }

        public void Attach()
        {
            InsertPlaceholderIfNecessary();
            AttachComboBoxEvents();
        }

        public void RestorePlaceholder()
        {
            if (!comboBox.Focused)
            {
                comboBox.SelectedItem = null;
                InsertPlaceholderIfNecessary();
            }
        }

        private void AttachComboBoxEvents()
        {
            comboBox.DropDown += comboBox_DropDown;
            comboBox.DropDownClosed += comboBox_DropDownClosed;
            comboBox.SelectedValueChanged += comboBox_SelectedValueChanged;
        }

        private void comboBox_DropDown(object sender, EventArgs e)
        {
            RemovePlaceholder();
        }

        private void comboBox_DropDownClosed(object sender, EventArgs e)
        {
            InsertPlaceholderIfNecessary();
        }

        private void comboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!IgnoreEvents)
            {
                InsertPlaceholderIfNecessary();
            }
        }

        private void InsertPlaceholderIfNecessary()
        {
            var isValidItemSelected = comboBox.SelectedItem != null;
            if (!isValidItemSelected)
            {
                if (!comboBox.Items.Contains(placeholder))
                {
                    comboBox.Items.Insert(0, placeholder);
                }
                comboBox.SelectedItem = placeholder;
                this.IsPlaceholderActive = true;
            }
        }

        private void RemovePlaceholder()
        {
            this.IsPlaceholderActive = false;
            comboBox.Items.Remove(placeholder);
        }
    }
}
