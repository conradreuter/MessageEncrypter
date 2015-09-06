using System;
using System.Windows.Forms;

namespace MessageEncrypter.UserInterface
{
    static class CenteringBehavior
    {
        public static void AttachCenteringBehavior(this Control control, Control centerAboveControl)
        {
            var impl = new Impl(control, centerAboveControl);
            impl.Attach();
        }

        private class Impl
        {
            private readonly Control control;
            private readonly Control centerAboveControl;

            public Impl(Control control, Control centerAboveControl)
            {
                this.control = control;
                this.centerAboveControl = centerAboveControl;
            }

            internal void Attach()
            {
                control.Resize += Control_Resize;
                centerAboveControl.Resize += Control_Resize;
            }

            private void Control_Resize(object sender, EventArgs e)
            {
                control.Left = centerAboveControl.Left + (centerAboveControl.Width - control.Width) / 2;
                control.Top = centerAboveControl.Top + (centerAboveControl.Height - control.Height) / 2;
            }
        }
    }
}
