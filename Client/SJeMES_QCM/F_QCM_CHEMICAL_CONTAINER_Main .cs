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
    public partial class F_QCM_ComplianceMangement_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_ComplianceMangement_Main()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
         Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        private void F_QCM_CHEMICAL_CONTAINER_Main_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            pageControl1.BindPageEvent += GetDataList;
            //GetDataList();
            FormLoad();
            timer1.Start();
            /*this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;*/
        }
        public void FormLoad()
        {

            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
        private void btn_Select_Click(object sender, EventArgs e)
        {
            FormLoad();
        }
        /// <summary>
        /// 搜索及视图展示（化学品容器管理看板视图展示）
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("CONTAINER_NO", txt_CONTAINER_NO.Text.Trim());
                p.Add("CHEMICAL_NAME", txt_CHEMICAL_NAME.Text.Trim().ToString());
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ChemicalcontainermBase",//类名
                                            "ChemicalcontainermKbList",//方法名
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
                        dgvr.Cells["CONTAINER_NO"].Value = dr["CONTAINER_NO"].ToString();
                        dgvr.Cells["CHEMICAL_NAME"].Value = dr["CHEMICAL_NAME"].ToString();
                        dgvr.Cells["GLUE_TIME"].Value = dr["GLUE_TIME"].ToString();
                        dgvr.Cells["EFFECTIVE_TIME"].Value = dr["EFFECTIVE_TIME"].ToString();

                        dgvr.Cells["EXPIRATION_TIME"].Value = dr["EXPIRATION_TIME"].ToString();
                        if (!string.IsNullOrEmpty(dr["EXPIRATION_TIME"].ToString()))
                        {
                            DateTime dd = DateTime.Now;
                            int Num = DateTime.Compare(dd, Convert.ToDateTime(dr["EXPIRATION_TIME"].ToString()));
                            int Num1 = DateTime.Compare(dd.AddMinutes(30), Convert.ToDateTime(dr["EXPIRATION_TIME"].ToString()));
                            if (Num > 0)
                            {
                                dataGridView1.Rows[i].Cells["EXPIRATION_TIME"].Style.BackColor = Color.Red;
                            }
                            if (dd<Convert.ToDateTime(dr["EXPIRATION_TIME"].ToString()))
                            {
                                if (Convert.ToDateTime(dr["EXPIRATION_TIME"].ToString()) < dd.AddMinutes(30))
                                {
                                    dataGridView1.Rows[i].Cells["EXPIRATION_TIME"].Style.BackColor = Color.Yellow;
                                }
                            }
                            if (dd.AddMinutes(30) < Convert.ToDateTime(dr["EXPIRATION_TIME"].ToString()))
                            {
                                dataGridView1.Rows[i].Cells["EXPIRATION_TIME"].Style.BackColor = Color.Green;
                            }

                        }
                        i++;
                    }
                     //GenClass.AutoSizeColumn(dataGridView1);
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            FormLoad();
        }
    }
}
