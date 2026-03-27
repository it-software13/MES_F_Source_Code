using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_Control_Library.Controls
{
    public partial class UCModuleBaseList : UCControlBase, IContainerControl
    {
        private SJeMES_Framework.Web.JSONPanelClassHList HList;

        #region 属性
        private string _SelectedId;
        public string SelectedId
        {
            get { return _SelectedId; }
        }


        private int _DataTotal;

        public int DataTotal
        {
            get
            {
                return _DataTotal;
            }
            set
            {
                _DataTotal = value;
                if ((value % PageRow) == 0)
                {
                    PageCount = value / PageRow;
                }
                else
                {
                    PageCount = (value / PageRow) + 1;
                }

                lab_DataTotal.Text = lab_DataTotal.Text +":"+ value.ToString();
            }
        }

        private DataTable _Data;

        public DataTable Data
        {
            get
            {
                return _Data;
            }
            set
            {
                _Data = value;
            }
        }

        private string _Where=string.Empty;

        public string Where
        {
            get
            {
                return _Where;
            }
            set
            {
                _Where = value;
            }
        }

        private string _OrderBy = string.Empty;

        public string OrderBy
        {
            get
            {
                return _OrderBy;
            }
            set
            {
                _OrderBy = value;
            }
        }

        private int _Page = 1;

        public int Page
        {
            get
            {
                return _Page;
            }
            set
            {
                _Page = value;
                ucPagerControl21.PageIndex = value;
                GetData();
            }
        }

        private int _PageRow =20;

        public int PageRow
        {
            get
            {
                return _PageRow;
            }
            set
            {
                _PageRow = value;
                if ((DataTotal % value) == 0)
                {
                    PageCount = DataTotal / value;
                }
                else
                {
                    PageCount = (DataTotal / value)+1;
                }
                ucPagerControl21.PageSize = value;
                GetData();
            }
        }

        private int _PageCount = 0;

        public int PageCount
        {
            get
            {
                return _PageCount;
            }
            set
            {
                _PageCount = value;
                ucPagerControl21.PageCount = value;
            }
        }

        private string _ModuleCode;

        public string ModuleCode
        {
            get
            {
                return _ModuleCode;
            }
            set
            {
                _ModuleCode = value;
            }
        }

        private SJeMES_Framework.Class.ClientClass _Client;

        public SJeMES_Framework.Class.ClientClass Client
        {
            get
            {
                return _Client;
            }
            set
            {
                _Client = value;

            }
        }

        private string _title;

        public string title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;

            }
        }
        #endregion


        //定义委托
        public delegate void SeeDataHandle(object sender, EventArgs e);
        //定义事件
        public event SeeDataHandle SeeData;

        //定义委托
        public delegate void EditDataHandle(object sender, EventArgs e);
        //定义事件
        public event EditDataHandle EditData;

        //定义委托
        public delegate void AddDataHandle(object sender, EventArgs e);
        //定义事件
        public event AddDataHandle AddData;

        public UCModuleBaseList(string ModuleCode, SJeMES_Framework.Class.ClientClass Client,string tilte)
        {
            try

            {
                InitializeComponent();
                this.ModuleCode = ModuleCode;
                this.Client = Client;
                this.title = tilte;
                ucSelectTool1.Client = Client;
                ucPagerControl21.Client = Client;
                string sql = "";
                if (Client.Language != "cn")
                {
                    sql = @"
                           SELECT 
                            ui_tittle AS '功能名称',
                            ui_code AS '控件ID',
                            ui_id AS '控件名称',
                            (case when isnull(ui_en,'')='' then ui_id else ui_en end) AS '英语名称',
                            (case when isnull(ui_yn,'')='' then ui_id else ui_yn end) AS '粤语名称'
                            FROM SJQDMS_UILAN where ui_tittle='all'";
                    DataTable dtLAN = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(_Client.WebServiceUrl, sql, new Dictionary<string, string>());
                    string name = "";
                    if (dtLAN != null && dtLAN.Rows.Count > 0)
                    { 
                        if (Client.Language == "hk" && dtLAN.Rows.Count > 0)
                        {
                            DataRow[] dr = dtLAN.Select("控件名称='" + label2.Text + "'");
                            if(dr.Length>0)
                            {
                                name = dr[0]["粤语名称"].ToString();
                                label2.Text = !string.IsNullOrEmpty(name) ? name : label2.Text;
                            }

                            dr = dtLAN.Select("控件名称='" + label3.Text + "'");
                            if(dr.Length > 0)
                            {
                                name = dr[0]["粤语名称"].ToString();
                                label3.Text = !string.IsNullOrEmpty(name) ? name : label3.Text;
                            }

                            dr = dtLAN.Select("控件名称='" + label4.Text + "'");
                            if (dr.Length > 0)
                            {
                                name = dr[0]["粤语名称"].ToString();
                                label4.Text = !string.IsNullOrEmpty(name) ? name : label4.Text;
                            }

                            dr = dtLAN.Select("控件名称='" + lab_DataTotal.Text + "'");
                            if (dr.Length > 0)
                            {
                                name = dr[0]["粤语名称"].ToString();
                                lab_DataTotal.Text = !string.IsNullOrEmpty(name) ? name : lab_DataTotal.Text;
                            }
                           

                        }
                        else if (Client.Language == "en" && dtLAN.Rows.Count > 0)
                        {
                            DataRow[] dr = dtLAN.Select("控件名称='" + label2.Text + "'");
                            if (dr.Length > 0)
                            {
                                name = dr[0]["英语名称"].ToString();
                                label2.Text = !string.IsNullOrEmpty(name) ? name : label2.Text;
                            }

                            dr = dtLAN.Select("控件名称='" + label3.Text + "'");
                            if (dr.Length > 0)
                            {
                                name = dr[0]["英语名称"].ToString();
                                label3.Text = !string.IsNullOrEmpty(name) ? name : label3.Text;
                            }

                            dr = dtLAN.Select("控件名称='" + label4.Text + "'");
                            if (dr.Length > 0)
                            {
                                name =dr[0]["英语名称"].ToString();
                                label4.Text = !string.IsNullOrEmpty(name) ? name : label4.Text;
                            }

                            dr = dtLAN.Select("控件名称='" + lab_DataTotal.Text + "'");
                            if (dr.Length > 0)
                            {
                                name = dr[0]["英语名称"].ToString();
                                lab_DataTotal.Text = !string.IsNullOrEmpty(name) ? name : lab_DataTotal.Text;
                            }
                        }
                    }
                }


                sql = @"
                        SELECT DISTINCT
                        a.AppCode AS '模块代号',
                        a.AppName AS '模块名称',
                        'False' AS '全部权限',
                        ISNULL([Select],'False') AS '查看数据',
                        ISNULL([Add],'False') AS '添加数据',
                        ISNULL([Edit],'False') AS '修改数据',
                        ISNULL([Delete],'False') AS '删除数据'
                        FROM SYSAPP03M a
                        LEFT JOIN (select a.UserCode,b.AppCode,
                        [Select],[Add],Edit,[Delete],
                        DoSure,Audit,DoWork,[Print],Fun from SYSROLE01A1 a
                        left join SYSROLE02M b on a.Role_Name=b.Role_Name) b ON a.AppCode = b.AppCode
                        where a.AppName in(select menuname from SYSPOWER 
                        where  UserCode='" + Client.UserCode + "')  and a.AppCode='" + ModuleCode.Remove(0, 3) + "'";//
                //DataTable dt = Client.GetDT(sql);

                DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Client.WebServiceUrl, sql, new Dictionary<string, string>());
                if (dt!=null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["添加数据"].ToString().Trim() == "False")
                    {
                        ucBtnImg4.Enabled = false;
                    }
                    if (dt.Rows[0]["修改数据"].ToString().Trim() == "False")
                    {
                        //ucBtnImg5.Visible = false;
                        ucBtnImg5.Enabled = false;

                    }
                    if (dt.Rows[0]["删除数据"].ToString().Trim() == "False")
                    {
                        //ucBtnImg3.Visible = false;
                        ucBtnImg3.Enabled = false;

                    }
                    if (dt.Rows[0]["查看数据"].ToString().Trim() == "False")
                    {
                        ucBtnImg5.Enabled = false;
                        ucBtnImg4.Enabled = false;
                        ucBtnImg3.Enabled = false;
                        //throw new Exception("无数据查看权限，请联系管理员");
                        throw new Exception("No permission to view data, please contact the administrator");
                    }
                }
                //else
                //{
                //    throw new Exception("权限不足，请联系管理员");
                //}
                HList = ModuleHelper.GetHList(this.ModuleCode,false ,this.Client);
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this.FindForm(),ex.ToString()); 
            }
        }

        private void UCModuleBaseList_Load(object sender, EventArgs e)
        {
            
        
            try
            {
                this.dataGridView1.AutoGenerateColumns = false;
                DataTotal = 0;
                List<KeyValuePair<string, string>> lstCom = new List<KeyValuePair<string, string>>();
                lstCom.Add(new KeyValuePair<string, string>("10", "10"));
                lstCom.Add(new KeyValuePair<string, string>("20", "20"));
                lstCom.Add(new KeyValuePair<string, string>("30", "30"));
                lstCom.Add(new KeyValuePair<string, string>("50", "50"));
                lstCom.Add(new KeyValuePair<string, string>("100", "100"));
                ucCombox1.Source = lstCom;

                this.panel1.SizeChanged += new System.EventHandler(this.panel1_SizeChanged);

                //            foreach (DataGridViewRow dr in dataGridView1.Rows)
                //            {
                //                if (Convert.ToBoolean(dr.Cells[3].Value.ToString())
                //                    && Convert.ToBoolean(dr.Cells[4].Value.ToString())
                //                     && Convert.ToBoolean(dr.Cells[5].Value.ToString())
                //                      && Convert.ToBoolean(dr.Cells[6].Value.ToString())
                //                       && Convert.ToBoolean(dr.Cells[7].Value.ToString())
                //                        && Convert.ToBoolean(dr.Cells[8].Value.ToString())
                //                         && Convert.ToBoolean(dr.Cells[9].Value.ToString())
                //                          && Convert.ToBoolean(dr.Cells[10].Value.ToString())
                //                           && Convert.ToBoolean(dr.Cells[11].Value.ToString())
                //                    )
                //                {
                //                    dr.Cells[2].Value = true;
                //                }
                //            }

                UpdateDataColumn();
                GetData();

            }
            catch(Exception ex)
            {
                MessageHelper.ShowErr(this.FindForm(), ex.Message);
            }
        }

        public void GetData()
        {
            try
            {
                if (HList != null)
                {
                    Dictionary<string, object> retData = ModuleHelper.GetListData(
                        ModuleCode, HList.tablename, Where, OrderBy, Page, PageRow,title,Client);

                    string json = JsonReplaceSign(retData[HList.tablename].ToString());
                    json = json.Replace(@"\", @"\\");
                    //string pattern = @"(\\[^bfrnt\\/‘\""])";
                    //json = System.Text.RegularExpressions.Regex.Replace(json, pattern, "\\$1");
                    //pattern = @"(\\[^bfrnt\\/‘\""])";
                    //json = System.Text.RegularExpressions.Regex.Replace(json, pattern, "\\$1");
                    Data = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);
                    DataTotal = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(retData["Total"].ToString());
                    if (Data!=null)
                    {
                        var enumFieldList = HList.tableHead.Where(x => x.enumData != null && x.enumData.Count > 0).Select(x => x);
                        if (enumFieldList.Count() > 0)
                        {
                            foreach (DataRow item in Data.Rows)
                            {
                                foreach (var enumField in enumFieldList)
                                {
                                    try
                                    {
                                        var getEnumValue = enumField.enumData.FirstOrDefault(x => x.value == item[enumField.prop].ToString());
                                        if (getEnumValue != null)
                                            item[enumField.prop] = getEnumValue.label;
                                    }
                                    catch (Exception)
                                    {
                                    }
                                }
                            }
                        }
                        dataGridView1.DataSource = Data.DefaultView;
                        if (Data.Rows.Count > 0)
                        {
                            _SelectedId =
                            (dataGridView1.Rows[0].DataBoundItem as DataRowView).Row["id"].ToString();
                        }
                        dataGridView1.Update();
                    }
                    else
                    {
                        dataGridView1.DataSource =null;
                        dataGridView1.Update();
                    }
                  
                }
            }catch(Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this.FindForm(),  ex.Message);
            }
        }

        /// <summary>
        /// json字符串将属性值中的英文双引号变成中文双引号
        /// </summary>
        /// <param name="strJson">json字符串</param>
        /// <returns></returns>
        public string JsonReplaceSign(string strJson)
        {
            //获取每个字符
            char[] temp = strJson.ToCharArray();
            //获取字符数组长度
            int n = temp.Length;
            //循环整个字符数组
            for (int i = 0; i < n; i++)
            {
                //查找json属性值（:+" ）
                if (temp[i] == ':' && temp[i + 1] == '"')
                {
                    //循环属性值内的字符（：+2 推算到value值）
                    for (int j = i + 2; j < n; j++)
                    {
                        //判断是否是英文双引号
                        if (temp[j] == '"')
                        {
                            //排除json属性的双引号
                            if (temp[j + 1] != ',' && temp[j + 1] != '}')
                            {
                                //替换成中文双引号
                                temp[j] = '”';
                            }
                            else if (temp[j + 1] == ',' || temp[j + 1] == '}')
                            {
                                break;
                            }
                        }
                        //else if (temp[j] == '-')
                        //{
                        //    temp[j] = ' ';
                        //}
                        else if (true)
                        {
                            // 要过虑其他字符，继续添加判断就可以
                        }
                    }
                }
            }
            return new String(temp);
        }

        private void UpdateDataColumn()
        {
            if (this.HList != null)
            {
                dataGridView1.Columns.Clear();
                string sql = @"select * from (select a.UserCode,b.AppCode,b.TableName,
b.ColumnName,b.ColumnID from SYSROLE01A1 a
left join SYSROLE01M c on a.Role_Name = c.Role_Name
left join SYSPOWER_R b on c.Role_No = b.Role_No)a where UserCode='" + Client.UserCode + "' and AppCode='" + ModuleCode.Remove(0, 3) + "'";
                DataTable dtPow = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Client.WebServiceUrl, sql, new Dictionary<string, string>());
                string columnName = "TableName", columnName1 = "ColumnName";

                string sql1 = "select * from SYSLANGUAGE where AppCode='" + ModuleCode.Remove(0, 3) + "'";
                //DataTable dt = Client.GetDT(sql);

                DataTable dtLAN = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Client.WebServiceUrl, sql1, new Dictionary<string, string>());
                if (dtPow.Rows.Count == 0)
                {
                    sql1 = "select * from SYSLANGUAGE where AppCode='" + ModuleCode + "'";
                    dtLAN = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Client.WebServiceUrl, sql1, new Dictionary<string, string>());
                }

                if (dtPow.Rows.Count == 0)
                {
                    foreach (SJeMES_Framework.Web.JSONPanelClassHListItem hli in this.HList.tableHead)
                    {
                        DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                        dgvc.Name = "dc_" + hli.prop;
                        dgvc.HeaderText = hli.label;
                        if (dtLAN.Rows.Count > 0 && Client.Language != "cn")
                        {
                            DataRow[] dataRows_LAN = dtLAN.Select(columnName1 + "='" + hli.label + "' and " + columnName + "='" + HList.tablename + "'");
                            if (Client.Language == "en" && dataRows_LAN.Length > 0)
                            {
                                dgvc.HeaderText = !string.IsNullOrEmpty(dataRows_LAN[0]["ColumnName_EN"].ToString()) ? dataRows_LAN[0]["ColumnName_EN"].ToString() : hli.label;
                            }
                            else if (Client.Language == "hk" && dataRows_LAN.Length > 0)
                            {
                                dgvc.HeaderText = !string.IsNullOrEmpty(dataRows_LAN[0]["ColumnName_HK"].ToString()) ? dataRows_LAN[0]["ColumnName_HK"].ToString() : hli.label;
                            }
                        }
                        dgvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dgvc.DataPropertyName = hli.prop;

                        dataGridView1.Columns.Add(dgvc);
                    }
                }
                else
                {
                    foreach (SJeMES_Framework.Web.JSONPanelClassHListItem hli in this.HList.tableHead)
                    {
                        DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                        DataRow[] dataRows = dtPow.Select(columnName1 + "='" + hli.label + "' and " + columnName + "='" + HList.tablename + "'");
                        if (dataRows.Length > 0)
                        {
                            dgvc.Name = "dc_" + hli.prop;
                            dgvc.HeaderText = hli.label;
                            if (dtLAN.Rows.Count > 0 && Client.Language != "cn")
                            {
                                DataRow[] dataRows_LAN = dtLAN.Select(columnName1 + "='" + hli.label + "' and " + columnName + "='" + HList.tablename + "'");
                                if (Client.Language == "en" && dataRows_LAN.Length > 0)
                                {
                                    dgvc.HeaderText = !string.IsNullOrEmpty(dataRows_LAN[0]["ColumnName_EN"].ToString()) ? dataRows_LAN[0]["ColumnName_EN"].ToString() : hli.label;
                                }
                                else if (Client.Language == "hk" && dataRows_LAN.Length > 0)
                                {
                                    dgvc.HeaderText = !string.IsNullOrEmpty(dataRows_LAN[0]["ColumnName_HK"].ToString()) ? dataRows_LAN[0]["ColumnName_HK"].ToString() : hli.label;
                                }
                            }
                            dgvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            dgvc.DataPropertyName = hli.prop;

                            dataGridView1.Columns.Add(dgvc);
                        }
                    }
                }

            }
        }

        private void ucSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                HList = ModuleHelper.GetHList(this.ModuleCode, ucSwitch1.Checked, this.Client);
                UpdateDataColumn();
            }catch (Exception ex)
            {
                MessageHelper.ShowErr(this.FindForm(), ex.Message);
            }
        }

        private void ucCombox1_SelectedChangedEvent(object sender, EventArgs e)
        {
            Page = 1;
            PageRow = Convert.ToInt32(ucCombox1.SelectedText);
        }

        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            if (this.FindForm() != null)
            {
                int Height = this.FindForm().Height;
                if (Height >= 760 && Height < 1000)
                {
                    ucCombox1.SelectedIndex = 1;
                }
                else if (Height >= 1000)
                {
                    ucCombox1.SelectedIndex = 2;
                }
            }
            
        }

        private void ucSelectTool1_SelectData(object sender, EventArgs e)
        {
            Where = "@ALL@" + ucSelectTool1.WhereKey;
            Page = 1;
            GetData();
        }

        private void ucBtnImg3_BtnClick(object sender, EventArgs e)
        {
            try
            {
                if(dataGridView1.SelectedCells.Count>0)
                {
                   // if (SJeMES_Control_Library.MessageHelper.ShowWarning(this.FindForm(), "是否确认删除选择的数据？") == DialogResult.OK)
                    if (SJeMES_Control_Library.MessageHelper.ShowWarning(this.FindForm(), "Are you sure to delete the selected data？") == DialogResult.OK)
                    {
                        List<string> Ids = new List<string>();
                        for (int i = 0; i < dataGridView1.SelectedCells.Count; i++)
                        {
                            string id =
                            (dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].DataBoundItem as DataRowView).Row["id"].ToString();

                            if (Data.Columns.Contains("status"))
                            {
                                string status = Data.Select("id='" + id + "'")[0]["status"].ToString();
                                if (status == "1" || status == "2" || status == "7")
                                {
                                    //throw new Exception("该数据为确认或审核状态，不能删除");
                                    throw new Exception("The data is confirmed or reviewed and cannot be deleted");
                                }
                            }

                            if(!Ids.Contains(id))
                            {
                                Ids.Add(id);
                            }
                        }
                        

                        DataTable dt = new DataTable();
                        dt.Columns.Add("TableName");
                        dt.Columns.Add("Id");
                        foreach (string id in Ids)
                        {
                            DataRow dr = dt.NewRow();
                            dr["TableName"] = HList.tablename;
                            dr["Id"] = id;
                            dt.Rows.Add(dr);
                        }
                        if (ModuleHelper.DelData(dt, Client))
                        {
                            //SJeMES_Control_Library.MessageHelper.ShowSuccess(this.FindForm(),"删除数据成功");
                            SJeMES_Control_Library.MessageHelper.ShowSuccess(this.FindForm(), "Data deleted successfully");
                            GetData();
                        }
                    }
                }
                else
                {
                    //SJeMES_Control_Library.MessageHelper.ShowErr(this.FindForm(), "请先选中要删除的行");
                    SJeMES_Control_Library.MessageHelper.ShowErr(this.FindForm(), "Please select the row to delete first");
                }
            }catch(Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this.FindForm(),  ex.Message);
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    if (SeeData != null)
                        SeeData(this, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this.FindForm(),  ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>=0 && e.ColumnIndex>=0)
            {
                _SelectedId =
                    (dataGridView1.Rows[e.RowIndex].DataBoundItem as DataRowView).Row["id"].ToString();
            }
        }

        private void ucBtnImg4_BtnClick(object sender, EventArgs e)
        {
            if (AddData != null)
                AddData(this, new EventArgs());
        }

        private void ucBtnImg5_BtnClick(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.RowCount == 0)
                {
                    //throw new Exception("暂无数据编辑！");
                    throw new Exception("No data edit！");
                }
                string id =
                                (dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].DataBoundItem as DataRowView).Row["id"].ToString();

                if (Data.Columns.Contains("status"))
                {
                    DataRow[] drs = Data.Select("id='" + id+"'");
                    string status = drs[0]["status"].ToString();
                    if (status != "8")
                    {
                        //throw new Exception("该数据为确认或审核状态，不能修改");
                        throw new Exception("This data is in confirmation or review status and cannot be modified");
                    }
                }
                if (EditData != null)
                    EditData(this, new EventArgs());
            }catch(Exception ex)
            {
                MessageHelper.ShowErr(this.FindForm(),  ex.Message);
            }
        }
         
        private void ucPagerControl21_ShowSourceChanged(object currentSource)
        {
            if (DataTotal > 0)
            {
                Page = ucPagerControl21.PageIndex;
            }
        }
    }
}
