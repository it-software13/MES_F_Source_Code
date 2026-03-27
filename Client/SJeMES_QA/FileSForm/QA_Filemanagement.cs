using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Controls.Btn;
using SJeMES_Control_Library.Controls.DataGridView;
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

namespace SJeMES_QA.FileSForm
{
    public partial class QA_Filemanagement : MaterialForm
    {
     
        private readonly MaterialSkinManager materialSkinManager;
        /// <summary>
        /// 鞋型
        /// </summary>
        private string develop_seasons;
        /// <summary>
        /// 鞋号
        /// </summary>
        private string shoe_nos;
        public QA_Filemanagement(string develop_season, string shoe_no)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
       Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            develop_seasons = develop_season;
            shoe_nos = shoe_no;
        }
       
        private void QA_Filemanagement_Load(object sender, EventArgs e)
        {
            #region 加载操作按钮
            //根据界面功能加载
            List<ActionButton> list = new List<ActionButton>();
            //list.Add(ActionButtonDefaultConfig.GetUpdateBtnConfig());//修改
            //list.Add(ActionButtonDefaultConfig.GetDeleteBtnConfig());//删除
            list.Add(ActionButtonDefaultConfig.GetDetailBtnConfig());//查看明细
            //list.Add(ActionButtonDefaultConfig.GetPrintBtnConfig());//打印
            //list.Add(ActionButtonDefaultConfig.GetUploadIMGBtnConfig());//上传图片
            //list.Add(ActionButtonDefaultConfig.GetUploadFileBtnConfig()); //上传文件
            DataGridViewActionButtonColumn dataGridViewColumn = new DataGridViewActionButtonColumn(list);

            dataGridViewColumn.Width = 80;
            dataGridViewColumn.HeaderText = "操作";
            dataGridViewColumn.Name = "operation";
            dataGridViewColumn.Resizable = DataGridViewTriState.False;
            dataGridViewColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns.Add(dataGridViewColumn);
            this.dataGridView1.Columns["operation"].DisplayIndex = 0;//设置列在最左侧
            this.dataGridView1.Columns["operation"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.dataGridView1.Columns["operation"].Frozen = true;//设置列冻结
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            Table();
            #endregion
        }
        public void Table()
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("develop_season", develop_seasons);
                p.Add("shoe_no", shoe_nos);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.QAShoeShapeTable",//类名
                                            "GET_qcm_qa_shoeshape_file_View",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);

                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["develop_season"].Value = dr["develop_season"].ToString();
                        dgvr.Cells["shoe_no"].Value = dr["shoe_no"].ToString();
                        dgvr.Cells["file_type"].Value = dr["file_type"].ToString();
                        dgvr.Cells["file_name"].Value = dr["file_name"].ToString();
                        i++;
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 &&e.ColumnIndex>=0)
            {
                if (this.dataGridView1.Columns[e.ColumnIndex].Name == "operation")
                {
                    DataGridViewActionButtonColumn dataGridViewColumn = (DataGridViewActionButtonColumn)this.dataGridView1.Columns[e.ColumnIndex];

                    List<ActionButton> buttonList = dataGridViewColumn.ButtonList;

                
                    foreach (ActionButton act in buttonList)
                    {
                        //此时鼠标悬浮在上面
                        if (act.MouseOnButton)
                        {
                            //MessageBox.Show("点击了:" + act.Name);
                            if (act.Name.Equals("DETAIL"))//查看
                            {
                               
                            }
                           
                        }
                    }

                }
            }
        }
    }
}
