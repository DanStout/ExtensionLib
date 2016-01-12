using System.Data;

namespace ExtensionLib
{
    /// <summary>
    /// Extensions for IDbConnection
    /// </summary>
    public static class IDbConnectionExtensions
    {
        ///<summary>Make a command using with the sql and parameters on the given database connection</summary>
        public static IDbCommand MakeCommand(this IDbConnection con, string sql, object parameters = null)
        {
            var cmd = con.CreateCommand();
            cmd.CommandText = sql;

            if (parameters != null)
            {
                foreach (var pair in parameters.GetPropertyNameValuePairs())
                {
                    var param = cmd.CreateParameter();
                    param.ParameterName = pair.Key;
                    param.Value = pair.Value;
                    cmd.Parameters.Add(param);
                }
            }
            return cmd;
        }
    }
}
