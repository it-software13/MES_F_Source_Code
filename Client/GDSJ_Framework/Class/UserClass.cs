using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDSJ_Framework.Class
{
    public class UserClass
    {
        public string UserCode;
        public string UserName;
        public bool MaxWindow;

        public UserClass(string UserCode, string UserName)
        {
            this.UserCode = UserCode;
            this.UserName = UserName;
        }

        public  Dictionary<string, Dictionary<string, bool>> Permissions = new Dictionary<string, Dictionary<string, bool>>();
    }
}
