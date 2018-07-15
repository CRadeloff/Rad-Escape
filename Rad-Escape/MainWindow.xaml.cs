using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Windows.Threading;

namespace Rad_Escape
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TimerClass Timer;

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            Timer = new TimerClass();
            Timer.SetTimer(1, 0, 0);
        }

        public class TimerClass : INotifyPropertyChanged

        {
            private DispatcherTimer Timer = new DispatcherTimer();
            private DateTime FutureTime = new DateTime();
            private TimeSpan TimeLeft;
            private bool TimerIsActive;
            private string TimeFormat = @"hh\:mm\:ss\.ff";
            public string VictoryMessage = "You've escaped!";
            public string DefeatMessage = "Times up!";

            private string currentText = "test";

            public string CurrentText
            {
                get { return currentText; }
                set
                {
                    currentText = value;
                    OnPropertyChanged();
                }
            }

            public TimerClass()
            {
                TimerIsActive = false;
                Timer.Interval = TimeSpan.FromMilliseconds(1);
                Timer.Tick += Timer_Tick;
                CurrentText = currentText;
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
                CurrentText = FutureTime.Subtract(DateTime.Now).ToString(TimeFormat);
            }

            public void AddTime(TimeSpan AddTime)
            {
                FutureTime += AddTime;
            }

            public event PropertyChangedEventHandler PropertyChanged;

            /// <summary>
            /// Raises this object's PropertyChanged event.
            /// </summary>
            /// <param name="propertyName">The property that has a new value.</param>
            private void OnPropertyChanged([CallerMemberName]string propertyName = null)
            {
                PropertyChangedEventHandler handler = this.PropertyChanged;
                if (handler != null)
                {
                    var e = new PropertyChangedEventArgs(propertyName);
                    handler(this, e);
                }
            }
        }

        private void timerSetButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void timerStartStopButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void timerEndbutton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void showOverlay_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}