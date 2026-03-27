using Newtonsoft.Json;
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
using AutocompleteMenuNS;
using SJeMES_Control_Library;
using System.IO;
using System.Drawing.Imaging;

namespace T_Quick_Changeover
{

    public partial class T_QCO_Checklist2 : Form
    {
        AutoCompleteStringCollection Autodata;
        public Boolean isTitle = false;
        IList<object[]> data = null;
        DataTable dt;
        DataTable radiodt;
        DataTable BRD;
        DataTable SBRD;
        public static string COTValue;
        public static string COPTValue;
        public static Image image2;
        public static Image image3;
        public static Image image4;
        public static Image image5;

        public static Image images1;
        public static Image images2;
        public static Image images3;
        public static Image images4;
        DataTable data1;
        string S_NO, Modelfrom, Modelto, COD, line, Article;
        string LINE_UPDATE_TIME, VALUE_UPDATE_TIME, MAN_UPDATE_TIME, MACHINE_UPDATE_TIME, CTPQ_UPDATE_TIME, LAU_UPDATE_TIME, GAUGE_UPDATE_TIME, SAMPLE_SH_UPDATE_TIME, CEMENT_PR_UPDATE_TIME;

        string S_NO1, Modelfrom1, Modelto1, COD1, line1, Article1;
        string S_LINEUPDATE_DATE, S_VALUEUPDATE_DATE, S_REQUIREDUPDATE_DATE, SMACHINUPDATE_DATE, S_CTPQ_UPDATE_DATE, S_SAMPLEUPDATE_DATE, S_GAUGE_UPDATE_DATE, S_MATERIAL_UPDATE_DATE, S_COMPONENT_UPDATE_DATE, S_ELIGIBLE_UPDATE_DATE, S_STITCHINGUPDATE_DATE, S_CS_MC_UPDATE_DATE;
        private bool openTabPage1;
        private bool openTabPage2;
        string dateString, dateString1;
        public void SetOpenTabPage1(bool value)
        {
            openTabPage1 = value;
            BindTabPage();
        }
        public void SetOpenTabPage2(bool value)
        {
            openTabPage2 = value;
            BindTabPage();
        }

        public void BindTabPage()
        {
            if (openTabPage1)
            {
                if (tabPage1 != null)
                {
                    SelectedTab = tabPage1;
                }
            }
            else
            {
                if (tabPage1 != null)
                {
                    SelectedTab = tabPage2;
                }
            }
        }
        public void GetInstance(string COT, string COPT)
        {
            COTValue = COT;
            COPTValue = COPT;
            BindCot(COTValue, COPTValue);
        }

        public void BindCot(string COT, string COPT)
        {
            if (COT == null && COPT == null)
            {

            }
            else
            {
                COTtxt.Text = COT;
                COPTtxt.Text = COPT;
            }
        }
        public void GetInstance1(string COT, string COPT)
        {
            COTValue = COT;
            COPTValue = COPT;
            BindCot1(COTValue, COPTValue);
        }

        public void BindCot1(string COT, string COPT)
        {
            if (COT == null && COPT == null)
            {

            }
            else
            {
                cottxt2.Text = COT;
                copttxt2.Text = COPT;
            }
        }
        private void cotbtn_Click(object sender, EventArgs e)
        {
            int Aline = 1;
            T_QCO_COT_COPT cot = new T_QCO_COT_COPT(Aline);
            cot.Show();
        }

        private void signaturebtn_Click(object sender, EventArgs e)
        {
            int A = 1;
            int indexValue = 1; 

            QCO_Get_Signatures pr = new QCO_Get_Signatures(indexValue,A);
            pr.Show();
        }

        private void pcsigbtn_Click(object sender, EventArgs e)
        {
            int A = 2;
            int indexValue = 2;
            QCO_Get_Signatures pr = new QCO_Get_Signatures(indexValue,A);
            pr.Show();
        }

        private void techsigbtn_Click(object sender, EventArgs e)
        {
            int A = 3;
            int indexValue = 3;
            QCO_Get_Signatures pr = new QCO_Get_Signatures(indexValue,A);
            pr.Show();
        }

        private void mainsigbtn_Click(object sender, EventArgs e)
        {
            int A = 4;
            int indexValue = 4;
            QCO_Get_Signatures pr = new QCO_Get_Signatures(indexValue,A);
            pr.Show();
        }
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
        public void Showimage(Image image)
        {
            image2 = image;
            ShowImageAndBind(image2, pictureBox1);
        }
        public void Shows1(Image img)
        {
            images1 = img;
            ShowImageAndBind(images1, pictureBox9);
        }
        public void Shows2(Image img)
        {
            images2 = img;
            ShowImageAndBind(images2, pictureBox7);
        }
        public void Shows3(Image img)
        {
            images3 = img;
            ShowImageAndBind(images3, pictureBox6);
        }
        public void Shows4(Image img)
        {
            images4 = img;
            ShowImageAndBind(images4, pictureBox5);
        }

        public void Showimage1(Image image)
        {
            image3 = image;
            ShowImageAndBind(image3, pictureBox2);
        }
        public void Showimage2(Image image)
        {
            image4 = image;
            ShowImageAndBind(image4, pictureBox3);
        }
        public void Showimage3(Image image)
        {
            image5 = image;
            ShowImageAndBind(image5, pictureBox4);
        }
       
        public T_QCO_Checklist2()
        {
            InitializeComponent();
            Articletext.Visible = false;
            Aarticlenolbl.Visible = false;
            this.WindowState = FormWindowState.Maximized;
        }

        private void Submitbtn_Click(object sender, EventArgs e)
        {
            Savedata();          
        }
        private double checkedCount = 0;
        private double totalCheckedCount = 0;
        private const double IncrementValue = 11.11111111;
       

        public TabPage SelectedTab { get; private set; }

        private void UpdateProgressBar()
        {
            int maxValue = progressBar1.Maximum;
            double scaledValue = (checkedCount / 100.0) * maxValue;
            progressBar1.Value = Math.Abs((int)Math.Round(scaledValue));
        }

        //private void UpdateCheckedCount(bool isChecked)
        //{
        //    checkedCount += isChecked ? IncrementValue : -IncrementValue;
        //    UpdateProgressBar();
        //}
        private double UpdateCheckedCount(bool isChecked)
        {
            double increment = isChecked ? IncrementValue : -IncrementValue;
            checkedCount += increment;
            totalCheckedCount += increment;
            UpdateProgressBar();
            return totalCheckedCount;
        }        
        private void Linelayoutrbt1_CheckedChanged_1(object sender, EventArgs e)
        {
            double increment = UpdateCheckedCount(Linelayoutrbt1.Checked);
            Progressvalue.Text = $"{increment:F2} %";

            string Line= DateTime.Now.ToString();
            DateTime da = DateTime.Parse(Line);
            string line1 = da.ToString("yyyy/MM/dd");
            line_update_txt.Text = line1;

        }

        private void valuerdb1_CheckedChanged_1(object sender, EventArgs e)
        {
            double increment=  UpdateCheckedCount(valuerdb1.Checked);
            Progressvalue.Text = $"{increment:F2} %";
            string  value = DateTime.Now.ToString();
            DateTime Va = DateTime.Parse(value);
            string val = Va.ToString("yyyy/MM/dd");
            Value_Update_txt.Text = val;
        }

        private void Manprdb1_CheckedChanged_1(object sender, EventArgs e)
        {
            double increment = UpdateCheckedCount(Manprdb1.Checked);
            Progressvalue.Text = $"{increment:F2} %";

            string Man = DateTime.Now.ToString();
            DateTime Ma = DateTime.Parse(Man);
            string man1 = Ma.ToString("yyyy/MM/dd");
            Man_Update_txt.Text = man1;
     
        }

        private void Machinerdb1_CheckedChanged_1(object sender, EventArgs e)
        {
            double increment = UpdateCheckedCount(Machinerdb1.Checked);
            Progressvalue.Text = $"{increment:F2} %";
       
            string Machine = DateTime.Now.ToString();
            DateTime Mac = DateTime.Parse(Machine);
            string manchine1 = Mac.ToString("yyyy/MM/dd");
            Machine_update_txt.Text = manchine1;
        }
    

        private void CTPQrdb1_CheckedChanged_1(object sender, EventArgs e)
        {
            double increment = UpdateCheckedCount(CTPQrdb1.Checked);
            Progressvalue.Text = $"{increment:F2} %";

            string ctpq = DateTime.Now.ToString();
            DateTime CT = DateTime.Parse(ctpq);
            string ctpq1 = CT.ToString("yyyy/MM/dd");
            Ctpq_Update_txt.Text = ctpq1;
        }

        private void materialrdb1_CheckedChanged_1(object sender, EventArgs e)
        {
            double increment = UpdateCheckedCount(materialrdb1.Checked);
            Progressvalue.Text = $"{increment:F2} %";

            string mold = DateTime.Now.ToString();
            DateTime MD = DateTime.Parse(mold);
            string mold1 = MD.ToString("yyyy/MM/dd");
            Mold_Update_txt.Text = mold1;
        }

        private void Gaugerbn1_CheckedChanged_1(object sender, EventArgs e)
        {
            double increment = UpdateCheckedCount(Gaugerbn1.Checked);
            Progressvalue.Text = $"{increment:F2} %";


            string Gauge = DateTime.Now.ToString();
            DateTime Gau = DateTime.Parse(Gauge);
            string gauge1 = Gau.ToString("yyyy/MM/dd");
            Gaue_Update_txt.Text = gauge1;
        }

        private void Sampleshoerbn1_CheckedChanged_1(object sender, EventArgs e)
        {
            double increment = UpdateCheckedCount(Sampleshoerbn1.Checked);
            Progressvalue.Text = $"{increment:F2} %";

            string SOp = DateTime.Now.ToString();
            DateTime sop = DateTime.Parse(SOp);
            string SOP1 = sop.ToString("yyyy/MM/dd");
            Sop_update_txt.Text = SOP1;
        }

        private void cementrbd1_CheckedChanged_1(object sender, EventArgs e)
        {
            double increment = UpdateCheckedCount(cementrbd1.Checked);
            Progressvalue.Text = $"{increment:F2} %";

            // string cement = DateTime.Now.ToString();
            //Cement_update_txt.Text = cement;

            string cement = DateTime.Now.ToString();
            DateTime CEM = DateTime.Parse(cement);
            string cement1 = CEM.ToString("yyyy/MM/dd");
            Cement_update_txt.Text = cement1;

        }     
        private void T_QCO_Checklist2_Load(object sender, EventArgs e)
        {
            LoadModel();
           
            autocompleteMenu1.SetAutocompleteMenu(BMCOtxt, autocompleteMenu1);
            this.MinimumSize = new System.Drawing.Size(this.Width, this.Height);
            this.MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            if (DateTime.TryParse(COD, out DateTime dateTime))
            {
                dateString1 = dateTime.ToString("yyyy/MM/dd");
            }
            QCO_Sequtxt.Text = S_NO;
            BMCOtxt.Text = Modelfrom;
            A_MCOtxt.Text = Modelto;
            linetxt.Text = line;
            COdatetxt.Text = dateString1;
            Articletext.Text = Article;

            line_update_txt.Text = LINE_UPDATE_TIME;
            Value_Update_txt.Text = VALUE_UPDATE_TIME;
            Man_Update_txt.Text = MAN_UPDATE_TIME;
            Machine_update_txt.Text = MACHINE_UPDATE_TIME;
            Ctpq_Update_txt.Text = CTPQ_UPDATE_TIME;
            Mold_Update_txt.Text = LAU_UPDATE_TIME;
            Gaue_Update_txt.Text = GAUGE_UPDATE_TIME;
            Sop_update_txt.Text = SAMPLE_SH_UPDATE_TIME;
            Cement_update_txt.Text = CEMENT_PR_UPDATE_TIME;

            if (DateTime.TryParse(COD1, out DateTime dateTime1))
            { 
                 dateString = dateTime1.ToString("yyyy/MM/dd");
            }
            sseqtxt.Text = S_NO1;
            sdpttxt.Text = line1;
            scotxt.Text = dateString;
            sbmodeltxt.Text= Modelfrom1;
            samodeltxt.Text = Modelto1;
            Aarttxt.Text= Article1;

            S_line_time_txt.Text = S_LINEUPDATE_DATE;
            S_vs_time_txt.Text = S_VALUEUPDATE_DATE;
            S_mp_time_txt.Text = S_REQUIREDUPDATE_DATE;
            S_man_time_txt.Text = SMACHINUPDATE_DATE;
            S_ctpt_time_txt.Text = S_CTPQ_UPDATE_DATE;
            S_sop_time_txt.Text = S_SAMPLEUPDATE_DATE;
            S_gau_time_txt.Text = S_GAUGE_UPDATE_DATE;
            S_mold_time_txt.Text = S_MATERIAL_UPDATE_DATE;
            S_com_time_txt.Text = S_COMPONENT_UPDATE_DATE;
            S_eq_time_txt.Text = S_ELIGIBLE_UPDATE_DATE;
            S_cm_time_txt.Text = S_STITCHINGUPDATE_DATE;
            S_cs_time_txt.Text = S_CS_MC_UPDATE_DATE;
            // Get the date from the TextBox
            if (DateTime.TryParse(COdatetxt.Text, out DateTime enteredDate))
            {                
                string enteredDat = enteredDate.ToString("yyyy/MM/dd");
                DateTime givenCOD = DateTime.Parse(enteredDat);
                DateTime currentDate = DateTime.Now;
                string cur = currentDate.ToString("yyyy/MM/dd");
                DateTime curdate = DateTime.Parse(cur);
                TimeSpan difference = givenCOD - curdate;
                int daysDifference = difference.Days;
                DateTime fourDaysBefore = enteredDate.AddDays(-4);
                if (progressBar1.Value == 100)
                {
                    fourlbl.Text = "COD Completed";
                    threelbl.Text = "COD Completed";
                    panel7.BackColor = Color.Red;
                    panel8.BackColor = Color.Red;
                }
                else
                {
                    if (daysDifference < 0)
                    {
                        panel7.BackColor = Color.Red;
                        panel8.BackColor = Color.Red;
                        fourlbl.Text = "COD date already Crossed";
                        threelbl.Text= "COD date already Crossed";
                    }
                    else
                    {
                        switch (daysDifference)
                        {
                            case 4:
                                panel7.BackColor = Color.Green;
                                fourlbl.Text = "Four days to go";
                                break;
                            case 3:
                                panel7.BackColor = Color.Yellow;
                                fourlbl.Text = "Three days to go";
                                break;
                            case 2:
                                panel7.BackColor = Color.Orange;
                                fourlbl.Text = "Two days to go";
                                break;
                            case 1:
                                panel7.BackColor = Color.Red;
                                fourlbl.Text = "One day to go";
                                break;
                            case 0:
                                panel7.BackColor = Color.DeepPink;
                                fourlbl.Text = "Need to Change Over today";
                                break;
                            default:
                                panel7.BackColor = Color.Gray;
                                fourlbl.Text = "More than 4 Days";
                                break;
                        }
                        // Panel 2
                        switch (daysDifference)
                        {
                            case 3:
                                panel8.BackColor = Color.Yellow;
                                threelbl.Text = "Three days to go";
                                break;
                            case 2:
                                panel8.BackColor = Color.Orange;
                                threelbl.Text = "Two days to go";
                                break;
                            case 1:
                                panel8.BackColor = Color.Red;
                                threelbl.Text = "One day to go";
                                break;
                            case 0:
                                panel8.BackColor = Color.DeepPink;
                                threelbl.Text = "Need to Change Over today";
                                break;
                            default:
                                panel8.BackColor = Color.Gray;
                                threelbl.Text = "More than 3 Days";
                                break;
                        }
                    }
                }
            }
            else if (DateTime.TryParse(scotxt.Text, out DateTime enteredDate1))
            {
                string enteredDat = enteredDate1.ToString("yyyy/MM/dd");
                DateTime givenCOD = DateTime.Parse(enteredDat);
                DateTime currentDate = DateTime.Now;
                string cur = currentDate.ToString("yyyy/MM/dd");
                DateTime curdate = DateTime.Parse(cur);
                TimeSpan difference = givenCOD - curdate;
                int daysDifference = difference.Days;
                if (progressBar2.Value == 100)
                {
                    sfourlbl.Text = "COD Completed";
                    Sthreelbl.Text = "COD Completed";
                    panel14.BackColor = Color.Red;
                    panel13.BackColor = Color.Red;
                }
                else
                {
                    if (daysDifference < 0)
                    {
                        panel14.BackColor = Color.Red;
                        panel13.BackColor = Color.Red;
                        sfourlbl.Text = "COD date already Crossed";
                        Sthreelbl.Text= "COD date already Crossed";
                    }
                    else
                    {
                        switch (daysDifference)
                        {
                            case 4:
                                panel14.BackColor = Color.Green;
                                string text = "Three days to go";
                                sfourlbl.Text = text;
                                break;
                            case 3:
                                panel14.BackColor = Color.Yellow;
                                sfourlbl.Text = "Three days to go";
                                break;
                            case 2:
                                panel14.BackColor = Color.Orange;
                                sfourlbl.Text = "Two days to go";
                                break;
                            case 1:
                                panel14.BackColor = Color.Red;
                                sfourlbl.Text = "One day to go";
                                break;
                            case 0:
                                panel14.BackColor = Color.DeepPink;
                                sfourlbl.Text = "Need to Change Over today";
                                break;
                            default:
                                panel14.BackColor = Color.Gray;
                                sfourlbl.Text = "More than 4 Days";
                                break;
                        }
                        // Panel 2
                        switch (daysDifference)
                        {
                            case 3:
                                panel13.BackColor = Color.Yellow;
                                Sthreelbl.Text = "Three days to go";
                                break;
                            case 2:
                                panel13.BackColor = Color.Orange;
                                Sthreelbl.Text = "Two days to go";
                                break;
                            case 1:
                                panel13.BackColor = Color.Red;
                                Sthreelbl.Text = "One day to go";
                                break;
                            case 0:
                                panel13.BackColor = Color.DeepPink;
                                Sthreelbl.Text = "Need to Change Over today";
                                break;
                            default:
                                panel13.BackColor = Color.Gray;
                                Sthreelbl.Text = "More than 3 Days";
                                break;
                        }
                    }    
                }  
            }
          
            else
            {

                MessageBox.Show("Invalid date format. Please enter a valid date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        public void LoadModel()
        {
            BMCOtxt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            BMCOtxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
            Autodata = new AutoCompleteStringCollection();
            dt = new DataTable();
            Dictionary<string, string> kk = new Dictionary<string, string>();
            string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_QCO", "KZ_QCO.Controllers.GeneralServer", "AutoMChange",
                Program.Client.UserToken, JsonConvert.SerializeObject(kk));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = JsonConvert.DeserializeObject<DataTable>(json);
                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }
                else
                {
                    autocompleteMenu1.MaximumSize = new Size(250, 350);
                    var columnWidth = new[] { 50, 200 };
                    int n = 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["B_MODEL"].ToString() }, dt.Rows[i]["B_MODEL"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                        n++;
                    }
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        internal void SetData(DataTable selectedRowData)
        {
            data1 = selectedRowData;
            for (int i = 0; i < data1.Rows.Count; i++)
            {
                S_NO = data1.Rows[i]["QCO_SEQUENCE"].ToString();
                Article = data1.Rows[i]["A_ART_NO"].ToString();
                Modelfrom = data1.Rows[i]["B_MODEL"].ToString();
                Modelto = data1.Rows[i]["A_MODEL"].ToString();
                COD = data1.Rows[i]["CO_DATE"].ToString();
                line = data1.Rows[i]["DEPARTMENT_CODE"].ToString();
            }          
            this.Refresh();
            GetRadiodata();
            tabPage1.Visible = true;
            tabPage2.Visible = false;
            tabControl1.TabPages.Remove(tabPage2);


        }
        internal void SetData2(DataTable selectedRowData)
        {
            data1 = selectedRowData;
            for (int i = 0; i < data1.Rows.Count; i++)
            {

                S_NO1 = data1.Rows[i]["QCO_SEQUENCE"].ToString();
                Article1 = data1.Rows[i]["A_ART_NO"].ToString();
                Modelfrom1 = data1.Rows[i]["B_MODEL"].ToString();
                Modelto1 = data1.Rows[i]["A_MODEL"].ToString();   
                COD1 = data1.Rows[i]["CO_DATE"].ToString();
                line1 = data1.Rows[i]["DEPARTMENT_CODE"].ToString();
            }
            this.Refresh();
            GetRadiodata1();      
            tabPage2.Visible = true;
            tabPage1.Visible = false;
            tabControl1.TabPages.Remove(tabPage1);
           // tabControl1.TabPages.Add(tabPage2);
        }
        public void GetAsemblyPictureImages()
        {
            List<Image> images = new List<Image>();
            byte[] img1;
            byte[] img2;
            byte[] img3;
            byte[] img4;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("QCO_Sequtxt", S_NO.ToString());

            string apiResponse = WebAPIHelper.Post(Program.Client.APIURL, "KZ_QCO", "KZ_QCO.Controllers.GeneralServer", "Getassemblypicture",
                Program.Client.UserToken, JsonConvert.SerializeObject(parameters));
            string retDataJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(apiResponse)?["RetData"]?.ToString();
            var response = JsonConvert.DeserializeObject<Dictionary<string, object>>(apiResponse);

            if (Convert.ToBoolean(response["IsSuccess"]))
            {
                if (response.ContainsKey("RetData"))
                {
 
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(apiResponse)["RetData"].ToString();
                    Dictionary<string, object> dic2 = new Dictionary<string, object>();
                    dic2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);  
                    SetImageFromDictionaryKey(dic2, "SECHEAD_NAME", pictureBox1);
                    SetImageFromDictionaryKey(dic2, "PC_NAME", pictureBox2);
                    SetImageFromDictionaryKey(dic2, "TECHNICAL_NAME", pictureBox3);
                    SetImageFromDictionaryKey(dic2, "MAINTENANCE_NAME", pictureBox4);
                }
                else
                {
                    MessageBox.Show("RetData does not contain the expected property.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageHelper.ShowErr(this, response["ErrMsg"].ToString());
            }
      
        }
        public void GetstitchPictureImages()
        {
            List<Image> images = new List<Image>();
            byte[] img1;
            byte[] img2;
            byte[] img3;
            byte[] img4;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("QCO_Sequtxt", S_NO1.ToString());//sseqtxt

            string apiResponse = WebAPIHelper.Post(Program.Client.APIURL, "KZ_QCO", "KZ_QCO.Controllers.GeneralServer", "Getstitchpictures",
                Program.Client.UserToken, JsonConvert.SerializeObject(parameters));
            string retDataJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(apiResponse)?["RetData"]?.ToString();
            var response = JsonConvert.DeserializeObject<Dictionary<string, object>>(apiResponse);

            if (Convert.ToBoolean(response["IsSuccess"]))
            {
                if (response.ContainsKey("RetData"))
                {

                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(apiResponse)["RetData"].ToString();
                    Dictionary<string, object> dic2 = new Dictionary<string, object>();
                    dic2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                    SetImageFromDictionaryKey(dic2, "SECHEAD_NAME", pictureBox9);
                    SetImageFromDictionaryKey(dic2, "PC_NAME", pictureBox7);
                    SetImageFromDictionaryKey(dic2, "TECHNICAL_NAME", pictureBox6);
                    SetImageFromDictionaryKey(dic2, "MAINTENANCE_NAME", pictureBox5);
                }
                else
                {
                    MessageBox.Show("RetData does not contain the expected property.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageHelper.ShowErr(this, response["ErrMsg"].ToString());
            }

        }
        void SetImageFromDictionaryKey(Dictionary<string, object> dic, string key, PictureBox pictureBox)
        {
            if (dic.ContainsKey(key))
            {
                string base64String = dic[key]?.ToString()?.Replace("\"", "");

                if (!string.IsNullOrEmpty(base64String))
                {
                    byte[] imgBytes = Convert.FromBase64String(base64String);
                    Image image = ByteArrayToImage(imgBytes);

                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox.Image = image;
                }
            }
        }


        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }



        public void GetRadiodata()
        {
           
            Dictionary<string, string> kk = new Dictionary<string, string>();
            kk.Add("QCO_Sequtxt", S_NO.ToString());
            string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_QCO", "KZ_QCO.Controllers.GeneralServer", "GetRadiodata",
                Program.Client.UserToken, JsonConvert.SerializeObject(kk));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                radiodt = JsonConvert.DeserializeObject<DataTable>(json);
                if (radiodt == null || radiodt.Rows.Count == 0)
                {
                    return;
                }
           
                else
                {             
                    RadioButton[] radioButtons = { Linelayoutrbt1, valuerdb1, Manprdb1, Machinerdb1, CTPQrdb1, materialrdb1, Gaugerbn1, Sampleshoerbn1, cementrbd1};

                    if (radiodt != null && radiodt.Rows.Count > 0 && radiodt.Columns.Contains("RECORD_PROBLEMS") && radiodt.Columns.Contains("COT") && radiodt.Columns.Contains("COPT"))
                    {
                        probtxt.Text = radiodt.Rows[0]["RECORD_PROBLEMS"].ToString();
                        COTtxt.Text = radiodt.Rows[0]["COT"].ToString();
                        COPTtxt.Text = radiodt.Rows[0]["COPT"].ToString();

                        var planTypeValue = radiodt.Rows[0]["S_PLAN_TYPE"];
                        if (planTypeValue != DBNull.Value && planTypeValue != null)
                        {
                            string planType = planTypeValue.ToString();
                            if (A_Combobox.Items.Contains(planType))
                            {

                                A_Combobox.SelectedItem = planType;
                            }
                            else
                            {
                                MessageBox.Show($"The plan type '{planType}' is not available in the ComboBox.", "Item Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {

                            MessageBox.Show("The plan type is null or invalid.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            A_Combobox.SelectedItem = null;
                        }
                    }

                    for (int i = 0; i < radioButtons.Length; i++)
                    {
                    
                        if (radiodt.Rows[0][i].ToString() == "Normal")
                        {
                            radioButtons[i].Checked = true;
                        }
                  
                    }
                    GetAsemblyPictureImages();
                    BindRaddiodates();
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        public void BindRaddiodates()
        {

            Dictionary<string, string> kk = new Dictionary<string, string>();
            kk.Add("QCO_Sequtxt", S_NO.ToString());
            string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_QCO", "KZ_QCO.Controllers.GeneralServer", "BindRaddiodates",
                Program.Client.UserToken, JsonConvert.SerializeObject(kk));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
               BRD = JsonConvert.DeserializeObject<DataTable>(json);
                if (BRD == null || BRD.Rows.Count == 0)
                {
                    return;
                }
             
                else
                {
                    for (int i = 0; i < BRD.Rows.Count; i++)
                    {
                        LINE_UPDATE_TIME = BRD.Rows[i]["LINE_LAY_UPDATE_DATE"].ToString();
                        VALUE_UPDATE_TIME = BRD.Rows[i]["VALUE_STR_UPDATE_DATE"].ToString();
                        MAN_UPDATE_TIME = BRD.Rows[i]["REQUIRED_MAN_UPDATE_DATE"].ToString();
                        MACHINE_UPDATE_TIME = BRD.Rows[i]["MACHINE_IN_UPDATE_DATE"].ToString();
                        CTPQ_UPDATE_TIME = BRD.Rows[i]["CTPQ_UPDATE_DATE"].ToString();
                        LAU_UPDATE_TIME = BRD.Rows[i]["MATERIAL_LAU_UPDATE_DATE"].ToString();
                        GAUGE_UPDATE_TIME = BRD.Rows[i]["GAUGE_MAR_UPDATE_DATE"].ToString();
                        SAMPLE_SH_UPDATE_TIME = BRD.Rows[i]["SAMPLE_SH_UPDATE_DATE"].ToString();
                        CEMENT_PR_UPDATE_TIME = BRD.Rows[i]["CEMENT_PR_UPDATE_DATE"].ToString();
                    }
                }
                
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void SSubmit_Click(object sender, EventArgs e)
        {
            Savedata1();
        }
       
        public void Savedata1()
        {
            if(comboBox1.SelectedIndex == -1)
            {
                MessageHelper.ShowErr(this, "Please Select Plan type");
                return;
            }
            if(comboBox1.SelectedIndex == 1 )
            {
               if( problemin_stit_txt.Text == "")
                {
                    MessageHelper.ShowErr(this, "You need to Mantion reason for Un_Schedule Plan");
                    return;
                }
            }
            byte[] byteArray1 = PictureBoxToByteArray(pictureBox9);
            byte[] byteArray2 = PictureBoxToByteArray(pictureBox7);
            byte[] byteArray3 = PictureBoxToByteArray(pictureBox6);
            byte[] byteArray4 = PictureBoxToByteArray(pictureBox6);
            if (string.IsNullOrEmpty(sbmodeltxt.Text) && string.IsNullOrEmpty(scotxt.Text))
            {
                ShowErrorMessage("Before submit, Please Enter Model name");
                return;
            }       
            if (!ValidateSection(Slinerbtn2, Slinertxt))
                return;
            if (!ValidateSection(Svaluesrbtn2, Svaluesrtxt))
                return;
            if (!ValidateSection(SManprbtn2, SManprtxt))
                return;
            if (!ValidateSection(Smachinerbtn2, Smachinertxt))
                return;
            if (!ValidateSection(Sctpqrbtn2, Sctpqrtxt))
                return;
            if (!ValidateSection(Ssamplerbtn2, Ssamplertxt))
                return;
            if (!ValidateSection(Sgaugerbtn2, Sgaugertxt))
                return;
            if (!ValidateSection(Smoldrbtn2, Smoldrtxt))
                return;
            if (!ValidateSection(SCcomprbtn2, SCcomprtxt))
                return;
            if (!ValidateSection(Seligibrbtn2, Seligibtxt))
                return;
            if (!ValidateSection(SStitchingrbtn2, SStitchingrtxt))
                return;
            if (!ValidateSection(Scsrbtn2, Scsrtxt))
                return;

            Dictionary<string, object> kk = new Dictionary<string, object>();
            kk.Add("BModel", sbmodeltxt.Text.ToString());
            kk.Add("AfModel", samodeltxt.Text.ToString());
            kk.Add("COdate", scotxt.Text.ToString());
            kk.Add("Prodline", sdpttxt.Text.ToString());
            if (Slinerbtn1.Checked)
            {
                kk.Add("Linelay", Slinerbtn1.Text.ToString());
            }
            else
            {
                kk.Add("Linelay", Slinerbtn2.Text.ToString());
            }
            if (Svaluesrbtn1.Checked)
            {
                kk.Add("valuer", Svaluesrbtn1.Text.ToString());
            }
            else
            {
                kk.Add("valuer", Svaluesrbtn2.Text.ToString());
            }
            if (SManprbtn1.Checked)
            {
                kk.Add("Manpr", SManprbtn1.Text.ToString());
            }
            else
            {
                kk.Add("Manpr", SManprbtn2.Text.ToString());
            }
            if (Smachinerbtn1.Checked)
            {
                kk.Add("Machine", Smachinerbtn1.Text.ToString());
            }
            else
            {
                kk.Add("Machine", Smachinerbtn2.Text.ToString());
            }
            if (Sctpqrbtn1.Checked)
            {
                kk.Add("CTPQr", Sctpqrbtn1.Text.ToString());
            }
            else
            {
                kk.Add("CTPQr", Sctpqrbtn2.Text.ToString());
            }
            if (Ssamplerbtn1.Checked)
            {
                kk.Add("material", Ssamplerbtn1.Text.ToString());
            }
            else
            {
                kk.Add("material", Ssamplerbtn2.Text.ToString());
            }
            if (Sgaugerbtn1.Checked)
            {
                kk.Add("Gauge", Sgaugerbtn1.Text.ToString());
            }
            else
            {
                kk.Add("Gauge", Sgaugerbtn2.Text.ToString());
            }
            if (Smoldrbtn1.Checked)
            {
                kk.Add("Sampleshoe", Smoldrbtn1.Text.ToString());
            }
            else
            {
                kk.Add("Sampleshoe", Smoldrbtn2.Text.ToString());
            }
            if (SCcomprbtn1.Checked)
            {
                kk.Add("Component", SCcomprbtn1.Text.ToString());
            }
            else
            {
                kk.Add("Component", SCcomprbtn2.Text.ToString());
            }
            if (Seligibrbtn1.Checked)
            {
                kk.Add("Eligible", Seligibrbtn1.Text.ToString());
            }
            else
            {
                kk.Add("Eligible", Seligibrbtn2.Text.ToString());
            }
            if (SStitchingrbtn1.Checked)
            {
                kk.Add("Stitch", SStitchingrbtn1.Text.ToString());
            }
            else
            {
                kk.Add("Stitch", SStitchingrbtn2.Text.ToString());
            }
            if (Scsrbtn1.Checked)
            {
                kk.Add("CS", Scsrbtn1.Text.ToString());
            }
            else
            {
                kk.Add("CS", Scsrbtn2.Text.ToString());
            }

            if (!string.IsNullOrEmpty(Slinertxt.Text))
            {
                kk.Add("Remarks", Slinertxt.Text);
            }
            if (!string.IsNullOrEmpty(Svaluesrtxt.Text))
            {
                kk.Add("Remarks", Svaluesrtxt.Text);
            }
            if (!string.IsNullOrEmpty(SManprtxt.Text))
            {
                kk.Add("Remarks", SManprtxt.Text);
            }
            if (!string.IsNullOrEmpty(Smachinertxt.Text))
            {
                kk.Add("Remarks", Smachinertxt.Text);
            }
            if (!string.IsNullOrEmpty(Sctpqrtxt.Text))
            {
                kk.Add("Remarks", Sctpqrtxt.Text);
            }
            else
            {
                kk.Add("Remarks", "All is Ok".ToString());
            }       
            kk.Add("SectionHd", byteArray1 != null ? byteArray1 : (object)DBNull.Value);
            kk.Add("Plantin", byteArray2 != null ? byteArray2 : (object)DBNull.Value);
            kk.Add("Technic", byteArray3 != null ? byteArray3 : (object)DBNull.Value);
            kk.Add("Maintenance", byteArray4 != null ? byteArray4 : (object)DBNull.Value);

            kk.Add("COT", cottxt2.Text.ToString());
            kk.Add("COPT", copttxt2.Text.ToString());
            kk.Add("QCO_Seq", sseqtxt.Text.ToString());
            kk.Add("Co_status", progressBar2.Value);
            kk.Add("problemin_stit", problemin_stit_txt.Text);

            kk.Add("S_line", S_line_time_txt.Text.ToString());
            kk.Add("S_vs", S_vs_time_txt.Text.ToString());
            kk.Add("S_mp", S_mp_time_txt.Text.ToString());
            kk.Add("S_man", S_man_time_txt.Text);
            kk.Add("S_ctpt", S_ctpt_time_txt.Text);
            kk.Add("S_sop", S_sop_time_txt.Text);
            kk.Add("S_gau", S_gau_time_txt.Text);
            kk.Add("S_mold", S_mold_time_txt.Text);
            kk.Add("S_com", S_com_time_txt.Text);
            kk.Add("S_eq", S_eq_time_txt.Text);
            kk.Add("S_cm", S_cm_time_txt.Text);
            kk.Add("S_cs", S_cs_time_txt.Text);

            kk.Add("S_plan", comboBox1.SelectedItem.ToString());


            if (progressBar2.Value == 100)
            {
                if (pictureBox9.Image == null || pictureBox7.Image == null || pictureBox6.Image == null || pictureBox5.Image == null)
                {
                    MessageHelper.ShowErr(this, "Before submit, Upload all Signatures");
                    return;
                }
            }

            string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_QCO", "KZ_QCO.Controllers.GeneralServer", "SChecklist",
                Program.Client.UserToken, JsonConvert.SerializeObject(kk));
            ProcessResponse(ret);           
            ClearFields();
        }

        private void ShowErrorMessage(string message)
        {
            MessageHelper.ShowErr(this, message);
        }

        private bool ValidateSection(RadioButton radioButton, TextBox textBox)
        {
            if (radioButton.Checked && string.IsNullOrEmpty(textBox.Text))
            {
                ShowErrorMessage("Please Mention the Remark");
                return false;
            }
            return true;
        }
       
        private void spcsigbtn_Click(object sender, EventArgs e)
        {
            int s = 6;
            int indexValue = 2;
            QCO_Get_Signatures pr = new QCO_Get_Signatures(indexValue, s);
            pr.Show();

        }

        private void Stechsigbtn_Click(object sender, EventArgs e)
        {
            int s = 7;
            int indexValue = 3;
            QCO_Get_Signatures pr = new QCO_Get_Signatures(indexValue, s);
            pr.Show();
        }

        private void smainsigbtn_Click(object sender, EventArgs e)
        {
            int s = 8;
            int indexValue = 4;
            QCO_Get_Signatures pr = new QCO_Get_Signatures(indexValue, s);
            pr.Show();
        }

        private void COTcalbtn_Click(object sender, EventArgs e)
        {
            int Sline = 2;
            T_QCO_COT_COPT cot = new T_QCO_COT_COPT(Sline);
            cot.Show();
        }
 
        private void getssigtxt_Click(object sender, EventArgs e)
        {

            int s = 5;
            int indexValue = 1;
            QCO_Get_Signatures pr = new QCO_Get_Signatures(indexValue, s);
            pr.Show();

        }
        private void Slineuploadbtn_Click_1(object sender, EventArgs e)
        {
            string seq = sseqtxt.Text;
            //  T_QCO_Ex_app_t_fileUpload_add File = new T_QCO_Ex_app_t_fileUpload_add();
            QCO_FileUpload File = new QCO_FileUpload();
            File.Addseq(seq);
            File.Show();
        }

        private void Slineviewbtn_Click_1(object sender, EventArgs e)
        {
            string seq = sseqtxt.Text;
            //T_QCO_Ex_app_t_fileUpload_view view = new T_QCO_Ex_app_t_fileUpload_view();
            QCO_Upload view = new QCO_Upload();
            view.Addseq2(seq);
            view.Show();

        }

        private void Svsuplodbtn_Click_1(object sender, EventArgs e)
        {
            string seq = sseqtxt.Text;
            // T_QCO_Ex_app_t_fileUpload_add File = new T_QCO_Ex_app_t_fileUpload_add();
            QCO_FileUpload File = new QCO_FileUpload();
            File.Addseq(seq);
            File.Show();

        }

        private void Svsviewbtn_Click_1(object sender, EventArgs e)
        {
            string seq = sseqtxt.Text;
            // T_QCO_Ex_app_t_fileUpload_view view = new T_QCO_Ex_app_t_fileUpload_view();
            QCO_Upload view = new QCO_Upload();
            view.Addseq2(seq);
            view.Show();

        }

        private void Smpuploadbtn_Click_1(object sender, EventArgs e)
        {
            string seq = sseqtxt.Text;
            //T_QCO_Ex_app_t_fileUpload_add File = new T_QCO_Ex_app_t_fileUpload_add();
            QCO_FileUpload File = new QCO_FileUpload();
            File.Addseq(seq);
            File.Show();
        }

        private void Smpviewbtn_Click_1(object sender, EventArgs e)
        {
            string seq = sseqtxt.Text;
            //T_QCO_Ex_app_t_fileUpload_view view = new T_QCO_Ex_app_t_fileUpload_view();
            QCO_Upload view = new QCO_Upload();
            view.Addseq2(seq);
            view.Show();

        }

        private void SMachinebtn_Click_1(object sender, EventArgs e)
        {
            //S_NO1, Modelfrom1, Modelto1, COD1, line1, Article1;
            string S_NO1 = sseqtxt.Text;
            string Modelfrom1 = sbmodeltxt.Text;
            string Modelto1 = samodeltxt.Text;
            string line1 = sdpttxt.Text;
            string COD1 = scotxt.Text;
            T_QCO_Equipment_Request newForm = new T_QCO_Equipment_Request();
            newForm.GetInstance(S_NO1, Modelfrom1, Modelto1, line1, COD1);
            newForm.Show();

        }

        private void Sctpqupload_Click_1(object sender, EventArgs e)
        {
            string seq = sseqtxt.Text;
            //T_QCO_Ex_app_t_fileUpload_add File = new T_QCO_Ex_app_t_fileUpload_add();
            QCO_FileUpload File = new QCO_FileUpload();
            File.Addseq(seq);
            File.Show();

        }

        private void Sctpqview_Click_1(object sender, EventArgs e)
        {
            string seq = sseqtxt.Text;
            //T_QCO_Ex_app_t_fileUpload_view view = new T_QCO_Ex_app_t_fileUpload_view();
            QCO_Upload view = new QCO_Upload();
            view.Addseq2(seq);
            view.Show();

        }

        private void Ssampbtn_Click_1(object sender, EventArgs e)
        {
            string Article1 = Aarttxt.Text;
            string Modelfrom1 = sbmodeltxt.Text;
            string Modelto1 = samodeltxt.Text;
            string line1 = sdpttxt.Text;
            string COD1 = scotxt.Text;

            T_QCO_SampleShoe shoe = new T_QCO_SampleShoe();
            shoe.GetInstance(Article1, Modelfrom1, Modelto1, line1, COD1);
            shoe.Show();

        }

        private void Ssopview_Click_1(object sender, EventArgs e)
        {
            string Article1 = Aarttxt.Text;
            string Modelfrom1 = sbmodeltxt.Text;
            string Modelto1 = samodeltxt.Text;
            string line1 = sdpttxt.Text;
            string COD1 = scotxt.Text;
            T_QCO_SOP_Require SOP = new T_QCO_SOP_Require();
            SOP.GetInstance(Article1, Modelfrom1, Modelto1, line1, COD1);
            SOP.Show();

        }

        private void Ssamupload_Click_1(object sender, EventArgs e)
        {
            string seq = sseqtxt.Text;
            //T_QCO_Ex_app_t_fileUpload_add File = new T_QCO_Ex_app_t_fileUpload_add();
            QCO_FileUpload File = new QCO_FileUpload();
            File.Addseq(seq);
            File.Show();
        }

        private void Ssampview_Click_1(object sender, EventArgs e)
        {
            string seq = sseqtxt.Text;
            //T_QCO_Ex_app_t_fileUpload_view view = new T_QCO_Ex_app_t_fileUpload_view();
            QCO_Upload view = new QCO_Upload();
            view.Addseq2(seq);
            view.Show();
        }

        private void Sgauuploadbtn_Click_1(object sender, EventArgs e)
        {
            string seq = sseqtxt.Text;
            // T_QCO_Ex_app_t_fileUpload_add File = new T_QCO_Ex_app_t_fileUpload_add();
            QCO_FileUpload File = new QCO_FileUpload();
            File.Addseq(seq);
            File.Show();
        }

        private void Sgaugeview_Click_1(object sender, EventArgs e)
        {
            string seq = sseqtxt.Text;
            //T_QCO_Ex_app_t_fileUpload_view view = new T_QCO_Ex_app_t_fileUpload_view();
            QCO_Upload view = new QCO_Upload();
            view.Addseq2(seq);
            view.Show();

        }

        private void Smoldupload_Click_1(object sender, EventArgs e)
        {
            string seq = sseqtxt.Text;
            //T_QCO_Ex_app_t_fileUpload_add File = new T_QCO_Ex_app_t_fileUpload_add();
            QCO_FileUpload File = new QCO_FileUpload();
            File.Addseq(seq);
            File.Show();

        }

        private void Smoldview_Click_1(object sender, EventArgs e)
        {
            string seq = sseqtxt.Text;
            //T_QCO_Ex_app_t_fileUpload_view view = new T_QCO_Ex_app_t_fileUpload_view();
            QCO_Upload view = new QCO_Upload();
            view.Addseq2(seq);
            view.Show();
        }

        private void Scaupload_Click_1(object sender, EventArgs e)
        {
            string seq = sseqtxt.Text;
            //T_QCO_Ex_app_t_fileUpload_add File = new T_QCO_Ex_app_t_fileUpload_add();
            QCO_FileUpload File = new QCO_FileUpload();
            File.Addseq(seq);
            File.Show();
        }

        private void Scaview_Click_1(object sender, EventArgs e)
        {
            string seq = sseqtxt.Text;
            //T_QCO_Ex_app_t_fileUpload_view view = new T_QCO_Ex_app_t_fileUpload_view();
            QCO_Upload view = new QCO_Upload();
            view.Addseq2(seq);
            view.Show();

        }

        private void Selibupload_Click_1(object sender, EventArgs e)
        {
            string seq = sseqtxt.Text;
            // T_QCO_Ex_app_t_fileUpload_add File = new T_QCO_Ex_app_t_fileUpload_add();
            QCO_FileUpload File = new QCO_FileUpload();
            File.Addseq(seq);
            File.Show();
        }

        private void Seligview_Click_1(object sender, EventArgs e)
        {
            string seq = sseqtxt.Text;
            //T_QCO_Ex_app_t_fileUpload_view view = new T_QCO_Ex_app_t_fileUpload_view();
            QCO_Upload view = new QCO_Upload();
            view.Addseq2(seq);
            view.Show();
        }

        private void Sstitupload_Click_1(object sender, EventArgs e)
        {
            string seq = sseqtxt.Text;
            //T_QCO_Ex_app_t_fileUpload_add File = new T_QCO_Ex_app_t_fileUpload_add();
            QCO_FileUpload File = new QCO_FileUpload();
            File.Addseq(seq);
            File.Show();
        }

        private void Sstictview_Click_1(object sender, EventArgs e)
        {
            string seq = sseqtxt.Text;
            //T_QCO_Ex_app_t_fileUpload_view view = new T_QCO_Ex_app_t_fileUpload_view();
            QCO_Upload view = new QCO_Upload();
            view.Addseq2(seq);
            view.Show();

        }

        private void Scsupload_Click_1(object sender, EventArgs e)
        {
            string seq = sseqtxt.Text;
            // T_QCO_Ex_app_t_fileUpload_add File = new T_QCO_Ex_app_t_fileUpload_add();
            QCO_FileUpload File = new QCO_FileUpload();
            File.Addseq(seq);
            File.Show();

        }

        private void Scsview_Click_1(object sender, EventArgs e)
        {
            string seq = sseqtxt.Text;
            //T_QCO_Ex_app_t_fileUpload_view view = new T_QCO_Ex_app_t_fileUpload_view();
            QCO_Upload view = new QCO_Upload();
            view.Addseq2(seq);
            view.Show();
        }

        private double checkedcount1 = 0;
        private int totalRadioButtons = 12;
        private int checkedRadioButtons = 0; // Track the number of checked radio buttons

        private void UpdateProgressBar2()
        {
            int minValue = progressBar2.Minimum;
            int maxValue = progressBar2.Maximum;
            if (checkedRadioButtons == totalRadioButtons)
            {
                progressBar2.Value = maxValue;
            }
            else
            {

            double scaleValue = (checkedRadioButtons * 100.0) / totalRadioButtons;
            scaleValue = Math.Max(minValue, Math.Min(maxValue, scaleValue));
            progressBar2.Value = (int)Math.Round(scaleValue);                
            }
        }

        private void UpdateCheckedCount2(bool isChecked)
        {
            checkedRadioButtons += isChecked ? 1 : -1;
            UpdateProgressBar2();

            // If all radio buttons are checked, update the label
            if (checkedRadioButtons == totalRadioButtons)
            {
                progressBar2.Value = progressBar2.Maximum;
                StitchProgress.Text = "100%";
            }
            else
            {
                StitchProgress.Text = $"{(checkedRadioButtons * 100.0 / totalRadioButtons):F2} %";
            }
        }

        // Event handlers
        private void Slinerbtn1_CheckedChanged_1(object sender, EventArgs e)
        {
            UpdateCheckedCount2(Slinerbtn1.Checked);

            string SLine = DateTime.Now.ToString();
            DateTime da = DateTime.Parse(SLine);
            string line1 = da.ToString("yyyy/MM/dd");
            S_vs_time_txt.Text = line1;
        }


        private void Svaluesrbtn1_CheckedChanged_1(object sender, EventArgs e)
        {
            UpdateCheckedCount2(Svaluesrbtn1.Checked);

            string Svalue = DateTime.Now.ToString();
            DateTime da = DateTime.Parse(Svalue);
            string Svalue1 = da.ToString("yyyy/MM/dd");
            S_line_time_txt.Text = Svalue1;


        }
        private void SManprbtn1_CheckedChanged_1(object sender, EventArgs e)
        {
             UpdateCheckedCount2(SManprbtn1.Checked);

            string SManpr = DateTime.Now.ToString();
            DateTime da = DateTime.Parse(SManpr);
            string SManpr1 = da.ToString("yyyy/MM/dd");

            S_mp_time_txt.Text = SManpr1;
        }

        private void Smachinerbtn1_CheckedChanged_1(object sender, EventArgs e)
        {
            UpdateCheckedCount2(Smachinerbtn1.Checked);

            string Smachin = DateTime.Now.ToString();
            DateTime da = DateTime.Parse(Smachin);
            string Smachin1 = da.ToString("yyyy/MM/dd");

            S_man_time_txt.Text = Smachin1;
        }

        private void Sctpqrbtn1_CheckedChanged_1(object sender, EventArgs e)
        {
            UpdateCheckedCount2(Sctpqrbtn1.Checked);

            string Sctpqr = DateTime.Now.ToString();
            DateTime da = DateTime.Parse(Sctpqr);
            string Sctpqr1 = da.ToString("yyyy/MM/dd");

            S_ctpt_time_txt.Text = Sctpqr1;
        }

        private void Ssamplerbtn1_CheckedChanged_1(object sender, EventArgs e)
        {
             UpdateCheckedCount2(Ssamplerbtn1.Checked);


            string Ssample = DateTime.Now.ToString();
            DateTime da = DateTime.Parse(Ssample);
            string Ssample1 = da.ToString("yyyy/MM/dd");

            S_sop_time_txt.Text = Ssample1;
        }

        private void Sgaugerbtn1_CheckedChanged_1(object sender, EventArgs e)
        {
           UpdateCheckedCount2(Sgaugerbtn1.Checked);


            string Sgauge = DateTime.Now.ToString();
            DateTime da = DateTime.Parse(Sgauge);
            string Sgauge1 = da.ToString("yyyy/MM/dd");

            S_gau_time_txt.Text = Sgauge1;
        }

        private void Smoldrbtn1_CheckedChanged_1(object sender, EventArgs e)
        {
            UpdateCheckedCount2(Smoldrbtn1.Checked);


            string Smold = DateTime.Now.ToString();
            DateTime da = DateTime.Parse(Smold);
            string Smold1 = da.ToString("yyyy/MM/dd");

            S_mold_time_txt.Text = Smold1;
        }

        private void SCcomprbtn1_CheckedChanged_1(object sender, EventArgs e)
        {
             UpdateCheckedCount2(SCcomprbtn1.Checked);


            string SCcom = DateTime.Now.ToString();
            DateTime da = DateTime.Parse(SCcom);
            string SCcom1 = da.ToString("yyyy/MM/dd");

            S_com_time_txt.Text = SCcom1;
        }

        private void Seligibrbtn1_CheckedChanged_1(object sender, EventArgs e)
        {
            UpdateCheckedCount2(Seligibrbtn1.Checked);


            string Seligi = DateTime.Now.ToString();
            DateTime da = DateTime.Parse(Seligi);
            string Seligi1 = da.ToString("yyyy/MM/dd");

            S_eq_time_txt.Text = Seligi1;
        }

        private void SStitchingrbtn1_CheckedChanged_1(object sender, EventArgs e)
        {
            UpdateCheckedCount2(SStitchingrbtn1.Checked);

            string SStitchin = DateTime.Now.ToString();
            DateTime da = DateTime.Parse(SStitchin);
            string SStitchin1 = da.ToString("yyyy/MM/dd");

            S_cm_time_txt.Text = SStitchin1;
        }

        private void Scsrbtn1_CheckedChanged_1(object sender, EventArgs e)
        {
            UpdateCheckedCount2(Scsrbtn1.Checked);


            string Scsr = DateTime.Now.ToString();
            DateTime da = DateTime.Parse(Scsr);
            string Scsr1 = da.ToString("yyyy/MM/dd");

            S_cs_time_txt.Text = Scsr1;
        }

        private void sshsigbtmn_Click(object sender, EventArgs e)
        {

            int s = 5;
            int indexValue = 1;
            QCO_Get_Signatures pr = new QCO_Get_Signatures(indexValue, s);
            pr.Show();
        }

        private void Uploadbtn1_Click_1(object sender, EventArgs e)
        {
            string seq = QCO_Sequtxt.Text;
            Cursor.Current = Cursors.WaitCursor;
            // T_QCO_Ex_app_t_fileUpload_add File = new T_QCO_Ex_app_t_fileUpload_add();
            QCO_FileUpload File = new QCO_FileUpload();
            File.Addseq(seq);
            File.Show();
        }

        private void ViewbtnL_Click_1(object sender, EventArgs e)
        {
            string seq = QCO_Sequtxt.Text;
            // T_QCO_Ex_app_t_fileUpload_view view = new T_QCO_Ex_app_t_fileUpload_view();
            QCO_Upload view = new QCO_Upload();
            view.Addseq2(seq);
            view.Show();
        }

        private void VSuploadbtn_Click_1(object sender, EventArgs e)
        {
            string seq = QCO_Sequtxt.Text;

            // T_QCO_Ex_app_t_fileUpload_add File = new T_QCO_Ex_app_t_fileUpload_add();
            QCO_FileUpload File = new QCO_FileUpload();
            File.Addseq(seq);
            File.Show();
        }

        private void VSviewbtn_Click_1(object sender, EventArgs e)
        {

            string seq = QCO_Sequtxt.Text;
            // T_QCO_Ex_app_t_fileUpload_view view = new T_QCO_Ex_app_t_fileUpload_view();
            QCO_Upload view = new QCO_Upload();
            view.Addseq2(seq);
            view.Show();
        }

        private void Mnuploadbtn_Click_1(object sender, EventArgs e)
        {
            string seq = QCO_Sequtxt.Text;

            // T_QCO_Ex_app_t_fileUpload_add File = new T_QCO_Ex_app_t_fileUpload_add();
            QCO_FileUpload File = new QCO_FileUpload();
            File.Addseq(seq);
            File.Show();
        }

        private void Mnviewbtn_Click_1(object sender, EventArgs e)
        {

            string seq = QCO_Sequtxt.Text;
            // T_QCO_Ex_app_t_fileUpload_view view = new T_QCO_Ex_app_t_fileUpload_view();
            QCO_Upload view = new QCO_Upload();
            view.Addseq2(seq);
            view.Show();
        }

        private void Clickbtn1_Click_1(object sender, EventArgs e)
        {
            string seq = QCO_Sequtxt.Text;
            string Modelfrom = BMCOtxt.Text;
            string Modelto = A_MCOtxt.Text;
            string line = linetxt.Text;
            string COD = COdatetxt.Text;
            T_QCO_Equipment_Request newForm = new T_QCO_Equipment_Request();
            newForm.GetInstance(seq, Modelfrom, Modelto, line, COD);
            newForm.Show();
        }

        private void Qipuploadbtn_Click_1(object sender, EventArgs e)
        {
            string seq = QCO_Sequtxt.Text;

            // T_QCO_Ex_app_t_fileUpload_add File = new T_QCO_Ex_app_t_fileUpload_add();
            QCO_FileUpload File = new QCO_FileUpload();
            File.Addseq(seq);
            File.Show();

        }

        private void Qipviewbtn_Click_1(object sender, EventArgs e)
        {
            string seq = QCO_Sequtxt.Text;
            // T_QCO_Ex_app_t_fileUpload_view view = new T_QCO_Ex_app_t_fileUpload_view();
            QCO_Upload view = new QCO_Upload();
            view.Addseq2(seq);
            view.Show();

        }

        private void Pcuploadbtn_Click_1(object sender, EventArgs e)
        {
            string seq = QCO_Sequtxt.Text;

            // T_QCO_Ex_app_t_fileUpload_add File = new T_QCO_Ex_app_t_fileUpload_add();
            QCO_FileUpload File = new QCO_FileUpload();
            File.Addseq(seq);
            File.Show();

        }

        private void Pcviewbtn_Click_1(object sender, EventArgs e)
        {
            string seq = QCO_Sequtxt.Text;
            // T_QCO_Ex_app_t_fileUpload_view view = new T_QCO_Ex_app_t_fileUpload_view();
            QCO_Upload view = new QCO_Upload();
            view.Addseq2(seq);
            view.Show();

        }

        private void Wsuploadbtn_Click_1(object sender, EventArgs e)
        {
            string seq = QCO_Sequtxt.Text;

            // T_QCO_Ex_app_t_fileUpload_add File = new T_QCO_Ex_app_t_fileUpload_add();
            QCO_FileUpload File = new QCO_FileUpload();
            File.Addseq(seq);
            File.Show();
        }

        private void Linelayoutrbt2_CheckedChanged(object sender, EventArgs e)
        {
            line_update_txt.Text = "";
        }

        private void Machinerdb2_CheckedChanged(object sender, EventArgs e)
        {
            Machine_update_txt.Text = "";
        }

        private void valuerdb2_CheckedChanged(object sender, EventArgs e)
        {
            Value_Update_txt.Text = "";
        }

        private void Manprdb2_CheckedChanged(object sender, EventArgs e)
        {
            Man_Update_txt.Text = "";
        }

        private void CTPQrdb2_CheckedChanged(object sender, EventArgs e)
        {
            Ctpq_Update_txt.Text = "";
        }

        private void materialrdb2_CheckedChanged(object sender, EventArgs e)
        {
            Mold_Update_txt.Text = "";
        }

        private void Gaugerbn2_CheckedChanged(object sender, EventArgs e)
        {
            Gaue_Update_txt.Text = "";
        }

        private void Sampleshoerbn2_CheckedChanged(object sender, EventArgs e)
        {
            Sop_update_txt.Text = "";
        }

        private void cementrbd2_CheckedChanged(object sender, EventArgs e)
        {
            Cement_update_txt.Text = "";
        }

        private void Svaluesrbtn2_CheckedChanged(object sender, EventArgs e)
        {
            S_line_time_txt.Text = "";
        }

        private void Slinerbtn2_CheckedChanged(object sender, EventArgs e)
        {
            S_vs_time_txt.Text = "";
        }

        private void SManprbtn2_CheckedChanged(object sender, EventArgs e)
        {
            S_mp_time_txt.Text = "";
        }

        private void Smachinerbtn2_CheckedChanged(object sender, EventArgs e)
        {
            S_man_time_txt.Text = "";
        }

        private void Sctpqrbtn2_CheckedChanged(object sender, EventArgs e)
        {
            S_ctpt_time_txt.Text = "";
        }

        private void Ssamplerbtn2_CheckedChanged(object sender, EventArgs e)
        {
            S_sop_time_txt.Text = "";
        }

        private void Sgaugerbtn2_CheckedChanged(object sender, EventArgs e)
        {
            S_gau_time_txt.Text = "";
        }

        private void Smoldrbtn2_CheckedChanged(object sender, EventArgs e)
        {
            S_mold_time_txt.Text = "";
        }

        private void SCcomprbtn2_CheckedChanged(object sender, EventArgs e)
        {
            S_com_time_txt.Text = "";
        }

        private void Seligibrbtn2_CheckedChanged(object sender, EventArgs e)
        {
            S_eq_time_txt.Text = "";
        }

        private void SStitchingrbtn2_CheckedChanged(object sender, EventArgs e)
        {
            S_cm_time_txt.Text = "";
        }

        private void Scsrbtn2_CheckedChanged(object sender, EventArgs e)
        {
            S_cs_time_txt.Text = "";
        }

        private void Wsviewbtn_Click_1(object sender, EventArgs e)
        {

            string seq = QCO_Sequtxt.Text;
            // T_QCO_Ex_app_t_fileUpload_view view = new T_QCO_Ex_app_t_fileUpload_view();
            QCO_Upload view = new QCO_Upload();
            view.Addseq2(seq);
            view.Show();
        }

        private void Shoeissuingbtn_Click_1(object sender, EventArgs e)
        {
            string Article = Articletext.Text;
            string Modelfrom = BMCOtxt.Text;
            string Modelto = A_MCOtxt.Text;
            string line = linetxt.Text;
            string COD = COdatetxt.Text;

            T_QCO_SampleShoe shoe = new T_QCO_SampleShoe();
            shoe.GetInstance(Article, Modelfrom, Modelto, line, COD);
            shoe.Show();
        }

        private void SOPbtn_Click_1(object sender, EventArgs e)
        {
            string Article = Articletext.Text;
            string Modelfrom = BMCOtxt.Text;
            string Modelto = A_MCOtxt.Text;
            string line = linetxt.Text;
            string COD = COdatetxt.Text;
            T_QCO_SOP_Require SOP = new T_QCO_SOP_Require();
            SOP.GetInstance(Article, Modelfrom, Modelto, line, COD);
            SOP.Show();
        }

        private void Sopupbtn_Click_1(object sender, EventArgs e)
        {
            string seq = QCO_Sequtxt.Text;

            // T_QCO_Ex_app_t_fileUpload_add File = new T_QCO_Ex_app_t_fileUpload_add();
            QCO_FileUpload File = new QCO_FileUpload();
            File.Addseq(seq);
            File.Show();

        }

        private void Sopviewbtn_Click_1(object sender, EventArgs e)
        {
            string seq = QCO_Sequtxt.Text;
            // T_QCO_Ex_app_t_fileUpload_view view = new T_QCO_Ex_app_t_fileUpload_view();
            QCO_Upload view = new QCO_Upload();
            view.Addseq2(seq);
            view.Show();

        }

        private void Cmupbtn_Click_1(object sender, EventArgs e)
        {
            string seq = QCO_Sequtxt.Text;

            // T_QCO_Ex_app_t_fileUpload_add File = new T_QCO_Ex_app_t_fileUpload_add();
            QCO_FileUpload File = new QCO_FileUpload();
            File.Addseq(seq);
            File.Show();

        }

        private void Cmviewbtn_Click_1(object sender, EventArgs e)
        {

            string seq = QCO_Sequtxt.Text;
            // T_QCO_Ex_app_t_fileUpload_view view = new T_QCO_Ex_app_t_fileUpload_view();
            QCO_Upload view = new QCO_Upload();
            view.Addseq2(seq);
            view.Show();
        }

        private void COTcalbtn_Click_1(object sender, EventArgs e)
        {

            int Sline = 2;
            T_QCO_COT_COPT cot = new T_QCO_COT_COPT(Sline);
            cot.Show();
        }

        private void AddRemarkToDictionary(Dictionary<string, object> dictionary, TextBox textBox, string key, string defaultValue = "")
        {
            if (!string.IsNullOrEmpty(textBox.Text))
                dictionary[key] = textBox.Text;
            else
                dictionary[key] = defaultValue;
        }

        public void GetRadiodata1()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
    {
        { "QCO_Sequtxt", S_NO1.ToString() }
    };

            string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_QCO", "KZ_QCO.Controllers.GeneralServer", "GetRadiodata1",
                Program.Client.UserToken, JsonConvert.SerializeObject(parameters));

            Dictionary<string, object> response = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);

            if (Convert.ToBoolean(response["IsSuccess"]))
            {
                string json = response["RetData"].ToString();
                dt = JsonConvert.DeserializeObject<DataTable>(json);

                if (dt == null || dt.Rows.Count == 0)
                    return;      
                RadioButton[] radioButtons = { Slinerbtn1, Svaluesrbtn1, SManprbtn1, Smachinerbtn1, Sctpqrbtn1, Ssamplerbtn1, Sgaugerbtn1, Smoldrbtn1, SCcomprbtn1, Seligibrbtn1, SStitchingrbtn1, Scsrbtn1 };
                if (dt != null && dt.Rows.Count > 0 && dt.Columns.Contains("RECORD_PROBLEMS") && dt.Columns.Contains("COT") && dt.Columns.Contains("COPT"))
                {
                    problemin_stit_txt.Text = dt.Rows[0]["RECORD_PROBLEMS"].ToString();
                    cottxt2.Text = dt.Rows[0]["COT"].ToString();
                    copttxt2.Text = dt.Rows[0]["COPT"].ToString();

                    var planTypeValue = dt.Rows[0]["S_PLAN_TYPE"];
                    if (planTypeValue != DBNull.Value && planTypeValue != null)
                    {
                        string planType = planTypeValue.ToString();
                        if (comboBox1.Items.Contains(planType))
                        {

                            comboBox1.SelectedItem = planType;
                        }
                        else
                        {                           
                            MessageBox.Show($"The plan type '{planType}' is not available in the ComboBox.", "Item Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                      
                        MessageBox.Show("The plan type is null or invalid.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        comboBox1.SelectedItem = null; 
                    }
                }

                for (int i = 0; i < radioButtons.Length; i++)
                {
                   // radioButtons[i].Text = dt.Rows[0][i].ToString();

                    if (dt.Rows[0][i].ToString() == "Normal")
                    {
                        radioButtons[i].Checked = true;
                    }
                }
                GetstitchPictureImages();
                BindStitchdate();


            }
            else
            {
                MessageHelper.ShowErr(this, response["ErrMsg"].ToString());
            }
        }
        public void BindStitchdate()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
    {
        { "QCO_Sequtxt", S_NO1.ToString() }
    };

            string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_QCO", "KZ_QCO.Controllers.GeneralServer", "BindStitchdate",
                Program.Client.UserToken, JsonConvert.SerializeObject(parameters));

            Dictionary<string, object> response = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);

            if (Convert.ToBoolean(response["IsSuccess"]))
            {
                string json = response["RetData"].ToString();
                SBRD = JsonConvert.DeserializeObject<DataTable>(json);

                if (SBRD == null || SBRD.Rows.Count == 0)
                    return;
                for (int i = 0; i < SBRD.Rows.Count; i++)
                {
                    S_LINEUPDATE_DATE = SBRD.Rows[i]["LINE_LAY_UPDATE_DATE"].ToString();
                    S_VALUEUPDATE_DATE = SBRD.Rows[i]["VALUE_STR_UPDATE_DATE"].ToString();
                    S_REQUIREDUPDATE_DATE = SBRD.Rows[i]["REQUIRED_MAN_UPDATE_DATE"].ToString();
                    SMACHINUPDATE_DATE = SBRD.Rows[i]["MACHINE_IN_UPDATE_DATE"].ToString();
                    S_CTPQ_UPDATE_DATE = SBRD.Rows[i]["CTPQ_UPDATE_DATE"].ToString();
                    S_SAMPLEUPDATE_DATE = SBRD.Rows[i]["SAMPLE_SH_UPDATE_DATE"].ToString();
                    S_GAUGE_UPDATE_DATE = SBRD.Rows[i]["GAUGE_MAR_UPDATE_DATE"].ToString();
                    S_MATERIAL_UPDATE_DATE = SBRD.Rows[i]["MATERIAL_LAU_UPDATE_DATE"].ToString();
                    S_COMPONENT_UPDATE_DATE = SBRD.Rows[i]["COMPONENT_UPDATE_DATE"].ToString();
                    S_ELIGIBLE_UPDATE_DATE = SBRD.Rows[i]["ELIGIBLE_UPDATE_DATE"].ToString();
                    S_STITCHINGUPDATE_DATE = SBRD.Rows[i]["STITCHING_UPDATE_DATE"].ToString();
                    S_CS_MC_UPDATE_DATE = SBRD.Rows[i]["CS_MC_UPDATE_DATE"].ToString();
                }


            }
            else
            {
                MessageHelper.ShowErr(this, response["ErrMsg"].ToString());
            }
        }

        public void Savedata()
        {
            if (A_Combobox.SelectedIndex == -1)
            {
                MessageHelper.ShowErr(this, "Please Select Plan type");
                return;
            }
            if (A_Combobox.SelectedIndex == 1)
            {
                if (probtxt.Text == "")
                {
                    MessageHelper.ShowErr(this, "You need to Mantion reason for Un_Schedule Plan");
                    return;
                }
            }


            if (!ValidateRequiredFields())
                return;
            if(progressBar1.Value == 100)
            {
              if(  pictureBox1.Image == null || pictureBox2.Image == null || pictureBox3.Image == null || pictureBox4.Image == null)
                {
                    MessageHelper.ShowErr(this, "Before submit, Upload all Signatures");
                    return;
                }
            }
            Dictionary<string, object> data = CollectData();

            string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_QCO", "KZ_QCO.Controllers.GeneralServer", "AChecklist",
                 Program.Client.UserToken, JsonConvert.SerializeObject(data));

            ProcessResponse(ret);

            ClearFields();
        }

        private bool ValidateRequiredFields()
        {
            if (BMCOtxt.Text == "" && COdatetxt.Text == "")
            {
                MessageHelper.ShowErr(this, "Before submit, Please Enter Model name");
                return false;
            }

            ValidateAndShowError(Linelayoutrbt2.Checked, Linelayouttxt.Text, "Please Mention the Remark");
            ValidateAndShowError(valuerdb2.Checked, valuestretxt.Text, "Please Mention the Remark");
            ValidateAndShowError(Manprdb2.Checked, manpotxt.Text, "Please Mention the Remark");
            ValidateAndShowError(Machinerdb2.Checked, machineinstxt.Text, "Please Mention the Remark");
            ValidateAndShowError(CTPQrdb2.Checked, ctpqtxt.Text, "Please Mention the Remark");
            ValidateAndShowError(materialrdb2.Checked, materiallasttxt.Text, "Please Mention the Remark");
            ValidateAndShowError(Gaugerbn2.Checked, gaugetxt.Text, "Please Mention the Remark");
            ValidateAndShowError(Sampleshoerbn2.Checked, sampleshoetxt.Text, "Please Mention the Remark");
            ValidateAndShowError(cementrbd2.Checked, cementtxt.Text, "Please Mention the Remark");

            ValidateSelection(Linelayoutrbt1.Checked || Linelayoutrbt2.Checked, "Line Layout");
            ValidateSelection(valuerdb1.Checked || valuerdb2.Checked, "Value stream");
            ValidateSelection(Manprdb1.Checked || Manprdb2.Checked, "Manpower");
            ValidateSelection(Machinerdb1.Checked || Machinerdb2.Checked, "Machine");
            ValidateSelection(CTPQrdb1.Checked || CTPQrdb2.Checked, "CTPQ");
            ValidateSelection(materialrdb1.Checked || materialrdb2.Checked, "Material");
            ValidateSelection(Gaugerbn1.Checked || Gaugerbn2.Checked, "Gauge");
            ValidateSelection(Sampleshoerbn1.Checked || Sampleshoerbn2.Checked, "Sampleshoe");
            ValidateSelection(cementrbd1.Checked || cementrbd2.Checked, "cement");

            return true;
        }

        private void ValidateAndShowError(bool isChecked, string text, string errorMsg)
        {
            if (isChecked && string.IsNullOrEmpty(text))
            {
                MessageHelper.ShowErr(this, errorMsg);
               
            }
        }
        private void ValidateSelection(bool isAnySelected, string fieldName)
        {
           

            if (!isAnySelected)
            {
                //string msg = $"Please select at least one({fieldName})";
              //  MessageHelper.ShowErr(this, msg);
              
            }
        }
        private byte[] PictureBoxToByteArray(params PictureBox[] pictureBoxes)
        {
            foreach (PictureBox pictureBox in pictureBoxes)
            {
                if (pictureBox.Image == null)
                {                 
                    //if ("pictureBox1" == pictureBox.Name)
                    //{                       
                    //    MessageBox.Show($"Please Upload Section Head Signature", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                    //else  if ("pictureBox2" == pictureBox.Name)
                    //{                     
                    //    MessageBox.Show($"Please Upload PC Signature", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                    //else if ("pictureBox3" == pictureBox.Name)
                    //{
                    //    MessageBox.Show($"Please Upload Technical Signature", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                    //else if ("pictureBox4" == pictureBox.Name)
                    //{
                    //    MessageBox.Show($"Please Upload Maintenance Signature", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                    return null;
                }
            }   
            using (MemoryStream memoryStream = new MemoryStream())
            {              
                Bitmap bitmap = new Bitmap(pictureBoxes[0].Image);
                // Save Bitmap to MemoryStream in a specific format (e.g., PNG)
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png); 
                // Convert MemoryStream to byte array
                return memoryStream.ToArray();
            }
        }

        private Dictionary<string, object> CollectData()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            try
            {
                byte[] byteArray1 = PictureBoxToByteArray(pictureBox1);
                byte[] byteArray2 = PictureBoxToByteArray(pictureBox2);
                byte[] byteArray3 = PictureBoxToByteArray(pictureBox3);
                byte[] byteArray4 = PictureBoxToByteArray(pictureBox4);
                data.Add("BModel", BMCOtxt.Text);
                data.Add("AfModel", A_MCOtxt.Text);
                data.Add("COdate", COdatetxt.Text);
                data.Add("Prodline", linetxt.Text);
                data.Add("Linelay", Linelayoutrbt1.Checked ? Linelayoutrbt1.Text : Linelayoutrbt2.Text);
                data.Add("valuer", valuerdb1.Checked ? valuerdb1.Text : valuerdb2.Text);
                data.Add("Manpr", Manprdb1.Checked ? Manprdb1.Text : Manprdb2.Text);
                data.Add("Machine", Machinerdb1.Checked ? Machinerdb1.Text : Machinerdb2.Text);
                data.Add("CTPQr", CTPQrdb1.Checked ? CTPQrdb1.Text : CTPQrdb2.Text);
                data.Add("material", materialrdb1.Checked ? materialrdb1.Text : materialrdb2.Text);
                data.Add("Gauge", Gaugerbn1.Checked ? Gaugerbn1.Text : Gaugerbn2.Text);
                data.Add("Sampleshoe", Sampleshoerbn1.Checked ? Sampleshoerbn1.Text : Sampleshoerbn2.Text);
                data.Add("cement", cementrbd1.Checked ? cementrbd1.Text : cementrbd2.Text);
                data.Add("Remarks", GetRemark());
                data.Add("COT", COTtxt.Text);
                data.Add("COPT", COPTtxt.Text);
                data.Add("Problem", probtxt.Text);
                data.Add("QCO_Seq", QCO_Sequtxt.Text);

                data.Add("Progressvalue", progressBar1.Value);

                // Convert PictureBox images to byte arrays and add to data dictionary
                data.Add("SectionHd", byteArray1 != null ? byteArray1 : (object)DBNull.Value);
                data.Add("Plantin", byteArray2 != null ? byteArray2 : (object)DBNull.Value);
                data.Add("Technic", byteArray3 != null ? byteArray3 : (object)DBNull.Value);
                data.Add("Maintenance", byteArray4 != null ? byteArray4 : (object)DBNull.Value);

                data.Add("LIne_Time", line_update_txt.Text);
                data.Add("Value_Time", Value_Update_txt.Text);
                data.Add("Man_time", Man_Update_txt.Text);
                data.Add("Machine_time", Machine_update_txt.Text);
                data.Add("Ctpq_time", Ctpq_Update_txt.Text);
                data.Add("Mold_time", Mold_Update_txt.Text);
                data.Add("Gaus_time", Gaue_Update_txt.Text);
                data.Add("Sop_time", Sop_update_txt.Text);
                data.Add("Cement_time", Cement_update_txt.Text);

                data.Add("A_S_Plan", A_Combobox.SelectedItem.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while collecting data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return data;
        }

        private string GetRemark()
        {
            if (!string.IsNullOrEmpty(Linelayouttxt.Text))
                return Linelayouttxt.Text;
            if (!string.IsNullOrEmpty(valuestretxt.Text))
                return valuestretxt.Text;
            if (!string.IsNullOrEmpty(manpotxt.Text))
                return manpotxt.Text;
            if (!string.IsNullOrEmpty(machineinstxt.Text))
                return machineinstxt.Text;
            if (!string.IsNullOrEmpty(ctpqtxt.Text))
                return ctpqtxt.Text;
            if (!string.IsNullOrEmpty(materiallasttxt.Text))
                return materiallasttxt.Text;
            if (!string.IsNullOrEmpty(gaugetxt.Text))
                return gaugetxt.Text;
            if (!string.IsNullOrEmpty(sampleshoetxt.Text))
                return sampleshoetxt.Text;
            return gaugetxt.Text;

            if (!string.IsNullOrEmpty(cementtxt.Text))
                return cementtxt.Text;

            return "All is Ok";
        }

        private void ProcessResponse(string ret)
        {
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();

                if (json == "1")
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Data inserted Successfully");
                }
                else if(json == "2")
                {
                    MessageHelper.ShowSuccess(this, "Updated Successfully");
                }
                else
                {
                    MessageHelper.ShowSuccess(this, " No data Updated");
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void ClearFields()
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Text = "";
                }
                else if (control is CheckBox checkBox)
                {
                    checkBox.Checked = false;
                }
            }
        }



    }
}
