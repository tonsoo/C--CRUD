using CRUD.Entities.Exceptions;
using MySql.Data.MySqlClient;

namespace CRUD.Entities.Database
{
    abstract class AConnectionExecuter : IConnectionExecuter
    {
        public MySqlCommand Command { get; protected set; } = new();
        public List<Dictionary<string, string>> Result { get; protected set; } = new();
        public bool ResultStatus { get; protected set; }

        public MySqlConnection Connection { get; private set; }

        public AConnectionExecuter(Connection connection)
        {
            Connection = connection.DatabaseConnection;
        }

        public abstract void Execute(string query);

        protected void Execute()
        {
            try
            {
                MySqlDataReader reader = Command.ExecuteReader();

                while (reader.Read())
                {
                    Dictionary<string, string> newRow = new();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        newRow.Add(reader.GetName(i), $"{reader.GetValue(i)}");
                    }

                    Result.Add(newRow);
                }

                reader.Close();

                ResultStatus = true;
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine("Invalid casting: " + e.Message);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Invalid Database Connection: " + e.Message);
            }
            catch (MySqlException e)
            {
                Console.WriteLine("MySql Error: " + e.Message);
            }
        }

        protected void SetCommand(string query)
        {
            try
            {
                Command = new MySqlCommand(query, Connection);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error defining the query: " + e.Message + e.StackTrace);
            }
        }

        protected void BindValues(string? binds = null)
        {
            try
            {

                if (binds == null)
                {
                    return;
                }

                List<MySqlParameter> parameters = new();

                string[] queryBinds = binds.Split("&");
                foreach (string bind in queryBinds)
                {
                    string[] bindValues = bind.Split("=");
                    if (bindValues.Length != 2)
                    {
                        throw new DatabaseBindException("Bind does not follow key value structure.");
                    }

                    string key = "@" + bindValues[0];
                    string observeValue = bindValues[1];

                    Command.Parameters.AddWithValue(key, observeValue);
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("MySql Error: " + e.Message);
            }
            catch (DatabaseBindException e)
            {
                Console.WriteLine("Bind error: " + e.Message);
            }

            return;
        }
    }
}
