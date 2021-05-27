using System;
using System.Collections.Generic;
using System.Data;

namespace Csp.Dapper
{
    /// <summary>
    /// 只读接口
    /// </summary>
    public interface IDapperRead
    {
        /// <summary>
        /// 连接
        /// </summary>
        public IDbConnection Connection
        {
            get;
        }
        /// <summary>
        /// Executes a single-row query, returning the data typed as type.
        /// </summary>
        /// <param name="type">The type to return.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="buffered">Whether to buffer results in memory.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>A sequence of data of the supplied type; if a basic type(int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is created per row, and a direct column-name===member-name mapping is assumed(case insensitive).
        /// 异常:T:System.ArgumentNullException:type is null.
        /// </returns>
        IEnumerable<object> Query(Type type, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);
        

    }
}
