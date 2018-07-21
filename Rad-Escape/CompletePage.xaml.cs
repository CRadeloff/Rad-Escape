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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class CompletePage : Page
    {
        public CompletePage(string topMessage, TimeSpan finalTime, string bottomMessage, int numCluesUsed)
        {
            InitializeComponent();
            TopMessage.Content = topMessage;
            AboveTimeMessage.Content = "Your Final Time:";
            addClues(ref finalTime, numCluesUsed);
            TimeMessage.Content = finalTime.ToString(@"hh\:mm\:ss\.ff");
            BottomMessage.Content = bottomMessage;
        }

        private void addClues(ref TimeSpan finalTime, int numClues)
        {
            if (numClues <= 3)
            {
                return;
            }
            for (int i = 0; i < numClues; i++)
            {
                finalTime = finalTime.Add(new TimeSpan(0, numClues, 0));
            }
        }
    }
}