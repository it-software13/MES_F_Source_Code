using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_TSM
{
    class Termination_Emp
    {
        
        public static string connection = $@"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS= (COMMUNITY = tcp.world)(PROTOCOL=TCP)(HOST=10.3.3.165)(PORT=1521)))(CONNECT_DATA=(SID = APEDB)));User Id=apctest;Password=apctest;Min Pool Size=10;Max Pool Size=20;Connection Lifetime=60000;Persist Security Info=True;";

        internal DataTable Import_Termination_Emp_List(string ProdMonth)
        {
            OracleConnection con = new OracleConnection(connection);
            DataTable dt = new DataTable();
            con.Open();
            string sql = $@"select e.EMP_NO,
                      e.NAME_E EMP_NAME,
                      e.DEPT_NO,
                      d.name_e DEPT_NAME,
                      GG_0002.GF_CODE_NAME(e.ORG_ID, 'WORK', e.WORK_NO, '1') POSITION
                 from ep_main e, dp_dept d
                where e.dept_no = d.dept_no
                  and e.status = 2 and to_char(e.out_date,'yyyy/MM')= '{ProdMonth}'";

            OracleCommand cmd = new OracleCommand(sql, con);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            return dt;


        }
    }
}
