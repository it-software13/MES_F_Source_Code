using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Report
{
    public static class RDLCExport
    {
        /// <summary>
        /// rdlc导出
        /// </summary>
        /// <param name="ReportViewer">ReportViewer控件对象</param>
        /// <param name="ft">导出文件类型</param>
        /// <param name="filename">导出文件名</param>
        /// <param name="savePath">导出文件保存路径</param>
        public static void CreateFile(ReportViewer ReportViewer, FileType ft, string filename, string savePath)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding = "utf-8";
            string extension;

            byte[] bytes = ReportViewer.LocalReport.Render(ft.ToString(), null, out mimeType,
                           out encoding, out extension, out streamids, out warnings);

            FileStream fs = new FileStream(Path.Combine(savePath, filename), FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            fs.Dispose();
        }

    }

    public enum FileType
    {
        PDF,
        Image,
        Excel,
        Word
    }
}
