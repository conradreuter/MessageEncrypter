using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace MessageEncrypter.UserInterface
{
    static class ControlFadeout
    {
        private static IDictionary<Control, FadeoutImpl> pendingFadeoutsByControl = new Dictionary<Control, FadeoutImpl>();

        public static void Fadeout(this Control control, int durationInMilliseconds)
        {
            control.Fadeout(durationInMilliseconds, null);
        }

        public static void Fadeout(this Control control, int durationInMilliseconds, Action<bool> fadeoutFinishCallback)
        {
            control.AbortExistingFadeoutIfNecessary();
            var fadeout = new FadeoutImpl(control, durationInMilliseconds, fadeoutFinishCallback);
            pendingFadeoutsByControl.Add(control, fadeout);
            fadeout.Run();
        }

        private static void AbortExistingFadeoutIfNecessary(this Control control)
        {
            FadeoutImpl existingFadeout;
            if (pendingFadeoutsByControl.TryGetValue(control, out existingFadeout))
            {
                existingFadeout.Abort();
            }
        }

        public static void AbortFadeout(this Control control)
        {
            FadeoutImpl existingFadeout;
            if (!pendingFadeoutsByControl.TryGetValue(control, out existingFadeout))
            {
                throw new InvalidOperationException("The given control does not have a fadeout attached.");
            }
            existingFadeout.Abort();
        }

        private class FadeoutImpl
        {
            private const int TimerIntervalInMilliseconds = 16;

            private readonly Control control;
            private readonly Color originalForeColor;
            private readonly Color originalBackColor;
            private readonly int durationInMilliseconds;
            private readonly Action<bool> fadeoutFinishCallback;
            private readonly Timer timer;
            private DateTime startTime;

            public FadeoutImpl(Control control,
                               int durationInMilliseconds,
                               Action<bool> fadeoutFinishCallback)
            {
                this.control = control;
                this.originalForeColor = control.ForeColor;
                this.originalBackColor = control.BackColor;
                this.durationInMilliseconds = durationInMilliseconds;
                this.fadeoutFinishCallback = fadeoutFinishCallback;
                this.timer = new Timer
                {
                    Interval = TimerIntervalInMilliseconds,
                    AutoReset = true
                };
                timer.Elapsed += timer_Elapsed;
            }

            private void timer_Elapsed(object sender, ElapsedEventArgs e)
            {
                var elapsedMilliseconds = (e.SignalTime - startTime).TotalMilliseconds;
                if (elapsedMilliseconds > durationInMilliseconds)
                {
                    CleanUp(false);
                }
                else
                {
                    var opacity = 1.0 - (elapsedMilliseconds / durationInMilliseconds);
                    SetOpacity(opacity);
                }
            }

            public void Abort()
            {
                CleanUp(true);
            }

            public void Run()
            {
                startTime = DateTime.Now;
                timer.Start();
            }

            private void SetOpacity(double opacity)
            {
                control.ForeColor = ApplyOpacity(originalForeColor, opacity);
                control.BackColor = ApplyOpacity(originalBackColor, opacity);
            }

            private static Color ApplyOpacity(Color originalColor, double opacity)
            {
                opacity = Math.Max(0.0, Math.Min(1.0, opacity));
                var alpha = (int)Math.Round(255.0 * opacity);
                return Color.FromArgb(alpha, originalColor);
            }

            public void CleanUp(bool hasBeenAborted)
            {
                timer.Stop();
                timer.Dispose();
                pendingFadeoutsByControl.Remove(control);
                SetOpacity(1.0);
                if (fadeoutFinishCallback != null)
                {
                    fadeoutFinishCallback(hasBeenAborted);
                }
            }
        }
    }
}
