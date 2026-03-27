using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SJeMESSystemTools
{
    public partial class FormEditHead : Form
    {
        public SJeMES_Framework.Web.JSONPanelClassH H=new SJeMES_Framework.Web.JSONPanelClassH();
        public string AppCpde = "";
        public string AppName = "";
        public string Key = "";
        public string App_JsonHList = "";

        public DataTable ChildrensTable;
        public ComboBox CB;
        public DataTable dtMore;

        public bool IsSave = false;
        public int IsAdd = 0;

        public FormEditHead(SJeMES_Framework.Web.JSONPanelClassH H)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            this.H = H;

            GetTableNames();
            IsAdd = 1;

            if (H != null)
            {
                IsAdd = 0;

                LoadXMLData();
            }
            else
            {
                label2.Visible = true;
                label3.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;

            }
            dataGridView1.CurrentCellChanged += DataGridView1_CurrentCellChanged;
            dataGridView1.DoubleClick += DataGridView1_DoubleClick;
            dataGridView1.CellLeave += DataGridView1_CellLeave;
            dataGridView1.Scroll += DataGridView1_Scroll;
            dataGridView1.UserDeletedRow += DataGridView1_UserDeletedRow;
        }

        private void DataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            for(int i=0;i<dataGridView1.Rows.Count-1;i++)
            {
                dataGridView1.Rows[i].Cells["Seq"].Value = (i+1);

            }
        }

        private void DataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            if(CB!=null && CB.Visible)
            {
                CB.Visible = false;
            }
        }

        private void DataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    if (CB != null && CB.Visible)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = CB.Text;
                    }

                    if (string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells["Seq"].Value.ToString()))
                    {
                        int max = 0;
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            if(string.IsNullOrEmpty(dataGridView1.Rows[i].Cells["Seq"].Value.ToString()))
                            {
                                break;
                            }
                            if (max < Convert.ToInt16(dataGridView1.Rows[i].Cells["Seq"].Value.ToString()))
                            {
                                max = Convert.ToInt16(dataGridView1.Rows[i].Cells["Seq"].Value.ToString());
                            }
                        }

                        max++;

                        dataGridView1.Rows[e.RowIndex].Cells["Seq"].Value = max;
                    }
                }


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void DataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.ColumnIndex > -1 && dataGridView1.CurrentCell.RowIndex > -1)
            {
                DataGridViewColumn dgvc = dataGridView1.CurrentCell.OwningColumn;
                if (dgvc.Name == "DataKey")
                {
                    if (string.IsNullOrEmpty(cmb_TableName.Text))
                    {
                        MessageBox.Show("请先选择数据表");
                        return;
                    }
                    //                    string sql = @"
                    //Select name as '字段名' from syscolumns Where ID=OBJECT_ID('" + cmb_TableName.Text + @"')
                    //";
                    string sql = string.Empty;
                    if (Program.Client.Org.DBType.ToLower() == "sqlserver")
                    {
                        sql = @"
Select name as '字段名' from syscolumns Where ID=OBJECT_ID('" + cmb_TableName.Text + @"')
";
                    }
                    if (Program.Client.Org.DBType.ToLower() == "oracle")
                    {
                        sql = @"
select column_name as 字段名 from user_tab_columns where table_name= upper('" + cmb_TableName.Text + "')";
                    }
                    if (Program.Client.Org.DBType.ToLower() == "mysql")
                    {
                        sql = @"
SELECT DISTINCT COL.COLUMN_NAME as 字段名  
FROM INFORMATION_SCHEMA.COLUMNS COL 
Where  COL.TABLE_NAME='" + cmb_TableName.Text + "' ";
                    }
                    SJeMES_Control_Library.Forms.FrmSelectData frm = new SJeMES_Control_Library.Forms.FrmSelectData(sql, true, Program.Client);
                    frm.ShowDialog();
                    if (frm.RetData!=null)
                    {
                        if (frm.RetData.Rows.Count > 0)
                        {
                            dataGridView1.CurrentCell.Value = frm.RetData.Rows[0][1].ToString();

                        }
                    }
                }
            }
        }

        private void DataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (CB != null)
            {
                CB.Visible = false;
            }
            if (dataGridView1.CurrentCell !=null && dataGridView1.CurrentCell.ColumnIndex > -1 && dataGridView1.CurrentCell.RowIndex > -1)
            {
                DataGridViewColumn dgvc = dataGridView1.CurrentCell.OwningColumn;
                if (dgvc.Name == "Edit" || dgvc.Name == "Add" || dgvc.Name == "Enable" || dgvc.Name=="IsNull")
                {
                    CB = new ComboBox();
                    CB.Enabled = true;
                    CB.DropDownStyle = ComboBoxStyle.DropDownList;

                    //ComBox.Font = new System.Drawing.Font("黑体", 9);
                    //ComBox.ForeColor = System.Drawing.Color.Black;
                    string Value = dataGridView1.CurrentCell.Value.ToString();


                    CB.Items.Add("True");
                    CB.Items.Add("False");

                    if (!string.IsNullOrEmpty(Value))
                    {
                        CB.Text = Value;
                    }
                    else
                    {
                        CB.SelectedIndex = 0;
                    }

                    CB.SelectedIndexChanged += CB_SelectedIndexChanged;

                    dataGridView1.Controls.Add(CB);

                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);

                    CB.Left = rect.Left;
                    CB.Top = rect.Top;
                    CB.Width = rect.Width;
                    CB.Height = rect.Height;
                    CB.Visible = true;
                }
                else if (dgvc.Name == "DataType" ||dgvc.Name == "DefaultValueType")
                {
                    CB = new ComboBox();
                    CB.Enabled = true;
                    CB.DropDownStyle = ComboBoxStyle.DropDownList;

                    //ComBox.Font = new System.Drawing.Font("黑体", 9);
                    //ComBox.ForeColor = System.Drawing.Color.Black;
                    string Value = dataGridView1.CurrentCell.Value.ToString();


                    CB.Items.Add("String");
                    CB.Items.Add("Int");
                    CB.Items.Add("Float");
                    CB.Items.Add("Bool");
                    CB.Items.Add("Enum");
                    CB.Items.Add("DataSource");
                    CB.Items.Add("OtherData");
                    CB.Items.Add("Date");
                    CB.Items.Add("DateTime");
                    CB.Items.Add("Time");

                    if (!string.IsNullOrEmpty(Value))
                    {
                        CB.Text = Value;
                    }
                    else
                    {
                        CB.SelectedIndex = 0;
                    }

                    CB.SelectedIndexChanged += CB_SelectedIndexChanged;

                    dataGridView1.Controls.Add(CB);

                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);

                    CB.Left = rect.Left;
                    CB.Top = rect.Top;
                    CB.Width = rect.Width;
                    CB.Height = rect.Height;
                    CB.Visible = true;
                }
            }
        }

        private void CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = CB.Text;
            CB.Visible = false;
            CB.Dispose();
        }

        private void GetTableNames()
        {
            try
            {
                #region old
                //                string sql = @"
                //select name from sys.tables 
                //";
                //                DataTable dt =
                //                    ModuleSettingHelper.GetDataTable(sql,string.Empty, "ORDER BY name asc",1,10000);
                //                List<string> sList = new List<string>();

                //                foreach (DataRow drr in dt.Rows)
                //                {
                //                    sList.Add(drr["name"].ToString());
                //                }
                //                DataRow dr_new = dt.NewRow();
                //                dt.Rows.InsertAt(dr_new, 0);
                //                cmb_TableName.AutoCompleteCustomSource.AddRange(sList.ToArray());
                //                cmb_TableName.AutoCompleteSource = AutoCompleteSource.ListItems;
                //                cmb_TableName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //                cmb_TableName.DataSource = dt;
                //                cmb_TableName.DisplayMember = "name";
                #endregion
                Dictionary<string, object> p = new Dictionary<string, object>();
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                                        "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "GetTable", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {

                    string json = j["RetData"].ToString();

                    DataTable dt= Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json.ToString());
                    List<string> sList = new List<string>();
                    foreach (DataRow drr in dt.Rows)
                    {
                        sList.Add(drr["name"].ToString());
                    }
                    DataRow dr_new = dt.NewRow();
                    dt.Rows.InsertAt(dr_new, 0);
                    cmb_TableName.AutoCompleteCustomSource.AddRange(sList.ToArray());
                    cmb_TableName.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cmb_TableName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmb_TableName.DataSource = dt;
                    cmb_TableName.DisplayMember = "name";
                }
                else
                {
                    throw new Exception(j["ErrMsg"].ToString());
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadXMLData()
        {
            try
            {


                cmb_TableName.Text = H.table;
                
                if (cmb_TableName.SelectedIndex == 0)
                {
                    cmb_TableName.SelectedIndex = 1;
                }
                foreach (string s in H.tableKeys)
                {
                    if (string.IsNullOrEmpty(txt_Keys.Text))
                    {
                        txt_Keys.Text += s;
                    }
                    else
                    {
                        txt_Keys.Text += "," + s;
                    }
                }

                

                ChildrensTable = new DataTable();
                ChildrensTable.Columns.Add("Seq");
                ChildrensTable.Columns.Add("Title");
                ChildrensTable.Columns.Add("DataType");
                ChildrensTable.Columns.Add("DataKey");
                ChildrensTable.Columns.Add("Add");
                ChildrensTable.Columns.Add("Edit");
                ChildrensTable.Columns.Add("IsNull");
                ChildrensTable.Columns.Add("DataSelectSQL");
                ChildrensTable.Columns.Add("DataEnum");
               

                int i = 1;
                foreach(SJeMES_Framework.Web.JSONControlH control in H.formData)
                {

                    DataRow dr = ChildrensTable.NewRow();
                    dr["Seq"] = i;
                    dr["Title"] = control.Item.label;


                    dr["DataType"] = SJeMES_Framework.Web.JSONFormClass.GetDataType(control);
                    dr["DataKey"] = control.name;
                    dr["Add"] = control.control.IsAdd;
                    dr["Edit"] = control.control.IsEdit;

                    dr["DataSelectSQL"] = control.otherData.sql;

                    dr["DataEnum"] = SJeMES_Framework.Web.JSONFormClass.GetEnumData(control);


                    dr["IsNull"] = SJeMES_Framework.Web.JSONFormClass.GetIsNULL(control);

                    ChildrensTable.Rows.Add(dr);
                    i++;

                }

                dataGridView1.DataSource = ChildrensTable.DefaultView;
                dataGridView1.Columns["Seq"].ReadOnly = false;
                dataGridView1.Columns["DataKey"].ReadOnly = true;
                dataGridView1.Columns["DataKey"].DefaultCellStyle.BackColor = Color.LightYellow;



            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataControl_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                TextBox tb = sender as TextBox;
                string sql = string.Empty;
                DataTable dt = new DataTable();
                DataTable dt2;
                SJeMES_Control_Library.Forms.FrmSelectData frm2;
                switch (tb.Name)
                {
                    case "txt_Keys":
                        if(string.IsNullOrEmpty(cmb_TableName.Text))
                        {
                            MessageBox.Show("请先选择数据表");
                            return;
                        }
                        if (Program.Client.Org.DBType.ToLower() == "sqlserver")
                        {
                            sql = @"
Select name as '字段名' from syscolumns Where ID=OBJECT_ID('" + cmb_TableName.Text + @"')
";
                        }
                        if (Program.Client.Org.DBType.ToLower() == "oracle")
                        {
                            sql = @"
select column_name as 字段名 from user_tab_columns where table_name= upper('"+ cmb_TableName.Text + "')";
                        }
                        if (Program.Client.Org.DBType.ToLower() == "mysql")
                        {
                            sql = @"
SELECT DISTINCT COL.COLUMN_NAME as 字段名  
FROM INFORMATION_SCHEMA.COLUMNS COL 
Where  COL.TABLE_NAME='"+ cmb_TableName.Text + "' ";
                        }
                        SJeMES_Control_Library.Forms.FrmSelectData frm = new SJeMES_Control_Library.Forms.FrmSelectData( sql, false, Program.Client);
                        frm.ShowDialog();
                        if (frm.RetData!=null)
                        {
                            if (frm.RetData.Rows.Count > 0)
                            {

                                txt_Keys.Text = string.Empty;
                                foreach (DataRow dr in frm.RetData.Rows)
                                {
                                    txt_Keys.Text += dr[1].ToString() + ",";
                                }

                                txt_Keys.Text = txt_Keys.Text.Remove(txt_Keys.Text.Length - 1);
                            }
                        }
                        break;
                    
                    default:
                        return;
                }


            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(IsAdd==1)
                {
                    if (string.IsNullOrEmpty(textBox1.Text.Trim()))
                    {
                        MessageBox.Show("请填写名称"); 
                        return;
                    }
                    if (string.IsNullOrEmpty(textBox2.Text.Trim()))
                    {
                        MessageBox.Show("请填写编码");
                        return;
                    }
                    if (string.IsNullOrEmpty(txt_Keys.Text.Trim()))
                    {
                        MessageBox.Show("请选择主键");
                        return;
                    }
                    AppCpde = textBox2.Text.Trim();
                    AppName = textBox1.Text.Trim();
                    Key = txt_Keys.Text.Trim();
                }
                DataTable dt = Program.Client.SYSGetDataTable("SELECT [APP_Code] FROM [SJEMSSYS].[dbo].[SYSAPP01M] where [APP_Code]='"+ textBox2.Text.Trim() + "'");
                if (dt!=null && dt.Rows.Count>0)
                {
                    MessageBox.Show(textBox2.Text.Trim() +"编码已存在！");
                    return;
                }
                int k = 0;
                foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                {
                    if (dgvr.Cells["Seq"].Value == null)
                    {
                        k++;
                    }
                }


                for (int i = 1; i <= dataGridView1.Rows.Count - k; i++)
                {
                    bool ishas = false;
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        if (dgvr.Cells["Seq"].Value != null)
                        {
                            if (dgvr.Cells["Seq"].Value.ToString() == i.ToString())
                            {
                                ishas = true;
                            }
                        }
                    }

                    if (!ishas)
                    {
                        MessageBox.Show("缺少顺序" + i + "的配置");
                        return;
                    }
                }

                UpdatePanelH();
                this.IsSave = true;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void UpdatePanelH()
        {
            try
            {
                if (H == null)
                {
                    H = new SJeMES_Framework.Web.JSONPanelClassH();
                }
                H.table = cmb_TableName.Text;

                H.tableKeys = new List<string>();

                if (!string.IsNullOrEmpty(txt_Keys.Text.Trim()))
                {
                    string[] Keys = txt_Keys.Text.Split(',');
                    foreach (string s in Keys)
                    {
                        H.tableKeys.Add(s);
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "关键字段必须填写");
                    return;
                }
                H.formData = new List<SJeMES_Framework.Web.JSONControlH>();
                List<SJeMES_Framework.Web.JSONPanelClassHListItem> itemlist = new List<SJeMES_Framework.Web.JSONPanelClassHListItem>();
                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    try
                    {
                        if (dr.Cells["Title"].Value.ToString() != null && !string.IsNullOrEmpty(dr.Cells["Title"].Value.ToString()))
                        {
                            SJeMES_Framework.Web.JSONControlH control = new SJeMES_Framework.Web.JSONControlH();
                            SJeMES_Framework.Web.JSONPanelClassHListItem item = new SJeMES_Framework.Web.JSONPanelClassHListItem();
                            control.Item.label = dr.Cells["Title"].Value.ToString();
                            control.name = dr.Cells["DataKey"].Value.ToString();
                            control.control.IsAdd = Convert.ToBoolean(dr.Cells["Add"].Value);
                            control.control.IsEdit = Convert.ToBoolean(dr.Cells["Edit"].Value);

                            control.otherData.sql = dr.Cells["DataSelectSQL"].Value.ToString();

                            control.enumData = new List<SJeMES_Framework.Web.JSONEnum>();
                            if (!string.IsNullOrEmpty(dr.Cells["DataEnum"].Value.ToString()))
                            {
                                //string[] keys = dr.Cells["DataEnum"].Value.ToString().Split(new char[] { '@', ';' }, StringSplitOptions.RemoveEmptyEntries);
                                try
                                {
                                    string[] keys = dr.Cells["DataEnum"].Value.ToString().Split(';');
                                    foreach (string s in keys)
                                    {
                                        //SJeMES_Framework.Web.JSONEnum e = new SJeMES_Framework.Web.JSONEnum(
                                        //    s.Split(new char[] { '@', ':' }, StringSplitOptions.RemoveEmptyEntries)[0],
                                        //    s.Split(new char[] { '@', ':' }, StringSplitOptions.RemoveEmptyEntries)[1]);
                                        SJeMES_Framework.Web.JSONEnum e = new SJeMES_Framework.Web.JSONEnum(
                                           s.Split(new char[] { '@', ':' }, StringSplitOptions.RemoveEmptyEntries)[0],
                                           s.Split(new char[] { '@', ':' }, StringSplitOptions.RemoveEmptyEntries)[1]);
                                        control.enumData.Add(e);
                                    }
                                }
                                catch
                                {}
                               
                            }



                            control = SJeMES_Framework.Web.JSONFormClass.GetHTypeAndRules(control, dr.Cells["DataType"].Value.ToString());

                            if (Convert.ToBoolean(dr.Cells["IsNull"].Value))
                            {
                                control.rules.Add(new SJeMES_Framework.Web.JSONControlHRules("required", "[" + control.Item.label + "]数据不能为空"));
                            }

                            H.formData.Add(control);

                            #region  App_JsonHList
                            item.enumData= new List<SJeMES_Framework.Web.JSONEnum>();
                            item.label = dr.Cells["Title"].Value.ToString();
                            item.prop= dr.Cells["DataKey"].Value.ToString();
                            //item.width = "200";
                            itemlist.Add(item);
                            #endregion
                        }
                    }
                    catch {}
                }
                #region  App_JsonHList
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("tablename", cmb_TableName.Text);
                p.Add("tableHead", itemlist);
                App_JsonHList = Newtonsoft.Json.JsonConvert.SerializeObject(p);
                #endregion
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void cmb_TableName_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cmb_TableName_TextChanged(object sender, EventArgs e)
        {
            if (IsAdd == 1 && !string.IsNullOrEmpty(cmb_TableName.Text))
            {
                txt_Keys.Text = "";

                ChildrensTable = new DataTable();
                ChildrensTable.Columns.Add("Seq");
                ChildrensTable.Columns.Add("Title");
                ChildrensTable.Columns.Add("DataType");
                ChildrensTable.Columns.Add("DataKey");
                ChildrensTable.Columns.Add("Add");
                ChildrensTable.Columns.Add("Edit");
                ChildrensTable.Columns.Add("IsNull");
                ChildrensTable.Columns.Add("DataSelectSQL");
                ChildrensTable.Columns.Add("DataEnum");
                dataGridView1.DataSource = ChildrensTable.DefaultView;
                dataGridView1.Columns["Seq"].ReadOnly = false;
                dataGridView1.Columns["DataKey"].ReadOnly = true;
                dataGridView1.Columns["DataKey"].DefaultCellStyle.BackColor = Color.LightYellow;
            }
        }
        //更多功能
        private void button2_Click(object sender, EventArgs e)
        {
            FrmMoreButton frm = new FrmMoreButton();
            frm.ShowDialog();
            dtMore = frm.dt;
        }
    }
}
