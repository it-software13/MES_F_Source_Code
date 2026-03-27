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
    public partial class F_QCM_VampQualityAudit_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_VampQualityAudit_Main()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.dataGridView1.ReadOnly = false;
        }

        private void F_QCM_VampQualityAudit_Main_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            Selectbdm_vamp_quality_m();
        }

        //鞋面品质标准查询
        public void Selectbdm_vamp_quality_m()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.VampQualityAudit", "Selectbdm_vamp_quality_m", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                this.labzs.Text = dic["zs"].ToString()==""?"0": dic["zs"].ToString();
                if (dataGridView1.Rows.Count >= 0)
                {
                    dataGridView1.Rows.Clear();
                }
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["QUALITY_TYPE_CODE"].Value = dr["QUALITY_TYPE_CODE"].ToString();
                        dgvr.Cells["QUALITY_TYPE_NAME"].Value = dr["QUALITY_TYPE_NAME"].ToString();
                        dgvr.Cells["QUALITY_ITEM_CODE"].Value = dr["QUALITY_ITEM_CODE"].ToString();
                        dgvr.Cells["QUALITY_ITEM_NAME"].Value = dr["QUALITY_ITEM_NAME"].ToString();
                        dgvr.Cells["SOCRE"].Value = dr["BASE_SOCRE"].ToString();
                        dgvr.Cells["TYPE"].Value = dr["TYPE"].ToString();
                        if (dr["TYPE"].ToString()=="0")
                        {
                            this.dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                        }
                        i++;
                    }
                }
                this.dataGridView1.ClearSelection();

                GenClass.AutoSizeColumn(dataGridView1);
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            //this.dataGridView1.Rows[index]
            if (this.dataGridView1.Rows[index].Cells["TYPE"].Value.ToString()=="0")
            {
                this.dataGridView1.Rows[index].ReadOnly = true;
            }
            else
            {
                //this.dataGridView1.ReadOnly = false;
                this.dataGridView1.Columns["QUALITY_TYPE_NAME"].ReadOnly = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            // 列强制转换
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                DataColumn dc = new DataColumn(dataGridView1.Columns[i].Name.ToString());
                dt.Columns.Add(dc);
            }

            // 循环行
            for (int j = 0; j < dataGridView1.Rows.Count; j++)
            {
                DataRow dr = dt.NewRow();
                for (int k = 0; k < dataGridView1.Columns.Count; k++)
                {
                    dr[k] = Convert.ToString(dataGridView1.Rows[j].Cells[k].Value);
                }
                dt.Rows.Add(dr);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    double var1 = Convert.ToDouble(dt.Rows[i]["SOCRE"]);
                }
                catch
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "请输入数字!");
                    return;
                }
            }
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("bdm_vamp_quality_m", dt);
            try
            {
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                              Program.Client.APIURL,
                                              "SJ_QCMAPI",//类库名
                                              "SJ_QCMAPI.VampQualityAudit",//类名
                                              "Insertbdm_vamp_quality_m",//方法名
                                              Program.Client.UserToken,//token
                                              Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("保存成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                Selectbdm_vamp_quality_m();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public DataGridViewTextBoxEditingControl CellEdit = null;
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.dataGridView1.CurrentCellAddress.X == 5)//获取当前处于活动状态的单元格索引
            {
                CellEdit = (DataGridViewTextBoxEditingControl)e.Control;
                CellEdit.SelectAll();
                CellEdit.KeyPress += Cells_KeyPress; //绑定事件
            }
        }

        private void Cells_KeyPress(object sender, KeyPressEventArgs e) //自定义事件
        {
            if ((this.dataGridView1.CurrentCellAddress.X == 5))//获取当前处于活动状态的单元格索引
            {
                if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
                if (e.KeyChar == '\b') e.Handled = false;
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
