using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_Report.IQC
{
    public partial class HPPrint : Form
    {
        public HPPrint(Dictionary<string, object> rdlcParam)
        {
            InitializeComponent();
            InitialReport(rdlcParam);
        }

        public void InitialReport(Dictionary<string, object> rdlcParam)
        {
            string item_no = rdlcParam["item_no"].ToString();//材料料号
            string date = rdlcParam["date"].ToString();//日期
            string item_name = rdlcParam["item_name"].ToString();//材料名称
            string supplier = rdlcParam["supplier"].ToString();//供应商
            string qty = rdlcParam["qty"].ToString();//画皮数量
            string ITEM_TYPE_NAME = rdlcParam["ITEM_TYPE_NAME"].ToString();//材料类型
            string mtl_qty = rdlcParam["mtl_qty"].ToString();//买进皮料数量
            string BuyQualityCoefficient = rdlcParam["BuyQualityCoefficient"].ToString();//购进质量系数
            string AverageUsage = rdlcParam["AverageUsage"].ToString();//平均使用率
            string assessment = rdlcParam["assessment"].ToString();//评估
            string area_diff_cft = rdlcParam["area_diff_cft"].ToString();//面积差异系数

            DataTable areadt = (DataTable)rdlcParam["areadt"];//面积

            DataTable leveldt = (DataTable)rdlcParam["leveldt"];//等级

            List<Microsoft.Reporting.WinForms.ReportParameter> PS = new List<Microsoft.Reporting.WinForms.ReportParameter>();
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("item_no", item_no));
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("date", date));
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("item_name", item_name));
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("supplier", supplier));
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("qty", qty));
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("ITEM_TYPE_NAME", ITEM_TYPE_NAME));
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("mtl_qty", mtl_qty));
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("BuyQualityCoefficient", BuyQualityCoefficient));
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("AverageUsage", AverageUsage));
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("assessment", assessment));
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("area_diff_cft", area_diff_cft));

            //面积
            for (int i = 0; i < areadt.Rows.Count - 1; i++)
            {
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter($@"SupplierArea{i + 1}", areadt.Rows[i]["gys_area"].ToString()));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter($@"ActualArea{i + 1}", areadt.Rows[i]["sj_area"].ToString()));
            }
            decimal gysSum = 0;//供应商面积和
            for (int a = 0; a < areadt.Rows.Count - 1; a++)
            {
                if (!string.IsNullOrEmpty(areadt.Rows[a]["gys_area"].ToString()))
                {
                    gysSum += Convert.ToDecimal(areadt.Rows[a]["gys_area"].ToString());
                }
            }
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter($@"gysSum", gysSum.ToString()));
            decimal sjSum = 0;//实际面积和
            for (int a = 0; a < areadt.Rows.Count - 1; a++)
            {
                if (!string.IsNullOrEmpty(areadt.Rows[a]["sj_area"].ToString()))
                {
                    sjSum += Convert.ToDecimal(areadt.Rows[a]["sj_area"].ToString());
                }
            }
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter($@"sjSum", sjSum.ToString()));
            for (int i = 0; i < leveldt.Rows.Count; i++)
            {
                if (leveldt.Rows[i]["pl_level"].ToString() == "I~V总和")
                {
                    leveldt.Rows.RemoveAt(i);
                }
            }
            for (int i = 0; i < leveldt.Rows.Count - 2; i++)
            {
                if (leveldt.Rows[i]["pl_level"].ToString() != "I~V总和")
                {
                    PS.Add(new Microsoft.Reporting.WinForms.ReportParameter($@"Level{i+1}", leveldt.Rows[i]["pl_level"].ToString()));
                    PS.Add(new Microsoft.Reporting.WinForms.ReportParameter($@"Quantity{i+1}", leveldt.Rows[i]["qty"].ToString()));
                    PS.Add(new Microsoft.Reporting.WinForms.ReportParameter($@"Coefficient{i+1}", leveldt.Rows[i]["coefficient"].ToString()));
                    PS.Add(new Microsoft.Reporting.WinForms.ReportParameter($@"Multiple{i+1}", leveldt.Rows[i]["multiple"].ToString()));
                }
            }
            decimal qtysum = 0;//数量和
            for (int i = 0; i < leveldt.Rows.Count - 1; i++)
            {
                if (!string.IsNullOrWhiteSpace(leveldt.Rows[i]["qty"].ToString()) && leveldt.Rows[i]["pl_level"].ToString() != "I~V总和")
                {
                    qtysum += Convert.ToDecimal(leveldt.Rows[i]["qty"].ToString());
                }
            }
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter($@"Quantity7", qtysum.ToString()));
            decimal multiplesum = 0;//倍数和
            for (int i = 0; i < leveldt.Rows.Count - 1; i++)
            {
                if (!string.IsNullOrWhiteSpace(leveldt.Rows[i]["multiple"].ToString()) && leveldt.Rows[i]["pl_level"].ToString() != "I~V总和")
                {
                    multiplesum += Convert.ToDecimal(leveldt.Rows[i]["multiple"].ToString());
                }
            }
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter($@"Multiple7", multiplesum.ToString()));


            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\rdlcReport\\IQC\\HPReport.rdlc";
            this.reportViewer1.LocalReport.SetParameters(PS);
            this.reportViewer1.RefreshReport();
        }
    }
}
