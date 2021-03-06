﻿using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Csp.Dapper
{
    public class DapperWrite : IDapperWrite
    {
        private readonly IDbConnection _connection;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="connection">连接</param>
        /// <param name="configuration">配置</param>
        public DapperWrite(IDbConnection connection, IConfiguration configuration)
        {
            var connectionStrings = configuration.GetSection("ConnectionStrings").Get<Dictionary<string, string>>();
            _connection = connection;
            _connection.ConnectionString = connectionStrings.Where(s => s.Key.ToLower().Contains("write")).FirstOrDefault().Value;
        }

        /// <summary>
        /// 连接
        /// </summary>
        public IDbConnection Connection
        {
            get
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                return _connection;
            }
        }

        /// <summary>
        /// Execute parameterized SQL.
        /// </summary>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The number of rows affected.</returns>
        public int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.Execute(sql, param, transaction, commandTimeout, commandType);
        }
    }
}
