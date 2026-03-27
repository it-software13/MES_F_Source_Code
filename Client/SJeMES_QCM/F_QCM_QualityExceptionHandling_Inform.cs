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
    public partial class F_QCM_QualityExceptionHandling_Inform : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_QualityExceptionHandling_Inform()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
        Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        public void FormLoad()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        private void F_QCM_QualityExceptionHandling_Inform_Load(object sender, EventArgs e)
        {
            pageControl1.BindPageEvent += GetDataList;
            FormLoad();
        }

        /// <summary>
        /// 视图展示
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

                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.AbnormalReport",//类名
                                            "SearchHR001CS",//方法名
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
                        dgvr.Cells["STAFF_NO"].Value = dr["STAFF_NO"].ToString();
                        dgvr.Cells["STAFF_NAME"].Value = dr["STAFF_NAME"].ToString();
                        dgvr.Cells["STAFF_DEPARTMENT"].Value = dr["STAFF_DEPARTMENT"].ToString();
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string staff_name = "";
            if ((bool)dataGridView1.Rows[e.RowIndex].Cells[0].EditedFormattedValue == true)
            {
                staff_name = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                TextBox txt = new TextBox();
                txt.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                txt.Text = staff_name;
                txt.Name = staff_name;
                txt.ReadOnly = true;
                this.flowLayoutPanel1.Controls.Add(txt);
            }
            
            
            if ((bool)dataGridView1.Rows[e.RowIndex].Cells[0].EditedFormattedValue ==false)
            {
                foreach (Control C in this.flowLayoutPanel1.Controls)
                {
                    TextBox c = (TextBox)C;
                    if (c.Text.Equals(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()))
                    {
                        this.flowLayoutPanel1.Controls.Remove(c);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.flowLayoutPanel1.Controls.Clear();
            Cancel_SelectBox();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("已通知");
            this.flowLayoutPanel1.Controls.Clear();
            Cancel_SelectBox();
        }

        //Clear the drop-down box to select
        private void Cancel_SelectBox()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value = true)))
                {
                    dataGridView1.Rows[i].Cells[0].Value = false;
                }
                else
                    continue;
            }
        }
    }
}
