using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace practica_todolist_server.Data
{
    public static class Database
    {
        public static SqlConnection GetConnection(IConfiguration configuration)
        {
            return new SqlConnection(configuration.GetConnectionString("ToDoListDB"));
        }
    }
}