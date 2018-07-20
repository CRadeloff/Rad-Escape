using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Windows.Threading;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Windows.Forms;

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

        public struct ClueStruct
        {
            public bool isUsed;
            public string cluePath;
            public string clueUsedPath;
        }

        private List<ClueStruct> cluelist = new List<ClueStruct>();

        public BitmapImage BackgroundBitmapImage { get { return backgroundBitmapImage; } set { backgroundBitmapImage = value; OnPropertyChanged("BackgroundBitmapImage"); } }
        private BitmapImage backgroundBitmapImage;

        public string CurrentText { get { return currentText; } set { currentText = value; OnPropertyChanged("CurrentText"); } }
        private string currentText;

        public GameWindowClass(ref TimerClass timer)
        {
            InitializeComponent();
            Timer = timer;
            InitializeUpdater();
        }

        public void addClue()
        {
            ClueStruct clue = new ClueStruct();
            clue.cluePath = @"\resources\images\lock.png"; clue.isUsed = false;
            cluelist.Add(clue);
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
            e.Cancel = true;
            this.Visibility = Visibility.Collapsed;
        }
    }
}