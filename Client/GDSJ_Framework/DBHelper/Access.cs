using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;



namespace GDSJ_Framework.DBHelper
{
    /// <summary>
    /// Access数据库连接器
    /// </summary>
    public class Access:DataBaseAccess
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public Access()
        {
            this.Connection = null;
            this.Command = null;
            this.DataAdapter = null;
            this.ConnectionOffline = null;
            this.CommandOffline = null;
            this.DataAdapterOffline = null;
            this.DataReader = null;
            this.DBType = "Access";
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ConnectionText">连接字符串</param>
        public Access(string ConnectionText)
        {
            this.ConnectionText = ConnectionText;
            this.Connection = new System.Data.Odbc.OdbcConnection(ConnectionText);
            this.Command = this.Connection.CreateCommand();
            this.DataAdapter = new System.Data.Odbc.OdbcDataAdapter();
            this.ConnectionOffline = new System.Data.Odbc.OdbcConnection(ConnectionText);
            this.CommandOffline = this.ConnectionOffline.CreateCommand();
            this.DataAdapterOffline = new System.Data.Odbc.OdbcDataAdapter();
            this.DataReader = null;
            this.DBType = "Access";
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DataBasePath">数据库路径</param>
        /// <param name="DataBaseFileName">数据库文件名</param>
        public Access(string DataBasePath,string DataBaseFileName)
        {
            string DataBaseFile = DataBasePath + @"\" + DataBaseFileName;
            ConnectionText = "Provider=Microsoft.ACE.OLEDB.12.0;data source='" + DataBaseFile + "'";
            this.Connection = new System.Data.Odbc.OdbcConnection(ConnectionText);
            this.Command = this.Connection.CreateCommand();
            this.DataAdapter = new System.Data.Odbc.OdbcDataAdapter();
            this.ConnectionOffline = new System.Data.Odbc.OdbcConnection(ConnectionText);
            this.CommandOffline = this.ConnectionOffline.CreateCommand();
            this.DataAdapterOffline = new System.Data.Odbc.OdbcDataAdapter();
            this.DataReader = null;
            this.DBType = "Access";

        } 

        #endregion

        #region GetDateTimeNow
        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取数据库当前时间
        /// </summary>
        public override DateTime GetDateTimeNow()
        {
            DateTime ret = DateTime.Now;
            using (System.Data.Odbc.OdbcConnection con = new System.Data.Odbc.OdbcConnection(this.ConnectionText))
            {
                using (System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand("select now()", con))
                {
                    con.Open();
                    ret = Convert.ToDateTime(cmd.ExecuteScalar());
                    con.Close();
                }
            }

            return ret;
        }
        #endregion
    }
}
