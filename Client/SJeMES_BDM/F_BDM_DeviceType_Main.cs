using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_BDM
{
    public partial class F_BDM_DeviceType_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        /// <summary>
        /// 记录当前数据唯一id
        /// </summary>
        public string idd { get; set; }
        public F_BDM_DeviceType_Main()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            this.DesktopBounds = Screen.GetWorkingArea(this); // 在桌面区域全屏显示。
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            //InitDateTimePicker(start_dateTime);
            //InitDateTimePicker(end_dateTime);

            #region 赋试穿枚举值
            DataTable dt = new DataTable();
            dt.Columns.Add("enum_code", typeof(string));
            dt.Columns.Add("enum_value", typeof(string));

            //for (int i = 0; i < 5; i++)
            //{
            //    DataRow dr = dt.NewRow();
            //    dr["enum_code"] = i;
            //    switch (i)
            //    {
            //        case 0:
            //            dr["enum_value"] = "全部";
            //            break;
            //        case 1:
            //            dr["enum_value"] = "制程机器";
            //            break;
            //        case 2:
            //            dr["enum_value"] = "检验工具";
            //            break;
            //        case 3:
            //            dr["enum_value"] = "测试设备";
            //            break;
            //        case 4:
            //            dr["enum_value"] = "其他";
            //            break;
            //        default:
            //            break;
            //    }
            //    dt.Rows.Add(dr);
            //}
            for (int i = 0; i < 5; i++)
            {
                DataRow dr = dt.NewRow();
                dr["enum_code"] = i;
                switch (i)
                {
                    case 0:
                        dr["enum_value"] = "All";
                        break;
                    case 1:
                        dr["enum_value"] = "Process_Machine";
                        break;
                    case 2:
                        dr["enum_value"] = "Validation_Tools";
                        break;
                    case 3:
                        dr["enum_value"] = "Test_Equipment";
                        break;
                    case 4:
                        dr["enum_value"] = "Other";
                        break;
                    default:
                        break;
                }
                dt.Rows.Add(dr);
            }
            //List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            //Dictionary<string, object> dic = new Dictionary<string, object>();
            //dic.Add("0", "全部");
            //dic.Add("1", "制程机器");
            //dic.Add("2", "检验工具");
            //dic.Add("3", "测试设备");
            //dic.Add("4", "其他");
            //list.Add(dic);

            combox_eq_type.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                combox_eq_type.DisplayMember = "enum_value";
                combox_eq_type.ValueMember = "enum_code";
            }
            #endregion
        }

        public void F_BDM_DeviceType_Main_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            //只要加载一次委托 
            pageControl1.BindPageEvent += GetData;
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="STAFF_NO"></param>
        public void GetData(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                string eq_no = this.order.Text;
                string eq_name = string.Empty;
                string correction_frequency = string.Empty;
                string control_type = string.Empty;


                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("EQ_NO", this.order.Text);
                data.Add("EQ_NAME", this.name.Text);
                data.Add("CORRECTION_FREQUENCY", this.txt_correction.Text);
                data.Add("CONTROL_TYPE", combox_eq_type.SelectedIndex);
                data.Add("REMARK", remark.Text);

                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);


                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_BDMAPI", "SJ_BDMAPI.BDM_Equipment", "GetEquipment",
                     Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                    throw new Exception(ret.ErrMsg);
                else
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);

                    dataGridView1.Rows.Clear();
                    var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dataGridView1.Rows.Add();
                            DataGridViewRow dgvr = dataGridView1.Rows[i];
                            dgvr.Cells["唯一id"].Value = dr["唯一ID"].ToString();//行号
                            dgvr.Cells["id"].Value = dr["LINE"].ToString();//行号
                            dgvr.Cells["EQ_NO"].Value = dr["EQ_NO"].ToString();//编号
                            dgvr.Cells["EQ_NAME"].Value = dr["EQ_NAME"].ToString();//名称
                            dgvr.Cells["CORRECTION_FREQUENCY"].Value = dr["CORRECTION_FREQUENCY"].ToString();//频率
                            dgvr.Cells["note"].Value = dr["REMARK"].ToString();//频率
                            dgvr.Cells["control_type"].Value = dr["CONTROL_TYPE"].ToString();//类型

                            i++;//LINE,EQ_NO,EQ_NAME,CORRECTION_FREQUENCY
                        }
                    }
                    totalCount = int.Parse(dic["rowCount"].ToString());
                    GenClass.AutoSizeColumn(dataGridView1);
                    this.splitContainer1.Visible = true;
                }

                this.dataGridView1.ClearSelection();
                //this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {

            F_BDM_DeviceType_Add f_BDM_DeviceType_Add = new F_BDM_DeviceType_Add();
            f_BDM_DeviceType_Add.ShowDialog();
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                switch (dataGridView1.Columns[e.ColumnIndex].Name)
                {
                    case "correction_frequency":
                        textBox.Visible = true;
                        textBox.Text = dataGridView1.CurrentCell.Value.ToString(); //对textbox赋值
                        Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                        textBox.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                        idd = dataGridView1.Rows[e.RowIndex].Cells["唯一id"].Value.ToString();
                        break;
                    case "Edit":
                        F_BDM_DeviceType_Edit f_BDM_DeviceType_Edit = new F_BDM_DeviceType_Edit(dataGridView1.Rows[e.RowIndex].Cells["eq_no"].Value.ToString());
                        f_BDM_DeviceType_Edit.ShowDialog();
                        NewLoad();
                        break;
                    case "del_eq_type":
                        Dictionary<string, object> p = new Dictionary<string, object>();
                        p.Add("id", dataGridView1.Rows[e.RowIndex].Cells["唯一id"].Value.ToString());
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                    "SJ_BDMAPI",//类库名
                                                    "SJ_BDMAPI.BDM_Equipment",//类名
                                                    "DeleteEquipmentType",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(p));

                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                        if (!ret.IsSuccess)
                        {
                            throw new Exception(ret.ErrMsg);
                        }
                        NewLoad();
                        break;
                    case "RelevantParameter":
                            F_BDM_DeviceType_Parameter f_bdm_devicetype_parameter = new F_BDM_DeviceType_Parameter(dataGridView1.Rows[e.RowIndex].Cells["唯一id"].Value.ToString());
                            f_bdm_devicetype_parameter.ShowDialog();
                        break;
                    case "correction":
                        F_BDM_DeviceType_correction f = new F_BDM_DeviceType_correction(dataGridView1.Rows[e.RowIndex].Cells["唯一id"].Value.ToString());
                        f.ShowDialog();
                        break;
                    default:
                        textBox.Visible = false;
                        break;
                }
                //#region old

                //if (dataGridView1.Columns[e.ColumnIndex].Name == "correction_frequency") // text显示条件 
                //{
                //    textBox.Visible = true;
                //    textBox.Text = dataGridView1.CurrentCell.Value.ToString(); //对textbox赋值
                //    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                //    textBox.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 

                //    idd = dataGridView1.Rows[e.RowIndex].Cells["唯一id"].Value.ToString();

                //}
                ////编辑
                //if (dataGridView1.Columns[e.ColumnIndex].Name == "Edit")
                //{

                //    F_BDM_DeviceType_Edit f_BDM_DeviceType_Edit = new F_BDM_DeviceType_Edit(dataGridView1.Rows[e.RowIndex].Cells["eq_no"].Value.ToString());

                //}
                ////关联参数项目
                //if (dataGridView1.Columns[e.ColumnIndex].Name == "RelevantParameter")
                //{
                //    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["accessory"] as DataGridViewOperationCell;
                //    if (cell.CurrentItem == null)
                //    {
                //        return;
                //    }
                //    else
                //    {

                //    }
                //}
                ////关联校正项目
                //if (dataGridView1.Columns[e.ColumnIndex].Name == "correction")
                //{
                //    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["accessory"] as DataGridViewOperationCell;
                //    if (cell.CurrentItem == null)
                //    {
                //        return;
                //    }
                //    else
                //    {

                //    }
                //}

                //#endregion

            }
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Enter)
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("id", idd);
                data.Add("CORRECTION_FREQUENCY", this.textBox.Text);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_BDMAPI", "SJ_BDMAPI.BDM_Equipment", "UpdateEquipment",
                     Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                    throw new Exception(ret.ErrMsg);
                else
                {
                    textBox.Visible = false;

                }

            }
            
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = textBox.Text.ToString();
        }


        public void NewLoad()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
    }
}
