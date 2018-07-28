using System;
using System.Windows;
using System.ComponentModel;
using System.Windows.Threading;

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
            GameWindow.resetClues();
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
            GameWindow.refreshBackground();
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
            GameWindow.refreshBackground();
            GameWindow.refreshClueImages();
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

        private void addOneClue_Click(object sender, RoutedEventArgs e)
        {
            GameWindow.addClue();
        }

        private void useOneCluie_Click(object sender, RoutedEventArgs e)
        {
            GameWindow.useClue();
        }

        private void giveClueBack_Click(object sender, RoutedEventArgs e)
        {
            GameWindow.giveClueBack();
        }

        private void removeClueButton_Click(object sender, RoutedEventArgs e)
        {
            GameWindow.removeClue();
        }

        private void timerSetButton_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            clearCompletePage();
            Timer.PauseTimer();
            Timer.SetTimer(1, 0, 0);
            GameWindow.resetClues();
        }

        private void showHintButton_Click(object sender, RoutedEventArgs e)
        {
            GameWindow.showHint(HintTextBox.Text);
        }

        private void clearHintButton_Click(object sender, RoutedEventArgs e)
        {
            GameWindow.clearHint();
        }
    }
}