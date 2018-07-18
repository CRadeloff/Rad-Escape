using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Windows.Threading;

namespace Rad_Escape
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window, INotifyPropertyChanged
    {
        private DispatcherTimer Updater = new DispatcherTimer();
        private TimerClass Timer;

        private string currentText;
        public string CurrentText { get { return currentText; } set { currentText = value; OnPropertyChanged("CurrentText"); } }

        private void InitializeUpdater()
        {
            Updater.Interval = TimeSpan.FromMilliseconds(1);
            Updater.Tick += updater_tick;
            Updater.Start();
            DataContext = this;
        }

        private void updater_tick(object sender, EventArgs e)
        {
            CurrentText = Timer.CurrentText;
        }

        public GameWindow(ref TimerClass timer)
        {
            InitializeComponent();
            Timer = timer;
            InitializeUpdater();
        }

        #region OnPropertyChanged things

        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion OnPropertyChanged things
    }
}