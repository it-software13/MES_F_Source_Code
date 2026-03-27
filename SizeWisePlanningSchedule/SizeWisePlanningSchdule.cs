using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SJeMES_Framework.WebAPI;
using Newtonsoft.Json;
using SizeWisePlanningSchedule;
using System.Diagnostics;
using SJeMES_Control_Library;
using NPOI.XWPF.UserModel;

namespace PlanningSchedule
{ 

    public partial class SizeWisePlanningSchdule : Form
    {

        private object originalValue = null;
        string checkstatus = "";
        string checkstatussize = ""; 



        public SizeWisePlanningSchdule() 
        { 
            InitializeComponent();
            textBox2.Multiline = true ; 
            textBox2.AcceptsReturn = true ; 
            textBox2.AcceptsTab = false ;   
            textBox2.ScrollBars = ScrollBars.Vertical ;  
            textBox2.WordWrap = false ;    
            textBox2.Font = new Font("Consolas", 11, FontStyle.Regular) ;  
            textBox2.BackColor = Color.White ; 
            textBox2.ForeColor = Color.Black ; 
            textBox2.Padding = new Padding(5) ;  
            textBox2.MinimumSize = new Size(300, 100) ; 
        } 

        private List<string> GetSOList()
        {
            List<string> so = new List<string>();

            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                string[] soArray = textBox2.Text.Split(new[] { ',', ';', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in soArray)
                {
                    string trimmed = item.Trim();
                    if (!string.IsNullOrEmpty(trimmed))
                        so.Add(trimmed);
                }
            }

            return so;
        }


        private List<string> GEtLInesList(List<string> solist)  
        {
            List<string> lines = new List<string>() ;  
            try 
            {
                Dictionary<string, object> data = new Dictionary<string, object>() ;
                data.Add("SOList", solist) ;
                data.Add("process", processs); 
                data.Add("stitching" , checkBox1.Checked ) ; 
                
                string retdata = WebAPIHelper.Post( 
                    Program.client.APIURL , 
                    "KZ_CUTMNT" , 
                    "KZ_CUTMNT.Controllers.SizePlanningController" , 
                    "GetLIneBySOList" ,   
                    Program.client.UserToken , 
                    JsonConvert.SerializeObject(data)   
                ); 
                ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess && ret.RetData != null)
                { 
                    string rawData = ret.RetData.ToString().Trim() ;  
                    lines = JsonConvert.DeserializeObject<List<string>>(ret.RetData.ToString())
                                      .SelectMany(item => item.Split(',', (char)StringSplitOptions.RemoveEmptyEntries))  
                                      .Select(x => x.Trim()) 
                                      .ToList();
                }  
                else  
                { 
                    MessageBox.Show("Failed to fetch lines: " + ret.ErrMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ; 
                }  
            } 
            catch (Exception ex) 
            { 
                MessageBox.Show("Error while fetching lines: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ; 
            } 
            return lines ;  
        }   

        private void ExploreSizeWiseSchedule()  
        {
            List<string> sovalue = GetSOList(); 
            List<string> linesvalue = GEtLInesList(sovalue); 
            SizeDevideForm form = new SizeDevideForm(sovalue, linesvalue , checkBox1.Checked );  
            form.ShowDialog();
            this.Close(); 
        }
        public static string processs = "";

        public static bool GetProcess(bool isRadio1Checked, bool isRadio2Checked , bool isRadio3Checked )  
        {

            if (isRadio1Checked) 
            {
                processs = "C";
                return true;
            }
            else if (isRadio2Checked)
            {
                processs = "S";
                return true;
            }
            else if(isRadio3Checked) 
            {
                processs = "A";
                return true; 
            }  
            else 
            {  
                MessageBox.Show("Please Select Process");
                return false ;    
            }  
        }     


        private void button1_Click_1(object sender, EventArgs e)  
        {
            if(checkstatus == "Ok" && checkstatussize == "Ok") 
            {
                GetProcess(radioButton1.Checked, radioButton2.Checked, radioButton3.Checked);
                ExploreSizeWiseSchedule();  
            } 
            else 
            {
                MessageBox.Show("Please Check Sales Orders are schedule or not and also check size wise scheduled pos"); 
                return;
            } 
        }   

        
        private void button2_Click(object sender, EventArgs e)
        {
            bool status = GetProcess(radioButton1.Checked , radioButton2.Checked , radioButton3.Checked );
            if (!status) return; 
            List<string> soList = GetSOList();
            if (soList == null || soList.Count == 0)
            {
                MessageBox.Show("Please enter at least one Sales Order in TextBox2.");
                return;
            }
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>(); 
                data.Add("SOList", soList);
                data.Add("process", processs);
                
                string retdata = WebAPIHelper.Post(
                    Program.client.APIURL, 
                    "KZ_CUTMNT" , 
                    "KZ_CUTMNT.Controllers.SizePlanningController" ,  
                    "VerifyScheduleOKNot" , 
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(data)
                );
                ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess && ret.RetData != null) 
                { 
                    checkstatus = "Ok";  
                    List<string> missingSOList = JsonConvert.DeserializeObject<List<string>>(ret.RetData.ToString()); 
                    if (missingSOList.Count > 0) 
                    { 
                        string msg = "The following Sales Orders are Not Scheduled:\n\n" + string.Join("\n", missingSOList) ;  
                        DialogResult result = MessageBox.Show(msg, "Missing Schedules", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) ; 
                        List<string> remaining = soList.Except(missingSOList).ToList() ; 
                        textBox2.Text = string.Join(Environment.NewLine, remaining) ;   
                    } 
                    else 
                    { 
                        MessageBox.Show("All sales orders have schedule data. ✅", "Verification", MessageBoxButtons.OK, MessageBoxIcon.Information) ; 
                    } 
                } 
                else 
                { 
                    MessageBox.Show("Error from server: " + ret.ErrMsg) ;  
                }  
            }  
            catch (Exception ex) 
            {  
                MessageBox.Show("Error while fetching sizes: " + ex.Message) ; 
            } 
        }


        private void button3_Click(object sender, EventArgs e)
        {
            GetProcess(radioButton1.Checked, radioButton2.Checked, radioButton3.Checked);  
            List<string> soList = GetSOList();
            if (soList == null || soList.Count == 0)
            {
                MessageBox.Show("Please enter at least one Sales Order in TextBox2.");
                return;
            }
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("SOList", soList);
                data.Add("process", processs); 
                string retdata = WebAPIHelper.Post(
                    Program.client.APIURL,
                    "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.SizePlanningController",
                    "VerifySizeScheduleOKNot",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(data)
                );
                ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess && ret.RetData != null)
                { 
                    checkstatussize = "Ok";
                    List<string> missingSOList = JsonConvert.DeserializeObject<List<string>>(ret.RetData.ToString());
                    if (missingSOList.Count > 0)
                    {
                        string msg = "The following Sales Orders are Already Scheduled:\n\n" + string.Join("\n", missingSOList); 
                        DialogResult result = MessageBox.Show(msg, "Missing Schedules", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        List<string> remaining = soList.Except(missingSOList).ToList();
                        textBox2.Text = string.Join(Environment.NewLine, remaining);
                    }
                    else
                    {
                        MessageBox.Show("All sales orders have need to schedule data. ✅", "Verification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Error from server: " + ret.ErrMsg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while fetching sizes: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        { 
            
            SizeWiseUpdation form = new SizeWiseUpdation();
            form.ShowDialog();
            this.Close();
        }  
    }
}
