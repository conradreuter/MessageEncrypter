using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MessageEncrypter.UserInterface
{
    interface IPlaceholderBehavior
    {
        bool IsPlaceholderActive { get; }
        bool IgnoreEvents { set; }
        bool HasLastEventBeenTriggeredByThis { get; }

        void Attach();
        void RestorePlaceholder();
    }

    static class PlaceholderBehavior
    {
        private static readonly IDictionary<Control, IPlaceholderBehavior> placeholderBehaviorsByControl = new Dictionary<Control, IPlaceholderBehavior>();

        public static void AttachPlaceholderBehavior(this TextBox textBox, string placeholder)
        {
            var placeholderBehavior = new TextBoxPlaceholderBehavior(textBox, placeholder);
            placeholderBehaviorsByControl.Add(textBox, placeholderBehavior);
            placeholderBehavior.Attach();
        }

        public static void AttachPlaceholderBehavior(this ComboBox comboBox, string placeholder)
        {
            var placeholderBehavior = new ComboBoxPlaceholderBehavior(comboBox, placeholder);
            placeholderBehaviorsByControl.Add(comboBox, placeholderBehavior);
            placeholderBehavior.Attach();
        }

        public static string GetTextOrNullInCaseOfPlaceholder(this Control control)
        {
            if (PlaceholderBehaviorFor(control).IsPlaceholderActive)
            {
                return null;
            }
            else
            {
                return control.Text;
            }
        }

        public static void IgnoreEvents(this Control control, Action action)
        {
            PlaceholderBehaviorFor(control).IgnoreEvents = true;
            action();
            PlaceholderBehaviorFor(control).IgnoreEvents = false;
        }

        public static bool HasLastEventBeenTriggeredByPlaceholderBehavior(this Control control)
        {
            return PlaceholderBehaviorFor(control).HasLastEventBeenTriggeredByThis;
        }

        public static void RestorePlaceholder(this Control control)
        {
            PlaceholderBehaviorFor(control).RestorePlaceholder();
        }

        private static IPlaceholderBehavior PlaceholderBehaviorFor(Control control)
        {
            IPlaceholderBehavior placeholderBehavior;
            if (!placeholderBehaviorsByControl.TryGetValue(control, out placeholderBehavior))
            {
                throw new InvalidOperationException("The given control does not have a placeholder behavior attached.");
            }
            return placeholderBehavior;
        }
    }
}
