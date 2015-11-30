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
            _colorCanvas.SelectedColor = Color.FromRgb(0, 0, 0);
            redSlider.Value = 0;
            blueSlider.Value = 0;
            greenSlider.Value = 0;
            original = Color.FromRgb(0, 0, 0);
            _colorCanvas.SelectedColorChanged += changedColor;
        }

        public ColorForm(Color c)
        {
            InitializeComponent();
            redSlider.Value = c.R;
            blueSlider.Value = c.B;
            greenSlider.Value = c.G;
            original = c;
            _colorCanvas.SelectedColor = c;
            _colorCanvas.SelectedColorChanged += changedColor;
        }

        private void changedColor(object s, EventArgs e)
        {
            botherUpdating = false;
            redSlider.Value = ((Color)_colorCanvas.SelectedColor).R;
            greenSlider.Value = ((Color)_colorCanvas.SelectedColor).G;
            blueSlider.Value = ((Color)_colorCanvas.SelectedColor).B;
            botherUpdating = true;
            updateColor(0);
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


        private void updateColor(int isButton)
        {
            if (!botherUpdating)
            {
                return;
            }
            if (isButton == 0)
            {
                _colorCanvas.SelectedColor = Color.FromRgb((byte)redSlider.Value, (byte)greenSlider.Value, (byte)blueSlider.Value);
                redTxt.Text = Convert.ToString(redSlider.Value);
                greenTxt.Text = Convert.ToString(greenSlider.Value);
                blueTxt.Text = Convert.ToString(blueSlider.Value);
            }

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
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
