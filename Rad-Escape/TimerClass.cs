using System;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using System.Drawing;

namespace Rad_Escape
{
    public class TimerClass
    {
        public string TimeFormatString = @"hh\:mm\:ss\.ff";
        public string VictoryMessage = "You've escaped!";
        public string DefeatMessage = "Times up!";
        public Color TextColor;
        private DispatcherTimer Timer = new DispatcherTimer();

        // We are letting the timer handle whats being shown.
        // By setting useCustomMessage we can easily switch between what we want to display
        // without too much of a headache.

        public DateTime FutureTime { get; set; }
        public TimeSpan timeLeft;
        public TimeSpan TimeLeft { get { return timeLeft; } set { FutureTime = DateTime.Now.Add(value); timeLeft = value; } }
        public bool IsActive { get; set; }

        public string CurrentText { get; set; }

        private string currentTextContent;

        private string CurrentTextContent
        {
            get
            {
                if (IsActive)
                {
                    currentTextContent = CurrentTimeLeft();
                    return currentTextContent;
                }
                updateFutureTime();
                return currentTextContent;
            }
            set
            {
                currentTextContent = value;
            }
        }

        public string CurrentTimeLeft()
        {
            TimeLeft = FutureTime.Subtract(DateTime.Now);
            return FutureTime.Subtract(DateTime.Now).ToString(TimeFormatString);
        }

        private void updateFutureTime()
        {
            FutureTime = DateTime.Now.Add(TimeLeft);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            CurrentText = CurrentTextContent;
        }

        public TimerClass()
        {
            // Setting up our timer properties
            IsActive = false;
            Timer.Interval = TimeSpan.FromMilliseconds(1);
            Timer.Tick += timer_Tick;
            Timer.Start();
            CurrentTextContent = "Timer Ready.";
        }

        public void SetTimer()
        {
            CurrentTextContent = new TimeSpan(1, 0, 0).ToString(TimeFormatString);
            TimeLeft = new TimeSpan(1, 0, 0);
        }

        public void ToggleTimer()
        {
            if (IsActive)
            {
                PauseTimer();
            }
            else
            {
                StartTimer();
            }
        }

        public void StartTimer()
        {
            IsActive = true;
        }

        public void PauseTimer()
        {
            IsActive = false;
        }
    }
}