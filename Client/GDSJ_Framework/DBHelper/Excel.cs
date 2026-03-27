using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;



namespace GDSJ_Framework.DBHelper
{
    /// <summary>
    /// Excel连接器
    /// </summary>
    public class Excel:DataBaseAccess
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public Excel()
        {
            this.Connection = null;
            this.Command = null;
            this.DataAdapter = null;
            this.ConnectionOffline = null;
            this.CommandOffline = null;
            this.DataAdapterOffline = null;
            this.DataReader = null;
            this.DBType = "Excel";
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ConnectionText">连接字符串</param>
        public Excel(string ConnectionText)
        {
            this.ConnectionText = ConnectionText;
            this.Connection = new System.Data.Odbc.OdbcConnection(ConnectionText);
            this.Command = this.Connection.CreateCommand();
            this.DataAdapter = new System.Data.Odbc.OdbcDataAdapter();
            this.ConnectionOffline = new System.Data.Odbc.OdbcConnection(ConnectionText);
            this.CommandOffline = this.ConnectionOffline.CreateCommand();
            this.DataAdapterOffline = new System.Data.Odbc.OdbcDataAdapter();
            this.DataReader = null;
            this.DBType = "Excel";
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DataBasePath">数据库路径</param>
        /// <param name="DataBaseFileName">数据库文件名</param>
        public Excel(string DataBasePath, string DataBaseFileName)
        {
            string DataBaseFile = DataBasePath + @"\" + DataBaseFileName;
            ConnectionText = "Provider=Microsoft.ACE.OLEDB.12.0;Driver={Microsoft Excel Driver (*.xls, *.xlsx, *.xlsm, *.xlsb)};DBQ=" + DataBaseFile + ";";
            this.Connection = new System.Data.Odbc.OdbcConnection(ConnectionText);
            this.Command = this.Connection.CreateCommand();
            this.DataAdapter = new System.Data.Odbc.OdbcDataAdapter();
            this.ConnectionOffline = new System.Data.Odbc.OdbcConnection(ConnectionText);
            this.CommandOffline = this.Connection.CreateCommand();
            this.DataAdapterOffline = new System.Data.Odbc.OdbcDataAdapter();
            this.DataReader = null;
            this.DBType = "Excel";

        } 

        #endregion

        #region GetDateTimeNow
        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取数据库当前时间
        /// </summary>
        public override DateTime GetDateTimeNow()
        {
            return DateTime.Now;
        }
        #endregion
    }
}
