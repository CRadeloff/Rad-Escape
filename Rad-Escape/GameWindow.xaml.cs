using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Windows.Threading;

namespace Rad_Escape
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private string currentText = "test";

        public string CurrentText
        {
            get { return currentText; }
            set
            {
                currentText = value;
                TimerLabel.Content = value;
            }
        }

        public GameWindow()
        {
            InitializeComponent();
        }
    }
}