using System.Windows;
using System.ComponentModel;
using System;
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using System.IO;

namespace Rad_Escape
{
    /// <summary>
    /// Interaction logic for GameWindowClass.xaml
    /// </summary>
    public partial class GameWindowClass : Window, INotifyPropertyChanged
    {
        #region Updater Region

        private DispatcherTimer Updater = new DispatcherTimer();
        private TimerClass Timer;

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

        #endregion Updater Region

        public int numClues;

        public BitmapImage BackgroundBitmapImage { get { return backgroundBitmapImage; } set { backgroundBitmapImage = value; OnPropertyChanged("BackgroundBitmapImage"); } }
        private BitmapImage backgroundBitmapImage;

        public string CurrentText { get { return currentText; } set { currentText = value; OnPropertyChanged("CurrentText"); } }
        private string currentText;

        public GameWindowClass(ref TimerClass timer)
        {
            // We're passing the timer as a refrence so we can modify it inside our GameWindow.
            // This may need to be changed in the future, as if multiple instances of GameWindow
            // are created it may lead to unexpecte dbehavior. To be looked into.
            InitializeComponent();
            Timer = timer;
            InitializeUpdater();
        }

        public void updateBackground()
        {
            if (Properties.Settings.Default.BackgroundPath == "")
            {
                var img = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + Properties.Settings.Default.DefaultBackgroundPath));
                img.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                BackgroundBitmapImage = img;
            }
            else
            {
                var img = new BitmapImage(new Uri(Properties.Settings.Default.BackgroundPath));
                img.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                BackgroundBitmapImage = img;
            }
        }

        public void addClue()
        {
            ClueControl Clue = new ClueControl();
            Clue.updateClueImage();
            Clue.updateClueUsedImage();
            Clue.IsUsed = false;
            ClueStackPannel.Children.Add(Clue);
            numClues++;
        }

        public void useClue()
        {
            foreach (ClueControl item in ClueStackPannel.Children)
            {
                if (!item.IsUsed)
                {
                    item.IsUsed = true;
                    return;
                }
            }
        }

        public void giveClueBack()
        {
            // With a stack pannel we cant go to a certain index, so we have a public variable that keeps track of the
            // number of clues that are currently being used.
            int count = 0;
            int trackCount = 0;
            foreach (ClueControl Clue in ClueStackPannel.Children)
            {
                count++;
                if (Clue.IsUsed == false)
                {
                    foreach (ClueControl item in ClueStackPannel.Children)
                    {
                        trackCount++;
                        if (trackCount == count - 1)
                        {
                            item.IsUsed = false;
                            return;
                        }
                    }
                }
                else if (count == ClueStackPannel.Children.Count)
                {
                    Clue.IsUsed = false;
                }
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Collapsed;
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