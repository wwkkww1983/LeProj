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
        private Action<Cursor, string> UpdateFatherCursor = null;
        private Action<int> removeSelf = null;
        private Action<FormNewComponent,ElementInfo> updateComponent = null;


        public ElementInfo ComponentInfo
        {
            set
            {
                this.componentInfo = value;
            }
            get
            {
                return this.componentInfo;
            }
        }

        public UcComponent(int idx, ElementInfo compoInfo, Action<Cursor, string> delegateCursor, Action<int> removeComponent, Action<FormNewComponent, ElementInfo> updateComponent)
        {
            this.componentInfo = compoInfo;
            this.index = idx;
            this.UpdateFatherCursor = delegateCursor;
            this.removeSelf = removeComponent;
            this.updateComponent = updateComponent;

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
                this.ContextMenuStrip = null;
            }
        }

        public void UpdateLocationY(int idx)
        {
            this.index = idx;
            this.Location = new Point(5, this.index * HEIGHT_ELEMENT + MARGIN_UP);
        }

        private void UcComponent_Click(object sender, EventArgs e)
        {
            if (Constants.SeiralPortStatusIsOpen)
            {
                MessageBox.Show("串口已打开，不能进行器件添加","警告",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            this.UpdateFatherCursor(this.Cursor, this.componentType);
        }

        private void picBox_Click(object sender, EventArgs e)
        {
            this.UcComponent_Click(null, null);
        }

        private void elementDelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogR = MessageBox.Show("删除后，之前用到本元器件的项目将无法使用", "确定删除？");
            if (dialogR == DialogResult.OK)
            {
                this.removeSelf(this.ComponentInfo.ID);
            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNewComponent formUpdate = new FormNewComponent(null,this.updateComponent, this.componentInfo);
            formUpdate.ShowDialog();
        }
    }
}
