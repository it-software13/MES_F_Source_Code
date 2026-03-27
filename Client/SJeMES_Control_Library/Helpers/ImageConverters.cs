using System;
using System.Collections.Generic;
using System.Text;
using OMCS.Engine.WhiteBoard;
using ESBasic;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;
using Aspose.Cells;

namespace SJeMES_Control_Library.WhiteBoardTest
{
    /*  
     * 
     * 将pdf、ppf、word转换给图片的组件有很多，这里仅使用Aspose组件（试用版）作为示例。
     * 
     * Aspose官网：www.aspose.com， 请支持和购买正版Aspose组件。
     * 
     */

    #region 图片转换器工厂 -> 将被注入到OMCS的多媒体管理器IMultimediaManager的ImageConverterFactory属性
    /// <summary>
    /// 图片转换器工厂。
    /// </summary>
    public class ImageConverterFactory : IImageConverterFactory
    {
        public IImageConverter CreateImageConverter(string extendName)
        {

            string licenseStr = "PExpY2Vuc2U+CiAgPERhdGE+CiAgICA8TGljZW5zZWRUbz5TdXpob3UgQXVuYm94IFNvZnR3YXJlIENvLiwgTHRkLjwvTGljZW5zZWRUbz4KICAgIDxFbWFpbFRvPnNhbGVzQGF1bnRlYy5jb208L0VtYWlsVG8+CiAgICA8TGljZW5zZVR5cGU+RGV2ZWxvcGVyIE9FTTwvTGljZW5zZVR5cGU+CiAgICA8TGljZW5zZU5vdGU+TGltaXRlZCB0byAxIGRldmVsb3BlciwgdW5saW1pdGVkIHBoeXNpY2FsIGxvY2F0aW9uczwvTGljZW5zZU5vdGU+CiAgICA8T3JkZXJJRD4yMDA2MDIwMTI2MzM8L09yZGVySUQ+CiAgICA8VXNlcklEPjEzNDk3NjAwNjwvVXNlcklEPgogICAgPE9FTT5UaGlzIGlzIGEgcmVkaXN0cmlidXRhYmxlIGxpY2Vuc2U8L09FTT4KICAgIDxQcm9kdWN0cz4KICAgICAgPFByb2R1Y3Q+QXNwb3NlLlRvdGFsIGZvciAuTkVUPC9Qcm9kdWN0PgogICAgPC9Qcm9kdWN0cz4KICAgIDxFZGl0aW9uVHlwZT5FbnRlcnByaXNlPC9FZGl0aW9uVHlwZT4KICAgIDxTZXJpYWxOdW1iZXI+OTM2ZTVmZDEtODY2Mi00YWJmLTk1YmQtYzhkYzBmNTNhZmE2PC9TZXJpYWxOdW1iZXI+CiAgICA8U3Vic2NyaXB0aW9uRXhwaXJ5PjIwMjEwODI3PC9TdWJzY3JpcHRpb25FeHBpcnk+CiAgICA8TGljZW5zZVZlcnNpb24+My4wPC9MaWNlbnNlVmVyc2lvbj4KICAgIDxMaWNlbnNlSW5zdHJ1Y3Rpb25zPmh0dHBzOi8vcHVyY2hhc2UuYXNwb3NlLmNvbS9wb2xpY2llcy91c2UtbGljZW5zZTwvTGljZW5zZUluc3RydWN0aW9ucz4KICA8L0RhdGE+CiAgPFNpZ25hdHVyZT5wSkpjQndRdnYxV1NxZ1kyOHFJYUFKSysvTFFVWWRrQ2x5THE2RUNLU0xDQ3dMNkEwMkJFTnh5L3JzQ1V3UExXbjV2bTl0TDRQRXE1aFAzY2s0WnhEejFiK1JIWTBuQkh1SEhBY01TL1BSeEJES0NGbWg1QVFZRTlrT0FxSzM5NVBSWmJRSGowOUNGTElVUzBMdnRmVkp5cUhjblJvU3dPQnVqT1oyeDc4WFE9PC9TaWduYXR1cmU+CjwvTGljZW5zZT4=";
            extendName = extendName.ToLower();
            if (extendName == ".doc" || extendName == ".docx")
            {
                new Aspose.Words.License().SetLicense(new MemoryStream(Convert.FromBase64String(licenseStr)));
                return new Word2ImageConverter();
            }

            if (extendName == ".pdf")
            {
                new Aspose.Pdf.License().SetLicense(new MemoryStream(Convert.FromBase64String(licenseStr)));
                return new Pdf2ImageConverter();
            }

            if (extendName == ".ppt" || extendName == ".pptx")
            {
                new Aspose.Pdf.License().SetLicense(new MemoryStream(Convert.FromBase64String(licenseStr)));
                new Aspose.Slides.License().SetLicense(new MemoryStream(Convert.FromBase64String(licenseStr)));
                return new Ppt2ImageConverter();
            }

            if (extendName == ".xls" || extendName == ".xlsx")
            {
                new Aspose.Cells.License().SetLicense(new MemoryStream(Convert.FromBase64String(licenseStr)));
                new Aspose.Slides.License().SetLicense(new MemoryStream(Convert.FromBase64String(licenseStr)));
                return new Execl2ImageConverter();
            }

            if (extendName == ".rar")
            {
                return new Rar2ImageConverter();
            }

            return null;
        }

        public bool Support(string extendName)
        {
            return extendName == ".doc" || extendName == ".docx" || extendName == ".pdf" || extendName == ".ppt" || extendName == ".pptx" || extendName == ".rar";
        }


        public List<string> GetSupportedFileTypes()
        {
            List<string> list = new List<string>();
            list.Add(".doc");
            list.Add(".docx");
            list.Add(".pdf");
            list.Add(".ppt");
            list.Add(".pptx");
            list.Add(".rar");
            return list;
        }
    }
    #endregion

    #region 将word文档转换为图片
    public class Word2ImageConverter : IImageConverter
    {
        private bool cancelled = false;
        public event CbGeneric<int, int> ProgressChanged;
        public event CbGeneric ConvertSucceed;
        public event CbGeneric<string> ConvertFailed;

        public void Cancel()
        {
            if (this.cancelled)
            {
                return;
            }

            this.cancelled = true;
        }

        public void ConvertToImage(string originFilePath, string imageOutputDirPath)
        {
            this.cancelled = false;
            ConvertToImage(originFilePath, imageOutputDirPath, 0, 0, null, 200);
        }

        /// <summary>
        /// 将Word文档转换为图片的方法      
        /// </summary>
        /// <param name="wordInputPath">Word文件路径</param>
        /// <param name="imageOutputDirPath">图片输出路径，如果为空，默认值为Word所在路径</param>      
        /// <param name="startPageNum">从PDF文档的第几页开始转换，如果为0，默认值为1</param>
        /// <param name="endPageNum">从PDF文档的第几页开始停止转换，如果为0，默认值为Word总页数</param>
        /// <param name="imageFormat">设置所需图片格式，如果为null，默认格式为PNG</param>
        /// <param name="resolution">设置图片的像素，数字越大越清晰，如果为0，默认值为128，建议最大值不要超过1024</param>
        private void ConvertToImage(string wordInputPath, string imageOutputDirPath, int startPageNum, int endPageNum, ImageFormat imageFormat, int resolution)
        {
            try
            {
                System.Net.ServicePointManager.DefaultConnectionLimit = 100;

                //string filename = imageOutputDirPath + "/" + wordInputPath.Substring(wordInputPath.LastIndexOf(@"/"));
                //System.Net.WebClient webclient = new System.Net.WebClient();
                //webclient.Credentials = System.Net.CredentialCache.DefaultCredentials;
                //webclient.DownloadFile(wordInputPath, filename);
                //webclient.Dispose();

                Aspose.Words.Document doc = new Aspose.Words.Document(wordInputPath);

                if (doc == null)
                {
                    throw new Exception("Word文件无效或者Word文件被加密！");
                }

                if (imageOutputDirPath.Trim().Length == 0)
                {
                    imageOutputDirPath = Path.GetDirectoryName(wordInputPath);
                }

                if (!Directory.Exists(imageOutputDirPath))
                {
                    Directory.CreateDirectory(imageOutputDirPath);
                }

                if (startPageNum <= 0)
                {
                    startPageNum = 1;
                }

                if (endPageNum > doc.PageCount || endPageNum <= 0)
                {
                    endPageNum = doc.PageCount;
                }

                if (startPageNum > endPageNum)
                {
                    int tempPageNum = startPageNum; startPageNum = endPageNum; endPageNum = startPageNum;
                }

                if (imageFormat == null)
                {
                    imageFormat = ImageFormat.Png;
                }

                if (resolution <= 0)
                {
                    resolution = 128;
                }

                string imageName = Path.GetFileNameWithoutExtension(wordInputPath);
                Aspose.Words.Saving.ImageSaveOptions imageSaveOptions = new Aspose.Words.Saving.ImageSaveOptions(Aspose.Words.SaveFormat.Png);
                imageSaveOptions.Resolution = resolution;
                for (int i = startPageNum; i <= endPageNum; i++)
                {
                    if (this.cancelled)
                    {
                        break;
                    }
                    MemoryStream stream = new MemoryStream();
                    imageSaveOptions.PageIndex = i - 1;
                    string imgPath = Path.Combine(imageOutputDirPath, imageName) + "_" + i.ToString("000") + "." + imageFormat.ToString();
                    //string imgPath = Path.Combine(imageOutputDirPath, imageName) + ".JPG";
                    doc.Save(stream, imageSaveOptions);
                    Image img = Image.FromStream(stream);
                    Bitmap bm = ESBasic.Helpers.ImageHelper.Zoom(img, 0.6f);
                    bm.Save(imgPath, ImageFormat.Png);
                    img.Dispose();
                    stream.Dispose();
                    bm.Dispose();
                    //让线程睡觉
                    System.Threading.Thread.Sleep(200);
                    if (this.ProgressChanged != null)
                    {
                        this.ProgressChanged(i - 1, endPageNum);
                    }
                }
                if (this.cancelled)
                {
                    return;
                }

                if (this.ConvertSucceed != null)
                {
                    this.ConvertSucceed();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    #endregion

    #region 将pdf文档转换为图片
    public class Pdf2ImageConverter : IImageConverter
    {
        private bool cancelled = false;
        public event CbGeneric<int, int> ProgressChanged;
        public event CbGeneric ConvertSucceed;
        public event CbGeneric<string> ConvertFailed;

        public void Cancel()
        {
            if (this.cancelled)
            {
                return;
            }

            this.cancelled = true;
        }

        public void ConvertToImage(string originFilePath, string imageOutputDirPath)
        {
            this.cancelled = false;
            ConvertToImage(originFilePath, imageOutputDirPath, 0, 0, 200);
        }

        /// <summary>
        /// 将pdf文档转换为图片的方法      
        /// </summary>
        /// <param name="originFilePath">pdf文件路径</param>
        /// <param name="imageOutputDirPath">图片输出路径，如果为空，默认值为pdf所在路径</param>       
        /// <param name="startPageNum">从PDF文档的第几页开始转换，如果为0，默认值为1</param>
        /// <param name="endPageNum">从PDF文档的第几页开始停止转换，如果为0，默认值为pdf总页数</param>       
        /// <param name="resolution">设置图片的像素，数字越大越清晰，如果为0，默认值为128，建议最大值不要超过1024</param>
        private void ConvertToImage(string originFilePath, string imageOutputDirPath, int startPageNum, int endPageNum, int resolution)
        {
            try
            {
                //  Stream ddd=new 
                //Aspose.Pdf.Generator.Pdf ss=new Aspose.Pdf.Generator.Pdf()
                string filename = originFilePath;
                if (originFilePath.Contains("http"))
                {
                    filename = imageOutputDirPath + "/" + originFilePath.Substring(originFilePath.LastIndexOf(@"/"));
                    System.Net.ServicePointManager.DefaultConnectionLimit = 100;
                    System.Net.WebClient webclient = new System.Net.WebClient();
                    webclient.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    webclient.DownloadFile(originFilePath, filename);
                    webclient.Dispose();
                }
                Aspose.Pdf.Document doc = new Aspose.Pdf.Document(filename);

                if (doc == null)
                {
                    throw new Exception("pdf文件无效或者pdf文件被加密！");
                }

                if (imageOutputDirPath.Trim().Length == 0)
                {
                    imageOutputDirPath = Path.GetDirectoryName(originFilePath);
                }

                if (!Directory.Exists(imageOutputDirPath))
                {
                    Directory.CreateDirectory(imageOutputDirPath);
                }

                if (startPageNum <= 0)
                {
                    startPageNum = 1;
                }

                if (endPageNum > doc.Pages.Count || endPageNum <= 0)
                {
                    endPageNum = doc.Pages.Count;
                }

                if (startPageNum > endPageNum)
                {
                    int tempPageNum = startPageNum; startPageNum = endPageNum; endPageNum = startPageNum;
                }

                if (resolution <= 0)
                {
                    resolution = 128;
                }

                string imageNamePrefix = Path.GetFileNameWithoutExtension(originFilePath);
                for (int i = startPageNum; i <= endPageNum; i++)
                {
                    if (this.cancelled)
                    {
                        break;
                    }

                    MemoryStream stream = new MemoryStream();
                    string imgPath = Path.Combine(imageOutputDirPath, imageNamePrefix) + "_" + i.ToString("000") + ".jpg";
                    Aspose.Pdf.Devices.Resolution reso = new Aspose.Pdf.Devices.Resolution(resolution);
                    Aspose.Pdf.Devices.JpegDevice jpegDevice = new Aspose.Pdf.Devices.JpegDevice(reso, 100);
                    jpegDevice.Process(doc.Pages[i], stream);

                    Image img = Image.FromStream(stream);
                    Bitmap bm = ESBasic.Helpers.ImageHelper.Zoom(img, 0.6f);
                    bm.Save(imgPath, ImageFormat.Jpeg);
                    img.Dispose();
                    stream.Dispose();
                    bm.Dispose();

                    System.Threading.Thread.Sleep(200);
                    if (this.ProgressChanged != null)
                    {
                        this.ProgressChanged(i - 1, endPageNum);
                    }
                }

                if (this.cancelled)
                {
                    return;
                }

                if (this.ConvertSucceed != null)
                {
                    this.ConvertSucceed();
                }
            }
            catch (Exception ex)
            {
                if (this.ConvertFailed != null)
                {
                    this.ConvertFailed(ex.Message);
                }
            }
        }
    }
    #endregion

    #region 将ppt文档转换为图片
    public class Ppt2ImageConverter : IImageConverter
    {
        private Pdf2ImageConverter pdf2ImageConverter;
        public event CbGeneric<int, int> ProgressChanged;
        public event CbGeneric ConvertSucceed;
        public event CbGeneric<string> ConvertFailed;

        public void Cancel()
        {
            if (this.pdf2ImageConverter != null)
            {
                this.pdf2ImageConverter.Cancel();
            }
        }

        public void ConvertToImage(string originFilePath, string imageOutputDirPath)
        {
            ConvertToImage(originFilePath, imageOutputDirPath, 0, 0, 200);
        }

        /// <summary>
        /// 将pdf文档转换为图片的方法      
        /// </summary>
        /// <param name="originFilePath">ppt文件路径</param>
        /// <param name="imageOutputDirPath">图片输出路径，如果为空，默认值为pdf所在路径</param>       
        /// <param name="startPageNum">从PDF文档的第几页开始转换，如果为0，默认值为1</param>
        /// <param name="endPageNum">从PDF文档的第几页开始停止转换，如果为0，默认值为pdf总页数</param>       
        /// <param name="resolution">设置图片的像素，数字越大越清晰，如果为0，默认值为128，建议最大值不要超过1024</param>
        private void ConvertToImage(string originFilePath, string imageOutputDirPath, int startPageNum, int endPageNum, int resolution)
        {
            try
            {
                /* string key ="PExpY2Vuc2U+DQogIDxEYXRhPgOKICAgIDxMaWNlbnNIZFRvPkFzcG9zZSBTY290bGFuZCB"+ "UZNFtPC9MaWNlbnN1ZFRvPgOKICAgIDxFbWFpbFRvPm3pbGx5Lmx1bmRpZUBhc3Bvc2UuY2"+ 
                     "9tPC9FbWFpbFRvPg0KICAgIDxMaWNlbnNlVHlwZT5EZXZlbG9wZXIgT8VNPC9MaWNlbnNlV" + 
                     "HlwZT4NCiAgICA8TGljZw5zZU5vdGU+TGltaXR1ZCBObyAxIGRldmVsb3Blciwgdw5saw1p"+
                     "dGVkTHBoeXNpY2FsIGxvY2F0aw9uczwvTGljZW5zZU5vdGU+DQogICAgPE9yZGVySUQ+MTQ"+
                     "wNDA4MDUyMzIOPC9PcmRlcklEPgoKICAgIDxVc2VySUQ+0TQyMzY8L1VzZX]RD4NCiAgIC"+
                     "A8TOVNPlRoaXMgaXMgYSByZWRpc3RyawJ1dGFibGUgbGljZW5zZTwvTOVNPgOKICAgIDxOc"+
                     "m9kdWNOcz4NCiAgICAgIDxQcm9kdWNOPKFzcG9zZS5Ub3RhbCBmb3IgLk5FVDwvUHJvZHVj"+
                     "dD4NCiAgICA8L1Byb2R1Y3RzPgOKICAgIDxFZG10aw9uVHlwZT5FbnRlcnByaXNlPC9FZGl"+
                     "Oaw9uVHlwZT4NCiAgICA8U2VyaWFsTnVtYmVyPjlhNTk1NDdjLTQxZjAtNDI4Yi1iYTcyLT"+
                     "djNDM20GYxNTFkNzwvU2VyaWFsTnVtYmVyPgoKICAgIDxTdWJzY3JpcHRpb25FeHBpcnk+M"+
                     "jAxNTEyMzEBL1N1YnNjcmlwdGlvbkV4cGlyeT4NCiAgICA8TG1jZw5zZVZlcnNpb24+My4w"+
                     "PC9MawNlbnNlVmVyc2lvbj4NCiAgICA8TGljZW5zZUluc3RydlWNOaW9ucz5odHRwOi8vd3d"+
                     "3LmFzcG9zZS5jb20vY29ycG9yYXRlL3B1cmNoYXN1L2xpY2Vuc2UtaW5zdHJ1Y3Rpb25zLm"+
                     "FzcHg8LOxpY2Vuc2VJbnNOcnVjdGlvbnM+DQogIDwvRGFOYT4NCiAgPFNpZ25hdHNyZT5GT"+
                     "zNQSHNibGdEdDhGNTlzTVQxbDFhbXlpOXFrMlY2RThkUVtJUDdMZFRKU3hEaWJORUZ1MXpP"+
                     "aw5RYnFGZkt2L3]1dHR2Y3hvUk9rYzFOVWUvRHRPNmNQWVpmNkowVmVtZ1NZ0GkvTFpFQ1R"+
                     "Hc3pScUpWUVJaME1vVm5CaHVQQUprNWVsaTdmaFZjRjhoV2QzRTRYUTNMemZtSkN1YWoyTk"+
                     "veZVJpNUhyZmc9PC9TawduYXR1cmU+DQo8LOxpY2Vuc2U+";*/

                string filename = imageOutputDirPath + "/" + originFilePath.Substring(originFilePath.LastIndexOf(@"/"));

                System.Net.ServicePointManager.DefaultConnectionLimit = 100;
                System.Net.WebClient webclient = new System.Net.WebClient();
                webclient.Credentials = System.Net.CredentialCache.DefaultCredentials;
                webclient.DownloadFile(originFilePath, filename);
                webclient.Dispose();
                Aspose.Slides.Presentation doc = new Aspose.Slides.Presentation(filename);

                if (doc == null)
                {
                    throw new Exception("ppt文件无效或者ppt文件被加密！");
                }

                if (imageOutputDirPath.Trim().Length == 0)
                {
                    imageOutputDirPath = Path.GetDirectoryName(originFilePath);
                }

                if (!Directory.Exists(imageOutputDirPath))
                {
                    Directory.CreateDirectory(imageOutputDirPath);
                }

                if (startPageNum <= 0)
                {
                    startPageNum = 1;
                }

                if (endPageNum > doc.Slides.Count || endPageNum <= 0)
                {
                    endPageNum = doc.Slides.Count;
                }

                if (startPageNum > endPageNum)
                {
                    int tempPageNum = startPageNum; startPageNum = endPageNum; endPageNum = startPageNum;
                }

                if (resolution <= 0)
                {
                    resolution = 128;
                }

                //先将ppt转换为pdf临时文件
                string tmpPdfPath = filename.Substring(0, filename.LastIndexOf(".")) + ".pdf";
                doc.Save(tmpPdfPath, Aspose.Slides.Export.SaveFormat.Pdf);

                //再将pdf转换为图片
                Pdf2ImageConverter converter = new Pdf2ImageConverter();
                converter.ConvertFailed += new CbGeneric<string>(converter_ConvertFailed);
                converter.ConvertSucceed += new CbGeneric(converter_ConvertSucceed);
                converter.ProgressChanged += new CbGeneric<int, int>(converter_ProgressChanged);
                converter.ConvertToImage(tmpPdfPath, imageOutputDirPath);

                //删除pdf临时文件
                File.Delete(tmpPdfPath);

                if (this.ConvertSucceed != null)
                {
                    this.ConvertSucceed();
                }
            }
            catch (Exception ex)
            {
                if (this.ConvertFailed != null)
                {
                    this.ConvertFailed(ex.Message);
                }
            }

            this.pdf2ImageConverter = null;
        }

        void converter_ProgressChanged(int done, int total)
        {
            if (this.ProgressChanged != null)
            {
                this.ProgressChanged(done, total);
            }
        }

        void converter_ConvertSucceed()
        {
            if (this.ConvertSucceed != null)
            {
                this.ConvertSucceed();
            }
        }

        void converter_ConvertFailed(string msg)
        {
            if (this.ConvertFailed != null)
            {
                this.ConvertFailed(msg);
            }
        }
    }
    #endregion

    #region 将execl转换为图片
    public class Execl2ImageConverter : IImageConverter
    {
        private bool cancelled = false;
        public event CbGeneric<int, int> ProgressChanged;
        public event CbGeneric ConvertSucceed;
        public event CbGeneric<string> ConvertFailed;

        public void Cancel()
        {
            if (this.cancelled)
            {
                return;
            }

            this.cancelled = true;
        }

        public void ConvertToImage(string originFilePath, string imageOutputDirPath)
        {
            this.cancelled = false;
            ConvertToImage(originFilePath, imageOutputDirPath, 0, 0, null, 200);
        }

        /// <summary>
        /// 将execl文档转换为图片的方法      
        /// </summary>
        /// <param name="wordInputPath">Word文件路径</param>
        /// <param name="imageOutputDirPath">图片输出路径，如果为空，默认值为Word所在路径</param>      
        /// <param name="startPageNum">从PDF文档的第几页开始转换，如果为0，默认值为1</param>
        /// <param name="endPageNum">从PDF文档的第几页开始停止转换，如果为0，默认值为Word总页数</param>
        /// <param name="imageFormat">设置所需图片格式，如果为null，默认格式为PNG</param>
        /// <param name="resolution">设置图片的像素，数字越大越清晰，如果为0，默认值为128，建议最大值不要超过1024</param>
        private void ConvertToImage(string wordInputPath, string imageOutputDirPath, int startPageNum, int endPageNum, ImageFormat imageFormat, int resolution)
        {
            try
            {
                System.Net.ServicePointManager.DefaultConnectionLimit = 100;
                string filename = imageOutputDirPath  + wordInputPath.Substring(wordInputPath.LastIndexOf(@"\"));
                //System.Net.WebClient webclient = new System.Net.WebClient();
                //webclient.Credentials = System.Net.CredentialCache.DefaultCredentials;
                //webclient.DownloadFile(wordInputPath, filename);
                //webclient.Dispose();
                string imageName = Path.GetFileNameWithoutExtension(wordInputPath);
                Workbook book = new Workbook(filename);
                //创建一个图表选项的对象
                Aspose.Cells.Rendering.ImageOrPrintOptions imgOptions = new Aspose.Cells.Rendering.ImageOrPrintOptions();

                imgOptions.OnePagePerSheet = true;

                imgOptions.PrintingPage = PrintingPageType.IgnoreBlank;
                imgOptions.ImageFormat = ImageFormat.Jpeg;
                int count = book.Worksheets.Count;
                for (int i = 0; i < count; i++)
                {
                    //获取一张工作表
                    Worksheet sheet = book.Worksheets[i];
                    //创建一个纸张底色渲染对象
                    Aspose.Cells.Rendering.SheetRender sr = new Aspose.Cells.Rendering.SheetRender(sheet, imgOptions);
                    for (int j = 0; j < sr.PageCount; j++)
                    {
                        string imgpath = imageOutputDirPath + "/" + imageName + "_" + i + "_" + j + "_.jpg";
                        if (!File.Exists(imgpath))
                        {
                            sr.ToImage(j, imgpath);
                        }
                    }
                }
                if (this.cancelled)
                {
                    return;
                }

                if (this.ConvertSucceed != null)
                {
                    this.ConvertSucceed();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }


    #endregion

    #region 将图片压缩包解压。（如果课件本身就是多张图片，那么可以将它们压缩成一个rar，作为一个课件）
    /// <summary>
    /// 将图片压缩包解压。（如果课件本身就是多张图片，那么可以将它们压缩成一个rar，作为一个课件）
    /// </summary>
    public class Rar2ImageConverter : IImageConverter
    {
        private bool cancelled = false;
        public event CbGeneric<string> ConvertFailed;
        public event CbGeneric<int, int> ProgressChanged;
        public event CbGeneric ConvertSucceed;

        public void Cancel()
        {
            this.cancelled = true;
        }


        public void ConvertToImage(string rarPath, string imageOutputDirPath)
        {
            try
            {
                //Unrar tmp = new Unrar(rarPath);
                //tmp.Open(Unrar.OpenMode.List);
                //string[] files = tmp.ListFiles();
                //tmp.Close();

                //int total = files.Length;
                //int done = 0;

                //Unrar unrar = new Unrar(rarPath);
                //unrar.Open(Unrar.OpenMode.Extract);
                //unrar.DestinationPath = imageOutputDirPath;

                //while (unrar.ReadHeader() && !cancelled)
                //{
                //    if (unrar.CurrentFile.IsDirectory)
                //    {
                //        unrar.Skip();
                //    }
                //    else
                //    {
                //        unrar.Extract();
                //        ++done;

                //        if (this.ProgressChanged != null)
                //        {
                //            this.ProgressChanged(done, total);
                //        }
                //    }
                //}
                //unrar.Close();

                //if (this.ConvertSucceed != null)
                //{
                //    this.ConvertSucceed();
                //}

            }
            catch (Exception ex)
            {
                if (this.ConvertFailed != null)
                {
                    this.ConvertFailed(ex.Message);
                }
            }
        }


    }
    #endregion
}
