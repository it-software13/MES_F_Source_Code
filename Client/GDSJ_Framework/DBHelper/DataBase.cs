using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Globalization;
using System.IO;

namespace GDSJ_Framework.DBHelper
{
    public class DataBase
    {
        #region 变量

        private string server;
        private string databasename;
        private string userid;
        private string password;
        private string databasefile;
        private string databasetype;
        private string connectiontext;
        private DataBaseAccess databaseaccess;
        #endregion

        #region 属性

        #region string ConnectionText 数据库连接字符串
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionText { get { return connectiontext; } set { connectiontext = value; UpdateDataBaseAccessInfo(); } }
        #endregion

        #region string Server 数据库服务器
        /// <summary>
        /// 数据库服务器
        /// </summary>
        public string Server { get { return server; } set { server = value; UpdateDataBaseAccessInfo(); } }
        #endregion

        #region string DataBaseName 数据库名称
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DataBaseName { get { return databasename; } set { databasename = value; UpdateDataBaseAccessInfo(); } }
        #endregion

        #region string UserId 数据库用户名称
        /// <summary>
        /// 数据库用户名称
        /// </summary>
        public string UserId { get { return userid; } set { userid = value; UpdateDataBaseAccessInfo(); } }
        #endregion

        #region string Password 数据库用户密码
        /// <summary>
        /// 数据库用户密码
        /// </summary>
        public string Password { get { return password; } set { password = value; UpdateDataBaseAccessInfo(); } }
        #endregion

        #region string DataBaseFile 数据库文件
        /// <summary>
        /// 数据库文件
        /// </summary>
        public string DataBaseFile { get { return databasefile; } set { databasefile = value; UpdateDataBaseAccessInfo(); } }
        #endregion

        #region string DataBaseType 数据库类型
        /// <summary>
        /// 数据库类型
        /// </summary>
        public string DataBaseType
        {
            get
            {
                return databasetype;
            }
            set
            {
                databasetype = value.ToLower();
                UpdateDataBaseAccessInfo();
            }
        }
        #endregion  

        #region DateTime DataBaseDateTime 数据库时间
        /// <summary>
        /// 数据库时间
        /// </summary>
        public DateTime DataBaseDateTime
        {
            get
            {
                return GetDateTimeNow();
            }
           
        }
        #endregion

        #region string DateYMD 数据库日期
        /// <summary>
        /// 数据库日期
        /// </summary>
        public string DateYMD
        {
            get
            {
                return DataBaseDateTime.ToString("yyyy/MM/dd", DateTimeFormatInfo.InvariantInfo);
            }

        }
        #endregion

        #region string TimeHMS 数据库时间
        /// <summary>
        /// 数据库时间
        /// </summary>
        public string TimeHMS
        {
            get
            {
                return DataBaseDateTime.ToString("HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
            }

        }
        #endregion

        #region ConnectionState State 数据库连接状态
        /// <summary>
        /// 数据库连接状态
        /// </summary>
        public ConnectionState State
        {
            get { return this.databaseaccess.Connection.State; }
        } 
        #endregion

        #region IDbConnection Connection 数据库连接器
        /// <summary>
        /// 数据库连接器
        /// </summary>
        public IDbConnection Connection
        {
            get { return this.databaseaccess.Connection; }
        } 
        #endregion

        #region IDataReader DataReader 在线DataReader
        /// <summary>
        /// 在线DataReader
        /// </summary>
        public IDataReader DataReader
        {
            get { return this.databaseaccess.DataReader; }
        }
        #endregion

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataBase()
        {
            this.DataBaseType = string.Empty;
            this.Server = string.Empty;
            this.DataBaseName = string.Empty;
            this.UserId = string.Empty;
            this.Password = string.Empty;
            this.DataBaseFile = string.Empty;
            this.databaseaccess = null;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="XML"></param>
        public DataBase(string XML)
        {

            try
            {
                string DBType = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<DBTYPE>", "</DBTYPE>");
                string Server = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<DBSERVER>", "</DBSERVER>");
                string DBName = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<DBNAME>", "</DBNAME>");
                string USER = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<DBUSER>", "</DBUSER>");
                string PWD = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<DBPASSWORD>", "</DBPASSWORD>");


                string strConfig = GDSJ_Framework.Common.TXTHelper.ReadToEnd(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6) + @"\DBConfig.xml");

                if (string.IsNullOrEmpty(DBType))
                {
                    DBType = "SqlServer";
                }

                if (string.IsNullOrEmpty(Server))
                {
                    Server = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(strConfig, "<SERVER>", "</SERVER>");
                }
                if (string.IsNullOrEmpty(DBName))
                {
                    DBName = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(strConfig, "<DB>", "</DB>");
                }
                if (string.IsNullOrEmpty(USER))
                {
                    USER = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(strConfig, "<USER>", "</USER>");
                }
                if (string.IsNullOrEmpty(PWD))
                {
                    PWD = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(strConfig, "<PWD>", "</PWD>");
                }

                this.databasetype = DBType.ToLower();
                this.server = Server;
                this.databasename = DBName;
                this.userid = USER;
                this.password = PWD;
                this.databasefile = string.Empty;
                this.databaseaccess = null;

                this.UpdateDataBaseAccessInfo();
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DataBaseType">数据库类型</param>
        /// <param name="Server">数据库服务器</param>
        /// <param name="DataBase">数据库名字</param>
        /// <param name="UserId">数据库用户</param>
        /// <param name="Password">数据库用户密码</param>
        /// <param name="DataBaseFile">数据库文件</param>
        public DataBase(string DataBaseType, string Server, string DataBaseName, string UserId, string Password, string DataBaseFile)
        {

            this.databasetype = DataBaseType.ToLower();
            this.server = Server;
            this.databasename = DataBaseName;
            this.userid = UserId;
            this.password = Password;
            this.databasefile = DataBaseFile;
            this.databaseaccess = null;

            this.UpdateDataBaseAccessInfo();

        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DataBaseType">数据库类型</param>
        /// <param name="ConStr">数据库连接</param>
        public DataBase(string DataBaseType,string ConStr)
        {

            this.databasetype = DataBaseType.ToLower();
            this.connectiontext = ConStr;
            this.databaseaccess = null;

            this.UpdateDataBaseAccessInfo();

        }
        
        #endregion

        #region 内部方法

        #region private void UpdateDataBaseAccessInfo() 更新连接字符串信息
        /// <summary>
        /// 更新连接字符串信息
        /// </summary>
        private void UpdateDataBaseAccessInfo()
        {
            switch (databasetype)
            {
                case "sqlserver":
                    if (!string.IsNullOrEmpty(server))
                    {
                        connectiontext = "server=" + server + @";database=" + databasename + @";user id=" + userid + @";pwd=" + password + ";";
                        if (!string.IsNullOrWhiteSpace(connectiontext))
                        {
                            this.databaseaccess = new SqlServer(connectiontext);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(connectiontext))
                        {
                            this.databaseaccess = new SqlServer(connectiontext);
                        }
                    }
                    break;
                case "access":
                    if (!string.IsNullOrWhiteSpace(databasefile))
                    {
                        //conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + databasefile;
                        connectiontext = "Driver={Microsoft Access Driver (*.mdb, *.accdb)};DBQ=" + databasefile;
                        //conStr = "Provider=Microsoft.ACE.OLEDB.12.0;data source='" + databasefile + "'";
                        if (!string.IsNullOrWhiteSpace(connectiontext))
                        {
                            this.databaseaccess = new Access(connectiontext);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(connectiontext))
                        {
                            this.databaseaccess = new Access(connectiontext);
                        }
                    }
                    break;
                case "mysql":
                    if (!string.IsNullOrWhiteSpace(server))
                    {
                        connectiontext = "server=" + server + @";database=" + databasename + @";uid=" + userid + @";pwd=" + password + ";";
                        if (!string.IsNullOrWhiteSpace(connectiontext))
                        {
                            this.databaseaccess = new MySQL(connectiontext);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(connectiontext))
                        {
                            this.databaseaccess = new MySQL(connectiontext);
                        }
                    }
                    
                    break;
                case "excel":
                    if (!string.IsNullOrWhiteSpace(databasefile))
                    {
                        connectiontext = "Driver={Microsoft Excel Driver (*.xls, *.xlsx, *.xlsm, *.xlsb)};DBQ=" + databasefile;
                        if (!string.IsNullOrWhiteSpace(connectiontext))
                        {
                            this.databaseaccess = new Excel(connectiontext);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(connectiontext))
                        {
                            this.databaseaccess = new Excel(connectiontext);
                        }
                    }
                    break;
                case "oracle":
                    if (!string.IsNullOrWhiteSpace(databasename))
                    {
                       // connectiontext = "user id=" + userid + ";data source=" + databasename + ";password=" + password + "";
                        connectiontext = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" + server + ")(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=" + databasename + ")));Persist Security Info=True;User ID=" + userid + ";Password=" + password + ";";
                        if (!string.IsNullOrWhiteSpace(connectiontext))
                        {
                            this.databaseaccess = new Oracle(connectiontext);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(connectiontext))
                        {
                            this.databaseaccess = new Oracle(connectiontext);
                        }
                    }
                    break;
            }
        }
        #endregion     

        #region private DateTime GetDateTime() 获取数据库当前日期时间
        /// <summary>
        /// 获取数据库当前日期时间
        /// </summary>
        /// <returns>日期时间</returns>
        private DateTime GetDateTimeNow()
        {
            return this.databaseaccess.GetDateTimeNow();
        } 
        #endregion

        #endregion

        #region 公开方法

        #region 离线数据库连接器方法

        #region GetScalar

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取第一行第一列对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <returns></returns>
        public virtual object GetScalar(string sql)
        {
            return this.databaseaccess.GetScalar(sql);
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取第一行第一列对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public virtual object GetScalar(string sql, Dictionary<string, object> Parameters)
        {
            return this.databaseaccess.GetScalar(sql, Parameters);
        }

        #endregion

        #region GetDataTable

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，GetDataTable
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string sql)
        {
            return this.databaseaccess.GetDataTable(sql);
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，GetDataTable
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="Parameters">参数</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string sql, Dictionary<string, object> Parameters)
        {

            return this.databaseaccess.GetDataTable(sql, Parameters);
        }

        #endregion

        #region GetDataTableReader

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，GetDataTableReader
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public IDataReader GetDataTableReader(string sql)
        {
            return this.databaseaccess.GetDataTableReader(sql);
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，GetDataTableReader
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public IDataReader GetDataTableReader(string sql, Dictionary<string, object> Parameters)
        {

            return this.databaseaccess.GetDataTableReader(sql, Parameters);
        }

        #endregion

        #region GetString

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，GetString
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public string GetString(string sql)
        {
            return this.databaseaccess.GetString(sql);
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，GetString
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public string GetString(string sql, Dictionary<string, object> Parameters)
        {

            return this.databaseaccess.GetString(sql, Parameters);
        }

        #endregion

        #region GetInt16

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，GetInt16
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public short GetInt16(string sql)
        {
            return this.databaseaccess.GetInt16(sql);
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，GetInt16
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public short GetInt16(string sql, Dictionary<string, object> Parameters)
        {

            return this.databaseaccess.GetInt16(sql, Parameters);
        }

        #endregion

        #region GetInt32

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，GetInt32
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public int GetInt32(string sql)
        {
            return this.databaseaccess.GetInt32(sql);
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，GetInt32
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public int GetInt32(string sql, Dictionary<string, object> Parameters)
        {

            return this.databaseaccess.GetInt32(sql, Parameters);
        }

        #endregion

        #region GetInt64

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，GetInt64
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public long GetInt64(string sql)
        {
            return this.databaseaccess.GetInt64(sql);
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，GetInt64
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public long GetInt64(string sql, Dictionary<string, object> Parameters)
        {

            return this.databaseaccess.GetInt64(sql, Parameters);
        }

        #endregion

        #region GetBool

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，GetBool
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public bool GetBool(string sql)
        {
            return this.databaseaccess.GetBool(sql);
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，GetBool
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public bool GetBool(string sql, Dictionary<string, object> Parameters)
        {

            return this.databaseaccess.GetBool(sql, Parameters);
        }

        #endregion

        #region GetDecimal

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，GetDecimal
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public decimal GetDecimal(string sql)
        {
            return this.databaseaccess.GetDecimal(sql);
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，GetDecimal
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public decimal GetDecimal(string sql, Dictionary<string, object> Parameters)
        {

            return this.databaseaccess.GetDecimal(sql, Parameters);
        }

        #endregion

        #region GetDouble

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，GetDouble
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public double GetDouble(string sql)
        {
            return this.databaseaccess.GetDouble(sql);
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，GetDouble
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public double GetDouble(string sql, Dictionary<string, object> Parameters)
        {

            return this.databaseaccess.GetDouble(sql, Parameters);
        }

        #endregion

        #region GetFloat

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，GetFloat
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public float GetFloat(string sql)
        {
            return this.databaseaccess.GetFloat(sql);
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，GetFloat
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public float GetFloat(string sql, Dictionary<string, object> Parameters)
        {

            return this.databaseaccess.GetFloat(sql, Parameters);
        }

        #endregion

        #region GetChar

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，GetChar
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public char GetChar(string sql)
        {
            return this.databaseaccess.GetChar(sql);
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，GetFloat
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public char GetChar(string sql, Dictionary<string, object> Parameters)
        {

            return this.databaseaccess.GetChar(sql, Parameters);
        }

        #endregion

        #region GetDateTime

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，GetDateTime
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public DateTime GetDateTime(string sql)
        {
            return this.databaseaccess.GetDateTime(sql);
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，GetDateTime
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public DateTime GetDateTime(string sql, Dictionary<string, object> Parameters)
        {

            return this.databaseaccess.GetDateTime(sql, Parameters);
        }

        #endregion

        #region ExecuteNonQueryOffline

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，执行SQL并返回影响行数
        /// </summary>
        /// <param name="sql">命令</param>
        /// <returns></returns>
        public int ExecuteNonQueryOffline(string sql)
        {
            return this.databaseaccess.ExecuteNonQueryOffline(sql);
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，执行SQL并返回影响行数
        /// </summary>
        /// <param name="sql">命令</param>
        /// <param name="Parameters">参数</param>
        public int ExecuteNonQueryOffline(string sql, Dictionary<string, object> Parameters)
        {
            return this.databaseaccess.ExecuteNonQueryOffline(sql, Parameters);
        }

        #endregion

        #region ExecuteProcedureOffline

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，执行存储过程
        /// </summary>
        /// <param name="ProcedureName">存储过程名称</param>
        /// <returns>输出参数</returns>
        public Dictionary<string, object> ExecuteProcedureOffline(string ProcedureName)
        {
            return this.databaseaccess.ExecuteProcedureOffline(ProcedureName);
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，执行存储过程
        /// </summary>
        /// <param name="ProcedureName">存储过程</param>
        /// <returns>DataTable</returns>
        public DataTable ExecuteProcedureOfflineToDataTable(string ProcedureName)
        {

            return this.databaseaccess.ExecuteProcedureOfflineToDataTable(ProcedureName);
        }

        #endregion

        #region AddProcedureParameterOffline

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，添加存储过程参数
        /// </summary>
        /// <param name="ParameterName">参数名</param>
        /// <param name="ParameterValue">参数值</param>
        /// <param name="DbType">对象类型</param>
        /// <param name="Direction">参数类型</param>
        public void AddProcedureParameterOffline(string ParameterName, object ParameterValue, System.Data.DbType DbType, System.Data.ParameterDirection Direction)
        {
            this.databaseaccess.AddProcedureParameterOffline(ParameterName, ParameterValue, DbType, Direction);
        }

        #endregion

        #region ProcedureParameterInitializeOffline

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，初始化存储过程参数
        /// </summary>
        public void ProcedureParameterInitializeOffline()
        {
            this.databaseaccess.ProcedureParameterInitializeOffline();
        }

        #endregion 

        #endregion

        #region 在线数据库连接器方法

        #region ExecuteScalar

        /// <summary>
        /// 获取第一行第一列对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <returns></returns>
        public object ExecuteScalar(string sql)
        {

            return this.databaseaccess.ExecuteScalar(sql);
        }

        /// <summary>
        /// 获取第一行第一列对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public object ExecuteScalar(string sql, Dictionary<string, object> Parameters)
        {
            return this.databaseaccess.ExecuteScalar(sql, Parameters);
        }

        #endregion

        #region ExecuteReader

        /// <summary>
        /// 使用在线连接器获取DataReader,使用完毕后记得用Close
        /// </summary>
        /// <param name="sql">命令</param>
        /// <returns></returns>
        public void ExecuteReader(string sql)
        {
            this.databaseaccess.ExecuteReader(sql);
        }

        /// <summary>
        /// 使用在线连接器获取DataReader,使用完毕后记得用Close
        /// </summary>
        /// <param name="sql">命令</param>
        /// <param name="Parameters">参数</param>
        public void ExecuteReader(string sql, Dictionary<string, object> Parameters)
        {
            this.databaseaccess.ExecuteReader(sql, Parameters);
        }

        #endregion

        #region ExecuteNonQuery

        /// <summary>
        /// 执行SQL并返回影响行数
        /// </summary>
        /// <param name="sql">命令</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql)
        {
            return this.databaseaccess.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 执行SQL并返回影响行数
        /// </summary>
        /// <param name="sql">命令</param>
        /// <param name="Parameters">参数</param>
        public int ExecuteNonQuery(string sql, Dictionary<string, object> Parameters)
        {
            return this.databaseaccess.ExecuteNonQuery(sql, Parameters);
        }

        #endregion

        #region ExecuteProcedure

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="ProcedureName">存储过程名称</param>
        /// <returns>输出参数</returns>
        public Dictionary<string, object> ExecuteProcedure(string ProcedureName)
        {
            return this.databaseaccess.ExecuteProcedure(ProcedureName);
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="ProcedureName">存储过程</param>
        /// <returns>DataTable</returns>
        public DataTable ExecuteProcedureToDataTable(string ProcedureName)
        {

            return this.databaseaccess.ExecuteProcedureToDataTable(ProcedureName);
        }

        #endregion

        #region AddProcedureParameter

        /// <summary>
        /// 添加存储过程参数
        /// </summary>
        /// <param name="ParameterName">参数名</param>
        /// <param name="ParameterValue">参数值</param>
        /// <param name="DbType">对象类型</param>
        /// <param name="Direction">参数类型</param>
        public void AddProcedureParameter(string ParameterName, object ParameterValue, System.Data.DbType DbType, System.Data.ParameterDirection Direction)
        {
            this.databaseaccess.AddProcedureParameter(ParameterName, ParameterValue, DbType, Direction);
        }

        #endregion

        #region ProcedureParameterInitialize

        /// <summary>
        /// 初始化存储过程参数
        /// </summary>
        public void ProcedureParameterInitialize()
        {
            this.databaseaccess.ProcedureParameterInitialize();
        }

        #endregion

        #region BeginTransaction

        /// <summary>
        /// 数据库开始事务
        /// </summary>
        public void BeginTransaction()
        {
            this.databaseaccess.BeginTransaction();
        }

        #endregion

        #region Commit

        /// <summary>
        /// 提交数据库事务
        /// </summary>
        public void Commit()
        {
            this.databaseaccess.Commit();
        }

        #endregion

        #region Rollback

        /// <summary>
        /// 回滚数据库事务
        /// </summary>
        public void Rollback()
        {
            this.databaseaccess.Rollback();
        }

        #endregion

        #region Open

        /// <summary>
        /// 打开在线数据库连接
        /// </summary>
        public void Open()
        {
            this.databaseaccess.Open();
        }

        #endregion

        #region Close

        /// <summary>
        /// 关闭在线数据库连接
        /// </summary>
        public void Close()
        {
            this.databaseaccess.Close();
        }

        #endregion 

        #endregion

        #endregion
    }
}
