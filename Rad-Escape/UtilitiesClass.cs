using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rad_Escape
{
    internal class UtilitiesClass
    {
        public string choosePath()
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