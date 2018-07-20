using System;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing;

namespace Rad_Escape
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private DispatcherTimer Updater = new DispatcherTimer();
        private TimerClass Timer;
        private GameWindowClass GameWindow;

        private string currentText;
        public string CurrentText { get { return currentText; } set { currentText = value; OnPropertyChanged("CurrentText"); } }

        public MainWindow()
        {
            InitializeUpdater();
            InitializeComponent();
            Timer = new TimerClass();
        }

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
            if (Timer.IsActive)
            {
                timerSetButton.IsEnabled = false;
            }
            else
            {
                timerSetButton.IsEnabled = true;
            }
        }

        private void timerSetButton_Click(object sender, RoutedEventArgs e)
        {
            Timer.SetTimer(1, 0, 0);
        }

        private void timerStartStopButton_Click(object sender, RoutedEventArgs e)
        {
            Timer.ToggleTimer();
        }

        private void timerCompleteButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void showOverlay_Click(object sender, RoutedEventArgs e)
        {
            if (GameWindow == null) // Create new instance of GameWindow if its not make and return
            {
                GameWindow = new GameWindowClass(ref this.Timer);
                GameWindow.Show();
                updateImageBackground();
                return;
            }
            if (GameWindow.Visibility == Visibility.Collapsed)
            {
                GameWindow.Visibility = Visibility.Visible;
            }
            else
            {
                GameWindow.Visibility = Visibility.Collapsed;
            }
            updateImageBackground();
        }

        public void updateImageBackground()
        {
            if (BackgroundPath.PathBoxText == "")
            {
                var img = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + Properties.Settings.Default.DefaultBackgroundPath));
                img.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                GameWindow.BackgroundBitmapImage = img;
            }
            else
            {
                var img = new BitmapImage(new Uri(BackgroundPath.PathBoxText));
                img.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                GameWindow.BackgroundBitmapImage = img;
            }
        }

        public void LoadSettings()
        {
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

        private void UpdateBackgroundButton_Click(object sender, RoutedEventArgs e)
        {
            updateImageBackground();
        }
    }
}