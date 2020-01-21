namespace Csp.EF
{
    public enum SqlType
    {
        SqlServer,
        MySql,
        Oracle
    }

    public class SqlOption
    {
        public SqlType SqlType { get; set; }

        public string SqlServerConnection { get; set; }

        public string MySqlConnection { get; set; }
    }
}
