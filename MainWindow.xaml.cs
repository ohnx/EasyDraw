using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Ink;

namespace EasyDraw
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Color currColor;
        private Boolean changed;
        private String lastFile;
        public MainWindow()
        {
            InitializeComponent();
            currColor = Colors.Black;
            changed = false;
            inkCanvas.Strokes.StrokesChanged += Strokes_StrokesChanged;
        }

        private void Strokes_StrokesChanged(object sender, StrokeCollectionChangedEventArgs e)
        {
            changed = true;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case 1:
                    inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
                    break;
                case 2:
                    inkCanvas.EditingMode = InkCanvasEditingMode.Select;
                    break;
                case 3:
                    inkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                    break;
            }
        }

        private void brushSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            inkCanvas.DefaultDrawingAttributes.Width = brushSize.Value * brushSize.Value;
            inkCanvas.DefaultDrawingAttributes.Height = brushSize.Value * brushSize.Value;
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|All files (*.*)|*.*";
            saveFileDialog.Title = "Export drawing to bitmap file";
            if (saveFileDialog.ShowDialog() == true)
            {
                var fs = new FileStream(saveFileDialog.FileName, FileMode.Create);
                int marg = int.Parse(this.inkCanvas.Margin.Left.ToString());
                RenderTargetBitmap rtb =
                        new RenderTargetBitmap((int)this.inkCanvas.ActualWidth - marg,
                                (int)this.inkCanvas.ActualHeight - marg, 0, 0,
                            PixelFormats.Default);
                rtb.Render(this.inkCanvas);
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));
                encoder.Save(fs);
                fs.Close();
            }
        }

        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            ColorForm color = new ColorForm(currColor);
            color.ShowDialog();
            inkCanvas.DefaultDrawingAttributes.Color = color.foundColor;
            currColor = color.foundColor;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Stroke files (*.ink)|*.ink|All files (*.*)|*.*";
            saveFileDialog.Title = "Save drawing";
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    var fs = new FileStream(saveFileDialog.FileName, FileMode.Create);
                    lastFile = saveFileDialog.FileName;
                    inkCanvas.Strokes.Save(fs);
                    fs.Close();
                    changed = false;
                }
                catch (Exception mitsake)
                {
                    MessageBox.Show("There was an error saving the file: " + mitsake.ToString(), "Error saving file", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            if (changed)
            {
                if (MessageBox.Show("The file has been changed. Are you sure you want to open another file?", "Destroy changes?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    return;
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Stroke files (*.ink)|*.ink|All files (*.*)|*.*";
            openFileDialog.Title = "Open drawing";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    var fs = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                    StrokeCollection strokes = new StrokeCollection(fs);
                    inkCanvas.Strokes = strokes;
                    lastFile = openFileDialog.FileName;
                    changed = false;
                }
                catch (Exception mitsake)
                {
                    //Console.Write(mitsake.ToString());
                    MessageBox.Show("There was an error reading the file: "+mitsake.ToString(), "Error reading file", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            if (changed)
            {
                if (MessageBox.Show("The file has been changed. Are you sure you want to clear the file?", "Destroy changes?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    return;
            }
            this.inkCanvas.Strokes.Clear();
            changed = false;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (changed)
            {
                if (MessageBox.Show("The file has been changed. Are you sure you want to exit?", "Destroy changes?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    return;
            }
            this.Close();
            changed = false;
        }
    }
}
