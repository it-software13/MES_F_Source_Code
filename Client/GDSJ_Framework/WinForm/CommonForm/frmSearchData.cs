
using SJeMES_Framework.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GDSJ_Framework.WinForm.CommonForm
{
    public partial class frmSearchData : Form
    {
        string SeletDataSql = string.Empty;
        string SQL = string.Empty;
        string whereStr = string.Empty;
        string orderStr = string.Empty;
       
        bool IsOneLine = false;
        bool IsSelectFirstItem = false;
        bool IsAdvanced = false;
        Class.OrgClass Org; 

        public string bodyStr = string.Empty;
        public string pageName = string.Empty;
        public string ReturnDataXML = string.Empty;
        public string WebServiceUrl = string.Empty;

        public SJeMES_Framework.Class.ClientClass Client = new SJeMES_Framework.Class.ClientClass();

        List<string> Order=new List<string>();
        List<string> OrderType=new List<string>();
         

        DataTable DT = null;

        int Page = 1;

        int PageCount = 0;

        int PageRow = 0;

        public FormDesign.Forms.FormClass FC;

        public FormXML.Forms.FormClass FC2;

        DBHelper.DataBase DB ;

        #region 构造函数
        public frmSearchData(DBHelper.DataBase DB, string SQL, bool IsOneLine, bool IsSelectFirstItem)
        {
            InitializeComponent();
             
            this.SeletDataSql = SQL;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            
            this.IsOneLine = IsOneLine;
            this.IsSelectFirstItem = IsSelectFirstItem;
            this.DB = DB;

            try
            {
                SQL = @"
SELECT TOP(1)*
FROM
(" + this.SeletDataSql + @")
TMP
";
                DataTable dt = DB.GetDataTable(SQL);

                cbWhereKey.Items.Add("模糊查询");

                foreach (DataColumn C in dt.Columns)
                {
                    cbWhereKey.Items.Add(C.ColumnName);
                }

                cbWhereKey.SelectedIndex = 0;

                DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                newColumn.HeaderText = "";
                dataGridView1.Columns.Insert(0, newColumn);

                Page = 1;
                SelectData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(SQL);
            }
        }

        public frmSearchData(DBHelper.DataBase DB, string SQL, bool IsOneLine, bool IsSelectFirstItem, int PageRow)
        {
            InitializeComponent();
            this.SeletDataSql = SQL;
            this.PageRow = PageRow;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


            this.IsOneLine = IsOneLine;
            this.IsSelectFirstItem = IsSelectFirstItem;
            this.DB = DB;

   
            try
            {
                SQL = @"
SELECT TOP(1)*
FROM
(" + this.SeletDataSql + @")
TMP
";
                DataTable dt = DB.GetDataTable(SQL);


                cbWhereKey.Items.Add("模糊查询");

                foreach (DataColumn C in dt.Columns)
                {
                    cbWhereKey.Items.Add(C.ColumnName);
                }

                cbWhereKey.SelectedIndex = 0;

                DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                newColumn.HeaderText = "";
                dataGridView1.Columns.Insert(0, newColumn);

                Page = 1;
                SelectData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(SQL);
            }
        }

        

        public frmSearchData(string WebServiceUrl, string SQL, bool IsOneLine, bool IsSelectFirstItem)
        {
            InitializeComponent();
            this.SeletDataSql = SQL;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            this.IsOneLine = IsOneLine;
            this.IsSelectFirstItem = IsSelectFirstItem;
            this.WebServiceUrl = WebServiceUrl;


            try
            {
                SQL = @"
SELECT TOP(1)*
FROM
(" + this.SeletDataSql + @")
TMP
";




                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("sql", SQL);

                string XML = GDSJ_Framework.Common.WebServiceHelper.RunService(this.WebServiceUrl, "SJEMS_API", "SJEMS_API.DataBase", "GetDataTable", p);

                if (Convert.ToBoolean(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>")))
                {
                    string dtXML = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");

                    DataTable dt = GDSJ_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);


                    cbWhereKey.Items.Add("模糊查询");

                    foreach (DataColumn C in dt.Columns)
                    {
                        cbWhereKey.Items.Add(C.ColumnName);
                    }

                    cbWhereKey.SelectedIndex = 0;

                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                    newColumn.HeaderText = "";
                    dataGridView1.Columns.Insert(0, newColumn);

                    Page = 1;
                    SelectData();
                }
                else
                {
                    MessageBox.Show(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>"));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(SQL);
            }
        }

        public frmSearchData(string WebServiceUrl, string SQL, bool IsOneLine, bool IsSelectFirstItem, int PageRow)
        {
            InitializeComponent();
            
            this.SeletDataSql = SQL;
            this.PageRow = PageRow;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            this.IsOneLine = IsOneLine;
            this.IsSelectFirstItem = IsSelectFirstItem;
            this.WebServiceUrl = WebServiceUrl;

         
            try
            {
                SQL = @"
SELECT TOP(1)*
FROM
(" + this.SeletDataSql + @")
TMP
";




                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("sql", SQL);

                string XML = GDSJ_Framework.Common.WebServiceHelper.RunService(this.WebServiceUrl, "SJEMS_API", "SJEMS_API.DataBase", "GetDataTable", p);

                if (Convert.ToBoolean(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>")))
                {
                    string dtXML = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");

                    DataTable dt = GDSJ_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);

                    cbWhereKey.Items.Add("模糊查询");

                    foreach (DataColumn C in dt.Columns)
                    {
                        cbWhereKey.Items.Add(C.ColumnName);
                    }

                    cbWhereKey.SelectedIndex = 0;

                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                    newColumn.HeaderText = "";
                    dataGridView1.Columns.Insert(0, newColumn);

                    Page = 1;
                    SelectData();
                }
                else
                {
                    MessageBox.Show(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>"));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(SQL);
            }
        }

        public frmSearchData(Class.OrgClass Org, string WebServiceUrl, string SQL, bool IsOneLine, bool IsSelectFirstItem)
        {
            InitializeComponent();
          
            this.SeletDataSql = SQL;
            this.Org = Org;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            this.IsOneLine = IsOneLine;
            this.IsSelectFirstItem = IsSelectFirstItem;
            this.WebServiceUrl = WebServiceUrl;

     
            try
            {
                SQL = @"
SELECT TOP(1)*
FROM
(" + this.SeletDataSql + @")
TMP
";




                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("sql", SQL);
                string XML = string.Empty;

                if (Org == null)
                {
                    XML = GDSJ_Framework.Common.WebServiceHelper.RunService(this.WebServiceUrl, "SJEMS_API", "SJEMS_API.DataBase", "GetDataTable", p);
                }
                else
                {
                    XML = GDSJ_Framework.Common.WebServiceHelper.RunService(Org,this.WebServiceUrl, "SJEMS_API", "SJEMS_API.DataBase", "GetDataTable", p);
                }

                if (Convert.ToBoolean(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>")))
                {
                    string dtXML = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");

                    DataTable dt = GDSJ_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);


                    cbWhereKey.Items.Add("模糊查询");

                    foreach (DataColumn C in dt.Columns)
                    {
                        cbWhereKey.Items.Add(C.ColumnName);
                    }

                    cbWhereKey.SelectedIndex = 0;

                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                    newColumn.HeaderText = "";
                    dataGridView1.Columns.Insert(0, newColumn);

                    Page = 1;
                    SelectData();
                }
                else
                {
                    MessageBox.Show(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>"));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(SQL);
            }
        }

        public frmSearchData(Class.OrgClass Org, string WebServiceUrl, string SQL, bool IsOneLine, bool IsSelectFirstItem, int PageRow)
        {
            InitializeComponent();
         
            this.SeletDataSql = SQL;
            this.PageRow = PageRow;
            this.Org = Org;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            this.IsOneLine = IsOneLine;
            this.IsSelectFirstItem = IsSelectFirstItem;
            this.WebServiceUrl = WebServiceUrl;

          
            try
            {
                SQL = @"
SELECT TOP(1)*
FROM
(" + this.SeletDataSql + @")
TMP
";




                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("sql", SQL);

                string XML = GDSJ_Framework.Common.WebServiceHelper.RunService(this.WebServiceUrl, "SJEMS_API", "SJEMS_API.DataBase", "GetDataTable", p);

                if (Convert.ToBoolean(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>")))
                {
                    string dtXML = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");

                    DataTable dt = GDSJ_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);

                    cbWhereKey.Items.Add("模糊查询");

                    foreach (DataColumn C in dt.Columns)
                    {
                        cbWhereKey.Items.Add(C.ColumnName);
                    }

                    cbWhereKey.SelectedIndex = 0;

                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                    newColumn.HeaderText = "";
                    dataGridView1.Columns.Insert(0, newColumn);

                    Page = 1;
                    SelectData();
                }
                else
                {
                    MessageBox.Show(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>"));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(SQL);
            }
        }

        public frmSearchData(string WebServiceUrl, string SQL, List<string> Order, List<string> OrderType, bool IsOneLine, bool IsSelectFirstItem)
        {
            InitializeComponent();
           
            this.SeletDataSql = SQL;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            this.IsOneLine = IsOneLine;
            this.IsSelectFirstItem = IsSelectFirstItem;
            this.WebServiceUrl = WebServiceUrl;
            this.Order = Order;
            this.OrderType = OrderType;

           
            try
            {
                SQL = @"
SELECT TOP(1)*
FROM
(" + this.SeletDataSql + @")
TMP
";




                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("sql", SQL);

                string XML = GDSJ_Framework.Common.WebServiceHelper.RunService(this.WebServiceUrl, "SJEMS_API", "SJEMS_API.DataBase", "GetDataTable", p);

                if (Convert.ToBoolean(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>")))
                {
                    string dtXML = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");

                    DataTable dt = GDSJ_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);


                    cbWhereKey.Items.Add("模糊查询");

                    foreach (DataColumn C in dt.Columns)
                    {
                        cbWhereKey.Items.Add(C.ColumnName);
                    }

                    cbWhereKey.SelectedIndex = 0;

                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                    newColumn.HeaderText = "";
                    dataGridView1.Columns.Insert(0, newColumn);

                    Page = 1;
                    SelectData();
                }
                else
                {
                    MessageBox.Show(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>"));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(SQL);
            }
        }

        public frmSearchData(string WebServiceUrl, string SQL, List<string> Order, List<string> OrderType, bool IsOneLine, bool IsSelectFirstItem, int PageRow)
        {
            InitializeComponent();
          
            this.SeletDataSql = SQL;
            this.PageRow = PageRow;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            this.IsOneLine = IsOneLine;
            this.IsSelectFirstItem = IsSelectFirstItem;
            this.WebServiceUrl = WebServiceUrl;
            this.Order = Order;
            this.OrderType = OrderType;

            try
            {
                SQL = @"
SELECT TOP(1)*
FROM
(" + this.SeletDataSql + @")
TMP
";




                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("sql", SQL);

                string XML = GDSJ_Framework.Common.WebServiceHelper.RunService(this.WebServiceUrl, "SJEMS_API", "SJEMS_API.DataBase", "GetDataTable", p);

                if (Convert.ToBoolean(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>")))
                {
                    string dtXML = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");

                    DataTable dt = GDSJ_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);


                    cbWhereKey.Items.Add("模糊查询");

                    foreach (DataColumn C in dt.Columns)
                    {
                        cbWhereKey.Items.Add(C.ColumnName);
                    }

                    cbWhereKey.SelectedIndex = 0;

                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                    newColumn.HeaderText = "";
                    dataGridView1.Columns.Insert(0, newColumn);

                    Page = 1;
                    SelectData();
                }
                else
                {
                    MessageBox.Show(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>"));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(SQL);
            }
        }

        public frmSearchData(Class.OrgClass Org, string WebServiceUrl, string SQL, List<string> Order, List<string> OrderType, bool IsOneLine, bool IsSelectFirstItem)
        {
            InitializeComponent();
            this.SeletDataSql = SQL;
            this.Org = Org;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            this.IsOneLine = IsOneLine;
            this.IsSelectFirstItem = IsSelectFirstItem;
            this.WebServiceUrl = WebServiceUrl;
            this.Order = Order;
            this.OrderType = OrderType;

         
            try
            {
                SQL = @"
SELECT TOP(1)*
FROM
(" + this.SeletDataSql + @")
TMP
";




                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("sql", SQL);

                string XML = string.Empty;
                if (Org == null)
                {
                    XML= GDSJ_Framework.Common.WebServiceHelper.RunService(this.WebServiceUrl, "SJEMS_API", "SJEMS_API.DataBase", "GetDataTable", p);
                }
                else
                {
                    XML= GDSJ_Framework.Common.WebServiceHelper.RunService(Org,this.WebServiceUrl, "SJEMS_API", "SJEMS_API.DataBase", "GetDataTable", p);
                }

                if (Convert.ToBoolean(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>")))
                {
                    string dtXML = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");

                    DataTable dt = GDSJ_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);

                    cbWhereKey.Items.Add("模糊查询");

                    foreach (DataColumn C in dt.Columns)
                    {
                        cbWhereKey.Items.Add(C.ColumnName);
                    }

                    cbWhereKey.SelectedIndex = 0;

                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                    newColumn.HeaderText = "";
                    dataGridView1.Columns.Insert(0, newColumn);

                    Page = 1;
                    SelectData();
                }
                else
                {
                    MessageBox.Show(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>"));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(SQL);
            }
        }

        public frmSearchData(Class.OrgClass Org, string WebServiceUrl, string SQL, List<string> Order, List<string> OrderType, bool IsOneLine, bool IsSelectFirstItem, int PageRow)
        {
            InitializeComponent();
           
            this.SeletDataSql = SQL;
            this.PageRow = PageRow;
            this.Org = Org;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            this.IsOneLine = IsOneLine;
            this.IsSelectFirstItem = IsSelectFirstItem;
            this.WebServiceUrl = WebServiceUrl;
            this.Order = Order;
            this.OrderType = OrderType;

      
            try
            {
                SQL = @"
SELECT TOP(1)*
FROM
(" + this.SeletDataSql + @")
TMP
";




                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("sql", SQL);

                string XML = GDSJ_Framework.Common.WebServiceHelper.RunService(this.WebServiceUrl, "SJEMS_API", "SJEMS_API.DataBase", "GetDataTable", p);

                if (Convert.ToBoolean(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>")))
                {
                    string dtXML = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");

                    DataTable dt = GDSJ_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);

                    cbWhereKey.Items.Add("模糊查询");

                    foreach (DataColumn C in dt.Columns)
                    {
                        cbWhereKey.Items.Add(C.ColumnName);
                    }

                    cbWhereKey.SelectedIndex = 0;

                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                    newColumn.HeaderText = "";
                    dataGridView1.Columns.Insert(0, newColumn);

                    Page = 1;
                    SelectData();
                }
                else
                {
                    MessageBox.Show(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>"));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(SQL);
            }
        }


        /// <summary>
        /// 万邦多语言弹窗
        /// </summary>
        /// <param name="client"></param>
        /// <param name="WebServiceUrl"></param>
        /// <param name="SQL"></param>
        /// <param name="IsOneLine"></param>
        /// <param name="IsSelectFirstItem"></param>

        DataTable cbDT = new DataTable();
        string FrmMenthName = string.Empty;
        string Language = string.Empty;
        public frmSearchData(string FrmMenthName, SJeMES_Framework.Class.ClientClass client, string WebServiceUrl, 
            string SQL,string Language, bool IsOneLine, bool IsSelectFirstItem)
        {
            InitializeComponent();


            if (string.IsNullOrEmpty(Language))
                Language = "cn";
            Language = Language.ToLower();
            this.FrmMenthName = FrmMenthName;
            this.Language = Language;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, client, WebServiceUrl, Language);

            this.SeletDataSql = SQL; 
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue; 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            this.IsOneLine = IsOneLine;
            this.IsSelectFirstItem = IsSelectFirstItem;
            this.WebServiceUrl = WebServiceUrl;

            try
            {
                SQL = @"
                        SELECT TOP(1)*
                        FROM
                        (" + this.SeletDataSql + @")
                        TMP
                        ";

                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("sql", SQL);

                string XML = GDSJ_Framework.Common.WebServiceHelper.RunService(this.WebServiceUrl, "SJEMS_API", "SJEMS_API.DataBase", "GetDataTable", p);

                if (Convert.ToBoolean(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>")))
                {
                    string dtXML = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");

                    DataTable dt = GDSJ_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);

                    cbDT.Columns.Add("code");
                    cbDT.Columns.Add("name"); 
                    DataRow dr = cbDT.NewRow();
                    dr["code"] = "模糊查询";
                    dr["name"] = Language.Equals("cn")? "模糊查询":(Language.Equals("en")? "Fuzzy Query" : "Hỏi Mờ");
                    cbDT.Rows.Add(dr);
                    string strNames = string.Empty;
                    if(dt!=null && dt.Rows.Count>0)
                    {
                        foreach (DataColumn C in dt.Columns)
                        {
                            dr = cbDT.NewRow();
                            dr["code"] = C.ColumnName;
                            dr["name"] = C.ColumnName;
                            cbDT.Rows.Add(dr);
                            strNames += C.ColumnName + ",";
                        }
                        strNames = "行号," + strNames.TrimEnd(',');
                    }

                    #region 将datatable的列保存到消息多语言表 SYSLAN05M 
                    if (!string.IsNullOrEmpty(strNames) && !string.IsNullOrEmpty(FrmMenthName))
                    {
                        string sql = string.Empty;
                        p = new Dictionary<string, string>();

                        sql = @"if not exists(select 1 from SYSLAN05M where msg='{0}')
                                insert into SYSLAN05M(msg,ui_en,ui_yn,ui_cn)values('{0}','{1}','{1}','{1}');";
                        sql = string.Format(sql, FrmMenthName, strNames);
                        p.Add("sql", sql);
                        GDSJ_Framework.Common.WebServiceHelper.RunService(this.WebServiceUrl,
                                "SJEMS_API", "SJEMS_API.DataBase", "ExecuteNonQuery", p);
                    }
                    #endregion

                    cbWhereKey.DataSource = cbDT;
                    cbWhereKey.DisplayMember = "name";
                    cbWhereKey.ValueMember = "code";
                    cbWhereKey.SelectedIndex = 0;

                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                    newColumn.HeaderText = "";
                    dataGridView1.Columns.Insert(0, newColumn);

                    Page = 1;
                    SelectData(FrmMenthName,Language);
                }
                else
                {
                    MessageBox.Show(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>"));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(SQL);
            }
        }

        public frmSearchData(OrgClass FOrg, string FrmMenthName, SJeMES_Framework.Class.ClientClass client, string WebServiceUrl,
            string SQL, string Language, bool IsOneLine, bool IsSelectFirstItem)
        {
            InitializeComponent();

            Class.OrgClass theOrg = new Class.OrgClass();

            theOrg.Org = FOrg.Org;
            theOrg.OrgName= FOrg.OrgName;
            theOrg.DBType = FOrg.DBType;
            theOrg.DBServer = FOrg.DBServer;
            theOrg.DBName= FOrg.DBName;
            theOrg.DBUser = FOrg.DBUser;
            theOrg.DBPassword = FOrg.DBPassword;
            theOrg.User.UserCode = FOrg.User.UserCode;
            theOrg.User.UserName = FOrg.User.UserName;
            this.Org = theOrg;

            if (string.IsNullOrEmpty(Language))
                Language = "cn";
            Language = Language.ToLower();
            this.FrmMenthName = FrmMenthName;
            this.Language = Language;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, client, WebServiceUrl, Language);

            this.SeletDataSql = SQL;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            this.IsOneLine = IsOneLine;
            this.IsSelectFirstItem = IsSelectFirstItem;
            this.WebServiceUrl = WebServiceUrl;

            try
            {
                SQL = @"
                        SELECT TOP(1)*
                        FROM
                        (" + this.SeletDataSql + @")
                        TMP
                        ";

                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("sql", SQL);
                string XML = string.Empty;
                if (this.Org == null)
                {
                    XML = GDSJ_Framework.Common.WebServiceHelper.RunService(this.WebServiceUrl, "SJEMS_API", "SJEMS_API.DataBase", "GetDataTable", p);
                }
                else
                {
                    XML = GDSJ_Framework.Common.WebServiceHelper.RunService(this.Org, this.WebServiceUrl, "SJEMS_API", "SJEMS_API.DataBase", "GetDataTable", p);
                }

                //string XML = GDSJ_Framework.Common.WebServiceHelper.RunService(this.WebServiceUrl, "SJEMS_API", "SJEMS_API.DataBase", "GetDataTable", p);

                if (Convert.ToBoolean(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>")))
                {
                    string dtXML = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");

                    DataTable dt = GDSJ_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);

                    cbDT.Columns.Add("code");
                    cbDT.Columns.Add("name");
                    DataRow dr = cbDT.NewRow();
                    dr["code"] = "模糊查询";
                    dr["name"] = Language.Equals("cn") ? "模糊查询" : (Language.Equals("en") ? "Fuzzy Query" : "Hỏi Mờ");
                    cbDT.Rows.Add(dr);
                    string strNames = string.Empty;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataColumn C in dt.Columns)
                        {
                            dr = cbDT.NewRow();
                            dr["code"] = C.ColumnName;
                            dr["name"] = C.ColumnName;
                            cbDT.Rows.Add(dr);
                            strNames += C.ColumnName + ",";
                        }
                        strNames = "行号," + strNames.TrimEnd(',');
                    }

                    #region 将datatable的列保存到消息多语言表 SYSLAN05M 
                    if (!string.IsNullOrEmpty(strNames) && !string.IsNullOrEmpty(FrmMenthName))
                    {
                        string sql = string.Empty;
                        p = new Dictionary<string, string>();

                        sql = @"if not exists(select 1 from SYSLAN05M where msg='{0}')
                                insert into SYSLAN05M(msg,ui_en,ui_yn,ui_cn)values('{0}','{1}','{1}','{1}');";
                        sql = string.Format(sql, FrmMenthName, strNames);
                        p.Add("sql", sql);
                        GDSJ_Framework.Common.WebServiceHelper.RunService(this.WebServiceUrl,
                                "SJEMS_API", "SJEMS_API.DataBase", "ExecuteNonQuery", p);
                    }
                    #endregion

                    cbWhereKey.DataSource = cbDT;
                    cbWhereKey.DisplayMember = "name";
                    cbWhereKey.ValueMember = "code";
                    cbWhereKey.SelectedIndex = 0;

                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                    newColumn.HeaderText = "";
                    dataGridView1.Columns.Insert(0, newColumn);

                    Page = 1;
                    SelectData(FrmMenthName, Language);
                }
                else
                {
                    MessageBox.Show(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>"));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(SQL);
            }
        }
        #endregion

        private void btnSelect_Click(object sender, EventArgs e)
        {
            IsAdvanced = false;
            Page = 1;
            if(!string.IsNullOrEmpty(FrmMenthName) && !string.IsNullOrEmpty(Language))
                SelectData(FrmMenthName, Language);
            else
                SelectData();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            Page = 1;
            SelectData();
        }

        public void SetWhereKey(string Key)
        {
            txtWhereKey.Text = Key;
        }

        public void SetFC(FormDesign.Forms.FormClass FC)
        {
            this.FC = FC;
        }


        public void SetFC(FormXML.Forms.FormClass FC)
        {
            this.FC2 = FC;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            GetPageCount();
            Page = PageCount;
            SelectData();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            GetPageCount();
            if (Page > 1)
            {

                Page -= 1;

                SelectData();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetPageCount();
            if (Page < PageCount)
            {
                Page += 1;
                SelectData();
            }
        }

        private void GetPageCount()
        {

            try
            {
                SQL = @"
SELECT
COUNT(*)
FROM
("+SeletDataSql+@")
T
WHERE 1=1 "+ whereStr+@"
";
                if (DB != null)
                {
                    PageCount = DB.GetInt32(SQL);
                }
                else
                {
                    Dictionary<string, string> p = new Dictionary<string, string>();
                    p.Add("sql", SQL);

                    string XML = string.Empty;
                    if (Org == null)
                    {
                        XML = GDSJ_Framework.Common.WebServiceHelper.RunService(this.WebServiceUrl, "SJEMS_API", "SJEMS_API.DataBase", "GetString", p);
                    }
                    else
                    {
                        XML = GDSJ_Framework.Common.WebServiceHelper.RunService(Org, this.WebServiceUrl, "SJEMS_API", "SJEMS_API.DataBase", "GetString", p);
                    }

                    if (Convert.ToBoolean(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>")))
                    {
                        string sXML = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");

                        PageCount = Convert.ToInt32(sXML);
                    }
                    else
                    {
                        MessageBox.Show(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>"));
                    }
                }

                labDataCount.Text = "Count：" + PageCount;

                
                if(PageRow ==-1)
                {
                    PageRow = PageCount;
                }
                else if(PageRow ==0)
                {
                    
                    string sql = @"
SELECT
parameters_value
FROM SYS002M(NOLOCK)
WHERE parameters_code='PageRows'
";
                    Dictionary<string, string> p = new Dictionary<string, string>();
                  
                    string XML = string.Empty;
                    try
                    {
                        if (Org == null)
                        {
                            PageRow = Convert.ToInt32(GDSJ_Framework.Common.WebServiceHelper.GetString(this.WebServiceUrl, sql, p));
                        }
                        else
                        {
                            PageRow = Convert.ToInt32(GDSJ_Framework.Common.WebServiceHelper.GetString(Org, this.WebServiceUrl, sql, p));
                        }
                    }
                    catch(Exception ex)
                    {
                        PageRow = PageCount;
                    }

                }

                if (PageCount != 0)
                {

                    if (PageCount % PageRow == 0)
                    {
                        PageCount = PageCount / PageRow;
                    }
                    else
                    {
                        PageCount = (PageCount / PageRow) + 1;
                    }

                }
                labPageNum.Text = "Current Page： " + Page + "/" + PageCount;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(SQL);
            }

        }

        public void SelectData()
        {
            
            try
            {
                if (!IsAdvanced)
                    GetWhere();
                GetPageCount();

          
                GetOrder();

                if(string.IsNullOrEmpty(orderStr))
                {
                    orderStr = "ORDER BY [" + cbWhereKey.Items[1] + @"]";
                }

                SQL = @"
SELECT 
ROW_NUMBER() OVER ("+ orderStr+@") AS '行号',
*
FROM
(" + SeletDataSql+@") T
where 1=1 "+ whereStr + @"
";


                SQL = @"
SELECT TOP "+ PageRow +@"* 
FROM 
        (
" + SQL + @"
        ) TMP
WHERE TMP.行号 > "+ PageRow +"* (" + Page + @"-1)
";
                if (DB != null)
                {
                    DT = DB.GetDataTable(SQL);
                }
                else
                {
                    Dictionary<string, string> p = new Dictionary<string, string>();
                    p.Add("sql", SQL);

                    string XML = string.Empty;
                    if(Org ==null)
                    {
                        XML = GDSJ_Framework.Common.WebServiceHelper.RunService(this.WebServiceUrl, "SJEMS_API", "SJEMS_API.DataBase", "GetDataTable", p);
                    }
                    else
                    {
                        XML = GDSJ_Framework.Common.WebServiceHelper.RunService(Org,this.WebServiceUrl, "SJEMS_API", "SJEMS_API.DataBase", "GetDataTable", p);
                    }

                    if (Convert.ToBoolean(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>")))
                    {
                        string dtXML = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");

                        DT = GDSJ_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);
                    }
                    else
                    {
                        MessageBox.Show(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>"));
                    }
                }

                dataGridView1.DataSource = DT.DefaultView;

           

                dataGridView1.Columns[0].Width = 40;
                dataGridView1.Columns[1].Width = 60;

                dataGridView1.Update();

                
                

                if(IsSelectFirstItem && dataGridView1.Rows.Count>0)
                {
                    dataGridView1.Rows[0].Cells[0].Value = true;
                }

              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(SQL);
            }
        }

        /// <summary>
        /// 万邦多语言查询
        /// </summary>
        /// <param name="FrmMenthName"></param>
        /// <param name="Language"></param>
        public void SelectData(string FrmMenthName, string Language)
        {
            try
            {
                if (!IsAdvanced)
                    GetWhereW();
                GetPageCount();
                GetOrder();

                
                if(string.IsNullOrEmpty(orderStr) && cbDT.Rows.Count>0)
                {
                    orderStr = "order by " + cbDT.Rows[1]["code"].ToString();
                }

                SQL = @"
                        SELECT 
                        ROW_NUMBER() OVER (" + orderStr + @") AS '行号',
                        *
                        FROM
                        (" + SeletDataSql + @") T
                        where 1=1 " + whereStr + @"
                        ";  
                SQL = @"
                        SELECT TOP " + PageRow + @"* 
                        FROM 
                                (
                        " + SQL + @"
                                ) TMP
                        WHERE TMP.行号 > " + PageRow + "* (" + Page + @"-1)
";
                if (DB != null)
                {
                    DT = DB.GetDataTable(SQL);
                }
                else
                {
                    Dictionary<string, string> p = new Dictionary<string, string>();
                    p.Add("sql", SQL);

                    string XML = string.Empty;
                    if (Org == null)
                    {
                        XML = GDSJ_Framework.Common.WebServiceHelper.RunService(this.WebServiceUrl, "SJEMS_API", "SJEMS_API.DataBase", "GetDataTable", p);
                    }
                    else
                    {
                        XML = GDSJ_Framework.Common.WebServiceHelper.RunService(Org, this.WebServiceUrl, "SJEMS_API", "SJEMS_API.DataBase", "GetDataTable", p);
                    }

                    if (Convert.ToBoolean(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>")))
                    {
                        string dtXML = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");

                        DT = GDSJ_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);
                    }
                    else
                    {
                        MessageBox.Show(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>"));
                    }
                }
                dataGridView1.DataSource = DT.DefaultView;

                #region 查询列明多语言信息  
                if (dataGridView1.DataSource != null && dataGridView1.Rows.Count > 0)
                {
                    string sql = @"select msg,ui_en,ui_yn,ui_cn from SYSLAN05M(nolock) where msg='" + FrmMenthName + "'";
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("sql", sql);

                    string retXML = string.Empty;
                    //if (Org == null)
                    //{
                     retXML = GDSJ_Framework.Common.WebServiceHelper.RunService(this.WebServiceUrl, "SJEMS_API", "SJEMS_API.DataBase", "GetDataTable", dic);
                    //}
                    //else
                    //{
                    //    retXML = GDSJ_Framework.Common.WebServiceHelper.RunService(Org, this.WebServiceUrl, "SJEMS_API", "SJEMS_API.DataBase", "GetDataTable", dic);
                    //}
                    if (Convert.ToBoolean(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(retXML, "<IsSuccess>", "</IsSuccess>")))
                    {
                        string dtXML = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(retXML, "<RetData>", "</RetData>");
                        DataTable dt = GDSJ_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            string[] str_ui_cn = dt.Rows[0]["ui_cn"].ToString().Split(',');
                            string[] str_ui_en = dt.Rows[0]["ui_en"].ToString().Split(',');
                            string[] str_ui_yn = dt.Rows[0]["ui_yn"].ToString().Split(',');

                            if (Language.Equals("cn") && str_ui_cn.Length > 0)
                            {
                                for (int i = 0; i < str_ui_cn.Length; i++)
                                {
                                    if ((i + 1) <= dataGridView1.Columns.Count-1)
                                    {
                                        dataGridView1.Columns[i+1].HeaderText = str_ui_cn[i];
                                    } 
                                }

                                if(cbDT.Rows.Count == str_ui_cn.Length)
                                {
                                    for (int j = 1; j < cbDT.Rows.Count; j++)
                                    {
                                        cbDT.Rows[j]["name"] = str_ui_cn[j];
                                    }
                                }
                            }
                            else if (Language.Equals("en") && str_ui_en.Length > 0)
                            {
                                for (int i = 0; i < str_ui_en.Length; i++)
                                {
                                    if ((i + 1) <= dataGridView1.Columns.Count-1)
                                    {
                                        dataGridView1.Columns[i+1].HeaderText = str_ui_en[i];
                                    } 
                                }
                                if (cbDT.Rows.Count == str_ui_en.Length)
                                {
                                    for (int j = 1; j < cbDT.Rows.Count; j++)
                                    {
                                        cbDT.Rows[j]["name"] = str_ui_en[j];
                                    }
                                }
                            }
                            else if (Language.Equals("hk") && str_ui_yn.Length > 0)
                            {
                                for (int i = 0; i < str_ui_yn.Length; i++)
                                {
                                    if ((i + 1) <= dataGridView1.Columns.Count-1)
                                    {
                                        dataGridView1.Columns[i+1].HeaderText = str_ui_yn[i];
                                    } 
                                }
                                if (cbDT.Rows.Count == str_ui_yn.Length)
                                {
                                    for (int j = 1; j < cbDT.Rows.Count; j++)
                                    {
                                        cbDT.Rows[j]["name"] = str_ui_yn[j];
                                    }
                                }
                            }
                        }
                    }
                    else
                        MessageBox.Show(GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(retXML, "<RetData>", "</RetData>"));
                }
                #endregion

                //cbWhereKey.DataSource = cbDT;
                //cbWhereKey.DisplayMember = "name";
                //cbWhereKey.ValueMember = "code";
                //cbWhereKey.SelectedIndex = 0;

                dataGridView1.Columns[0].Width = 40;
                dataGridView1.Columns[1].Width = 60;
                dataGridView1.Update();


                if (IsSelectFirstItem && dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.Rows[0].Cells[0].Value = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(SQL);
            }
        }


        private void GetOrder()
        {
            orderStr = string.Empty;
            for(int i=0;i<Order.Count;i++)
            {
                if(string.IsNullOrEmpty(orderStr))
                {
                    orderStr += " ORDER BY ";
                }

                if (Order[i].IndexOf("[") > -1 && Order[i].IndexOf("]") > -1)
                {
                    orderStr += "[" + Order[i].Replace("["+
                        GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(Order[i],"[","]")+"]","") + @"] " + OrderType[i];
                }
                else
                {
                    orderStr += "[" + Order[i] + @"] " + OrderType[i];
                }

                if(i<Order.Count-1)
                {
                    orderStr += ",";
                }
            }
        }


        public void GetWhere()
        {
            this.whereStr = string.Empty;
            if (!string.IsNullOrEmpty(txtWhereKey.Text.Trim()))
            {
                if (cbWhereKey.Text == "模糊查询")
                {
                    foreach (string s in cbWhereKey.Items)
                    {
                        if (s != "模糊查询")
                            if (!string.IsNullOrEmpty(this.whereStr))
                            {
                                this.whereStr += " OR [" + s + @"] LIKE '%" + txtWhereKey.Text.Trim() + @"%' ";
                            }
                            else
                            {
                                this.whereStr += " [" + s + @"] LIKE '%" + txtWhereKey.Text.Trim() + @"%' ";
                            }


                    }
                    whereStr = " AND ( " + this.whereStr + @" ) ";
                }
                else
                {

                    this.whereStr = " AND [" + cbWhereKey.Text.Trim() + @"] LIKE '%" + txtWhereKey.Text.Trim() + @"%' ";
                }
            }
            else
            {
                this.whereStr = string.Empty;
            }

        }

        public void GetWhereW()
        {
            try
            {
                this.whereStr = string.Empty;
                if (!string.IsNullOrEmpty(txtWhereKey.Text.Trim()))
                {
                    if (cbWhereKey.SelectedIndex == 0)
                    {
                        foreach (DataRow dr in cbDT.Rows)
                        {
                            string code = dr["code"].ToString();
                            if(!code.Equals("模糊查询"))
                            {
                                if (!string.IsNullOrEmpty(this.whereStr))
                                {
                                    this.whereStr += " OR [" + code + @"] LIKE '%" + txtWhereKey.Text.Trim() + @"%' ";
                                }
                                else
                                {
                                    this.whereStr += " [" + code + @"] LIKE '%" + txtWhereKey.Text.Trim() + @"%' ";
                                }
                            }
                        } 

                        whereStr = " AND ( " + this.whereStr + @" ) ";
                    }
                    else if (cbWhereKey.SelectedIndex > 0)
                    {
                        this.whereStr = " AND [" + cbWhereKey.SelectedValue + @"] LIKE '%" + txtWhereKey.Text.Trim() + @"%' ";
                    }
                }
                else
                {
                    this.whereStr = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (IsOneLine)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Cells[0].Value = false;
                    }
                }

                dataGridView1.Rows[e.RowIndex].Cells[0].Value = !Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {


            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(r.Cells[0].Value))
                {
                    if (string.IsNullOrEmpty(ReturnDataXML))
                    {
                        ReturnDataXML += "<ReturnDataXML>";
                    }

                    ReturnDataXML += "<Columns>";
                    for (int i = 1; i < dataGridView1.Columns.Count; i++)
                    {
                        ReturnDataXML += dataGridView1.Columns[i].Name + @"@;";
                    }

                    ReturnDataXML = ReturnDataXML.Remove(ReturnDataXML.Length - 2);
                    ReturnDataXML += "</Columns>";

                    ReturnDataXML += "<Row>";

                    for(int i=1;i<dataGridView1.Columns.Count;i++)
                    {
                        ReturnDataXML += "<" + dataGridView1.Columns[i].Name + ">"
                            + r.Cells[dataGridView1.Columns[i].Name].Value.ToString()
                            + "</" + dataGridView1.Columns[i].Name + ">";
                    }

                    ReturnDataXML += "</Row>";

                }
            }

            if (!string.IsNullOrEmpty(ReturnDataXML))
            {
                ReturnDataXML += "</ReturnDataXML>";
            }


            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow dgvr in dataGridView1.Rows)
            {
                dgvr.Cells[0].Value = checkBox1.Checked;
            }
        }

        private void frmSearchData_Load(object sender, EventArgs e)
        {
            this.checkBox1.Visible = !IsOneLine;

            if (FC == null && FC2 ==null)//如果不是系统调用隐藏表身查询按钮
                btn_BodyQuery.Visible = false;
            else
            {
                if(FC !=null)
                switch (FC.FormType)
                {
                    case "单表头":
                        btn_BodyQuery.Visible = false;
                        break;
                    case "表头表身":
                        btn_BodyQuery.Visible = true;
                        break;
                    default:
                        btn_BodyQuery.Visible = true;
                        break;
                }

                if(FC2 !=null)
                    switch (FC2.FormType)
                    {
                        case "单表头":
                            btn_BodyQuery.Visible = false;
                            break;
                        case "表头表身":
                            btn_BodyQuery.Visible = true;
                            break;
                        default:
                            btn_BodyQuery.Visible = true;
                            break;
                    }
            }
                
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (IsOneLine)
            {
                if (e.RowIndex > -1)
                {

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Cells[0].Value = false;
                    }


                    dataGridView1.Rows[e.RowIndex].Cells[0].Value = true;
                    btnOk_Click(btnOk, new EventArgs());
                }
            }
        }

        private void txtWhereKey_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==13)
            {
                btnSelect_Click(btnSelect, new EventArgs());
            }
        }

        private void btn_Advanced_Click(object sender, EventArgs e)
        {
            FrmAdvancedSearch frm;
            if (DB!=null)
             frm = new CommonForm.FrmAdvancedSearch(SeletDataSql,DB);
            else
               frm = new CommonForm.FrmAdvancedSearch(SeletDataSql);
            frm.isClose = false;
            frm.ShowDialog();
            whereStr = frm.sqlWhere;
            IsAdvanced = true;
            if (!frm.isClose)
            {
                Page = 1;
                SelectData();
            }
           
        }
        string bodySQL;
        string bodyPageName;
        /// <summary>
        /// 表身查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_BodyQuery_Click(object sender, EventArgs e)
        {
            try
            {
                var num = string.Empty;
                frmBodyQuery frm;
                if (FC != null)
                {
                    frm = new frmBodyQuery(FC);
                }
                else 
                {
                    frm = new frmBodyQuery(FC2);
                }
                frm.sqlWhere = bodySQL;
                frm.cboIndex = string.IsNullOrEmpty(num) ? -1 : Convert.ToInt32(num);
                frm.ShowDialog();
                bodySQL = frm.sqlWhere;
                bodyPageName = frm.pageName;
                if (!string.IsNullOrEmpty(frm.pageName))
                {

                    foreach (TabPage tab in ((FormDesign.FormDesignTMP)((FormDesign.FormPanel.HeadPanelClass)this.FC.FormPanels["Panel1"]).Control.FindForm()).tabBody.TabPages)
                    {
                        if (bodyPageName == tab.Text)
                        {
                            
                             num = tab.Name.Split('_')[2].Replace("TabPage", "");//获取是第几个页签

                            var tableName = string.Empty;

                            if (FC != null)
                            {
                                tableName = FC.DataSource.Tables["Table" + num].TableName;//table名称
                            }
                            else
                            {
                                tableName = FC2.DataSource.Tables["Table" + num].TableName;//table名称
                            }

                            //拼接SQL
                            var headSql = string.Empty;

                            if (FC != null)
                            {
                                headSql = FC.DataSource.Tables["Table1"].SearchSQL;
                            }
                            else
                            {
                                headSql = FC2.DataSource.Tables["Table1"].SearchSQL;
                            }

                            var newSql = string.Empty;

                            if (FC != null)
                            {
                                newSql = "select distinct tabBody.* from(" + headSql + ")tabBody  left join " + tableName + " on tabBody."
                                    + FC.DataSource.Tables["Table1"].RetrunKeys[0] + "=" + tableName + "." + FC.DataSource.Tables["Table" + num].Keys[0] + " where 1=1 " + bodySQL;
                            }
                            else
                            {
                                newSql = "select distinct tabBody.* from(" + headSql + ")tabBody  left join " + tableName + " on tabBody."
                                    + FC2.DataSource.Tables["Table1"].RetrunKeys[0] + "=" + tableName + "." + FC2.DataSource.Tables["Table" + num].Keys[0] + " where 1=1 " + bodySQL;
                            }

                            if (string.IsNullOrEmpty(frm.sqlWhere))
                            {
                                if (FC != null)
                                {
                                    SeletDataSql = FC.DataSource.Tables["Table1"].SearchSQL;
                                }
                                else
                                {
                                    SeletDataSql = FC2.DataSource.Tables["Table1"].SearchSQL;
                                }
                            }
                            else
                                SeletDataSql = newSql;
                           
                        }
                    }

                }
                else
                {
                    if (FC != null)
                    {
                        SeletDataSql = FC.DataSource.Tables["Table1"].SearchSQL;
                    }
                    else
                    {
                        SeletDataSql = FC2.DataSource.Tables["Table1"].SearchSQL;
                    }
                }
                SelectData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
