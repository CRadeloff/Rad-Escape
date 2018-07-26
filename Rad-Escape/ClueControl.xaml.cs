using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rad_Escape
{
    /// <summary>
    /// Interaction logic for ClueControl.xaml
    /// </summary>
    public partial class ClueControl : UserControl, INotifyPropertyChanged
    {
        public BitmapImage ClueImage;
        public BitmapImage ClueUsedImage;
        private bool isUsed = false;

        public bool IsUsed
        {
            get { return isUsed; }
            set
            {
                if (value == true)
                {
                    CurrentImage = ClueUsedImage;
                }
                else
                {
                    CurrentImage = ClueImage;
                }
                isUsed = value;
            }
        }

        public BitmapImage CurrentImage { get { return currentImage; } set { currentImage = value; OnPropertyChanged("CurrentImage"); } }
        private BitmapImage currentImage;

        public ClueControl()
        {
            DataContext = this;
            InitializeComponent();
        }

        public void updateClueImage()
        {
            // Check if the ClueImagePath is empty, if its empty just use the default image
            if (Properties.Settings.Default.ClueImagePath == "")
            {
                var img = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + Properties.Settings.Default.DefaultClueImagePath));
                img.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                ClueImage = img;
            }
            else
            {
                var img = new BitmapImage(new Uri(Properties.Settings.Default.ClueImagePath));
                img.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                ClueImage = img;
            }
            if (IsUsed == false)
            {
                CurrentImage = ClueImage;
            }
            refreshImages();
        }

        public void updateClueUsedImage()
        {
            // do the same for the imagePath
            if (Properties.Settings.Default.ClueUsedImagePath == "")
            {
                var img = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + Properties.Settings.Default.DefaultClueUsedImagePath));
                img.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                ClueUsedImage = img;
            }
            else
            {
                var img = new BitmapImage(new Uri(Properties.Settings.Default.ClueUsedImagePath));
                img.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                ClueUsedImage = img;
            }
            refreshImages();
        }

        private void refreshImages()
        {
            if (IsUsed == false)
            {
                CurrentImage = ClueImage;
            }
            else if (IsUsed == true)
            {
                CurrentImage = ClueUsedImage;
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
    }
}