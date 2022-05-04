using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DesktopBannerCountdown
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DispatcherTimer dispatcherTimer = null;
        string StringFormat = ".000";

        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer_Tick(null, null);

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            dispatcherTimer.Start();

            Properties.Settings.Default.PropertyChanged += Default_PropertyChanged;
            Default_PropertyChanged(null, null);
        }

        private void Default_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            TextBlockTitle.Text = (isJFMode ? Properties.Settings.Default.TitleTextB : Properties.Settings.Default.TitleTextA).Split('|')[0];
            TextBlock_TitleB.Text = (isJFMode ? Properties.Settings.Default.TitleTextB : Properties.Settings.Default.TitleTextA).Split('|')[1];

            TextBlockDaysDetails.Width = Properties.Settings.Default.CounterDecimalWidth;

            Border_Root.Background = new SolidColorBrush(Properties.Settings.Default.WindowBackground);
            Border_Root.BorderBrush = new SolidColorBrush(Properties.Settings.Default.WindowBorderBrush);
            Border_Root.BorderThickness = Properties.Settings.Default.WindowBorderThickness;
            Border_Root.CornerRadius = Properties.Settings.Default.WindowCornerRadius;
            Border_Root.Padding = Properties.Settings.Default.WindowPadding;

            TextBlockTitle.FontFamily = TextBlock_TitleB.FontFamily = new FontFamily(Properties.Settings.Default.TitleFontFamily);
            TextBlockTitle.FontSize = TextBlock_TitleB.FontSize = Properties.Settings.Default.TitleFontSize;
            TextBlockTitle.Foreground = TextBlock_TitleB.Foreground = new SolidColorBrush(Properties.Settings.Default.TitleForeground);

            TextBlockDays.FontFamily = TextBlockDaysDetails.FontFamily = new FontFamily(Properties.Settings.Default.CounterFontFamily);
            TextBlockDays.FontSize = Properties.Settings.Default.CounterFontSize;
            TextBlockDaysDetails.FontSize = Properties.Settings.Default.CounterDecimalFontSize;
            TextBlockDays.Foreground = TextBlockDaysDetails.Foreground = new SolidColorBrush(Properties.Settings.Default.CounterNormalForeground);


            StringFormat = ".";
            try
            {
                int n = Properties.Settings.Default.DecimalPlaces;
                while (n-- > 0)
                {
                    StringFormat += "0";
                }
            }
            catch
            {
                StringFormat = ".000";
            }



        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan timeSpan = Properties.Settings.Default.DestinationDateA - DateTime.Now;
            if (isJFMode)
            {
                timeSpan = Properties.Settings.Default.DestinationDateB - DateTime.Now;
            }
            TextBlockDays.Text = timeSpan.Days.ToString();
            if (timeSpan.Days < Properties.Settings.Default.EmergencyDays) TextBlockDays.Foreground = TextBlockDaysDetails.Foreground = new SolidColorBrush(Properties.Settings.Default.CounterEmergencyForeground);
            string detailStr = ((timeSpan.Hours * 3600000 + timeSpan.Minutes * 60000 + timeSpan.Seconds * 1000 + timeSpan.Milliseconds) / 86400000.0).ToString(StringFormat);
            if (detailStr.StartsWith("1."))
            {
                try
                {
                    detailStr = ".";
                    int n = Properties.Settings.Default.DecimalPlaces;
                    while (n-- > 0)
                    {
                        StringFormat += "9";
                    }
                }
                catch
                {
                    detailStr = ".999";
                }
            }
            TextBlockDaysDetails.Text = detailStr;
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Top = 20;
            Left = SystemParameters.WorkArea.Width /2 - this.Width/2;

            //if (App.StartArgs != null)
            //{
            //    StringFormat = ".";
            //    try
            //    {
            //        int n = Properties.Settings.Default.DecimalPlaces;
            //        while (n-- > 0)
            //        {
            //            StringFormat += "0";
            //        }
            //    }
            //    catch
            //    {
            //        StringFormat = ".000";
            //    }

            //    if (App.StartArgs.Contains("-c"))
            //    {
            //    }

            //    if (App.StartArgs.Contains("-t"))
            //    {
            //        ShowInTaskbar = true;
            //    }

            //    if (App.StartArgs.Contains("-l"))
            //    {
            //        Foreground = Brushes.White;
            //    }

            //    if (App.StartArgs.Contains("-jf"))
            //    {
            //        TextBlockTitle.Text = "距离解放还有";
            //        isJFMode = true;
            //    }
            //}
        }

        bool isJFMode = false;

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal;
            }
        }

        private void MenuItem_Settings_Click(object sender, RoutedEventArgs e)
        {
            new SettingsWindow().Show();
        }

        private void MenuItem_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Properties.Settings.Default.SwitchByDoubleClick)
            {
                isJFMode = !isJFMode;
                Default_PropertyChanged(null, null);
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Properties.Settings.Default.SwitchByLeftClick)
            {
                isJFMode = !isJFMode;
                Default_PropertyChanged(null, null);
            }

        }
    }
}