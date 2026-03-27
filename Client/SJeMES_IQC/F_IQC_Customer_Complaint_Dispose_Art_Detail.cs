using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_IQC
{
    public partial class F_IQC_Customer_Complaint_Dispose_Art_Detail : MaterialForm
    {
        public F_IQC_Customer_Complaint_Dispose_Art_Detail(Dictionary<string, string> showValues)
        {
            InitializeComponent();

            ShowData(showValues);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public void ShowData(Dictionary<string, string> showValues)
        {
            tb_shoe_name.Text = showValues["shoe_name"];
            tb_art.Text = showValues["art"];
            tb_delivery_time.Text = showValues["delivery_time"];
            tb_production_plant.Text = showValues["production_plant"];
            tb_line_type.Text = showValues["line_type"];
            rtb_complaint_points.Text = showValues["complaint_points"];

            if (!string.IsNullOrEmpty(showValues["imglist"]))
            {
                var currRowFileDt = F_IQC_Customer_Complaint_Dispose.Getimage_guid(showValues["imglist"]);

                int index = 1;
                int width = 220;
                int height = 140;
                int x = 0;
                int y = 0;
                foreach (DataRow item in currRowFileDt.Rows)
                {
                    string net_file_url = item["net_file_url"].ToString();
                    System.IO.Stream stream;
                    try
                    {
                        stream = System.Net.WebRequest.Create(net_file_url).GetResponse().GetResponseStream();
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                    Panel pl = new Panel();
                    pl.Name= $@"pl_img_{index}";
                    pl.Width = width;
                    pl.Height = height;
                    PictureBox pb = new PictureBox();
                    pb.Name = $@"pb_{index}";
                    pb.Image = Image.FromStream(stream);
                    pb.SizeMode = PictureBoxSizeMode.Zoom;
                    pb.Parent = pl;
                    pb.Dock = DockStyle.Fill;
                    pl.Controls.Add(pb);
                    pl_images.Controls.Add(pl);
                    pl.Location = new Point(pl.Location.X + x, pl.Location.Y + y);
                    x += (width + 1);
                    if (index % 3 == 0)
                    {
                        x = 0;
                        y += (height + 5);
                    }
                    index++;
                }

            }

        }

    }
}
