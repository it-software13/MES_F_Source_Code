using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_Shared_Form.SJeMES_AQL
{
    public class AqlreportExportHelper
    {
        public static void ExportAqlReport(string filePath, string exportPath, string exportFileName, Dictionary<string, string> replaceDic, DataGridView dgv_left, DataGridView dgv_right)
        {
            //首先根据需要读取的文件创建一个文件流对象
            using (FileStream fs = File.OpenRead(filePath))
            {
                IWorkbook workbook = null;
                //这里需要根据文件名格式判断一下
                //HSSF只能读取xls的
                //XSSF只能读取xlsx格式的
                string suffix = Path.GetExtension(fs.Name);
                if (suffix == ".xls")
                {
                    workbook = new HSSFWorkbook(fs);
                }
                else if (suffix == ".xlsx")
                {
                    workbook = new XSSFWorkbook(fs);
                }
                //因为Excel表中可能不止一个工作表，这里为了演示，我们遍历所有工作表
                for (int i = 0; i < workbook.NumberOfSheets; i++)
                {
                    //得到当前sheet
                    ISheet sheet = workbook.GetSheetAt(i);

                    //for (int r = 0; r < 100; r++)
                    //{
                    //    IRow row = sheet.CreateRow(r);       //在第一行位置创建一行。
                    //    row.CreateCell(0).SetCellValue("测试");       //在第一列位置创建一列，并赋值“测试”。
                    //}
                    //也可以通过GetSheet(name)得到
                    //遍历表中所有的行
                    //注意这里加1，这里得到的最后一个单元格的索引默认是从0开始的
                    for (int j = 0; j < sheet.LastRowNum + 1; j++)
                    {
                        //得到当前的行
                        IRow row = sheet.GetRow(j);
                        if (row == null)
                            continue;
                        //遍历每行所有的单元格
                        //注意这里不用加1，这里得到的最后一个单元格的索引默认是从1开始的
                        for (int k = 0; k < row.LastCellNum; k++)
                        {
                            //得到当前单元格
                            ICell cell = row.GetCell(k);
                            var objCell = GetCellValue(cell);
                            if (replaceDic.ContainsKey(objCell.ToString()))
                            {
                                cell.SetCellValue(replaceDic[objCell.ToString()]);
                            }
                        }
                    }

                    //27行开始插入数据
                    int insert_index = 26;
                    int for_count = 0;
                    if (dgv_left.Rows.Count > dgv_right.Rows.Count)
                        for_count = dgv_left.Rows.Count;
                    else
                        for_count = dgv_right.Rows.Count;

                    sheet.ShiftRows(insert_index, insert_index + 8, for_count);

                    for (int d = 0; d < for_count; d++)
                    {
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        var curr_insert_row = sheet.CreateRow(insert_index);
                        curr_insert_row.Height = 600;
                        for (int e = 0; e < 22; e++)
                        {
                            curr_insert_row.CreateCell(e);
                        }
                        CellRangeAddress region1 = new CellRangeAddress(insert_index, insert_index, 1, 6);
                        sheet.AddMergedRegion(region1);
                        CellRangeAddress region2 = new CellRangeAddress(insert_index, insert_index, 12, 16);
                        sheet.AddMergedRegion(region2);
                        if (!(d > (dgv_left.Rows.Count - 1)))
                        {
                            ICell CodeDefect_Cell = curr_insert_row.GetCell(0);//不良代码
                            SetICellStyle(CodeDefect_Cell, workbook);
                            CodeDefect_Cell.SetCellValue(GetCellValueStr(dgv_left.Rows[d].Cells["CodeDefect"].Value));
                            ICell DEFECT_DESCRIPTION_Cell = curr_insert_row.GetCell(1);//不良描述
                            SetICellStyle(DEFECT_DESCRIPTION_Cell, workbook);
                            SetICellStyle(curr_insert_row.GetCell(2), workbook);
                            SetICellStyle(curr_insert_row.GetCell(3), workbook);
                            SetICellStyle(curr_insert_row.GetCell(4), workbook);
                            SetICellStyle(curr_insert_row.GetCell(5), workbook);
                            SetICellStyle(curr_insert_row.GetCell(6), workbook);
                            DEFECT_DESCRIPTION_Cell.SetCellValue(GetCellValueStr(dgv_left.Rows[d].Cells["DEFECT_DESCRIPTION"].Value));
                            ICell BAD_STANDARD_Cell = curr_insert_row.GetCell(7);//不良描述标准
                            SetICellStyle(BAD_STANDARD_Cell, workbook);
                            BAD_STANDARD_Cell.SetCellValue(GetCellValueStr(dgv_left.Rows[d].Cells["BAD_STANDARD"].Value));
                            ICell MINOR_DEFECT_Cell = curr_insert_row.GetCell(8);//轻微不良
                            SetICellStyle(MINOR_DEFECT_Cell, workbook);
                            MINOR_DEFECT_Cell.SetCellValue(GetCellValueStr(dgv_left.Rows[d].Cells["MINOR_DEFECT"].Value));
                            ICell MAJOR_DEFECT_Cell = curr_insert_row.GetCell(9);//严重不良
                            SetICellStyle(MAJOR_DEFECT_Cell, workbook);
                            MAJOR_DEFECT_Cell.SetCellValue(GetCellValueStr(dgv_left.Rows[d].Cells["MAJOR_DEFECT"].Value));
                            ICell CRITICAL_DEFECT_Cell = curr_insert_row.GetCell(10);//重大不良
                            SetICellStyle(CRITICAL_DEFECT_Cell, workbook);
                            CRITICAL_DEFECT_Cell.SetCellValue(GetCellValueStr(dgv_left.Rows[d].Cells["CRITICAL_DEFECT"].Value));

                            if (dgv_left.Rows[d].DefaultCellStyle.BackColor == Color.Gray)
                            {
                                IFont font = workbook.CreateFont();
                                font.FontName = "宋体";
                                font.FontHeightInPoints = 11;
                                font.Boldweight = (short)FontBoldWeight.Bold;
                                CodeDefect_Cell.CellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                                CodeDefect_Cell.CellStyle.FillPattern = FillPattern.SolidForeground;
                                CodeDefect_Cell.CellStyle.SetFont(font);
                                DEFECT_DESCRIPTION_Cell.CellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                                DEFECT_DESCRIPTION_Cell.CellStyle.FillPattern = FillPattern.SolidForeground;
                                DEFECT_DESCRIPTION_Cell.CellStyle.SetFont(font);
                                BAD_STANDARD_Cell.CellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                                BAD_STANDARD_Cell.CellStyle.FillPattern = FillPattern.SolidForeground;
                                BAD_STANDARD_Cell.CellStyle.SetFont(font);
                                MINOR_DEFECT_Cell.CellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                                MINOR_DEFECT_Cell.CellStyle.FillPattern = FillPattern.SolidForeground;
                                MINOR_DEFECT_Cell.CellStyle.SetFont(font);
                                MAJOR_DEFECT_Cell.CellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                                MAJOR_DEFECT_Cell.CellStyle.FillPattern = FillPattern.SolidForeground;
                                MAJOR_DEFECT_Cell.CellStyle.SetFont(font);
                                CRITICAL_DEFECT_Cell.CellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                                CRITICAL_DEFECT_Cell.CellStyle.FillPattern = FillPattern.SolidForeground;
                                CRITICAL_DEFECT_Cell.CellStyle.SetFont(font);

                                curr_insert_row.GetCell(2).CellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                                curr_insert_row.GetCell(2).CellStyle.FillPattern = FillPattern.SolidForeground;
                                curr_insert_row.GetCell(2).CellStyle.SetFont(font);
                                curr_insert_row.GetCell(3).CellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                                curr_insert_row.GetCell(3).CellStyle.FillPattern = FillPattern.SolidForeground;
                                curr_insert_row.GetCell(3).CellStyle.SetFont(font);
                                curr_insert_row.GetCell(4).CellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                                curr_insert_row.GetCell(4).CellStyle.FillPattern = FillPattern.SolidForeground;
                                curr_insert_row.GetCell(4).CellStyle.SetFont(font);
                                curr_insert_row.GetCell(5).CellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                                curr_insert_row.GetCell(5).CellStyle.FillPattern = FillPattern.SolidForeground;
                                curr_insert_row.GetCell(5).CellStyle.SetFont(font);
                                curr_insert_row.GetCell(6).CellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                                curr_insert_row.GetCell(6).CellStyle.FillPattern = FillPattern.SolidForeground;
                                curr_insert_row.GetCell(6).CellStyle.SetFont(font);
                            }
                            else
                            {
                                if (MINOR_DEFECT_Cell.ToString() == "")
                                {
                                    MINOR_DEFECT_Cell.CellStyle.FillPattern = FillPattern.ThinForwardDiagonals;
                                }
                                if (MAJOR_DEFECT_Cell.ToString() == "")
                                {
                                    MAJOR_DEFECT_Cell.CellStyle.FillPattern = FillPattern.ThinForwardDiagonals;
                                }
                                if (CRITICAL_DEFECT_Cell.ToString() == "")
                                {
                                    CRITICAL_DEFECT_Cell.CellStyle.FillPattern = FillPattern.ThinForwardDiagonals;
                                }
                            }
                        }
                        if (!(d > (dgv_right.Rows.Count - 1)))
                        {
                            ICell CodeDefect_Cell = curr_insert_row.GetCell(11);//不良代码
                            SetICellStyle(CodeDefect_Cell, workbook);
                            CodeDefect_Cell.SetCellValue(GetCellValueStr(dgv_right.Rows[d].Cells["CodeDefect2"].Value));
                            ICell DEFECT_DESCRIPTION_Cell = curr_insert_row.GetCell(12);//不良描述
                            SetICellStyle(DEFECT_DESCRIPTION_Cell, workbook);
                            SetICellStyle(curr_insert_row.GetCell(13), workbook);
                            SetICellStyle(curr_insert_row.GetCell(14), workbook);
                            SetICellStyle(curr_insert_row.GetCell(15), workbook);
                            SetICellStyle(curr_insert_row.GetCell(16), workbook);
                            DEFECT_DESCRIPTION_Cell.SetCellValue(GetCellValueStr(dgv_right.Rows[d].Cells["DEFECT_DESCRIPTION2"].Value));
                            ICell BAD_STANDARD_Cell = curr_insert_row.GetCell(17);//不良描述标准
                            SetICellStyle(BAD_STANDARD_Cell, workbook);
                            BAD_STANDARD_Cell.SetCellValue(GetCellValueStr(dgv_right.Rows[d].Cells["BAD_STANDARD2"].Value));
                            ICell MINOR_DEFECT_Cell = curr_insert_row.GetCell(18);//轻微不良
                            SetICellStyle(MINOR_DEFECT_Cell, workbook);
                            MINOR_DEFECT_Cell.SetCellValue(GetCellValueStr(dgv_right.Rows[d].Cells["MINOR_DEFECT2"].Value));
                            ICell MAJOR_DEFECT_Cell = curr_insert_row.GetCell(19);//严重不良
                            SetICellStyle(MAJOR_DEFECT_Cell, workbook);
                            MAJOR_DEFECT_Cell.SetCellValue(GetCellValueStr(dgv_right.Rows[d].Cells["MAJOR_DEFECT2"].Value));
                            ICell CRITICAL_DEFECT_Cell = curr_insert_row.GetCell(20);//重大不良
                            SetICellStyle(CRITICAL_DEFECT_Cell, workbook);
                            CRITICAL_DEFECT_Cell.SetCellValue(GetCellValueStr(dgv_right.Rows[d].Cells["CRITICAL_DEFECT2"].Value));

                            if (dgv_right.Rows[d].DefaultCellStyle.BackColor == Color.Gray)
                            {
                                IFont font = workbook.CreateFont();
                                font.FontName = "宋体";
                                font.FontHeightInPoints = 11;
                                font.Boldweight = (short)FontBoldWeight.Bold;
                                CodeDefect_Cell.CellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                                CodeDefect_Cell.CellStyle.FillPattern = FillPattern.SolidForeground;
                                CodeDefect_Cell.CellStyle.SetFont(font);
                                DEFECT_DESCRIPTION_Cell.CellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                                DEFECT_DESCRIPTION_Cell.CellStyle.FillPattern = FillPattern.SolidForeground;
                                DEFECT_DESCRIPTION_Cell.CellStyle.SetFont(font);
                                BAD_STANDARD_Cell.CellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                                BAD_STANDARD_Cell.CellStyle.FillPattern = FillPattern.SolidForeground;
                                BAD_STANDARD_Cell.CellStyle.SetFont(font);
                                MINOR_DEFECT_Cell.CellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                                MINOR_DEFECT_Cell.CellStyle.FillPattern = FillPattern.SolidForeground;
                                MINOR_DEFECT_Cell.CellStyle.SetFont(font);
                                MAJOR_DEFECT_Cell.CellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                                MAJOR_DEFECT_Cell.CellStyle.FillPattern = FillPattern.SolidForeground;
                                MAJOR_DEFECT_Cell.CellStyle.SetFont(font);
                                CRITICAL_DEFECT_Cell.CellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                                CRITICAL_DEFECT_Cell.CellStyle.FillPattern = FillPattern.SolidForeground;
                                CRITICAL_DEFECT_Cell.CellStyle.SetFont(font);

                                curr_insert_row.GetCell(13).CellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                                curr_insert_row.GetCell(13).CellStyle.FillPattern = FillPattern.SolidForeground;
                                curr_insert_row.GetCell(13).CellStyle.SetFont(font);
                                curr_insert_row.GetCell(14).CellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                                curr_insert_row.GetCell(14).CellStyle.FillPattern = FillPattern.SolidForeground;
                                curr_insert_row.GetCell(14).CellStyle.SetFont(font);
                                curr_insert_row.GetCell(15).CellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                                curr_insert_row.GetCell(15).CellStyle.FillPattern = FillPattern.SolidForeground;
                                curr_insert_row.GetCell(15).CellStyle.SetFont(font);
                                curr_insert_row.GetCell(16).CellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                                curr_insert_row.GetCell(16).CellStyle.FillPattern = FillPattern.SolidForeground;
                                curr_insert_row.GetCell(16).CellStyle.SetFont(font);
                            }
                            else
                            {
                                if (MINOR_DEFECT_Cell.ToString() == "")
                                {
                                    MINOR_DEFECT_Cell.CellStyle.FillPattern = FillPattern.ThinForwardDiagonals;
                                }
                                if (MAJOR_DEFECT_Cell.ToString() == "")
                                {
                                    MAJOR_DEFECT_Cell.CellStyle.FillPattern = FillPattern.ThinForwardDiagonals;
                                }
                                if (CRITICAL_DEFECT_Cell.ToString() == "")
                                {
                                    CRITICAL_DEFECT_Cell.CellStyle.FillPattern = FillPattern.ThinForwardDiagonals;
                                }
                            }
                        }

                        insert_index++;
                    }

                }

                SaveExcel(exportPath, exportFileName, workbook, suffix);
            }
        }

        public static string GetCellValueStr(object cell)
        {
            return (cell == null) ? "" : cell.ToString();
        }

        /// <summary>
        /// 获取单元格的值
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static object GetCellValue(ICell item)
        {
            if (item == null)
            {
                return string.Empty;
            }
            switch (item.CellType)
            {
                case CellType.Boolean:
                    return item.BooleanCellValue;

                case CellType.Error:
                    return ErrorEval.GetText(item.ErrorCellValue);

                case CellType.Formula:
                    switch (item.CachedFormulaResultType)
                    {
                        case CellType.Boolean:
                            return item.BooleanCellValue;

                        case CellType.Error:
                            return ErrorEval.GetText(item.ErrorCellValue);

                        case CellType.Numeric:
                            if (DateUtil.IsCellDateFormatted(item))
                            {
                                return item.DateCellValue.ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                return item.NumericCellValue;
                            }
                        case CellType.String:
                            string str = item.StringCellValue;
                            if (!string.IsNullOrEmpty(str))
                            {
                                return str.ToString();
                            }
                            else
                            {
                                return string.Empty;
                            }
                        case CellType.Unknown:
                        case CellType.Blank:
                        default:
                            return string.Empty;
                    }
                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(item))
                    {
                        return item.DateCellValue.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        return item.NumericCellValue;
                    }
                case CellType.String:
                    string strValue = item.StringCellValue;
                    return strValue.ToString().Trim();

                case CellType.Unknown:
                case CellType.Blank:
                default:
                    return string.Empty;
            }
        }

        public static void SetICellStyle(ICell cell, IWorkbook workbook)
        {
            var cellStyle = workbook.CreateCellStyle();
            cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.VerticalAlignment = VerticalAlignment.Center;//垂直居中

            cell.CellStyle = cellStyle;
        }

        public static void SaveExcel(string dicPath, string fileName, IWorkbook workbook, string suffix)
        {
            if (Directory.Exists(dicPath) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(dicPath);
            }
            FileStream file = new FileStream(Path.Combine(dicPath, fileName + suffix), FileMode.Create);
            workbook.Write(file);
            file.Close();
            workbook.Close();
        }

    }
}
