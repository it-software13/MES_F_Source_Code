using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMESSystemTools
{
    public class LoginHelper
    {
        public static List<string> GetLanguagesType()
        {
            List<string> ret = new List<string>();

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                "SJ_SYSAPI", "SJ_SYSAPI.SYS", "GetLanguagesType", string.Empty, string.Empty);

            var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(j["IsSuccess"].ToString()))
            {
                ret = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(j["RetData"].ToString());
            }
            else
            {
                throw new Exception(j["ErrMsg"].ToString());
            }

            return ret;
        }

        public static Dictionary<string,string> GetOrg()
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                "SJ_SYSAPI", "SJ_SYSAPI.SYS", "GetOrg", string.Empty, string.Empty);

            var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(j["IsSuccess"].ToString()))
            {
                System.Data.DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(j["RetData"].ToString());
                foreach(System.Data.DataRow dr in dt.Rows)
                {
                    

                    ret.Add(dr["org"].ToString(), dr["orgname"].ToString());
                }
            }
            else
            {
                throw new Exception(j["ErrMsg"].ToString());
            }

            return ret;
        }

        public static bool Login(string CompanyCode, string CompanyName, string User, string Password)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("CompanyCode", CompanyCode);
            data.Add("CompanyName", CompanyName);
            data.Add("UserCode", User);
            data.Add("UserPassword",SJeMES_Framework.Common.Security.MD5(Password).ToUpper());

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.User", "Login", string.Empty, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(j["IsSuccess"].ToString()))
            {
                var j2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(j["RetData"].ToString());

                Program.Client.CompanyCode = CompanyCode;
                Program.Client.CompanyName = CompanyName;
                Program.Client.UserToken = j2["UserToken"].ToString();
                Program.Client.UserCode = User;
                Program.Client.UserName = j2["userName"].ToString();
            }
            else
            {
                throw new Exception(j["ErrMsg"].ToString());
            }

            return Convert.ToBoolean(j["IsSuccess"].ToString());
        }

        
    }
}
