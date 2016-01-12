using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionLib
{
    ///<summary>Possible values for the Number field of a SqlException</summary>
    public enum ErrorCode
    {
        ///<summary>A violation of a unique or primary key constraint</summary>
        DUPLICATE_KEY = 2627
    }

    /// <summary>
    /// Extensions for SqlException
    /// </summary>
    public static class SqlExceptionExtensions
    {
        private static Common.Logging.ILog logger = Common.Logging.LogManager.GetLogger(typeof(SqlExceptionExtensions));

        ///<summary>Returns whether this SqlException was caused by the given ErrorCode (A SQL Server error number)</summary>
        public static bool IsError(this SqlException sqlEx, ErrorCode error)
        {
            var code = sqlEx.Number;
            bool isErrorCode = Enum.IsDefined(typeof(ErrorCode), code);
            if (!isErrorCode) logger.Info(m => m("Code {0} undefined", code));
            return isErrorCode && (ErrorCode)code == error;
        }
    }
}
