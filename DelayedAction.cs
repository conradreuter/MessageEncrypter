using System;
using System.Timers;

namespace MessageEncrypter.UserInterface
{
    static class ActionDelay
    {
        public static void Delay(this Action action, int delayInMilliseconds)
        {
            var timer = new Timer
            {
                Interval = delayInMilliseconds,
                AutoReset = false
            };
            timer.Elapsed += delegate
            {
                timer.Dispose();
                action();
            };
            timer.Start();
        }
    }
}
