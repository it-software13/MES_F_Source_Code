using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace SJEMS_QX
{
    public partial class Frm_ButtonPermissions : Form
    {
        private int mod = 0;//标识当前是添加还是更新

        public Frm_ButtonPermissions()
        {
            InitializeComponent();
            
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, Program.WebServiceUrl, Program.Language);


            //            string sql = @"
            //SELECT
            //a.AppCode AS '模块代号',
            //a.AppName AS '模块名称',
            //'False' AS '全部权限'
            //FROM SYSAPP03M a";
            //            //DataTable dt = Program.SYSDB.GetDataTable(sql);
            //            DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());
            //            dataGridView1.DataSource = dt.DefaultView;
        }     

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.ColumnIndex >1)
            {

                if(e.ColumnIndex ==2)
                {


                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

                    if (Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                    {
                        for (int i = 3; i < 12; i++)
                        {
                            dataGridView1.Rows[e.RowIndex].Cells[i].Value = true;
                        }

                    }
                    else
                    {
                        for (int i = 3; i < 12; i++)
                        {
                            dataGridView1.Rows[e.RowIndex].Cells[i].Value = false;
                        }
                    }
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);



                    if (e.ColumnIndex > 3 && Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[3].Value = true;
                    }

                    if (e.ColumnIndex == 3 && !Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                    {
                        for (int i = 4; i < 12; i++)
                        {
                            dataGridView1.Rows[e.RowIndex].Cells[i].Value = false;
                        }
                    }

                    if (Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString())
                    && Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString())
                     && Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString())
                      && Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString())
                       && Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString())
                        && Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString())
                         && Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString())
                          && Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString())
                           && Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString())
                    )
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[2].Value = true;
                    }

                    if (!Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString())
                    && !Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString())
                     && !Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString())
                      && !Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString())
                       && !Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString())
                        && !Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString())
                         && !Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString())
                          && !Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString())
                           && !Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString())
                    )
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[2].Value = false;
                    }

                }
            }
        }

        private void btn_SelectAll_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow dr in dataGridView1.Rows)
            {
                for (int i = 2; i < 12; i++)
                {
                    dr.Cells[i].Value = true;
                }
            }
        }

        private void btn_SelectNone_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                for (int i = 2; i < 12; i++)
                {
                    dr.Cells[i].Value = false;
                }
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(txt_UserCode.Text))
            {
                msg = SJeMES_Framework.Common.UIHelper.UImsg("请填写角色！", Program.Client, Program.WebServiceUrl, Program.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg); 
                return;
            }

            string sql = "";
            sql = @"
SELECT * FROM SYSROLE01M
WHERE Role_No='" + txt_UserCode.Text + @"'
";
            DataTable dtRole= GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());
            if (dtRole.Rows.Count > 0)//角色已经存在,判断是否需要更新角色
            {
                if (dtRole.Rows[0]["Role_Name"].ToString().Trim() != textBox1.Text.Trim())
                    sql = @"
UPDATE SYSROLE01M
SET [Role_Name]=@Role_Name
WHERE Role_No=@Role_No";
            }
            else
                sql = @"
INSERT INTO SYSROLE01M
(Role_No,Role_Name)
VALUES
(@Role_No,@Role_Name)";
            Dictionary<string, string> P = new Dictionary<string, string>();
            P.Add("Role_No", txt_UserCode.Text);
            P.Add("Role_Name", textBox1.Text);

            GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.WebServiceUrl, sql, P);
            //            sql = @"
            //if not Exists(select 1 from SYSROLE02M where Role_No=@Role_No and AppCode=@AppCode)
            //INSERT  INTO SYSROLE02M
            //(Role_No,Role_Name,AppCode,[Select],[Add],[Edit],[Delete],[DoSure],[Audit],[DoWork],[Print],[Fun])
            //VALUES
            //(@Role_No,@Role_Name,@AppCode,@Select,@Add,@Edit,@Delete,@DoSure,@Audit,@DoWork,@Print,@Fun)
            //ELSE
            //UPDATE SYSROLE02M
            //SET [Select]=@Select,[Add]=@Add,[Edit]=@Edit,[Delete]=@Delete,[DoSure]=@DoSure,
            //[Audit]=@Audit,[DoWork]=@DoWork,[Print]=@Print,[Fun]=@Fun,Role_Name=@Role_Name
            //WHERE Role_No=@Role_No and AppCode=@AppCode
            //";

            //string usercode = txt_UserCode.Text.Trim();
            DataTable dt = new DataTable();
            string AppCode = string.Empty;
            //foreach (DataGridViewRow dr in dataGridView1.Rows)
            //{
            //    if (string.IsNullOrEmpty(AppCode))
            //    {
            //        AppCode = "'" + dr.Cells[0].Value.ToString() + "'";
            //    }
            //    else
            //    {
            //        AppCode += ",'" + dr.Cells[0].Value.ToString() + "'";
            //    }
            //}
            //sql = "select * from SYSROLE02M where Role_No='" + txt_UserCode.Text + "' and AppCode='" + AppCode + "'";
            //dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, P);
            string sql1 = string.Empty;
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                #region old
                //                sql = @"
                //UPDATE SYSUSER02M
                //SET [Select]=@Select,[Add]=@Add,[Edit]=@Edit,[Delete]=@Delete,[DoSure]=@DoSure,
                //[Audit]=@Audit,[DoWork]=@DoWork,[Print]=@Print,[Fun]=@Fun
                //WHERE UserCode=@UserCode and AppCode=@AppCode
                //";
                //Dictionary<string, string> P = new Dictionary<string, string>();
                //P.Clear();
                //P.Add("Role_No", txt_UserCode.Text);
                //P.Add("Role_Name", textBox1.Text);
                //P.Add("AppCode", dr.Cells[0].Value.ToString());
                //P.Add("Select", dr.Cells[3].Value.ToString());
                //P.Add("Add", dr.Cells[4].Value.ToString());
                //P.Add("Edit", dr.Cells[5].Value.ToString());
                //P.Add("Delete", dr.Cells[6].Value.ToString());
                //P.Add("DoSure", dr.Cells[7].Value.ToString());
                //P.Add("Audit", dr.Cells[8].Value.ToString());
                //P.Add("DoWork", dr.Cells[9].Value.ToString());
                //P.Add("Print", dr.Cells[10].Value.ToString());
                //P.Add("Fun", dr.Cells[11].Value.ToString());
                //Program.SYSDB.ExecuteNonQueryOffline(sql, P);

                //GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.WebServiceUrl, sql, P);
                #endregion
                #region OLD
                //                if (dt.Rows.Count==0)
                //                {
                //                    sql1 += @"INSERT INTO SYSROLE02M
                //(Role_No, Role_Name, AppCode,[Select],[Add],[Edit],[Delete],[DoSure],[Audit],[DoWork],[Print],[Fun])
                //VALUES
                //('"+ txt_UserCode.Text + "', '"+ textBox1.Text + "', '"+ dr.Cells[0].Value.ToString() + "', '"+ dr.Cells[3].Value.ToString() + "'," +
                //" '"+ dr.Cells[4].Value.ToString() + "', '"+ dr.Cells[5].Value.ToString() + "', '"+ dr.Cells[6].Value.ToString() + "', " +
                //"'"+ dr.Cells[7].Value.ToString() + "', '"+ dr.Cells[8].Value.ToString() + "', '"+ dr.Cells[9].Value.ToString() + "', " +
                //"'"+ dr.Cells[10].Value.ToString() + "', '"+ dr.Cells[11].Value.ToString() + "')";
                //                }
                //                else
                //                {
                //                    sql1 += @"UPDATE SYSROLE02M SET [Select]='"+ dr.Cells[3].Value.ToString() + @"',[Add]='"+ dr.Cells[4].Value.ToString() + @"',
                //[Edit]='"+ dr.Cells[5].Value.ToString() + @"',[Delete]='"+ dr.Cells[6].Value.ToString() + @"',[DoSure]='"+ dr.Cells[7].Value.ToString() + @"',
                //[Audit]='"+ dr.Cells[8].Value.ToString() + @"',[DoWork]='"+ dr.Cells[9].Value.ToString() + @"',[Print]='"+ dr.Cells[10].Value.ToString() + @"',
                //[Fun]='"+ dr.Cells[11].Value.ToString() + @"',Role_Name='"+ textBox1.Text + @"'
                //WHERE Role_No='"+ txt_UserCode.Text + @"' and AppCode='"+ dr.Cells[0].Value.ToString() + @"'";
                //                }
                #endregion
                sql1 += @"
                if not Exists(select 1 from SYSROLE02M where Role_No='" + txt_UserCode.Text + @"' and AppCode='" + textBox1.Text + @"')
                INSERT  INTO SYSROLE02M
                (Role_No,Role_Name,AppCode,[Select],[Add],Edit,[Delete],DoSure,Audit,DoWork,[Print],Fun)
                VALUES
                ( '" + txt_UserCode.Text + @"','" + textBox1.Text + @"','" + dr.Cells[0].Value.ToString() + @"','" + dr.Cells[3].Value.ToString() + @"',
'" + dr.Cells[4].Value.ToString() + @"','" + dr.Cells[5].Value.ToString() + @"','" + dr.Cells[6].Value.ToString() + @"','" + dr.Cells[7].Value.ToString() + @"','" + dr.Cells[8].Value.ToString() + @"',
'" + dr.Cells[9].Value.ToString() + @"','" + dr.Cells[10].Value.ToString() + @"','" + dr.Cells[11].Value.ToString() + @"')
                ELSE
                UPDATE SYSROLE02M
                SET [Select]='"+ dr.Cells[3].Value.ToString() + @"',[Add]='" + dr.Cells[4].Value.ToString() + @"',
[Edit]='" + dr.Cells[5].Value.ToString() + @"',[Delete]='" + dr.Cells[6].Value.ToString() + @"',[DoSure]='" + dr.Cells[7].Value.ToString() + @"',
                [Audit]='" + dr.Cells[8].Value.ToString() + @"',[DoWork]='" + dr.Cells[9].Value.ToString() + @"',
[Print]='" + dr.Cells[10].Value.ToString() + @"',[Fun]='" + dr.Cells[11].Value.ToString() + @"',Role_Name='" + textBox1.Text + @"'
                WHERE Role_No='"+ txt_UserCode.Text + @"' and AppCode='"+ dr.Cells[0].Value.ToString() + @"'
                ";
               
            }

            GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.WebServiceUrl, sql1, P);


            msg = SJeMES_Framework.Common.UIHelper.UImsg("保存成功！", Program.Client, Program.WebServiceUrl, Program.Language);
            SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
            
        }

        private void txt_UserCode_Click(object sender, EventArgs e)
        {
            //当前窗体名称+"_"+当前方法名称
            string FrmMenthName = this.Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name;
            string sql = "SELECT Role_No AS '角色代号',Role_Name AS '角色名称' FROM SYSROLE01M";
            GDSJ_Framework.WinForm.CommonForm.frmSearchData frm = new GDSJ_Framework.WinForm.CommonForm.frmSearchData(FrmMenthName, Program.Client, Program.WebServiceUrl,
                     sql, Program.Language, true, true);

            //GDSJ_Framework.WinForm.CommonForm.frmSearchData frm = new GDSJ_Framework.WinForm.CommonForm.frmSearchData(Program.WebServiceUrl, "SELECT Role_No AS '角色代号',Role_Name AS '角色名称' FROM SYSROLE01M", true, true);
            frm.ShowDialog();

            if (!string.IsNullOrEmpty(frm.ReturnDataXML))
            {
                string nodes = "";
                ///多账号同时设置
                ////定义DataTable结构   
                //DataTable dt = new DataTable();
                //dt.Columns.Add(new DataColumn("name", typeof(string)));
                //dt.Columns.Add(new DataColumn("value", typeof(int)));

                ////加载XML数据，也可XElement.Load("文件名")
                //var xdoc = XElement.Parse(frm.ReturnDataXML);
                //var m = (from mod in xdoc.Elements("Row")
                //         select new
                //         {
                //             //给DataTabel添加数据行
                //             a = dt.Rows.Add(new[] { mod.Element("行号").Value, mod.Element("账号").Value })
                //         }).ToList();
                ////DataTable dt = GDSJ_Framework.Common.StringHelper.GetDataTableFromXML(frm.ReturnDataXML);
                //
                //for (int i=0;i<dt.Rows.Count;i++)
                //{
                //    nodes += dt.Rows[i]["value"].ToString()+",";
                //}
                ///多账号同时设置
                txt_UserCode.Text = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<角色代号>", "</角色代号>");
                textBox1.Text = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<角色名称>", "</角色名称>");
            }
            GetData();
            //Getparent();
        }

        private void txt_UserCode_TextChanged(object sender, EventArgs e)
        {
        }


        public void Getparent()
        {
            //dataGridView1.DataSource = null;
            //dataGridView1.Columns.Clear();

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //            string sql = @"
            //SELECT DISTINCT
            //a.AppCode AS '模块代号',
            //a.AppName AS '模块名称',
            //'False' AS '全部权限',
            //ISNULL([Select],'False') AS '查看数据',
            //ISNULL([Add],'False') AS '添加数据',
            //ISNULL([Edit],'False') AS '修改数据',
            //ISNULL([Delete],'False') AS '删除数据',
            //ISNULL(DoSure ,'False') AS '确认操作',
            //ISNULL(Audit ,'False') AS '审核操作',
            //ISNULL(DoWork ,'False') AS '其他操作',
            //ISNULL([Print] ,'False') AS '打印',
            //ISNULL(Fun ,'False') AS '更多功能',
            //FROM SYSAPP03M a
            //LEFT JOIN SYSROLE02M b ON a.AppCode = b.AppCode WHERE 1=1 ";
            string sql = @"SELECT AppCode AS '模块代号',
                AppName AS '模块名称',
                'True' AS '全部权限',
                'True' AS '查看数据',
                'True' AS '添加数据',
                'True' AS '修改数据',
                'True' AS '删除数据',
                'True' AS '确认操作',
                'True' AS '审核操作',
                'True' AS '其他操作',
                'True' AS '打印',
                'True' AS '更多功能'
                 FROM SYSAPP03M WHERE 1=1 ";
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                sql += " AND AppCode in (" + textBox2.Text + ")";
            }
            DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());
            if (dt.Rows.Count>0)
            {
                #region old
                //DataTable TempDT = dt.Clone();
                //DataRow[] Rows = dt.Select("Role_No ='" + txt_UserCode.Text + "'");
                //foreach (DataRow DR in Rows)
                //{
                //    TempDT.ImportRow(DR);
                //}
                //                if (TempDT.Rows.Count > 0) dataGridView1.DataSource = TempDT.DefaultView;
                //                else
                //                {
                //                    sql = @"SELECT AppCode AS '模块代号',
                //AppName AS '模块名称',
                //'True' AS '全部权限',
                //'True' AS '查看数据',
                //'True' AS '添加数据',
                //'True' AS '修改数据',
                //'True' AS '删除数据',
                //'True' AS '确认操作',
                //'True' AS '审核操作',
                //'True' AS '其他操作',
                //'True' AS '打印',
                //'True' AS '更多功能'
                // FROM SYSAPP03M WHERE 1=1 ";
                //                    if (!string.IsNullOrEmpty(textBox2.Text))
                //                    {
                //                        sql += " AND AppCode in (" + textBox2.Text + ")";
                //                    }
                //                    dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());
                #endregion
                dataGridView1.DataSource = dt.DefaultView;
                #region
                //foreach (DataGridViewRow dr in dataGridView1.Rows)
                //{
                //    //if (TempDT.Rows.Count>0)
                //    //{
                //    //    for (int i = 0; i < TempDT.Rows.Count; i++)
                //    //    {
                //    //        if (dr.Cells[0].Value.ToString()== TempDT.Rows[i][0].ToString())
                //    //        {
                //    //            dr.Cells[3].Value = TempDT.Rows[i][3].ToString();
                //    //            dr.Cells[4].Value = TempDT.Rows[i][4].ToString();
                //    //            dr.Cells[5].Value = TempDT.Rows[i][5].ToString();
                //    //            dr.Cells[6].Value = TempDT.Rows[i][6].ToString();
                //    //            dr.Cells[7].Value = TempDT.Rows[i][7].ToString();
                //    //            dr.Cells[8].Value = TempDT.Rows[i][8].ToString();
                //    //            dr.Cells[9].Value = TempDT.Rows[i][9].ToString();
                //    //            dr.Cells[10].Value = TempDT.Rows[i][10].ToString();
                //    //            dr.Cells[11].Value = TempDT.Rows[i][11].ToString();
                //    //        }
                //    //    }
                //    //}
                //    //if (Convert.ToBoolean(dr.Cells[3].Value.ToString())
                //    //    && Convert.ToBoolean(dr.Cells[4].Value.ToString())
                //    //     && Convert.ToBoolean(dr.Cells[5].Value.ToString())
                //    //      && Convert.ToBoolean(dr.Cells[6].Value.ToString())
                //    //       && Convert.ToBoolean(dr.Cells[7].Value.ToString())
                //    //        && Convert.ToBoolean(dr.Cells[8].Value.ToString())
                //    //         && Convert.ToBoolean(dr.Cells[9].Value.ToString())
                //    //          && Convert.ToBoolean(dr.Cells[10].Value.ToString())
                //    //           && Convert.ToBoolean(dr.Cells[11].Value.ToString())
                //    //    )
                //    //{
                //    //    dr.Cells[2].Value = true;
                //    //}

                //}
                #endregion
            }
          
        }

        public void GetData()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            string sql = @"
SELECT
a.AppCode AS '模块代号',
a.AppName AS '模块名称',
'False' AS '全部权限',
ISNULL([Select],'False') AS '查看数据',
ISNULL([Add],'False') AS '添加数据',
ISNULL([Edit],'False') AS '修改数据',
ISNULL([Delete],'False') AS '删除数据',
ISNULL(DoSure ,'False') AS '确认操作',
ISNULL(Audit ,'False') AS '审核操作',
ISNULL(DoWork ,'False') AS '其他操作',
ISNULL([Print] ,'False') AS '打印',
ISNULL(Fun ,'False') AS '更多功能',
b.Role_No
FROM SYSAPP03M a
LEFT JOIN SYSROLE02M b ON a.AppCode = b.AppCode WHERE 1=1 ";
            if (!string.IsNullOrEmpty(txt_UserCode.Text))
            {
                sql += " AND b.Role_No ='" + txt_UserCode.Text + "'";
            }
            DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());
            if (dt.Rows.Count>0)
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                sql = @"SELECT AppCode AS '模块代号',
                AppName AS '模块名称',
                'True' AS '全部权限',
                'True' AS '查看数据',
                'True' AS '添加数据',
                'True' AS '修改数据',
                'True' AS '删除数据',
                'True' AS '确认操作',
                'True' AS '审核操作',
                'True' AS '其他操作',
                'True' AS '打印',
                'True' AS '更多功能'
                 FROM SYSAPP03M WHERE 1=1 ";
                dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());
                dataGridView1.DataSource = dt;
            }
            
        }

        private void frmAddUser_Load(object sender, EventArgs e)
        {
            //MultiLanguage.SetDefaultLanguage("en-US");
            ////对所有打开的窗口重新加载语言  
            //foreach (Form form in Application.OpenForms)
            //{
            //    LoadAll(form);
            //}

        }

        private void txt_UserCode_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            //当前窗体名称+"_"+当前方法名称
            string FrmMenthName = this.Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name;
            
            if (!string.IsNullOrEmpty(txt_UserCode.Text))
            {
                string sql = "select distinct AppCode as '模块代号',AppName as '模块名称' from SYSAPP03M";
                GDSJ_Framework.WinForm.CommonForm.frmSearchData frm = new GDSJ_Framework.WinForm.CommonForm.frmSearchData(FrmMenthName, Program.Client, Program.WebServiceUrl,
                         sql, Program.Language, false, true);

                //GDSJ_Framework.WinForm.CommonForm.frmSearchData frm = new GDSJ_Framework.WinForm.CommonForm.frmSearchData(Program.WebServiceUrl, "select distinct AppCode as '模块代号',AppName as '模块名称' from SYSAPP03M", false, true);
                frm.ShowDialog();
                string nodes = string.Empty;
                string node = string.Empty;
                if (!string.IsNullOrEmpty(frm.ReturnDataXML))
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("name", typeof(string)));
                    dt.Columns.Add(new DataColumn("value", typeof(string)));
                    dt.Columns.Add(new DataColumn("value2", typeof(string)));
                    var xdoc = XElement.Parse(frm.ReturnDataXML);
                    var m = (from mod in xdoc.Elements("Row")
                             select new
                             {
                                 a = dt.Rows.Add(new[] { mod.Element("行号").Value, mod.Element("模块代号").Value, mod.Element("模块名称").Value })
                             }).ToList();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (string.IsNullOrEmpty(nodes))
                        {
                            nodes = "'" + dt.Rows[i]["value"].ToString() + "'";
                        }
                        else
                        {
                            nodes += ",'" + dt.Rows[i]["value"].ToString() + "'";
                        }
                        if (string.IsNullOrEmpty(node))
                        {
                            node = "'" + dt.Rows[i]["value2"].ToString() + "'";
                        }
                        else
                        {
                            node += "'" + dt.Rows[i]["value2"].ToString() + "'";
                        }

                    }
                    textBox2.Text = nodes;
                    textBox3.Text = node;
                    //textBox2.Text = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<模块代号>", "</模块代号>");
                    //textBox3.Text = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<模块名称>", "</模块名称>");
                    Getparent();
                }
                
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("请先选择角色！", Program.Client, Program.WebServiceUrl, Program.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg); 
            }
  
        }

     
    }
}
