using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.Common;

namespace F_TailorRounds
{
    public partial class ProductionWorkOrder : MaterialForm
    {
        public delegate void DataChangeHandler(object sender, DataChangeEventArgs args);
        public event DataChangeHandler DataChange;
        DataTable dataDt = null;
        string art = string.Empty;
        DataTable dataTable = new DataTable();
        List<string> strOrderNo = new List<string>();
        string lingroupvalue = string.Empty; 
        public ProductionWorkOrder(DataTable dt, string art, DataTable dataTable , string linegroup ) 
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            dataDt = dt;
            dataGridView1.DataSource = dt;
            this.art = art;
            this.dataTable = dataTable;
            this.lingroupvalue = linegroup;  
        }

        private string Translate(string value)
        {
            return UIHelper.UImsg(value, Program.client, "", Program.client.Language);
        }

        private void ProductionWorkOrder_Load(object sender, EventArgs e)
        {
            LoadSelectedProductWork(dataTable);
        }
        private void LoadSelectedProductWork(DataTable dataTable)
        {
            if (dataGridView1.Rows.Count > 0 && dataGridView1 != null)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataTable.Rows.Count; j++)
                    {
                        if (dataGridView1.Rows[i].Cells[1].Value.ToString() == dataTable.Rows[j][0].ToString())
                        {
                            dataGridView1.Rows[i].Cells[0].Value = true;
                        }
                    }
                }
            }
        }
        public void OnDataChange(object sender, DataChangeEventArgs args)
        {
            DataChange?.Invoke(this, args);
        }
        public class DataChangeEventArgs : EventArgs
        {
            public DataTable dataTable;
            public DataTable lsDt;
            public DataChangeEventArgs(DataTable dataTable, DataTable lsDataTable)
            {
                this.dataTable = dataTable;
                lsDt = lsDataTable;
            }
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn() { ColumnName = "ProductWorkOrder", DataType = typeof(string) });
            if (dataGridView2.Rows.Count > 0 && dataGridView2 != null)
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                   dt.Rows.Add(row.Cells[0].Value.ToString());
                }
                if (dt == null || dt.Rows.Count < 1)
                {
                    Close();
                    DataTable dtNull = new DataTable();
                    OnDataChange(this, new DataChangeEventArgs(dt, dtNull));
                    return;
                }

                //检查所选主工单的工艺路线是否一致

                {
                    //  Manohar Modification Code Start 

                    Dictionary<string, object> podata = new Dictionary<string, object>();

                    string linevalue = lingroupvalue.ToString();                      

                    DataTable podt = new DataTable();
                    podt.Columns.Add(new DataColumn() { ColumnName = "ProductWorkOrder", DataType = typeof(string) }); 
                    foreach(DataGridViewRow row in dataGridView2.Rows)
                    {
                        podt.Rows.Add(row.Cells[0].Value.ToString()); 
                    }
                    List<string> maposList = new List<string>();

                    if (podt != null && podt.Rows.Count > 0)
                    {
                        foreach (DataRow row in podt.Rows)
                        {
                            if (row[0] != DBNull.Value)
                            {
                                maposList.Add(row[0].ToString());
                            }
                        }
                    } 
                    podata.Add("line", linevalue);
                    podata.Add("Mapos", maposList);
                     
                    string poret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                     Program.client.APIURL,
                                     "KZ_CUTMNT",
                                     "KZ_CUTMNT.Controllers.GeneralServer",
                                     "CheckLinveStatusForPO",
                                     Program.client.UserToken,  
                                     JsonConvert.SerializeObject(podata)
                                 ); 
                     
                    ResultObject resultt = JsonConvert.DeserializeObject<ResultObject>(poret); 
                     
                    if (!resultt.IsSuccess)
                    { 
                        DataTable notLinkedPOs = JsonConvert.DeserializeObject<DataTable>(resultt.RetData);
                        if(notLinkedPOs != null && notLinkedPOs.Rows.Count > 0) {
                            string poList = string.Join(
                           Environment.NewLine,
                           notLinkedPOs.AsEnumerable()
                                       .Select(r => $"{r["Mapo"]}  -  {r["D_DEPT"]}") 
                       ); 

                            MessageBox.Show(
                                $"{resultt.ErrMsg}\n\nNot Linked Master POs:\n{poList}", 
                                "Validation Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );

                            return;
                        }
                       
                    } 

                    //  Manohar Modification Code end
                    

                    Dictionary<object, object> p = new Dictionary<object, object>();
                    p.Add("art", art);
                    p.Add("dt", dt);
                    string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_TailorRoundsServer", "SelectCraftRouting", Program.client.UserToken, JsonConvert.SerializeObject(p));
                    if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<object, object>>(ret)["IsSuccess"]))
                    {
                        string json = JsonConvert.DeserializeObject<Dictionary<object, object>>(ret)["RetData"].ToString();
                        Dictionary<object, object> pairs = JsonConvert.DeserializeObject<Dictionary<object, object>>(json);
                        dt = JsonConvert.DeserializeObject<DataTable>(pairs["dt"].ToString());
                        //查询每个工单的未分配数量
                        bool result = GetProWorkOrderNum(dt);
                        if (result == true)
                        {
                            //查询这些工单的所有尺码
                            DataTable dataTable = new DataTable();
                            dataTable = GetAllSizeByWorkOrder(dt);
                            OnDataChange(this, new DataChangeEventArgs(dt, dataTable));
                            Close();
                        }
                        else
                        {
                            return;
                        }

                    }


                    else
                    {
                        MessageBox.Show(Translate(JsonConvert.DeserializeObject<Dictionary<object, object>>(ret)["RetData"].ToString()), Translate("Tips"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }
        }
        class ResultObject
        {
            public bool IsSuccess { get; set; }
            public string RetData { get; set; } // JSON DataTable
            public string ErrMsg { get; set; }
        }
 
        private DataTable GetAllSizeByWorkOrder(DataTable dt)
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
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_TailorRoundsServer", "GetSizeQty", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<object, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<object, object>>(ret)["RetData"].ToString();
                Dictionary<object, object> pairs = JsonConvert.DeserializeObject<Dictionary<object, object>>(json);
                string list = pairs["list"].ToString();
                listDt = JsonConvert.DeserializeObject<DataTable>(list);
            }
            return listDt;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string filter = "UDF01 like '%" + txtProductWorKOrder.Text.Trim() + "%'";
            DataView dv = dataDt.DefaultView;
            dv.RowFilter = filter;
            dataGridView1.DataSource = dv;
        }
        private void btnAllCheckOrNo_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button.Text == Translate("全选"))
            {
                dataGridView2.DataSource = null;
                dataGridView2.Rows.Clear();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[0].Value = true;
                    dataGridView2.Rows.Add(row.Cells[1].Value);
                }
                button.Text = Translate("全不选");
            }
            else
            {
                dataGridView2.DataSource = null;
                dataGridView2.Rows.Clear();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[0].Value = false;
                }
                button.Text = Translate("全选");
            }
        }
        private bool GetProWorkOrderNum(DataTable dt)
        {
            bool flag = true;
            Dictionary<object, object> p = new Dictionary<object, object>();
            p.Add("dt", dt);
            p.Add("art", art);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_TailorRoundsServer", "GetProWorkOrderNum", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<object, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<object, object>>(ret)["RetData"].ToString();
                if (json != "")
                {
                    MessageBox.Show(string.Format(Translate("工单{0}，已经全部分配，无法再分配轮次。"), json), Translate("Tips"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    flag = false;
                }
            }
            else
            {
                MessageBox.Show(Translate(JsonConvert.DeserializeObject<Dictionary<object, object>>(ret)["RetData"].ToString()), Translate("Tips"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return flag;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "dgvCheckBox")
            {
                if ((bool)dataGridView1.CurrentCell.EditedFormattedValue)
                {
                    if (strOrderNo.Contains(dataGridView1.CurrentRow.Cells[1].Value.ToString()))
                    {
                        strOrderNo.Remove(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    }
                    else
                    {
                        strOrderNo.Clear();
                        strOrderNo.Add(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                        if (dataGridView2.Rows.Count == 0)
                        {
                            for (int i = 0; i < strOrderNo.Count; i++)
                            {
                                dataGridView2.Rows.Add(strOrderNo[i].ToString());
                            }
                        }
                        else
                        {
                            bool flag = false;
                            for (int i = 0; i < dataGridView2.Rows.Count; i++)
                            {
                                if (dataGridView2.Rows[i].Cells[0].Value.ToString() != strOrderNo[0].ToString())
                                {
                                    flag = true;
                                }
                                else
                                {
                                    flag = false;
                                    break;
                                }
                            }
                            if (flag)
                            {
                                dataGridView2.Rows.Add(strOrderNo[0].ToString());
                               
                            }
                        }
                    }
                }
                else
                {
                    strOrderNo.Clear();
                    strOrderNo.Add(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        if (dataGridView2.Rows[i].Cells[0].Value.ToString() == strOrderNo[0].ToString())
                        {
                            dataGridView2.Rows.RemoveAt(i);
                            break;
                        }
                    }
                    strOrderNo.Clear();
                }
            }
        }

        private void dataGridView2_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (this.dataGridView2.Columns[e.ColumnIndex].Name == "Column2")
                {
                    StringFormat sf = StringFormat.GenericDefault.Clone() as StringFormat;
                    sf.FormatFlags = StringFormatFlags.DisplayFormatControl;
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Trimming = StringTrimming.EllipsisCharacter;
                    e.PaintBackground(e.CellBounds, false);
                    string text = Translate("删除");
                    e.Graphics.DrawString(text, dataGridView2.Font, Brushes.Black, e.CellBounds, sf);
                    e.Handled = true;
                }
            }
        }
        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (dataGridView2.Columns[e.ColumnIndex].Name == "Column2")
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (dataGridView1.Rows[row.Index].Cells[1].Value.ToString() == dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString())
                        {
                            row.Cells[0].Value = false;
                            dataGridView2.Rows.Remove(dataGridView2.CurrentRow);
                            return;
                        }
                    }
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show(Translate("Confirm delete？"), Translate("Tips"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (DialogResult.OK == result)
                {
                    dataGridView2.DataSource = null;
                    dataGridView2.Rows.Clear();
                }
            }
        }
    }
}
