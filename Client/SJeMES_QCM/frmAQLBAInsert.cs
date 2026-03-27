using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SJeMES_QCM
{
    public partial class frmAQLBAInsert : Form
    { 
        string PO = string.Empty;

        string DoSureUser = string.Empty;

        int listBox1SelectIndex = 0;

        //QAInfo QA = new QAInfo();

        DataTable DTPACK = new DataTable();
        DataTable DTAQLBAD = new DataTable();
        DataTable DTBABAD = new DataTable();
        DataTable DTBABADTMP = new DataTable();

        string seq = "";

        Dictionary<string,int> AQLBadInfo = new Dictionary<string, int>();
        Dictionary<string, string> AQLBadPic = new Dictionary<string, string>();

        Dictionary<string, int> BABadInfo = new Dictionary<string, int>();
        Dictionary<string, string> BABadPic = new Dictionary<string, string>();


        public frmAQLBAInsert()
        {
            InitializeComponent();


        }

        private void ClearUI()
        {
            DataTable dt = new DataTable();
            


        }

        private void ClearUI2()
        {
            DataTable dt = new DataTable();
             


            BABadInfo = new Dictionary<string, int>();
           
            BABadInfo.Add("精美", 0);
             

          

        }

        private void getData(string num)
        {
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void frmAQLBAInsert_btn01_Click_1(object sender, EventArgs e)
        {
            this.PO = txt01.Text.Trim();
            getData(string.Empty);
        }

        

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = !checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = !checkBox2.Checked;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (dataGridView2.SelectedCells.Count > 0)
                //{
                //    frmAQLPACK frm = new frmAQLPACK(
                //        dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[1].Value.ToString(),
                //        Convert.ToInt32(dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[2].Value.ToString()),
                //        dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
                //    frm.ShowDialog();

                //    DTPACK.Rows[dataGridView2.SelectedCells[0].RowIndex][0] = frm.Pack;
                //    DTPACK.Rows[dataGridView2.SelectedCells[0].RowIndex][2] = frm.Num;

                //    dataGridView2.DataSource = DTPACK.DefaultView;
                //    dataGridView2.Update();

                //}
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TakeBABad(string BadName)
        {
            try
            {
                string c = string.Empty;
                if (BadName == "精美")
                {

                   

                   
                    txt21.Text = (Convert.ToInt32(txt21.Text) + 1).ToString("0");
                    txt20.Text = (Convert.ToInt32(txt20.Text) + 1).ToString("0");
                    DTBABADTMP.Rows.Clear();
                    dataGridView4.DataSource = DTBABADTMP.DefaultView;
                    panel8.Visible = false;
                }
                else
                {
                    
                        if (BadName == "不精美")
                        {
                        txt22.Text = (Convert.ToInt32(txt22.Text) + 1).ToString("0");
                        txt20.Text = (Convert.ToInt32(txt20.Text) + 1).ToString("0");
                       

                        foreach (DataRow dr in DTBABADTMP.Rows)
                            {
                                
                                if (!BABadInfo.ContainsKey(dr[0].ToString()))
                                {
                                    //SystemSetting.frmCamera frm = new SystemSetting.frmCamera();
                                    //frm.ShowDialog();
                                    //if (!string.IsNullOrEmpty(frm.PicString))
                                    //{
                                    //    BABadPic.Add(BadName, frm.PicString);
                                    //}
                                    BABadInfo.Add(dr[0].ToString(), Convert.ToInt32 (dr[1].ToString()));
                                }
                                else
                                {
                                    BABadInfo[dr[0].ToString()] += Convert.ToInt32(dr[1].ToString());
                                }
                            }

                            DTBABADTMP.Rows.Clear();
                            dataGridView4.DataSource = DTBABADTMP.DefaultView;
                            panel8.Visible = false;
                        }
                        else
                        {
                            panel8.Visible = true;
                            //panel8.Dock = DockStyle.Fill;
                            bool isHas = false;
                            foreach(DataRow dr in DTBABADTMP.Rows)
                            {
                                if(dr[0].ToString()==BadName)
                                {
                                    isHas = true;
                                    dr[1] = Convert.ToInt32(dr[1].ToString()) + 1;
                                }
                            }
                            if(!isHas)
                            {
                                DataRow dr = DTBABADTMP.NewRow();
                                dr[0] = BadName;
                                dr[1] = "1";
                                DTBABADTMP.Rows.Add(dr);
                            }
                             
                        DTBABADTMP.DefaultView.Sort = c+" DESC";
                            dataGridView4.DataSource = DTBABADTMP.DefaultView;

                        }
                    
                }

                
               // txt21.Text = Convert.ToInt32(BABadInfo["精美"]).ToString("0");

                  

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TakeBABad("C7");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TakeBABad("C8");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TakeBABad("C9");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TakeBABad("C4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TakeBABad("C5");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TakeBABad("C6");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            TakeBABad("C1");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            TakeBABad("C2");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            TakeBABad("C3");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            TakeBABad("精美");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            int num = 0;

            foreach(DataGridViewRow dr in dataGridView2.Rows)
            {
                if(dr.Cells[2].Value.ToString() !="0" && string.IsNullOrEmpty(dr.Cells[0].Value.ToString()))
                { 
                }

                if(dr.Cells[0].Value.ToString().LastIndexOf(",")>0)
                {
                    num += dr.Cells[0].Value.ToString().Split(',').Length;
                }
                else if(dr.Cells[0].Value.ToString().LastIndexOf('/')>0)
                {
                    num += dr.Cells[0].Value.ToString().Split('/').Length;
                }
                else if (!string.IsNullOrEmpty(dr.Cells[0].Value.ToString()))
                {
                    num += 1;
                }

                
            }

            if(num >0)
            {
                txt12.Text = num.ToString();

                panel7.Dock = DockStyle.Fill;

                Dictionary<string, string> Data = new Dictionary<string, string>();
                Data.Add("Art", txt05.Text);
                Data.Add("Model", txt08.Text);
                Data.Add("PO", txt01.Text);
                Data.Add("qty", txt06.Text);
                Data.Add("qty2", txt09.Text);
                Data.Add("Customer", txt02.Text);
                Data.Add("date", txt04.Text);
                Data.Add("SampleLot", txt11.Text);
                Data.Add("SampleSize", txt15.Text);
                Data.Add("Cartons", txt12.Text);
                Data.Add("Pairs", txt16.Text);
                Data.Add("MaxQty", txt13.Text);
                Data.Add("MinQty", txt17.Text);

                if (checkBox1.Checked)
                {
                    Data.Add("Final", "√");
                }
                if (checkBox2.Checked)
                {
                    Data.Add("Re", "√");
                }

                int i = 1;
                foreach(DataGridViewRow  dr in dataGridView2.Rows)
                {
                    Data.Add("Cartion" + i, dr.Cells[0].Value.ToString());
                    Data.Add("Size" + i, dr.Cells[1].Value.ToString());
                    Data.Add("Qtyi" + i, dr.Cells[2].Value.ToString());
                    i++;
                }

                

            }
            else
            { 
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            button13_Click(button17, new EventArgs());

            //for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //{
            //    if (i < 3 && !BABadPic.ContainsKey(dataGridView1.Rows[i].Cells[0].Value.ToString()))
            //    {
            //        //BA前三项没有拍照
            //        return;
            //    }
            //}

            //for (int i = 0; i < dataGridView3.Rows.Count; i++)
            //{
            //    if (i < 3 && !AQLBadPic.ContainsKey(dataGridView3.Rows[i].Cells[0].Value.ToString()))
            //    {
            //        ErrHelper.ShowErr("01008"); //AQL前三项不良没有拍照
            //        return;
            //    }
            //}

            try
            {
                Dictionary<string, string> P = new Dictionary<string, string>();
                string IsFX = string.Empty;
                if(checkBox1.Checked)
                {
                    IsFX = "0";
                }
                else
                {
                    IsFX = "1";
                }

                string AQL =
                    txt01.Text.Trim() + "@;" +
                    txt02.Text.Trim() + "@;" +
                    txt03.Text.Trim() + "@;" +
                    txt04.Text.Trim() + "@;" +
                    txt05.Text.Trim() + "@;" +
                    txt06.Text.Trim() + "@;" +
                    txt07.Text.Trim() + "@;" +
                    IsFX+"@;"+
                    txt08.Text.Trim() + "@;" +
                    txt09.Text.Trim() + "@;" +
                    txt10.Text.Trim() + "@;" +
                    txt11.Text.Trim() + "@;" +
                    txt12.Text.Trim() + "@;" +
                    txt13.Text.Trim() + "@;" +
                    txt14.Text.Trim() + "@;" +
                    txt15.Text.Trim() + "@;" +
                    txt16.Text.Trim() + "@;" +
                    txt17.Text.Trim() + "@;" +
                    txt18.Text.Trim() + "@;" +
                    seq;

                string AQLPACK = string.Empty;

                foreach(DataGridViewRow dr in dataGridView2.Rows)
                {
                    AQLPACK += dr.Cells[1].Value.ToString() + "@," +
                        dr.Cells[2].Value.ToString() + "@," +
                        dr.Cells[3].Value.ToString() + "@,"+
                        dr.Cells[0].Value.ToString() + "@;";
                }

                string AQLBad = string.Empty;

                foreach (DataGridViewRow dr in dataGridView3.Rows)
                {
                    if (AQLBadPic.ContainsKey(dr.Cells[0].Value.ToString()))
                    {
                        AQLBad += dr.Cells[0].Value.ToString() + "@," +
                            dr.Cells[1].Value.ToString() + "@," +
                        AQLBadPic[dr.Cells[0].Value.ToString()] + "@;";
                    }
                    else
                    {
                        AQLBad += dr.Cells[0].Value.ToString() + "@," +
                            dr.Cells[1].Value.ToString() + "@,@;";
                    }
                }

                P.Add("AQL", AQL);
                P.Add("AQLPACK", AQLPACK);
                P.Add("AQLBad", AQLBad);

               

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        
        }

        private void button18_Click(object sender, EventArgs e)
        {
            
            //for (int i = 0; i < dataGridView3.Rows.Count; i++)
            //{
            //    if (i < 3 && !AQLBadPic.ContainsKey(dataGridView3.Rows[i].Cells[0].Value.ToString()))
            //    {
            //        ErrHelper.ShowErr("01008"); //AQL前三项不良没有拍照
            //        return;
            //    }
            //}

            //for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //{
            //    if (i < 3 && !BABadPic.ContainsKey(dataGridView1.Rows[i].Cells[0].Value.ToString()))
            //    {
            //        ErrHelper.ShowErr("01009"); //BA前三项不良没有拍照
            //        return;
            //    }
            //}
             
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt01.Text.Trim()))
            {
                getData(string.Empty);
            }
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
           
            //if (e.KeyData ==  Keys.Enter)
            //{
            //    e.Handled = true;
            //    dataGridView2_CellClick(dataGridView2, new DataGridViewCellEventArgs(0, 0));
            //}
        }

        private void button13_Click(object sender, EventArgs e)
        { 
        }

        private void button11_Click(object sender, EventArgs e)
        {
            TakeBABad("不精美");
        }

        private void txt01_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==13)
            {
                frmAQLBAInsert_btn01_Click_1(frmAQLBAInsert_btn01, new EventArgs());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txt07_Click(object sender, EventArgs e)
        {
            
        }

        private void txt09_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                getData(txt09.Text);
            }
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CountPack();
        }

        private void CountPack()
        {
            int num = 0;
            foreach(DataGridViewRow dr in dataGridView2.Rows)
            {
                num += Convert.ToInt32(dr.Cells[2].Value.ToString());
            }

            labPackNum.Text = num.ToString();
        }

        private void dataGridView3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            AQLBadInfo[dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString()] = Convert.ToInt32(dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString());
            txt14.Text = 0.ToString();
            foreach (string key in AQLBadInfo.Keys)
            {
                txt14.Text = (Convert.ToInt32(txt14.Text) + AQLBadInfo[key]).ToString("0");
            }

            if (Convert.ToInt32(txt14.Text) > Convert.ToInt32(txt13.Text))
            {
                txt18.Text = "Rejected";
            }
            else
            {
                txt18.Text = "Accepted";
            } 

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            BABadInfo[dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()] = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());

        }

        

        private void button12_Click(object sender, EventArgs e)
        {
            DTBABADTMP.Rows.Clear();
            dataGridView4.DataSource = DTBABADTMP.DefaultView;

            panel8.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txt19_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmAQLBAInsert_KeyDown(object sender, KeyEventArgs e)
        {

            if(txt01.Focused || txt09.Focused || dataGridView1.Focused || dataGridView2.Focused || dataGridView3.Focused)
            {
                return;
            }

            

            if (string.IsNullOrEmpty(txt12.Text))
            {
                return;
            }

            if (e.KeyCode == Keys.NumPad1)
            {
                TakeBABad("C1");
                e.Handled = true;
            }

            if (e.KeyCode == Keys.NumPad2)
            {
                TakeBABad("C2");
                e.Handled = true;
            }

            if (e.KeyCode == Keys.NumPad3)
            {
                TakeBABad("C3");
                e.Handled = true;
            }

            if (e.KeyCode == Keys.NumPad4)
            {
                TakeBABad("C4");
                e.Handled = true;
            }

            if (e.KeyCode == Keys.NumPad5)
            {
                TakeBABad("C5");
                e.Handled = true;
            }

            if (e.KeyCode == Keys.NumPad6)
            {
                TakeBABad("C6");
                e.Handled = true;
            }

            if (e.KeyCode == Keys.NumPad7)
            {
                TakeBABad("C7");
                e.Handled = true;
            }

            if (e.KeyCode == Keys.NumPad8)
            {
                TakeBABad("C8");
                e.Handled = true;
            }

            if (e.KeyCode == Keys.NumPad9)
            {
                TakeBABad("C9");
                e.Handled = true;
            }

            if(e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                if(listBox1.SelectedIndex == listBox1.Items.Count -1)
                {
                    listBox1.SelectedIndex = 0;
                }
                else
                {
                    listBox1.SelectedIndex = listBox1.SelectedIndex + 1;
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                if (listBox1.SelectedIndex ==  - 1)
                {
                    listBox1.SelectedIndex = listBox1.Items.Count-1;
                }
                else
                {
                    listBox1.SelectedIndex = listBox1.SelectedIndex - 1;
                }
            }
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            if(panel7.Dock == DockStyle.Right)
            {
                panel7.Dock = DockStyle.Fill;
            }
            else
            {
                panel7.Dock = DockStyle.Right;
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.txt07.Text = string.Empty;

        }

        private void txt22_TextChanged(object sender, EventArgs e)
        {
            try
            {
                 txt21.Text = (Convert.ToInt32(txt20.Text) - Convert.ToInt32(txt22.Text)).ToString();
                BABadInfo["精美"] = Convert.ToInt32(txt21.Text);

                txt19.Text = (Convert.ToDouble(BABadInfo["精美"]) / Convert.ToDouble(txt20.Text) * 5).ToString("0.0");

            }
            catch { }
        }

        

        private void txt20_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt22_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt20_TextChanged(object sender, EventArgs e)
        {
            txt21.Text = (Convert.ToInt32(txt20.Text) - Convert.ToInt32(txt22.Text)).ToString();
            BABadInfo["精美"] = Convert.ToInt32(txt21.Text);
            txt19.Text = (Convert.ToDouble(BABadInfo["精美"]) / Convert.ToDouble(txt20.Text) * 5).ToString("0.0");

        }

        private void txt20_MouseClick(object sender, MouseEventArgs e)
        {
            txt20.SelectAll();
        }

        private void txt22_MouseClick(object sender, MouseEventArgs e)
        {
            txt22.SelectAll();
        }
    }
}
