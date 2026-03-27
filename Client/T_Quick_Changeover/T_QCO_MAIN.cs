using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace T_Quick_Changeover
{
    public partial class T_QCO_MAIN : Form
    {
        public T_QCO_MAIN()
        {
            InitializeComponent();
        }
        public Button lastClickedButton1;

        public void Changecoulor(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            if (lastClickedButton1 != null)
            {
                lastClickedButton1.BackColor = SystemColors.Control;
            }
            clickedButton.BackColor = Color.Green;
            lastClickedButton1 = clickedButton;
        }

        private void Sch_plan_import_Click(object sender, EventArgs e)
        {
            Changecoulor(sender, e);
            pages.SelectTab("SchedulePlan_Upload");
            pages.SelectedIndex = 0;         
            T_QCO_Schedule_PlanImport p1 = new T_QCO_Schedule_PlanImport();
            p1.TopLevel = false;
            p1.FormBorderStyle = FormBorderStyle.None;
            p1.Dock = DockStyle.Fill;
            SchedulePlan_Upload.Controls.Add(p1);
            this.WindowState = FormWindowState.Maximized;
            p1.Show();
        }

        public void Plan_report_Click(object sender, EventArgs e)
        {
            T_QCO_MAIN main= new T_QCO_MAIN();
            Changecoulor(sender, e);
            pages.SelectTab("Schedulereport");
            pages.SelectedIndex = 1;
            T_QCO_Schedule_Report ss = new T_QCO_Schedule_Report(main);
            ss.TopLevel = false;
            ss.FormBorderStyle = FormBorderStyle.None;
            ss.Dock = DockStyle.Fill;
            Schedulereport.Controls.Add(ss);
            this.WindowState = FormWindowState.Maximized;
            ss.Show();
        }
        public void Check()
        {
            pages.SelectTab("Checklist");
            pages.SelectedIndex = 2;
            T_QCO_Checklist2 p2 = new T_QCO_Checklist2();
            p2.TopLevel = false;
            p2.FormBorderStyle = FormBorderStyle.None;
            p2.Dock = DockStyle.Fill;
            Checklist.Controls.Add(p2);
            this.WindowState = FormWindowState.Maximized;
            p2.Show();
        }

        //public void Checklistbtn_Click(object sender, EventArgs e)
        //{
        //    Changecoulor(sender, e);
        //    Check();
        //}

        //private void Shoeissueing_Click(object sender, EventArgs e)
        //{
        //    Changecoulor(sender, e);
        //    pages.SelectTab("SAMPLESHOE");
        //    pages.SelectedIndex = 4;
        //    T_QCO_SampleShoe sh = new T_QCO_SampleShoe();
        //    sh.TopLevel = false;
        //    sh.FormBorderStyle = FormBorderStyle.None;
        //    sh.Dock = DockStyle.Fill;
        //    SAMPLESHOE.Controls.Add(sh);
        //    //this.WindowState = FormWindowState.Maximized;
        //    sh.Show();
        //}

        //private void Soprequire_Click(object sender, EventArgs e)
        //{
        //    Changecoulor(sender, e);
        //    pages.SelectTab("PRODUCTIONSOP");
        //    pages.SelectedIndex = 5;
        //    T_QCO_SOP_Require sop = new T_QCO_SOP_Require();
        //    sop.TopLevel = false;
        //    sop.FormBorderStyle = FormBorderStyle.None;
        //    sop.Dock = DockStyle.Fill;
        //    PRODUCTIONSOP.Controls.Add(sop);
        //   // this.WindowState = FormWindowState.Maximized;
        //    sop.Show();
        //}
    }
}
