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

namespace SJeMES_AQL
{
    public partial class F_AQL_ConfirmShoes_Warehouse : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string MODULE_TYPE = string.Empty;//模块类型
        public F_AQL_ConfirmShoes_Warehouse(string _MODULE_TYPE)
        {
            InitializeComponent();
            MODULE_TYPE = _MODULE_TYPE;
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
        /// 查询-确认鞋-仓库维护-主页-aql
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetConfirmShoesWarehouse_Main(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("keycode", textBox1.Text.Trim());
                p.Add("MODULE_TYPE", MODULE_TYPE);
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_ConfirmShoes",//类名
                                            "GetConfirmShoesWarehouse_Main",//方法名
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
                        dgvr.Cells["id"].Value = dr["id"].ToString();
                        dgvr.Cells["warehouse_code"].Value = dr["WAREHOUSE_CODE"].ToString();
                        dgvr.Cells["warehouse_name"].Value = dr["WAREHOUSE_NAME"].ToString();
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

        private void F_AQL_ConfirmShoes_Warehouse_Load(object sender, EventArgs e)
        {
            //只要加载一次委托 
            pageControl1.BindPageEvent += GetConfirmShoesWarehouse_Main;
            LoadPage();
            this.dataGridView1.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (F_AQL_ConfirmShoes_Warehouse_Add i = new F_AQL_ConfirmShoes_Warehouse_Add(MODULE_TYPE))
            {
                i.ShowDialog();
            }
            LoadPage();
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
                        string wid = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();//仓库id
                        DeleteConfirmShoesWarehouse(wid);
                    }
                }
            }
        }

        /// <summary>
        /// 删除-确认鞋-仓库维护-aql
        /// </summary>
        public void DeleteConfirmShoesWarehouse(string wid)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("id", wid);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_ConfirmShoes", "DeleteConfirmShoesWarehouse", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
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

        private void button3_Click(object sender, EventArgs e)
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
                dts.Columns.Add("WAREHOUSE_CODE");
                dts.Columns.Add("WAREHOUSE_NAME");
                Dictionary<string, string> Execldic = new Dictionary<string, string>();
                Execldic.Add("WAREHOUSE_CODE", "warehouse code");//仓库代号
                Execldic.Add("WAREHOUSE_NAME", "warehouse name");//仓库名称

                ExeclHelper.ExportToTrueExcel(dts, Execldic, "Warehouse maintenance import template");
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Before importing, make sure that the warehouse code and warehouse name cannot be empty, otherwise the import will fail", "Operation prompt! ! !", MessageBoxButtons.YesNo) == DialogResult.Yes) //导入前要确保仓库代号、仓库名称不能为空，否则导入失败", "操作提示！！！
                {
                    //创建文件弹出选择窗口（包括文件名）对象
                    OpenFileDialog ofd = new OpenFileDialog();
                    //判断选择的路径
                    string path = string.Empty;
                    ofd.Title = "Please select a file";
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
                            MessageBox.Show("Wrong file type, please select (.xlsx,.xls) type file");
                            return;
                        }
                        DataTable dt = SJeMES_Framework.Common.NPOIHelper.ExcelToTable(filePath);
                        //|| dt.Columns[dt.Columns.Count - 1].ColumnName != "机台"
                        if (dt.Columns.Count != 2)
                        {
                            MessageBox.Show("Import template error, please refer to");
                            return;
                        }

                        dt.Columns.Add("MODULE_TYPE");
                        //不能为空
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["仓库代号"] == null || string.IsNullOrWhiteSpace(dt.Rows[i]["仓库代号"].ToString()))
                            {
                                MessageBox.Show($@"仓库代号不能为空!第{i + 1}行!");
                                return;
                            }
                            if (dt.Rows[i]["仓库名称"] == null || string.IsNullOrWhiteSpace(dt.Rows[i]["仓库名称"].ToString()))
                            {
                                MessageBox.Show($@"仓库名称不能为空!第{i + 1}行!");
                                return;
                            }
                            dt.Rows[i]["MODULE_TYPE"] = MODULE_TYPE;
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
                                p.Add("import_type", 11);//出货仓库维护导入
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
