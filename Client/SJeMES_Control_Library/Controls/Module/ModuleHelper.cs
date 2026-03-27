using SJeMES_Framework.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Control_Library.Controls
{
    public class ModuleHelper
    {
        public static SJeMES_Framework.Web.JSONPanelClassHList GetHList(string ModuleCode,bool ShowSYS, SJeMES_Framework.Class.ClientClass Client)
        {
            SJeMES_Framework.Web.JSONPanelClassHList j = new SJeMES_Framework.Web.JSONPanelClassHList();

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("APP_Code", ModuleCode);
            data.Add("ShowSYS", ShowSYS);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.SYS", "WebGetModuleConfigPanelHList", Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
            {


                j = Newtonsoft.Json.JsonConvert.DeserializeObject< SJeMES_Framework.Web.JSONPanelClassHList > (ret["RetData"].ToString());
            }
            else
            {
                throw new Exception(ret["ErrMsg"].ToString());
            }


            return j;
        }

        public static Dictionary<string,object> GetModuleConfig(string ModuleCode, SJeMES_Framework.Class.ClientClass Client)
        {
            
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("APP_Code", ModuleCode);
           
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.SYS", "WebGetModuleConfig", Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
            {

                return Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret["RetData"].ToString());
            }
            else
            {
                throw new Exception(ret["ErrMsg"].ToString());
            }

          
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="data">DataTable(TableName,Id)</param>
        /// <param name="Client"></param>
        /// <returns></returns>
        public static bool DelData(System.Data.DataTable data, SJeMES_Framework.Class.ClientClass Client)
        {
            SJeMES_Framework.Web.JSONPanelClassHList j = new SJeMES_Framework.Web.JSONPanelClassHList();

            

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.SYS", "DelModuleData", Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
            {


                return Convert.ToBoolean(ret["IsSuccess"].ToString());
            }
            else
            {
                throw new Exception(ret["ErrMsg"].ToString());
            }

        }

        public static Dictionary<string,object> GetListData(string ModuleCode, string TableName,
            string Where,string OrderBy,int Page,int PageRow,string title,
            SJeMES_Framework.Class.ClientClass Client)
        {
            SJeMES_Framework.Web.JSONPanelClassHList j = new SJeMES_Framework.Web.JSONPanelClassHList();

            if(string.IsNullOrEmpty(OrderBy))
            {
                OrderBy = " Order by id desc";
            }

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("title", title);
            data.Add("APP_Code", ModuleCode);
            data.Add("TableName", TableName);
            data.Add("Where", Where);
            data.Add("OrderBy", OrderBy);
            data.Add("Page", Page);
            data.Add("PageRow", PageRow);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.SYS", "GetModuleData", Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
            {


                return Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret["RetData"].ToString());
            }
            else
            {
                //if (string.IsNullOrEmpty(ret["ErrMsg"].ToString()))
                //{
                //    return new Dictionary<string, object>();
                //}
                //else
                //{
                    throw new Exception(ret["ErrMsg"].ToString());
                //}
                
            }

        }

        public static System.Data.DataTable GetHData(string ModuleCode, string TableName,
            string DataId,string title,
            SJeMES_Framework.Class.ClientClass Client)
        {
            SJeMES_Framework.Web.JSONPanelClassHList j = new SJeMES_Framework.Web.JSONPanelClassHList();

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("APP_Code", ModuleCode);
            data.Add("title", title);
            data.Add("TableName", TableName);
            data.Add("Where", " AND id="+DataId);
            data.Add("OrderBy", string.Empty);
            data.Add("Page", "1");
            data.Add("PageRow", "1");
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.SYS", "GetModuleData", Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
            {
                string jsondata = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret["RetData"].ToString())[TableName].ToString();
                string json =JsonReplaceSign(jsondata);
                json = json.Replace(@"\", @"\\");
                return Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(json);
            }
            else
            {
                throw new Exception(ret["ErrMsg"].ToString());
            }

        }

        /// <summary>
        /// json字符串将属性值中的英文双引号变成中文双引号
        /// </summary>
        /// <param name="strJson">json字符串</param>
        /// <returns></returns>
        public static string JsonReplaceSign(string strJson)
        {
            //获取每个字符
            char[] temp = strJson.ToCharArray();
            //获取字符数组长度
            int n = temp.Length;
            //循环整个字符数组
            for (int i = 0; i < n; i++)
            {
                //查找json属性值（:+" ）
                if (temp[i] == ':' && temp[i + 1] == '"')
                {
                    //循环属性值内的字符（：+2 推算到value值）
                    for (int j = i + 2; j < n; j++)
                    {
                        //判断是否是英文双引号
                        if (temp[j] == '"')
                        {
                            //排除json属性的双引号
                            if (temp[j + 1] != ',' && temp[j + 1] != '}')
                            {
                                //替换成中文双引号
                                temp[j] = '”';
                            }
                            else if (temp[j + 1] == ',' || temp[j + 1] == '}')
                            {
                                break;
                            }
                        }
                        //else if (temp[j] == '-')
                        //{
                        //    temp[j] = ' ';
                        //}
                        else if (true)
                        {
                            // 要过虑其他字符，继续添加判断就可以
                        }
                    }
                }
            }
            return new String(temp);
        }
        public static string DocDoSure(string ModuleCode,
            string DataId,bool DoSure,
            SJeMES_Framework.Class.ClientClass Client)
        {
            SJeMES_Framework.Web.JSONPanelClassHList j = new SJeMES_Framework.Web.JSONPanelClassHList();

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("App_Code", ModuleCode);
            data.Add("DoSure", DoSure);
            data.Add("Id", DataId);

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.Logic", "DocDoSure", Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
            {

                return ret["RetData"].ToString();
            }
            else
            {
                throw new Exception(ret["ErrMsg"].ToString());
            }

        }

        public static string DocAudit(string ModuleCode,
            string DataId, bool Audit,
            SJeMES_Framework.Class.ClientClass Client)
        {
            SJeMES_Framework.Web.JSONPanelClassHList j = new SJeMES_Framework.Web.JSONPanelClassHList();

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("App_Code", ModuleCode);
            data.Add("Audit", Audit);
            data.Add("Id", DataId);

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.Logic", "DocAudit", Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
            {

                return ret["RetData"].ToString();
            }
            else
            {
                throw new Exception(ret["ErrMsg"].ToString());
            }

        }


        public static string GetDocStatus(string ModuleCode,
            string DataId,
            SJeMES_Framework.Class.ClientClass Client)
        {
            SJeMES_Framework.Web.JSONPanelClassHList j = new SJeMES_Framework.Web.JSONPanelClassHList();

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("APP_Code", ModuleCode);

            data.Add("Id",DataId);

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.SYS", "GetDocStatus", Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
            {


                return ret["RetData"].ToString();
            }
            else
            {
                throw new Exception(ret["ErrMsg"].ToString());
            }

        }

        public static Dictionary<string,object> GetBData(string ModuleCode,int Seq, string TableName,
            string HeadId,
            SJeMES_Framework.Class.ClientClass Client)
        {
            SJeMES_Framework.Web.JSONPanelClassHList j = new SJeMES_Framework.Web.JSONPanelClassHList();


            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("APP_Code", ModuleCode);
            data.Add("Seq", Seq);
            data.Add("TableName", TableName);
            data.Add("Where", string.Empty);
            data.Add("OrderBy", " Order by id desc ");
            data.Add("Page", "1");
            data.Add("PageRow", string.Empty);
            data.Add("HeadId", HeadId);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.SYS", "GetModuleBodyData", Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
            {
                Dictionary<string, object> retData = new Dictionary<string, object>();
                retData.Add("Data", Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>
                    (Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret["RetData"].ToString())["Data"].ToString()));
                retData.Add("Total", Newtonsoft.Json.JsonConvert.DeserializeObject<int>
                    (Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret["RetData"].ToString())["Total"].ToString()));
                retData.Add("Heads", Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>
                  (Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret["RetData"].ToString())["Heads"].ToString()));
                return retData;
            }
            else
            {
                throw new Exception(ret["ErrMsg"].ToString());
            }

        }

        

        public static List<string> GetDataColumn(string sql, 
            SJeMES_Framework.Class.ClientClass Client)
        {
            SJeMES_Framework.Web.JSONPanelClassHList j = new SJeMES_Framework.Web.JSONPanelClassHList();

            Dictionary<string, object> retData = new Dictionary<string, object>();

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("sql", sql);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "GetDataColumn", Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
            {
                

                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(ret["RetData"].ToString());


            }
            else
            {
                throw new Exception(ret["ErrMsg"].ToString());
            }

        }


        public static string AddHData(string ModuleCode,
            string TableName, Dictionary<string,string> TableData,
            SJeMES_Framework.Class.ClientClass Client)
        {
            SJeMES_Framework.Web.JSONPanelClassHList j = new SJeMES_Framework.Web.JSONPanelClassHList();

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("APP_Code", ModuleCode);
            data.Add("TableName", TableName);

            foreach(string key in TableData.Keys)
            {
                data.Add(key, TableData[key]);
            }
            

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.SYS", "AddModuleData", Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
            {

                return ret["RetData"].ToString();
            }
            else
            {
                throw new Exception(ret["ErrMsg"].ToString());
            }

        }

        public static bool AddBData(string ModuleCode,
            string TableName,string HeadId, Dictionary<string, string> TableData,
            SJeMES_Framework.Class.ClientClass Client)
        {
            SJeMES_Framework.Web.JSONPanelClassHList j = new SJeMES_Framework.Web.JSONPanelClassHList();

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("APP_Code", ModuleCode);
            data.Add("TableName", TableName);
            data.Add("HeadId", HeadId);
            foreach (string key in TableData.Keys)
            {
                data.Add(key, TableData[key]);
            }


            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.SYS", "AddModuleData", Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
            {

                return Convert.ToBoolean(ret["IsSuccess"].ToString());
            }
            else
            {
                throw new Exception(ret["ErrMsg"].ToString());
            }

        }

        public static bool EditHData(string ModuleCode,
            string TableName, Dictionary<string, string> TableData,
            SJeMES_Framework.Class.ClientClass Client)
        {
            SJeMES_Framework.Web.JSONPanelClassHList j = new SJeMES_Framework.Web.JSONPanelClassHList();

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("APP_Code", ModuleCode);
            data.Add("TableName", TableName);

            foreach (string key in TableData.Keys)
            {
                data.Add(key, TableData[key]);
            }


            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Client.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.SYS", "EditModuleData", Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
            {

                return Convert.ToBoolean(ret["IsSuccess"].ToString());
            }
            else
            {
                throw new Exception(ret["ErrMsg"].ToString());
            }

        }

        public static DataTable GetTableHeadsValue(string moduleCode, string table, string headId, ClientClass client)
        {
            SJeMES_Framework.Web.JSONPanelClassHList j = new SJeMES_Framework.Web.JSONPanelClassHList();

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("APP_Code", moduleCode);
            data.Add("TableName", table);
            data.Add("HeadId", headId);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(client.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.SYS", "GetTableHeadsValue", client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
            {
                // string ss = JsonReplaceSign(ret["RetData"].ToString());


                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(ret["RetData"].ToString());

                return dt;
            }
            return null;

        }

    }
}
