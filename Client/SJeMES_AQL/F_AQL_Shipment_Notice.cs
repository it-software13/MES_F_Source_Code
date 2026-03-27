using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SjeMES_QCM_Ex;
using SJeMES_Shared_Form;
using SJeMES_Control_Library;

namespace SJeMES_AQL
{
    public partial class F_AQL_Shipment_Notice : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_AQL_Shipment_Notice()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

            InitDateTimePicker(starttime);
            InitDateTimePicker(endtime);
        }

        private void F_AQL_Shipment_Notice_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            pageControl1.BindPageEvent += GetShipment_Notice_Main;
            this.dataGridView1.ClearSelection();
            LoadPage();
        }

        /// <summary>
        /// 初始化分页
        /// </summary>
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        /// <summary>
        /// 查询-出货通知
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetShipment_Notice_Main(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                string start_date = string.Empty;
                string end_date = string.Empty;
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("PO", textBox1.Text.Trim());
                p.Add("SE_ID", textBox2.Text.Trim());//销售订单
                p.Add("SHIPCOUNTRY_NAME", textBox4.Text.Trim());//国家
                p.Add("ART", textBox5.Text.Trim());
                p.Add("NAME_T", txt_shoes.Text.Trim());//鞋型

                if (!string.IsNullOrWhiteSpace(this.starttime.Text))
                {
                    start_date = Convert.ToDateTime(this.starttime.Value).ToString("yyyy-MM-dd");
                    p.Add("start_date", start_date);
                }
                if (!string.IsNullOrWhiteSpace(this.endtime.Text))
                {
                    end_date = Convert.ToDateTime(this.endtime.Value).ToString("yyyy-MM-dd");
                    p.Add("end_date", end_date);
                }


                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_Shipment_Notice",//类名
                                            "GetShipment_Notice_Main",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());

                totalCount = dt.Rows.Count;
                dt = GetPagedTable(dt, pageIndex, pageSize);

                
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["序号"].Value =(i+1).ToString();
                        dgvr.Cells["销售订单号"].Value = dr["SE_ID"].ToString();
                        dgvr.Cells["鞋型"].Value = dr["shoe_name"].ToString();
                        dgvr.Cells["客户号"].Value = dr["se_custid"].ToString();
                        dgvr.Cells["制令"].Value = dr["WORKORDER_NO"].ToString();
                        dgvr.Cells["客户订单号"].Value = dr["PO_NO"].ToString(); 
                        dgvr.Cells["ART_NO"].Value = dr["prod_no"].ToString();
                        dgvr.Cells["数量"].Value = dr["se_qty"].ToString();
                        dgvr.Cells["箱数"].Value = dr["BOXS_NUMBERS"].ToString();
                        dgvr.Cells["国家"].Value = dr["SHIPCOUNTRY_NAME"].ToString();
                        dgvr.Cells["柜车"].Value = dr["CONTAINER_TRUCK"].ToString();
                        dgvr.Cells["组别"].Value = dr["Lines_List"].ToString();
                        dgvr.Cells["crd"].Value = dr["CR_REQDATE"].ToString();
                        dgvr.Cells["planshipdate"].Value = dr["nst"].ToString();
                        dgvr.Cells["出货日期"].Value = dr["posting_date"].ToString();
                        dgvr.Cells["车次"].Value = dr["TRAIN_NUMBER"].ToString();
                        i++;
                    }
                }
                //totalCount = int.Parse(dic["rowCount"].ToString());
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
            LoadPage();
        }

        /// <summary>
        /// 查询-出货通知_导出
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public DataTable GetShipment_Notice_Main_Export()
        {
            DataTable dt = new DataTable();
            try
            {
                string start_date = string.Empty;
                string end_date = string.Empty;
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("PO", textBox1.Text.Trim());
                p.Add("SE_ID", textBox2.Text.Trim());//销售订单
                p.Add("SHIPCOUNTRY_NAME", textBox4.Text.Trim());//国家
                p.Add("ART", textBox5.Text.Trim());
                p.Add("NAME_T", txt_shoes.Text.Trim());//鞋型

                if (!string.IsNullOrWhiteSpace(this.starttime.Text))
                {
                    start_date = Convert.ToDateTime(this.starttime.Value).ToString("yyyy-MM-dd");
                    p.Add("start_date", start_date);
                }
                if (!string.IsNullOrWhiteSpace(this.endtime.Text))
                {
                    end_date = Convert.ToDateTime(this.endtime.Value).ToString("yyyy-MM-dd");
                    p.Add("end_date", end_date);
                }

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_Shipment_Notice",//类名
                                            "GetShipment_Notice_Main_Export",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            return dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //视图数据显示
                DataTable dts = GetShipment_Notice_Main_Export();
                if (dts.Rows.Count < 1)
                {
                    MessageBox.Show("No data export yet，Please check if it is done correctly");
                    return;
                }

                /* if (DT_EXCEL.Rows.Count < 1)
                 {
                     MessageBox.Show("数据为空，先搜索再做导出操作");
                     return;
                 }*/
                //for (int i = 0; i < dts.Rows.Count; i++)
                //{
                //    dts.Rows.RemoveAt(i);
                //}
                Dictionary<string, string> Execldic = new Dictionary<string, string>();
                //Execldic.Add("序号", "序号");
                Execldic.Add("SE_ID", "Sales order number");//销售订单号
                Execldic.Add("SHOE_NAME", "Shoe type");//鞋型 
                Execldic.Add("SE_CUSTID", "Client number");//客户号 
                Execldic.Add("WORKORDER_NO", "Injunction");//制令
                Execldic.Add("PO_NO", "Customer Order Number");//客户订单号
                Execldic.Add("PROD_NO", "ART_NO");
                Execldic.Add("SE_QTY", "Quantity");//数量
                Execldic.Add("BOXS_NUMBERS", "Number of boxes");//箱数
                Execldic.Add("SHIPCOUNTRY_NAME", "Country");//国家
                Execldic.Add("CONTAINER_TRUCK", "Cabinet car");//柜车
                Execldic.Add("Lines_List", "group");//组别
                Execldic.Add("POSTING_DATE", "Shipping date");//出货日期
                Execldic.Add("TRAIN_NUMBER", "Train number");//车次
                Execldic.Add("CR_REQDATE", "CR_REQDATE");//出货日期 
                Execldic.Add("NST", "Shipment_Plan_Date");//车次 

                /*
                  m.DELIVERY_NO, -- 出货单号
                        m.SE_ID, -- 销售订单号
						r.name_t as shoe_name,
						o.se_custid, -- 客户号
						o.WORKORDER_NO, -- 制令
						m.PO_NO, -- 客户订单号
						b.prod_no, -- art
                        b.se_qty,
						m.BOXS_NUMBERS,-- 箱数
						o.SHIPCOUNTRY_NAME,-- 国家
						m.CONTAINER_TRUCK,-- 柜车
                       (select listagg(distinct from_line,',') from mms_finishedtrackin_list where se_id=m.SE_ID) as 组别,
						(select max(posting_date) from bmd_se_shipment_m where se_id=b.se_qty) posting_date,
						m.TRAIN_NUMBER -- 车次
                 */

                ExeclHelper.ExportToTrueExcel(dts, Execldic, "Shipping Notice");//出货通知
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
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

        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="PageIndex">页索引,注意：从1开始</param>
        /// <param name="PageSize">每页大小</param>
        /// <returns>分好页的DataTable数据</returns>              第1页        每页10条
        public static DataTable GetPagedTable(DataTable dt, int PageIndex, int PageSize)
        {
            if (PageIndex == 0) { return dt; }
            DataTable newdt = dt.Copy();
            newdt.Clear();
            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;

            if (rowbegin >= dt.Rows.Count)
            { return newdt; }

            if (rowend > dt.Rows.Count)
            { rowend = dt.Rows.Count; }
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }
            return newdt;
        }

        /// <summary>
        /// 返回分页的页数
        /// </summary>
        /// <param name="count">总条数</param>
        /// <param name="pageye">每页显示多少条</param>
        /// <returns>如果 结尾为0：则返回1</returns>
        public static int PageCount(int count, int pageye)
        {
            int page = 0;
            int sesepage = pageye;
            if (count % sesepage == 0) { page = count / sesepage; }
            else { page = (count / sesepage) + 1; }
            if (page == 0) { page += 1; }
            return page;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.Columns[e.ColumnIndex].Name == "aql_result")
            {
                //string Art = dataGridView1.Rows[e.RowIndex].Cells["ART_NO"].Value.ToString();
                //string Model_Name = dataGridView1.Rows[e.RowIndex].Cells["鞋型"].Value.ToString();
                //Dictionary<string, object> p = new Dictionary<string, object>();
                //p.Add("Art", Art);
                //p.Add("Model_Name", Model_Name);

                string PO = dataGridView1.Rows[e.RowIndex].Cells["客户订单号"].Value.ToString();

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("PO", PO);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_Shipment_Notice",
                                            "Get_AQL_Result",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
               ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    MessageBox.Show("No test result found!");
                    return;
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData); 
                string frmName = $@"F_AQL_Aqlreport_New_{dic["task_no"]}";
                var findFrm = Application.OpenForms[frmName];
                if (findFrm == null)
                {
                    F_AQL_Aqlreport_New a = new F_AQL_Aqlreport_New(dic, Program.Client);
                    a.Name = frmName;
                    a.Show();
                }
                else
                {
                    findFrm.Activate();
                }
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "bonding")
            {
                string PO = dataGridView1.Rows[e.RowIndex].Cells["客户订单号"].Value.ToString();

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("PO", PO);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_Shipment_Notice",
                                            "GetEx_LookResult",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    MessageBox.Show("No test result found!");
                    return;
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                //var info = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(dic["task_no"].ToString());
                if (string.IsNullOrWhiteSpace(dic["task_no"].ToString()))
                {
                    MessageBox.Show("No test result found!");
                    return;
                }
                using (F_QCM_Ex_LookResult_New frm = new F_QCM_Ex_LookResult_New(dic["task_no"].ToString(), Program.Client))
                {
                    frm.ShowDialog();
                }
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "a01report")
            {
                string PO = dataGridView1.Rows[e.RowIndex].Cells["客户订单号"].Value.ToString();
                string ART_NO = dataGridView1.Rows[e.RowIndex].Cells["ART_NO"].Value.ToString();
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("po", PO);
                p.Add("art", ART_NO);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_Checkthedata1",//类名
                                            "Get_ArtFileInfo",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                if (ret.IsSuccess)
                {
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    string file_name = dt.Rows[0]["FILE_NAME"].ToString();
                    string file_url = Program.Client.PicUrl + dt.Rows[0]["file_url"].ToString();
                    ShowFileHelper.ShowFile(file_url, file_name);
                }
                else
                {
                    MessageBox.Show("No test result found!");
                }
              
            }
         }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var cell1 = row.Cells[15];
                var cell2 = row.Cells[17];
                DateTime d1 = cell1.Value.ToDate();
                DateTime d2 = cell2.Value.ToDate();
                int i = DateTime.Compare(d1, d2);
                if(i > 0)
                {
                    cell2.Style.BackColor = Color.Green;
                }
                else if(i < 0)
                {
                    cell2.Style.BackColor = Color.Red;
                }
                else
                {
                    cell2.Style.BackColor = Color.Yellow;
                }
            }
        }
    }
}
