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
        public DateTime FutureTime = new DateTime();
        public TimeSpan TimeLeft;
        public bool TimerIsActive;
        public string TimeFormat = @"hh\:mm\:ss\.ff";
        public string VictoryMessage = "You've escaped!";
        public string DefeatMessage = "Times up!";
        private MainWindow CurrentWindow;
        private DispatcherTimer Timer = new DispatcherTimer();

        public TimerClass(MainWindow mainWindow)
        {
            TimerIsActive = false;
            Timer.Interval = TimeSpan.FromMilliseconds(10);
            Timer.Tick += timer_Tick;
            CurrentWindow = mainWindow;
        }

        public void resetTimer(int startingHours, int startingMinutes, int startingSeconds)
        {
            if (TimerIsActive)
            {
                Timer.Stop();
                TimerIsActive = false;
                TimeLeft = new TimeSpan(startingHours, startingMinutes, startingSeconds);
            }
            else
            {
                TimeLeft = new TimeSpan(startingHours, startingMinutes, startingSeconds);
            }
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

        public void completeRoom()
        {
            Timer.Stop();
            TimerIsActive = false;
            CurrentWindow.CurrentText = VictoryMessage;
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

        public void addTime(TimeSpan time)
        {
            FutureTime += time;
        }
    }
}