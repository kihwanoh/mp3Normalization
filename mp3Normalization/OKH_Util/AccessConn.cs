using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*-- 2010년03월20일 Kim Hee-Sung Add using --*/
using System.Data.OleDb;            // Access use

namespace HS.HS_Util
{
    class HS_AccessConn
    {
        public OleDbConnection gConn;

        public void Connection()
        {
            string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Mode={1};";

            connStr = String.Format(connStr, "Test.accdb", "ReadWrite");

            gConn = new OleDbConnection(connStr);

            try
            {
                gConn.Open();
                HS_Trace.trace(TRACE_TYPE.MSG, methodName, gConn.State.ToString());
            }
            catch (Exception ex)
            {
                // MSG : Unable to connect to any of the specified MySQL host.              -> MYSQL 서버를 찾지 못함
                // MSG : Unknown database 'TEST'                                            -> 데이터베이스를 찾지 못함.
                // MSG : Access denied for user 'root1'@'localhost' (using password: YES)   -> 로그인을 못함

                HS_Trace.trace(TRACE_TYPE.ERR, methodName, ex.Message);

                return;
            }
        }

        public void Diconnect()
        {
            string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            gConn.Close();
            HS_Trace.trace(TRACE_TYPE.MSG, methodName, gConn.State.ToString());
        }
    }
}
