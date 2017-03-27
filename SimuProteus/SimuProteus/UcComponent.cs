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
    public partial class UcComponent : UserControl
    {
        private const int HEIGHT_ELEMENT = 60, MARGIN_UP=10;
        private int index = 0;
        private string componentType = enumComponent.NONE.ToString ();
        private ElementInfo componentInfo = null;
        Action<Cursor, string> UpdateFatherCursor = null;


        public ElementInfo ComponentInfo
        {
            get
            {
                return this.componentInfo;
            }
        }

        public UcComponent(int idx, ElementInfo compoInfo, Action<Cursor, string> delegateCursor)
        {
            this.componentInfo = compoInfo;
            this.index = idx;
            this.UpdateFatherCursor = delegateCursor;

            InitializeComponent();

            this.BackColor = this.componentInfo.BackColor;
            this.picBox.Image = Image.FromFile(this.componentInfo.BackImage); ;
            this.picBox.SizeMode = PictureBoxSizeMode.Zoom;
            this.Location = new Point(5, this.index * HEIGHT_ELEMENT + MARGIN_UP);
            this.componentType = this.componentInfo.Name;
            if (this.index > 0)
            {
                Bitmap myNewCursor = new Bitmap((Bitmap)this.picBox.Image);
                this.Cursor = new Cursor(myNewCursor.GetHicon());
                myNewCursor.Dispose();
            }
            else
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        private void UcComponent_Click(object sender, EventArgs e)
        {
            this.UpdateFatherCursor(this.Cursor, this.componentType);
        }

        private void picBox_Click(object sender, EventArgs e)
        {
            this.UcComponent_Click(null, null);
        }
    }
}
