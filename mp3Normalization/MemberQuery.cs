using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*-- 2010년03월20일 Kim Hee-Sung Add using --*/
using System.Data.OleDb;            // Access use
using OKH_Util;

namespace mp3Normalization
{
    class MemberQuery : AccessConn
    {
        private string strTableName;

        public MemberQuery(string tableName)
        {
            strTableName = tableName;
        }

        public MemberQuery()
        {
            strTableName = "member";
        }

        /// <summary>
        /// Table Create
        /// </summary>
        public void memberTableCreate()
        {
            string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string field = "";
            string tName = strTableName;
            string query = "CREATE TABLE {0} ({1})";

            field += "id  text( 20 )" + ", ";
            field += "pwd text( 20 )";

            query = String.Format(query, tName, field);

            OleDbCommand cmd = new OleDbCommand(query, gConn);

            try
            {
                int iCmdVal = cmd.ExecuteNonQuery();
                Trace.trace(TRACE_TYPE.MSG, methodName, iCmdVal);
            }
            catch (Exception ex)
            {
                Trace.trace(TRACE_TYPE.ERR, methodName, ex.Message);
                return;
            }
        }

        /// <summary>
        /// create Drop
        /// </summary>
        public void memberTablDrop()
        {
            string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string tName = strTableName;
            string query = "DROP TABLE {0}";

            query = String.Format(query, tName);

            OleDbCommand cmd = new OleDbCommand(query, gConn);

            try
            {
                int iCmdVal = cmd.ExecuteNonQuery();
                Trace.trace(TRACE_TYPE.MSG, methodName, iCmdVal);
            }
            catch (Exception ex)
            {
                Trace.trace(TRACE_TYPE.ERR, methodName, ex.Message);
                return;
            }
        }

        /// <summary>
        /// Select
        /// </summary>
        public void memberSelect()
        {
            string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string field = "ID, PWD";
            string tName = strTableName;
            string query = "select {0} from {1}";
            OleDbDataReader reader = null;

            query = String.Format(query, field, tName);

            OleDbCommand cmd = new OleDbCommand(query, gConn);

            try
            {
                reader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                // MSG : There is already an open DataReader associated with this Connection which must be closed first.    -> MySqlDataReader를 닫아주지 않아서 발생된 Error
                //       reader 값을 닫아 주어야 한다.

                Trace.trace(TRACE_TYPE.ERR, methodName, ex.Message);
                return;
            }

            while (reader.Read())
            {
                Trace.trace(TRACE_TYPE.MSG, methodName, reader.GetString(0) + "-" + reader.GetString(1));
            }

            reader.Dispose();
            if (reader != null) { reader = null; }
        }

        /// <summary>
        /// insert
        /// </summary>
        public void memberInsert()
        {
            string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string field = "id, pwd";
            string value = "@id, @pwd";
            string tName = strTableName;
            string query = "INSERT INTO {0} ({1}) VALUES({2})";

            query = String.Format(query, tName, field, value);

            OleDbCommand cmd = new OleDbCommand(query, gConn);

            cmd.Parameters.Add("@id", OleDbType.VarWChar, 20).Value = "aaaa";
            cmd.Parameters.Add("@pwd", OleDbType.VarWChar, 20).Value = "1111";

            try
            {
                int iCmdVal = cmd.ExecuteNonQuery();
                Trace.trace(TRACE_TYPE.MSG, methodName, iCmdVal);
            }
            catch (Exception ex)
            {
                // MSG : Unknown column 'mb_i' in 'field list'                          -> TABLE 에서 Column를 찾지 못함
                // MSG : Column count doesn't match value count at row 1                -> Field 갯수와 Column 갯수가 다름.
                // MSG : You have an error in your SQL syntax; check the manual that corresponds to your MySQL server version for the right 
                //       syntax to use near 'ISERT INTO member (id,pwd)' at line 1      -> 쿼리문이 틀린 경우 발생됨.
                // MSG : Fatal error encountered during command execution.              -> Parameters 값이 부족할때 
                // MSG : Table 'test.member' doesn't exist                              -> 테이블을 찾을 수 없을 때 발생됨.

                Trace.trace(TRACE_TYPE.ERR, methodName, ex.Message);
                return;
            }

        }

        /// <summary>
        /// update
        /// </summary>
        public void memberUpdate()
        {
            string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string tName = strTableName;
            string value = "pwd=@pwd";
            string where = "id=@id";
            string query = "UPDATE {0} set {1} where {2}";

            query = String.Format(query, tName, value, where);

            OleDbCommand cmd = new OleDbCommand(query, gConn);

            cmd.Parameters.Add("@pwd", OleDbType.VarWChar, 4).Value = "3333";
            cmd.Parameters.Add("@id", OleDbType.VarWChar, 4).Value = "aaaa";
            
            try
            {
                int iCmdVal = cmd.ExecuteNonQuery();
                Trace.trace(TRACE_TYPE.MSG, methodName, iCmdVal);
            }
            catch (Exception ex)
            {
                Trace.trace(TRACE_TYPE.ERR, methodName, ex.Message);
                return;
            }
        }

        /// <summary>
        /// delete
        /// </summary>
        public void memberDelete()
        {
            string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string tName = strTableName;
            string where = "id=@id";
            string query = "DELETE FROM {0} where {1}";

            query = String.Format(query, tName, where);

            OleDbCommand cmd = new OleDbCommand(query, gConn);

            cmd.Parameters.Add("@id", OleDbType.VarWChar, 20).Value = "aaaa";

            try
            {
                int iCmdVal = cmd.ExecuteNonQuery();
                Trace.trace(TRACE_TYPE.MSG, methodName, iCmdVal);
            }
            catch (Exception ex)
            {
                Trace.trace(TRACE_TYPE.ERR, methodName, ex.Message);
                return;
            }
        }
    }
}
