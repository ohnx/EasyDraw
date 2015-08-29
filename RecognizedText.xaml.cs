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
using System.Windows.Shapes;

namespace EasyDraw
{
    /// <summary>
    /// Interaction logic for RecognizedText.xaml
    /// </summary>
    public partial class RecognizedText : Window
    {
        public RecognizedText()
        {
            InitializeComponent();
        }

        public RecognizedText(string recognized)
        {
            InitializeComponent();
            recogText.Text = recognized;
        }

        private void doneBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
