using CRUD.Entities.Exceptions;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;

namespace CRUD.Entities.Database
{
    abstract class AConnectionExecuter
    {
        public MySqlCommand Command { get; protected set; } = new();

        public List<Dictionary<string, object>> Result { get; protected set; } = new();
        public bool ResultStatus { get; protected set; }

        public MySqlConnection Connection { get; private set; }

        public AConnectionExecuter(Connection connection)
        {
            Connection = connection.DatabaseConnection;
        }

        public void Execute(string queryToExecute, string binds = "")
        {
            ResultStatus = false;

            if(queryToExecute == "")
            {
                throw new ArgumentException("Query to be executed cannot be empty");
            }

            SetCommand(queryToExecute);
            if(binds != "")
            {
                BindValues(binds);
            }

            try
            {
                Result = new();

                using MySqlDataReader commandReader = Command.ExecuteReader();
                while (commandReader.Read())
                {
                    Dictionary<string, object> newDatabaseRow = new();

                    for (int i = 0; i < commandReader.FieldCount; i++)
                    {
                        string databaseKey = commandReader.GetName(i);
                        object databaseValue = commandReader.GetValue(i);

                        newDatabaseRow.Add(databaseKey, databaseValue);
                    }

                    Result.Add(newDatabaseRow);
                }

                commandReader.Close();
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

            ResultStatus = true;
        }

        protected void SetCommand(string queryToExecute)
        {
            try
            {
                Command = new MySqlCommand(queryToExecute, Connection);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error defining the query: " + e.Message + e.StackTrace);
            }
        }

        protected void BindValues(string? stringBinds = null)
        {
            try
            {

                if (stringBinds == null)
                {
                    return;
                }

                string[] pairOfBinds = stringBinds.Split("&");
                foreach (string pair in pairOfBinds)
                {
                    string[] keyValueBind = pair.Split("=");
                    if (keyValueBind.Length != 2)
                    {
                        throw new DatabaseBindException($"Bind '{pair}' does not follow key value structure.");
                    }

                    string bindKey = "@" + keyValueBind[0];
                    string bindValue = keyValueBind[1];

                    Command.Parameters.AddWithValue(bindKey, bindValue);
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
        }
    }
}
