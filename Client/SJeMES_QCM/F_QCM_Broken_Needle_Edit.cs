using MaterialSkin;
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

namespace SJeMES_QCM
{
    public partial class F_QCM_Broken_Needle_Edit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_Broken_Needle_Edit()
        {
            InitializeComponent();
            List<info> ii = new List<info>();
            info info1 = new info() { code = "一线", name = "一线" };
            info info2 = new info() { code = "二线", name = "二线" };
            info info3 = new info() { code = "三线", name = "三线" };
            ii.Add(info1);
            ii.Add(info2);
            ii.Add(info3);
            comboBox1.DataSource = ii;
            comboBox1.ValueMember = "code";
            comboBox1.DisplayMember = "name";
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        public class info
        {
            public string code { get; set; }
            public string name { get; set; }
        }

        string plant = string.Empty;
        string Production_line = string.Empty;
        public string pa
        {
            get { return plant; }
            set { plant = value; }
        }
        public string pl
        {
            get { return Production_line; }
            set { Production_line=value; }
        }

        public string filePath = string.Empty;
        public string SafeFileName = string.Empty;
        private void btnimg_Click(object sender, EventArgs e)
        {
            //创建文件弹出选择窗口（包括文件名）对象
            OpenFileDialog ofd = new OpenFileDialog();
            //判断选择的路径
            string path = string.Empty;
            ofd.Title = "请选择文件夹";
            ofd.Filter = "图像文件(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                filePath = ofd.FileName;
                MessageBox.Show("上传文件成功！");
                var webC = new System.Net.WebClient();
                string url = Program.Client.PicUrl + Convert.ToString("/File/断针照片.png");
                Image image = new Bitmap(webC.OpenRead(url));
                pictureBox1.BackgroundImage = image;
                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("厂区和产线不能为空!");
                textBox1.Text = "";
            }
            else if (textBox1.Text!="香洲"&& textBox1.Text != "东红"&& textBox1.Text != "宏国")
            {
                MessageBox.Show("产线不存在!");
                textBox1.Text = "";
            }
            else
            {
                MessageBox.Show("保存成功!");
                plant = textBox1.Text;
                Production_line = comboBox1.SelectedValue.ToString();
                this.Close();
            }
        }

        private void F_QCM_Broken_Needle_Edit_Load(object sender, EventArgs e)
        {

        }
    }
}
