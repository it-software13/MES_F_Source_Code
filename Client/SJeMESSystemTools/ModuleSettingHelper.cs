using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMESSystemTools
{
    public class ModuleSettingHelper
    {
        public static SJeMES_Framework.Web.JSONFormClass FormClass = new SJeMES_Framework.Web.JSONFormClass();

        public static System.Data.DataTable GetDataTable(string sql,string Where,string OrderBy,int Page,int PageRow)
        {
            try
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("sql", sql);
                p.Add("where", Where);
                p.Add("orderby", OrderBy);
                p.Add("page", Page);
                p.Add("pagerow", PageRow);
                p.Add("sqlp", string.Empty);
                p.Add("pname", string.Empty);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                        "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "GetDataTable", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {

                    string json = j["RetData"].ToString();

                    return  Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(
                        Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string,object>>(
                        json)["data"].ToString());

                }
                else
                {
                    throw new Exception(j["ErrMsg"].ToString());
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static System.Data.DataTable SYSGetDataTable(string sql, string Where, string OrderBy, int Page, int PageRow)
        {
            try
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("sql", sql);
                p.Add("where", Where);
                p.Add("orderby", OrderBy);
                p.Add("page", Page);
                p.Add("pagerow", PageRow);
                p.Add("sqlp", string.Empty);
                p.Add("pname", string.Empty);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                        "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "SYSGetDataTable", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {

                    string json = j["RetData"].ToString();

                    return Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(
                       Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(
                       json)["data"].ToString());

                }
                else
                {
                    throw new Exception(j["ErrMsg"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void GetFormClass(string ModuleCode)
        {
            try
            {
                string sql = @"
select App_Json from SYSAPP01M
where APP_Code='" + ModuleCode+@"'
";
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("sql", sql);
                p.Add("sqlp", string.Empty);
                p.Add("pname", string.Empty);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "SYSGetString", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {

                    string json = j["RetData"].ToString();

                    ModuleSettingHelper.FormClass = Newtonsoft.Json.JsonConvert.DeserializeObject<SJeMES_Framework.Web.JSONFormClass>(json);

                }
                else
                {
                    throw new Exception(j["ErrMsg"].ToString());
                }

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string GetDataType(SJeMES_Framework.Web.JSONControlB B)
        {
            string ret = "String";

            switch (B.datatype)
            {
                case "input":
                    foreach (SJeMES_Framework.Web.JSONControlHRules rule in B.rules)
                    {
                        if (rule.type == "digits")
                        {
                            ret = "Int";
                        }
                        else if (rule.type == "number")
                        {
                            ret = "Decimal";
                        }
                    }
                    break;
                case "other":
                    ret ="DataSource";
                    break;
                case "date":
                    switch (B.format)
                    {
                        case "ymd":
                            ret ="Date";
                            
                            break;
                        case "ymdHms":
                            ret ="DateTime";
                            break;
                        case "Hms":
                            ret ="Time";

                            break;
                    }
                    break;
            }


            return ret;
        }
    }
}
