using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*-- 2010년02월27일 Kim Hee-Sung Add using --*/
using System.Diagnostics;           // Trace 사용을 위해

namespace HS.HS_Util
{
    /// <summary>
    /// 메시지 출력 방식
    /// ERR     : 에러(Error)
    /// MSG     : 메시지(Message)
    /// </summary>
    public enum TRACE_TYPE { ERR, MSG }

    static class HS_Trace
    {
        /// <summary>
        /// Trace() 를 이용한 MESSAGE 출력
        /// </summary>
        /// <param name="tType">메시지 출력 방식</param>
        /// <param name="funName">함수 이름</param>
        /// <param name="strMsg">메시지</param>
        public static void trace(TRACE_TYPE tType, string funName, object strMsg)
        {
            trace(tType, funName, "{0}", strMsg.ToString());
        }

        /// <summary>
        /// Trace() 를 이용한 MESSAGE 출력
        /// </summary>
        /// <param name="tType">메시지 출력 방식</param>
        /// <param name="funName">함수 이름</param>
        /// <param name="format">메시지</param>
        /// <param name="args">format 메소드에 입력될 값</param>
        public static void trace(TRACE_TYPE tType, string funName, string format, params object[] args)
        {
            int i = 0;
            string strMSG = "";
            string strNTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            foreach (string s in args)
            {
                format = format.Replace("{" + i++ + "}", s);
            }

            strMSG = String.Format("[{0}][{1}][{2}] {3}", strNTime, tType.ToString(), funName, format);

            Trace.WriteLine(strMSG);
        }
    }
}
