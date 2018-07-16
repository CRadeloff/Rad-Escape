using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Rad_Escape
{
    public class TimerClass
    {
        private DispatcherTimer Timer = new DispatcherTimer();
        private DateTime FutureTime = new DateTime();
        private TimeSpan TimeLeft;
        private bool TimerIsActive;
        private string TimeFormat = @"hh\:mm\:ss\.ff";
        public string VictoryMessage = "You've escaped!";
        public string DefeatMessage = "Times up!";
        private MainWindow CurrentWindow;

        public TimerClass(MainWindow mainWindow)
        {
            TimerIsActive = false;
            Timer.Interval = TimeSpan.FromMilliseconds(1);
            Timer.Tick += Timer_Tick;
            CurrentWindow = mainWindow;
        }

        public void SetTimer(int startingHours, int startingMinutes, int startingSeconds)
        {
            TimeLeft = new TimeSpan(startingHours, startingMinutes, startingSeconds);
        }

        public void PauseUnpauseTimer()
        {
            if (TimerIsActive) //pause timer and set the timeleft
            {
                Timer.Stop();
                TimeLeft = FutureTime.Subtract(DateTime.Now);
                TimerIsActive = false;
            }
            else //unpause timer
            {
                FutureTime = DateTime.Now.Add(TimeLeft);
                Timer.Start();
                TimerIsActive = true;
            }
        }

        public void TimeUp()
        {
            Timer.Stop();
            return;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (FutureTime <= DateTime.Now)
            {
                TimeUp();
                FutureTime = DateTime.Now;
                return;
            }
            CurrentWindow.CurrentText = FutureTime.Subtract(DateTime.Now).ToString(TimeFormat);
        }

        public void AddTime(TimeSpan AddTime)
        {
            FutureTime += AddTime;
        }
    }
}