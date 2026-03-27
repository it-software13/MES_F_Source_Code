using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using System.Data;
using SJeMES_Control_Library;
using static F_TailorRounds.ProductionWorkOrder;
using System.Reflection;
using System.Collections;
using System.Drawing;
using System.Linq;
using SJeMES_Framework.WebAPI;
using SJeMES_Framework.Common;

namespace F_TailorRounds
{
    public partial class F_TailorRounds : MaterialForm
    {

        public F_TailorRounds()
        {
            InitializeComponent(); 
            LoadDepartment();
            //设置窗体的双缓冲
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            //利用反射设置DataGridView的双缓冲
            Type dgvType = this.dataGridView1.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.dataGridView1, true, null);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
        }
        int lastTurnNo = 0; //定义已生成的最后一轮

        public void LoadDepartment() //加载部门
        {
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_TailorRoundsServer", "LoadDepartment", Program.client.UserToken, JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                txtCutGroup.Text = json;
            }
            else
            {
                string msg = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                MessageHelper.ShowErr(this, msg);
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtART.Text))
            {
                MessageBox.Show("Please enter ART!", "Tips", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Dictionary<string, string> p = new Dictionary<string, string>(); 

            p.Add("art", txtART.Text.Trim()); 

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_TailorRoundsServer", "SelectArt", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<object, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<object, object>>(ret)["RetData"].ToString();
                Dictionary<object, object> pairs = JsonConvert.DeserializeObject<Dictionary<object, object>>(json);
                string shoesType = pairs["shoesType"].ToString();
                string dt = pairs["dt"].ToString();
                DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(dt);
                rtxShoesType.Text = shoesType;
                DataTable dataTable1 = new DataTable();
                dataTable1.Columns.Add(new DataColumn() { ColumnName = "ProductWorkOrder", DataType = typeof(string) });
                if (dataGridView2.Rows.Count > 0 && dataGridView1 != null)
                {
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        dataTable1.Rows.Add(row.Cells[0].Value.ToString());
                    }
                }
                ProductionWorkOrder productionWorkOrder = new ProductionWorkOrder(dataTable, txtART.Text, dataTable1 , txtCutGroup.Text);  

                productionWorkOrder.DataChange += new ProductionWorkOrder.DataChangeHandler(DataChanged_ProductOrderDt);
                productionWorkOrder.ShowDialog();
            }
            else
            {
                MessageBox.Show(JsonConvert.DeserializeObject<Dictionary<object, object>>(ret)["ErrMsg"].ToString(), "Hint", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //MessageBox.Show(
                //    helper.MultiLanguage(JsonConvert.DeserializeObject<Dictionary<object, object>>(ret)["ErrMsg"].ToString()),
                //    helper.MultiLanguage("Tips"),
                //    MessageBoxButtons.OK,
                //    MessageBoxIcon.Warning);
                rtxShoesType.Text = "";
                txtInSingleNum.Text = "";
                txtRoundNum.Text = "";
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView2.DataSource = null;
            }
        }
        public void DataChanged_ProductOrderDt(object sender, DataChangeEventArgs args)
        {
            dataGridView2.DataSource = args.dataTable;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            DataTable listDt = args.lsDt;
            if (listDt.Rows.Count > 0)
            {
                dataGridView1.Columns.Add("ColSize", "Size");
                for (int i = 0; i < listDt.Rows.Count; i++)
                {
                    dataGridView1.Columns.Add(listDt.Rows[i][0].ToString(), listDt.Rows[i][0].ToString());
                }
                dataGridView1.Columns.Add("ColTotal", "Total");

                DataGridViewRow dr1 = new DataGridViewRow();
                dr1.CreateCells(dataGridView1);
                dr1.Cells[0].Value = "Number of assigned rounds";
                DataGridViewRow dr2 = new DataGridViewRow();
                dr2.CreateCells(dataGridView1);
                dr2.Cells[0].Value = "Number of unassigned rounds";
                DataGridViewRow dr3 = new DataGridViewRow();
                dr3.CreateCells(dataGridView1);
                dr3.Cells[0].Value = "The number of rounds";

                decimal sum01 = 0;
                decimal sum02 = 0;
                int count = listDt.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    decimal qty1 = decimal.Parse(listDt.Rows[i][2].ToString());
                    sum01 += qty1;
                    dr1.Cells[i + 1].Value = listDt.Rows[i][2].ToString();
                    decimal qty2 = decimal.Parse(listDt.Rows[i][1].ToString());
                    sum02 += qty2;
                    dr2.Cells[i + 1].Value = listDt.Rows[i][1].ToString();
                    dr3.Cells[i + 1].Value = listDt.Rows[i][1].ToString();
                }
                dr1.Cells[count + 1].Value = sum01.ToString();
                dr2.Cells[count + 1].Value = sum02.ToString();
                dr3.Cells[count + 1].Value = sum02.ToString();
                dataGridView1.Rows.Add(dr1);
                dataGridView1.Rows.Add(dr2);
                dataGridView1.Rows.Add(dr3);
                dataGridView1.Rows[0].ReadOnly = true;
                dataGridView1.Rows[1].ReadOnly = true;
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
        }

        private void btnCreateRound_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtART.Text))
            {
                MessageBox.Show("Please enter ART!", "Hint", MessageBoxButtons.OK);
                return;
            }
            if (string.IsNullOrEmpty(txtRoundNum.Text))
            {
                MessageBox.Show("The number of each round is empty!", "Hint", MessageBoxButtons.OK);
                return;
            }
            if (dataGridView1.Rows.Count == 0 || dataGridView2.Rows.Count == 0)
            {
                MessageBox.Show("Please select a production work order!", "Tips", MessageBoxButtons.OK);
                return;
            }
            double autoQty = 0;  //一轮的最大数量
            try
            {
                autoQty = double.Parse(txtRoundNum.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            if (dataGridView1.Rows.Count > 0)
            {
                double maxQty = 0;
                for (int k = 1; k < dataGridView1.Columns.Count - 1; k++)
                {
                    if (dataGridView1.Rows[2].Cells[k].Value != null && !"".Equals(dataGridView1.Rows[2].Cells[k].Value))
                    {
                        double generateQty = double.Parse(dataGridView1.Rows[2].Cells[k].Value.ToString());
                        if (generateQty > maxQty)
                            maxQty = generateQty;
                    }
                }
                if (maxQty == 0)
                {
                    MessageBox.Show("The number of this round is 0, no rounds!");
                    return;
                }
                string udf01 = "";
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    udf01 += "'" + dataGridView2.Rows[i].Cells[0].Value.ToString() + "',";
                    if (i == dataGridView2.Rows.Count - 1)
                    {
                        udf01 += "'" + dataGridView2.Rows[i].Cells[0].Value.ToString() + "'";
                    }
                }
                //查询之前生成的最大轮次，后续生成的轮次在这个基础上
                QueryMaxTurnByDept(udf01, txtCutGroup.Text);
                //移除已经存在的各个轮次表
                for (int c = dataGridView1.Rows.Count - 1; c > 2; c--)
                {
                    dataGridView1.Rows.RemoveAt(c);
                }
                //如果最大生成数量不大于自动生成数量，则只生成一轮
                if (maxQty <= autoQty)
                {
                    DataGridViewRow dr = new DataGridViewRow();
                    dr.CreateCells(dataGridView1);
                    dr.Cells[0].Value = string.Format("Round {0}", (lastTurnNo + 1));
                    int count = dataGridView1.Columns.Count;
                    for (int i = 1; i < count; i++)
                    {
                        dr.Cells[i].Value = dataGridView1.Rows[2].Cells[i].Value;
                    }
                    dataGridView1.Rows.Add(dr);
                }
                else
                {
                    int num = Convert.ToInt32(Math.Ceiling(maxQty / autoQty));
                    for (int i = 1; i < num + 1; i++)
                    {
                        DataGridViewRow dr = new DataGridViewRow();
                        dr.CreateCells(dataGridView1);
                        dr.Cells[0].Value = string.Format("Round {0}", (lastTurnNo + i));
                        int count = dataGridView1.Columns.Count;
                        double sunQty = 0;
                        for (int k = 1; k < count - 1; k++)
                        {
                            double orderQty = dataGridView1.Rows[2].Cells[k].Value != null && !"".Equals(dataGridView1.Rows[2].Cells[k].Value.ToString()) ? double.Parse(dataGridView1.Rows[2].Cells[k].Value.ToString()) : 0;
                            if (orderQty - autoQty * i >= 0)
                            {
                                dr.Cells[k].Value = autoQty;
                                sunQty += autoQty;
                            }
                            else if ((0 - autoQty) < (orderQty - autoQty * i) && (orderQty - autoQty * i) < 0)
                            {
                                dr.Cells[k].Value = orderQty - autoQty * (i - 1);
                                sunQty += orderQty - autoQty * (i - 1);
                            }
                            else
                            {
                                dr.Cells[k].Value = "";
                            }
                        }
                        dr.Cells[count - 1].Value = sunQty;
                        dataGridView1.Rows.Add(dr);
                    }
                }
            }
            else
            {
                MessageBox.Show("No work orders to assign!", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private int QueryMaxTurnByDept(string orderNo, string dept) //获取最大轮次
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vOrderNo", orderNo);
            p.Add("vDept", dept);
            string retArt = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_TailorRoundsServer", "QueryMaxTurnByDept", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(retArt)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(retArt)["RetData"].ToString();
                lastTurnNo = int.Parse(json);
            }
            return lastTurnNo;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtART.Text.Trim()))
            {
                MessageBox.Show("Failed to save, please enter ART!", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            if (dataGridView2.Rows.Count <= 0 || dataGridView2 == null)
            {
                MessageBox.Show("Failed to save, please select a ticket!", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dataGridView1.Rows.Count <= 3)
            {
                MessageBox.Show("The round has not been assigned, the save failed, please assign the round first!", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int colCount = dataGridView1.Columns.Count;
            int rowCount = dataGridView1.Rows.Count;
            if (colCount > 0 && rowCount > 3)
            {
                for (int i = 1; i < colCount; i++)
                {
                    //查询需要保存的数量，如果为空，则为0
                    double remainQty = dataGridView1.Rows[2].Cells[i].Value != null && !"".Equals(dataGridView1.Rows[2].Cells[i].Value.ToString()) ? double.Parse(dataGridView1.Rows[2].Cells[i].Value.ToString()) : 0;
                    double sumQty = 0;  //同尺码的分成的数量总和
                    for (int j = 3; j < rowCount; j++)
                    {
                        if (dataGridView1.Rows[j].Cells[i].Value != null)
                        {
                            string qty = dataGridView1.Rows[j].Cells[i].Value.ToString();
                            if (!"".Equals(qty))
                                sumQty += double.Parse(qty);
                        }
                    }
                    if (sumQty > remainQty)
                    {
                        MessageBox.Show(
                            "Size:" +
                            dataGridView1.Columns[i].HeaderText + ": The number of rounds is greater than the number of this sub-round");
                        return;
                    }
                }
                for (int c = 3; c < rowCount; c++)
                {
                    if (dataGridView1.Rows[c].Cells[colCount - 1].Value != null)
                    {
                        //qty 单轮次的总数量
                        string qty = dataGridView1.Rows[c].Cells[colCount - 1].Value.ToString();
                        if (!"".Equals(qty) && double.Parse(qty) <= 0)
                        {
                            MessageBox.Show(string.Format("Round {0}",
                                    (lastTurnNo + c - 1)) + ": The total number of rounds must be greater than 0");
                            return;
                        }
                    }
                }

                DialogResult dr = MessageBox.Show("Confirm the submission of the data?", "Hint", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    DataTable dtWorkOrder = new DataTable();
                    dtWorkOrder.Columns.Add(new DataColumn()
                    {
                        ColumnName = "ProductWorkOrder",
                        DataType = typeof(string)
                    });
                    if (dataGridView2.Rows.Count > 0 && dataGridView2 != null)
                    {
                        foreach (DataGridViewRow row in dataGridView2.Rows)
                        {
                            dtWorkOrder.Rows.Add(row.Cells[0].Value.ToString());
                        }
                    }
                    DataTable roundsDt = GetDgvToTable(dataGridView1);
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("data", roundsDt);   // 轮次尺码数据
                    p.Add("vArtNo", txtART.Text);   //ART
                    p.Add("vArtName", rtxShoesType.Text);  //鞋型
                    p.Add("vRoutNo", "C");   //制程
                    p.Add("vOrderNo", dtWorkOrder);    //工单
                    p.Add("vDept", txtCutGroup.Text);        //部门 
                    string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_TailorRoundsServer", "SaveWorkOrder", Program.client.UserToken, JsonConvert.SerializeObject(p));
                    if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                        if (json != "")
                        {
                            MessageBox.Show("Saved success" + "\n" + "Order number:" + " " + json, "Remind", MessageBoxButtons.OK);
                            RefreshTable(txtART.Text, dtWorkOrder);
                            txtInSingleNum.Text = json;
                        }
                    }
                    else
                    {
                        MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("No round data to save!");
            }
        }
        //将datagridview中的数据转为datatable
        private DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }

            for (int count = 3; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                if (string.IsNullOrEmpty(Convert.ToString(dgv.Rows[count].Cells[0].Value)))
                {
                    continue;
                }
                dr[0] = lastTurnNo + count - 2;
                for (int countsub = 1; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void RefreshTable(string art, DataTable dt)
        {
            Dictionary<object, object> p = new Dictionary<object, object>();
            p.Add("art", art);
            p.Add("dt", dt);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_TailorRoundsServer", "SelectCraftRouting", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<object, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<object, object>>(ret)["RetData"].ToString();
                Dictionary<object, object> pairs = JsonConvert.DeserializeObject<Dictionary<object, object>>(json);
                dt = JsonConvert.DeserializeObject<DataTable>(pairs["dt"].ToString());
                //查询这些工单的所有尺码
                DataTable dataTable = new DataTable();
                dataTable = GetAllSizeByWorkOrder(art, dt);
                AA(dataTable);
            }
        }
        private DataTable GetAllSizeByWorkOrder(string art, DataTable dt)
        {
            string udf01 = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == dt.Rows.Count - 1)
                {
                    udf01 += "'" + dt.Rows[i][0].ToString() + "'";
                }
                else
                {
                    udf01 += "'" + dt.Rows[i][0].ToString() + "',";
                }
            }
            DataTable listDt = new DataTable();
            Dictionary<object, object> p = new Dictionary<object, object>();
            p.Add("art", art);
            p.Add("udf01", udf01);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_TailorRoundsServer", "GetSizeQty", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<object, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<object, object>>(ret)["RetData"].ToString();
                Dictionary<object, object> pairs = JsonConvert.DeserializeObject<Dictionary<object, object>>(json);
                string list = pairs["list"].ToString();
                listDt = JsonConvert.DeserializeObject<DataTable>(list);
            }
            return listDt;
        }

        private void AA(DataTable lsDt)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            DataTable listDt = lsDt;
            if (listDt.Rows.Count > 0)
            {
                dataGridView1.Columns.Add("ColSize", "Size");
                for (int i = 0; i < listDt.Rows.Count; i++)
                {
                    dataGridView1.Columns.Add(listDt.Rows[i][0].ToString(), listDt.Rows[i][0].ToString());
                }
                dataGridView1.Columns.Add("ColTotal", "Total");

                DataGridViewRow dr1 = new DataGridViewRow();
                dr1.CreateCells(dataGridView1);
                dr1.Cells[0].Value = "Number of assigned rounds";
                DataGridViewRow dr2 = new DataGridViewRow();
                dr2.CreateCells(dataGridView1);
                dr2.Cells[0].Value = "Number of unassigned rounds";
                DataGridViewRow dr3 = new DataGridViewRow();
                dr3.CreateCells(dataGridView1);
                dr3.Cells[0].Value = "The number of rounds";

                decimal sum01 = 0;
                decimal sum02 = 0;
                int count = listDt.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    decimal qty1 = decimal.Parse(listDt.Rows[i][2].ToString());
                    sum01 += qty1;
                    dr1.Cells[i + 1].Value = listDt.Rows[i][2].ToString();
                    decimal qty2 = decimal.Parse(listDt.Rows[i][1].ToString());
                    sum02 += qty2;
                    dr2.Cells[i + 1].Value = listDt.Rows[i][1].ToString();
                    dr3.Cells[i + 1].Value = listDt.Rows[i][1].ToString();
                }
                dr1.Cells[count + 1].Value = sum01.ToString();
                dr2.Cells[count + 1].Value = sum02.ToString();
                dr3.Cells[count + 1].Value = sum02.ToString();
                dataGridView1.Rows.Add(dr1);
                dataGridView1.Rows.Add(dr2);
                dataGridView1.Rows.Add(dr3);
                dataGridView1.Rows[0].ReadOnly = true;
                dataGridView1.Rows[1].ReadOnly = true;
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
        }

        private void btnSelectHedan_Click(object sender, EventArgs e)
        {
            btnDel.Enabled = false;
            btnDel.BackColor = Color.LightGray;
            dataGridView3.AutoGenerateColumns = false;
            if (string.IsNullOrEmpty(txtHeDanNo.Text))
            {
                MessageBox.Show("The order number cannot be empty!", "Hint", MessageBoxButtons.OK);
                txtHeDanNo.Focus();
                return;
            }
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("inaSingleNum", txtHeDanNo.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_TailorRoundsServer", "SelectRounds", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<object, object>>(ret)["RetData"].ToString();
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
                dataGridView3.DataSource = dt;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                dataGridView3.DataSource = null;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.RowIndex == 2)
            {
                int row = dataGridView1.CurrentCell.RowIndex - 1;
                int column = dataGridView1.CurrentCell.ColumnIndex;
                double values_one = double.Parse(dataGridView1.Rows[row].Cells[column].Value.ToString());//当前单元格的上一个单元格的值
                double values_two = double.Parse(dataGridView1.CurrentCell.Value == null ? "0" : dataGridView1.CurrentCell.Value.ToString());//当前单元格的值
                if (values_two > values_one || values_two < 0)
                {
                    MessageBox.Show("The number of this sub-round is greater than the number of unallocated rounds or the number of this sub-round is less than 0!", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridView1.CurrentCell.Value = dataGridView1.Rows[row].Cells[column].Value.ToString();
                }
                double result = 0;
                //循环第二行的每一列
                for (int i = 1; i < dataGridView1.Rows[2].Cells.Count - 1; i++)
                {
                    result += double.Parse(dataGridView1.Rows[2].Cells[i].Value == null ? "0" : dataGridView1.Rows[2].Cells[i].Value.ToString());
                }
                dataGridView1.Rows[2].Cells[dataGridView1.Rows[2].Cells.Count - 1].Value = result;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHeDanNo.Text))
            {
                MessageBox.Show("The order number cannot be empty!", "Hint", MessageBoxButtons.OK);
                txtHeDanNo.Focus();
                return;
            }
            DataTable dt = (DataTable)dataGridView3.DataSource;
            Dictionary<object, object> p = new Dictionary<object, object>();
            p.Add("inaSingleNum", txtHeDanNo.Text);
            p.Add("dTable", dt);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_TailorRoundsServer", "DelRounds", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                MessageHelper.ShowSuccess(this, "Delete success");
                dataGridView3.DataSource = null;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                dataGridView3.DataSource = null;
            }
        }
        private void btnSelectNotPrint_Click(object sender, EventArgs e)
        {
            btnDel.Enabled = true;
            btnDel.BackColor = Color.MediumTurquoise;
            dataGridView3.AutoGenerateColumns = false;
            if (string.IsNullOrEmpty(txtHeDanNo.Text))
            {
                MessageBox.Show("The order number cannot be empty!", "Hint", MessageBoxButtons.OK);
                txtHeDanNo.Focus();
                return;
            }
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("inaSingleNum", txtHeDanNo.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_TailorRoundsServer", "SelectNotPrint", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
                dataGridView3.DataSource = dt;
            }
            else
            {
                btnDel.Enabled = false;
                btnDel.BackColor = Color.LightGray;
                dataGridView3.DataSource = null;
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());

            }
        }

        private void F_TailorRounds_Load(object sender, EventArgs e)
        {
            btnDel.Enabled = false;
            btnDel.BackColor = Color.LightGray;
        }
    }
}
