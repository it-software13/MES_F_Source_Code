using SJeMES_Control_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T_Quick_Changeover
{
    public partial class T_QCO_COT_COPT : Form
    {
        string Hello;
        int Sline;
        string EndofTime;

       // DateTime Starttime=
        public T_QCO_COT_COPT(int S)
        {
            InitializeComponent();
            Sline = S;
          
        }
        private void Calculatebtn_Click(object sender, EventArgs e)
        {
            TimeSpan cot;
            if (EndofdayTimePicker.Visible)
            {

                DateTime endtime1 = DateTime.Parse(endTimeP1txt.Text);
                DateTime Endodtime = DateTime.Parse(Endofdattxt.Text);
                TimeSpan CoT1 = Endodtime - endtime1;

                DateTime starttime1 = DateTime.Parse(startTimeP2txt.Text);
               
                DateTime specificDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 30, 0);
                TimeSpan COT2 = starttime1 - specificDateTime;
                cot = CoT1 + COT2;
            }
            else
            {
                if (endTimeP1txt.Text == "")
                {
                    MessageHelper.ShowErr(this, " Please Enter Old Model Last Pair Time!");
                    return;
                }
                if (startTimeP2txt.Text == "")
                {
                    MessageHelper.ShowErr(this, " Please Enter New Model First Pair Time!");
                    return;
                }
                DateTime endtime = DateTime.Parse(endTimeP1txt.Text);
                DateTime starttime = DateTime.Parse(startTimeP2txt.Text);
                cot = starttime - endtime;
                if ((endtime.TimeOfDay.Hours <= 12 && starttime.TimeOfDay.Hours >= 13) || (endtime.TimeOfDay.Hours <= 12 && starttime.TimeOfDay.Hours >= 14) || (endtime.TimeOfDay.Hours <= 13 && starttime.TimeOfDay.Hours >= 14))
                {
                    // Lunch break from 12:30 to 1:15
                    if (endtime.TimeOfDay <= new TimeSpan(12, 30, 0) && starttime.TimeOfDay >= new TimeSpan(13, 15, 0))
                    {
                        cot = cot.Subtract(new TimeSpan(0, 45, 0));
                    }                 
                    else
                    {
                        cot = cot.Subtract(new TimeSpan(0, 45, 0));
                    }

                }
            }
                double cotMinutes = Math.Round(cot.TotalMinutes,2);             
                double absoluteValue = Math.Abs(cotMinutes);
                Resultcottxt.Text = absoluteValue.ToString();           
                MessageHelper.ShowSuccess(this, "Changeover Time (COT): " + absoluteValue + " minutes");
                Hello = startTimeP2txt.Text;
                textBox2.Text = Hello;
           

        }
        private void COPTbtn_Click(object sender, EventArgs e)
        {          
            DateTime starttime1 = DateTime.Parse(textBox1.Text);
            DateTime endtime = DateTime.Parse(textBox2.Text);
            // TimeSpan COPT = endtime - starttime1;
            TimeSpan COPT = starttime1 - endtime;
            if ((endtime.TimeOfDay.Hours <= 12 && starttime1.TimeOfDay.Hours >= 13) || (endtime.TimeOfDay.Hours <= 12 && starttime1.TimeOfDay.Hours >= 14) || (endtime.TimeOfDay.Hours <= 13 && starttime1.TimeOfDay.Hours >= 14))
            {
                // Lunch break from 12:30 to 1:15
                if (endtime.TimeOfDay <= new TimeSpan(12, 30, 0) && starttime1.TimeOfDay >= new TimeSpan(13, 15, 0))
                {
                    COPT = COPT.Subtract(new TimeSpan(0, 45, 0));
                }              
                else
                {
                    COPT = COPT.Subtract(new TimeSpan(0, 45, 0));
                }

            }
            double cotMinutes = Math.Round (COPT.TotalMinutes,2);
            double absoluteValue = Math.Abs(cotMinutes);
            COPTtext.Text = absoluteValue.ToString();
            MessageHelper.ShowSuccess(this,"Changeover Time(COT): " + absoluteValue + " minutes");
        }

        private void Clickhourbtn_Click(object sender, EventArgs e)
        {
            double min = double.Parse(Resultcottxt.Text);
            double Hour = min / 60;
            string Hr = Hour + " Hours";
            Hourstxt.Text = Hr.ToString();
        }
        string Edate;
        DateTime endTime1;
        private void endTimeProduct1_KeyDown(object sender, KeyEventArgs e)
        {
            startTimeProduct2.MinDate = endTimeProduct1.Value;
            endTimeProduct1.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            string endTime = endTimeProduct1.Value.ToString();
             endTime1 = endTimeProduct1.Value;
            string edate = endTime1.ToString("yyyy/MM/dd");
            Edate = edate;
            endTimeP1txt.Text = endTime;
        }

        private void startTimeProduct2_KeyDown(object sender, KeyEventArgs e)
        {
            startTimeProduct2.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            string start = startTimeProduct2.Value.ToString();
            DateTime start1 = startTimeProduct2.Value;
            string sdate = start1.ToString("yyyy/MM/dd");
            if(start1 <= endTime1)
            {
                startTimeP2txt.Text = start;
                label4.Visible = false;
                EndofdayTimePicker.Visible = false;
                Endofdattxt.Visible = false;
            }
            else
            {
                label4.Visible = true;
                EndofdayTimePicker.Visible = true;
                Endofdattxt.Visible = true;

            }
            startTimeP2txt.Text = start;
        }

        private void Submittxt_Click(object sender, EventArgs e)
        {
            QCO_Prop.Param = "Checklist";
            QCO_Prop.COT = Resultcottxt.Text;
            QCO_Prop.COPT = COPTtext.Text;
            string cotValue = QCO_Prop.COT;
            string coptValue = QCO_Prop.COPT;        
            this.Hide();
            T_QCO_Checklist2 existingForm = Application.OpenForms.OfType<T_QCO_Checklist2>().FirstOrDefault();
            
            if (existingForm != null && Sline ==1)
            {
                existingForm.GetInstance(cotValue, coptValue);
                existingForm.Focus();
            }
            else
            {              
                existingForm.GetInstance1(cotValue, coptValue);
                existingForm.Show();
            }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            dateTimePicker1.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            string start = dateTimePicker1.Value.ToString();
            textBox1.Text = start;
        }

        private void T_QCO_COT_COPT_Load(object sender, EventArgs e)
        {
          
            label4.Visible = false;
            EndofdayTimePicker.Visible = false;
            Endofdattxt.Visible = false;
           
        }

        private void EndofdayTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {         
            EndofdayTimePicker.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            string End = EndofdayTimePicker.Value.ToString();
            Endofdattxt.Text = End;           
        }

       
    }
}
 