using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_ZL_KanBan.Common
{
   public  class ConfigHelper
    {
        public static Dictionary<string, string> GetConfigInfo()
        {
            Dictionary<string, string> Pconfig = new Dictionary<string, string>();
            string configstring = SJeMES_Framework.Common.TXTHelper.ReadToEnd("Config.json");
            int sIndex = configstring.IndexOf("{");
            int eIndex = configstring.IndexOf("}");
            if (!string.IsNullOrEmpty(configstring) && sIndex >= 0 && eIndex >= 0)
            {
                configstring = configstring.Substring(sIndex, eIndex + 1);
                Pconfig = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(configstring);

            }

            return Pconfig;
        }

        public static string GetConfigUrl()
        {

            Dictionary<string, object> data = new Dictionary<string, object>();

            data.Add("org",Program.Client.CompanyCode);
            //data.Add("USERCODE", USERCODE);

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_KanBanAPI",//类库名
                                        "SJ_KanBanAPI.WholeLife",//类名
                                        "GetfrontUrl",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

             return ret.RetData;
        }
    }
}
