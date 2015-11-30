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
using System.Windows.Ink;
using Microsoft.Ink;
using Unity3.Eyedropper;

namespace EasyDraw
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Color currColor;
        private Boolean changed;
        private String currFile;
        private Boolean botherUpdatingBrushSize;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
            currColor = Colors.Black;
            changed = false;
            PenMode.IsChecked = true;
            inkCanvas.Strokes.StrokesChanged += Strokes_StrokesChanged;
            this.Title += " - Untitled";
            _colorCanvas.SelectedColorChanged += colorCanvas_colorChanged;
            botherUpdatingBrushSize = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host =
                new System.Windows.Forms.Integration.WindowsFormsHost();

            EyeDropper eD = new EyeDropper();
            eD.ScreenCaptured += eyeDropper1_ScreenCaptured;
            // Assign the MaskedTextBox control as the host control's child.
            host.Child = eD;
            // Add the interop host control to the Grid
            // control's collection of child controls.
            this.eyeDropperHost.Children.Add(host);
        }

        private void eyeDropper1_ScreenCaptured(System.Drawing.Bitmap cP, System.Drawing.Color cC)
        {
            _colorCanvas.SelectedColor = Color.FromRgb(cC.R, cC.G, cC.B);
        }

        private void colorCanvas_colorChanged(object sender, EventArgs e)
        {
            inkCanvas.DefaultDrawingAttributes.Color = (Color) _colorCanvas.SelectedColor;
            currColor = (Color)_colorCanvas.SelectedColor;
        }

        private void Strokes_StrokesChanged(object sender, StrokeCollectionChangedEventArgs e)
        {
            if (changed)
            {
                return;
            }
            changed = true;
            this.Title += "*";
        }

        private void modeChange(object sender, RoutedEventArgs e)
        {
            try {
                switch (((MenuItem)sender).Name)
                {
                    case "PenMode":
                        inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                        EraserMode.IsChecked = false;
                        SelectMode.IsChecked = false;
                        SEraserMode.IsChecked = false;
                        break;
                    case "EraserMode":
                        inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
                        PenMode.IsChecked = false;
                        SelectMode.IsChecked = false;
                        SEraserMode.IsChecked = false;
                        break;
                    case "SelectMode":
                        inkCanvas.EditingMode = InkCanvasEditingMode.Select;
                        EraserMode.IsChecked = false;
                        PenMode.IsChecked = false;
                        SEraserMode.IsChecked = false;
                        break;
                    case "SEraserMode":
                        inkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                        EraserMode.IsChecked = false;
                        SelectMode.IsChecked = false;
                        PenMode.IsChecked = false;
                        break;
                    default:
                        break;
                }
            } catch (Exception err) {

            }
        }

        private void brushSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!botherUpdatingBrushSize) return;
            inkCanvas.DefaultDrawingAttributes.Width = brushSize.Value * brushSize.Value;
            inkCanvas.DefaultDrawingAttributes.Height = brushSize.Value * brushSize.Value;
            botherUpdatingBrushSize = false;
            mainSlider.Value = brushSize.Value;
            botherUpdatingBrushSize = true;
        }

        private void mainSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!botherUpdatingBrushSize) return;
            inkCanvas.DefaultDrawingAttributes.Width = mainSlider.Value * mainSlider.Value;
            inkCanvas.DefaultDrawingAttributes.Height = mainSlider.Value * mainSlider.Value;
            botherUpdatingBrushSize = false;
            brushSize.Value = mainSlider.Value;
            botherUpdatingBrushSize = true;

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
                RenderTargetBitmap rtb = new RenderTargetBitmap(
                            (int)this.inkCanvas.ActualWidth - marg,
                            (int)this.inkCanvas.ActualHeight - marg, 0, 0,
                            PixelFormats.Default);
                rtb.Render(this.inkCanvas);
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));
                encoder.Save(fs);
                fs.Close();
            }
        }

        private void RecognizeButton_Click(object sender, RoutedEventArgs e)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                inkCanvas.Strokes.Save(ms);
                var myInkCollector = new InkCollector();
                var ink = new Ink();
                ink.Load(ms.ToArray());

                using (RecognizerContext myRecoContext = new RecognizerContext())
                {
                    RecognitionStatus status;
                    myRecoContext.Strokes = ink.Strokes;
                    var recoResult = myRecoContext.Recognize(out status);

                    if (status == RecognitionStatus.NoError)
                    {
                        RecognizedText rt = new RecognizedText(recoResult.TopString);
                        rt.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("There was an error recognizing text: " + status.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            ColorForm color = new ColorForm(currColor);
            color.ShowDialog();
            _colorCanvas.SelectedColor = color.foundColor;
        }

        private bool showSaveDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Stroke files (*.ink)|*.ink|All files (*.*)|*.*";
            saveFileDialog.Title = "Save drawing";
            if (saveFileDialog.ShowDialog() == true)
            {
                currFile = saveFileDialog.FileName;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            bool needShowing = true;
            try
            {
                if (((MenuItem)sender).Name == "SaveAsButton")
                {
                    if (!showSaveDialog())
                    {
                        return;
                    }
                    needShowing = false;
                }
            }
            catch (Exception err)
            {

            }
            if (currFile != null)
            {

            }
            else
            {
                if (needShowing)
                {
                    if (!showSaveDialog())
                    {
                        return;
                    }
                }
            }
            try
            {
                var fs = new FileStream(currFile, FileMode.Create);
                inkCanvas.Strokes.Save(fs);
                fs.Close();
                changed = false;
                this.Title = "EasyDraw - " + Path.GetFileName(currFile);
            }
            catch (Exception mitsake)
            {
                MessageBox.Show("There was an error saving the file: " + mitsake.ToString(), "Error saving file", MessageBoxButton.OK, MessageBoxImage.Error);
                changed = true;
                this.Title += "*";
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
                    currFile = openFileDialog.FileName;
                    changed = false;
                    this.Title = "EasyDraw - " + Path.GetFileName(currFile);
                    // don't know why this is needed
                    inkCanvas.Strokes.StrokesChanged += Strokes_StrokesChanged;
                    fs.Close();
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
            string msg;
            try
            {
                if (((MenuItem)sender).Name == "ClearButton")
                {
                    msg = "The canvas has been changed. Are you sure you want to clear the canvas?";
                }
                else
                {
                    msg = "The file has been changed. Are you sure you want to create a new file?";
                }
            }
            catch (Exception err)
            {
                msg = "The file has been changed. Are you sure you want to create a new file?";
            }

            if (changed)
            {
                if (MessageBox.Show(msg, "Destroy changes?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    return;
                changed = true;
            }

            try
            {
                if (((MenuItem)sender).Name != "ClearButton")
                {
                    this.Title = "EasyDraw - Untitled";
                    currFile = null;
                }
            }
            catch (Exception err)
            {
                this.Title = "EasyDraw - Untitled";
                currFile = null;
            }
            this.inkCanvas.Strokes.Clear();

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
