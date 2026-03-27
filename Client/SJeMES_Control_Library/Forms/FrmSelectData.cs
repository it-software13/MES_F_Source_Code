using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_Control_Library.Forms
{
    public partial class FrmSelectData : MaterialForm
    {
        private MaterialSkinManager materialSkinManager;

        private DataTable _RetData;
        public DataTable RetData
        {
            get { return _RetData; }
            set
            {
                _RetData = value;
            }
        }

        private DataTable _Data;
        public DataTable Data
        {
            get { return _Data; }
            set
            {
                _Data = value;
                if (value != null)
                {
                    dataGridView1.DataSource = value.DefaultView;
                    dataGridView1.Update();
                    if (dataGridView1.Rows.Count > 0)
                    {
                        dataGridView1.Rows[0].Cells[0].Selected = true;
                    }
                }
               
            }
        }

        private int _DataTotal;
        public int DataTotal
        {
            get { return _DataTotal; }
            set
            {
                _DataTotal = value;
                if (value % PageRow == 0)
                {
                    PageTotal = value / PageRow;
                }
                else
                {
                    PageTotal = (value / PageRow) + 1;
                }
            }
        }

        private int _PageTotal;
        public int PageTotal
        {
            get { return _PageTotal; }
            set
            {
                _PageTotal = value;
                ucPagerControl21.PageCount = value;
            }
        }


        private string _SQL;
        public string SQL
        {
            get { return _SQL; }
            set { _SQL = value; }
        }

        private bool _OnlyOne;
        public bool OnlyOne
        {
            get { return _OnlyOne; }
            set { _OnlyOne = value; }
        }

        private SJeMES_Framework.Class.ClientClass _Client;
        public SJeMES_Framework.Class.ClientClass Client
        {
            get { return _Client; }
            set { _Client = value; }
        }

        private string _Where = string.Empty;
        public string Where
        {
            get { return _Where; }
            set { _Where = value; LoadData(); }
        }

        private string _OrderBy = string.Empty;
        public string OrderBy
        {
            get { return _OrderBy; }
            set { _OrderBy = value; LoadData(); }
        }

        private int _Page = 1;
        public int Page
        {
            get { return _Page; }
            set { _Page = value; LoadData(); }
        }

        private int _PageRow = 20;
        private OrgClass org;
        private string webServiceUrl;


        public int PageRow
        {
            get { return _PageRow; }
            set { _PageRow = value; LoadData(); }
        }



        public FrmSelectData(string SQL, bool OnlyOne, SJeMES_Framework.Class.ClientClass Client)
        {
            InitializeComponent();
            //SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
             MaterialSkinManager.Themes.LIGHT, materialSkinManager, this);
           

            this.SQL = SQL;
            this.OnlyOne = OnlyOne;
            this.Client = Client;
            //this.dataGridView1.MultiSelect = true;
            ucPagerControl21.PageIndex = 1;
            ucPagerControl21.PageSize = PageRow;

            LoadData();
        }

        /// <summary>
        /// 弹窗选择数据
        /// </summary>
        /// <param name="SQL">Sql语句</param>
        /// <param name="OnlyOne">是否单选</param>
        /// <param name="Client">ClientClass 对象</param>
        /// <param name="ColumnsName">隐藏列</param>
        public FrmSelectData(string SQL, bool OnlyOne, SJeMES_Framework.Class.ClientClass Client, string ColumnsName)
        {
            InitializeComponent();

            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
             MaterialSkinManager.Themes.LIGHT, materialSkinManager, this);

            this.SQL = SQL;
            this.OnlyOne = OnlyOne;
            this.Client = Client;
            //this.dataGridView1.MultiSelect = true;
            ucPagerControl21.PageIndex = 1;
            ucPagerControl21.PageSize = PageRow;

            LoadData();
            string[] arr = ColumnsName.Split(',');
            if (dataGridView1.Rows.Count>0)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    dataGridView1.Columns[arr[i]].Visible = false;
                }
            }
        }



        private void LoadData()
        {
            try
            {

                List<string> DataColumn = SJeMES_Control_Library.Controls.ModuleHelper.GetDataColumn(SQL, this.Client);

                string over = "order by " + DataColumn[0] + @" asc";

                if (!string.IsNullOrEmpty(OrderBy))
                {
                    over = OrderBy;
                }

                DataTable dt = new DataTable();
                Dictionary<string, object> ret = Client.GetDataTable(
                    this.SQL, this.Where, over, this.PageRow.ToString(), this.Page.ToString());

                this.DataTotal = Convert.ToInt32(ret["Total"].ToString());
                if (this.DataTotal == 0)
                {
                    foreach (string s in DataColumn)
                    {
                        dt.Columns.Add(s);
                    }
                }
                else
                {
                    dt = ret["Data"] as System.Data.DataTable;
                }

                this.Data = dt;
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void ucPagerControl21_ShowSourceChanged(object currentSource)
        {
            if (PageRow != ucPagerControl21.PageSize)
                PageRow = ucPagerControl21.PageSize;
            if (Page != ucPagerControl21.PageIndex)
                Page = ucPagerControl21.PageIndex;
        }

        private void ucSelectTool1_SelectData(object sender, EventArgs e)
        {
            this.Where = "@ALL@" + ucSelectTool1.WhereKey;
        }

        private void ucBtnImg4_BtnClick(object sender, EventArgs e)
        {
            ReturnData();
        }

        private void ReturnData()
        {
            RetData = Data.Clone();
            List<int> RowIndexs = new List<int>();
            //for (int i = 0; i < dataGridView1.SelectedCells.Count; i++)
            //{
            //    int RowIndex = dataGridView1.SelectedCells[i].RowIndex;
            //    if (!RowIndexs.Contains(RowIndex))
            //    {
            //        DataRow dr = RetData.NewRow();
            //        foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
            //        {
            //            if (!dgvc.Name.Equals("check"))
            //            {
            //                dr[dgvc.HeaderText] = dataGridView1.Rows[RowIndex].Cells[dgvc.HeaderText].Value;
            //            }
            //        }
            //        RetData.Rows.Add(dr);
            //        RowIndexs.Add(RowIndex);
            //    }
            //}

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                bool check = Convert.ToBoolean(dataGridView1.Rows[i].Cells["check"].Value);
                //int RowIndex = dataGridView1.SelectedCells[i].RowIndex;
                if (check)
                {
                    DataRow dr = RetData.NewRow();
                    foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                    {
                        if (!dgvc.Name.Equals("check"))
                        {
                            dr[dgvc.HeaderText] = dataGridView1.Rows[i].Cells[dgvc.HeaderText].Value;
                        }
                    }
                    RetData.Rows.Add(dr);
                    //RowIndexs.Add(RowIndex);
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ReturnData();
            }
        }

        /// <summary>
        /// 单击单元格信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0 && e.RowIndex >= 0)
                {
                    //只能选择一行
                    if (this.OnlyOne)
                    {
                        foreach (DataGridViewRow dr in dataGridView1.Rows)
                        {
                            dr.Cells["check"].Value = false;
                        }
                    }
                    //可以选择多行
                    else
                    {
                    }

                    bool check = Convert.ToBoolean(dataGridView1.CurrentRow.Cells["check"].Value);
                    if (check)
                    {
                        dataGridView1.CurrentRow.Cells["check"].Value = false;
                    }
                    else
                    {
                        dataGridView1.CurrentRow.Cells["check"].Value = true;
                    }



                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ucSelectTool1_Load(object sender, EventArgs e)
        {

        }
    }
}
