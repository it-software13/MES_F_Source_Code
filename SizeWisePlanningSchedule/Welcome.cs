using PlanningSchedule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SizeWisePlanningSchedule
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
            DesignButton(button1);
        }

        private void DesignButton(Button btn)
        {
            // 🌟 Base style
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btn.Size = new Size(180, 50);
            btn.Text = "Open Schedule";
            btn.Cursor = Cursors.Hand;
            btn.BackColor = Color.Transparent;

            // 🎨 Rounded corners
            btn.Region = new Region(new Rectangle(0, 0, btn.Width, btn.Height));

            // 🌈 Gradient background
            btn.Paint += (s, e) =>
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = btn.ClientRectangle;
                using (LinearGradientBrush brush = new LinearGradientBrush(rect,
                    Color.FromArgb(52, 143, 80),   // Start color (greenish)
                    Color.FromArgb(86, 180, 211),  // End color (blue)
                    LinearGradientMode.Horizontal))
                {
                    g.FillRectangle(brush, rect);
                }

                // ✨ Rounded border
                using (Pen borderPen = new Pen(Color.White, 2))
                {
                    g.DrawRectangle(borderPen, 1, 1, btn.Width - 3, btn.Height - 3);
                }

                // 🖋️ Center text
                TextRenderer.DrawText(g, btn.Text, btn.Font, rect, Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };

            // 💫 Hover animation
            btn.MouseEnter += (s, e) =>
            {
                btn.Invalidate();
                btn.ForeColor = Color.Yellow;
                btn.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            };

            btn.MouseLeave += (s, e) =>
            {
                btn.Invalidate();
                btn.ForeColor = Color.White;
                btn.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form = new SizeWisePlanningSchdule(); 
            form.ShowDialog(); 
        } 

    }
}
