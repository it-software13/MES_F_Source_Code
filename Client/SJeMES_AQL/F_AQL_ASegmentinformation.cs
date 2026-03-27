using MaterialSkin;
using MaterialSkin.Controls;
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

namespace SJeMES_AQL
{
    public partial class F_AQL_ASegmentinformation : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private Dictionary<string, object> dic2 = new Dictionary<string, object>();
        public F_AQL_ASegmentinformation(Dictionary<string, object> _dic)
        {
            InitializeComponent();
            dic2 = _dic;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            #region Dgv样式初始化

            //单元格换行
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dataGridView2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dataGridView3.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dataGridView4.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView4.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            //列标题居中
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView3.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView4.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //内容居中

            dataGridView3.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            #endregion
        }
        private void F_AQL_ASegmentinformation_Load(object sender, EventArgs e)
        {
           string retdata=  GridViewList("GetDfdDatas");
            if (dataGridView1.Rows.Count < 1)
            {
                retdata = GridViewList("GetDfdDatas");
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object>  dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["list"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["Columna1"].Value = dr["art_no"].ToString();
                        dgvr.Cells["Columna2"].Value = dr["workshop_section"].ToString();
                        dgvr.Cells["Columna3"].Value = dr["start_code"].ToString();
                        dgvr.Cells["Columna4"].Value = dr["end_code"].ToString();
                        dgvr.Cells["Columna5"].Value = dr["item_no"].ToString();
                        dgvr.Cells["Columna6"].Value = dr["item_name"].ToString();
                        dgvr.Cells["Columna7"].Value = dr["effective_date"].ToString();
                        dgvr.Cells["Columna8"].Value = dr["expiration_date"].ToString();
                        i++;
                    }
                }
                dataGridView1.ClearSelection();
                //列宽部分居中
                //ChangedgvStyle(dataGridView1);
            }
        }

        public static void ChangedgvStyle(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    if (j < 4)
                    {
                        dgv.Rows[i].Cells[j].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
        }

        public static void ChangedgvStyleBydgv4(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    if (j < 2)
                    {
                        dgv.Rows[i].Cells[j].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
        }

        public string GridViewList(string type)
        {
            string retdata = string.Empty;
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                string putin_date = string.Empty;
                //键值对传值
                data.Add("po", dic2["po"].ToString());//po
                retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_Checkthedata1",//类名
                                            $"{type}",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));
               
               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            return retdata;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string retdata = string.Empty;
            ResultObject ret = null;
            DataTable dt = new DataTable();
            Dictionary<string, object> dic = null;
            switch (tabControl1.SelectedTab.Name)
            {
                
                case "tabPage1":
                    if (dataGridView1.Rows.Count < 1)
                    {
                        retdata=GridViewList("GetDfdDatas");
                        ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        if (!ret.IsSuccess)
                        {
                            throw new Exception(ret.ErrMsg);
                        }
                        dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                        dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["list"].ToString());
                        dataGridView1.Rows.Clear();
                        if (dt.Rows.Count > 0)
                        {
                            int i = 0;
                            foreach (DataRow dr in dt.Rows)
                            {
                                dataGridView1.Rows.Add();
                                DataGridViewRow dgvr = dataGridView1.Rows[i];
                                dgvr.Cells["Columna1"].Value = dr["art_no"].ToString();
                                dgvr.Cells["Columna2"].Value = dr["workshop_section"].ToString();
                                dgvr.Cells["Columna3"].Value = dr["start_code"].ToString();
                                dgvr.Cells["Columna4"].Value = dr["end_code"].ToString();
                                dgvr.Cells["Columna5"].Value = dr["item_no"].ToString();
                                dgvr.Cells["Columna6"].Value = dr["item_name"].ToString();
                                dgvr.Cells["Columna7"].Value = dr["effective_date"].ToString();
                                dgvr.Cells["Columna8"].Value = dr["expiration_date"].ToString();
                                i++;
                            }
                        }
                        dataGridView1.ClearSelection();
                        //列宽部分居中
                        //ChangedgvStyle(dataGridView1);
                    }
                    break;
                case "tabPage2":
                    if (dataGridView2.Rows.Count < 1)
                    {
                        retdata = GridViewList("GetDfdDatas1");
                        ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        if (!ret.IsSuccess)
                        {
                            throw new Exception(ret.ErrMsg);
                        }
                        dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                        //视图数据显示

                        dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["list1"].ToString());
                        dataGridView2.Rows.Clear();
                        if (dt.Rows.Count > 0)
                        {
                            int i = 0;
                            foreach (DataRow dr in dt.Rows)
                            {
                                dataGridView2.Rows.Add();
                                DataGridViewRow dgvr = dataGridView2.Rows[i];
                                dgvr.Cells["Columnb1"].Value = dr["art_no"].ToString();
                                dgvr.Cells["Columnb2"].Value = dr["workshop_section"].ToString();
                                dgvr.Cells["Columnb3"].Value = dr["start_code"].ToString();
                                dgvr.Cells["Columnb4"].Value = dr["end_code"].ToString();
                                dgvr.Cells["Columnb5"].Value = dr["item_no"].ToString();
                                dgvr.Cells["Columnb6"].Value = dr["item_name"].ToString();
                                dgvr.Cells["Columnb7"].Value = dr["effective_date"].ToString();
                                dgvr.Cells["Columnb8"].Value = dr["expiration_date"].ToString();
                                i++;
                            }
                        }
                        dataGridView2.ClearSelection();
                        //列宽部分居中
                        //ChangedgvStyle(dataGridView2);
                    }
                    break;
                case "tabPage3":
                    if (dataGridView3.Rows.Count < 1)
                    {
                        retdata = GridViewList("GetDfdDatas2");
                        ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        if (!ret.IsSuccess)
                        {
                            throw new Exception(ret.ErrMsg);
                        }
                        dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                        //视图数据显示

                        dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["list2"].ToString());
                        dataGridView3.Rows.Clear();
                        if (dt.Rows.Count > 0)
                        {
                            int i = 0;
                            foreach (DataRow dr in dt.Rows)
                            {
                                dataGridView3.Rows.Add();
                                DataGridViewRow dgvr = dataGridView3.Rows[i];
                                dgvr.Cells["Columnc1"].Value = dr["art_no"].ToString();
                                dgvr.Cells["Columnc2"].Value = dr["size"].ToString();
                                dgvr.Cells["Columnc3"].Value = dr["NTGEW"].ToString();
                                dgvr.Cells["Columnc4"].Value = dr["heel_height"].ToString();
                                dgvr.Cells["Columnc5"].Value = dr["inner_waist_height"].ToString();
                                dgvr.Cells["Columnc6"].Value = dr["outer_waist_height"].ToString();
                                dgvr.Cells["Columnc7"].Value = dr["toe_spring"].ToString();
                                dgvr.Cells["Columnc8"].Value = dr["heel_camber"].ToString();
                                i++;
                            }
                        }
                        dataGridView3.ClearSelection();
                    }
                    break;
                case "tabPage4":
                    if (dataGridView4.Rows.Count < 1)
                    {
                        Application.DoEvents();//转让控制权
                        retdata = GridViewList("GetDfdDatas3");
                        ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        if (!ret.IsSuccess)
                        {
                            throw new Exception(ret.ErrMsg);
                        }
                        dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                        //视图数据显示

                        dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["list3"].ToString());
                        dataGridView4.Rows.Clear();
                        if (dt.Rows.Count > 0)
                        {
                            int i = 0;
                            foreach (DataRow dr in dt.Rows)
                            {
                                dataGridView4.Rows.Add();
                                DataGridViewRow dgvr = dataGridView4.Rows[i];
                                dgvr.Cells["Columnd1"].Value = dr["art_no"].ToString();
                                dgvr.Cells["Columnd2"].Value = dr["size"].ToString();
                                dgvr.Cells["Columnd3"].Value = dr["part_name"].ToString();
                                dgvr.Cells["Columnd4"].Value = dr["process_desc"].ToString();
                                i++;
                                Application.DoEvents();//转让控制权
                            }
                        }
                        dataGridView4.ClearSelection();
                        //ChangedgvStyleBydgv4(dataGridView4);
                    }
                    break;

            }
        }
    }
}
