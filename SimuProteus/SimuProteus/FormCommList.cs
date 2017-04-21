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
    public partial class FormCommList : Form
    {
        List<CommunicateInfo> contentList = null;
        public FormCommList(List<CommunicateInfo> communicateList)
        {
            this.contentList = communicateList;

            InitializeComponent();

            this.RefreshData();
        }

        private DataTable ConstructData(List<CommunicateInfo> datalist)
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("number");
            dt.Columns.Add(dc); dc = new DataColumn("direct");
            dt.Columns.Add(dc); dc = new DataColumn("content");
            dt.Columns.Add(dc); dc = new DataColumn("time");
            dt.Columns.Add(dc);

            int idx = 1;
            foreach (CommunicateInfo item in datalist)
            {
                DataRow dr = dt.NewRow();
                dr["number"] = idx++;
                dr["direct"] = item.DirectUp ? "上传" : "下发";
                dr["content"] = item.Content;
                dr["time"] = item.Time.ToShortTimeString();

                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void RefreshData()
        {
            DataTable dt = this.ConstructData(this.contentList);
            this.dgvDataList.DataSource = dt;
        }
    }
}
