using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_QCM_Inspection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QCM_Inspection
{
    public partial class F_QCM_Fitting_inspectionPrint : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        Dictionary<string, object> dic = new Dictionary<string, object>();
        public F_QCM_Fitting_inspectionPrint(Dictionary<string,object> _dic)
        {
            InitializeComponent();
            dic = _dic;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void F_QCM_Fitting_inspectionPrint_Load(object sender, EventArgs e)
        {
            #region 打印单头
            string order = DateTime.Now.ToString("yyyyMMddHHmmss"); // 检测单号
            UcInspectionPrintHead UcPrintHead = new UcInspectionPrintHead(order);

            UcPrintHead.No.Text = order; // 单号

            UcPrintHead.SJType1.Text = "试穿部送检";

            UcPrintHead.JCsubmit.Text = DateTime.Now.ToString("yyyy-MM-dd");

            if (dic["category_no"] != null)
                UcPrintHead.SYKind1.Text = dic["category_no"].ToString(); // 试样种类
            else
                UcPrintHead.SYKind1.Text = "";

            //if (datasource[0]["GENERAL_TESTTYPE_NAME"] != null)
            //    UcPrintHead.TYPEJC.Text = datasource[0]["GENERAL_TESTTYPE_NAME"].ToString(); // 检测类型
            //else
            //    UcPrintHead.TYPEJC.Text = "";

            if (dic["PARENT_ITEM_NO"] != null)
                UcPrintHead.ARTTest.Text = dic["PARENT_ITEM_NO"].ToString(); // ART
            else
                UcPrintHead.ARTTest.Text = "";



            if (dic["department_no"] != null)
                UcPrintHead.JDText.Text = dic["department_no"].ToString(); // 阶段
            else
                UcPrintHead.JDText.Text = "";




            if (dic["plantarea_no"] != null)
                UcPrintHead.AreaText.Text = dic["plantarea_no"].ToString(); // 厂区
            else
                UcPrintHead.AreaText.Text = "";

            UcPrintHead.Dock = DockStyle.Fill;

            this.panel1.Controls.Add(UcPrintHead);

            #endregion
        }
    }
}
