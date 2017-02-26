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
        private const int HEIGHT_ELEMENT = 60;
        private int index = 0;
        private enumComponent componentType = enumComponent.NONE;
        private ElementInfo componentInfo = null;
        Action<Cursor, enumComponent> UpdateFatherCursor = null;


        public ElementInfo ComponentInfo
        {
            get
            {
                return this.componentInfo;
            }
        }

        public UcComponent(int idx, ElementInfo compoInfo, Action<Cursor, enumComponent> delegateCursor)
        {
            this.componentInfo = compoInfo;
            this.index = idx;
            this.UpdateFatherCursor = delegateCursor;

            InitializeComponent();

            this.BackColor = this.componentInfo.BackColor;
            this.BackgroundImage = Image.FromFile(Constants.CurrentDirectory + this.componentInfo.BackImage);
            this.Location = new Point(5, this.index * HEIGHT_ELEMENT);
            this.componentType = (enumComponent)Enum.Parse(typeof(enumComponent), this.componentInfo.Name);
            if (this.index > 1)
            {
                SetCursor((Bitmap)this.BackgroundImage, new Point(this.BackgroundImage.Width / 2, this.BackgroundImage.Height / 2));
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

        /// <summary>
        /// 设置鼠标样式
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="hotPoint"></param>
        public void SetCursor(Bitmap cursor, Point hotPoint)
        {
            int hotX = hotPoint.X;
            int hotY = hotPoint.Y;
            Bitmap myNewCursor = new Bitmap(cursor.Width * 2 - hotX, cursor.Height * 2 - hotY);
            Graphics g = Graphics.FromImage(myNewCursor);
            g.Clear(Color.FromArgb(0, 0, 0, 0));
            g.DrawImage(cursor, cursor.Width - hotX, cursor.Height - hotY, cursor.Width, cursor.Height);

            this.Cursor = new Cursor(myNewCursor.GetHicon());

            g.Dispose();
            myNewCursor.Dispose();
        }
    }
}
