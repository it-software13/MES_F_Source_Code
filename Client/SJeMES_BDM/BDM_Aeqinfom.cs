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
    public partial class BDM_Aeqinfom : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public BDM_Aeqinfom()
        {
            InitializeComponent();
            InitDateTimePicker(dateTimePicker1);
            InitDateTimePicker(dateTimePicker2);
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
        Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        #region 日期控件初始为空值处理

        /// <summary>
        /// 初始化日期时间控件
        /// </summary>
        /// <param name="dtp"></param>
        public static void InitDateTimePicker(DateTimePicker dtp)
        {
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = " ";  //必须设置成" "
            dtp.ValueChanged -= DateTimePicker_ValueChanged;
            dtp.ValueChanged += DateTimePicker_ValueChanged;
            dtp.KeyPress -= DateTimePicker_KeyPress;
            dtp.KeyPress += DateTimePicker_KeyPress;
        }

        public static void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = "yyyy-MM-dd"; //null;
            dtp.Checked = false;// 解决BUG ：防止日期控件不能选择相同日期的 --- 要放置在设置格式之后
        }

        public static void DateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)8)  // backspace左删除键
            {
                DateTimePicker dtp = (DateTimePicker)sender;
                dtp.Format = DateTimePickerFormat.Custom;
                dtp.CustomFormat = " ";
            }
        }
        #endregion
        public void LoadPage()
        {
            pageControl1.PageSize = 25;
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
        private void BDM_Aeqinfom_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            pageControl1.BindPageEvent += GetMain_List;
            LoadPage();
        }
        public void GetMain_List(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                string jiaz_date = string.Empty;
                string jinc_date = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.dateTimePicker1.Text))
                {
                    jinc_date = Convert.ToDateTime(this.dateTimePicker1.Value).ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(this.dateTimePicker2.Text))
                {
                    jiaz_date = Convert.ToDateTime(this.dateTimePicker2.Value).ToString("yyyy-MM-dd");
                }
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("eq_info_no", textBox1.Text);//编号
                data.Add("eq_info_name", textBox2.Text);//设备名称
                data.Add("department_name", textBox3.Text);//部门
                data.Add("control_type", textBox4.Text);//管控类型
                data.Add("remark", textBox5.Text);//备注
                data.Add("eq_name", textBox6.Text);//设备类型

                data.Add("jiaz_date", jiaz_date);
                data.Add("jinc_date", jinc_date);

                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Aeqinfom",//类名
                                            "GetData",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["Column2"].Value = i;
                        dgvr.Cells["Column3"].Value = dr["eq_info_no"].ToString();
                        dgvr.Cells["Column4"].Value = dr["eq_info_name"].ToString();
                        dgvr.Cells["Column5"].Value = dr["department_name"].ToString();
                        dgvr.Cells["Column6"].Value = dr["workshop_section_name"].ToString();
                        dgvr.Cells["Column7"].Value = dr["eq_name"].ToString();
                        dgvr.Cells["Column8"].Value = dr["control_name"].ToString();
                        dgvr.Cells["Column9"].Value = dr["wh_date"].ToString();
                        // correction_frequency
                        string jz_date = dr["jz_date"].ToString();
                        DateTime dateTime = DateTime.Now;
                        bool convert_jz_date = DateTime.TryParse(jz_date,out dateTime);
                        string correction_frequency_str = dr["correction_frequency"].ToString();
                        int correction_frequency = 0;
                        bool convert_correction_frequency = int.TryParse(correction_frequency_str, out correction_frequency);

                        dgvr.Cells["Column10"].Value = dr["jz_date"].ToString();
                        dgvr.Cells["Column11"].Value = (convert_jz_date && convert_correction_frequency) ? dateTime.AddDays(correction_frequency).ToString("yyyy-MM-dd") : "";//下次校正日期
                        dgvr.Cells["Column12"].Value =dr["device_state"].ToString();
                        dgvr.Cells["Column13"].Value = dr["remark"].ToString();
                        dgvr.Cells["Column14"].Value = dr["id"].ToString();
                        dgvr.Cells["Column15"].Value = dr["eq_no"].ToString();
                        i++;
                    }
                    GenClass.AutoSizeColumn(dataGridView1);
                }
              
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadPage();//khaleel
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    string name = this.dataGridView1.Columns[e.ColumnIndex].Name;
                 
                    if (name=="Column1")
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("id", dataGridView1.CurrentRow.Cells["Column14"].Value.ToString());
                        dic.Add("eq_info_no", dataGridView1.CurrentRow.Cells["Column3"].Value.ToString());
                        dic.Add("eq_no", dataGridView1.CurrentRow.Cells["Column15"].Value.ToString());
                        BDM_Aeqinfomty frm = new BDM_Aeqinfomty(dic);
                        frm.ShowDialog();
                        LoadPage();

                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
    }
}
