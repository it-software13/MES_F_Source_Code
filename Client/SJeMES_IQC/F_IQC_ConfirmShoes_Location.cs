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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_IQC
{
    public partial class F_IQC_ConfirmShoes_Location : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_IQC_ConfirmShoes_Location()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
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
        /// 查询-确认鞋-库位维护-主页
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetConfirmShoesLocation_Main(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_ConfirmShoes",//类名
                                            "GetConfirmShoesLocation_Main",//方法名
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
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["xh"].Value = i+1;
                        dgvr.Cells["id"].Value = dr["id"].ToString();
                        dgvr.Cells["STOCK_CODE"].Value = dr["STOCK_CODE"].ToString();
                        dgvr.Cells["warehouse_name"].Value = dr["WAREHOUSE_NAME"].ToString();
                        dgvr.Cells["STOCK_NAME"].Value = dr["STOCK_NAME"].ToString();
                        dgvr.Cells["remark"].Value = dr["remark"].ToString();
                        dgvr.Cells["ref_standard"].Value = dr["ref_standard"].ToString();

                        if (dr["ref_standard"].ToString()=="0")
                            dgvr.Cells["ref_standard"].Value = "入库时间";
                        else if (dr["ref_standard"].ToString() == "1")
                            dgvr.Cells["ref_standard"].Value = "量产时间";
                        else
                            dgvr.Cells["ref_standard"].Value = "";

                        dgvr.Cells["expire_day"].Value = dr["expire_day"].ToString();
                        dgvr.Cells["remind_day"].Value = dr["remind_day"].ToString();
                        i++;
                    }
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

        private void F_IQC_ConfirmShoes_Location_Load(object sender, EventArgs e)
        {
            //只要加载一次委托 
            pageControl1.BindPageEvent += GetConfirmShoesLocation_Main;
            LoadPage();
            this.dataGridView1.ClearSelection();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "cz")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["cz"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }

                    if (cell.CurrentItem.Equals("DELETE"))
                    {
                        //DialogResult dr = MessageBox.Show("确认要删除吗!", "删除鞋型品质状况", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        string sid = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();//库位id
                        DeleteConfirmShoesLocation(sid);
                    }
                    else if (cell.CurrentItem.Equals("EDIT"))
                    {
                        string sid = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();//库位id
                        using (F_IQC_ConfirmShoes_Location_Add c=new F_IQC_ConfirmShoes_Location_Add(sid))
                        {
                            c.ShowDialog();
                        }
                        LoadPage();
                    }
                }
            }
        }

        /// <summary>
        /// 删除-确认鞋-仓库维护
        /// </summary>
        public void DeleteConfirmShoesLocation(string sid)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("id", sid);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJeMES_IQC", "SJeMES_IQC.IQC_ConfirmShoes", "DeleteConfirmShoesLocation", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("删除成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    LoadPage();
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (F_IQC_ConfirmShoes_Location_Add r=new F_IQC_ConfirmShoes_Location_Add())
            {
                r.ShowDialog();
            }
            LoadPage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //视图数据显示
                DataTable dts = new DataTable();
                //if (dts.Rows.Count < 1)
                //{
                //    MessageBox.Show("暂无数据导出，请检查是否操作正确");
                //    return;
                //}
                /* if (DT_EXCEL.Rows.Count < 1)
                 {
                     MessageBox.Show("数据为空，先搜索再做导出操作");
                     return;
                 }*/
                //for (int i = 0; i < dts.Rows.Count; i++)
                //{
                //    dts.Rows.RemoveAt(i);
                //}
                dts.Columns.Add("STOCK_CODE");
                dts.Columns.Add("STOCK_NAME");
                dts.Columns.Add("WAREHOUSE_CODE");
                dts.Columns.Add("REMARK");
                dts.Columns.Add("REF_STANDARD");
                dts.Columns.Add("EXPIRE_DAY");
                dts.Columns.Add("REMIND_DAY");
                Dictionary<string, string> Execldic = new Dictionary<string, string>();
                Execldic.Add("STOCK_CODE", "库位代号");
                Execldic.Add("STOCK_NAME", "库位名称");
                Execldic.Add("WAREHOUSE_CODE", "仓库代号");
                Execldic.Add("REMARK", "备注");
                Execldic.Add("REF_STANDARD", "参照标准");
                Execldic.Add("EXPIRE_DAY", "到期时间");
                Execldic.Add("REMIND_DAY", "提醒时间");

                ExeclHelper.ExportToTrueExcel(dts, Execldic, "确认鞋库位维护导入模板");
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("导入前要确保到期时间、提醒时间为整数类型，参照标准:0:入库时间，1:量产时间，否则导入失败", "操作提示！！！", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //创建文件弹出选择窗口（包括文件名）对象
                    OpenFileDialog ofd = new OpenFileDialog();
                    //判断选择的路径
                    string path = string.Empty;
                    ofd.Title = "请选择文件";
                    ofd.Filter = "EXECL|*.xlsx;*.xls";
                    string SafeFileName = "";
                    string filePath = "";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        SafeFileName = Path.GetExtension(ofd.FileName);
                        filePath = ofd.FileName;
                    }
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        if (SafeFileName != ".xlsx" && SafeFileName != ".xls")
                        {
                            MessageBox.Show("文件类型错误,请选择(.xlsx,.xls)类型文件");
                            return;
                        }
                        DataTable dt = SJeMES_Framework.Common.NPOIHelper.ExcelToTable(filePath);
                        //|| dt.Columns[dt.Columns.Count - 1].ColumnName != "机台"
                        if (dt.Columns.Count != 7)
                        {
                            MessageBox.Show("导入模板错误,请查阅");
                            return;
                        }

                        //不能为空
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["库位代号"] == null || string.IsNullOrWhiteSpace(dt.Rows[i]["库位代号"].ToString()))
                            {
                                MessageBox.Show($@"库位代号不能为空!第{i + 1}行!");
                                return;
                            }
                            if (dt.Rows[i]["库位名称"] == null || string.IsNullOrWhiteSpace(dt.Rows[i]["库位名称"].ToString()))
                            {
                                MessageBox.Show($@"库位名称不能为空!第{i + 1}行!");
                                return;
                            }
                            if (dt.Rows[i]["仓库代号"] == null || string.IsNullOrWhiteSpace(dt.Rows[i]["仓库代号"].ToString()))
                            {
                                MessageBox.Show($@"仓库代号不能为空!第{i + 1}行!");
                                return;
                            }
                            if (dt.Rows[i]["到期时间"] == null || string.IsNullOrWhiteSpace(dt.Rows[i]["到期时间"].ToString()))
                            {
                                MessageBox.Show($@"到期时间不能为空!第{i + 1}行!");
                                return;
                            }
                            if (dt.Rows[i]["提醒时间"] == null || string.IsNullOrWhiteSpace(dt.Rows[i]["提醒时间"].ToString()))
                            {
                                MessageBox.Show($@"提醒时间不能为空!第{i + 1}行!");
                                return;
                            }
                        }

                        if (dt != null)
                        {
                            SJeMES_Control_Library.Forms.FrmImport frm = new SJeMES_Control_Library.Forms.FrmImport(dt);
                            frm.StartPosition = FormStartPosition.CenterScreen;
                            frm.ShowDialog();
                            bool is_sure = frm.is_sure;
                            if (is_sure)
                            {
                                //请求api的数据展示
                                Dictionary<string, object> p = new Dictionary<string, object>();
                                p.Add("SOURCE", dt);
                                p.Add("import_type", 9);//客户投诉导入
                                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                            Program.Client.APIURL,
                                                            "SJ_QCMAPI",//类库名
                                                            "SJ_QCMAPI.BASE",//类名
                                                            "ImportData",//方法名
                                                            Program.Client.UserToken,//token
                                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                if (ret.IsSuccess)
                                {
                                    MessageBox.Show("导入成功");
                                    LoadPage();
                                }
                                else
                                {
                                    MessageBox.Show(ret.ErrMsg);
                                }
                            }
                        }
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
