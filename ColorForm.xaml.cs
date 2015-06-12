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
using System.Text.RegularExpressions;

namespace EasyDraw
{
    /// <summary>
    /// Interaction logic for ColorForm.xaml
    /// </summary>
    public partial class ColorForm : Window
    {
        public Color foundColor { get; set; } 
        private Boolean botherUpdating = true;
        private Color original;
        public ColorForm()
        {
            InitializeComponent();
            cCanvas.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            redSlider.Value = 0;
            blueSlider.Value = 0;
            greenSlider.Value = 0;
            original = Color.FromRgb(0, 0, 0);
        }

        public ColorForm(Color c)
        {
            InitializeComponent();
            redSlider.Value = c.R;
            blueSlider.Value = c.B;
            greenSlider.Value = c.G;
            original = c;
            cCanvas.Background = new SolidColorBrush(c);
        }

        private void redSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            updateColor(0);
        }

        private void greenSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            updateColor(0);
        }

        private void blueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            updateColor(0);
        }

        private void redBtn_Click(object sender, RoutedEventArgs e)
        {
            updateColor(1);
        }

        private void blueBtn_Click(object sender, RoutedEventArgs e)
        {
            updateColor(2);
        }

        private void greenBtn_Click(object sender, RoutedEventArgs e)
        {
            updateColor(3);
        }

        private void updateColor(int isButton)
        {
            if (!botherUpdating)
            {
                return;
            }
            if (isButton == 0)
            {
                cCanvas.Background = new SolidColorBrush(Color.FromRgb((byte)redSlider.Value, (byte)greenSlider.Value, (byte)blueSlider.Value));
                redTxt.Text = Convert.ToString(redSlider.Value);
                greenTxt.Text = Convert.ToString(greenSlider.Value);
                blueTxt.Text = Convert.ToString(blueSlider.Value);
            }
            else if (isButton == 1)
            {
                //cCanvas.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                redSlider.Value = 255;
                redTxt.Text = "255";
                blueSlider.Value = 0;
                blueTxt.Text = "0";
                greenSlider.Value = 0;
                greenTxt.Text = "0";
            }
            else if (isButton == 2)
            {
                //cCanvas.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                redSlider.Value = 0;
                redTxt.Text = "0";
                blueSlider.Value = 255;
                blueTxt.Text = "255";
                greenSlider.Value = 0;
                greenTxt.Text = "0";
            }
            else if (isButton == 3)
            {
                //cCanvas.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                redSlider.Value = 0;
                redTxt.Text = "0";
                blueSlider.Value = 0;
                blueTxt.Text = "0";
                greenSlider.Value = 255;
                greenTxt.Text = "255";
            }
            else if (isButton == 4)
            {
                //cCanvas.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                redSlider.Value = 0;
                redTxt.Text = "0";
                blueSlider.Value = 0;
                blueTxt.Text = "0";
                greenSlider.Value = 0;
                greenTxt.Text = "0";
            }
            else if (isButton == 5)
            {
                //cCanvas.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                redSlider.Value = 255;
                redTxt.Text = "255";
                blueSlider.Value = 0;
                blueTxt.Text = "0";
                greenSlider.Value = 255;
                greenTxt.Text = "255";
            }
            else if (isButton == 6)
            {
                //cCanvas.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                redSlider.Value = 255;
                redTxt.Text = "255";
                blueSlider.Value = 130;
                blueTxt.Text = "130";
                greenSlider.Value = 90;
                greenTxt.Text = "90";
            }
        }

        private void blackBtn_Click(object sender, RoutedEventArgs e)
        {
            updateColor(4);
        }

        private void pinkBtn_Click(object sender, RoutedEventArgs e)
        {
            updateColor(6);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void yellowBtn_Click(object sender, RoutedEventArgs e)
        {
            updateColor(5);
        }

        private void loadRGB_Click(object sender, RoutedEventArgs e)
        {
            botherUpdating = false;
            redSlider.Value = Convert.ToDouble(redTxt.Text);
            blueSlider.Value = Convert.ToDouble(blueTxt.Text);
            greenSlider.Value = Convert.ToDouble(greenTxt.Text);
            botherUpdating = true;
            updateColor(0);
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.foundColor = original;
            this.Close();
        }

        private void useBtn_Click(object sender, RoutedEventArgs e)
        {
            this.foundColor = Color.FromRgb((byte)redSlider.Value, (byte)greenSlider.Value, (byte)blueSlider.Value);
            this.Close();
        }
    }
}
