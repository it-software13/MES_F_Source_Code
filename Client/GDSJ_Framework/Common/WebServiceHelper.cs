using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace GDSJ_Framework.Common
{
    public class WebServiceHelper
    {
        public static string RunService(string WebServiceUrl,string DllName,string ClassName,string Method, Dictionary<string, string> P)
        {
            string ret = string.Empty;
            string Data = string.Empty;

            foreach (string key in P.Keys)
            {
                Data += "<" + key + ">" + P[key] + "</" + key + ">";
                Data += @"
";
            }

            try
            {
                SJ_WebService.SJ_WebService WS = new SJ_WebService.SJ_WebService();
                WS.Url = WebServiceUrl;

                string XML = string.Empty;
                string IP4 = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();


                string MAC = string.Empty;


                List<string> macs = new List<string>();
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface ni in interfaces)
                {
                    macs.Add(ni.GetPhysicalAddress().ToString());
                }

                if(macs.Count>0)
                {
                    MAC = macs[0];
                }


                XML = @"
            <WebServie>
                <DllName>" + DllName + @"</DllName>
                <ClassName>" + ClassName + @"</ClassName>
                <Method>" + Method + @"</Method>
                <IP4>"+IP4+@"</IP4>
                <MAC>"+MAC+@"</MAC>
                <Data>" + @"
                    " + Data + @"
                </Data>
            </WebServie>
            ";

                ret = WS.RunService(XML).Replace("&lt;","<").Replace("&gt;",">");

            }
            catch(Exception ex)
            {
                ret = @"
            <WebServie>
                <DllName>" + DllName + @"</DllName>
                <ClassName>" + ClassName + @"</ClassName>
                <Method>" + Method + @"</Method>
                <Data>" + @"
                    " + Data + @"
                </Data>
                <Return>
                    <IsSuccess>False</IsSuccess>
                    <RetData>00000:"+ex.Message+@"</RetData>
                </Return>
            </WebServie>
            ";
            }

            return ret;
          
        }

        public static string RunService(string WebServiceUrl, string XML)
        {
            string ret = string.Empty;
            string Data = string.Empty;

           
            try
            {
                SJ_WebService.SJ_WebService WS = new SJ_WebService.SJ_WebService();
                WS.Url = WebServiceUrl;

               
                ret = WS.RunService(XML).Replace("&lt;", "<").Replace("&gt;", ">");

            }
            catch (Exception ex)
            {
                ret = @"
            <WebServie>
                <Return>
                    <IsSuccess>False</IsSuccess>
                    <RetData>00000:" + ex.Message + @"</RetData>
                </Return>
            </WebServie>
            ";
            }

            return ret;

        }

        public static string RunService(Class.OrgClass Org, string WebServiceUrl, string DllName, string ClassName, string Method, Dictionary<string, string> P)
        {
            string ret = string.Empty;
            string Data = string.Empty;

            foreach (string key in P.Keys)
            {
                Data += "<" + key + ">" + P[key] + "</" + key + ">";
                Data += @"
";
            }

            try
            {
                SJ_WebService.SJ_WebService WS = new SJ_WebService.SJ_WebService();
                WS.Url = WebServiceUrl;

                string XML = string.Empty;
                string IP4 = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();


                string MAC = string.Empty;


                List<string> macs = new List<string>();
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface ni in interfaces)
                {
                    macs.Add(ni.GetPhysicalAddress().ToString());
                }

                if (macs.Count > 0)
                {
                    MAC = macs[0];
                }


                XML = @"
            <WebServie>
                <DllName>" + DllName + @"</DllName>
                <ClassName>" + ClassName + @"</ClassName>
                <Method>" + Method + @"</Method>
                <IP4>" + IP4 + @"</IP4>
                <MAC>" + MAC + @"</MAC>
                <DBTYPE>"+Org.DBType+ @"</DBTYPE>
                <DBSERVER>" + Org.DBServer + @"</DBSERVER>
                <DBNAME>" + Org.DBName + @"</DBNAME>
                <DBUSER>" + Org.DBUser + @"</DBUSER>
                <DBPASSWORD>" + Org.DBPassword + @"</DBPASSWORD>
                <Data>" + @"
                    " + Data + @"
                </Data>
            </WebServie>
            ";

                ret = WS.RunService(XML).Replace("&lt;", "<").Replace("&gt;", ">");

            }
            catch (Exception ex)
            {
                ret = @"
            <WebServie>
                <DllName>" + DllName + @"</DllName>
                <ClassName>" + ClassName + @"</ClassName>
                <Method>" + Method + @"</Method>
                <DBTYPE>" + Org.DBType + @"</DBTYPE>
                <DBSERVER>" + Org.DBServer + @"</DBSERVER>
                <DBNAME>" + Org.DBName + @"</DBNAME>
                <DBUSER>" + Org.DBUser + @"</DBUSER>
                <DBPASSWORD>" + Org.DBPassword + @"</DBPASSWORD>
                <Data>" + @"
                    " + Data + @"
                </Data>
                <Return>
                    <IsSuccess>False</IsSuccess>
                    <RetData>00000:" + ex.Message + @"</RetData>
                </Return>
            </WebServie>
            ";
            }

            return ret;

        }


        /// <summary>
        /// 请求Web Service获取DataTable
        /// </summary>
        /// <param name="WebService"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static System.Data.DataTable GetDataTable(string WebService,string sql,Dictionary<string,string> SqlP)
        {
            System.Data.DataTable dt=new System.Data.DataTable();
            try
            {
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("sql", sql);


                string sqlp = "";
                string pname = "<pname>";
                foreach (string key in SqlP.Keys)
                {
                    pname += key + ",";
                }
                pname += "</pname>";
                sqlp += pname;
                foreach (string key in SqlP.Keys)
                {
                    sqlp += "<" + key + @">" + SqlP[key] + @"</" + key + @">";
                }
                sqlp += "";

                p.Add("sqlp", sqlp);

                string xml = RunService(WebService, "SJEMS_API", "SJEMS_API.DataBase", "GetDataTable", p);
                if(Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(xml, "<IsSuccess>", "</IsSuccess>")))
                {
                    string xmlRet = Common.StringHelper.GetDataFromFirstTag(xml, "<RetData>", "</RetData>");
                    dt = Common.StringHelper.GetDataTableFromXML(xmlRet);
                }
            }
            catch(Exception ex)
            {
               
            }

            return dt;
        }

        /// <summary>
        /// 请求Web Service获取DataTable
        /// </summary>
        /// <param name="WebService"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static System.Data.DataTable GetDataTable(Class.OrgClass Org, string WebService, string sql, Dictionary<string, string> SqlP)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("sql", sql);


                string sqlp = "";
                string pname = "<pname>";
                foreach (string key in SqlP.Keys)
                {
                    pname += key + ",";
                }
                pname += "</pname>";
                sqlp += pname;
                foreach (string key in SqlP.Keys)
                {
                    sqlp += "<" + key + @">" + SqlP[key] + @"</" + key + @">";
                }
                sqlp += "";

                p.Add("sqlp", sqlp);

                string xml = RunService(Org,WebService, "SJEMS_API", "SJEMS_API.DataBase", "GetDataTable", p);
                if (Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(xml, "<IsSuccess>", "</IsSuccess>")))
                {
                    string xmlRet = Common.StringHelper.GetDataFromFirstTag(xml, "<RetData>", "</RetData>");
                    dt = Common.StringHelper.GetDataTableFromXML(xmlRet);
                }
            }
            catch (Exception ex)
            {

            }

            return dt;
        }

        /// <summary>
        /// 请求Web Service获取AppXML
        /// </summary>
        /// <param name="WebService"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string GetAppXML(string WebService, string AppCode)
        {
            string ret = string.Empty;
            try
            {
                string sql = @"
SELECT
APP_XML
FROM SJEMSSYS.dbo.SYSAPP01M
WHERE APP_Code = '"+AppCode+@"'
";
                ret = GetString(WebService, sql, new Dictionary<string, string>());
                
               
            }
            catch (Exception ex)
            {

            }

            return ret;
        }

        /// <summary>
        /// 请求Web Service获取AppXML
        /// </summary>
        /// <param name="WebService"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string GetAppXML(Class.OrgClass Org, string WebService, string AppCode)
        {
            string ret = string.Empty;
            try
            {
                string sql = @"
SELECT
APP_XML
FROM SJEMSSYS.dbo.SYSAPP01M
WHERE APP_Code = '" + AppCode + @"'
";
                ret = GetString(Org,WebService, sql, new Dictionary<string, string>());


            }
            catch (Exception ex)
            {

            }

            return ret;
        }


        public static string GetString(Class.OrgClass Org, string WebService, string sql, Dictionary<string, string> SqlP)
        {
            string ret = string.Empty;
            try
            {
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("sql", sql);

                string sqlp = "";
                string pname = "<pname>";
                foreach (string key in SqlP.Keys)
                {
                    pname += key + ",";
                }
                pname += "</pname>";
                sqlp += pname;
                foreach (string key in SqlP.Keys)
                {
                    sqlp += "<" + key + @">" + SqlP[key] + @"</" + key + @">";
                }
                sqlp += "";

                p.Add("sqlp", sqlp);
                string xml = RunService(Org,WebService, "SJEMS_API", "SJEMS_API.DataBase", "GetString", p);
                if (Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(xml, "<IsSuccess>", "</IsSuccess>")))
                {
                    ret = Common.StringHelper.GetDataFromFirstTag(xml, "<RetData>", "</RetData>");
                }
            }
            catch (Exception ex)
            {

            }

            return ret;
        }


        /// <summary>
        /// 请求Web Service获取String
        /// </summary>
        /// <param name="WebService"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string GetString(string WebService, string sql, Dictionary<string, string> SqlP)
        {
            string ret = string.Empty;
            try
            {
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("sql", sql);

                string sqlp = "";
                string pname = "<pname>";
                foreach (string key in SqlP.Keys)
                {
                    pname += key + ",";
                }
                pname += "</pname>";
                sqlp += pname;
                foreach (string key in SqlP.Keys)
                {
                    sqlp += "<" + key + @">" + SqlP[key] + @"</" + key + @">";
                }
                sqlp += "";

                p.Add("sqlp", sqlp);
                string xml = RunService(WebService, "SJEMS_API", "SJEMS_API.DataBase", "GetString", p);
                if (Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(xml, "<IsSuccess>", "</IsSuccess>")))
                {
                    ret = Common.StringHelper.GetDataFromFirstTag(xml, "<RetData>", "</RetData>");
                }
            }
            catch (Exception ex)
            {

            }

            return ret;
        }


        /// <summary>
        /// 请求Web Service执行SQL语句
        /// </summary>
        /// <param name="WebService"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ExecuteNonQuery(Class.OrgClass Org, string WebService, string sql, Dictionary<string, string> SqlP)
        {
            Dictionary<string, object> ret = new Dictionary<string, object>(); ;
            try
            {
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("sql", sql);

                string sqlp = "";
                string pname = "<pname>";
                foreach (string key in SqlP.Keys)
                {
                    pname += key + ",";
                }
                pname += "</pname>";
                sqlp += pname;
                foreach (string key in SqlP.Keys)
                {
                    sqlp += "<" + key + @">" + SqlP[key] + @"</" + key + @">";
                }
                sqlp += "";

                p.Add("sqlp", sqlp);

                string xml = RunService(Org,WebService, "SJEMS_API", "SJEMS_API.DataBase", "ExecuteNonQuery", p);

                ret.Add("IsSuccess", Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(xml, "<IsSuccess>", "</IsSuccess>")));
                ret.Add("RetData", Common.StringHelper.GetDataFromFirstTag(xml, "<RetData>", "</RetData>"));
                ret.Add("RetXml", xml);

            }
            catch (Exception ex)
            {
                ret.Add("IsSuccess", false);
                ret.Add("RetData", ex.Message);

            }

            return ret;
        }

        /// <summary>
        /// 请求Web Service执行SQL语句
        /// </summary>
        /// <param name="WebService"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static Dictionary<string,object> ExecuteNonQuery(string WebService, string sql, Dictionary<string, string> SqlP)
        {
            Dictionary<string, object> ret = new Dictionary<string, object>(); ;
            try
            {
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("sql", sql);

                string sqlp = "";
                string pname = "<pname>";
                foreach (string key in SqlP.Keys)
                {
                    pname += key + ",";
                }
                pname += "</pname>";
                sqlp += pname;
                foreach (string key in SqlP.Keys)
                {
                    sqlp += "<" + key + @">" + SqlP[key] + @"</" + key + @">";
                }
                sqlp += "";

                p.Add("sqlp", sqlp);

                string xml = RunService(WebService, "SJEMS_API", "SJEMS_API.DataBase", "ExecuteNonQuery", p);

                ret.Add("IsSuccess", Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(xml, "<IsSuccess>", "</IsSuccess>")));
                ret.Add("RetData",Common.StringHelper.GetDataFromFirstTag(xml,"<RetData>", "</RetData>"));
                ret.Add("RetXml", xml);

            }
            catch (Exception ex)
            {
                ret.Add("IsSuccess", false);
                ret.Add("RetData", ex.Message);

            }

            return ret;
        }

        /// <summary>
        /// 保存请求记录
        /// </summary>
        /// <param name="DB">数据库</param>
        /// <param name="IP4">请求者IP4地址</param>
        /// <param name="MAC">请求者MAC地址</param>
        /// <param name="DLL">请求的DLL</param>
        /// <param name="Class">请求的类</param>
        /// <param name="Method">请求的方法</param>
        /// <param name="Data">请求的数据</param>
        /// <returns></returns>
        public static string SavePostLog(DBHelper.DataBase DB, string IP4,string MAC,string DLL, string Class, string Method, string Data)
        {
            string guid = Guid.NewGuid().ToString();
            try
            {
                

                string sql = @"
INSERT INTO SJEMSSYS.dbo.SYSLOG01M
([date],[time],[ip4],[mac],[dll],[class],[method],[data],[guid])
VALUES
(@date,@time,@ip4,@mac,@dll,@class,@method,@data,@guid)
";
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                p.Add("@time", DateTime.Now.ToString("HH:mm:ss"));
                p.Add("@ip4", IP4);
                p.Add("@mac", MAC);
                p.Add("@dll", DLL);
                p.Add("@class", Class);
                p.Add("@method", Method);
                p.Add("@data", Data.Replace("\r", "").Replace("\n", "").Replace(" ", ""));
                p.Add("@guid", guid);
                DB.ExecuteNonQueryOffline(sql, p);

            }
            catch(Exception ex) { }
            return guid;

        }


        /// <summary>
        /// 保存返回记录
        /// </summary>
        /// <param name="DB">数据库</param>
        /// <param name="Guid">请求的guid</param>
        /// <param name="IsSuccess">是否成功</param>
        /// <param name="RetData">返回的数据</param>
        /// <returns></returns>
        public static void SaveRetLog(DBHelper.DataBase DB, string Guid,string IsSuccess, string RetData)
        {
            try
            {
                string sql = @"
UPDATE SJEMSSYS.dbo.SYSLOG01M SET
issuccess =@issuccess,
retdata =@retdata
WHERE guid=@guid
";
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("@issuccess", IsSuccess);
                p.Add("@retdata", RetData);
                p.Add("@guid", Guid);
                DB.ExecuteNonQueryOffline(sql, p);
            }
            catch (Exception ex) { }
        }


        /// <summary>
        /// 根据DATA获取记录
        /// </summary>
        /// <param name="DB">数据库</param>
        /// <param name="Guid">请求的guid</param>
        /// <param name="IsSuccess">是否成功</param>
        /// <param name="RetData">返回的数据</param>
        /// <returns>guid</returns>
        public static Dictionary<string,string> GetLog(DBHelper.DataBase DB, string DLL, string Class, string Method, string Data)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            try
            {
                string sql = @"
SELECT guid,retdata FROM SJEMSSYS.dbo.SYSLOG01M 
WHERE data=@data
AND dll =@dll
AND class=@class
AND method=@method
";
                Dictionary<string, object> p = new Dictionary<string, object>();
                
                p.Add("@data", Data.Replace("\r","").Replace("\n","").Replace(" ",""));
                p.Add("@dll", DLL);
                p.Add("@class", Class);
                p.Add("@method", Method);
                System.Data.IDataReader idr = DB.GetDataTableReader(sql, p);

                if(idr.Read())
                {
                    ret.Add("guid", idr[0].ToString());
                    ret.Add("retdata", idr[1].ToString());
                }
            }
            catch (Exception ex) { }

            return ret;
        }
    }
}
