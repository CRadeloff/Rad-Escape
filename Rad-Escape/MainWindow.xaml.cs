using System;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using System.IO;
using System.Collections.Generic;
using System.Drawing;

namespace Rad_Escape
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private UtilitiesClass Utilities = new UtilitiesClass();
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
            GameWindow = new GameWindowClass(ref Timer);
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
            clearCompletePage();
            timerStartStopButton.IsEnabled = true;
            Timer.SetTimer(1, 0, 0);
        }

        private void clearCompletePage()
        {
            GameWindow.DisplayFrame.Content = null;
        }

        private void timerStartStopButton_Click(object sender, RoutedEventArgs e)
        {
            Timer.ToggleTimer();
        }

        private void timerCompleteButton_Click(object sender, RoutedEventArgs e)
        {
            Timer.PauseTimer();
            timerStartStopButton.IsEnabled = false;
            GameWindow.DisplayFrame.Content = new CompletePage("GameName", Timer.TimeLeft, "Bottom Message", 3);
        }

        private void showOverlay_Click(object sender, RoutedEventArgs e)
        {
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
            if (Properties.Settings.Default.BackgroundPath == "")
            {
                var img = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + Properties.Settings.Default.DefaultBackgroundPath));
                img.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                GameWindow.BackgroundBitmapImage = img;
            }
            else
            {
                var img = new BitmapImage(new Uri(Properties.Settings.Default.BackgroundPath));
                img.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                GameWindow.BackgroundBitmapImage = img;
            }
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

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //Ensures windows that may be hidden are closed when the user presses the exit button. Provides confirmation first
            MessageBoxResult result = MessageBox.Show("Are you sure you wish to close Rad-Escape?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
            updateImageBackground();
            updateClueImages();
            MessageBox.Show("Settings Saved");
        }

        private void updateClueImages()
        {
        }

        private void BackgroundPathButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.BackgroundPath = Utilities.choosePath();
        }

        private void ClueImagePathButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ClueImagePath = Utilities.choosePath();
        }

        private void ClueUsedImagePathButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ClueUsedImagePath = Utilities.choosePath();
        }

        private void ResetSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you wish to revert ALL settings to their default values?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                Properties.Settings.Default.Reset();
            }
        }
    }
}