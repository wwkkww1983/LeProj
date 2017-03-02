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
        private int currentColor = -1;
        private int radius;
        private Action<UcPoint, enumNetPointType> changeStatus;
        public int X
        {
            get;
            set;
        }
        public int Y
        {
            get;
            set;
        }

        public UcPoint(int x, int y, Action<UcPoint, enumNetPointType> changeStatus)
        {
            this.X = x;
            this.Y = y;
            this.changeStatus = changeStatus;

            InitializeComponent();
            currentColor = colorNone;
        }

        public void UpdateStatusOnInitialLoad(enumNetPointType status)
        {
            switch (status)
            {
                case enumNetPointType.VCC: currentColor = colorVCC; break;
                case enumNetPointType.GND: currentColor = colorGND; break;
                default: currentColor = colorNone; break;
            }
            this.OnPaint(null);
        }

        private void UcPoint_Paint(object sender, PaintEventArgs e)
        {
            this.radius = this.sizeNetPoint / 2;
            this.Size = new Size(sizeNetPoint, sizeNetPoint);
            Draw.DrawSolidCircle(this, currentColor, new Point(this.radius, this.radius), this.radius);
        }

        private void vCCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentColor = colorVCC;
            this.OnPaint(null);
            this.changeStatus(this, enumNetPointType.VCC);
        }

        private void gNDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentColor = colorGND;
            this.OnPaint(null);
            this.changeStatus(this, enumNetPointType.GND);
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentColor = colorNone;
            this.OnPaint(null);
            this.changeStatus(this, enumNetPointType.NONE);
        }
    }
}
