using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SJeMES_Framework.WebAPI;
using Newtonsoft.Json; 
using SJeMES_Framework.Common;
using SJeMES_Control_Library;
using Oracle.ManagedDataAccess.Client;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System.IO;
using OfficeOpenXml.Style;
using NewExportExcels;
using Microsoft.Office.Interop.Excel;
using LicenseContext = OfficeOpenXml.LicenseContext;
using NPOI.SS.UserModel;
using System.Net.Sockets;
using System.Net;
using PlanningSchedule;
using System.Linq;

namespace SizeWisePlanningSchedule 
{
    public partial class SizeDevideForm : Form
    {
        private List<string> _soList;  
        private List<string> _lineList;
        private DataTable _sizeData;

        private Dictionary<string, List<string>> _lineSizeMap = new Dictionary<string, List<string>>() ;  
        
        private CheckedListBox checkedListBoxSizes ; 
        private Button btnOk ; 
        
        private HashSet<string> _allocatedSizes = new HashSet<string>();  
        private bool status = false ; 

        public SizeDevideForm(List<string> soList, List<string> lineList , bool stitstatus ) 
        {
            InitializeComponent();
            _soList = soList ;    
            _lineList = lineList.Distinct().ToList() ;    
            listBox1.DrawMode = DrawMode.OwnerDrawFixed ; 
            listBox1.ItemHeight = 30 ;   
            listBox1.DrawItem += listBox1_DrawItem_1; 
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged_1;  
            status = stitstatus ; 
        }

        private void SizeDevideForm_Load(object sender, EventArgs e)
        { 
            listBox1.Items.Clear(); 
            foreach (var item in _lineList)
            {
                listBox1.Items.Add(item);
            }
            comboBox1.Visible = false;
          
            checkedListBoxSizes = new CheckedListBox
            {
                Visible = false,
                CheckOnClick = true,
                Height = 150,
                Width = 150,
                Location = new Point(listBox1.Right + 20, listBox1.Top)
            };
            this.Controls.Add(checkedListBoxSizes);
             
            btnOk = new Button
            {
                Text = "OK",
                Visible = false,
                Width = 80,
                Location = new Point(checkedListBoxSizes.Right + 10, checkedListBoxSizes.Top)
            };
            btnOk.Click += Btn_OK; 
            this.Controls.Add(btnOk);

            GetSizes();
        } 


        private void GetSizes() 
        { 
            try
            { 
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("SOList", _soList);   
                 
                string retdata = WebAPIHelper.Post(
                    PlanningSchedule.Program.client.APIURL,
                    "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.SizePlanningController",
                    "GetSizeValuesBySOList",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(data)
                );
                 
                ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess && ret.RetData != null && ret.RetData.ToString() != "")
                {
                    string json = ret.RetData.ToString();
                    DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                    _sizeData = dtJson; 
                } 
                else
                {
                    MessageBox.Show("No size data found or request failed.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while fetching sizes: " + ex.Message);
            }
        }

      

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;

            string selectedLine = ExtractLineName(listBox1.SelectedItem.ToString());
            // string selectedLine = listBox1.SelectedItem.ToString();

            if (_sizeData == null || _sizeData.Rows.Count == 0)
            {
                MessageBox.Show("No sizes available.");
                return;
            }

            // Show CheckedListBox near the selected line
            checkedListBoxSizes.Items.Clear();
            List<string> currentSizes = _lineSizeMap.ContainsKey(selectedLine)
                ? new List<string>(_lineSizeMap[selectedLine])
                : new List<string>();

            foreach (DataRow row in _sizeData.Rows)
            {
                string size = row["SIZE_NO"].ToString();
                bool isAllocated = _allocatedSizes.Contains(size);
                bool isMine = currentSizes.Contains(size);

                int index = checkedListBoxSizes.Items.Add(size, isMine);

                if (isAllocated && !isMine)
                {
                    checkedListBoxSizes.SetItemCheckState(index, CheckState.Indeterminate); 
                } 
                 
            }
             
            int y = listBox1.GetItemRectangle(listBox1.SelectedIndex).Y + listBox1.Top;
            checkedListBoxSizes.Location = new Point(listBox1.Right + 20, y);
            btnOk.Location = new Point(checkedListBoxSizes.Right + 10, y);

            checkedListBoxSizes.Visible = true;
            btnOk.Visible = true; 
        }


        private void listBox1_DrawItem_1(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            string text = listBox1.Items[e.Index].ToString();

            Color backColor = (e.Index % 2 == 0) ? Color.LightYellow : Color.LightBlue;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                backColor = Color.Gold;

            e.Graphics.FillRectangle(new SolidBrush(backColor), e.Bounds);
            Font font = new Font(e.Font, FontStyle.Bold);
            Rectangle textRect = new Rectangle(e.Bounds.X + 10, e.Bounds.Y + 5, e.Bounds.Width, e.Bounds.Height);
            TextRenderer.DrawText(e.Graphics, text, font, textRect, Color.Black, TextFormatFlags.Left);
            e.DrawFocusRectangle();
        }

   
        private void button1_Click(object sender, EventArgs e) 
        {

            SizeWiseForm form = new SizeWiseForm(_allocatedSizes , _lineSizeMap , _soList , status ); 
            form.ShowDialog();
            this.Close(); 
        }  

        private void Btn_OK(object sender, EventArgs e)  
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a line first.");
                return;
            } 

            string selectedLine = ExtractLineName(listBox1.SelectedItem.ToString()); 
            List<string> oldSizes = _lineSizeMap.ContainsKey(selectedLine)
               ? _lineSizeMap[selectedLine]
               : new List<string>();
            List<string> newSizes = new List<string>();
            List<string> selectedSizes = new List<string>();

            foreach (var item in checkedListBoxSizes.CheckedItems)
            {
                string size = item.ToString();
                if (!_allocatedSizes.Contains(size) || oldSizes.Contains(size))
                    newSizes.Add(size);
            }
            foreach (var old in oldSizes)
                _allocatedSizes.Remove(old);
            foreach (var newsz in newSizes)
                _allocatedSizes.Add(newsz);

            _lineSizeMap[selectedLine] = newSizes;

            UpdateListBoxItem(selectedLine, newSizes);

            checkedListBoxSizes.Visible = false;
            btnOk.Visible = false;
           
        } 

        
        private void UpdateListBoxItem(string line, List<string> sizes)
        {
            string newText = $"{line}  →  [{string.Join(", ", sizes)}]";
            int index = -1;
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string current = ExtractLineName(listBox1.Items[i].ToString());
                if (current == line)
                {
                    index = i;
                    break;
                }
            }
            if (index >= 0)
                listBox1.Items[index] = newText;
        }

        private string ExtractLineName(string text)
        { 
            if (text.Contains("→"))
                return text.Split('→')[0].Trim();
            return text.Trim();
        } 

    }
}
