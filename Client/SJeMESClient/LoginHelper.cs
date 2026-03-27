using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMESClient
{
    public class LoginHelper
    {
        public static Dictionary<string, SJeMES_Framework.Class.OrgClass> OrgInfos;

        public static List<string> GetLanguagesType()
        {
            List<string> ret = new List<string>();
            try
            { 
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
            }
            catch (Exception ex)
            { 
                throw;
            }

            return ret;
        }

        public static Dictionary<string,string> GetOrg()
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            OrgInfos = new Dictionary<string, SJeMES_Framework.Class.OrgClass>();
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                "SJ_SYSAPI", "SJ_SYSAPI.SYS", "GetOrg", string.Empty, string.Empty);

            var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(j["IsSuccess"].ToString()))
            {
                System.Data.DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(j["RetData"].ToString());
                foreach(System.Data.DataRow dr in dt.Rows)
                {
                    

                    ret.Add(dr["org"].ToString(), dr["orgname"].ToString());
                    SJeMES_Framework.Class.OrgClass Org = new SJeMES_Framework.Class.OrgClass();
                    Org.Org = dr["org"].ToString();
                    Org.OrgName = dr["orgname"].ToString();
                    Org.DBType = dr["dbtype"].ToString();
                    Org.DBServer = dr["dbserver"].ToString();
                    Org.DBName = dr["dbname"].ToString();
                    Org.DBUser = dr["dbuser"].ToString();
                    Org.DBPassword = dr["dbpassword"].ToString();

                    OrgInfos.Add(Org.Org, Org);
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
            data.Add("UserPassword",SJeMES_Framework.Common.Security.MD5(Password.ToLower()));

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.User", "Login", string.Empty, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
            //MessageBox.Show(retdata);

            if (Convert.ToBoolean(j["IsSuccess"].ToString()))
            {
                var j2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(j["RetData"].ToString());

                Program.Client.CompanyCode = CompanyCode;
                Program.Client.CompanyName = CompanyName;
                Program.Client.UserToken = j2["UserToken"].ToString();
                Program.Client.UserCode = User;
                Program.Client.UserName = j2["userName"].ToString();
                Program.Client.UploadUrl = j2["uploadurl"].ToString();
                Program.Client.PicUrl = Program.Client.UploadUrl.ToLower().Replace("/api/commoncall", "");
            }
            else
            {
                //MessageBox.Show(j["ErrMsg"].ToString());
                throw new Exception(j["ErrMsg"].ToString());
            }

            return Convert.ToBoolean(j["IsSuccess"].ToString());
        }

        
    }
}
