using System;
using System.Collections.Generic;
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
    /// Interaction logic for PathAndLabel.xaml
    /// </summary>
    public partial class PathChooser : UserControl
    {
        public string PathBoxText
        {
            get { return PathBox.Text; }
            set { PathBox.Text = value; }
        }

        public string ButtonText
        {
            get { return PathButton.Content.ToString(); }
            set { PathButton.Content = value; }
        }

        public PathChooser()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PathBoxText = choosePath();
        }

        private string choosePath()
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dialog.DefaultExt = ".jpg";
            dialog.Filter = "Image Files (*.jpg *.jpeg *.png *.gif *.bmp)|*.JPG;*.JPEG;*.PNG;*.GIF;*.BMP|All files (*.*)|*.*";

            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(dialog.FileName);
                string filePath = fileInfo.FullName;
                return filePath;
            }
            else
            {
                return "";
            }
        }
    }
}