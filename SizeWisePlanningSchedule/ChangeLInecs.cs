using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SizeWisePlanningSchedule
{
    public partial class ChangeLInecs : Form
    {
        public string SelectedCode { get; private set; }
        public string SelectedName { get; private set; }

        public ChangeLInecs(List<SizeWiseUpdation.ComboboxEntry> Plants) 
        {
            InitializeComponent();
            StyleComboBox(comboBox2); 
            comboBox2.DataSource = Plants;
            comboBox2.DisplayMember = "Name";  
            comboBox2.ValueMember = "Code"; 

            // Add OK button (optional if you don’t already have one)
            System.Windows.Forms.Button okButton = new System.Windows.Forms.Button();
            okButton.Text = "OK";
            okButton.Left = 150;
            okButton.Top = 150;
            okButton.Click += OkButton_Click;
            this.Controls.Add(okButton);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex >= 0 && comboBox2.SelectedValue != null)
            {
                SelectedCode = comboBox2.SelectedValue.ToString();
                SelectedName = comboBox2.Text;
            } 
            this.DialogResult = DialogResult.OK; // Close the form and return OK
            this.Close();
        }
        private void StyleComboBox(System.Windows.Forms.ComboBox comboBox)
        {
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList; // Prevent typing, select only
            comboBox.FlatStyle = FlatStyle.Flat;                 // Flat modern look
            comboBox.BackColor = Color.White;                    // Background color
            comboBox.ForeColor = Color.Black;                    // Text color
            comboBox.Font = new Font("Segoe UI", 10, FontStyle.Regular); // Font style
            comboBox.Margin = new Padding(2);
            comboBox.Cursor = Cursors.Hand;                      // Change cursor to hand
            comboBox.Width = 200;                                // Optional width

            // Optional: Add a border (simulate since WinForms ComboBox doesn’t support border color)
            comboBox.Region = new Region(comboBox.ClientRectangle);

            comboBox.DrawItem += (s, e) =>
            {
                e.DrawBackground();
                if (e.Index >= 0)
                {
                    string text = comboBox.Items[e.Index].ToString();
                    Brush brush = new SolidBrush(e.ForeColor);
                    e.Graphics.DrawString(text, e.Font, brush, e.Bounds);
                }
                e.DrawFocusRectangle();
            };
        }

    }
}
