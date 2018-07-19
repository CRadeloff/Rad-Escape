﻿using System;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using System.IO;

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
            Timer.SetTimer();
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
            if (GameWindow == null)
            {
                GameWindow = new GameWindowClass(ref this.Timer);
                setGameWindowBackground();
                GameWindow.Show();
            }
        }

        public void setGameWindowBackground()
        {
            GameWindow.BackgroundImage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\resources\images\DefaultBackground.jpg"));
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