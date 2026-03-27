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
    public partial class FormEditBody : Form
    {
        public SJeMES_Framework.Web.JSONPanelClassB B=new SJeMES_Framework.Web.JSONPanelClassB();

        public DataTable ChildrensTable;
        public ComboBox CB;

        public bool IsSave = false;
        public List<string> HeadKeys=new List<string>();

        public int IsAdd = 0;//判断是否添加表身

        public FormEditBody(SJeMES_Framework.Web.JSONPanelClassB B)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            this.B = B;
            GetTableNames();
            IsAdd = 1;

            if (B.table != null)
            {
                IsAdd = 0;
                LoadPanelData();
            }
            else
            {
                label2.Visible = true;
                label3.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox2.Enabled = false;
                textBox2.Text = B.seq.ToString();

            }

            dataGridView1.CurrentCellChanged += DataGridView1_CurrentCellChanged;
            dataGridView1.DoubleClick += DataGridView1_DoubleClick;
            dataGridView1.CellLeave += DataGridView1_CellLeave;
            dataGridView1.Scroll += DataGridView1_Scroll;
            dataGridView1.UserDeletedRow += DataGridView1_UserDeletedRow;
        }



        private void DataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                dataGridView1.Rows[i].Cells["Seq"].Value = (i + 1);

            }
        }

        private void DataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            if (CB != null && CB.Visible)
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
                            if (string.IsNullOrEmpty(dataGridView1.Rows[i].Cells["Seq"].Value.ToString()))
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
                    SJeMES_Control_Library.Forms.FrmSelectData frm = new SJeMES_Control_Library.Forms.FrmSelectData(sql, false, Program.Client);
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
            if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.ColumnIndex > -1 && dataGridView1.CurrentCell.RowIndex > -1)
            {
                DataGridViewColumn dgvc = dataGridView1.CurrentCell.OwningColumn;
                if (dgvc.Name == "Edit" || dgvc.Name == "Add" || dgvc.Name == "Enable" || dgvc.Name == "IsNull")
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
                else if (dgvc.Name == "DataType" || dgvc.Name == "DefaultValueType")
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

                    if (dgvc.Name == "DefaultValueType")
                    {
                        CB.Items.Add("HeadData");
                    }

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

                //                DataTable dt = ModuleSettingHelper.GetDataTable(sql, string.Empty, "ORDER BY name asc", 1, 10000);

                //                List<string> sList = new List<string>();
                //                foreach (DataRow drr in dt.Rows)
                //                {
                //                    sList.Add(drr["name"].ToString());
                //                }
                //                if (IsAdd == 1)
                //                {
                //                    DataRow dr_new = dt.NewRow();
                //                    dt.Rows.InsertAt(dr_new, 0);
                //                }
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

                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json.ToString());
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
                }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadPanelData()
        {
            try
            {
                cmb_TableName.Text = B.table;

                cmb_disabled.Text = B.disabled.ToString();

                foreach (string s in B.tableKeys)
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

                foreach (string s in B.HeadKeys)
                {
                    if (string.IsNullOrEmpty(txt_HeadKeys.Text))
                    {
                        txt_HeadKeys.Text += s;
                    }
                    else
                    {
                        txt_HeadKeys.Text += "," + s;
                    }
                }


               

                this.Name += "[" + B.Title + "]";


                ChildrensTable = new DataTable();
                ChildrensTable.Columns.Add("Seq");
                ChildrensTable.Columns.Add("Title");
                ChildrensTable.Columns.Add("DataType");
                ChildrensTable.Columns.Add("DataKey");
                ChildrensTable.Columns.Add("Add");
                ChildrensTable.Columns.Add("Edit");
                ChildrensTable.Columns.Add("Enable");
                ChildrensTable.Columns.Add("DataSelectSQL");
                ChildrensTable.Columns.Add("DataEnum");
                ChildrensTable.Columns.Add("IsNull");


                int ColumnsCount = 0;
                try
                {
                    ColumnsCount = Convert.ToInt32(B.tableHead.Count);
                }
                catch { }

                for (int i = 0; i < ColumnsCount; i++)
                {
                    SJeMES_Framework.Web.JSONControlB control = B.tableHead[i];

                    string datakey = control.prop;

                    //if(datakey =="org" ||
                    //    datakey =="createby" || datakey =="createdate" ||datakey =="createtime" ||
                    //    datakey =="modifyby" || datakey == "modifydate" || datakey =="modifytime")
                    //{
                    //    continue;
                    //}

                    DataRow dr = ChildrensTable.NewRow();
                    dr["Seq"] = i+1;
                    dr["Title"] = control.label;


                    dr["DataType"] = SJeMES_Framework.Web.JSONFormClass.GetDataType(control);
                    dr["DataKey"] = datakey;
                    dr["Add"] = control.IsAdd;
                    dr["Edit"] = control.IsEdit;

                    dr["DataSelectSQL"] = control.otherData.sql;

                    dr["DataEnum"] = SJeMES_Framework.Web.JSONFormClass.GetEnumData(control);


                    dr["IsNull"] = control.IsNull;

                    ChildrensTable.Rows.Add(dr);

                }

                dataGridView1.DataSource = ChildrensTable.DefaultView;
                dataGridView1.Columns["Seq"].ReadOnly = false;
                dataGridView1.Columns["DataKey"].ReadOnly = true;
                dataGridView1.Columns["DataKey"].DefaultCellStyle.BackColor = Color.LightYellow;



            }
            catch (Exception ex)
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
                SJeMES_Control_Library.Forms.FrmSelectData frm;
                switch (tb.Name)
                {
                    case "txt_Keys":
                        if (string.IsNullOrEmpty(cmb_TableName.Text))
                        {
                            MessageBox.Show("请先选择数据表");
                            return;
                        }
                        //                        sql = @"
                        //Select name as '字段名' from syscolumns Where ID=OBJECT_ID('" + cmb_TableName.Text + @"')
                        //";
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
                        frm = new SJeMES_Control_Library.Forms.FrmSelectData(sql, false, Program.Client);
                        frm.ShowDialog();
                        if (frm.RetData!=null)
                        {
                            if (frm.RetData.Rows.Count > 0)
                            {

                                //txt_Keys.Text = string.Empty;
                                foreach (DataRow dr in frm.RetData.Rows)
                                {
                                    if (!string.IsNullOrEmpty(txt_Keys.Text)) txt_Keys.Text += ",";
                                    txt_Keys.Text += dr[1].ToString();
                                }

                                //txt_Keys.Text = txt_Keys.Text.Remove(txt_Keys.Text.Length - 1);
                            }
                        }
                        break;

                    default:
                        return;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsAdd == 1)
                {
                    if (string.IsNullOrEmpty(textBox1.Text.Trim()))
                    {
                        MessageBox.Show("请填写表体");
                        return;
                    }
                    if (string.IsNullOrEmpty(textBox2.Text.Trim()))
                    {
                        MessageBox.Show("请填写Sep");
                        return;
                    }
                    if (string.IsNullOrEmpty(txt_Keys.Text.Trim()))
                    {
                        MessageBox.Show("请选择主键");
                        return;
                    }
                    B.Title = textBox1.Text.Trim(); 
                    B.seq =!string.IsNullOrEmpty(textBox2.Text.Trim())?int.Parse(textBox2.Text.Trim()) :0;
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

                UpdatePanelB();
                this.IsSave = true;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void UpdatePanelB()
        {
            try
            {
                B.table = cmb_TableName.Text;



                if (!string.IsNullOrEmpty(txt_Keys.Text.Trim()))
                {
                    B.tableKeys = new List<string>();
                    string[] Keys = txt_Keys.Text.Split(',');
                    foreach (string s in Keys)
                    {
                        B.tableKeys.Add(s);
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "关键字段必须填写");
                    return;
                }

                B.disabled = Convert.ToBoolean(cmb_disabled.Text);

                if (!string.IsNullOrEmpty(txt_HeadKeys.Text.Trim()))
                {
                    B.HeadKeys = new List<string>();
                    string[] Keys = txt_HeadKeys.Text.Split(',');
                    foreach (string s in Keys)
                    {
                        B.HeadKeys.Add(s);
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "关键字段必须填写");
                    return;
                }

                B.tableHead = new List<SJeMES_Framework.Web.JSONControlB>();

                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(dr.Cells["Title"].Value.ToString()))
                        {
                            SJeMES_Framework.Web.JSONControlB control = new SJeMES_Framework.Web.JSONControlB();
                            control.label = dr.Cells["Title"].Value.ToString();



                            control.prop = dr.Cells["DataKey"].Value.ToString();
                            control.IsAdd = Convert.ToBoolean(dr.Cells["Add"].Value);
                            control.IsEdit = Convert.ToBoolean(dr.Cells["Edit"].Value);

                            control.otherData.sql = dr.Cells["DataSelectSQL"].Value.ToString();

                            control.enumData = new List<SJeMES_Framework.Web.JSONEnum>();
                            control.options = new List<SJeMES_Framework.Web.JSONControlBOption>();
                            if (!string.IsNullOrEmpty(dr.Cells["DataEnum"].Value.ToString()))
                            {
                                //string[] keys = dr.Cells["DataEnum"].Value.ToString().Split(new char[] { '@', ';' }, StringSplitOptions.RemoveEmptyEntries);
                                try
                                {
                                    string[] keys = dr.Cells["DataEnum"].Value.ToString().Split(';');
                                    foreach (string s in keys)
                                    {
                                        SJeMES_Framework.Web.JSONEnum e = new SJeMES_Framework.Web.JSONEnum(
                                            s.Split(new char[] { '@', ':' }, StringSplitOptions.RemoveEmptyEntries)[0],
                                            s.Split(new char[] { '@', ':' }, StringSplitOptions.RemoveEmptyEntries)[1]);
                                        control.enumData.Add(e);

                                        SJeMES_Framework.Web.JSONControlBOption o = new SJeMES_Framework.Web.JSONControlBOption(
                                            s.Split(new char[] { '@', ':' }, StringSplitOptions.RemoveEmptyEntries)[0],
                                            s.Split(new char[] { '@', ':' }, StringSplitOptions.RemoveEmptyEntries)[1]);
                                        control.options.Add(o);
                                    }
                                }
                                catch 
                                {}
                               
                            }


                            control.datatype = dr.Cells["DataType"].Value.ToString();
                            control = SJeMES_Framework.Web.JSONFormClass.GetBTypeAndRules(control, dr.Cells["DataType"].Value.ToString());

                            control.IsNull = Convert.ToBoolean(dr.Cells["IsNull"].Value);
                            if (!Convert.ToBoolean(dr.Cells["IsNull"].Value))
                            {
                                control.rules.Add(new SJeMES_Framework.Web.JSONControlHRules("required", "[" + control.label + "]数据不能为空"));

                            }

                            B.tableHead.Add(control);
                        }
                    }
                    catch 
                    { }
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
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
    }
}
