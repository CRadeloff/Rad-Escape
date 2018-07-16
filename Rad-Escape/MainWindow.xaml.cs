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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private TimerClass Timer;

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

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            Timer = new TimerClass(this);
        }

        private void timerSetButton_Click(object sender, RoutedEventArgs e)
        {
            Timer.setTimer(1, 0, 0);
            TimeSpan t = new TimeSpan(1, 0, 0);
            CurrentText = t.ToString(Timer.TimeFormat);
        }

        private void timerStartStopButton_Click(object sender, RoutedEventArgs e)
        {
            Timer.startStopTimer();
        }

        private void timerEndbutton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void showOverlay_Click(object sender, RoutedEventArgs e)
        {
        }

        #region Event binding things

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

        #endregion Event binding things
    }
}