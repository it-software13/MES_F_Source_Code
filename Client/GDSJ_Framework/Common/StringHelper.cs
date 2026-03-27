using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace GDSJ_Framework.Common
{
    public class StringHelper
    {
        public static string GetXMLFormDictionary(string DllName, string ClassName, string Method, Dictionary<string,string> P)
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
                
                <Data>" + @"
                    " + Data + @"
                </Data>
            </WebServie>
            ";

                ret = XML;

            }
            catch (Exception ex)
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
                    <RetData>00000:" + ex.Message + @"</RetData>
                </Return>
            </WebServie>
            ";
            }

            return ret;
        }

        /// <summary>
        /// 根据标签替换标签中的内容
        /// </summary>
        /// <param name="data">全部数据</param>
        /// <param name="data2">要处理的数据</param>
        /// <param name="NewData">要替换的数据集合</param>
        /// <param name="StartTag">开始标签数据集合</param>
        /// <param name="EndTag">结束标签数据集合</param>
        /// <returns></returns>
        public static string ChangeDataFromTag(string data,string data2,List<string> NewData,List<string> StartTag,List<string> EndTag)
        {
            string ret = data+" ";

            string ret2 = data2+"   ";

            for (int i = 0; i < NewData.Count; i++)
            {
                int s = ret2.LastIndexOf(StartTag[i]);

                int e = ret2.LastIndexOf(EndTag[i]) + EndTag[i].Length;

                string sTmp = ret2.Substring(s, e - s);

                ret2 = ret2.Replace(sTmp, NewData[i]);
            }

            ret=ret.Replace(data2, ret2);

            return ret;
        }

        /// <summary>
        /// 根据标签获取标签中的内容
        /// </summary>
        /// <param name="StartTag">开始标签</param>
        /// <param name="EndTag">结束</param>
        /// <returns></returns>
        public static List<string> GetDataFromTag(string data, string StartTag, string EndTag)
        {
            List<string> ret = new List<string>();
            try
            {
                data = data + "         ";
                while (data.Length > 0)
                {
                    int startIndex = -1;
                    int endIndex = -1;
                    if (data.IndexOf(StartTag) > -1)
                    {
                        startIndex = data.IndexOf(StartTag) + StartTag.Length;
                    }

                    if (startIndex > -1)
                    {
                        if (data.Substring(startIndex).IndexOf(EndTag) > -1)
                        {
                            endIndex = data.Substring(startIndex).IndexOf(EndTag) + EndTag.Length;
                        }
                    }

                    if (startIndex > -1 && endIndex > -1)
                    {
                        string tmp = data.Substring(startIndex);
                        tmp = tmp.Remove(endIndex);
                        tmp = tmp.Replace(EndTag, "");
                        ret.Add(tmp);
                        data = data.Substring(startIndex).Substring(endIndex);

                    }


                    if (startIndex == -1 || endIndex == -1)
                    {
                        data = string.Empty;
                    }

                }
            }
            catch { }

            return ret;
        }


        /// <summary>
        /// 根据标签获第一个取标签中的内容
        /// </summary>
        /// <param name = "StartTag" > 开始标签 </ param >
        /// <param name="EndTag">结束</param>
        /// <returns></returns>
        public static string GetDataFromFirstTag(string data, string StartTag, string EndTag)
        {
            string ret = string.Empty;
            try
            {
                data = data + "         ";
                while (data.Length > 0)
                {
                    int startIndex = -1;
                    int endIndex = -1;
                    if (data.IndexOf(StartTag) > -1)
                    {
                        startIndex = data.IndexOf(StartTag) + StartTag.Length;
                    }

                    if (startIndex > -1)
                    {
                        if (data.Substring(startIndex).IndexOf(EndTag) > -1)
                        {
                            endIndex = data.Substring(startIndex).IndexOf(EndTag) + EndTag.Length;
                        }
                    }

                    if (startIndex > -1 && endIndex > -1)
                    {
                        string tmp = string.Empty;
                        if (data.Substring(startIndex).Length > endIndex)
                        {
                            tmp = data.Substring(startIndex).Remove(endIndex).Replace(EndTag, "");
                        }
                        else
                        {
                            tmp = data.Substring(startIndex).Replace(EndTag, "");
                        }
                        ret = tmp;

                        return ret;
                    }


                    if (startIndex == -1 || endIndex == -1)
                    {
                        data = string.Empty;
                    }

                }
            }
            catch { }

            return ret;
        }

        public static System.Data.DataTable GetDataTableFromXML(string XML)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
           
            string dtXML = GetDataFromFirstTag(XML, "<DataTable>", "</DataTable>");
            string[] s = new string[1];
            s[0] = "<dt@;>";
            string[] cXML = GetDataFromFirstTag(dtXML, "<Columns>", "</Columns>").Split(s,StringSplitOptions.RemoveEmptyEntries);

            foreach(string c in cXML)
            {
                dt.Columns.Add(c.Trim());
            }

            List<string> RowXML = GetDataFromTag(dtXML, "<Row>", "</Row>");

            foreach(string r in RowXML)
            {
                string[] rowdata = r.Split(s, StringSplitOptions.None);

                System.Data.DataRow dr = dt.NewRow();

                for(int i=0;i<rowdata.Length;i++)
                {
                    dr[i] = rowdata[i];
                }

                dt.Rows.Add(dr);
            }



            return dt;
        }


        public static string GetXMLFromDataTable(System.Data.DataTable dt)
        {
            string XML = string.Empty;

            XML += "<DataTable>";

            XML += "<Columns>";
            foreach (System.Data.DataColumn c in dt.Columns)
            {
                XML += c.ColumnName + "<dt@;>";
            }
            XML = XML.Remove(XML.Length - 6);
            XML += "</Columns>";

            foreach(System.Data.DataRow dr in dt.Rows)
            {
                XML += "<Row>";

                foreach (System.Data.DataColumn c in dt.Columns)
                {
                    XML += dr[c.ColumnName].ToString() + "<dt@;>";
                }
                XML = XML.Remove(XML.Length - 6);
                XML += "</Row>";
            }



            XML += "</DataTable>";


            return XML;
        }
    }
}
