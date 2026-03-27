using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMESClient
{
    public class MenuHelper
    {
        public static bool GetMenu()
        {

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.Menu", "GetSYSMenu", Program.Client.UserToken, string.Empty);

            var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(j["IsSuccess"].ToString()))
            {
               

                Program.Menus = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SJeMES_Framework.Web.JSONMenu>>(j["RetData"].ToString());
            }
            else
            {
                throw new Exception(j["ErrMsg"].ToString());
            }

            return Convert.ToBoolean(j["IsSuccess"].ToString());
        }
    }
}
