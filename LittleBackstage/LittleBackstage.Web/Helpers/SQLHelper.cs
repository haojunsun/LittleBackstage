using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace LittleBackstage.Web.Helpers
{
    public class SqlHelper
    {
        // 超时时间
        private static int Timeout = 1000;
        // 数据库名称
        public const String BaseDb = "LittleBackstageDb";
        //存储过程名称
        public const String UserInfoCURD = "UserInfoCURD";
        // 数据库连接字符串
        private static Dictionary<String, String> ConnStrs = new Dictionary<String, String>();

         //IDictionary<string, object> values = new Dictionary<string, object>();
         //values.Add("@UserName", UserName);      
         //values.Add("@PassWord", passWord);
         //object Scalar = SQLHelper.QueryScalar(SQLHelper.BaseDb, "", CommandType.Text, values);


        /// <summary>
        /// SQLServer操作类(静态构造函数)
        /// </summary>
        static SqlHelper()
        {
            var configs = WebConfigurationManager.ConnectionStrings;
            foreach (ConnectionStringSettings config in configs)
            {
                ConnStrs.Add(config.Name, config.ConnectionString);
            }
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="database">数据库(配置文件内connectionStrings的name)</param>
        /// <returns>数据库连接</returns>
        private static SqlConnection GetConnection(string database)
        {
            if (string.IsNullOrEmpty(database))
            {
                throw new Exception("未设置参数：database");
            }
            if (!ConnStrs.ContainsKey(database))
            {
                throw new Exception("未找到数据库：" + database);
            }
            return new SqlConnection(ConnStrs[database]);
        }

        /// <summary>
        /// 获取SqlCommand
        /// </summary>
        /// <param name="conn">SqlConnection</param>
        /// <param name="transaction">SqlTransaction</param>
        /// <param name="cmdType">CommandType</param>
        /// <param name="sql">SQL</param>
        /// <param name="parms">SqlParameter数组</param>
        /// <returns></returns>
        private static SqlCommand GetCommand(SqlConnection conn, SqlTransaction transaction, CommandType cmdType, string sql, SqlParameter[] parms)
        {
            var cmd = new SqlCommand(sql, conn);
            cmd.CommandType = cmdType;
            cmd.CommandTimeout = Timeout;
            if (transaction != null)
                cmd.Transaction = transaction;
            if (parms != null && parms.Length != 0)
                cmd.Parameters.AddRange(parms);
            return cmd;
        }

        /// <summary>
        /// 查询数据，返回DataTable
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="sql">SQL语句或存储过程名</param>
        /// <param name="parms">参数</param>
        /// <param name="cmdType">查询类型(SQL语句/存储过程名)</param>
        /// <returns>DataTable</returns>
        public static DataTable QueryDataTable(string database, string sql, SqlParameter[] parms, CommandType cmdType)
        {
            if (string.IsNullOrEmpty(database))
            {
                throw new Exception("未设置参数：database");
            }
            if (string.IsNullOrEmpty(sql))
            {
                throw new Exception("未设置参数：sql");
            }

            try
            {
                using (SqlConnection conn = GetConnection(database))
                {
                    conn.Open();

                    using (SqlCommand cmd = GetCommand(conn, null, cmdType, sql, parms))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Text.StringBuilder log = new System.Text.StringBuilder();
                log.Append("查询数据出错：");
                log.Append(ex);
                throw new Exception(log.ToString());
            }
        }

        /// <summary>
        /// 查询数据，返回DataSet
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="sql">SQL语句或存储过程名</param>
        /// <param name="parms">参数</param>
        /// <param name="cmdType">查询类型(SQL语句/存储过程名)</param>
        /// <returns>DataSet</returns>
        public static DataSet QueryDataSet(string database, string sql, SqlParameter[] parms, CommandType cmdType)
        {
            if (string.IsNullOrEmpty(database))
            {
                throw new Exception("未设置参数：database");
            }
            if (string.IsNullOrEmpty(sql))
            {
                throw new Exception("未设置参数：sql");
            }

            try
            {
                using (SqlConnection conn = GetConnection(database))
                {
                    conn.Open();

                    using (SqlCommand cmd = GetCommand(conn, null, cmdType, sql, parms))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            return ds;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Text.StringBuilder log = new System.Text.StringBuilder();
                log.Append("查询数据出错：");
                log.Append(ex);
                throw new Exception(log.ToString());
            }
        }

        /// <summary>
        /// 执行命令获取唯一值(第一行第一列)
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="sql">SQL语句或存储过程名</param>
        /// <param name="parms">参数</param>
        /// <param name="cmdType">查询类型(SQL语句/存储过程名)</param>
        /// <returns>获取值</returns>
        public static object QueryScalar(string database, string sql, SqlParameter[] parms, CommandType cmdType)
        {
            if (string.IsNullOrEmpty(database))
            {
                throw new Exception("未设置参数：database");
            }
            if (string.IsNullOrEmpty(sql))
            {
                throw new Exception("未设置参数：sql");
            }
            try
            {
                using (SqlConnection conn = GetConnection(database))
                {
                    conn.Open();
                    using (SqlCommand cmd = GetCommand(conn, null, cmdType, sql, parms))
                    {
                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Text.StringBuilder log = new System.Text.StringBuilder();
                log.Append("处理出错：");
                log.Append(ex);
                throw new Exception(log.ToString());
            }
        }

        /// <summary>
        /// 执行命令更新数据
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="sql">SQL语句或存储过程名</param>
        /// <param name="parms">参数</param>
        /// <param name="cmdType">查询类型(SQL语句/存储过程名)</param>
        /// <returns>更新的行数</returns>
        public static int Execute(string database, string sql, SqlParameter[] parms, CommandType cmdType)
        {
            if (string.IsNullOrEmpty(database))
            {
                throw new Exception("未设置参数：database");
            }
            if (string.IsNullOrEmpty(sql))
            {
                throw new Exception("未设置参数：sql");
            }

            //返回(增删改)的更新行数
            int count = 0;

            try
            {
                using (SqlConnection conn = GetConnection(database))
                {
                    conn.Open();

                    using (SqlCommand cmd = GetCommand(conn, null, cmdType, sql, parms))
                    {
                        if (cmdType == CommandType.StoredProcedure)
                            cmd.Parameters.AddWithValue("@RETURN_VALUE", "").Direction = ParameterDirection.ReturnValue;

                        count = cmd.ExecuteNonQuery();

                        if (count <= 0)
                            if (cmdType == CommandType.StoredProcedure)
                                count = (int)cmd.Parameters["@RETURN_VALUE"].Value;
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Text.StringBuilder log = new System.Text.StringBuilder();
                log.Append("处理出错：");
                log.Append(ex);
                throw new Exception(log.ToString());
            }
            return count;
        }

        /// <summary>
        /// 执行命令获取唯一值(第一行第一列)
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="sql">SQL语句或存储过程名</param>
        /// <param name="cmdType">查询类型(SQL语句/存储过程名)</param>
        /// <param name="values">参数</param>
        /// <returns>唯一值</returns>
        public static object QueryScalar(string database, string sql, CommandType cmdType, IDictionary<string, object> values)
        {
            SqlParameter[] parms = DicToParams(values);
            return QueryScalar(database, sql, parms, cmdType);
        }

        /// <summary>
        /// 执行存储过程查询数据，返回DataSet
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="sql">SQL语句或存储过程名</param>
        /// <param name="cmdType">查询类型(SQL语句/存储过程名)</param>
        /// <param name="values">参数
        /// <returns>DataSet</returns>
        public static DataSet QueryDataSet(string database, string sql, CommandType cmdType, IDictionary<string, object> values)
        {
            SqlParameter[] parms = DicToParams(values);
            return QueryDataSet(database, sql, parms, cmdType);
        }

        /// <summary>
        /// 查询数据，返回DataTable
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="sql">SQL语句或存储过程名</param>
        /// <param name="cmdType">查询类型(SQL语句/存储过程名)</param>
        /// <param name="values">参数</param>
        /// <returns>DataTable</returns>
        public static DataTable QueryDataTable(string database, string sql, CommandType cmdType, IDictionary<string, object> values)
        {
            SqlParameter[] parms = DicToParams(values);
            return QueryDataTable(database, sql, parms, cmdType);
        }

        /// <summary>
        /// 执行命令更新数据
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="sql">SQL语句或存储过程名</param>
        /// <param name="cmdType">查询类型(SQL语句/存储过程名)</param>
        /// <param name="values">参数</param>
        /// <returns>更新的行数</returns>
        public static int Execute(string database, string sql, CommandType cmdType, IDictionary<string, object> values)
        {
            SqlParameter[] parms = DicToParams(values);
            return Execute(database, sql, parms, cmdType);
        }

        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="type">参数类型</param>
        /// <param name="size">参数大小</param>
        /// <param name="direction">参数方向(输入/输出)</param>
        /// <param name="value">参数值</param>
        /// <returns>新参数对象</returns>
        public static SqlParameter[] DicToParams(IDictionary<string, object> values)
        {
            if (values == null) return null;

            SqlParameter[] parms = new SqlParameter[values.Count];
            int index = 0;
            foreach (KeyValuePair<string, object> kv in values)
            {
                SqlParameter parm = null;
                if (kv.Value == null)
                {
                    parm = new SqlParameter(kv.Key, DBNull.Value);
                }
                else
                {
                    Type t = kv.Value.GetType();
                    parm = new SqlParameter(kv.Key, NetToSql(kv.Value.GetType()));
                    parm.Value = kv.Value;
                }

                parms[index++] = parm;
            }
            return parms;
        }

        /// <summary>
        /// .net类型转换为Sql类型
        /// </summary>
        /// <param name="t">.net类型</param>
        /// <returns>Sql类型</returns>
        public static SqlDbType NetToSql(Type t)
        {
            SqlDbType dbType = SqlDbType.Variant;
            switch (t.Name)
            {
                case "Int16":
                    dbType = SqlDbType.SmallInt;
                    break;
                case "Int32":
                    dbType = SqlDbType.Int;
                    break;
                case "Int64":
                    dbType = SqlDbType.BigInt;
                    break;
                case "Single":
                    dbType = SqlDbType.Real;
                    break;
                case "Decimal":
                    dbType = SqlDbType.Decimal;
                    break;

                case "Byte[]":
                    dbType = SqlDbType.VarBinary;
                    break;
                case "Boolean":
                    dbType = SqlDbType.Bit;
                    break;
                case "String":
                    dbType = SqlDbType.NVarChar;
                    break;
                case "Char[]":
                    dbType = SqlDbType.Char;
                    break;
                case "DateTime":
                    dbType = SqlDbType.DateTime;
                    break;
                case "DateTime2":
                    dbType = SqlDbType.DateTime2;
                    break;
                case "DateTimeOffset":
                    dbType = SqlDbType.DateTimeOffset;
                    break;
                case "TimeSpan":
                    dbType = SqlDbType.Time;
                    break;
                case "Guid":
                    dbType = SqlDbType.UniqueIdentifier;
                    break;
                case "Xml":
                    dbType = SqlDbType.Xml;
                    break;
                case "Object":
                    dbType = SqlDbType.Variant;
                    break;
            }
            return dbType;
        }
    }
}