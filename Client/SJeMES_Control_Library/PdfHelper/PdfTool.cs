using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Control_Library.PdfHelper
{
    public static class PdfTool
    {
        /// </summary>
        /// <param name="directory">存放多个pdf的文件夹路径</param>
        /// <param name="pdfpath">合并的pdf路径</param>
        public static void MergePdf(string directory, string pdfName)
        {
            iTextSharp.text.Document document = null;
            try
            {
                string[] fileList = Directory.GetFiles(directory);//获取文件夹下的文件集合
                PdfReader reader;
                if (fileList.Length >= 1)
                {
                    //此处将内容从文本提取至文件流中的目的是避免文件被占用,无法删除
                    FileStream fs1 = new FileStream(fileList[0], FileMode.Open);
                    byte[] bytes1 = new byte[(int)fs1.Length];
                    fs1.Read(bytes1, 0, bytes1.Length);
                    fs1.Close();
                    reader = new PdfReader(bytes1);
                    reader.GetPageSize(1);
                    iTextSharp.text.Rectangle rec = reader.GetPageSize(1);
                    document = new iTextSharp.text.Document(rec, 50, 50, 50, 50);
                    FileStream f = new FileStream(Path.Combine(directory, pdfName + ".pdf"), FileMode.OpenOrCreate);
                    PdfWriter writer = PdfWriter.GetInstance(document, f);
                    document.Open();
                    PdfContentByte cb = writer.DirectContent;
                    PdfImportedPage newPage;
                    for (int i = 0; i < fileList.Length; i++)
                    {
                        FileStream fs = new FileStream(fileList[i], FileMode.Open);
                        byte[] bytes = new byte[(int)fs.Length];
                        fs.Read(bytes, 0, bytes.Length);
                        fs.Close();
                        reader = new PdfReader(bytes);
                        int iPageNum = reader.NumberOfPages;
                        for (int j = 1; j <= iPageNum; j++)
                        {
                            document.NewPage();
                            newPage = writer.GetImportedPage(reader, j);
                            cb.AddTemplate(newPage, 0, 0);
                        }
                        File.Delete(fileList[i]);
                    }
                    document.Close();
                }

            }
            catch (Exception e)
            {
            }
            finally
            {
                if (document != null)
                    document.Close();
            }
        }

    }
}
