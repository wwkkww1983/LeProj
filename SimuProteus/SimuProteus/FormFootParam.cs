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
        private ElementInfo element;
        public FormFootParam(ElementInfo info)
        {
            this.element = info;

            InitializeComponent();

            this.tbName.Text = this.element.Name;
            this.dgvFoots.DataSource = GetPinsInfo(this.element.LineFoots);
        }

        private DataTable GetPinsInfo(List<LineFoot> footList)
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("footName");
            dt.Columns.Add(dc);
            dc = new DataColumn("footType");
            dt.Columns.Add(dc);

            foreach (LineFoot item in footList)
            {
                DataRow dr = dt.NewRow();
                dr["footName"] = item.Name;
                dr["footType"] = ((enumPinsType)item.PinsType).ToString ();
                
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
