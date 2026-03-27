using Newtonsoft.Json;
using SJeMES_AQL.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SJeMES_AQL.Common.Enum;

namespace SJeMES_AQL.AQL_FrmBase
{
    public partial class F_AQL_ShowFrm2 : UserControl
    {
        private Dictionary<string, object> dic_list;
        public F_AQL_ShowFrm2(Dictionary<string, object> _dic_list)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            dic_list = _dic_list;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 12f, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
            GetView();
        }
        private void GetView()
        {
            try
            {

                Dictionary<string, object> data = (Dictionary<string, object>)(dic_list.ContainsKey(BotAtype.typekey2) ? dic_list[BotAtype.typekey2] : null);
                List<Dictionary<string, object>> list = (List<Dictionary<string, object>>)(data.ContainsKey("inputdata") ? data["inputdata"] : null);
                textBox1.Text = data["remark"].ToString();
                dataGridView1.Rows.Clear();
                if (list.Count > 0)
                {
                    int i = 0;
                    foreach (Dictionary<string, object> dr in list)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["Column1"].Value = dr["name"].ToString();
                        dgvr.Cells["ctype"].Value = dr["type"].ToString();
                        dgvr.Cells["btn_passflag"].Value = "0";
                        dgvr.Cells["btn_failflag"].Value = "0";
                        dgvr.Cells["naflag"].Value = "0";
                        switch (dr["status"].ToString())
                        {
                            //0：未核对 1：已核对 2：N/A
                            case "1":
                                dgvr.Cells["btn_pass"].Style.BackColor = Color.PaleGreen;//绿色
                                dgvr.Cells["btn_passflag"].Value = "1";
                                break;
                            case "0":
                                dgvr.Cells["btn_fail"].Style.BackColor = Color.Red;//红色
                                dgvr.Cells["btn_failflag"].Value = "1";
                                break;
                            case "2":
                                dgvr.Cells["na"].Style.BackColor = Color.DarkGray;//灰色
                                dgvr.Cells["naflag"].Value = "1";
                                break;
                        }
                        i++;
                    }
                }
                dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        public void getdata()
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                Dictionary<string, object> data = (Dictionary<string, object>)(dic_list.ContainsKey(BotAtype.typekey2) ? dic_list[BotAtype.typekey2] : null);
                data["remark"] = textBox1.Text;
            }
            DataTable dt = Auxiliary.GetDatagridviewDatable(dataGridView1);
            if (dt.Rows.Count > 0)
            {
                Dictionary<string, object> data = (Dictionary<string, object>)(dic_list.ContainsKey(BotAtype.typekey2) ? dic_list[BotAtype.typekey2] : null);
                data["returndata"] = JsonConvert.SerializeObject(dt);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    switch (dataGridView1.Columns[e.ColumnIndex].Name)
                    {
                        case "btn_pass":

                            dataGridView1.CurrentRow.Cells["na"].Style.BackColor = Color.White;
                            dataGridView1.CurrentRow.Cells["naflag"].Value = "0";
                            dataGridView1.CurrentRow.Cells["btn_fail"].Style.BackColor = Color.White;
                            dataGridView1.CurrentRow.Cells["btn_failflag"].Value = "0";

                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].DefaultCellStyle.SelectionBackColor = Color.PaleGreen;
                            dataGridView1.CurrentRow.Cells["btn_pass"].Style.BackColor = Color.PaleGreen;
                            dataGridView1.CurrentRow.Cells["btn_passflag"].Value = "1";
                            break;
                        case "btn_fail":
                            dataGridView1.CurrentRow.Cells["na"].Style.BackColor = Color.White;
                            dataGridView1.CurrentRow.Cells["btn_passflag"].Value = "0";
                            dataGridView1.CurrentRow.Cells["btn_pass"].Style.BackColor = Color.White;
                            dataGridView1.CurrentRow.Cells["naflag"].Value = "0";

                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].DefaultCellStyle.SelectionBackColor = Color.Red;
                            dataGridView1.CurrentRow.Cells["btn_fail"].Style.BackColor = Color.Red;
                            dataGridView1.CurrentRow.Cells["btn_failflag"].Value = "1";
                            break;
                        case "na":

                            dataGridView1.CurrentRow.Cells["btn_pass"].Style.BackColor = Color.White;
                            dataGridView1.CurrentRow.Cells["btn_passflag"].Value = "0";
                            dataGridView1.CurrentRow.Cells["btn_fail"].Style.BackColor = Color.White;
                            dataGridView1.CurrentRow.Cells["btn_failflag"].Value = "0";

                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].DefaultCellStyle.SelectionBackColor = Color.DarkGray;
                            dataGridView1.CurrentRow.Cells["na"].Style.BackColor = Color.DarkGray;
                            dataGridView1.CurrentRow.Cells["naflag"].Value = "1";
                            break;
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

      
    }
}
