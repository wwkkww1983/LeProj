using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CamInter
{
    public partial class MainView : Form
    {
        #region 实例化
        DBUtility dbHandler = new DBUtility(true);
        public MainView()
        {
            InitializeComponent();

            dbHandler.InitialTable();
            this.InitialCamInter();
        }

        private void InitialCamInter()
        {
            this.cbCamInter.DataSource = dbHandler.GetAllConnector();
            this.cbCamInter.DisplayMember = "Name";
            this.cbCamInter.ValueMember = "Idx";
            this.cbCamInter.SelectedIndex = -1;
        }

        #endregion

        #region 回调
        private void HanderAfterAddItem(Form window, enumProductType type, bool isSuccess)
        {
            if (isSuccess)
            {
                MessageBox.Show("增加成功");
                window.Close();
            }
            else
            {
                MessageBox.Show("操作失败");
            }
            if (type == enumProductType.Interface)
            {
                this.InitialCamInter();
            }
        }
        #endregion

        #region 菜单
        private void CamLensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewLens viewShow = new ViewLens(this.HanderAfterAddItem);
            viewShow.ShowDialog();
        }

        private void FocusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MidRing viewShow = new MidRing(enumProductType.Focus, this.HanderAfterAddItem);
            viewShow.ShowDialog();
        }

        private void AdapterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MidRing viewShow = new MidRing(enumProductType.Adapter, this.HanderAfterAddItem);
            viewShow.ShowDialog();
        }

        private void ExtendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MidRing viewShow = new MidRing(enumProductType.Extend, this.HanderAfterAddItem);
            viewShow.ShowDialog();
        }

        private void connToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewConnctor viewConn = new ViewConnctor(this.HanderAfterAddItem);
            viewConn.ShowDialog();
        }
        #endregion

        #region 用户行为
        private void cbCamInter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
