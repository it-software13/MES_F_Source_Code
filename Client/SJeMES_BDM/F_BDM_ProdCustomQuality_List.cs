using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Controls.Btn;
using SJeMES_Control_Library.Controls.DataGridView;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SJeMES_Control_Library.Controls.PageControl;

namespace SJeMES_BDM
{
    public partial class F_BDM_ProdCustomQuality_List : MaterialForm
    {
        #region 参数定义
        string prod_no = string.Empty;//ART编号

        //通用检测类型
        string general_testtype_no = string.Empty;//通用检测类型代号
        string general_testtype_name = string.Empty;//通用检测类型名称

        //二级分类代号名称
        string category_no = string.Empty;
        string category_name = string.Empty;

        #endregion
        private readonly MaterialSkinManager materialSkinManager;
        public F_BDM_ProdCustomQuality_List(string _prod_no)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
           Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            prod_no = _prod_no;
            IsDelete();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        /// <summary>
        /// 保存修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn3_Click(object sender, EventArgs e)
        {
            GetDateUpdate();
        }
        public void GetDateUpdate()
        {
            try
            {
                DataTable dt = new DataTable();
                for (int count = 0; count < DgvAll.Columns.Count; count++)
                {
                    DataColumn dc = new DataColumn(DgvAll.Columns[count].Name.ToString());
                    dt.Columns.Add(dc);
                }
                for (int count = 0; count < DgvAll.Rows.Count; count++)
                {
                    DataRow dr = dt.NewRow();
                    for (int countsub = 0; countsub < DgvAll.Columns.Count; countsub++)
                    {
                        dr[countsub] = Convert.ToString(DgvAll.Rows[count].Cells[countsub].Value);
                    }
                    dt.Rows.Add(dr);
                }
                if (string.IsNullOrEmpty(prod_no) ||
                   string.IsNullOrEmpty(category_no) ||
                   string.IsNullOrEmpty(general_testtype_no)
                    )
                {
                    throw new Exception("指定编号无值，请联系管理员！");
                }
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("表身无值可修改，请确认是否操作正确！");
                    return;
                }
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("prod_nos", prod_no);
                p.Add("category_no", category_no);
                p.Add("general_testtype_nos", general_testtype_no);
                //table
                p.Add("DgvAll", dt);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.ProdBASE", "UpdateList", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                else
                {
                    MessageBox.Show("保存成功");
                    LoadPage();
                }

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        /// <summary>
        /// 加载通用检测类型
        /// </summary>
        public void GetTitle()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("prod_no", prod_no);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.ProdBASE", "GetTitle", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(j["RetData1"].ToString());
                    if (dt == null)
                    {
                        return;
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        foreach (DataRow item in dt.Rows)
                        {
                            TabPage tabPage = new TabPage();
                            this.tab1.TabPages.Add(tabPage);
                            tabPage.Text = item["general_testtype_name"].ToString();
                            tabPage.Tag = item["general_testtype_no"].ToString();
                            general_testtype_no = dt.Rows[0]["general_testtype_no"].ToString().Trim();
                            general_testtype_name = dt.Rows[0]["general_testtype_name"].ToString().Trim();
                        }
                        GetTitle2();
                        GetTitle3();
                    }
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 获取二级页签
        /// </summary>
        public void GetTitle2()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("prod_no", prod_no);
                data.Add("general_testtype_no", general_testtype_no);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.ProdBASE", "GetTitle2", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(j["RetData1"].ToString());
                    if (dt == null)
                    {
                        return;
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        foreach (DataRow item in dt.Rows)
                        {
                        }
                    }
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        //判断数据还是否存在不存在就删除
        public void IsDelete()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("prod_no", prod_no);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.ProdBASE", "IsDelete", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(j["RetData1"].ToString());
                    if (dt == null)
                    {
                        return;
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        foreach (DataRow item in dt.Rows)
                        {
                        }
                    }
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }


        /// <summary>
        /// 加载ART基本信息
        /// </summary>
        public void Information()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("prod_no", prod_no);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.ProdBASE", "Information", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                DataTable dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>((string)ret.RetData1);
                if (dt2.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(this.pictureBox1.ImageLocation) || this.pictureBox1.ImageLocation != "")//查询默认图片是否存在
                    {
                        //如果存在就展示，并隐藏上传图片
                        try
                        {
                            this.pictureBox1.Image = Image.FromStream(System.Net.WebRequest.Create(Program.Client.PicUrl + dt2.Rows[0]["IMG_URL"].ToString()).GetResponse().GetResponseStream());
                        }
                        catch
                        {
                        }
                        //btnAddimg.Hide();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        //ART
                        labelA.Text = !string.IsNullOrEmpty(item["PROD_NO"].ToString()) ? item["PROD_NO"].ToString() : "";
                        //季度
                        labelB.Text = !string.IsNullOrEmpty(item["DEVELOP_SEASON"].ToString()) ? item["DEVELOP_SEASON"].ToString() : "";
                        //系列
                        /*   labelC.Text = !string.IsNullOrEmpty(item["COLORWAY_NO"].ToString()) ? item["COLORWAY_NO"].ToString() : "";*/
                        labelC.Text = "";
                        //鞋型
                        labelD.Text = !string.IsNullOrEmpty(item["SHOE_NO"].ToString()) ? item["SHOE_NO"].ToString() : "";
                        //材料版本
                        /*    labelE.Text = !string.IsNullOrEmpty(item["PROD_NO"].ToString()) ? item["PROD_NO"].ToString() : "";*/
                        labelE.Text = "";
                        //生产阶段
                        /*labelF.Text = !string.IsNullOrEmpty(item["DEVELOP_TYPE"].ToString()) ? item["DEVELOP_TYPE"].ToString() : "";*/
                        labelF.Text = "";
                        //生产月
                        labelG.Text = !string.IsNullOrEmpty(item["PRODUCT_MONTH"].ToString()) ? item["PRODUCT_MONTH"].ToString() : "";
                        //厂商/厂区
                        /*    labelH.Text = !string.IsNullOrEmpty(item["PROD_NO"].ToString()) ? item["PROD_NO"].ToString() : "";*/
                        labelH.Text = "万邦鞋厂";

                    }
                }
                else
                    throw new Exception(ret.ErrMsg);


            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        //加载二级页签
        public void GetTitle3()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("prod_no", prod_no);
                data.Add("general_testtype_no", general_testtype_no);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.ProdBASE", "GetTitle3", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    this.tab2.TabPages.Clear();

                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(j["RetData1"].ToString());
                    if (dt == null)
                    {
                        return;
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            TabPage tabPage = new TabPage();
                            this.tab2.TabPages.Add(tabPage);
                            tabPage.Text = item["category_name"].ToString();
                            tabPage.Tag = item["category_no"].ToString();
                            category_no = dt.Rows[0]["category_no"].ToString().Trim();
                            category_name = dt.Rows[0]["category_name"].ToString().Trim();
                            LoadPage();
                        }
                    }
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        //ART定制检测项明细给通用类型加上数据
        public void InsertTY()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("prod_no", prod_no);
                data.Add("general_testtype_no", general_testtype_no);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.ProdBASE", "InsertTY", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        //加载页签就查数据
        public void GetList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("prod_no", prod_no);
                data.Add("general_testtype_no", general_testtype_no);
                data.Add("category_no", category_no);
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);


                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.ProdBASE", "GetList", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                //跟以前的不同
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());

                DgvAll.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {

                        DgvAll.Rows.Add();

                        DataGridViewRow dgvr = DgvAll.Rows[i];
                        dgvr.Cells["prod_nos"].Value = dr["prod_no"].ToString();
                        dgvr.Cells["category_nos"].Value = dr["category_no"].ToString();
                        dgvr.Cells["general_testtype_nos"].Value = dr["general_testtype_no"].ToString();
                        dgvr.Cells["testtype_no"].Value = dr["testtype_no"].ToString();
                        dgvr.Cells["testtype_name"].Value = dr["testtype_name"].ToString();//检测名称
                        dgvr.Cells["testtype_names"].Value = dr["testtype_name_1"].ToString();//检测类型，试穿....
                        dgvr.Cells["testitem_category"].Value = dr["testitem_category"].ToString();
                        dgvr.Cells["testitem_code"].Value = dr["testitem_code_1"].ToString();
                        dgvr.Cells["testitem_name"].Value = dr["testitem_name_1"].ToString();
                        dgvr.Cells["check_itemTY"].Value = dr["check_itemTY_1"].ToString();
                        dgvr.Cells["check_valueTY"].Value = dr["check_valueTY_1"].ToString();
                        dgvr.Cells["check_itemDZ"].Value = dr["check_itemDZ_1"].ToString();
                        dgvr.Cells["check_valueDZ"].Value = dr["check_valueDZ_1"].ToString();
                        dgvr.Cells["unit"].Value = dr["unit_1"].ToString();
                        dgvr.Cells["reference_levelTY"].Value = dr["d_reference_level_1"].ToString();//定制
                        dgvr.Cells["reference_levelTYS"].Value = dr["reference_levelTY_1"].ToString();//通用
                        //dgvr.Cells["reference_levelBZ"].Value = dr["reference_levelTY_1"].ToString();

                        dgvr.Cells["sample_num"].Value = dr["sample_num_1"].ToString();

                        dgvr.Cells["currency_formulaName"].Value = dr["currency_formulaName"].ToString();
                        dgvr.Cells["custom_formulaName"].Value = dr["custom_formulaName"].ToString();


                        dgvr.Cells["currency_formula"].Value = dr["currency_formula_1"].ToString();
                        dgvr.Cells["custom_formula"].Value = dr["custom_formula_1"].ToString();
                        dgvr.Cells["art_remarks"].Value = dr["art_remarks_1"].ToString();

                        dgvr.Cells["delectrow"].Value = "删除";//虚拟列判断删除还是勾选.


                        //新增的三个（AQL级别，AC值，RE值）
                        dgvr.Cells["AQL_LEVEL"].Value = dr["AQL_LEVEL_1"].ToString();

                        i++;

                    }

                    DgvAll.Columns["prod_nos"].Visible = false;
                    DgvAll.Columns["category_nos"].Visible = false;
                    DgvAll.Columns["general_testtype_nos"].Visible = false;
                    DgvAll.Columns["testitem_category"].Visible = false;
                    GenClass.AutoSizeColumn(DgvAll);
                }
               
                //加多一条
                totalCount = int.Parse(dic["rowCount"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        //判断标准
        public DataTable GetDGVComboBox(string type)
        {
            #region 查询枚举
            List<string> lst_enum_type = new List<string>();
            lst_enum_type.Add(type);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                       Program.Client.APIURL,
                                       "SJ_QCMAPI",//类库名
                                       "SJ_QCMAPI.BASE",//类名
                                       "GetSYS001MDataListS",//方法名
                                       Program.Client.UserToken,//token
                                       Newtonsoft.Json.JsonConvert.SerializeObject(lst_enum_type));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
            #endregion

            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic[type].ToString());


            return dt;
        }

        //测试项测量标准
        public DataTable GetTestValue(string code)
        {
            DataTable dt = new DataTable();
            try
            {
                Dictionary<string, object> lst_enum_type = new Dictionary<string, object>();
                lst_enum_type.Add("code", code);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                           Program.Client.APIURL,
                                           "SJ_QCMAPI",//类库名
                                           "SJ_QCMAPI.Generalquality",//类名
                                           "GetTestValue",//方法名
                                           Program.Client.UserToken,//token
                                           Newtonsoft.Json.JsonConvert.SerializeObject(lst_enum_type));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                if (dic.Count > 0 && !string.IsNullOrEmpty(dic["values"].ToString()))
                {
                    dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["values"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

        //外观检查项测量标准
        public DataTable GetAppearanceValue(string code)
        {
            DataTable dt = new DataTable();
            try
            {
                Dictionary<string, object> lst_enum_type = new Dictionary<string, object>();
                lst_enum_type.Add("code", code);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                           Program.Client.APIURL,
                                           "SJ_QCMAPI",//类库名
                                           "SJ_QCMAPI.Generalquality",//类名
                                           "GetAppearanceValue",//方法名
                                           Program.Client.UserToken,//token
                                           Newtonsoft.Json.JsonConvert.SerializeObject(lst_enum_type));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                if (dic.Count > 0 && !string.IsNullOrEmpty(dic["values"].ToString()))
                {
                    dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["values"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

        //试穿检测标准
        public DataTable GetTryOnValue(string code)
        {
            DataTable dt = new DataTable();
            try
            {
                Dictionary<string, object> lst_enum_type = new Dictionary<string, object>();
                lst_enum_type.Add("code", code);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                           Program.Client.APIURL,
                                           "SJ_QCMAPI",//类库名
                                           "SJ_QCMAPI.Generalquality",//类名
                                           "GetTryOnValue",//方法名
                                           Program.Client.UserToken,//token
                                           Newtonsoft.Json.JsonConvert.SerializeObject(lst_enum_type));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                if (dic.Count > 0 && !string.IsNullOrEmpty(dic["values"].ToString()))
                {
                    dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["values"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

        //委托加载数据
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void F_BDM_ProdCustomQuality_List_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(DgvAll);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            //只要加载一次委托 
            pageControl1.BindPageEvent += GetList;
            //    
            Information();
            GetTitle();
            this.DgvAll.ClearSelection();
            this.DgvAll.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }

        /// <summary>
        /// 通用类型切换 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tab1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Visible = false;
            int index = this.tab1.SelectedIndex;
            general_testtype_no = this.tab1.TabPages[index].Tag.ToString();
            general_testtype_name = this.tab1.TabPages[index].Text;
            GetTitle3();
            InsertTY();
            LoadPage();
        }

        private void btn2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 二级分类切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tab2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Visible = false;
            try
            {
                int index = this.tab2.SelectedIndex;
                if (index >= 0)
                {
                    category_no = this.tab2.TabPages[index].Tag.ToString();
                    LoadPage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //弹出
        private void btn4_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Empty;
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("general_testtype_no", general_testtype_no);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                             "SJ_QCMAPI", "SJ_QCMAPI.Generalquality", "IFTY", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                string dic = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(ret.RetData);
                if (ret.IsSuccess)
                {
                    if (dic=="0")
                    {
                        sql = $@" SELECT
                        '测试项目' as 检测类型，
	                    TESTITEM_CODE  AS 检测项编号，
	                    TESTITEM_NAME AS 检测项名称,
                        testtype_no as 检测项类型,
                        testtype_name as 检测项类型名称,
                        check_type as 检验项目类型,
	                    CHECK_ITEM AS	判断标准通用,
	                    CHECK_VALUE AS 测量标准通用,
                        AQL_LEVEL AS AQL级别,
	                    '' AS 判断标准定制,
	                    '' AS 测量标准定制 ,
	                    UNIT AS 单位,
	                    REFERENCE_LEVEL AS 项目引用级别通用 ，
                        '' as 项目引用级别标准,
	                    SAMPLE_NUM AS 试样数量,
	                    CURRENCY_FORMULA AS 通用公式类型,
	                    custom_formula AS 自定义公式类型，
	                    '' AS ART定制备注,
	                    '1' AS TYPENAME
                       
                    FROM
	                    BDM_QUALITYTEST_ITEM 
                    WHERE general_testtype_no='{general_testtype_no}' 
                            and (quality_category_no in (select ITEM_TYPE from BDM_RD_ITEM where ITEM_NO='{category_no}') or secondary_category_no in (select ITEM_TYPE from BDM_RD_ITEM where ITEM_NO='{category_no}'))
                          and check_item is not null and check_value is not null
	                UNION
                    SELECT
                        '外观项目' as 检测类型，
	                    testitem_code  AS  检测项编号 ,
	                    testitem_name AS  检测项名称 ,
                        testtype_no as 检测项类型,
                        testtype_name as 检测项类型名称,
                        '' as 检验项目类型,
	                    CHECK_ITEM AS	 判断标准通用  ,
		                    CHECK_VALUE AS  测量标准通用  ,
                        AQL_LEVEL AS AQL级别,
	                    '' AS  判断标准定制  ,
	                    '' AS  测量标准定制  ,
	                    '' AS  单位 ,
	                    REFERENCE_LEVEL AS  项目引用级别通用  ,
                        '' as 项目引用级别标准,
	                    SAMPLE_NUM AS  试样数量 ,
	                    '' AS  通用公式类型 ,
	                    '' AS  自定义公式类型 ,
	                    '' AS  ART定制备注,
	                    '2' AS TYPENAME
                    FROM
	                    bdm_qualityaptest_item 
                    WHERE general_testtype_no='{general_testtype_no}' 
                            and (quality_category_no in (select ITEM_TYPE from BDM_RD_ITEM where ITEM_NO='{category_no}') or secondary_category_no in (select ITEM_TYPE from BDM_RD_ITEM where ITEM_NO='{category_no}'))
                            and check_item is not null and check_value is not null
	                UNION
	                    SELECT
                        '试穿项目' as 检测类型，
	                    TESTITEM_CODE  AS  检测项编号 ，
	                    TESTITEM_NAME AS  检测项名称 ,
                        testtype_no as 检测项类型,
                        testtype_name as 检测项类型名称,
                        '' as 检验项目类型,
	                    CHECK_ITEM AS	 判断标准通用  ,
	                    CHECK_VALUE AS  测量标准通用  ,
                        AQL_LEVEL AS AQL级别,
	                    '' AS  判断标准定制  ,
	                    '' AS  测量标准定制  ,
	                    '' AS  单位 ,
	                    REFERENCE_LEVEL AS  项目引用级别通用  ，
                        '' as 项目引用级别标准,
	                    SAMPLE_NUM AS  试样数量 ,
	                    '' AS  通用公式类型 ,
	                    '' AS  自定义公式类型 ，
	                    '' AS  ART定制备注,
	                    '3' AS TYPENAME
                    FROM
	                    bdm_qualitytntest_item
                    WHERE general_testtype_no='{general_testtype_no}' 
                            and (quality_category_no in (select ITEM_TYPE from BDM_RD_ITEM where ITEM_NO='{category_no}') or secondary_category_no in (select ITEM_TYPE from BDM_RD_ITEM where ITEM_NO='{category_no}'))
                            and check_item is not null and check_value is not null";
                    }
                    else
                    {
                        sql = $@" SELECT
                        '测试项目' as 检测类型，
	                    TESTITEM_CODE  AS 检测项编号，
	                    TESTITEM_NAME AS 检测项名称,
                        testtype_no as 检测项类型,
                        testtype_name as 检测项类型名称,
                        check_type as 检验项目类型,
	                    CHECK_ITEM AS	判断标准通用,
	                    CHECK_VALUE AS 测量标准通用,
                        AQL_LEVEL AS AQL级别,
	                    '' AS 判断标准定制,
	                    '' AS 测量标准定制 ,
	                    UNIT AS 单位,
	                    REFERENCE_LEVEL AS 项目引用级别通用 ，
                        '' as 项目引用级别标准,
	                    SAMPLE_NUM AS 试样数量,
	                    CURRENCY_FORMULA AS 通用公式类型,
	                    custom_formula AS 自定义公式类型，
	                    '' AS ART定制备注,
	                    '1' AS TYPENAME
                       
                    FROM
	                    BDM_QUALITYTEST_ITEM 
                    WHERE general_testtype_no='{general_testtype_no}' 
                            and (quality_category_no='{category_no}' or secondary_category_no='{category_no}')
                          and check_item is not null and check_value is not null
	                UNION
                    SELECT
                        '外观项目' as 检测类型，
	                    testitem_code  AS  检测项编号 ,
	                    testitem_name AS  检测项名称 ,
                        testtype_no as 检测项类型,
                        testtype_name as 检测项类型名称,
                        '' as 检验项目类型,
	                    CHECK_ITEM AS	 判断标准通用  ,
		                    CHECK_VALUE AS  测量标准通用  ,
                        AQL_LEVEL AS AQL级别,
	                    '' AS  判断标准定制  ,
	                    '' AS  测量标准定制  ,
	                    '' AS  单位 ,
	                    REFERENCE_LEVEL AS  项目引用级别通用  ,
                        '' as 项目引用级别标准,
	                    SAMPLE_NUM AS  试样数量 ,
	                    '' AS  通用公式类型 ,
	                    '' AS  自定义公式类型 ,
	                    '' AS  ART定制备注,
	                    '2' AS TYPENAME
                    FROM
	                    bdm_qualityaptest_item 
                    WHERE general_testtype_no='{general_testtype_no}' 
                            and (quality_category_no='{category_no}' or secondary_category_no='{category_no}')
                            and check_item is not null and check_value is not null
	                UNION
	                    SELECT
                        '试穿项目' as 检测类型，
	                    TESTITEM_CODE  AS  检测项编号 ，
	                    TESTITEM_NAME AS  检测项名称 ,
                        testtype_no as 检测项类型,
                        testtype_name as 检测项类型名称,
                        '' as 检验项目类型,
	                    CHECK_ITEM AS	 判断标准通用  ,
	                    CHECK_VALUE AS  测量标准通用  ,
                        AQL_LEVEL AS AQL级别,
	                    '' AS  判断标准定制  ,
	                    '' AS  测量标准定制  ,
	                    '' AS  单位 ,
	                    REFERENCE_LEVEL AS  项目引用级别通用  ，
                        '' as 项目引用级别标准,
	                    SAMPLE_NUM AS  试样数量 ,
	                    '' AS  通用公式类型 ,
	                    '' AS  自定义公式类型 ，
	                    '' AS  ART定制备注,
	                    '3' AS TYPENAME
                    FROM
	                    bdm_qualitytntest_item
                    WHERE general_testtype_no='{general_testtype_no}' 
                            and (quality_category_no='{category_no}' or secondary_category_no='{category_no}')
                            and check_item is not null and check_value is not null";
                    }
                }
                else
                {
                    throw new Exception(ret.ErrMsg);
                }
                
                FrmSelectData frmData = new FrmSelectData(sql, false, Program.Client, "R,TYPENAME");

                frmData.ShowDialog();

                if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
                {
                    DataTable ddd = null;
                    int a = DgvAll.Rows.Count;
                    for (int i = 0; i < frmData.RetData.Rows.Count; i++)
                    {
                        object jc = frmData.RetData.Rows[i]["检测类型"];
                        object bh = frmData.RetData.Rows[i]["检测项编号"];
                        object mc = frmData.RetData.Rows[i]["检测项名称"];
                        object lx = frmData.RetData.Rows[i]["检测项类型"];
                        object jcmc = frmData.RetData.Rows[i]["检测项类型名称"];
                        object jy = frmData.RetData.Rows[i]["检验项目类型"];
                        object typd = frmData.RetData.Rows[i]["判断标准通用"];
                        object tyjc = frmData.RetData.Rows[i]["测量标准通用"];
                        object pd = frmData.RetData.Rows[i]["判断标准定制"];
                        object cl = frmData.RetData.Rows[i]["测量标准定制"];
                        object dw = frmData.RetData.Rows[i]["单位"];
                        object xmty = frmData.RetData.Rows[i]["项目引用级别通用"];
                        object xmbz = frmData.RetData.Rows[i]["项目引用级别标准"];
                        object sy = frmData.RetData.Rows[i]["试样数量"];
                        object tygx = frmData.RetData.Rows[i]["通用公式类型"];
                        object zdygs = frmData.RetData.Rows[i]["自定义公式类型"];
                        object bz = frmData.RetData.Rows[i]["ART定制备注"];
                        object lb = frmData.RetData.Rows[i]["TYPENAME"];
                        //新增AQL级别
                        object aql = frmData.RetData.Rows[i]["AQL级别"];
                        ddd = new DataTable();
                        for (int count = 0; count < DgvAll.Columns.Count; count++)
                        {
                            DataColumn dc = new DataColumn(DgvAll.Columns[count].Name.ToString());
                            ddd.Columns.Add(dc);
                        }
                        for (int count = 0; count < DgvAll.Rows.Count; count++)
                        {
                            DataRow dr = ddd.NewRow();
                            for (int countsub = 0; countsub < DgvAll.Columns.Count; countsub++)
                            {
                                dr[countsub] = Convert.ToString(DgvAll.Rows[count].Cells[countsub].Value);
                            }
                            ddd.Rows.Add(dr);
                        }
                        DataRow[] dcl = ddd.Select($"testitem_code='{frmData.RetData.Rows[i]["检测项编号"].ToString()}'");
                        if (dcl.Length == 0)
                        {
                            DgvAll.Rows.Add();
                            DataGridViewRow dgvr = DgvAll.Rows[a];
                            dgvr.Cells["testtype_name"].Value = jcmc;
                            dgvr.Cells["testitem_code"].Value = bh;
                            dgvr.Cells["testitem_name"].Value = mc;
                            dgvr.Cells["check_itemTY"].Value = typd;
                            dgvr.Cells["check_valueTY"].Value = tyjc;
                            dgvr.Cells["check_itemDZ"].Value = pd;
                            dgvr.Cells["check_valueDZ"].Value = cl;
                            dgvr.Cells["unit"].Value = dw;
                            dgvr.Cells["reference_levelTYS"].Value = xmty;
                            dgvr.Cells["reference_levelTY"].Value = xmbz;
                            dgvr.Cells["sample_num"].Value = sy;
                            dgvr.Cells["currency_formula"].Value = tygx;
                            dgvr.Cells["custom_formula"].Value = zdygs;
                            dgvr.Cells["art_remarks"].Value = bz;
                            dgvr.Cells["testitem_category"].Value = lb;
                            dgvr.Cells["testtype_no"].Value = lx;
                            dgvr.Cells["testtype_names"].Value = jc;
                            dgvr.Cells["AQL_LEVEL"].Value = aql;
                            //加多四个虚拟列做删除判断（是否删除，是否勾选）
                            dgvr.Cells["delectrow"].Value = "删除勾选";
                            //AQL级别

                            a++;
                        }

                    }
                    GetDateUpdate();
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 点击上传图片
        /// </summary>
        public string filePath = string.Empty;
        public string SafeFileName = string.Empty;
        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
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

                    //调用接口上传图片
                    UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoad(Program.Client.APIURL, filePath, (int)enum_filepath1.enum_filepath_2, Program.Client.UserToken);
                    if (res.IsSuccess)
                    {
                        var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());

                        //返回图片名称和图片路径
                        string guid = Guid.NewGuid().ToString("N");
                        //保存图片信息QCM_CUSTOMER_COMPLAINT_FILE 
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("prod_no", prod_no);//检验单号
                        data.Add("IMG_NAME", resultDIC["filename"].ToString());//图片名称
                        data.Add("IMG_URL", resultDIC["url"].ToString());//图片路径
                        data.Add("guid", guid);//guid  

                        //保存图片信息
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                             "SJ_QCMAPI", "SJ_QCMAPI.Generalquality", "SavePhotoImgList", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                        if (!ret.IsSuccess)
                            throw new Exception(ret.ErrMsg);
                        else
                        {
                            MessageBox.Show("上传图片成功！");
                        }
                    }
                    else
                    {
                        MessageBox.Show("上传图片失败！");
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 点击上传文件
        /// </summary>
        private void UploadAll(string code)
        {
            try
            {
                string guid = Guid.NewGuid().ToString("N");
                // 创建文件弹出选择窗口（包括文件名）对象
                OpenFileDialog ofd = new OpenFileDialog();
                //判断选择的路径
                string path = string.Empty;
                ofd.Title = "请选择文件";
                ofd.Filter = "所有文件|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                    filePath = ofd.FileName;

                    //调用上传文件接口
                    UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoad(Program.Client.APIURL, filePath, (int)enum_filepath1.enum_filepath_3, Program.Client.UserToken);
                    if (res.IsSuccess)
                    {
                        var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());

                        //保存图片信息QCM_CUSTOMER_COMPLAINT_FILE 
                        Dictionary<string, object> data = new Dictionary<string, object>();

                        data.Add("prod_no", prod_no);
                        data.Add("general_testtype_no", general_testtype_no);
                        data.Add("category_no", category_no);
                        data.Add("testitem_code", code);
                        data.Add("FILE_NAME", resultDIC["filename"].ToString());//文件名称
                        data.Add("FILE_URL", resultDIC["url"].ToString());//文件路径
                        data.Add("guid", guid);//guid

                        //保存上传的文件信息
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                             "SJ_QCMAPI", "SJ_QCMAPI.Generalquality", "SaveFile", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                        if (!ret.IsSuccess)
                            throw new Exception(ret.ErrMsg);
                        else
                        {
                            MessageBox.Show("上传文件成功！");
                        }
                    }
                    else
                    {
                        MessageBox.Show("上传文件失败！");
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        public string UpLoad(string type, string code)
        {
            string isload = "no";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string saveName = DateTime.Now.ToString("yyyyMMddHHmmss") + SafeFileName;
                    var content = new MultipartFormDataContent();
                    string path = Path.Combine(filePath);

                    content.Add(new ByteArrayContent(System.IO.File.ReadAllBytes(path)), "file", saveName);
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("usertoken", Program.Client.UserToken);
                    p.Add("type", type);
                    p.Add("prod_no", prod_no);
                    p.Add("general_testtype_no", general_testtype_no);
                    p.Add("category_no", category_no);
                    p.Add("testitem_code", code);
                    p.Add("ImgName", SafeFileName);
                    content.Add(new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(p)), "p");
                    var requestUri = Program.Client.APIURL + "/UploadIMG";
                    var result = client.PostAsync(requestUri, content).Result.Content.ReadAsStringAsync().Result;

                    if (!string.IsNullOrEmpty(result))
                    {
                        Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(result.ToString());
                        Dictionary<string, object> ImgName = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(dic["returnObj"].ToString());
                        string url = Program.Client.PicUrl + ImgName["url"].ToString();
                        if (type == "1")
                        {
                            try
                            {
                                this.pictureBox1.Image = Image.FromStream(System.Net.WebRequest.Create(url).GetResponse().GetResponseStream());
                                //btnAddimg.Hide();
                            }
                            catch
                            {
                            }
                        }
                        if (dic.ContainsKey("isSuccess"))
                        {
                            string ss = dic["isSuccess"].ToString();
                            if (dic["isSuccess"].ToString().Trim().ToLower() == "true")
                            {
                                isload = "ok";
                            }
                            else
                            {
                                throw new Exception("上传失败");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("上传失败");
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            return isload;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// 单元格点击触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private void DgvAll_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                    return;


                //检测类别
                string testitem_category = DgvAll.Rows[e.RowIndex].Cells["testitem_category"].Value.ToString();
                //测试项
                if (testitem_category == "1")
                {
                    if (DgvAll.Columns[e.ColumnIndex].Name == "check_itemDZ")
                    {
                        DataTable dt_tval = GetDGVComboBox("enum_judge_symbol");
                        comboBox1.DataSource = dt_tval;
                        if (dt_tval != null && dt_tval.Rows.Count > 0)
                        {
                            comboBox1.DisplayMember = "enum_value";
                            comboBox1.ValueMember = "enum_code";
                        }
                        comboBox1.Text = DgvAll.CurrentCell.Value.ToString(); //对combobox赋值

                        Rectangle R = DgvAll.GetCellDisplayRectangle(DgvAll.CurrentCell.ColumnIndex, DgvAll.CurrentCell.RowIndex, false); //获取单元格位置 
                        comboBox1.SetBounds(R.X + DgvAll.Location.X, R.Y + DgvAll.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                        comboBox1.Visible = true;
                    }
                    else if (DgvAll.Columns[e.ColumnIndex].Name == "check_valueDZ")
                    {
                        string testitem_code = DgvAll.Rows[e.RowIndex].Cells["testitem_code"].Value.ToString();
                        DataTable dt_tval = GetTestValue(testitem_code);
                        comboBox1.DataSource = dt_tval;
                        if (dt_tval != null && dt_tval.Rows.Count > 0)
                        {
                            comboBox1.DisplayMember = "value";
                            comboBox1.ValueMember = "value";
                        }
                        comboBox1.Text = DgvAll.CurrentCell.Value.ToString(); //对combobox赋值

                        Rectangle R = DgvAll.GetCellDisplayRectangle(DgvAll.CurrentCell.ColumnIndex, DgvAll.CurrentCell.RowIndex, false); //获取单元格位置 
                        comboBox1.SetBounds(R.X + DgvAll.Location.X, R.Y + DgvAll.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                        comboBox1.Visible = true;
                    }
                    else if (DgvAll.Columns[e.ColumnIndex].Name == "reference_levelTY")
                    {
                        DataTable dt_tval = GetDGVComboBox("enum_ref_level");
                        comboBox1.DataSource = dt_tval;
                        if (dt_tval != null && dt_tval.Rows.Count > 0)
                        {
                            comboBox1.DisplayMember = "enum_value";
                            comboBox1.ValueMember = "enum_code";
                        }
                        comboBox1.Text = DgvAll.CurrentCell.Value.ToString(); //对combobox赋值

                        Rectangle R = DgvAll.GetCellDisplayRectangle(DgvAll.CurrentCell.ColumnIndex, DgvAll.CurrentCell.RowIndex, false); //获取单元格位置 
                        comboBox1.SetBounds(R.X + DgvAll.Location.X, R.Y + DgvAll.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                        comboBox1.Visible = true;
                    }
                    else if (DgvAll.Columns[e.ColumnIndex].Name == "art_remarks")
                    {
                        txt1.Text = DgvAll.CurrentCell.Value.ToString(); //对textbox赋值

                        Rectangle R = DgvAll.GetCellDisplayRectangle(DgvAll.CurrentCell.ColumnIndex, DgvAll.CurrentCell.RowIndex, false); //获取单元格位置 
                        txt1.SetBounds(R.X + DgvAll.Location.X, R.Y + DgvAll.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                        txt1.Visible = true;
                    }
                    else
                    {
                        comboBox1.Visible = false;
                        txt1.Visible = false;
                    }
                }
                else if (testitem_category == "2")
                {
                    if (DgvAll.Columns[e.ColumnIndex].Name == "check_itemDZ")
                    {
                        DataTable dt_tval = GetDGVComboBox("enum_whether");
                        comboBox1.DataSource = dt_tval;
                        if (dt_tval != null && dt_tval.Rows.Count > 0)
                        {
                            comboBox1.DisplayMember = "enum_value";
                            comboBox1.ValueMember = "enum_code";
                        }
                        comboBox1.Text = DgvAll.CurrentCell.Value.ToString(); //对combobox赋值

                        Rectangle R = DgvAll.GetCellDisplayRectangle(DgvAll.CurrentCell.ColumnIndex, DgvAll.CurrentCell.RowIndex, false); //获取单元格位置 
                        comboBox1.SetBounds(R.X + DgvAll.Location.X, R.Y + DgvAll.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                        comboBox1.Visible = true;
                    }
                    else if (DgvAll.Columns[e.ColumnIndex].Name == "check_valueDZ")
                    {
                        string testitem_code = DgvAll.Rows[e.RowIndex].Cells["testitem_code"].Value.ToString();
                        DataTable dt_tval = GetAppearanceValue(testitem_code);
                        comboBox1.DataSource = dt_tval;
                        if (dt_tval != null && dt_tval.Rows.Count > 0)
                        {
                            comboBox1.DisplayMember = "test_standard";
                            comboBox1.ValueMember = "test_standard";
                        }
                        comboBox1.Text = DgvAll.CurrentCell.Value.ToString(); //对combobox赋值

                        Rectangle R = DgvAll.GetCellDisplayRectangle(DgvAll.CurrentCell.ColumnIndex, DgvAll.CurrentCell.RowIndex, false); //获取单元格位置 
                        comboBox1.SetBounds(R.X + DgvAll.Location.X, R.Y + DgvAll.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                        comboBox1.Visible = true;
                    }
                    else if (DgvAll.Columns[e.ColumnIndex].Name == "reference_levelTY")
                    {
                        DataTable dt_tval = GetDGVComboBox("enum_ref_level");
                        comboBox1.DataSource = dt_tval;
                        if (dt_tval != null && dt_tval.Rows.Count > 0)
                        {
                            comboBox1.DisplayMember = "enum_value";
                            comboBox1.ValueMember = "enum_code";
                        }
                        comboBox1.Text = DgvAll.CurrentCell.Value.ToString(); //对combobox赋值

                        Rectangle R = DgvAll.GetCellDisplayRectangle(DgvAll.CurrentCell.ColumnIndex, DgvAll.CurrentCell.RowIndex, false); //获取单元格位置 
                        comboBox1.SetBounds(R.X + DgvAll.Location.X, R.Y + DgvAll.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                        comboBox1.Visible = true;
                    }
                    else if (DgvAll.Columns[e.ColumnIndex].Name == "art_remarks")
                    {
                        txt1.Text = DgvAll.CurrentCell.Value.ToString(); //对textbox赋值

                        Rectangle R = DgvAll.GetCellDisplayRectangle(DgvAll.CurrentCell.ColumnIndex, DgvAll.CurrentCell.RowIndex, false); //获取单元格位置 
                        txt1.SetBounds(R.X + DgvAll.Location.X, R.Y + DgvAll.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                        txt1.Visible = true;
                    }
                    else
                    {
                        comboBox1.Visible = false;
                        txt1.Visible = false;
                    }
                }
                else if (testitem_category == "3")
                {
                    if (DgvAll.Columns[e.ColumnIndex].Name == "check_itemDZ")
                    {
                        DataTable dt_tval = GetDGVComboBox("enum_whether");
                        comboBox1.DataSource = dt_tval;
                        if (dt_tval != null && dt_tval.Rows.Count > 0)
                        {
                            comboBox1.DisplayMember = "enum_value";
                            comboBox1.ValueMember = "enum_code";
                        }
                        comboBox1.Text = DgvAll.CurrentCell.Value.ToString(); //对combobox赋值

                        Rectangle R = DgvAll.GetCellDisplayRectangle(DgvAll.CurrentCell.ColumnIndex, DgvAll.CurrentCell.RowIndex, false); //获取单元格位置 
                        comboBox1.SetBounds(R.X + DgvAll.Location.X, R.Y + DgvAll.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                        comboBox1.Visible = true;
                    }
                    else if (DgvAll.Columns[e.ColumnIndex].Name == "check_valueDZ")
                    {
                        string testitem_code = DgvAll.Rows[e.RowIndex].Cells["testitem_code"].Value.ToString();
                        DataTable dt_tval = GetTryOnValue(testitem_code);
                        comboBox1.DataSource = dt_tval;
                        if (dt_tval != null && dt_tval.Rows.Count > 0)
                        {
                            comboBox1.DisplayMember = "test_standard";
                            comboBox1.ValueMember = "test_standard";
                        }
                        comboBox1.Text = DgvAll.CurrentCell.Value.ToString(); //对combobox赋值

                        Rectangle R = DgvAll.GetCellDisplayRectangle(DgvAll.CurrentCell.ColumnIndex, DgvAll.CurrentCell.RowIndex, false); //获取单元格位置 
                        comboBox1.SetBounds(R.X + DgvAll.Location.X, R.Y + DgvAll.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                        comboBox1.Visible = true;
                    }
                    else if (DgvAll.Columns[e.ColumnIndex].Name == "reference_levelTY")
                    {
                        DataTable dt_tval = GetDGVComboBox("enum_ref_level");
                        comboBox1.DataSource = dt_tval;
                        if (dt_tval != null && dt_tval.Rows.Count > 0)
                        {
                            comboBox1.DisplayMember = "enum_value";
                            comboBox1.ValueMember = "enum_code";
                        }
                        comboBox1.Text = DgvAll.CurrentCell.Value.ToString(); //对combobox赋值

                        Rectangle R = DgvAll.GetCellDisplayRectangle(DgvAll.CurrentCell.ColumnIndex, DgvAll.CurrentCell.RowIndex, false); //获取单元格位置 
                        comboBox1.SetBounds(R.X + DgvAll.Location.X, R.Y + DgvAll.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                        comboBox1.Visible = true;
                    }
                    else if (DgvAll.Columns[e.ColumnIndex].Name == "art_remarks")
                    {
                        txt1.Text = DgvAll.CurrentCell.Value.ToString(); //对textbox赋值

                        Rectangle R = DgvAll.GetCellDisplayRectangle(DgvAll.CurrentCell.ColumnIndex, DgvAll.CurrentCell.RowIndex, false); //获取单元格位置 
                        txt1.SetBounds(R.X + DgvAll.Location.X, R.Y + DgvAll.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                        txt1.Visible = true;
                    }
                    else
                    {
                        comboBox1.Visible = false;
                        txt1.Visible = false;
                    }
                }

                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    string name = this.DgvAll.Columns[e.ColumnIndex].Name;
                    if (name == "operation")
                    {
                        DataGridViewOperationCell cell = this.DgvAll.Rows[this.DgvAll.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                        if (cell.CurrentItem == null)
                        {
                            return;
                        }
                        //删除检验项目
                        if (cell.CurrentItem.Equals("DELETE"))
                        {
                            if (MessageBox.Show("确认删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                try
                                {
                                    string prod_nos = DgvAll.CurrentRow.Cells["prod_nos"].Value.ToString();
                                    string testitem_codes = DgvAll.CurrentRow.Cells["testitem_code"].Value.ToString();
                                    string category_no = DgvAll.CurrentRow.Cells["category_nos"].Value.ToString();
                                    string general_testtype_nos = DgvAll.CurrentRow.Cells["general_testtype_nos"].Value.ToString();
                                    Dictionary<string, object> data = new Dictionary<string, object>();

                                    data.Add("prod_no", prod_nos);
                                    data.Add("general_testtype_no", general_testtype_nos);
                                    data.Add("category_no", category_no);
                                    data.Add("testitem_code", testitem_codes);
                                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                                         "SJ_QCMAPI", "SJ_QCMAPI.ProdBASE", "ARTDelete", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                    if (!ret.IsSuccess)
                                    {
                                        throw new Exception(ret.ErrMsg);
                                    }
                                    else
                                    {
                                        LoadPage();
                                        MessageBox.Show("删除原有数据成功");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                                }
                            }
                        }
                        //查看文件
                        else if (cell.CurrentItem.Equals("DETAIL"))
                        {
                            //表名
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            p.Add("tablename", "BDM_PROD_CUSTOMQUALITY_FILE");

                            //字段名
                            Dictionary<string, object> fileddic = new Dictionary<string, object>();
                            fileddic.Add("file_url", "file_url");
                            fileddic.Add("file_name", "file_name");
                            p.Add("fileds", fileddic);

                            //查询条件
                            Dictionary<string, object> parmsdic = new Dictionary<string, object>();
                            parmsdic.Add("prod_no", prod_no);
                            parmsdic.Add("general_testtype_no", general_testtype_no);
                            parmsdic.Add("category_no", category_no);
                            parmsdic.Add("testitem_code", DgvAll.Rows[e.RowIndex].Cells["testitem_code"].Value.ToString());
                            p.Add("parms", parmsdic);

                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                        Program.Client.APIURL,
                                                        "SJ_QCMAPI",//类库名
                                                        "SJ_QCMAPI.BASE",//类名
                                                        "GetFileView",//方法名
                                                        Program.Client.UserToken,//token
                                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));

                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                            if (!ret.IsSuccess)
                            {
                                throw new Exception(ret.ErrMsg);
                            }
                            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                            if (dt.Rows.Count > 0)
                            {
                                dt.Columns.Add("net_file_url", typeof(string));
                                foreach (DataRow dr in dt.Rows)
                                {
                                    dr["net_file_url"] = Program.Client.PicUrl + dr["file_url"];
                                }
                            }

                            FrmFileList add = new FrmFileList(dt, Program.Client.UploadUrl, Program.Client.UserToken);
                            add.StartPosition = FormStartPosition.CenterParent;
                            add.ShowDialog();
                        }
                        //上传文件
                        else if (cell.CurrentItem.Equals("UploadFile"))
                        {
                            string code = DgvAll.Rows[e.RowIndex].Cells["testitem_code"].Value.ToString();
                            UploadAll(code);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        public DataTable GetVIew(string testitem_codes)
        {

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("prod_no", prod_no);
            p.Add("general_testtype_no", general_testtype_no);
            p.Add("category_no", category_no);
            p.Add("testitem_code", testitem_codes);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.InspectionTableView",//类名
                                        "GET_PROD_File_List",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata.ToString());
            return Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["RetData1"].ToString());
        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DgvAll.CurrentCell.Value = comboBox1.SelectedValue.ToString();
        }

        private void txt1_Validated(object sender, EventArgs e)
        {
            DgvAll.CurrentCell.Value = txt1.Text.ToString();
        }

        private void tab2_Selected(object sender, TabControlEventArgs e)
        {

        }

        private void DgvAll_Scroll(object sender, ScrollEventArgs e)
        {
            comboBox1.Visible = false;
        }

        private void btnSelect_Click(object sender, MouseEventArgs e)
        {
            try
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
                    string res = UpLoad("1", "");
                    if (res == "ok")
                    {
                        MessageBox.Show("上传文件成功！");
                    }
                    else
                    {
                        MessageBox.Show("上传文件失败！");
                    }
                }

            }
            catch (Exception ex)
            {

                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void uploadbtn_Click(object sender, EventArgs e)
        {
            //创建文件弹出选择窗口（包括文件名）对象
            OpenFileDialog ofd = new OpenFileDialog();
            //判断选择的路径
            string path = string.Empty;
            ofd.Title = "请选择文件";
            ofd.Filter = "所有文件|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string SafeFileName = Path.GetFileName(ofd.FileName);
                string filePath = ofd.FileName;
                UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoad(Program.Client.APIURL, filePath, 11, Program.Client.UserToken);
                if (res.IsSuccess)
                {
                    var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                    //string file_url = resultDIC["url"].ToString();
                    //string file_name = resultDIC["filename"].ToString();
                    //this.fileName.Enabled = false;
                    //this.link_file_url.Text = Program.Client.PicUrl + file_url;
                    this.panel1.Visible = true;
                    MessageBox.Show("上传文件成功！");
                }
                else
                {

                    MessageBox.Show("上传文件失败！");
                }
            }
        }

        //查看文件
        private void selectbtn_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("file_name", typeof(string));
            dt.Columns.Add("file_url", typeof(string));
            dt.Columns.Add("net_file_url", typeof(string));
            dt.Columns.Add("tablename", typeof(string));


            DataRow dr = dt.NewRow();
            dr["id"] = "1";
            dr["file_name"] = "PB.pdf";
            dr["file_url"] = "/File/PB.pdf";
            dr["net_file_url"] = "/File/PB.pdf";
            dr["tablename"] = "XXX";
            //dt.Rows.Add(dr);

            DataRow dr2 = dt.NewRow();
            dr2["id"] = "2";
            dr2["file_name"] = "VS FW20 Harden Vol 4 - Daniel Patrick Visual Standard 022620.pdf";
            dr2["file_url"] = "/File/VS%20FW20%20Harden%20Vol%20%204%20-%20Daniel%20Patrick%20Visual%20Standard%20022620.pdf";
            dr2["net_file_url"] = "";
            dr2["tablename"] = "ZZZ";

            dt.Rows.Add(dr);
            dt.Rows.Add(dr2);

            if (dt.Rows.Count > 0)
            {
                //dt.Columns.Add("net_file_url", typeof(string));
                foreach (DataRow dr3 in dt.Rows)
                {
                    dr3["net_file_url"] = Program.Client.PicUrl + dr3["file_url"];
                }
            }
            FrmFileList add = new FrmFileList(dt, Program.Client.UploadUrl, Program.Client.UserToken);
            add.ShowDialog();
        }
    }
}
