using System;
using System.Windows;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace QucikScreenShot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string savePath;
        private bool startCatchScreenShot = false;

        private KeyEventHandler keyEventHandler = null;
        private KeyboardHook keyboardHook = new KeyboardHook();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClosedMainWindow(object sender, EventArgs e)
        {
            startCatchScreenShot = false;
            StopListen();
        }

        private void LoadedDefaultSavePath(object sender, RoutedEventArgs e)
        {
            string myPictures = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            SavePath_TextBox.Text = Path.Combine(myPictures, "ScreenShots");
            savePath = SavePath_TextBox.Text; if (!Directory.Exists(SavePath_TextBox.Text))
            Directory.CreateDirectory(SavePath_TextBox.Text);
        }

        public void SelectSavePath(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = savePath;
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SavePath_TextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        public void OpenSavePath(object sender, RoutedEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "explorer.exe";
            process.StartInfo.Arguments = savePath;
            process.Start();
        }

        private void ChangeStatus(object sender, RoutedEventArgs e)
        {
            startCatchScreenShot = !startCatchScreenShot;
            if (startCatchScreenShot)
            {
                if (!Directory.Exists(SavePath_TextBox.Text))
                {
                    try
                    {
                        Directory.CreateDirectory(SavePath_TextBox.Text);
                    }
                    catch
                    {
                        System.Windows.MessageBox.Show("Not an available path.");
                        startCatchScreenShot = false;
                        return;
                    }
                }
                savePath = SavePath_TextBox.Text;
                StartListen();
                Log_TextBox.Text += "Start Listen" + "\n";
            }
            else
            {
                StopListen();
                Log_TextBox.Text += "Stop Listen" + "\n";
            }
            changeStatus_Button.Content = startCatchScreenShot ? "Stop" : "Start";
            status_Label.Content = startCatchScreenShot ? "Start" : "Stop";
        }

        private void StartListen()
        {
            keyEventHandler = new KeyEventHandler(Hook_KeyDown);
            keyboardHook.KeyDownEvent += keyEventHandler;
            keyboardHook.Start();
        }

        private void StopListen()
        {
            if (keyEventHandler != null)
            {
                keyboardHook.KeyDownEvent -= keyEventHandler;
                keyEventHandler = null;
                keyboardHook.Stop();
            }
        }

        private void Hook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)Keys.A && 
                (int)Control.ModifierKeys == (int)Keys.Control + (int)Keys.Alt)
            {
                try
                {
                    ScreenSnapshot(out string filePath);
                    Log_TextBox.Text += "Saving ScreenShot in " + filePath + "\n";
                }
                catch (Exception exception)
                {
                    Log_TextBox.Text += exception.ToString() + "\n";
                }
            }
        }

        private void Log_TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            Log_TextBox.ScrollToEnd();
        }

        /// <summary>
        /// http://www.yuantk.com/weblog/7221810f-9750-4285-8c20-2732fb55e013.html
        /// </summary>
        private void ScreenSnapshot(out string filePath)
        {
            Screen screen = Screen.PrimaryScreen;
            int xDpi = screen.Bounds.Width,
                yDpi = screen.Bounds.Height;
            double xDpiScaled = xDpi * Scaling(xDpi);
            double yDpiScaled = yDpi * Scaling(xDpi);
            Rectangle rectangle = new Rectangle(0, 0, (int)xDpiScaled, (int)yDpiScaled);
            Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height, PixelFormat.Format32bppArgb);
            using (Graphics memoryGrahics = Graphics.FromImage(bitmap))
            {
                memoryGrahics.CopyFromScreen(
                    rectangle.X, rectangle.Y, 0, 0, rectangle.Size, 
                    CopyPixelOperation.SourceCopy
                );
            }
            filePath = Path.Combine(savePath, Now() + ".png");
            bitmap.Save(filePath, ImageFormat.Png);
        }

        private void SystemDpi(out int x, out int y)
        {
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
            {
                x = (int)g.DpiX;
                y = (int)g.DpiY;
                g.Dispose();
            }
        }

        private double Scaling(int DpiIndex)
        {
            switch (DpiIndex)
            {
                case 96: return 1;
                case 120: return 1.25;
                case 144: return 1.5;
                case 168: return 1.75;
                case 192: return 2;
            }
            return 1;
        }

        private string Now()
        {
            return DateTime.Now.ToString("yyyy_MM_dd HH_mm_ss_ffff");
        }
    }
}
