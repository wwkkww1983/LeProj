using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimuProteus
{
    public partial class UcPoint : UserControl
    {
        private int sizeNetPoint = int.Parse(Ini.GetItemValue("sizeInfo", "pixelNetPoint"));
        private int colorNone = int.Parse(Ini.GetItemValue("colorInfo", "colorNONE"));
        private int colorVCC = int.Parse(Ini.GetItemValue("colorInfo", "colorVCC"));
        private int colorGND = int.Parse(Ini.GetItemValue("colorInfo", "colorGND"));
        private int radius;
        public UcPoint()
        {
            InitializeComponent();            
        }

        private void UcPoint_Paint(object sender, PaintEventArgs e)
        {
            this.radius = this.sizeNetPoint / 2;
            this.Size = new Size(sizeNetPoint, sizeNetPoint);
            Draw.DrawSolidCircle(this, colorNone, new Point(this.radius, this.radius), this.radius);
        }
    }
}
