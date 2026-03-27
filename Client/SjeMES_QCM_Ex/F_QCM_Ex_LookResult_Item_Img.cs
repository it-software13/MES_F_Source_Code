using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SjeMES_QCM_Ex
{
    public partial class F_QCM_Ex_LookResult_Item_Img : MaterialForm
    {
        string d_id;
        Dictionary<string, string> imgPathDic = new Dictionary<string, string>();
        public F_QCM_Ex_LookResult_Item_Img(string _d_id)
        {
            InitializeComponent();
            d_id = _d_id;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void F_QCM_Ex_LookResult_Item_Img_Load(object sender, EventArgs e)
        {
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("d_id", d_id);
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "GetTaskItemImg",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

            if (!ret.IsSuccess)
            {
                MessageBox.Show(ret.ErrMsg);
            }
            else
            {
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, DataTable>>(ret.RetData.ToString());

                int y_location = 16;
                int x_location = 30;

                int label_margin_bottom = 20;

                int pictureBox_width = 352;
                int pictureBox_height = 226;
                int pictureBox_margin_right = 20;
                int pictureBox_margin_bottom = 10;
                int rowCount = 2;
                foreach (var item in result)
                {
                    Label label = new Label();
                    label.Name = item.Key;
                    label.Text = $@"样品单{item.Key}";
                    label.AutoSize = true;
                    pl_img.Controls.Add(label);
                    label.Location = new Point(x_location, y_location);
                    label.SendToBack();

                    y_location += label_margin_bottom;
                    DataTable dt = item.Value;
                    int addPicture = 0;
                    int index = 0;
                    foreach (DataRow dt_row in dt.Rows)
                    {
                        index++;
                        try
                        {
                            PictureBox pictureBox = new PictureBox();
                            pictureBox.Name = dt_row["IMG_GUID"].ToString();
                            pictureBox.Size = new Size(pictureBox_width, pictureBox_height);
                            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                            pictureBox.Click += pictureBox_Click;
                            string url = Program.Client.PicUrl + dt_row["FILE_URL"].ToString();
                            imgPathDic.Add(dt_row["IMG_GUID"].ToString(), dt_row["FILE_URL"].ToString());
                            var webC = new System.Net.WebClient();
                            Image image = new Bitmap(webC.OpenRead(url));
                            pictureBox.Image = image;
                            pl_img.Controls.Add(pictureBox);
                            addPicture++;
                            if ((addPicture % rowCount) == 0)
                            {//图片换行
                                pictureBox.Location = new Point(x_location + pictureBox_width + pictureBox_margin_right, y_location);
                                addPicture = 0;
                                if (index != dt.Rows.Count)
                                    y_location += (pictureBox_height + pictureBox_margin_bottom);
                            }
                            else
                            {
                                pictureBox.Location = new Point(x_location, y_location);
                            }
                            pictureBox.BringToFront();
                        }
                        catch
                        {
                        }
                    }

                    y_location += (pictureBox_height + pictureBox_margin_bottom);

                }

            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            string url = Program.Client.PicUrl + imgPathDic[pictureBox.Name];
            FrmShowImg add = new FrmShowImg(url, "");
            add.StartPosition = FormStartPosition.CenterParent;
            add.Width = 459;
            add.Height = 549;
            add.ShowDialog();
        }

    }
}
