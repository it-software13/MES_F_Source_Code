using GDSJ_Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_Control_Library.Forms
{
    public partial class FrmSelectPrint : Form
    {
        private string fileNames;
        private List<DataSet> dsas;
        public FrmSelectPrint(string fileName,List<DataSet> dataSets)
        {
            InitializeComponent();
            fileNames = fileName;
            dsas = dataSets;
        }

        private void FrmSelectPrint_Load(object sender, EventArgs e)
        {
            string name = "printPreviewControl";
            if (dsas.Count > 0)
            {
                for (int i = 0; i < dsas.Count; i++)
                {
                    PrintPreviewControl printPreviewControl = new PrintPreviewControl();

                    printPreviewControl.Name = name + i;
                    printPreviewControl.Width = this.flowLayoutPanel1.Width;
                    printPreviewControl.Height = 800;
                    printPreviewControl.Anchor = AnchorStyles.None;
                    this.Invoke(new MethodInvoker(delegate
                    {
                        this.flowLayoutPanel1.Controls.Add(printPreviewControl);

                    }));
                    LoadFastReport(printPreviewControl, fileNames, dsas[i]);
                }
            }
            else
            {
                MessageBox.Show("Could not find print data, please check");
            }
        }
        public static void LoadFastReport(Control ctr, string fileName, DataSet dsa)
        {
            try
            {
                if (!System.IO.File.Exists(fileName))
                {
                    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                    MessageBox.Show("Report file not found: " + fileName, "report prompt");
                    return;
                }
                ctr.Controls.Clear();
                FastReport.Report report = new FastReport.Report();
                FastReport.Preview.PreviewControl previewControl = new FastReport.Preview.PreviewControl();//Create a report control

                previewControl.Dock = System.Windows.Forms.DockStyle.Fill;//Fill the entire control
                ctr.Controls.Add(previewControl);//Add controls

                report.Preview = previewControl;//Specify the preview in this control. If there is no such line, a window preview will pop up.

                report.Load(fileName);//Load report

                report.RegisterData(dsa);

                report.Prepare();
                report.ShowPrepared(true);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
