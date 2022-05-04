using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace DesktopBannerCountdown
{
    /// <summary>
    /// SettingsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void Button_SaveAndApply_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoadSettings()
        {
            try
            {
                TextBox_TitleTextA1.Text = Properties.Settings.Default.TitleTextA.Split('|')[0];
                TextBox_TitleTextA2.Text = Properties.Settings.Default.TitleTextA.Split('|')[1];
            }
            catch { }
            try
            {
                TextBox_TitleTextB1.Text = Properties.Settings.Default.TitleTextB.Split('|')[0];
                TextBox_TitleTextB2.Text = Properties.Settings.Default.TitleTextB.Split('|')[1];
            }
            catch { }

            DatePicker_DestinationDateA.DateTimeStr = Properties.Settings.Default.DestinationDateA.ToString();
            DatePicker_DestinationDateB.DateTimeStr = Properties.Settings.Default.DestinationDateB.ToString();

            CheckBox_SwitchByDoubleClick.IsChecked = Properties.Settings.Default.SwitchByDoubleClick;
            CheckBox_SwitchByLeftClick.IsChecked = Properties.Settings.Default.SwitchByLeftClick;

            FontComboBox_TitleFontFamily.Text= Properties.Settings.Default.TitleFontFamily;
            FontComboBox_CounterFontFamily.Text=Properties.Settings.Default.CounterFontFamily;
            TextBox_TitleFontSize.Text = Properties.Settings.Default.TitleFontSize.ToString();
            TextBox_CounterFontSize.Text = Properties.Settings.Default.CounterFontSize.ToString();
            TextBox_CounterDecimalFontSize.Text= Properties.Settings.Default.CounterDecimalFontSize.ToString();
            TextBox_CounterDecimalPlaces.Text = Properties.Settings.Default.DecimalPlaces.ToString();
            TextBox_CounterDecimalWidth.Text = Properties.Settings.Default.CounterDecimalWidth.ToString();
            TextBox_EmergencyDays.Text = Properties.Settings.Default.EmergencyDays.ToString();

            ColorPicker_TitleForeground.Color = Properties.Settings.Default.TitleForeground;
            ColorPicker_CounterNormalForeground.Color= Properties.Settings.Default.CounterNormalForeground;
            ColorPicker_CounterEmergencyForeground.Color=Properties.Settings.Default.CounterEmergencyForeground;

            TextBox_WindowBorderThickness.Text=Properties.Settings.Default.WindowBorderThickness.ToString(); 
            ColorPicker_WindowBorderBrush.Color= Properties.Settings.Default.WindowBorderBrush;
            ColorPicker_WindowBackground.Color = Properties.Settings.Default.WindowBackground;
            TextBox_WindowCornerRadius.Text= Properties.Settings.Default.WindowCornerRadius.ToString();
            TextBox_WindowPadding.Text = Properties.Settings.Default.WindowPadding.ToString();
        }

        private bool SaveSettings()
        {
            bool result = true;

            Properties.Settings.Default.TitleTextA = TextBox_TitleTextA1.Text + "|" + TextBox_TitleTextA2.Text;
            Properties.Settings.Default.TitleTextB = TextBox_TitleTextB1.Text + "|" + TextBox_TitleTextB2.Text;

            try
            {
                Properties.Settings.Default.DestinationDateA = DateTime.Parse(DatePicker_DestinationDateA.DateTimeStr);
            }
            catch { result = false; }
            try
            {
                Properties.Settings.Default.DestinationDateB = DateTime.Parse(DatePicker_DestinationDateB.DateTimeStr);
            }
            catch { result = false; }

            Properties.Settings.Default.SwitchByDoubleClick = CheckBox_SwitchByDoubleClick.IsChecked.GetValueOrDefault(true);
            Properties.Settings.Default.SwitchByLeftClick = CheckBox_SwitchByLeftClick.IsChecked.GetValueOrDefault(false);

            Properties.Settings.Default.TitleFontFamily = FontComboBox_TitleFontFamily.Text;
            Properties.Settings.Default.CounterFontFamily = FontComboBox_CounterFontFamily.Text;

            try
            {
                Properties.Settings.Default.TitleFontSize = int.Parse(TextBox_TitleFontSize.Text);
                Properties.Settings.Default.CounterFontSize = int.Parse(TextBox_CounterFontSize.Text);
                Properties.Settings.Default.CounterDecimalFontSize = int.Parse(TextBox_CounterDecimalFontSize.Text);
                Properties.Settings.Default.EmergencyDays = int.Parse(TextBox_EmergencyDays.Text);

            }
            catch { result = false; }

            Properties.Settings.Default.TitleForeground = ColorPicker_TitleForeground.Color;
            Properties.Settings.Default.CounterNormalForeground = ColorPicker_CounterNormalForeground.Color;
            Properties.Settings.Default.CounterEmergencyForeground = ColorPicker_CounterEmergencyForeground.Color;

            Properties.Settings.Default.WindowBorderBrush = ColorPicker_WindowBorderBrush.Color;
            Properties.Settings.Default.WindowBackground = ColorPicker_WindowBackground.Color;

            try
            {
                Properties.Settings.Default.WindowBorderThickness = (Thickness)new ThicknessConverter().ConvertFromString(TextBox_WindowBorderThickness.Text);
                Properties.Settings.Default.WindowCornerRadius = (CornerRadius)new CornerRadiusConverter().ConvertFromString(TextBox_WindowCornerRadius.Text);
                Properties.Settings.Default.WindowPadding = (Thickness)new ThicknessConverter().ConvertFromString(TextBox_WindowPadding.Text);
            }
            catch { result = false; }

            try
            {
                Properties.Settings.Default.DecimalPlaces = int.Parse(TextBox_CounterDecimalPlaces.Text);
                Properties.Settings.Default.CounterDecimalWidth = int.Parse(TextBox_CounterDecimalWidth.Text);

            }
            catch { result = false; }
            Properties.Settings.Default.Save();

            return result;
        }

        private void Button_Reset_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Reset();
            LoadSettings();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/YujunA/DesktopBannerCountdown");
        }
    }
}
