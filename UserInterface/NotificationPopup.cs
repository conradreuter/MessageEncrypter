using System;
using System.Windows.Forms;

namespace MessageEncrypter.UserInterface
{
    partial class NotificationPopup : UserControl
    {
        private static class DurationsInMilliseconds
        {
            public const int ShowFully = 500;
            public const int Fadeout = 300;
        }

        public NotificationPopup()
        {
            InitializeComponent();
            this.Visible = false;
        }

        public void Show(string message)
        {
            this.SuspendLayout();
            label.Text = message;
            this.Visible = true;
            this.ResumeLayout();

            ((Action)Fadeout).Delay(DurationsInMilliseconds.ShowFully);
        }

        public void Fadeout()
        {
            this.Fadeout(DurationsInMilliseconds.Fadeout, FadeoutFinished);
        }

        private void FadeoutFinished(bool hasBeenAborted)
        {
            this.Invoke((MethodInvoker)(() =>
            {
                this.Visible = false;
            }));
        }
    }
}
