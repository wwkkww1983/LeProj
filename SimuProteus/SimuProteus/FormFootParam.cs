using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimuProteus
{
    public partial class FormFootParam : Form
    {
        private DBUtility dbhadler = new DBUtility();
        private ElementInfo element;
        private Action<int, string> updateElementName;
        private Action<LineFootView> updateFootsInfo;
        private List<LineFootView> pinsList;

        public FormFootParam(ElementInfo info, Action<int, string> updateElementName, Action<LineFootView> updateFootInfo, List<LineFootView> pinsList)
        {
            this.element = info;
            this.updateElementName = updateElementName;
            this.updateFootsInfo = updateFootInfo;
            this.pinsList = pinsList;

            InitializeComponent();

            this.tbName.Text = this.element.Name;
            this.tbIdx.Text = this.element.ID.ToString ();
            this.tbNumber.Text = this.element.Number;
            this.dgvFoots.DataSource = GetPinsInfo(this.element.LineFoots);
            this.dgvFoots.AutoGenerateColumns = false;
        }

        private DataTable GetPinsInfo(List<LineFoot> footList)
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("footName");
            dt.Columns.Add(dc);
            dc = new DataColumn("footType");
            dt.Columns.Add(dc);
            dc = new DataColumn("innerIdx");
            dt.Columns.Add(dc);
            dc = new DataColumn("idx");
            dt.Columns.Add(dc);

            foreach (LineFoot item in footList)
            {
                LineFootView itemTmp = pinsList.Find(itmp => itmp.Element == this.element.InnerIdx && itmp.Foot == item.Idx);
                DataRow dr = dt.NewRow();
                dr["idx"] = item.Idx;
                dr["innerIdx"] = item.InnerIdx;
                dr["footName"] = itemTmp.Idx > 0 ? itemTmp.PinsName : item.Name;
                dr["footType"] = ((enumPinsType)(itemTmp.Idx > 0 ? itemTmp.PinsType : item.PinsType)).ToString();

                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            this.updateElementName(this.element.InnerIdx, tbName.Text.Trim ());
        }

        private void dgvFoots_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView objData = sender as DataGridView;
            LineFootView item = new LineFootView()
            {
                Element = this.element.InnerIdx,
                Foot = Convert.ToInt32(objData.Rows[e.RowIndex].Cells["idx"].Value),
                PinsName = objData.Rows[e.RowIndex].Cells["footName"].Value.ToString(),
                PinsType = (enumPinsType)Enum.Parse(typeof(enumPinsType), objData.Rows[e.RowIndex].Cells["footType"].Value.ToString())
            };
            this.updateFootsInfo(item);
        }
    }
}
