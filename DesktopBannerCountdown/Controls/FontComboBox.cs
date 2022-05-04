using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;


namespace DesktopBannerCountdown.Controls
{
    internal class FontComboBox : ComboBox
    {
        public FontComboBox()
        {
            this.IsEditable = true;
            this.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            foreach(FontFamily fam in System.Windows.Media.Fonts.SystemFontFamilies)
            {
                this.Items.Add(fam.Source);
            }
        }
    }
}
