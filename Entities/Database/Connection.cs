using MySql.Data.MySqlClient;

namespace CRUD.Entities.Database
{
    class Connection
    {
        public string Server { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Database { get; private set; }

        public MySqlConnection DatabaseConnection { get; private set; } = new MySqlConnection();

        public Connection(string server, string username, string password, string database)
        {
            Server = server;
            Username = username;
            Password = password;
            Database = database;

            DatabaseConnection = RetrieveConnection();
        }

        private MySqlConnection RetrieveConnection()
        {
            MySqlConnection connection = new();

            try
            {
                string connectionString = $"server={Server};database={Database};uid={Username};password={Password};";
                connection.ConnectionString = connectionString;
                connection.Open();
            }
            catch (MySqlException e)
            {
                Console.Write("Database Error: " + e.Message);
            }

            return connection;
        }
    }
}
