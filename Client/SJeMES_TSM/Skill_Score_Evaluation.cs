using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutocompleteMenuNS;
using FastReport;
using FastReport.Data;
using FastReport.Utils;
using MaterialSkin;
using MaterialSkin.Controls;
using NewExportExcels;
using Newtonsoft.Json;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.WebAPI; 

namespace SJeMES_TSM
{
    public partial class Skill_Score_Evaluation : MaterialForm
    {
        public static Image image1;
        public static Image image2;
        public static Image image3;
        public static Image image4;
        public static Image image5;
        public static Image image6;
        public static Image image7;
        public static Image image8;
        AutoCompleteStringCollection Autodata;
        public Skill_Score_Evaluation()
        {
            InitializeComponent();
            emptyfields();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string Type = string.Empty;
                try
                {
                    if (checkBox1.Checked)
                    {
                        Type = "0";//registered employee
                    }
                    else if (checkBox2.Checked)
                    {
                        Type = "2";//All employees
                    }
                    else
                    {
                        Type = "1";//
                    }
                   

                    Dictionary<string, object> retData = new Dictionary<string, object>();
                    retData.Add("Barcode", textBox1.Text);
                    retData.Add("Type", Type);
                    string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                        Program.Client.APIURL,
                        "SJ_TSMAPI",
                        "SJ_TSMAPI.Skill_Score_Evaluation",
                        "GetEmp_RegistrationDetails",
                        Program.Client.UserToken,
                        Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                        );
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);
                    if (ret.IsSuccess)
                    {
                        Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                        if (Type == "0")
                        {
                            DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data1"].ToString());
                            string Status = dic["Status"].ToString();

                            if (Status == "0")
                            {
                                textBox2.Text = dtJson1.Rows[0]["EMP_NAME"].ToString();
                                textBox5.Text = dtJson1.Rows[0]["DEPARTMENT"].ToString();
                                comboBox3.Text = dtJson1.Rows[0]["PROCESS_NAME"].ToString();
                                textBox3.Text = dtJson1.Rows[0]["TRAINER"].ToString();
                                comboBox4.Enabled = false;
                            }
                            else if (Status == "1")
                            {
                                textBox1.Text = dtJson1.Rows[0]["Barcode"].ToString();
                                textBox2.Text = dtJson1.Rows[0]["Name"].ToString();
                                textBox3.Text = dtJson1.Rows[0]["Trainer"].ToString();
                                comboBox3.Text = dtJson1.Rows[0]["Process"].ToString();
                                textBox5.Text = dtJson1.Rows[0]["Dept"].ToString();
                                textBox6.Text = dtJson1.Rows[0]["Model"].ToString();
                                textBox7.Text = dtJson1.Rows[0]["IE_st_time"].ToString();
                                textBox8.Text = dtJson1.Rows[0]["SkillLevel"].ToString();
                                textBox9.Text = dtJson1.Rows[0]["FirstCycle"].ToString();
                                textBox10.Text = dtJson1.Rows[0]["SecondCycle"].ToString();
                                textBox11.Text = dtJson1.Rows[0]["ThirdCycle"].ToString();
                                textBox12.Text = dtJson1.Rows[0]["FifthCycle"].ToString();
                                textBox13.Text = dtJson1.Rows[0]["FourthCycle"].ToString();
                                textBox14.Text = dtJson1.Rows[0]["IEScore"].ToString();
                                textBox15.Text = dtJson1.Rows[0]["QIPScore"].ToString();
                                textBox16.Text = dtJson1.Rows[0]["AvgCycleTime"].ToString();
                                textBox17.Text = dtJson1.Rows[0]["TotalScore"].ToString();
                                textBox18.Text = dtJson1.Rows[0]["Totalpairs"].ToString();
                                textBox19.Text = dtJson1.Rows[0]["Qualitypairs"].ToString();
                                textBox20.Text = dtJson1.Rows[0]["TCT"].ToString();

                                #region Old Code Don't delete it
                                //byte[] image1 = dtJson1.Rows[0]["TRAINER_SIG"] != null ? Convert.FromBase64String(dtJson1.Rows[0]["TRAINER_SIG"].ToString()) : new byte[0];
                                //byte[] image2 = dtJson1.Rows[0]["OPERATOR_SIG"] != null ? Convert.FromBase64String(dtJson1.Rows[0]["OPERATOR_SIG"].ToString()) : new byte[0];
                                //byte[] image3 = dtJson1.Rows[0]["IE_SPECIALIST_SIG"] != null ? Convert.FromBase64String(dtJson1.Rows[0]["IE_SPECIALIST_SIG"].ToString()) : new byte[0];
                                //byte[] image4 = dtJson1.Rows[0]["QIP_INCHARGE_SIG"] != null ? Convert.FromBase64String(dtJson1.Rows[0]["QIP_INCHARGE_SIG"].ToString()) : new byte[0];
                                //byte[] image5 = dtJson1.Rows[0]["LINE_SUPERVISOR_SIG"] != null ? Convert.FromBase64String(dtJson1.Rows[0]["LINE_SUPERVISOR_SIG"].ToString()) : new byte[0];
                                //byte[] image6 = dtJson1.Rows[0]["PLANT_INCHARGE_SIG"] != null ? Convert.FromBase64String(dtJson1.Rows[0]["PLANT_INCHARGE_SIG"].ToString()) : new byte[0];
                                //byte[] image7 = dtJson1.Rows[0]["ASSEMBLY_TRAINING_SUPERVISOR_SIG"] != null ? Convert.FromBase64String(dtJson1.Rows[0]["ASSEMBLY_TRAINING_SUPERVISOR_SIG"].ToString()) : new byte[0];
                                //byte[] image8 = dtJson1.Rows[0]["SENIOR_SUPERVISOR_OF_TRAINING_DEPT_SIG"] != null ? Convert.FromBase64String(dtJson1.Rows[0]["SENIOR_SUPERVISOR_OF_TRAINING_DEPT_SIG"].ToString()) : new byte[0];

                                //label34.Text = dtJson1.Rows[0]["TRAINER_SIG_DATE"].ToString();
                                //label35.Text = dtJson1.Rows[0]["OPERATOR_SIG_DATE"].ToString();
                                //label36.Text = dtJson1.Rows[0]["IE_SPECIALIST_SIG_DATE"].ToString();
                                //label37.Text = dtJson1.Rows[0]["QIP_INCHARGE_SIG_DATE"].ToString();
                                //label38.Text = dtJson1.Rows[0]["LINE_SUPERVISOR_SIG_DATE"].ToString();
                                //label39.Text = dtJson1.Rows[0]["PLANT_INCHARGE_SIG_DATE"].ToString();
                                //label40.Text = dtJson1.Rows[0]["ASSEMBLY_TRAINING_SUPERVISOR_SIG_DATE"].ToString();
                                //label41.Text = dtJson1.Rows[0]["SENIOR_SUPERVISOR_OF_TRAINING_DEPT_SIG_DATE"].ToString();
                                // label42.Text = dtJson1.Rows[0]["IESCORE_DATE"].ToString();
                                //  label43.Text = dtJson1.Rows[0]["QIPSCORE_DATE"].ToString();
                                // label44.Text = dtJson1.Rows[0]["PRODUCTIONSCORE_DATE"].ToString();
                                //label45.Text = dtJson1.Rows[0]["TRAININGCENTRESCORE_DATE"].ToString();

                                //if (image1.Length > 0)
                                //{
                                //    Image Image1 = BytesToImage(image1);
                                //    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                                //    pictureBox1.Image = Image1;
                                //}
                                //if (image2.Length > 0)
                                //{
                                //    Image Image2 = BytesToImage(image2);
                                //    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                                //    pictureBox2.Image = Image2;
                                //}
                                //if (image3.Length > 0)
                                //{
                                //    Image Image3 = BytesToImage(image3);
                                //    pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                                //    pictureBox3.Image = Image3;
                                //}
                                //if (image4.Length > 0)
                                //{
                                //    Image Image4 = BytesToImage(image4);
                                //    pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
                                //    pictureBox4.Image = Image4;
                                //}
                                //if (image5.Length > 0)
                                //{
                                //    Image Image5 = BytesToImage(image5);
                                //    pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
                                //    pictureBox5.Image = Image5;
                                //}
                                //if (image6.Length > 0)
                                //{
                                //    Image Image6 = BytesToImage(image6);
                                //    pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
                                //    pictureBox6.Image = Image6;
                                //}
                                //if (image7.Length > 0)
                                //{
                                //    Image Image7 = BytesToImage(image7);
                                //    pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
                                //    pictureBox7.Image = Image7;
                                //}
                                //if (image8.Length > 0)
                                //{
                                //    Image Image8 = BytesToImage(image8);
                                //    pictureBox8.SizeMode = PictureBoxSizeMode.Zoom;
                                //    pictureBox8.Image = Image8;
                                //}

                                #endregion
                            }
                        }
                        else if (Type == "1")
                        {
                            DataTable dtJson2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data2"].ToString());
                            DataTable dtJson3 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data3"].ToString());
                            string Status = dic["Status"].ToString();

                            if (Status == "0")
                            {
                                //txtProcess.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                                //txtProcess.AutoCompleteSource = AutoCompleteSource.CustomSource;
                               // Autodata = new AutoCompleteStringCollection();
                                autocompleteMenu1.Items = new string[0];
                                textBox2.Text = dtJson2.Rows[0]["EMP_NAME"].ToString();
                                textBox5.Text = dtJson2.Rows[0]["DEPARTMENT"].ToString();
                                comboBox3.Items.Clear();
                                //foreach (DataRow dr in dtJson3.Rows)
                                //{
                                //    comboBox3.Items.Add(dr["skill_name"].ToString());
                                //}

                                //if (dtJson3.Rows.Count > 0)
                                //{
                                //    autocompleteMenu1.MaximumSize = new Size(250, 350);
                                //    var columnWidth = new[] { 50, 200 };
                                //    int n = 1;
                                //    for (int i = 0; i < dtJson3.Rows.Count; i++)
                                //    {
                                //        autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dtJson3.Rows[i]["SKILL_NAME"].ToString() }, dtJson3.Rows[i]["SKILL_NAME"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                                //        n++;

                                //    }
                                //}
                            }
                            else if (Status == "1")
                            {

                            }

                        }
                        else if (Type == "2")
                        {
                            DataTable dtJson2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data2"].ToString());
                            DataTable dtJson3 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data3"].ToString());
                            string Status = dic["Status"].ToString();

                            if (Status == "0")
                            {
                                //txtProcess.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                                //txtProcess.AutoCompleteSource = AutoCompleteSource.CustomSource;
                                //Autodata = new AutoCompleteStringCollection();
                                autocompleteMenu1.Items = new string[0];
                                textBox2.Text = dtJson2.Rows[0]["EMP_NAME"].ToString();
                                textBox5.Text = dtJson2.Rows[0]["DEPARTMENT"].ToString();
                                comboBox3.Items.Clear();
                                //foreach (DataRow dr in dtJson3.Rows)
                                //{
                                //    comboBox3.Items.Add(dr["NAME"].ToString());
                                //}
                                //if (dtJson3.Rows.Count > 0)
                                //{
                                //    autocompleteMenu1.MaximumSize = new Size(250, 350);
                                //    var columnWidth = new[] { 50, 200 };
                                //    int n = 1;
                                //    for (int i = 0; i < dtJson3.Rows.Count; i++)
                                //    {
                                //        autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dtJson3.Rows[i]["name"].ToString() }, dtJson3.Rows[i]["name"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                                //        n++;

                                //    }
                                //}
                            }
                        }
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                        emptyfields();
                    }
                    
                }
                catch (Exception ex)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                    emptyfields();
                }

            }
        }
        private Image BytesToImage(byte[] byteArray)
        {
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            {
                Image image = Image.FromStream(memoryStream);
                return image;
            }
        }
        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            // textBox16.Text=GetAvgTime();
            Dictionary<string, Decimal> data = GetAvgTime();
            textBox20.Text = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(data["TotalTime"].ToString());
            textBox16.Text = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(data["AvgTime"].ToString());
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            Dictionary<string, Decimal> data = GetAvgTime();
            textBox20.Text = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(data["TotalTime"].ToString());
            textBox16.Text = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(data["AvgTime"].ToString());
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            Dictionary<string, Decimal> data = GetAvgTime();
            textBox20.Text = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(data["TotalTime"].ToString());
            textBox16.Text = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(data["AvgTime"].ToString());
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            Dictionary<string, Decimal> data = GetAvgTime();
            textBox20.Text = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(data["TotalTime"].ToString());
            textBox16.Text = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(data["AvgTime"].ToString());
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            Dictionary<string, Decimal> data = GetAvgTime();
            textBox20.Text = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(data["TotalTime"].ToString());
            textBox16.Text = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(data["AvgTime"].ToString());
        }
        private Dictionary<string, Decimal> GetAvgTime()
        {
            decimal AvgTime;
            decimal TotalTime;
            if (string.IsNullOrEmpty(textBox9.Text) || string.IsNullOrEmpty(textBox10.Text) || string.IsNullOrEmpty(textBox11.Text) || string.IsNullOrEmpty(textBox12.Text) || string.IsNullOrEmpty(textBox13.Text))
            {
                AvgTime = 0;
                TotalTime = 0;
            }
            else
            {
                TotalTime = Convert.ToDecimal(textBox9.Text) + Convert.ToDecimal(textBox10.Text) + Convert.ToDecimal(textBox11.Text) + Convert.ToDecimal(textBox12.Text) + Convert.ToDecimal(textBox13.Text);
                AvgTime = TotalTime / 5;
            }
            Dictionary<string, Decimal> data = new Dictionary<string, Decimal>();
            data.Add("TotalTime", TotalTime);
            data.Add("AvgTime", AvgTime);
            return data;
        }

        private string TotalScore()
        {
            decimal TotalScore;
            if(!string.IsNullOrEmpty(textBox14.Text)&&!string.IsNullOrEmpty(textBox15.Text))
            {
                TotalScore = Math.Round(Convert.ToDecimal(textBox14.Text) + Convert.ToDecimal(textBox15.Text), 1);
            }
            else if (string.IsNullOrEmpty(textBox14.Text)&& !string.IsNullOrEmpty(textBox15.Text))
            {
                TotalScore = Math.Round(0 + Convert.ToDecimal(textBox15.Text), 1);
            }
            else if (string.IsNullOrEmpty(textBox15.Text)&&!string.IsNullOrEmpty(textBox14.Text))
            {
                TotalScore = Math.Round(Convert.ToDecimal(textBox14.Text) + 0, 1);
            }
            else
            {
                TotalScore = 0;
            }

            return TotalScore.ToString();
        }

        private string RFTScore()
        {
            decimal RFTScore;

            if (string.IsNullOrEmpty(textBox18.Text) || string.IsNullOrEmpty(textBox19.Text))
            {
                RFTScore = 0;
            }
            else
            {
                RFTScore = Math.Round(((((Convert.ToDecimal(textBox19.Text) / Convert.ToDecimal(textBox18.Text))* 100)/95)*30), 1);
                if(RFTScore>30)
                {
                    RFTScore = 30;
                }
            }


            return RFTScore.ToString();
        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox10.Focus();
            }
        }

        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox11.Focus();
            }
        }

        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox13.Focus();
            }
        }

        private void textBox13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox12.Focus();
            }
        }

        private void textBox12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox9.Focus();
            }
        }


        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox18.Text))
            {
                Decimal Total_Pairs = Convert.ToDecimal(textBox18.Text);
                Decimal Quality_Pairs = string.IsNullOrEmpty(textBox19.Text) ? 0 : Convert.ToDecimal(textBox19.Text);
                if (Quality_Pairs > Total_Pairs)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Quantity should not be more than total quantity");
                    textBox19.Text = "";
                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please enter inspection pairs first");
                textBox19.Text = "";
            }
            textBox15.Text = RFTScore();
        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(textBox18.Text))
            //{
            //    Decimal value = Convert.ToDecimal(textBox18.Text);
            //    if (value > 30)
            //    {
            //        SJeMES_Control_Library.MessageHelper.ShowWarning(this, "Score should not be greater than 30");
            //        textBox18.Text = "";
            //    }

            //}
            textBox15.Text = RFTScore();
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox17.Text)&& Convert.ToDecimal(textBox17.Text) > 78)
            {
                if (Convert.ToDecimal(textBox17.Text) >= 78 && Convert.ToDecimal(textBox17.Text) <= 87.5m)
                {
                    textBox8.Text = "1";
                }
                else if (Convert.ToDecimal(textBox17.Text) >= 87.5m && Convert.ToDecimal(textBox17.Text) <= 93.5m)
                {
                    textBox8.Text = "2";
                }
                else if (Convert.ToDecimal(textBox17.Text) >= 93.5m && Convert.ToDecimal(textBox17.Text) <= 99.5m)
                {
                    textBox8.Text = "3";
                }
                else if (Convert.ToDecimal(textBox17.Text) >= 99.5m)
                {
                    textBox8.Text = "4";
                }

            }
            else if (textBox17.Text == "0")
            {
                textBox17.Text = "";
            }
            else
            {
                textBox8.Text = "";
            }

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(textBox16.Text) && !string.IsNullOrEmpty(textBox7.Text) && Convert.ToDecimal(textBox16.Text) != 0)
            {
                decimal value = Math.Round(((Convert.ToDecimal(textBox7.Text) / Convert.ToDecimal(textBox16.Text) * 100)/80)*70, 1);
                if (value >= 70)
                {
                    textBox14.Text = "70";
                }
                else
                {
                    textBox14.Text = value.ToString();
                }

            }

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox16.Text) && !string.IsNullOrEmpty(textBox7.Text) && Convert.ToDecimal(textBox16.Text) != 0)
            {
                decimal value = Math.Round(((Convert.ToDecimal(textBox7.Text) / Convert.ToDecimal(textBox16.Text) * 100) / 80) * 70, 1);
                if (value >= 70)
                {
                    textBox14.Text = "70";
                }
                else
                {
                    textBox14.Text = value.ToString();
                }
            }
            else if (string.IsNullOrEmpty(textBox7.Text))
            {
                textBox14.Text = "";
            }
        }



        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void textBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox19_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            // Allow digits only
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox18_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            // Allow digits only
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnsig1_Click(object sender, EventArgs e)
        {
            int index = 1;
            int f = 1;
            Signature_Upload su = new Signature_Upload(index, f);
            su.Show();
        }

        private void btnsig2_Click(object sender, EventArgs e)
        {
            int index = 2;
            int f = 2;
            Signature_Upload su = new Signature_Upload(index, f);
            su.Show();
        }

        private void btnsig3_Click(object sender, EventArgs e)
        {
            int index = 3;
            int f = 3;
            Signature_Upload su = new Signature_Upload(index, f);
            su.Show();
        }

        private void btnsig4_Click(object sender, EventArgs e)
        {
            int index = 4;
            int f = 4;
            Signature_Upload su = new Signature_Upload(index, f);
            su.Show();
        }

        private void btnsig5_Click(object sender, EventArgs e)
        {
            int index = 5;
            int f = 5;
            Signature_Upload su = new Signature_Upload(index, f);
            su.Show();
        }

        private void btnsig6_Click(object sender, EventArgs e)
        {
            int index = 6;
            int f = 6;
            Signature_Upload su = new Signature_Upload(index, f);
            su.Show();
        }

        private void btnsig7_Click(object sender, EventArgs e)
        {
            int index = 7;
            int f = 7;
            Signature_Upload su = new Signature_Upload(index, f);
            su.Show();
        }

        private void btnsig8_Click(object sender, EventArgs e)
        {
            int index = 8;
            int f = 8;
            Signature_Upload su = new Signature_Upload(index, f);
            su.Show();
        }

        //public void Showimage1(Image image)
        //{
        //    image1 = image;
        //    ShowImageAndBind(image1, pictureBox1);
        //    label34.Text = DateTime.Now.ToString();
        //}
        //public void Showimage2(Image image)
        //{
        //    image2 = image;
        //    ShowImageAndBind(image2, pictureBox2);
        //    label35.Text = DateTime.Now.ToString();
        //}
        //public void Showimage3(Image image)
        //{
        //    image3 = image;
        //    ShowImageAndBind(image3, pictureBox3);
        //    label36.Text = DateTime.Now.ToString();
        //}
        //public void Showimage4(Image image)
        //{
        //    image4 = image;
        //    ShowImageAndBind(image4, pictureBox4);
        //    label37.Text = DateTime.Now.ToString();
        //}

        //public void Showimage5(Image image)
        //{
        //    image5 = image;
        //    ShowImageAndBind(image5, pictureBox5);
        //    label38.Text = DateTime.Now.ToString();
        //}
        //public void Showimage6(Image image)
        //{
        //    image6 = image;
        //    ShowImageAndBind(image6, pictureBox6);
        //    label39.Text = DateTime.Now.ToString();
        //}
        //public void Showimage7(Image image)
        //{
        //    image7 = image;
        //    ShowImageAndBind(image7, pictureBox7);
        //    label40.Text = DateTime.Now.ToString();
        //}
        //public void Showimage8(Image image)
        //{
        //    image8 = image;
        //    ShowImageAndBind(image8, pictureBox8);
        //    label41.Text = DateTime.Now.ToString();
        //}
        public void ShowImageAndBind(Image image, PictureBox pictureBox)
        {
            if (image != null && pictureBox != null)
            {
                BindImage(image, pictureBox);
            }
        }
        private void BindImage(Image image, PictureBox pictureBox)
        {
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Image = image;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(comboBox3.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select Barcode and Process");
                return;
            }
            if (!CheckSkillName())
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "In correct Process Name");
                comboBox3.Text = "";
                return;
            }
            else
            {
                //byte[] imageBytes1 = PictureBoxToByteArray(pictureBox1);
                //byte[] imageBytes2 = PictureBoxToByteArray(pictureBox2);
                //byte[] imageBytes3 = PictureBoxToByteArray(pictureBox3);
                //byte[] imageBytes4 = PictureBoxToByteArray(pictureBox4);
                //byte[] imageBytes5 = PictureBoxToByteArray(pictureBox5);
                //byte[] imageBytes6 = PictureBoxToByteArray(pictureBox6);
                //byte[] imageBytes7 = PictureBoxToByteArray(pictureBox7);
                //byte[] imageBytes8 = PictureBoxToByteArray(pictureBox8);

                Dictionary<string, object> Data = new Dictionary<string, object>();
                Data.Add("Barcode", textBox1.Text);
                Data.Add("Name", textBox2.Text);
                Data.Add("Dept", textBox5.Text);
                Data.Add("Process", comboBox3.Text);
                Data.Add("Trainer", textBox3.Text);
                Data.Add("Model", textBox6.Text);
                Data.Add("IE_st_time", textBox7.Text);
                Data.Add("FirststCycle", textBox9.Text);
                Data.Add("SecondCycle", textBox10.Text);
                Data.Add("ThirdCycle", textBox11.Text);
                Data.Add("FourthCycle", textBox13.Text);
                Data.Add("FifthCycle", textBox12.Text);
                Data.Add("TCT", textBox20.Text);
                Data.Add("AvgCycleTime", textBox16.Text);
                Data.Add("IEScore", textBox14.Text);
                Data.Add("QIPScore", textBox15.Text);
                Data.Add("Qualitypairs", textBox19.Text);
                Data.Add("Totalpairs", textBox18.Text);
                Data.Add("TotalScore", textBox17.Text);
                Data.Add("SkillLevel", textBox8.Text);
                //Data.Add("image1", imageBytes1 != null ? imageBytes1 : (object)DBNull.Value);
                //Data.Add("image2", imageBytes2 != null ? imageBytes2 : (object)DBNull.Value);
                //Data.Add("image3", imageBytes3 != null ? imageBytes3 : (object)DBNull.Value);
                //Data.Add("image4", imageBytes4 != null ? imageBytes4 : (object)DBNull.Value);
                //Data.Add("image5", imageBytes5 != null ? imageBytes5 : (object)DBNull.Value);
                //Data.Add("image6", imageBytes6 != null ? imageBytes6 : (object)DBNull.Value);
                //Data.Add("image7", imageBytes7 != null ? imageBytes7 : (object)DBNull.Value);
                //Data.Add("image8", imageBytes8 != null ? imageBytes8 : (object)DBNull.Value);
                //Data.Add("TRAINER_SIG_DATE", label34.Text);
                //Data.Add("OPERATOR_SIG_DATE", label35.Text);
                //Data.Add("IE_SPECIALIST_SIG_DATE", label36.Text);
                //Data.Add("QIP_INCHARGE_SIG_DATE", label37.Text);
                //Data.Add("LINE_SUPERVISOR_SIG_DATE", label38.Text);
                //Data.Add("PLANT_INCHARGE_SIG_DATE", label39.Text);
                //Data.Add("ASSEMBLY_TRAINING_SUPERVISOR_SIG_DATE", label40.Text);
                //Data.Add("SENIOR_SUPERVISOR_OF_TRAINING_DEPT_SIG_DATE", label41.Text);
                // Data.Add("IESCORE_DATE", label42.Text);
                // Data.Add("QIPSCORE_DATE", label43.Text);
                // Data.Add("PRODUCTIONSCORE_DATE", label44.Text);
                //Data.Add("TRAININGCENTRESCORE_DATE", label45.Text);
                string retData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "SJ_TSMAPI",
                    "SJ_TSMAPI.Skill_Score_Evaluation",
                    "Add_SkillScoreData",
                    Program.Client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(Data)
                    );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retData);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ret.ErrMsg);
                emptyfields();
            }
        }

        private byte[] PictureBoxToByteArray(params PictureBox[] pictureBoxes)
        {
            foreach (PictureBox pictureBox in pictureBoxes)
            {
                if (pictureBox.Image == null)
                {
                    return null;
                }
            }
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Bitmap bitmap = new Bitmap(pictureBoxes[0].Image);
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                return memoryStream.ToArray();
            }
        }

        public Boolean CheckSkillName()
        {
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("SkillName", comboBox3.Text);
            string retData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                 Program.Client.APIURL,
                 "SJ_TSMAPI",
                 "SJ_TSMAPI.Skill_Score_Evaluation",
                 "CheckSkillName",
                 Program.Client.UserToken,
                 Newtonsoft.Json.JsonConvert.SerializeObject(Data)
                 );
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retData);
            return ret.IsSuccess;
        }
        private void emptyfields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox15.Text = "";
            textBox14.Text = "";
            textBox16.Text = "";
            textBox17.Text = "";
            textBox19.Text = "";
            //textBox18.Text = "";
            textBox20.Text = "";
            //pictureBox1.Image = null;
            //pictureBox2.Image = null;
            //pictureBox3.Image = null;
            //pictureBox4.Image = null;
            //pictureBox5.Image = null;
            //pictureBox6.Image = null;
            //pictureBox7.Image = null;
            //pictureBox8.Image = null;
            //label34.Text = "";
            //label35.Text = "";
            //label36.Text = "";
            //label37.Text = "";
            //label38.Text = "";
            //label39.Text = "";
            //label40.Text = "";
            //label41.Text = "";
            label42.Text = "";
            label43.Text = "";
            label44.Text = "";
            label45.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            emptyfields();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(comboBox3.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select Barcode and Process");
            }
            else
            {
                Dictionary<string, object> retData = new Dictionary<string, object>();
                retData.Add("Barcode", textBox1.Text);
                retData.Add("Process", comboBox3.Text);
                string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "SJ_TSMAPI",
                    "SJ_TSMAPI.Skill_Score_Evaluation",
                    "Print_SkillScore",
                    Program.Client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                    );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    //string url = @"D:\AEQS_FinalSourceCode\deliveryFile\Client\SJeMES_TSM\SkillScoreEvaluation.frx";
                    string url = @"D:\AEQS_FinalSourceCode\deliveryFile\Client\SJeMES_TSM\NewFrx\SkillScoreEvaluation.frx";

                    prints(url, dt);


                }

            }

        }

        private void prints(string url, DataTable dt)
        {
            try
            {
                List<DataTable> list_dt = new List<DataTable>();
                List<DataSet> list_ds = new List<DataSet>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataTable dd = dt.Clone();
                        DataRow row = dt.Rows[i];
                        dd.ImportRow(row);
                        list_dt.Add(dd);
                    }
                    for (int i = 0; i < list_dt.Count; i++)
                    {
                        list_dt[i].TableName = "Table";
                        DataSet dsa = new DataSet();
                        dsa.Tables.Add(list_dt[i].Copy());
                        list_ds.Add(dsa);
                    }
                    using (FrmSelectPrint add = new FrmSelectPrint(url, list_ds))
                    {
                        add.ShowDialog();
                    }
                }


            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            { 
                Dictionary<string, object> retData = new Dictionary<string, object>();
                retData.Add("TYPE", comboBox1.Text); 
                string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "SJ_TSMAPI",
                    "SJ_TSMAPI.Registration",
                    "GetTypeOfProcess",
                    Program.Client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);

                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());

                    comboBox2.Items.Clear();

                    if (dtJson1.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtJson1.Rows)
                        {
                            comboBox2.Items.Add(dr["NAME"].ToString());
                        } 
                    } 
                    else
                    { 
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found.");
                    }
                }
                else
                { 
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }
            }
            catch (Exception ex)
            { 
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("Barcode", textBox21.Text);
            Data.Add("ProcessType", comboBox1.Text);
            Data.Add("ProcessName", comboBox2.Text);
            Data.Add("Training_Type", comboBox6.Text);
            Data.Add("StartDate", dateTimePicker1.Text);
            Data.Add("EndDate", dateTimePicker2.Text);
            string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                Program.Client.APIURL,
                "SJ_TSMAPI",
                "SJ_TSMAPI.Skill_Score_Evaluation",
                "GetEmp_SkillDetails",
                Program.Client.UserToken,
                Newtonsoft.Json.JsonConvert.SerializeObject(Data)
                );
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);
            if (ret.IsSuccess)
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if(dtJson1.Rows.Count>0)
                {
                    dataGridView1.DataSource = dtJson1;
                }
                else
                {
                    dataGridView1.DataSource = null;
                }
               
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
                label5.Visible = true;
                textBox3.Visible = true;
                checkBox2.Checked = false;
                textBox3.ReadOnly = true;
                emptyfields();
            }
            else if (checkBox2.Checked)
            {
                label5.Visible = true;
                textBox3.Visible = true;
                textBox3.ReadOnly = false;
                comboBox3.Enabled = true;
                comboBox4.Enabled = true;
            }
            else
            {

                comboBox3.Enabled = true;
                comboBox4.Enabled = true;
                label5.Visible = false;
                textBox3.Visible = false;
                emptyfields();
            }
            
        }

        private void Skill_Score_Evaluation_Load(object sender, EventArgs e)
        {
            label5.Visible = false;
            textBox3.Visible = false;
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            textBox17.Text = TotalScore();
        }
        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(textBox15.Text) || textBox15.Text == "0")&& !string.IsNullOrEmpty(textBox14.Text))
            { 
                textBox17.Text = Math.Round(Convert.ToDecimal(textBox14.Text) + 0, 1).ToString();
            }
            else if(textBox15.Text == "0")
            {
                textBox15.Text = "";
            }
            else
            {
                textBox17.Text = TotalScore();
            }

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Check_All_Employees_Data_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                label5.Visible = true;
                textBox3.Visible = true;
                textBox3.ReadOnly = false;
                checkBox1.Checked = false;
                comboBox3.Enabled = true;
                comboBox4.Enabled = true;
                emptyfields();
            }
            else if (checkBox1.Checked)
            {
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
                label5.Visible = true;
                textBox3.Visible = true;
                checkBox2.Checked = false;
                textBox3.ReadOnly = true;
                emptyfields();
            }
            else
            {
                comboBox3.Enabled = true;
                comboBox4.Enabled = true;
                label5.Visible = false;
                textBox3.Visible = false;
                emptyfields();
            }
        }



        private void TextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable dt = new DataTable();
                try
                {
                    Dictionary<string, object> retData = new Dictionary<string, object>();
                    retData.Add("Trainer", textBox3.Text);
                    string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                        Program.Client.APIURL,
                        "SJ_TSMAPI",
                        "SJ_TSMAPI.Skill_Score_Evaluation",
                        "GetTrainerDetails",
                        Program.Client.UserToken,
                        Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                        );
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                    if (ret.IsSuccess)
                    {

                        if (dtJson1.Rows.Count > 0)
                        {
                            textBox3.Text = dtJson1.Rows[0]["EMP_NAME"].ToString();
                        }
                        else
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found.");
                        }
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                    }
                }

                catch (Exception ex)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");

            }
            else
            {
                string a = "Get_Skill_Score_data.xls";
                ExportExcels.Export(a, dataGridView1);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Successfully downloaded");
            }
        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select Barcode");
                comboBox4.Text = "";
                comboBox3.Text = "";
                return;
            }
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("Barcode", textBox1.Text);
            Data.Add("ProcessName", comboBox3.Text);
            string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                Program.Client.APIURL,
                "SJ_TSMAPI",
                "SJ_TSMAPI.Skill_Score_Evaluation",
                "GetEmp_SkillScoreDetails",
                Program.Client.UserToken,
                Newtonsoft.Json.JsonConvert.SerializeObject(Data)
                );
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);
            if (ret.IsSuccess)
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dtJson1 != null && dtJson1.Rows.Count > 0)
                {
                    textBox1.Text = dtJson1.Rows[0]["Barcode"].ToString();
                    textBox2.Text = dtJson1.Rows[0]["Name"].ToString();
                    textBox3.Text = dtJson1.Rows[0]["Trainer"].ToString();
                    comboBox3.Text = dtJson1.Rows[0]["Process"].ToString();
                    textBox5.Text = dtJson1.Rows[0]["Dept"].ToString();
                    textBox6.Text = dtJson1.Rows[0]["Model"].ToString();
                    textBox7.Text = dtJson1.Rows[0]["IE_st_time"].ToString();
                    textBox8.Text = dtJson1.Rows[0]["SkillLevel"].ToString();
                    textBox9.Text = dtJson1.Rows[0]["FirstCycle"].ToString();
                    textBox10.Text = dtJson1.Rows[0]["SecondCycle"].ToString();
                    textBox11.Text = dtJson1.Rows[0]["ThirdCycle"].ToString();
                    textBox12.Text = dtJson1.Rows[0]["FifthCycle"].ToString();
                    textBox13.Text = dtJson1.Rows[0]["FourthCycle"].ToString();
                    textBox14.Text = dtJson1.Rows[0]["IEScore"].ToString();
                    textBox15.Text = dtJson1.Rows[0]["QIPScore"].ToString();
                    textBox16.Text = dtJson1.Rows[0]["AvgCycleTime"].ToString();
                    textBox17.Text = dtJson1.Rows[0]["TotalScore"].ToString();
                    textBox18.Text = dtJson1.Rows[0]["Totalpairs"].ToString();
                    textBox19.Text = dtJson1.Rows[0]["Qualitypairs"].ToString();
                    textBox20.Text = dtJson1.Rows[0]["TCT"].ToString();
                }
                else
                {
                    textBox3.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
                    textBox9.Text = "";
                    textBox10.Text = "";
                    textBox11.Text = "";
                    textBox12.Text = "";
                    textBox13.Text = "";
                    textBox14.Text = "";
                    textBox15.Text = "";
                    textBox16.Text = "";
                    textBox17.Text = "";
                    //textBox18.Text = "";
                    textBox19.Text = "";
                    textBox20.Text = "";
                }

            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, object> retData = new Dictionary<string, object>();
                retData.Add("TYPE", comboBox4.Text);
                string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "SJ_TSMAPI",
                    "SJ_TSMAPI.Registration",
                    "GetTypeOfProcess",
                    Program.Client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());

                    comboBox3.Items.Clear();
                    comboBox3.Text = "";

                    if (dtJson1.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtJson1.Rows)
                        {
                            comboBox3.Items.Add(dr["NAME"].ToString());

                        }
                    }

                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found.");
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }
    }
}
