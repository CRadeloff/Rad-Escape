using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Windows.Threading;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Collections.Generic;

namespace Rad_Escape
{
    /// <summary>
    /// Interaction logic for GameWindowClass.xaml
    /// </summary>
    public partial class GameWindowClass : Window, INotifyPropertyChanged
    {
        public struct ClueStruct
        {
            public bool isUsed;
            public string cluePath;
            public string clueUsedPath;
        }

        private List<ClueStruct> cluelist = new List<ClueStruct>();

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
    }
}