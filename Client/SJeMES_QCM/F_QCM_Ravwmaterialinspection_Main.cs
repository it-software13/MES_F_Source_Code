using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QCM
{
    public partial class F_QCM_Ravwmaterialinspection_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_Ravwmaterialinspection_Main()
        {
            InitializeComponent();
            BindingData();
            pageControl1.BindPageEvent += BindingData2;
            FormLoad();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        public void FormLoad()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        public static DataTable InitializeData()
        {
            #region 初始化数据
            DataTable dt = new DataTable();
            dt.Columns.Add("MaterialID");
            dt.Columns.Add("MaterialType");
            dt.Columns.Add("Vendor");
            dt.Columns.Add("WarehouseEntryDate");
            dt.Columns.Add("Batch");
            dt.Columns.Add("WarehouseEntryNumber");
            dt.Columns.Add("VisualInspectionResult");
            dt.Columns.Add("PhysicalProperties");
            dt.Columns.Add("SpecialMining");
            dt.Columns.Add("Article");
            dt.Columns.Add("Shoe_Name");
            dt.Columns.Add("DevelopmentPhase");
            dt.Columns.Add("po");
            dt.Rows.Add();
            dt.Rows[0]["MaterialID"] = "1008611";
            dt.Rows[0]["MaterialType"] = "鞋";
            dt.Rows[0]["Vendor"] = "创达";
            dt.Rows[0]["WarehouseEntryDate"] = "2021-11-15";
            dt.Rows[0]["Batch"] = "22";
            dt.Rows[0]["WarehouseEntryNumber"] = "22";
            dt.Rows[0]["VisualInspectionResult"] = "PASS";
            dt.Rows[0]["PhysicalProperties"] = "PASS";
            dt.Rows[0]["SpecialMining"] = "fd";
            dt.Rows[0]["Article"] = "GW6003";
            dt.Rows[0]["Shoe_Name"] = "板鞋";
            dt.Rows[0]["DevelopmentPhase"] = "无";
            dt.Rows[0]["po"] = "11";
            dt.Rows.Add();
            dt.Rows[1]["MaterialID"] = "1008612";
            dt.Rows[1]["MaterialType"] = "鞋";
            dt.Rows[1]["Vendor"] = "万国";
            dt.Rows[1]["WarehouseEntryDate"] = "2021-11-15";
            dt.Rows[1]["Batch"] = "23";
            dt.Rows[1]["WarehouseEntryNumber"] = "22";
            dt.Rows[1]["VisualInspectionResult"] = "PASS";
            dt.Rows[1]["PhysicalProperties"] = "PASS";
            dt.Rows[1]["SpecialMining"] = "fd";
            dt.Rows[1]["Article"] = "CK6003";
            dt.Rows[1]["Shoe_Name"] = "篮球鞋";
            dt.Rows[1]["DevelopmentPhase"] = "无";
            dt.Rows[1]["po"] = "143";
            dt.Rows.Add();
            dt.Rows[2]["MaterialID"] = "1008613";
            dt.Rows[2]["MaterialType"] = "鞋";
            dt.Rows[2]["Vendor"] = "禾云";
            dt.Rows[2]["WarehouseEntryDate"] = "2021-11-15";
            dt.Rows[2]["Batch"] = "24";
            dt.Rows[2]["WarehouseEntryNumber"] = "22";
            dt.Rows[2]["VisualInspectionResult"] = "PASS";
            dt.Rows[2]["PhysicalProperties"] = "PASS";
            dt.Rows[2]["SpecialMining"] = "fd";
            dt.Rows[2]["Article"] = "CH6003";
            dt.Rows[2]["Shoe_Name"] = "足球鞋";
            dt.Rows[2]["DevelopmentPhase"] = "无";
            dt.Rows[2]["po"] = "432";
            dt.Rows.Add();
            dt.Rows[3]["MaterialID"] = "1008614";
            dt.Rows[3]["MaterialType"] = "鞋";
            dt.Rows[3]["Vendor"] = "大辉";
            dt.Rows[3]["WarehouseEntryDate"] = "2021-11-15";
            dt.Rows[3]["Batch"] = "25";
            dt.Rows[3]["WarehouseEntryNumber"] = "22";
            dt.Rows[3]["VisualInspectionResult"] = "PASS";
            dt.Rows[3]["PhysicalProperties"] = "PASS";
            dt.Rows[3]["SpecialMining"] = "fd";
            dt.Rows[3]["Article"] = "AM6003";
            dt.Rows[3]["Shoe_Name"] = "跑鞋";
            dt.Rows[3]["DevelopmentPhase"] = "无";
            dt.Rows[3]["po"] = "521";
            dt.Rows.Add();
            dt.Rows[4]["MaterialID"] = "1008615";
            dt.Rows[4]["MaterialType"] = "鞋";
            dt.Rows[4]["Vendor"] = "万丰";
            dt.Rows[4]["WarehouseEntryDate"] = "2021-11-15";
            dt.Rows[4]["Batch"] = "26";
            dt.Rows[4]["WarehouseEntryNumber"] = "22";
            dt.Rows[4]["VisualInspectionResult"] = "PASS";
            dt.Rows[4]["PhysicalProperties"] = "PASS";
            dt.Rows[4]["SpecialMining"] = "fd";
            dt.Rows[4]["Article"] = "SW6003";
            dt.Rows[4]["Shoe_Name"] = "老北京布鞋";
            dt.Rows[4]["DevelopmentPhase"] = "无";
            dt.Rows[4]["po"] = "421";
            dt.Rows.Add();
            dt.Rows[5]["MaterialID"] = "1008616";
            dt.Rows[5]["MaterialType"] = "鞋";
            dt.Rows[5]["Vendor"] = "众联";
            dt.Rows[5]["WarehouseEntryDate"] = "2021-11-15";
            dt.Rows[5]["Batch"] = "27";
            dt.Rows[5]["WarehouseEntryNumber"] = "22";
            dt.Rows[5]["VisualInspectionResult"] = "PASS";
            dt.Rows[5]["PhysicalProperties"] = "PASS";
            dt.Rows[5]["SpecialMining"] = "fd";
            dt.Rows[5]["Article"] = "NJ6003";
            dt.Rows[5]["Shoe_Name"] = "棉鞋";
            dt.Rows[5]["DevelopmentPhase"] = "无";
            dt.Rows[5]["po"] = "523";
            dt.Rows.Add();
            dt.Rows[6]["MaterialID"] = "1008617";
            dt.Rows[6]["MaterialType"] = "鞋";
            dt.Rows[6]["Vendor"] = "丰泰";
            dt.Rows[6]["WarehouseEntryDate"] = "2021-11-15";
            dt.Rows[6]["Batch"] = "28";
            dt.Rows[6]["WarehouseEntryNumber"] = "22";
            dt.Rows[6]["VisualInspectionResult"] = "PASS";
            dt.Rows[6]["PhysicalProperties"] = "PASS";
            dt.Rows[6]["SpecialMining"] = "fd";
            dt.Rows[6]["Article"] = "CG6003";
            dt.Rows[6]["Shoe_Name"] = "拖鞋";
            dt.Rows[6]["DevelopmentPhase"] = "无";
            dt.Rows[6]["po"] = "632";
            dt.Rows.Add();
            dt.Rows[7]["MaterialID"] = "1008618";
            dt.Rows[7]["MaterialType"] = "鞋";
            dt.Rows[7]["Vendor"] = "Sadase";
            dt.Rows[7]["WarehouseEntryDate"] = "2021-11-15";
            dt.Rows[7]["Batch"] = "29";
            dt.Rows[7]["WarehouseEntryNumber"] = "22";
            dt.Rows[7]["VisualInspectionResult"] = "PASS";
            dt.Rows[7]["PhysicalProperties"] = "PASS";
            dt.Rows[7]["SpecialMining"] = "fd";
            dt.Rows[7]["Article"] = "LL6003";
            dt.Rows[7]["Shoe_Name"] = "休闲鞋";
            dt.Rows[7]["DevelopmentPhase"] = "无";
            dt.Rows[7]["po"] = "621";
            dt.Rows.Add();
            dt.Rows[8]["MaterialID"] = "1008619";
            dt.Rows[8]["MaterialType"] = "鞋";
            dt.Rows[8]["Vendor"] = "Prime";
            dt.Rows[8]["WarehouseEntryDate"] = "2021-11-15";
            dt.Rows[8]["Batch"] = "30";
            dt.Rows[8]["WarehouseEntryNumber"] = "22";
            dt.Rows[8]["VisualInspectionResult"] = "PASS";
            dt.Rows[8]["PhysicalProperties"] = "PASS";
            dt.Rows[8]["SpecialMining"] = "fd";
            dt.Rows[8]["Article"] = "GB6003";
            dt.Rows[8]["Shoe_Name"] = "草鞋";
            dt.Rows[8]["DevelopmentPhase"] = "无";
            dt.Rows[8]["po"] = "772";
            dt.Rows.Add();
            dt.Rows[9]["MaterialID"] = "1008620";
            dt.Rows[9]["MaterialType"] = "鞋";
            dt.Rows[9]["Vendor"] = "香洲";
            dt.Rows[9]["WarehouseEntryDate"] = "2021-11-15";
            dt.Rows[9]["Batch"] = "31";
            dt.Rows[9]["WarehouseEntryNumber"] = "22";
            dt.Rows[9]["VisualInspectionResult"] = "PASS";
            dt.Rows[9]["PhysicalProperties"] = "PASS";
            dt.Rows[9]["SpecialMining"] = "fd";
            dt.Rows[9]["Article"] = "NK6003";
            dt.Rows[9]["Shoe_Name"] = "凉鞋";
            dt.Rows[9]["DevelopmentPhase"] = "无";
            dt.Rows[9]["po"] = "283";
            dt.Rows.Add();
            dt.Rows[10]["MaterialID"] = "1008621";
            dt.Rows[10]["MaterialType"] = "鞋";
            dt.Rows[10]["Vendor"] = "东红";
            dt.Rows[10]["WarehouseEntryDate"] = "2021-11-15";
            dt.Rows[10]["Batch"] = "32";
            dt.Rows[10]["WarehouseEntryNumber"] = "22";
            dt.Rows[10]["VisualInspectionResult"] = "PASS";
            dt.Rows[10]["PhysicalProperties"] = "PASS";
            dt.Rows[10]["SpecialMining"] = "fd";
            dt.Rows[10]["Article"] = "AT6003";
            dt.Rows[10]["Shoe_Name"] = "夹板";
            dt.Rows[10]["DevelopmentPhase"] = "无";
            dt.Rows[10]["po"] = "142";
            dt.Rows.Add();
            dt.Rows[11]["MaterialID"] = "1008622";
            dt.Rows[11]["MaterialType"] = "鞋";
            dt.Rows[11]["Vendor"] = "宏国";
            dt.Rows[11]["WarehouseEntryDate"] = "2021-11-15";
            dt.Rows[11]["Batch"] = "33";
            dt.Rows[11]["WarehouseEntryNumber"] = "22";
            dt.Rows[11]["VisualInspectionResult"] = "PASS";
            dt.Rows[11]["PhysicalProperties"] = "PASS";
            dt.Rows[11]["SpecialMining"] = "fd";
            dt.Rows[11]["Article"] = "MT6003";
            dt.Rows[11]["Shoe_Name"] = "马丁靴";
            dt.Rows[11]["DevelopmentPhase"] = "无";
            dt.Rows[11]["po"] = "412";
            dt.Rows.Add();
            dt.Rows[12]["MaterialID"] = "1008623";
            dt.Rows[12]["MaterialType"] = "鞋";
            dt.Rows[12]["Vendor"] = "良甲";
            dt.Rows[12]["WarehouseEntryDate"] = "2021-11-15";
            dt.Rows[12]["Batch"] = "34";
            dt.Rows[12]["WarehouseEntryNumber"] = "22";
            dt.Rows[12]["VisualInspectionResult"] = "PASS";
            dt.Rows[12]["PhysicalProperties"] = "PASS";
            dt.Rows[12]["SpecialMining"] = "fd";
            dt.Rows[12]["Article"] = "LS6003";
            dt.Rows[12]["Shoe_Name"] = "高跟鞋";
            dt.Rows[12]["DevelopmentPhase"] = "无";
            dt.Rows[12]["po"] = "512";
            dt.Rows.Add();
            dt.Rows[13]["MaterialID"] = "1008624";
            dt.Rows[13]["MaterialType"] = "鞋";
            dt.Rows[13]["Vendor"] = "柏鑫";
            dt.Rows[13]["WarehouseEntryDate"] = "2021-11-15";
            dt.Rows[13]["Batch"] = "35";
            dt.Rows[13]["WarehouseEntryNumber"] = "22";
            dt.Rows[13]["VisualInspectionResult"] = "PASS";
            dt.Rows[13]["PhysicalProperties"] = "PASS";
            dt.Rows[13]["SpecialMining"] = "fd";
            dt.Rows[13]["Article"] = "BX6003";
            dt.Rows[13]["Shoe_Name"] = "战靴";
            dt.Rows[13]["DevelopmentPhase"] = "无";
            dt.Rows[13]["po"] = "514";
            dt.Rows.Add();
            dt.Rows[14]["MaterialID"] = "1008625";
            dt.Rows[14]["MaterialType"] = "鞋";
            dt.Rows[14]["Vendor"] = "先峰";
            dt.Rows[14]["WarehouseEntryDate"] = "2021-11-15";
            dt.Rows[14]["Batch"] = "36";
            dt.Rows[14]["WarehouseEntryNumber"] = "22";
            dt.Rows[14]["VisualInspectionResult"] = "PASS";
            dt.Rows[14]["PhysicalProperties"] = "PASS";
            dt.Rows[14]["SpecialMining"] = "fd";
            dt.Rows[14]["Article"] = "NF6003";
            dt.Rows[14]["Shoe_Name"] = "拖鞋";
            dt.Rows[14]["DevelopmentPhase"] = "无";
            dt.Rows[14]["po"] = "142";
            #endregion

            return dt;
        }

        public void BindingData()
        {
            int i = 0;
            foreach (DataRow dr in InitializeData().Rows)
            {
                dataGridView1.Rows.Add();
                DataGridViewRow dgvr = dataGridView1.Rows[i];
                dgvr.Cells["MaterialID"].Value = dr["MaterialID"].ToString();
                dgvr.Cells["MaterialType"].Value = dr["MaterialType"].ToString();
                dgvr.Cells["Vendor"].Value = dr["Vendor"].ToString();
                dgvr.Cells["WarehouseEntryDate"].Value = dr["WarehouseEntryDate"].ToString();
                dgvr.Cells["Batch"].Value = dr["Batch"].ToString();
                dgvr.Cells["WarehouseEntryNumber"].Value = dr["WarehouseEntryNumber"].ToString();
                dgvr.Cells["VisualInspectionResult"].Value = dr["VisualInspectionResult"].ToString();
                dgvr.Cells["PhysicalProperties"].Value = dr["PhysicalProperties"].ToString();
                dgvr.Cells["SpecialMining"].Value = dr["SpecialMining"].ToString();
                dgvr.Cells["Article"].Value = dr["Article"].ToString();
                dgvr.Cells["Shoe_Name"].Value = dr["Shoe_Name"].ToString();
                dgvr.Cells["DevelopmentPhase"].Value = dr["DevelopmentPhase"].ToString();
                dgvr.Cells["po"].Value = dr["po"].ToString();
                i++;
            }
            GenClass.AutoSizeColumn(dataGridView1);
        }

        public void BindingData2(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 1;
        }

        private void F_QCM_Ravwmaterialinspection_Main_Load(object sender, EventArgs e)
        {

            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            chart1.Legends[0].Enabled = false;//不显示图例

            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "0%";//格式化，为了显示百分号
            chart1.ChartAreas[0].AxisY.Interval = 0.2;//设置刻度间隔为5%
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;//不显示网格线
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Coral;
            //数据

            ////DataTable Dt=数据源
            //DataTable dt = new DataTable();
            //foreach (DataRow item in dt.Rows)
            //{
            //    chart1.Series[0].Points.AddXY("香洲", 0.201);
            //    chart1.Series[0].Points.AddXY("东红", 0.395);
            //    chart1.Series[0].Points.AddXY("宏国", 0.173);
            //    chart1.Series[0].Points.AddXY("先锋", 0.236);
            //    chart1.Series[0].Points.AddXY("良甲", 0.201);
            //    chart1.Series[0].Points.AddXY("宏霖", 0.395);
            //    chart1.Series[0].Points.AddXY("兴艺", 0.473);
            //    chart1.Series[0].Points.AddXY("栢鑫", 0.336);
            //    chart1.Series[0].Points.AddXY("创达", 0.211);
            //    chart1.Series[0].Points.AddXY("万国", 0.345);
            //    chart1.Series[0].Points.AddXY("禾云", 0.463);
            //    chart1.Series[0].Points.AddXY("大辉", 0.376);
            //    chart1.Series[0].Points.AddXY("万丰", 0.356);
            //    chart1.Series[0].Points.AddXY("众联", 0.123);
            //    chart1.Series[0].Points.AddXY("丰泰", 0.163);
            //    chart1.Series[0].Points.AddXY("prime", 0.761);

            //}
            chart1.Series[0].Points.AddXY("香洲", 0.201);
            chart1.Series[0].Points.AddXY("东红", 0.395);
            chart1.Series[0].Points.AddXY("宏国", 0.253);
            chart1.Series[0].Points.AddXY("先锋", 0.046);
            chart1.Series[0].Points.AddXY("良甲", 0.201);
            chart1.Series[0].Points.AddXY("宏霖", 0.395);
            chart1.Series[0].Points.AddXY("兴艺", 0.473);
            chart1.Series[0].Points.AddXY("栢鑫", 0.336);
            chart1.Series[0].Points.AddXY("创达", 0.211);
            chart1.Series[0].Points.AddXY("万国", 0.143);
            chart1.Series[0].Points.AddXY("禾云", 0.123);
            chart1.Series[0].Points.AddXY("大辉", 0.023);
            chart1.Series[0].Points.AddXY("万丰", 0.123);
            chart1.Series[0].Points.AddXY("众联", 0.023);
            chart1.Series[0].Points.AddXY("丰泰", 0.145);
            chart1.Series[0].Points.AddXY("prime", 0.358);
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            string where = string.Empty;
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                where = $@"and (MaterialID='{textBox1.Text}' or MaterialType like '%{textBox1.Text}%' or Vendor like '%{textBox1.Text}%' or Batch='{textBox1.Text}' or 
                    WarehouseEntryNumber='{textBox1.Text}' or VisualInspectionResult like '%{textBox1.Text}%' or PhysicalProperties like '%{textBox1.Text}@'or 
                    SpecialMining like '%{textBox1.Text}%' or Article like '%{textBox1.Text}%' or Shoe_Name like '%{textBox1.Text}%' or DevelopmentPhase like '%{textBox1.Text}%' or po='{textBox1.Text}')";
            }
            DataRow[] drr = InitializeData().Select($@"WarehouseEntryDate='{dateTimeP_putin_date.Value.ToString("yyyy-MM-dd")}' {where}");
            DataTable dt = InitializeData().Clone();
            for (int i = 0; i < drr.Length; i++)
            {
                dt.ImportRow(drr[i]);
            }

            if (dataGridView1.Rows.Count >= 0)
            {
                dataGridView1.Rows.Clear();
            }
            int a = 0;
            foreach (DataRow dr in dt.Rows)
            {
                dataGridView1.Rows.Add();
                DataGridViewRow dgvr = dataGridView1.Rows[a];
                dgvr.Cells["MaterialID"].Value = dr["MaterialID"].ToString();
                dgvr.Cells["MaterialType"].Value = dr["MaterialType"].ToString();
                dgvr.Cells["Vendor"].Value = dr["Vendor"].ToString();
                dgvr.Cells["WarehouseEntryDate"].Value = dr["WarehouseEntryDate"].ToString();
                dgvr.Cells["Batch"].Value = dr["Batch"].ToString();
                dgvr.Cells["WarehouseEntryNumber"].Value = dr["WarehouseEntryNumber"].ToString();
                dgvr.Cells["VisualInspectionResult"].Value = dr["VisualInspectionResult"].ToString();
                dgvr.Cells["PhysicalProperties"].Value = dr["PhysicalProperties"].ToString();
                dgvr.Cells["SpecialMining"].Value = dr["SpecialMining"].ToString();
                dgvr.Cells["Article"].Value = dr["Article"].ToString();
                dgvr.Cells["Shoe_Name"].Value = dr["Shoe_Name"].ToString();
                dgvr.Cells["DevelopmentPhase"].Value = dr["DevelopmentPhase"].ToString();
                dgvr.Cells["po"].Value = dr["po"].ToString();
                a++;
            }
            GenClass.AutoSizeColumn(dataGridView1);
        }


    }
}
