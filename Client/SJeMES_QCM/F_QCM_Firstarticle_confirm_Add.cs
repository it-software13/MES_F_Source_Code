using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QCM
{
    public partial class F_QCM_Firstarticle_confirm_Add : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        /// <summary>
        /// 检验单号
        /// </summary>
        private string INSPECT_NOS = string.Empty;
        private string Type = string.Empty;
        public F_QCM_Firstarticle_confirm_Add(string INSPECT_NO,string type)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
     Program.SkinThemes, materialSkinManager, this);

            INSPECT_NOS = INSPECT_NO;
            Type = type;
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void F_QCM_Firstarticle_confirm_Add_Load(object sender, EventArgs e)
        {
            GetDataList();
            //首页点击查看首件确认明细显示
            if (!string.IsNullOrEmpty(INSPECT_NOS) && Type == "DETAIL")
            {
                btn_Out.Visible = false;
                btn_Add.Visible = false;
                txt_MODULE_NO.ReadOnly = true;
                txt_MACHINE.ReadOnly = true;
                txt_PHYSICAL_NAME.ReadOnly = true;
                txt_CODE_NUMBER.ReadOnly = true;
                stringtypeList();


            }
            if (!string.IsNullOrEmpty(INSPECT_NOS) && Type == "UPDATE")
            {
                stringtypeList();
            }
        }
        /// <summary>
        /// 首件确认单明细数据展示
        /// </summary>
        public void GetDataList()
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("inspect_no", INSPECT_NOS);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.FirstarticleconfirmmBase",//类名
                                            "FirstarticleconfirmmXView",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["inspect_no"].Value = dr["inspect_no"].ToString();
                        dgvr.Cells["inspect_seq"].Value = dr["inspect_seq"].ToString();
                        dgvr.Cells["defect_no"].Value = dr["defect_no"].ToString();
                        dgvr.Cells["defect_name"].Value = dr["defect_name"].ToString();
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 修改还有视图表头展示
        /// </summary>
        public void stringtypeList()
        {
            DataTable dt = DataList();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    txt_PO_ORDER.Text = item["PO_ORDER"].ToString();
                    txt_PROD_NO.Text = item["PROD_NO"].ToString();
                    txt_SHOE_NO.Text = item["SHOE_NO"].ToString();
                    txt_MODULE_NO.Text = item["MODULE_NO"].ToString();
                    txt_MACHINE.Text = item["MACHINE"].ToString();
                    txt_PHYSICAL_NAME.Text = item["PHYSICAL_NAME"].ToString();
                    txt_CODE_NUMBER.Text = item["CODE_NUMBER"].ToString();
                  
                    label_INSPECT_NO.Text ="检验单号："+item["INSPECT_NO"].ToString(); 
                }
            }
        }
        /// <summary>
        /// 首件确认单修改简单记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (NotNull.Trues(
                    txt_PO_ORDER.Text,
                    txt_PROD_NO.Text,
                    txt_SHOE_NO.Text,
                    txt_MODULE_NO.Text,
                    txt_MACHINE.Text,
                    txt_PHYSICAL_NAME.Text
                    )
                )
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("必填项不能为空！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }
                else
                {
                    //点击添加
                    if (Type == "Add")
                    {
                        Dictionary<string, object> p = new Dictionary<string, object>();
                        p.Add("PO_ORDER", txt_PO_ORDER.Text.Trim());
                        p.Add("PROD_NO", txt_PROD_NO.Text.Trim());
                        p.Add("SHOE_NO", txt_SHOE_NO.Text.Trim());
                        p.Add("MODULE_NO", txt_MODULE_NO.Text.Trim());
                        p.Add("MACHINE", txt_MACHINE.Text.Trim());
                        p.Add("PHYSICAL_NAME", txt_PHYSICAL_NAME.Text.Trim());
                        p.Add("CODE_NUMBER", txt_CODE_NUMBER.Text.Trim());
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                   "SJ_QCMAPI",//类库名
                                                    "SJ_QCMAPI.FirstarticleconfirmmBase",//类名
                                                    "FirstarticleconfirmmAdd",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        if (!ret.IsSuccess)
                        {
                            MessageBox.Show(ret.ErrMsg);
                        }
                        else
                        {
                            MessageBox.Show("保存数据成功");
                            Thread.Sleep(700);//当前线程睡一下
                            this.Close();
                        }
                    }
                    //点击修改
                    else if (!string.IsNullOrEmpty(INSPECT_NOS) && Type == "UPDATE")
                    {
                        DataTable dt = DataList();
                        if (dt.Rows.Count > 0)
                        {
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            p.Add("INSPECT_NO", INSPECT_NOS);
                            p.Add("PO_ORDER", txt_PO_ORDER.Text.Trim());
                            p.Add("PROD_NO", txt_PROD_NO.Text.Trim());
                            p.Add("SHOE_NO", txt_SHOE_NO.Text.Trim());
                            p.Add("MODULE_NO", txt_MODULE_NO.Text.Trim());
                            p.Add("MACHINE", txt_MACHINE.Text.Trim());
                            p.Add("PHYSICAL_NAME", txt_PHYSICAL_NAME.Text.Trim());
                            p.Add("CODE_NUMBER", txt_CODE_NUMBER.Text.Trim());
                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                        Program.Client.APIURL,
                                                       "SJ_QCMAPI",//类库名
                                                        "SJ_QCMAPI.FirstarticleconfirmmBase",//类名
                                                        "FirstarticleconfirmmUpdate",//方法名
                                                        Program.Client.UserToken,//token
                                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                            if (!ret.IsSuccess)
                            {
                                MessageBox.Show(ret.ErrMsg);
                            }
                            else
                            {
                                MessageBox.Show("修改数据成功");
                                Thread.Sleep(700);//当前线程睡一下
                                this.Close();
                            }
                        }
                        else
                        {

                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 首页查看或者修改数据展示
        /// </summary>
        public DataTable DataList()
        {
            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("INSPECT_NO", INSPECT_NOS);//ART

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.FirstarticleconfirmmBase",//类名
                                        "FirstarticleconfirmmViewByid",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            //视图数据显示
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            return dt;
        }
        /// <summary>
        /// PO单号弹框选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_PO_ORDER_Click(object sender, EventArgs e)
        {
            //当前窗体名称+"_"+当前方法名称
            string sql = "select a.MER_PO PO单号,b.PROD_NO ART编号,b.SHOE_NO 鞋型 from BDM_SE_ORDER_MASTER a left join  BDM_SE_ORDER_ITEM b on a.SE_ID=b.SE_ID";

            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                txt_PO_ORDER.Text = frmData.RetData.Rows[0]["PO单号"].ToString();
                txt_PROD_NO.Text = frmData.RetData.Rows[0]["ART编号"].ToString();
                txt_SHOE_NO.Text = frmData.RetData.Rows[0]["鞋型"].ToString(); 
            }
          
        }

        private void btn_Out_Click(object sender, EventArgs e)
        {
            this.Close();
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
                    if (name == "operation")
                    {
                        DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;

                        if (cell.CurrentItem == null)
                        {
                            return;
                        }
                        if (cell.CurrentItem.Equals("DETAIL"))//查看
                        {
                            
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                                                e.RowBounds.Location.Y,
                                                dgv.RowHeadersWidth - 4,
                                                e.RowBounds.Height);


            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                                    dgv.RowHeadersDefaultCellStyle.Font,
                                    rectangle,
                                    dgv.RowHeadersDefaultCellStyle.ForeColor,
                                    TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
    }
}
