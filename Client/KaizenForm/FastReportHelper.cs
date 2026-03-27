


using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using FastReport;
using System.Drawing; // Required for Image
using FastReport.Preview;

namespace KaizenForm
{
    class FastReportHelper
    {
        public static void LoadFastReport1(Control ctr, string fileName, Dictionary<string, string> dicParameter, DataTable dt, string tablename)
        {
            try
            {
                ctr.Controls.Clear();
                FastReport.Report report = new FastReport.Report();
                PreviewControl previewControl = new PreviewControl();

                previewControl.Dock = DockStyle.Fill;
                ctr.Controls.Add(previewControl);
                report.Preview = previewControl;

                report.Load(fileName);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    string Kaizen_number = dt.Rows[0]["Kaizen #"].ToString();
                    string KaizenHeading = dt.Rows[0]["KaizenHeading"].ToString();
                    string Proposer_Department = dt.Rows[0]["Proposer_Department"].ToString();
                    string Proposer_area = dt.Rows[0]["Proposer_area"].ToString();
                    string Proposer_line = dt.Rows[0]["Proposer_line"].ToString();
                    string Kaizen_Type = dt.Rows[0]["Kaizen_Type"].ToString();
                    string Dept_Code = dt.Rows[0]["Dept_Code"].ToString();
                    string Date = dt.Rows[0]["Date"].ToString();
                    string Projected_Area = dt.Rows[0]["Projected_Area"].ToString();
                    string Projected_Line = dt.Rows[0]["Projected_Line"].ToString();
                    string Status = dt.Rows[0]["Status"].ToString();
                    byte[] Before_Image = (byte[])dt.Rows[0]["Before_Image"];
                    byte[] After_Image = (byte[])dt.Rows[0]["After_Image"];
                    byte[] Proposer_Pic = (byte[])dt.Rows[0]["Proposer_Pic"];
                    string Proposer_Barcode = dt.Rows[0]["Proposer_Barcode"].ToString();
                    string Proposer_Name = dt.Rows[0]["Proposer_Name"].ToString();
                    string Proposer_Designation = dt.Rows[0]["Proposer_Designation"].ToString();
                    string Bonus = dt.Rows[0]["Bonus"].ToString();
                    string CT_Before = dt.Rows[0]["CT_Before"].ToString();
                    string CT_After = dt.Rows[0]["CT_After"].ToString();
                    string CT_Savings = dt.Rows[0]["CT_Savings"].ToString();
                    string CT_Improved = dt.Rows[0]["CT_Improved"].ToString();
                    string Output_Before = dt.Rows[0]["Output_Before"].ToString();
                    string Output_After = dt.Rows[0]["Output_After"].ToString();
                    string Output_Saved = dt.Rows[0]["Output_Saved"].ToString();
                    string Output_Improve = dt.Rows[0]["Output_Improve"].ToString();
                    string Manpower_Before = dt.Rows[0]["Manpower_Before"].ToString();
                    string Manpower_After = dt.Rows[0]["Manpower_After"].ToString();
                    string Manpower_Saved = dt.Rows[0]["Manpower_Saved"].ToString();
                    string Manpower_Improved = dt.Rows[0]["Manpower_Improved"].ToString();
                    string Monthly_Order_Quantity = dt.Rows[0]["Monthly_Order_Quantity"].ToString();
                    string Overall_CT_Savings = dt.Rows[0]["Overall_CT_Savings"].ToString();
                    string Overall_Savings = dt.Rows[0]["Overall_Savings"].ToString();
                    string Bonus_Evalution = dt.Rows[0]["Bonus_Evalution"].ToString();
                    string Before_Kaizen = dt.Rows[0]["Before_Kaizen"].ToString();
                    string After_Kaizen = dt.Rows[0]["After_Kaizen"].ToString();
                    string model = dt.Rows[0]["model"].ToString();
                    string Type_ECRS = dt.Rows[0]["Type(ECRS)"].ToString();
                    string Projected_Department = dt.Rows[0]["Projected_Department"].ToString();
                    string CW_Barcode = dt.Rows[0]["CW_Barcode"].ToString();
                    string CW_Name = dt.Rows[0]["CW_Name"].ToString();

                    // Find text and image objects
                    var TextObject1 = report.FindObject("Text78") as FastReport.TextObject;
                    var TextObject2 = report.FindObject("Kaizentxt") as FastReport.TextObject;
                    var TextObject3 = report.FindObject("KaizenTypeTxt") as FastReport.TextObject;
                    var TextObject4 = report.FindObject("ProposerDeptTxt") as FastReport.TextObject;
                    var TextObject5 = report.FindObject("ProposerAreaTxt") as FastReport.TextObject;
                    var TextObject6 = report.FindObject("ProposerLineTxt") as FastReport.TextObject;
                    var TextObject7 = report.FindObject("DeptCodeTxt") as FastReport.TextObject;
                    var TextObject8 = report.FindObject("TypeECRSTxt") as FastReport.TextObject;
                    var TextObject9 = report.FindObject("CWBarcodeTxt") as FastReport.TextObject;
                    var TextObject10 = report.FindObject("CWNameTxt") as FastReport.TextObject;
                    var TextObject11 = report.FindObject("ProjectedDeptTxt") as FastReport.TextObject;
                    var TextObject12 = report.FindObject("KaizenDateTxt") as FastReport.TextObject;
                    var TextObject13 = report.FindObject("ProjectedAreaTxt") as FastReport.TextObject;
                    var TextObject14 = report.FindObject("ProjectedLineTxt") as FastReport.TextObject;
                    var TextObject15 = report.FindObject("StatusTxt") as FastReport.TextObject;
                    var Picture1 = report.FindObject("Picture1") as FastReport.PictureObject;
                    var Picture2 = report.FindObject("Picture2") as FastReport.PictureObject;
                    var Picture3 = report.FindObject("Picture3") as FastReport.PictureObject;
                    var TextObject16 = report.FindObject("ProposerBarcodeTxt") as FastReport.TextObject;
                    var TextObject17 = report.FindObject("ProposerNameTxt") as FastReport.TextObject;
                    var TextObject18 = report.FindObject("ProposerDesignationTxt") as FastReport.TextObject;
                    var TextObject19 = report.FindObject("BonusTxt") as FastReport.TextObject;
                    var TextObject20 = report.FindObject("CTBeforeTxt") as FastReport.TextObject;
                    var TextObject21 = report.FindObject("CTAfterTxt") as FastReport.TextObject;
                    var TextObject22 = report.FindObject("CTSavingsTxt") as FastReport.TextObject;
                    var TextObject23 = report.FindObject("CTImprovedTxt") as FastReport.TextObject;
                    var TextObject24 = report.FindObject("OutputBeforeTxt") as FastReport.TextObject;
                    var TextObject25 = report.FindObject("OutputAfterTxt") as FastReport.TextObject;
                    var TextObject26 = report.FindObject("OutputSavedTxt") as FastReport.TextObject;
                    var TextObject27 = report.FindObject("OutputImproveTxt") as FastReport.TextObject;
                    var TextObject28 = report.FindObject("ManpowerBeforeTxt") as FastReport.TextObject;
                    var TextObject29 = report.FindObject("ManpowerAfterTxt") as FastReport.TextObject;
                    var TextObject30 = report.FindObject("ManpowerSavedTxt") as FastReport.TextObject;
                    var TextObject31 = report.FindObject("ManpowerImprovedTxt") as FastReport.TextObject;
                    var TextObject32 = report.FindObject("MonthlyOrderQtyTxt") as FastReport.TextObject;
                    var TextObject33 = report.FindObject("OverallCTSavingsTxt") as FastReport.TextObject;
                    var TextObject34 = report.FindObject("OverallSavingsTxt") as FastReport.TextObject;
                    var TextObject35 = report.FindObject("BonusEvalTxt") as FastReport.TextObject;
                    var TextObject36 = report.FindObject("BeforeKaizenTxt") as FastReport.TextObject;
                    var TextObject37 = report.FindObject("AfterKaizenTxt") as FastReport.TextObject;
                    var TextObject38 = report.FindObject("ModelTxt") as FastReport.TextObject;

                    // Set values
                    if (TextObject1 != null) TextObject1.Text = KaizenHeading;
                    if (TextObject2 != null) TextObject2.Text = Kaizen_number;
                    if (TextObject3 != null) TextObject3.Text = Kaizen_Type;
                    if (TextObject4 != null) TextObject4.Text = Proposer_Department;
                    if (TextObject5 != null) TextObject5.Text = Proposer_area;
                    if (TextObject6 != null) TextObject6.Text = Proposer_line;
                    if (TextObject7 != null) TextObject7.Text = Dept_Code;
                    if (TextObject8 != null) TextObject8.Text = Type_ECRS;
                    if (TextObject9 != null) TextObject9.Text = CW_Barcode;
                    if (TextObject10 != null) TextObject10.Text = CW_Name;
                    if (TextObject11 != null) TextObject11.Text = Projected_Department;
                    if (TextObject12 != null) TextObject12.Text = Date;
                    if (TextObject13 != null) TextObject13.Text = Projected_Area;
                    if (TextObject14 != null) TextObject14.Text = Projected_Line;
                    if (TextObject15 != null) TextObject15.Text = Status;
                    if (TextObject16 != null) TextObject16.Text = Proposer_Barcode;
                    if (TextObject17 != null) TextObject17.Text = Proposer_Name;
                    if (TextObject18 != null) TextObject18.Text = Proposer_Designation;
                    if (TextObject19 != null) TextObject19.Text = Bonus;
                    if (TextObject20 != null) TextObject20.Text = CT_Before;
                    if (TextObject21 != null) TextObject21.Text = CT_After;
                    if (TextObject22 != null) TextObject22.Text = CT_Savings;
                    if (TextObject23 != null) TextObject23.Text = CT_Improved;
                    if (TextObject24 != null) TextObject24.Text = Output_Before;
                    if (TextObject25 != null) TextObject25.Text = Output_After;
                    if (TextObject26 != null) TextObject26.Text = Output_Saved;
                    if (TextObject27 != null) TextObject27.Text = Output_Improve;
                    if (TextObject28 != null) TextObject28.Text = Manpower_Before;
                    if (TextObject29 != null) TextObject29.Text = Manpower_After;
                    if (TextObject30 != null) TextObject30.Text = Manpower_Saved;
                    if (TextObject31 != null) TextObject31.Text = Manpower_Improved;
                    if (TextObject32 != null) TextObject32.Text = Monthly_Order_Quantity;
                    if (TextObject33 != null) TextObject33.Text = Overall_CT_Savings;
                    if (TextObject34 != null) TextObject34.Text = Overall_Savings;
                    if (TextObject35 != null) TextObject35.Text = Bonus_Evalution;
                    if (TextObject36 != null) TextObject36.Text = Before_Kaizen;
                    if (TextObject37 != null) TextObject37.Text = After_Kaizen;
                    if (TextObject38 != null) TextObject38.Text = model;
                    // Assign images
                    if (Picture1 != null && Before_Image != null)
                    {
                        using (MemoryStream ms = new MemoryStream(Before_Image))
                        {
                            Picture1.Image = Image.FromStream(ms);
                        }
                    }

                    if (Picture2 != null && After_Image != null)
                    {
                        using (MemoryStream ms = new MemoryStream(After_Image))
                        {
                            Picture2.Image = Image.FromStream(ms);
                        }
                    }

                    if (Picture3 != null && Proposer_Pic != null)
                    {
                        using (MemoryStream ms = new MemoryStream(Proposer_Pic))
                        {
                            Picture3.Image = Image.FromStream(ms);
                        }
                    }
                    // ✅ Ensure report scales properly when printing
                    report.PrintSettings.PrintMode = FastReport.PrintMode.Scale;
                    report.PrintSettings.ShowDialog = true;  // Set false if you don't want print dialog
                    report.PrintSettings.Copies = 1;


                }

                report.Prepare();
                report.ShowPrepared(true);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading FastReport: " + ex.Message);
            }

        }


        public static void LoadFastReport2(Control ctr, string fileName, Dictionary<string, string> dicParameter, DataTable dt, string tablename)
        {
            try
            {
                ctr.Controls.Clear();
                FastReport.Report report = new FastReport.Report();
                PreviewControl previewControl = new PreviewControl();

                previewControl.Dock = DockStyle.Fill;
                ctr.Controls.Add(previewControl);
                report.Preview = previewControl;

                report.Load(fileName);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    string Kaizen_number = row["KaizenNumber"].ToString();
                    string Kaizen_Heading = row["KaizenHeading"].ToString();
                    string Proposer_Department = row["ProposerDept"].ToString();
                    string Proposer_area = row["ProposerArea"].ToString();
                    string Proposer_line = row["ProposerLine"].ToString();
                    string Kaizen_Type = row["Kaizen"].ToString();
                    string Dept_Code = row["DepartmentCode"].ToString();
                    string Date = row["Date"].ToString();
                    string Projected_Area = row["ProjectArea"].ToString();
                    string Projected_Line = row["ProjectLine"].ToString();
                    string Status = row["Status"].ToString();

                    byte[] Before_Image = (byte[])row["BeforeImage"];
                    byte[] After_Image = (byte[])row["AfterImage"];
                    byte[] Proposer_Pic = (byte[])row["ProposerImage"];

                    string Proposer_Barcode = row["ProposerBarcode"].ToString();
                    string Proposer_Name = row["ProposerName"].ToString();
                    string Proposer_Designation = row["ProposerDesignation"].ToString();

                    string Before = row["BeforeValue"].ToString();
                    string After = row["AfterValue"].ToString();

                    string Output_Before = row["BeforeText"].ToString();
                    string Output_After = row["AfterText"].ToString();
                    string Output_Saved = row["Savings"].ToString();
                    string Output_Improve = row["ImprovementPercent"].ToString();

                    string model = row["Model"].ToString();
                    string Type_ECRS = row["TypeECRS"].ToString();
                    string Projected_Department = row["ProjectedDept"].ToString();
                    string CW_Barcode = row["CWBarcode"].ToString();
                    string CW_Name = row["CWName"].ToString();

                    // Find objects
                    var TextObject1 = report.FindObject("Text5") as FastReport.TextObject;
                    var TextObject2 = report.FindObject("Text13") as FastReport.TextObject;
                    var TextObject3 = report.FindObject("Text7") as FastReport.TextObject;
                    var TextObject4 = report.FindObject("Text9") as FastReport.TextObject;
                    var TextObject5 = report.FindObject("Text11") as FastReport.TextObject;
                    var TextObject6 = report.FindObject("Text15") as FastReport.TextObject;
                    var TextObject7 = report.FindObject("Text19") as FastReport.TextObject;
                    var TextObject8 = report.FindObject("Text21") as FastReport.TextObject;
                    var TextObject9 = report.FindObject("Text23") as FastReport.TextObject;
                    var TextObject10 = report.FindObject("Text25") as FastReport.TextObject;
                    var TextObject11 = report.FindObject("Text17") as FastReport.TextObject;
                    var TextObject12 = report.FindObject("Text27") as FastReport.TextObject;
                    var TextObject13 = report.FindObject("Text29") as FastReport.TextObject;
                    var TextObject14 = report.FindObject("Text31") as FastReport.TextObject;
                    var TextObject15 = report.FindObject("Text49") as FastReport.TextObject;
                    var Picture1 = report.FindObject("Picture1") as FastReport.PictureObject;
                    var Picture2 = report.FindObject("Picture2") as FastReport.PictureObject;
                    var Picture3 = report.FindObject("Picture3") as FastReport.PictureObject;
                    var TextObject16 = report.FindObject("Text48") as FastReport.TextObject;
                    var TextObject17 = report.FindObject("Text50") as FastReport.TextObject;
                    var TextObject18 = report.FindObject("Text57") as FastReport.TextObject;
                    var TextObject19 = report.FindObject("Text58") as FastReport.TextObject;
                    var TextObject20 = report.FindObject("Text40") as FastReport.TextObject;
                    var TextObject21 = report.FindObject("Text41") as FastReport.TextObject;
                    var TextObject22 = report.FindObject("Text59") as FastReport.TextObject;
                    var TextObject23 = report.FindObject("Text60") as FastReport.TextObject;
                    var TextObject24 = report.FindObject("Text33") as FastReport.TextObject;
                    var TextObject25 = report.FindObject("Text3") as FastReport.TextObject;

                    // Assign values
                    if (TextObject1 != null) TextObject1.Text = Kaizen_number;
                    if (TextObject2 != null) TextObject2.Text = Kaizen_Type;
                    if (TextObject3 != null) TextObject3.Text = Proposer_Department;
                    if (TextObject4 != null) TextObject4.Text = Proposer_area;
                    if (TextObject5 != null) TextObject5.Text = Proposer_line;
                    if (TextObject6 != null) TextObject6.Text = Dept_Code;
                    if (TextObject7 != null) TextObject7.Text = Type_ECRS;
                    if (TextObject8 != null) TextObject8.Text = CW_Barcode;
                    if (TextObject9 != null) TextObject9.Text = CW_Name;
                    if (TextObject10 != null) TextObject10.Text = Projected_Department;
                    if (TextObject11 != null) TextObject11.Text = Date;
                    if (TextObject12 != null) TextObject12.Text = Projected_Area;
                    if (TextObject13 != null) TextObject13.Text = Projected_Line;
                    if (TextObject14 != null) TextObject14.Text = Status;
                    if (TextObject15 != null) TextObject15.Text = Proposer_Barcode;
                    if (TextObject16 != null) TextObject16.Text = Proposer_Name;
                    if (TextObject17 != null) TextObject17.Text = Proposer_Designation;
                    if (TextObject18 != null) TextObject18.Text = Before;
                    if (TextObject19 != null) TextObject19.Text = After;
                    if (TextObject20 != null) TextObject20.Text = Output_Before;
                    if (TextObject21 != null) TextObject21.Text = Output_After;
                    if (TextObject22 != null) TextObject22.Text = Output_Saved;
                    if (TextObject23 != null) TextObject23.Text = Output_Improve;
                    if (TextObject24 != null) TextObject24.Text = model;
                    if (TextObject25 != null) TextObject25.Text = Kaizen_Heading;

                    if (Picture1 != null && Before_Image != null)
                    {
                        using (MemoryStream ms = new MemoryStream(Before_Image))
                        {
                            Picture1.Image = Image.FromStream(ms);
                        }
                    }

                    if (Picture2 != null && After_Image != null)
                    {
                        using (MemoryStream ms = new MemoryStream(After_Image))
                        {
                            Picture2.Image = Image.FromStream(ms);
                        }
                    }

                    if (Picture3 != null && Proposer_Pic != null)
                    {
                        using (MemoryStream ms = new MemoryStream(Proposer_Pic))
                        {
                            Picture3.Image = Image.FromStream(ms);
                        }
                    }
                }

                // ✅ Ensure report scales properly when printing
                report.PrintSettings.PrintMode = FastReport.PrintMode.Scale;
                report.PrintSettings.ShowDialog = true;  // Set false if you don't want print dialog
                report.PrintSettings.Copies = 1;

                report.Prepare();

                // Show preview
                report.ShowPrepared(true);

                // Optional: Direct print
                // report.Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading FastReport: " + ex.Message);
            }
        }

        public static void LoadFastReport3(Control ctr, string fileName, Dictionary<string, string> dicParameter, DataTable dt, string tablename)
        {
            try
            {
                ctr.Controls.Clear();
                FastReport.Report report = new FastReport.Report();
                PreviewControl previewControl = new PreviewControl();

                previewControl.Dock = DockStyle.Fill;
                ctr.Controls.Add(previewControl);
                report.Preview = previewControl;

                report.Load(fileName);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    string Kaizen_number = row["KaizenNumber"].ToString();
                    string Kaizen_Heading = row["KaizenHeading"].ToString();
                    string Proposer_Department = row["ProposerDept"].ToString();
                    string Proposer_area = row["ProposerArea"].ToString();
                    string Proposer_line = row["ProposerLine"].ToString();
                    string Kaizen_Type = row["Kaizen"].ToString();
                    string Dept_Code = row["DepartmentCode"].ToString();
                    string Date = row["Date"].ToString();
                    string Projected_Area = row["ProjectArea"].ToString();
                    string Projected_Line = row["ProjectLine"].ToString();
                    string Status = row["Status"].ToString();

                    byte[] Before_Image = (byte[])row["BeforeImage"];
                    byte[] After_Image = (byte[])row["AfterImage"];
                    byte[] Proposer_Pic = (byte[])row["ProposerImage"];

                    string Proposer_Barcode = row["ProposerBarcode"].ToString();
                    string Proposer_Name = row["ProposerName"].ToString();
                    string Proposer_Designation = row["ProposerDesignation"].ToString();

                    string Before = row["BeforeText"].ToString();
                    string After = row["AfterText"].ToString();

                    string RFT_Before= row["BeforeRft"].ToString();
                    string RFT__After = row["AfterRft"].ToString();
                    string RFT_Saved = row["SavingsRft"].ToString();
                    string RFT_Improve = row["ImprovedRft"].ToString();
                    string Before_inspected = row["Before_inspected"].ToString();
                    string After_inspected = row["After_inspected"].ToString();
                    string Before_Defects_QTY = row["Before_Defects_QTY"].ToString();
                    string After_Defectes_QTY = row["After_Defectes_QTY"].ToString();
                    string Before_RFT = row["Before_RFT(%)"].ToString();
                    string After_RFT= row["After_RFT(%)"].ToString();

                    string model = row["Model"].ToString();
                    string Type_ECRS = row["TypeECRS"].ToString();
                    string Projected_Department = row["ProjectedDept"].ToString();
                    string CW_Barcode = row["CWBarcode"].ToString();
                    string CW_Name = row["CWName"].ToString();

                    // Find objects
                    var TextObject1 = report.FindObject("Text5") as FastReport.TextObject;
                    var TextObject2 = report.FindObject("Text13") as FastReport.TextObject;
                    var TextObject3 = report.FindObject("Text7") as FastReport.TextObject;
                    var TextObject4 = report.FindObject("Text9") as FastReport.TextObject;
                    var TextObject5 = report.FindObject("Text11") as FastReport.TextObject;
                    var TextObject6 = report.FindObject("Text15") as FastReport.TextObject;
                    var TextObject7 = report.FindObject("Text19") as FastReport.TextObject;
                    var TextObject8 = report.FindObject("Text21") as FastReport.TextObject;
                    var TextObject9 = report.FindObject("Text23") as FastReport.TextObject;
                    var TextObject10 = report.FindObject("Text25") as FastReport.TextObject;
                    var TextObject11 = report.FindObject("Text17") as FastReport.TextObject;
                    var TextObject12 = report.FindObject("Text27") as FastReport.TextObject;
                    var TextObject13 = report.FindObject("Text29") as FastReport.TextObject;
                    var TextObject14 = report.FindObject("Text31") as FastReport.TextObject;
                    var TextObject15 = report.FindObject("Text49") as FastReport.TextObject;
                    var Picture1 = report.FindObject("Picture1") as FastReport.PictureObject;
                    var Picture2 = report.FindObject("Picture2") as FastReport.PictureObject;
                    var Picture3 = report.FindObject("Picture3") as FastReport.PictureObject;
                    var TextObject16 = report.FindObject("Text48") as FastReport.TextObject;
                    var TextObject17 = report.FindObject("Text50") as FastReport.TextObject;
                    var TextObject18 = report.FindObject("Text40") as FastReport.TextObject;
                    var TextObject19 = report.FindObject("Text41") as FastReport.TextObject;
                    var TextObject20 = report.FindObject("Text57") as FastReport.TextObject;
                    var TextObject21 = report.FindObject("Text58") as FastReport.TextObject;
                    var TextObject22 = report.FindObject("Text59") as FastReport.TextObject;
                    var TextObject23 = report.FindObject("Text60") as FastReport.TextObject;
                    var TextObject24 = report.FindObject("Text33") as FastReport.TextObject;
                    var TextObject25 = report.FindObject("Text3") as FastReport.TextObject;
                    var TextObject26 = report.FindObject("Text63") as FastReport.TextObject;
                    var TextObject27 = report.FindObject("Text65") as FastReport.TextObject;
                    var TextObject28 = report.FindObject("Text67") as FastReport.TextObject;
                    var TextObject29 = report.FindObject("Text69") as FastReport.TextObject;
                    var TextObject30 = report.FindObject("Text43") as FastReport.TextObject;
                    var TextObject31 = report.FindObject("Text61") as FastReport.TextObject;


                    // Assign values
                    if (TextObject1 != null) TextObject1.Text = Kaizen_number;
                    if (TextObject2 != null) TextObject2.Text = Kaizen_Type;
                    if (TextObject3 != null) TextObject3.Text = Proposer_Department;
                    if (TextObject4 != null) TextObject4.Text = Proposer_area;
                    if (TextObject5 != null) TextObject5.Text = Proposer_line;
                    if (TextObject6 != null) TextObject6.Text = Dept_Code;
                    if (TextObject7 != null) TextObject7.Text = Type_ECRS;
                    if (TextObject8 != null) TextObject8.Text = CW_Barcode;
                    if (TextObject9 != null) TextObject9.Text = CW_Name;
                    if (TextObject10 != null) TextObject10.Text = Projected_Department;
                    if (TextObject11 != null) TextObject11.Text = Date;
                    if (TextObject12 != null) TextObject12.Text = Projected_Area;
                    if (TextObject13 != null) TextObject13.Text = Projected_Line;
                    if (TextObject14 != null) TextObject14.Text = Status;
                    if (TextObject15 != null) TextObject15.Text = Proposer_Barcode;
                    if (TextObject16 != null) TextObject16.Text = Proposer_Name;
                    if (TextObject17 != null) TextObject17.Text = Proposer_Designation;
                    if (TextObject18 != null) TextObject18.Text = Before;
                    if (TextObject19 != null) TextObject19.Text = After;
                    if (TextObject20 != null) TextObject20.Text = RFT_Before;
                    if (TextObject21 != null) TextObject21.Text = RFT__After;
                    if (TextObject22 != null) TextObject22.Text = RFT_Saved;
                    if (TextObject23 != null) TextObject23.Text = RFT_Improve;
                    if (TextObject24 != null) TextObject24.Text = model;
                    if (TextObject25 != null) TextObject25.Text = Kaizen_Heading;
                    if (TextObject26 != null) TextObject26.Text = Before_inspected;
                    if (TextObject27 != null) TextObject27.Text = After_inspected;
                    if (TextObject28 != null) TextObject28.Text = Before_Defects_QTY;
                    if (TextObject29 != null) TextObject29.Text = After_Defectes_QTY;
                    if (TextObject30 != null) TextObject30.Text = Before_RFT;
                    if (TextObject31 != null) TextObject31.Text = After_RFT;
                    if (Picture1 != null && Before_Image != null)
                    {
                        using (MemoryStream ms = new MemoryStream(Before_Image))
                        {
                            Picture1.Image = Image.FromStream(ms);
                        }
                    }

                    if (Picture2 != null && After_Image != null)
                    {
                        using (MemoryStream ms = new MemoryStream(After_Image))
                        {
                            Picture2.Image = Image.FromStream(ms);
                        }
                    }

                    if (Picture3 != null && Proposer_Pic != null)
                    {
                        using (MemoryStream ms = new MemoryStream(Proposer_Pic))
                        {
                            Picture3.Image = Image.FromStream(ms);
                        }
                    }
                }

                // ✅ Ensure report scales properly when printing
                report.PrintSettings.PrintMode = FastReport.PrintMode.Scale;
                report.PrintSettings.ShowDialog = true;  // Set false if you don't want print dialog
                report.PrintSettings.Copies = 1;

                report.Prepare();

                // Show preview
                report.ShowPrepared(true);

                // Optional: Direct print
                // report.Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading FastReport: " + ex.Message);
            }
        }



        public static void LoadFastReportPallet(Control ctr, string fileName, Dictionary<string, string> dicParameter, DataTable dt, string tablename)
        {
            try
            {
                ctr.Controls.Clear();
                FastReport.Report report = new FastReport.Report();
                PreviewControl previewControl = new PreviewControl();

                previewControl.Dock = DockStyle.Fill;
                ctr.Controls.Add(previewControl);
                report.Preview = previewControl;

                report.Load(fileName);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    void SetText(string textName, string value)
                    {
                        var textObject = report.FindObject(textName) as FastReport.TextObject;
                        if (textObject != null) textObject.Text = value;
                    }

                    SetText("Text3", row["kaizen_num"].ToString());
                    SetText("Text30", row["total_sizes_before"].ToString());
                    SetText("Text31", row["total_sizes_after"].ToString());

                    SetText("Text32", row["top_pallet1_before"].ToString());
                    SetText("Text33", row["top_pallet1_after"].ToString());
                    SetText("Text34", row["top_pallet2_before"].ToString());
                    SetText("Text35", row["top_pallet2_after"].ToString());

                    SetText("Text36", row["inside_pallet1_before"].ToString());
                    SetText("Text37", row["inside_pallet1_after"].ToString());
                    SetText("Text38", row["inside_pallet2_before"].ToString());
                    SetText("Text39", row["inside_pallet2_after"].ToString());

                    SetText("Text40", row["bottom_pallet1_before"].ToString());
                    SetText("Text41", row["bottom_pallet1_after"].ToString());
                    SetText("Text42", row["bottom_pallet2_before"].ToString());
                    SetText("Text43", row["bottom_pallet2_after"].ToString());

                    SetText("Text44", row["fb_dimension_before"].ToString());
                    SetText("Text45", row["fb_dimension_after"].ToString());
                    SetText("Text46", row["top_pallet_dimension_before"].ToString());
                    SetText("Text47", row["top_pallet_dimension_after"].ToString());
                    SetText("Text48", row["inside_pallet_dimension_before"].ToString());
                    SetText("Text49", row["inside_pallet_dimension_after"].ToString());
                    SetText("Text50", row["bottom_pallet_dimension_before"].ToString());
                    SetText("Text51", row["bottom_pallet_dimension_after"].ToString());

                    SetText("Text52", row["per_hour_output_before"].ToString());
                    SetText("Text53", row["per_hour_output_after"].ToString());
                    SetText("Text54", row["order_qty_before"].ToString());
                    SetText("Text55", row["order_qty_after"].ToString());
                    SetText("Text56", row["working_hours_before"].ToString());
                    SetText("Text57", row["working_hours_after"].ToString());

                    SetText("Text58", row["each_line_output_before"].ToString());
                    SetText("Text59", row["each_line_output_after"].ToString());
                    SetText("Text60", row["required_machines_before"].ToString());
                    SetText("Text61", row["required_machines_after"].ToString());

                    SetText("Text62", row["top_pallets_before"].ToString());
                    SetText("Text63", row["top_pallets_after"].ToString());
                    SetText("Text64", row["inside_pallets_before"].ToString());
                    SetText("Text65", row["inside_pallets_after"].ToString());
                    SetText("Text66", row["bottom_pallets_before"].ToString());
                    SetText("Text67", row["bottom_pallets_after"].ToString());
                    SetText("Text68", row["no_of_fiber_board_before"].ToString());
                    SetText("Text69", row["no_of_fiber_board_after"].ToString());

                    SetText("Text70", row["fiber_board_cost_before"].ToString());
                    SetText("Text71", row["fiber_board_cost_after"].ToString());
                    SetText("Text72", row["total_cost_before"].ToString());
                    SetText("Text73", row["total_cost_after"].ToString());
                    SetText("Text74", row["overall_savings"].ToString());
                }

                report.PrintSettings.PrintMode = FastReport.PrintMode.Scale;
                report.PrintSettings.ShowDialog = true;
                report.PrintSettings.Copies = 1;

                report.Prepare();
                report.ShowPrepared(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading FastReport: " + ex.Message);
            }
        }


        public static void LoadFastReportPower(Control ctr, string fileName, Dictionary<string, string> dicParameter, DataTable dt, string tablename)
        {
            try
            {
                ctr.Controls.Clear();
                FastReport.Report report = new FastReport.Report();
                PreviewControl previewControl = new PreviewControl();
                previewControl.Dock = DockStyle.Fill;
                ctr.Controls.Add(previewControl);
                report.Preview = previewControl;
                report.Load(fileName);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    void SetText(string textName, string value)
                    {
                        var textObject = report.FindObject(textName) as FastReport.TextObject;
                        if (textObject != null) textObject.Text = value;
                    }
                    SetText("Text3", row["KAIZEN_NUMBER"].ToString());
                    SetText("Text13", row["POWER_CONSUMPTION_B"].ToString());
                    SetText("Text14", row["POWER_CONSUMPTION_A"].ToString());
                    SetText("Text15", row["CT_B"].ToString());
                    SetText("Text16", row["CT_A"].ToString());
                    SetText("Text17", row["ORDER_QTY_B"].ToString());
                    SetText("Text18", row["ORDER_QTY_A"].ToString());
                    SetText("Text19", row["WORK_HRS_B"].ToString());
                    SetText("Text20", row["WORK_HRS_A"].ToString());
                    SetText("Text21", row["W_TOTAL_OUTPUT_PER_HOUR_B"].ToString());
                    SetText("Text22", row["W_TOTAL_OUTPUT_PER_HOUR_A"].ToString());
                    SetText("Text23", row["REQUIRED_MACHINES_B"].ToString());
                    SetText("Text24", row["REQUIRED_MACHINES_A"].ToString());
                    SetText("Text25", row["TOTAL_POWER_B"].ToString());
                    SetText("Text26", row["TOTAL_POWER_A"].ToString());
                    SetText("Text27", row["ONE_KW_PRICE_B"].ToString());
                    SetText("Text28", row["ONE_KW_PRICE_A"].ToString());
                    SetText("Text29", row["TOTAL_COST_B"].ToString());
                    SetText("Text30", row["TOTAL_COST_A"].ToString());
                    SetText("Text31", row["OVERALL_SAVINGS"].ToString());
                }
                report.PrintSettings.PrintMode = FastReport.PrintMode.Scale;
                report.PrintSettings.ShowDialog = true;
                report.PrintSettings.Copies = 1;
                report.Prepare();
                report.ShowPrepared(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading FastReport: " + ex.Message);
            }
        }

        public static void LoadFastReportMaterial(Control ctr, string fileName, Dictionary<string, string> dicParameter, DataTable dt, string tablename)
        {
            try
            {
                ctr.Controls.Clear();
                FastReport.Report report = new FastReport.Report();
                PreviewControl previewControl = new PreviewControl();
                previewControl.Dock = DockStyle.Fill;
                ctr.Controls.Add(previewControl);
                report.Preview = previewControl;
                report.Load(fileName);
                if (dt != null && dt.Rows.Count > 0)
                {
                  DataRow row = dt.Rows[0];
                void SetText(string textName, string value)
                    {
                        var textObject = report.FindObject(textName) as FastReport.TextObject;
                        if (textObject != null) textObject.Text = value;
                    }
                    SetText("Text3", row["KAIZEN_NUMBER"].ToString());
                    SetText("Text13", row["ONE_YARD_PAIRS_B"].ToString());
                    SetText("Text14", row["ONE_YARD_PAIRS_A"].ToString());
                    SetText("Text15", row["ORDER_QTY"].ToString());
                    SetText("Text16", row["REQUIRED_YARDS_B"].ToString());
                    SetText("Text17", row["REQUIRED_YARDS_A"].ToString());
                    SetText("Text18", row["ONE_YARD_COST"].ToString());
                    SetText("Text19", row["REQUIRED_YARD_COST_B"].ToString());
                    SetText("Text20", row["REQUIRED_YARD_COST_A"].ToString());
                    SetText("Text21", row["OVERALL_YARD_SAVING_COST"].ToString());
                }
                report.PrintSettings.PrintMode = FastReport.PrintMode.Scale;
                report.PrintSettings.ShowDialog = true;
                report.PrintSettings.Copies = 1;
                report.Prepare();
                report.ShowPrepared(true);
            }
            catch (Exception ex)
            {
             Console.WriteLine("Error loading FastReport: " + ex.Message);
            }
        }



        public static void LoadFastReportGlue(Control ctr, string fileName, Dictionary<string, string> dicParameter, DataTable dt, string tablename)
        {
            try
            {
                ctr.Controls.Clear();
                FastReport.Report report = new FastReport.Report();
                PreviewControl previewControl = new PreviewControl();
                previewControl.Dock = DockStyle.Fill;
                ctr.Controls.Add(previewControl);
                report.Preview = previewControl;
                report.Load(fileName);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    void SetText(string textName, string value)
                    {
                        var textObject = report.FindObject(textName) as FastReport.TextObject;
                        if (textObject != null) textObject.Text = value;
                    }
                    SetText("Text3", row["KAIZEN_NUMBER"].ToString());
                    SetText("Text13", row["GLUE_ITEM_B"].ToString());
                    SetText("Text14", row["GLUE_ITEM_A"].ToString());
                    SetText("Text15", row["PAIR_CONSUMPTION_B"].ToString());
                    SetText("Text16", row["PAIR_CONSUMPTION_A"].ToString());
                    SetText("Text17", row["ORDER_QTY_B"].ToString());
                    SetText("Text18", row["ORDER_QTY_A"].ToString());
                    SetText("Text19", row["TOTAL_CONSUMPTION_B"].ToString());
                    SetText("Text20", row["TOTAL_CONSUMPTION_A"].ToString());
                    SetText("Text22", row["CONVERTED_KG_B"].ToString());
                    SetText("Text23", row["CONVERTED_KG_A"].ToString());
                    SetText("Text24", row["PER_KG_COST_B"].ToString());
                    SetText("Text25", row["PER_KG_COST_A"].ToString());
                    SetText("Text26", row["TOTAL_COST_B"].ToString());
                    SetText("Text27", row["TOTAL_COST_A"].ToString());
                    SetText("Text28", row["OVERALL_SAVINGS"].ToString());
                }
                report.PrintSettings.PrintMode = FastReport.PrintMode.Scale;
                report.PrintSettings.ShowDialog = true;
                report.PrintSettings.Copies = 1;
                report.Prepare();
                report.ShowPrepared(true);
            }
            catch (Exception ex)
            {
             Console.WriteLine("Error loading FastReport: " + ex.Message);
            }
        }



        public static void LoadFastReportChemical(Control ctr, string fileName, Dictionary<string, string> dicParameter, DataTable dt, string tablename)
        {
            try
            {
                ctr.Controls.Clear();
                FastReport.Report report = new FastReport.Report();
                PreviewControl previewControl = new PreviewControl();
                previewControl.Dock = DockStyle.Fill;
                ctr.Controls.Add(previewControl);
                report.Preview = previewControl;
                report.Load(fileName);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    void SetText(string textName, string value)
                    {
                        var textObject = report.FindObject(textName) as FastReport.TextObject;
                        if (textObject != null) textObject.Text = value;
                    }
                    SetText("Text3", row["kaizen_num"].ToString());
                    SetText("Text13", row["Item_Before"].ToString());
                    SetText("Text22", row["Item_After"].ToString());
                    SetText("Text14", row["PairConsumption_Before"].ToString());
                    SetText("Text23", row["PairConsumption_After"].ToString());
                    SetText("Text15", row["OrderQty_Before"].ToString());
                    SetText("Text24", row["OrderQty_After"].ToString());
                    SetText("Text16", row["BasedOn_Order_Before"].ToString());
                    SetText("Text25", row["BasedOn_Order_After"].ToString());
                    SetText("Text17", row["Converted_Before"].ToString());
                    SetText("Text26", row["Converted_After"].ToString());
                    SetText("Text18", row["KG_Cost_Before"].ToString());
                    SetText("Text27", row["KG_Cost_After"].ToString());
                    SetText("Text19", row["Total_Cost_Before"].ToString());
                    SetText("Text28", row["Total_Cost_After"].ToString());
                    SetText("Text20", row["overall_savings"].ToString());
                }
                report.PrintSettings.PrintMode = FastReport.PrintMode.Scale;
                report.PrintSettings.ShowDialog = true;
                report.PrintSettings.Copies = 1;
                report.Prepare();
                report.ShowPrepared(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading FastReport: " + ex.Message);
            }
        }
        public static void LoadFastReportThread(Control ctr, string fileName, Dictionary<string, string> dicParameter, DataTable dt, string tablename)
        {
            try
            {
                ctr.Controls.Clear();
                FastReport.Report report = new FastReport.Report();
                PreviewControl previewControl = new PreviewControl();
                previewControl.Dock = DockStyle.Fill;
                ctr.Controls.Add(previewControl);
                report.Preview = previewControl;
                report.Load(fileName);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    void SetText(string textName, string value)
                    {
                        var textObject = report.FindObject(textName) as FastReport.TextObject;
                        if (textObject != null) textObject.Text = value;
                    }
                    SetText("Text3", row["kaizen_num"].ToString());
                    SetText("Text13", row["Item_Before"].ToString());
                    SetText("Text22", row["Item_After"].ToString());
                    SetText("Text14", row["TotalRoll_Before"].ToString());
                    SetText("Text23", row["TotalRoll_After"].ToString());
                    SetText("Text15", row["RollCost_Before"].ToString());
                    SetText("Text24", row["RollCost_After"].ToString());
                    SetText("Text16", row["PairConsumption_Before"].ToString());
                    SetText("Text25", row["PairConsumption_After"].ToString());
                    SetText("Text17", row["OrderQty_Before"].ToString());
                    SetText("Text26", row["OrderQty_After"].ToString());
                    SetText("Text18", row["BasedOn_Order_Before"].ToString());
                    SetText("Text27", row["BasedOn_Order_After"].ToString());
                    SetText("Text19", row["ReqThreads_Before"].ToString());
                    SetText("Text28", row["ReqThreads_After"].ToString());
                    SetText("Text20", row["Total_Cost_Before"].ToString());
                    SetText("Text31", row["Total_Cost_After"].ToString());
                    SetText("Text30", row["overall_savings"].ToString());



                }

                report.PrintSettings.PrintMode = FastReport.PrintMode.Scale;
                report.PrintSettings.ShowDialog = true;
                report.PrintSettings.Copies = 1;

                report.Prepare();
                report.ShowPrepared(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading FastReport: " + ex.Message);
            }
        }


        public static void LoadFastReportTape(Control ctr, string fileName, Dictionary<string, string> dicParameter, DataTable dt, string tablename)


        {
            try
            {
                ctr.Controls.Clear();
                FastReport.Report report = new FastReport.Report();
                PreviewControl previewControl = new PreviewControl();

                previewControl.Dock = DockStyle.Fill;
                ctr.Controls.Add(previewControl);
                report.Preview = previewControl;

                report.Load(fileName);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    void SetText(string textName, string value)
                    {
                        var textObject = report.FindObject(textName) as FastReport.TextObject;
                        if (textObject != null) textObject.Text = value;
                    }

                    SetText("Text3", row["kaizen_num"].ToString());
                    SetText("Text13", row["Tape_PairConsumption_Before"].ToString());
                    SetText("Text22", row["Tape_PairConsumption_After"].ToString());

                    SetText("Text14", row["Converted_Before"].ToString());
                    SetText("Text23", row["Converted_After"].ToString());
                    SetText("Text15", row["TapeRoll_Before"].ToString());
                    SetText("Text24", row["TapeRoll_After"].ToString());

                    SetText("Text16", row["RollCost_Before"].ToString());
                    SetText("Text25", row["RollCost_After"].ToString());
                    SetText("Text17", row["1mTape_Cost_Before"].ToString());
                    SetText("Text26", row["1mTape_Cost_After"].ToString());

                    SetText("Text18", row["OrderQty_Before"].ToString());
                    SetText("Text27", row["OrderQty_After"].ToString());
                    SetText("Text19", row["Total_Cost_Before"].ToString());
                    SetText("Text28", row["Total_Cost_After"].ToString());
                    SetText("Text20", row["overall_savings"].ToString());





                }

                report.PrintSettings.PrintMode = FastReport.PrintMode.Scale;
                report.PrintSettings.ShowDialog = true;
                report.PrintSettings.Copies = 1;

                report.Prepare();
                report.ShowPrepared(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading FastReport: " + ex.Message);
            }
        }



        public static void LoadFastReportSingleNeedle(Control ctr, string fileName, Dictionary<string, string> dicParameter, DataTable dt, string tablename)
        {
            try
            {
                ctr.Controls.Clear();
                FastReport.Report report = new FastReport.Report();
                PreviewControl previewControl = new PreviewControl();
                previewControl.Dock = DockStyle.Fill;
                ctr.Controls.Add(previewControl);
                report.Preview = previewControl;
                report.Load(fileName);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    void SetText(string textName, string value)
                    {
                        var textObject = report.FindObject(textName) as FastReport.TextObject;
                        if (textObject != null) textObject.Text = value;
                    }

                    SetText("Text3", row["KaizenNum"].ToString());
                    SetText("Text44", row["powerConsumption"].ToString());
                    SetText("Text45", row["cycleTime"].ToString());
                    SetText("Text46", row["orderQty"].ToString());
                    SetText("Text47", row["workHours"].ToString());
                    SetText("Text48", row["totalWorkHours"].ToString());
                    SetText("Text49", row["reqMachines"].ToString());
                    SetText("Text50", row["totalPower"].ToString());
                    SetText("Text51", row["kwPrice"].ToString());
                    SetText("Text52", row["totalCost"].ToString());

                    SetText("Text54", row["csPowerConsumption"].ToString());
                    SetText("Text55", row["csCycleTime"].ToString());
                    SetText("Text56", row["csOrderQty"].ToString());
                    SetText("Text57", row["csWorkHours"].ToString());
                    SetText("Text58", row["csTotalWorkHours"].ToString());
                    SetText("Text59", row["csReqMachines"].ToString());
                    SetText("Text60", row["csTotalPower"].ToString());
                    SetText("Text61", row["csKwPrice"].ToString());
                    SetText("Text62", row["csTotalCost"].ToString());

                    SetText("Text63", row["totalSizes"].ToString());
                    SetText("Text64", row["fbDimensions"].ToString());
                    SetText("Text65", row["palletDimensions"].ToString());
                    SetText("Text66", row["Top_Pallet_Dimen"].ToString());
                    SetText("Text67", row["Top_Pallet"].ToString());
                    SetText("Text68", row["Bottom_Pallet"].ToString());
                    SetText("Text69", row["hourOutput"].ToString());
                    SetText("Text70", row["orderQtyPallet"].ToString());
                    SetText("Text71", row["workingHours"].ToString());
                    SetText("Text72", row["eachLineOutput"].ToString());
                    SetText("Text73", row["requiredMachines"].ToString());
                    SetText("Text74", row["noOfPallets"].ToString());
                    SetText("Text75", row["noOfFiberBoards"].ToString());
                    SetText("Text76", row["fbCost"].ToString());
                    SetText("Text77", row["newPalletCost"].ToString());
                    SetText("Text78", row["total"].ToString());



                }

                report.PrintSettings.PrintMode = FastReport.PrintMode.Scale;
                report.PrintSettings.ShowDialog = true;
                report.PrintSettings.Copies = 1;

                report.Prepare();
                report.ShowPrepared(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading FastReport: " + ex.Message);
            }
        }



    }
}

