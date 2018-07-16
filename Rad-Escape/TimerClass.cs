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
        public DateTime FutureTime = new DateTime();
        public TimeSpan TimeLeft;
        private bool TimerIsActive;
        public string TimeFormat = @"hh\:mm\:ss\.ff";
        public string VictoryMessage = "You've escaped!";
        public string DefeatMessage = "Times up!";
        private MainWindow CurrentWindow;

        public TimerClass(MainWindow mainWindow)
        {
            TimerIsActive = false;
            Timer.Interval = TimeSpan.FromMilliseconds(1);
            Timer.Tick += timer_Tick;
            CurrentWindow = mainWindow;
        }

        public void setTimer(int startingHours, int startingMinutes, int startingSeconds)
        {
            TimeLeft = new TimeSpan(startingHours, startingMinutes, startingSeconds);
        }

        public void startStopTimer()
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

        public void timeUp()
        {
            Timer.Stop();
            return;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (FutureTime <= DateTime.Now)
            {
                timeUp();
                FutureTime = DateTime.Now;
                return;
            }
            CurrentWindow.CurrentText = FutureTime.Subtract(DateTime.Now).ToString(TimeFormat);
        }

        public void addTimer(TimeSpan AddTime)
        {
            FutureTime += AddTime;
        }
    }
}