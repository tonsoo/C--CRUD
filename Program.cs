using CRUD.Entities.Database;

namespace CRUD
{
    class Program
    {
        public static void Main()
        {
            /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
             * 
             * Connection
             * 
             * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

            // Conexão instanciada passando os parametros host(servidor), nome de usuario, senha, nome do banco de dados(database)
            Connection connection = new("127.0.0.1", "root", "", "bank");

            /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
             * 
             * Read
             * 
             * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

            Read read = new(connection);
            // Query comum
            read.Execute("SELECT * FROM table_name WHERE column1_name=column1_value");
            // Query com binds
            read.Execute("SELECT * FROM table_name WHERE column1_name=@column1_name", "column1_name=column1_value");
            // Query com binds estilo 2
            read.Execute("table_name", "WHERE column1_name=@column1_name", "column1_name=column1_value");

            // Executa o ultimo comando
            read.ExecuteLastCommand();

            // Faz um simples forEach e chama uma Action com um parametro do tipo Dictionary<string, object>
            read.Display(x => Console.WriteLine(x));

            // Chama a ultima Action passada para a função Display
            read.LastDisplay();

            /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
             * 
             * Create
             * 
             * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

            Create create = new(connection);
            // Query comum
            create.Execute("INSERT INTO table_name (column1_name, column2_name) VALUES (column1_value, column2_value)");
            // Query com binds
            create.Execute("INSERT INTO table_name (column1_name, column2_name) VALUES (@column1_name, @column2_name)", "column1_name=column1_value&column2_name=column2_value");
            // Query com binds estilo 2
            create.Execute("table_name", new() { { "column1_name", "column1_value"}, { "column2_name", "column2_value" } });

            /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
             * 
             * Update
             * 
             * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

            Update update = new(connection);
            // Query comum
            update.Execute("UPDATE table_name SET column1_name=column1_value WHERE column2_name=column2_value");
            // Query com binds
            update.Execute("UPDATE table_name SET column1_name=@column1_name WHERE column2_name=@column2_name", "column1_name=column1_value&column2_name=column2_value");
            // Query com binds estilo 2
            update.Execute("table_name", new() { { "column1_name", "column1_value" } }, "WHERE column2_name=@column2_name", "column2_name=column2_value");

            /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
             * 
             * Delete
             * 
             * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

            Delete delete = new(connection);
            // Query comum
            delete.Execute("DELETE FROM table_name WHERE column1_name=column1_value");
            // Query com binds
            delete.Execute("DELETE FROM table_name WHERE column1_name=@column1_name", "column1_name=column1_value");
            // Query com binds estilo 2
            delete.Execute("table_name", "WHERE column1_name=@column1_name", "column1_name=column1_value");
        }
    }
}