using System;
using System.Data.OracleClient;

namespace GDSJ_Framework.DBHelper
{
    /// <summary>
    /// Oracle数据库连接器
    /// </summary>
    public class Oracle:DataBaseAccess
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public Oracle()
        {
            this.Connection = null;
            this.Command = null;
            this.DataAdapter = null;
            this.ConnectionOffline = null;
            this.CommandOffline = null;
            this.DataAdapterOffline = null;
            this.DataReader = null;
            this.DBType = "Oracle";
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ConnectionText">连接字符串</param>
        public Oracle(string ConnectionText)
        {
            this.ConnectionText = ConnectionText;
            this.Connection = new System.Data.OracleClient.OracleConnection(ConnectionText);

            this.Command = this.Connection.CreateCommand();
            this.Command.CommandTimeout = 900;
            this.DataAdapter = new System.Data.OracleClient.OracleDataAdapter();

            this.ConnectionOffline = new System.Data.OracleClient.OracleConnection(ConnectionText);

            this.CommandOffline = this.ConnectionOffline.CreateCommand();
            this.CommandOffline.CommandTimeout = 900;
            this.DataAdapterOffline = new System.Data.OracleClient.OracleDataAdapter();

            this.DataReader = null;
            this.DBType = "Oracle";


        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DataSource">服务器</param>
        /// <param name="UserId">数据库用户</param>
        /// <param name="Password">用户密码</param>
        public Oracle(string DataSource, string UserId,string Password)
        {
            //ConnectionText = "Provider=OraOLEDB.Oracle.1;user id=" + UserId+";data source="+DataSource+";password="+Password+ "";
            
            //this.Connection = new OleDbConnection(ConnectionText);
            //this.Command = this.Connection.CreateCommand();

            //this.DataAdapter = new OleDbDataAdapter();

            //this.ConnectionOffline = new OleDbConnection(ConnectionText);
            //this.CommandOffline = this.ConnectionOffline.CreateCommand();
 
            //this.DataAdapterOffline = new OleDbDataAdapter();
            //this.DataReader = null;
            //this.DBType = "Oracle";


            ConnectionText = "user id=" + UserId + ";data source=" + DataSource + ";password=" + Password + "";
            this.Connection = new System.Data.OracleClient.OracleConnection(ConnectionText);

            this.Command = this.Connection.CreateCommand();
            this.Command.CommandTimeout = 900;
            this.DataAdapter = new System.Data.OracleClient.OracleDataAdapter();

            this.ConnectionOffline = new System.Data.OracleClient.OracleConnection(ConnectionText);

            this.CommandOffline = this.ConnectionOffline.CreateCommand();
            this.CommandOffline.CommandTimeout = 900;
            this.DataAdapterOffline = new System.Data.OracleClient.OracleDataAdapter();

            this.DataReader = null;
            this.DBType = "Oracle";
        }

        public Oracle(string DataSource, string UserId, string Password, string server, string port)
        {
            //ConnectionText = "Provider=OraOLEDB.Oracle.1;user id=" + UserId+";data source="+DataSource+";password="+Password+ "";

            //this.Connection = new OleDbConnection(ConnectionText);
            //this.Command = this.Connection.CreateCommand();

            //this.DataAdapter = new OleDbDataAdapter();

            //this.ConnectionOffline = new OleDbConnection(ConnectionText);
            //this.CommandOffline = this.ConnectionOffline.CreateCommand();

            //this.DataAdapterOffline = new OleDbDataAdapter();
            //this.DataReader = null;
            //this.DBType = "Oracle";


            ConnectionText = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" + server + ")(PORT=" + port + "))(CONNECT_DATA=(SERVICE_NAME=" + DataSource + ")));Persist Security Info=True;User ID=" + UserId + ";Password=" + Password + ";";
            this.Connection = new OracleConnection(ConnectionText);

            this.Command = this.Connection.CreateCommand();
            this.Command.CommandTimeout = 900;
            this.DataAdapter = new OracleDataAdapter();

            this.ConnectionOffline = new OracleConnection(ConnectionText);

            this.CommandOffline = this.ConnectionOffline.CreateCommand();
            this.CommandOffline.CommandTimeout = 900;
            this.DataAdapterOffline = new OracleDataAdapter();

            this.DataReader = null;
            this.DBType = "Oracle";
        }

        #endregion

        #region GetDateTimeNow
        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取数据库当前时间
        /// </summary>
        public override DateTime GetDateTimeNow()
        {
            DateTime ret = DateTime.Now;
            using (System.Data.OracleClient.OracleConnection con = new System.Data.OracleClient.OracleConnection(this.ConnectionText))
            {
                using (System.Data.OracleClient.OracleCommand cmd = new System.Data.OracleClient.OracleCommand("select sysdate from dual", con))
                {
                    con.Open();
                    ret = Convert.ToDateTime(cmd.ExecuteScalar());
                    con.Close();
                }
            }

            //using (OleDbConnection con = new OleDbConnection(this.ConnectionText))
            //{
            //    using (OleDbCommand cmd = new OleDbCommand("select sysdate()", con))
            //    {
            //        con.Open();
            //        ret = Convert.ToDateTime(cmd.ExecuteScalar());
            //        con.Close();
            //    }
            //}

            return ret;
        }
        #endregion
    }
}
