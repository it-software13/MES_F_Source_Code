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
    public partial class F_AQL_ShowFrm5 : UserControl
    {
        private Dictionary<string, object> dic_list;
        public F_AQL_ShowFrm5(Dictionary<string, object> _dic_list)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            dic_list = _dic_list;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 12f, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 12f, FontStyle.Bold);
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.White;
            GetView();
        }
        private void GetView()
        {
            try
            { 
                Dictionary<string, object> data = (Dictionary<string, object>)(dic_list.ContainsKey(BotAtype.typekey5) ? dic_list[BotAtype.typekey5] : null);
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
                        dgvr.Cells["ctype1"].Value = dr["type"].ToString();
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


                data = (Dictionary<string, object>)(dic_list.ContainsKey(BotAtype.typekey6) ? dic_list[BotAtype.typekey6] : null);
                list = (List<Dictionary<string, object>>)(data.ContainsKey("inputdata") ? data["inputdata"] : null);
                textBox2.Text = data["remark"].ToString();
                dataGridView2.Rows.Clear();
                if (list.Count > 0)
                {
                    int i = 0;
                    foreach (Dictionary<string, object> dr in list)
                    {
                        dataGridView2.Rows.Add();
                        DataGridViewRow dgvr = dataGridView2.Rows[i];
                        dgvr.Cells["Column2"].Value = dr["name"].ToString();
                        dgvr.Cells["ctype2"].Value = dr["type"].ToString();
                        dgvr.Cells["btn_pass2flag"].Value = "0";
                        dgvr.Cells["btn_fail2flag"].Value = "0";
                        dgvr.Cells["na2flag"].Value = "0";
                        switch (dr["status"].ToString())
                        {
                            //0：未核对 1：已核对 2：N/A
                            case "1":
                                dgvr.Cells["btn_pass2"].Style.BackColor = Color.PaleGreen;//绿色
                                dgvr.Cells["btn_pass2flag"].Value = "1";
                                break;
                            case "0":
                                dgvr.Cells["btn_fail2"].Style.BackColor = Color.Red;//红色
                                dgvr.Cells["btn_fail2flag"].Value = "1";
                                break;
                            case "2":
                                dgvr.Cells["na2"].Style.BackColor = Color.DarkGray;//灰色
                                dgvr.Cells["na2flag"].Value = "1";
                                break;
                        }
                        i++;
                    }
                }
                dataGridView2.ClearSelection();
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
                Dictionary<string, object> data = (Dictionary<string, object>)(dic_list.ContainsKey(BotAtype.typekey5) ? dic_list[BotAtype.typekey5] : null);
                data["remark"] = textBox1.Text;
            }
            DataTable dt = Auxiliary.GetDatagridviewDatable(dataGridView1);
            if (dt.Rows.Count > 0)
            {
                Dictionary<string, object> data = (Dictionary<string, object>)(dic_list.ContainsKey(BotAtype.typekey5) ? dic_list[BotAtype.typekey5] : null);
                data["returndata"] = JsonConvert.SerializeObject(dt);
            }


            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                Dictionary<string, object> data = (Dictionary<string, object>)(dic_list.ContainsKey(BotAtype.typekey6) ? dic_list[BotAtype.typekey6] : null);
                data["remark"] = textBox2.Text;
            }
            dt = Auxiliary.GetDatagridviewDatable(dataGridView2);
            if (dt.Rows.Count > 0)
            {
                Dictionary<string, object> data = (Dictionary<string, object>)(dic_list.ContainsKey(BotAtype.typekey6) ? dic_list[BotAtype.typekey6] : null);
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
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].DefaultCellStyle.SelectionBackColor = Color.PaleGreen;
                            dataGridView1.CurrentRow.Cells["btn_pass"].Style.BackColor = Color.PaleGreen;
                            dataGridView1.CurrentRow.Cells["btn_passflag"].Value = "1";

                            dataGridView1.CurrentRow.Cells["naflag"].Value = "0";
                            dataGridView1.CurrentRow.Cells["na"].Style.BackColor = Color.White;
                            dataGridView1.CurrentRow.Cells["btn_failflag"].Value = "0";
                            dataGridView1.CurrentRow.Cells["btn_fail"].Style.BackColor = Color.White;
                            break;
                        case "btn_fail":
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].DefaultCellStyle.SelectionBackColor = Color.Red;
                            dataGridView1.CurrentRow.Cells["btn_fail"].Style.BackColor = Color.Red;
                            dataGridView1.CurrentRow.Cells["btn_failflag"].Value = "1";

                            dataGridView1.CurrentRow.Cells["naflag"].Value = "0";
                            dataGridView1.CurrentRow.Cells["na"].Style.BackColor = Color.White;
                            dataGridView1.CurrentRow.Cells["btn_passflag"].Value = "0";
                            dataGridView1.CurrentRow.Cells["btn_pass"].Style.BackColor = Color.White;
                            break;
                        case "na":
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].DefaultCellStyle.SelectionBackColor = Color.DarkGray;
                            dataGridView1.CurrentRow.Cells["na"].Style.BackColor = Color.DarkGray;
                            dataGridView1.CurrentRow.Cells["naflag"].Value = "1";

                            dataGridView1.CurrentRow.Cells["btn_passflag"].Value = "0";
                            dataGridView1.CurrentRow.Cells["btn_pass"].Style.BackColor = Color.White;
                            dataGridView1.CurrentRow.Cells["btn_failflag"].Value = "0";
                            dataGridView1.CurrentRow.Cells["btn_fail"].Style.BackColor = Color.White;
                            break;
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    switch (dataGridView2.Columns[e.ColumnIndex].Name)
                    {
                        case "btn_pass2":
                            dataGridView2.Rows[dataGridView2.CurrentRow.Index].DefaultCellStyle.SelectionBackColor = Color.PaleGreen;
                            dataGridView2.CurrentRow.Cells["btn_pass2"].Style.BackColor = Color.PaleGreen;
                            dataGridView2.CurrentRow.Cells["btn_pass2flag"].Value = "1";

                            dataGridView2.CurrentRow.Cells["na2flag"].Value = "0";
                            dataGridView2.CurrentRow.Cells["na2"].Style.BackColor = Color.White;
                            dataGridView2.CurrentRow.Cells["btn_fail2flag"].Value = "0";
                            dataGridView2.CurrentRow.Cells["btn_fail2"].Style.BackColor = Color.White;
                            break;
                        case "btn_fail2":
                            dataGridView2.Rows[dataGridView2.CurrentRow.Index].DefaultCellStyle.SelectionBackColor = Color.Red;
                            dataGridView2.CurrentRow.Cells["btn_fail2"].Style.BackColor = Color.Red;
                            dataGridView2.CurrentRow.Cells["btn_fail2flag"].Value = "1";
                            dataGridView2.CurrentRow.Cells["na2flag"].Value = "0";
                            dataGridView2.CurrentRow.Cells["na2"].Style.BackColor = Color.White;
                            dataGridView2.CurrentRow.Cells["btn_pass2flag"].Value = "0";
                            dataGridView2.CurrentRow.Cells["btn_pass2"].Style.BackColor = Color.White;
                            break;
                        case "na2":
                           dataGridView2.Rows[dataGridView2.CurrentRow.Index].DefaultCellStyle.SelectionBackColor = Color.DarkGray;
                           dataGridView2.CurrentRow.Cells["na2"].Style.BackColor = Color.DarkGray;
                           dataGridView2.CurrentRow.Cells["na2flag"].Value = "1";
                           dataGridView2.CurrentRow.Cells["btn_pass2flag"].Value = "0";
                           dataGridView2.CurrentRow.Cells["btn_pass2"].Style.BackColor = Color.White;
                           dataGridView2.CurrentRow.Cells["btn_fail2flag"].Value = "0";
                           dataGridView2.CurrentRow.Cells["btn_fail2"].Style.BackColor = Color.White;
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
