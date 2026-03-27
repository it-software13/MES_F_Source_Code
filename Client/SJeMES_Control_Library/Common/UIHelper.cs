using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SJeMES_Framework.Common
{
    public class UIHelper
    {
        static Dictionary<string, Internationalization> Data = new Dictionary<string, Internationalization>();
        public static void GetAllUIInfo(SJeMES_Framework.Class.ClientClass Client,string WebServiceUrl)
        {
            Data = new Dictionary<string, Internationalization>();
            try
            {

                Dictionary<string, string> P = new Dictionary<string, string>();
                string retdata = string.Empty;
                bool IsSuccess = true;
                string strDT = string.Empty;
                string ErrMsg = string.Empty;
                if (string.IsNullOrEmpty(Client.APIURL))
                {
                    retdata = WebServiceHelper.RunService(WebServiceUrl,
                        "SJEMS_API", "SJEMS_API.SJQMS_API",
                        "GetAllUIInfo", P);
                    IsSuccess = Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(retdata, "<IsSuccess>", "</IsSuccess>"));
                    if (IsSuccess)
                    {
                        string retData = Common.StringHelper.GetDataFromFirstTag(retdata, "<RetData>", "</RetData>");
                        strDT = Common.StringHelper.GetDataFromFirstTag(retData, "<DataTable>", "</DataTable>");
                    }
                }
                else
                {
                    retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
              "SJ_SYSAPI", "SJ_SYSAPI.UIHelper", "GetAllUIInfo", Client.UserToken, string.Empty);
                   var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                    IsSuccess = Convert.ToBoolean(j["IsSuccess"].ToString());
                    if (IsSuccess)
                    {
                       strDT = j["RetData"].ToString();

                    }
                    else
                    {
                        ErrMsg = j["ErrMsg"].ToString();
                    }
                }

               

                // bool IsSuccess = Convert.ToBoolean(WebServiceHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>"));
                
                if (IsSuccess)
                {
                   
                    //string retData = WebServiceHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");

                    DataTable DT = new DataTable();
                    DT.Columns.Add("ui_code");
                    DT.Columns.Add("ui_cn");
                    DT.Columns.Add("ui_en");
                    DT.Columns.Add("ui_yn");

                    //string strDT = WebServiceHelper.GetDataFromFirstTag(retData, "<DataTable>", "</DataTable>");
                    
                    string[] s = new string[1];
                    s[0] = "@;";
                    string[] strRow = strDT.Split(s, StringSplitOptions.RemoveEmptyEntries);

                    s = new string[1];
                    s[0] = "@,";
                    foreach (string strTmp in strRow)
                    {
                        DataRow dr = DT.NewRow();
                        dr["ui_code"] = strTmp.Split(s, StringSplitOptions.None)[0];
                        dr["ui_cn"] = strTmp.Split(s, StringSplitOptions.None)[1];
                        dr["ui_en"] = strTmp.Split(s, StringSplitOptions.None)[2];
                        dr["ui_yn"] = strTmp.Split(s, StringSplitOptions.None)[3];
                        DT.Rows.Add(dr);
                    }

                    foreach (DataRow DR in DT.Rows)
                    {
                        Internationalization inter = new Internationalization();
                        inter.SetValue("cn", DR["ui_cn"].ToString());
                        inter.SetValue("en", DR["ui_en"].ToString());
                        inter.SetValue("yn", DR["ui_yn"].ToString());
                        if(!Data.ContainsKey(DR["ui_code"].ToString().Replace("\r", "").Replace("\n", "")))
                        Data.Add(DR["ui_code"].ToString().Replace("\r","").Replace("\n",""), inter);
                    }
                }
                else
                {
                    throw new Exception(ErrMsg);
                }

                
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static string  GetLanguage(string s, SJeMES_Framework.Class.ClientClass Client,string WebServiceUrl,string Language)
        {
            string ret = s;

            if (Data.Keys.Count == 0)
            {
                GetAllUIInfo(Client, WebServiceUrl);
            }

            if (Data.ContainsKey(s))
            {
                if(!string.IsNullOrEmpty(Data[s].GetValue(Language)))
                ret = Data[s].GetValue(Language);
            }
            else if (!string.IsNullOrEmpty(s))
            {
                Dictionary<string, string> P = new Dictionary<string, string>();
                P.Add("ui_code", s);
                P.Add("ui_cn", s);
                if (!string.IsNullOrEmpty(WebServiceUrl))
                {
                    string XML = WebServiceHelper.RunService(WebServiceUrl,
                   "SJEMS_API", "SJEMS_API.SJQMS_API",
                   "SetUIInfo", P);
                }
                else
                {
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
            "SJ_SYSAPI", "SJ_SYSAPI.UIHelper", "SetUIInfo", Client.UserToken, string.Empty);
                }

              
            }

            if(string.IsNullOrEmpty(ret))
            {
                ret = s;
            }

            return ret;
        }

        public static void UIUpdate2( Control control, SJeMES_Framework.Class.ClientClass Client,string WebServiceUrl,string Language)
        {
            try
            {
                if(Data.Keys.Count ==0)
                {
                    GetAllUIInfo(Client, WebServiceUrl);
                }

                if (control is ComboBox)
                {
                    ComboBox c = control as ComboBox;

                    for(int i = 0;i<c.Items.Count;i++)
                    {
                        if (Data.ContainsKey(c.Items[i].ToString().Trim().Replace("\r\n", "")))
                        {
                            if (!string.IsNullOrEmpty(Data[c.Items[i].ToString().Trim().Replace("\r\n", "")].GetValue(Language)))
                                c.Items[i] = Data[c.Items[i].ToString().Trim().Replace("\r\n", "")].GetValue(Language);
                        }
                        
                        else if (!string.IsNullOrEmpty(c.Items[i].ToString()))
                        {
                            Dictionary<string, string> P = new Dictionary<string, string>();
                            P.Add("ui_code", c.Items[i].ToString().Trim().Replace("\r\n", ""));
                            P.Add("ui_cn", c.Items[i].ToString().Trim().Replace("\r\n", ""));

                            // string XML = WebServiceHelper.RunService(
                            //"SJQMS_API", "SJQMS_API.SJQMS_API",
                            //"SetUIInfo", P);
                            if (!string.IsNullOrEmpty(WebServiceUrl))
                            {
                                string XML = WebServiceHelper.RunService(WebServiceUrl,
                               "SJEMS_API", "SJEMS_API.SJQMS_API",
                               "SetUIInfo", P);
                            }
                            else
                            {
                                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                        "SJ_SYSAPI", "SJ_SYSAPI.UIHelper", "SetUIInfo", Client.UserToken, string.Empty);
                            }
                        }
                    }
                }
                else
                {
                    if (Data.ContainsKey(control.Text.Trim().Replace("\r\n", "")))
                    {
                        if (!string.IsNullOrEmpty(Data[control.Text.Trim().Replace("\r\n", "")].GetValue(Language)))
                            control.Text = Data[control.Text.Trim().Replace("\r\n", "")].GetValue(Language);
                    }
                    else if (!string.IsNullOrEmpty(control.Text.Trim()))
                    {
                        Dictionary<string, string> P = new Dictionary<string, string>();
                        P.Add("ui_code", control.Text.Trim().Replace("\r\n", ""));
                        P.Add("ui_cn", control.Text.Trim().Replace("\r\n", ""));

                        if (!string.IsNullOrEmpty(WebServiceUrl))
                        {
                            string XML = WebServiceHelper.RunService(WebServiceUrl,
                           "SJEMS_API", "SJEMS_API.SJQMS_API",
                           "SetUIInfo", P);
                        }
                        else
                        {
                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                    "SJ_SYSAPI", "SJ_SYSAPI.UIHelper", "SetUIInfo", Client.UserToken, string.Empty);
                        }
                    }



                    foreach (Control c in control.Controls)
                    {

                        if (c.Controls.Count > 0)
                        {
                            UIUpdate2(c, Client, WebServiceUrl, Language);
                        }
                        if (c is ComboBox)
                        {
                            ComboBox cc = c as ComboBox;

                            for (int i = 0; i < cc.Items.Count; i++)
                            {
                                if (Data.ContainsKey(cc.Items[i].ToString().Trim().Replace("\r\n", "")))
                                {
                                    if (!string.IsNullOrEmpty(Data[cc.Items[i].ToString().Trim().Replace("\r\n", "")].GetValue(Language)))
                                        cc.Items[i] = Data[cc.Items[i].ToString().Trim().Replace("\r\n", "")].GetValue(Language);
                                }
                                else if (!string.IsNullOrEmpty(cc.Items[i].ToString().Trim().Replace("\r\n", "")))
                                {
                                    Dictionary<string, string> P = new Dictionary<string, string>();
                                    P.Add("ui_code", cc.Items[i].ToString().Trim().Replace("\r\n", ""));
                                    P.Add("ui_cn", cc.Items[i].ToString().Trim().Replace("\r\n", ""));

                                    if (!string.IsNullOrEmpty(WebServiceUrl))
                                    {
                                        string XML = WebServiceHelper.RunService(WebServiceUrl,
                                       "SJEMS_API", "SJEMS_API.SJQMS_API",
                                       "SetUIInfo", P);
                                    }
                                    else
                                    {
                                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                                "SJ_SYSAPI", "SJ_SYSAPI.UIHelper", "SetUIInfo", Client.UserToken, string.Empty);
                                    }
                                }
                            }
                        }
                        else if (Data.ContainsKey(c.Text.Replace("\r\n","")))
                        {
                            if (!string.IsNullOrEmpty(Data[c.Text.Replace("\r\n", "")].GetValue(Language)))
                                c.Text = Data[c.Text.Replace("\r\n", "")].GetValue(Language);
                        }
                        else if (!string.IsNullOrEmpty(c.Text.Trim().Replace("\r\n", "")))
                        {
                            Dictionary<string, string> P = new Dictionary<string, string>();
                            P.Add("ui_code", c.Text.Trim().Replace("\r\n", ""));
                            P.Add("ui_cn", c.Text.Trim().Replace("\r\n", ""));

                            if (!string.IsNullOrEmpty(WebServiceUrl))
                            {
                                string XML = WebServiceHelper.RunService(WebServiceUrl,
                               "SJEMS_API", "SJEMS_API.SJQMS_API",
                               "SetUIInfo", P);
                            }
                            else
                            {
                                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                        "SJ_SYSAPI", "SJ_SYSAPI.UIHelper", "SetUIInfo", Client.UserToken, string.Empty);
                            }
                        }

                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void UIUpdate(string Form,Control control, SJeMES_Framework.Class.ClientClass Client,string WebServiceUrl,string Language)
        {
            try
            {
                if (Data.Keys.Count == 0)
                {
                    GetAllUIInfo(Client, WebServiceUrl);
                }

                if (control is ComboBox)
                {
                    ComboBox c = control as ComboBox;

                    for (int i = 0; i < c.Items.Count; i++)
                    {
                        if (Data.ContainsKey(c.Items[i].ToString().Trim().Replace("\r\n", "")))
                        {
                            if (!string.IsNullOrEmpty(Data[c.Items[i].ToString().Trim().Replace("\r\n", "")].GetValue(Language)))
                                c.Items[i] = Data[c.Items[i].ToString().Trim().Replace("\r\n", "")].GetValue(Language);
                        }

                        else if (!string.IsNullOrEmpty(c.Items[i].ToString()))
                        {
                            Dictionary<string, string> P = new Dictionary<string, string>();
                            P.Add("ui_code", c.Items[i].ToString().Trim().Replace("\r\n", ""));
                            P.Add("ui_cn", c.Items[i].ToString().Trim().Replace("\r\n", ""));

                            if (!string.IsNullOrEmpty(WebServiceUrl))
                            {
                                string XML = WebServiceHelper.RunService(WebServiceUrl,
                               "SJEMS_API", "SJEMS_API.SJQMS_API",
                               "SetUIInfo", P);
                            }
                            else
                            {
                                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                        "SJ_SYSAPI", "SJ_SYSAPI.UIHelper", "SetUIInfo", Client.UserToken, string.Empty);
                            }
                        }
                    }
                }
                else
                {
                    if (Data.ContainsKey(Form+"."+ control.Name))
                    {
                        if (!string.IsNullOrEmpty(Data[Form + "." + control.Name].GetValue(Language)))
                            control.Text = Data[Form + "." + control.Name].GetValue(Language);
                    }
                    else if (!string.IsNullOrEmpty(control.Text.Trim()))
                    {
                        Dictionary<string, string> P = new Dictionary<string, string>();
                        P.Add("ui_code", Form + "." + control.Name);
                        P.Add("ui_cn", control.Text.Trim());

                        if (!string.IsNullOrEmpty(WebServiceUrl))
                        {
                            string XML = WebServiceHelper.RunService(WebServiceUrl,
                           "SJEMS_API", "SJEMS_API.SJQMS_API",
                           "SetUIInfo", P);
                        }
                        else
                        {
                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                    "SJ_SYSAPI", "SJ_SYSAPI.UIHelper", "SetUIInfo", Client.UserToken, string.Empty);
                        }

                    }



                    foreach (Control c in control.Controls)
                    {

                        if (c.Controls.Count > 0)
                        {
                            UIUpdate(Form,c, Client, WebServiceUrl, Language);
                        }
                        if (c is ComboBox)
                        {
                            ComboBox cc = c as ComboBox;

                            for (int i = 0; i < cc.Items.Count; i++)
                            {
                                if (Data.ContainsKey(cc.Items[i].ToString().Trim().Replace("\r\n", "")))
                                {
                                    if (!string.IsNullOrEmpty(Data[cc.Items[i].ToString().Trim().Replace("\r\n", "")].GetValue(Language)))
                                        cc.Items[i] = Data[cc.Items[i].ToString().Trim().Replace("\r\n", "")].GetValue(Language);
                                }
                                else if (!string.IsNullOrEmpty(cc.Items[i].ToString().Trim().Replace("\r\n", "")))
                                {
                                    Dictionary<string, string> P = new Dictionary<string, string>();
                                    P.Add("ui_code", cc.Items[i].ToString().Trim().Replace("\r\n", ""));
                                    P.Add("ui_cn", cc.Items[i].ToString().Trim().Replace("\r\n", ""));

                                    if (!string.IsNullOrEmpty(WebServiceUrl))
                                    {
                                        string XML = WebServiceHelper.RunService(WebServiceUrl,
                                       "SJEMS_API", "SJEMS_API.SJQMS_API",
                                       "SetUIInfo", P);
                                    }
                                    else
                                    {
                                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                                "SJ_SYSAPI", "SJ_SYSAPI.UIHelper", "SetUIInfo", Client.UserToken, string.Empty);
                                    }

                                }
                            }
                        }
                        else if (Data.ContainsKey(Form + "." + c.Name))
                        {
                            if (!string.IsNullOrEmpty(Data[Form + "." + c.Name].GetValue(Language)))
                                c.Text = Data[Form + "." + c.Name].GetValue(Language);
                        }
                        else if (!string.IsNullOrEmpty(c.Text.Trim()))
                        {
                            Dictionary<string, string> P = new Dictionary<string, string>();
                            P.Add("ui_code", Form + "." + c.Name);
                            P.Add("ui_cn", c.Text.Trim());

                            if (!string.IsNullOrEmpty(WebServiceUrl))
                            {
                                string XML = WebServiceHelper.RunService(WebServiceUrl,
                               "SJEMS_API", "SJEMS_API.SJQMS_API",
                               "SetUIInfo", P);
                            }
                            else
                            {
                                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                        "SJ_SYSAPI", "SJ_SYSAPI.UIHelper", "SetUIInfo", Client.UserToken, string.Empty);
                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
