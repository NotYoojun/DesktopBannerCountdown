using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Forms = System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Drawing = System.Drawing;

namespace DesktopBannerCountdown.Controls
{
    /// <summary>
    /// ColorPicker.xaml 的交互逻辑
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        public ColorPicker()
        {
            InitializeComponent();
        }

        public static DependencyProperty ColorProperty =DependencyProperty.Register("Color", typeof(Color),typeof(ColorPicker),new PropertyMetadata(Colors.Gray, ColorProperty_ValueChanged));

        private static void ColorProperty_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ColorPicker)?.ColorChanged?.Invoke(d, e);
        }

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public event DependencyPropertyChangedEventHandler ColorChanged;
        Forms.ColorDialog dialog;
        private void Button_Select_Click(object sender, RoutedEventArgs e)
        {
            if (dialog == null)
                dialog = new Forms.ColorDialog();

            dialog.Color = Drawing.Color.FromArgb(Color.A, Color.R, Color.G, Color.B);

            if (dialog.ShowDialog() == Forms.DialogResult.OK)
            {
                Color = Color.FromArgb((byte)(Math.Max(Math.Min(255d * (int.Parse(TextBox_Alpha.Text)) / 100d, 255), 0)), dialog.Color.R, dialog.Color.G, dialog.Color.B);
            }
        }

        private void TextBox_Alpha_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(this.Color.A != (byte)(Math.Max(Math.Min(255d * (int.Parse(TextBox_Alpha.Text)) / 100d, 255), 0)))
            {
                Color c = Color.FromArgb((byte)(Math.Max(Math.Min(255d * (int.Parse(TextBox_Alpha.Text)) / 100d, 255),0)), Color.R, Color.G, Color.B);
                this.Color = c;
            }
        }
    }
}
