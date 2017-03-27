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
        private bool initialConditionFlag = false;
        private DBUtility dbHandler = new DBUtility(true);
        private Algorithm alg = null;

        public MainView()
        {
            InitializeComponent();

            //dbHandler.InitialTable();
            this.InitialCamInter();
        }

        private void InitialCamInter()
        {
            DataTable dt = dbHandler.GetAllConnector();
            this.cbCamInter.DataSource = dt;
            this.cbCamInter.DisplayMember = "Name";
            this.cbCamInter.ValueMember = "Idx";
            this.cbCamInter.SelectedIndex = -1;

            this.cbLensInter.DataSource = dt.Copy();
            this.cbLensInter.DisplayMember = "Name";
            this.cbLensInter.ValueMember = "Idx";
            this.cbLensInter.SelectedIndex = -1;

            this.initialConditionFlag = true;
            this.alg = new Algorithm(dbHandler.GetAllDevices(enumProductType.Focus));
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


        private void selectPatchItems(object sender, EventArgs e)
        {
            if (!initialConditionFlag || this.tbInterLength.Text.Trim().Equals(string.Empty)) return;

            int lenInter = Convert.ToInt32(this.cbLensInter.SelectedValue);
            int camInter = Convert.ToInt32(this.cbCamInter.SelectedValue);
            float interLength = Convert.ToSingle(this.tbInterLength.Text);
        }

        #endregion

    }
}
