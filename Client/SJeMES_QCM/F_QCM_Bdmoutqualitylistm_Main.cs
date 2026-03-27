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

namespace SJeMES_QCM
{
    public partial class F_QCM_Bdmoutqualitylistm_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_Bdmoutqualitylistm_Main()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
      Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void F_QCM_Bdmoutqualitylistm_Main_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            GetDataList();
        }
        /// <summary>
        /// 发外厂商品质体系审核标准视图展示
        /// </summary>
        public void GetDataList()
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.OutQuantityStandard",//类名
                                            "GetAllProjectList",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                if (string.IsNullOrEmpty(ret["IsSuccess"].ToString()))
                {
                    throw new Exception(ret["ErrMsg"].ToString());
                }

                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret["RetData1"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["ID"].Value = dr["ID"].ToString();
                        dgvr.Cells["PROJECT"].Value = dr["PROJECT"].ToString();
                        dgvr.Cells["SCORE"].Value = dr["SCORE"].ToString();
                        i++;
                    }
                    GenClass.AutoSizeColumn(dataGridView1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private static bool IsNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^[0-9]\d*$");
            return reg.IsMatch(str);
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    List<BDM_OUT_QUALITY_LIST_M_ModityDto> listAdd = new List<BDM_OUT_QUALITY_LIST_M_ModityDto>();
                    BDM_OUT_QUALITY_LIST_M_ModityDto OUT_QUALITY_LIST_M = new BDM_OUT_QUALITY_LIST_M_ModityDto();
                    int j = 0;
                    foreach (DataGridViewRow dgr in dataGridView1.Rows)
                    {
                        if (j < dataGridView1.Rows.Count - 1)
                        {
                            if (dgr.Cells["ID"].Value != null)
                            {
                                OUT_QUALITY_LIST_M.ID = Convert.ToInt32(dgr.Cells["ID"].Value.ToString());
                            }
                            if (dgr.Cells["PROJECT"].Value != null)
                            {
                                if (!string.IsNullOrEmpty(dgr.Cells["PROJECT"].Value.ToString().Trim()))
                                {
                                    OUT_QUALITY_LIST_M.PROJECT = dgr.Cells["PROJECT"].Value.ToString();
                                }
                                else
                                {
                                    throw new Exception("评分项目不能为空！");
                                }
                               
                            }
                            else
                            {
                                throw new Exception("评分项目不能为空！");
                            }
                            if (dgr.Cells["SCORE"].Value != null  )
                            {
                                if(string.IsNullOrEmpty(dgr.Cells["SCORE"].Value.ToString().Trim())||
                                   IsNumeric(dgr.Cells["SCORE"].Value.ToString()) != false)
                                {
                                    OUT_QUALITY_LIST_M.SCORE = Convert.ToInt32(dgr.Cells["SCORE"].Value.ToString());
                                }
                                else
                                {
                                    throw new Exception("项目评分不能为空，且必须为正整数！");
                                }
                              
                            }
                            else
                            {
                                throw new Exception("项目评分不能为空，且必须为正整数！");
                            }
                            
                            listAdd.Add(OUT_QUALITY_LIST_M);
                            j++;
                            //清除原有的实体类值再带入
                            OUT_QUALITY_LIST_M = new BDM_OUT_QUALITY_LIST_M_ModityDto();
                        }


                    }
                    //匿名方法
                    var sdsd = new
                    {
                        project_datas = listAdd
                    };
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                   "SJ_QCMAPI",//类库名
                                                    "SJ_QCMAPI.OutQuantityStandard",//类名
                                                    "UpdateProjectList",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(sdsd));
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (!ret.IsSuccess)
                    {
                        MessageBox.Show(ret.ErrMsg);
                    }
                    else
                    {
                        MessageBox.Show("保存数据成功");
                        GetDataList();
                    }
                }
                else
                {
                    MessageBox.Show("请填写内容再保存");
                }
            }
            catch (Exception ex)
            {

                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        public class UpdateProjectListReq
        {
            /// <summary>
            /// 项目集合
            /// </summary>
            public List<BDM_OUT_QUALITY_LIST_M_ModityDto> project_datas { get; set; }
        }
        public class BDM_OUT_QUALITY_LIST_M_ModityDto
        {
            public int? ID { get; set; }
            /// <summary>
            /// 项目
            /// </summary>
            public string PROJECT { get; set; }
            /// <summary>
            /// 配分
            /// </summary>
            public int SCORE { get; set; }
            /// <summary>
            /// 实际得分
            /// </summary>
            public int REAL_SCORE { get; set; }
            /// <summary>
            /// 问题得分
            /// </summary>
            public string PROBLEM_POINT { get; set; }
            /// <summary>
            /// 备注
            /// </summary>
            public string REMARK { get; set; }
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
