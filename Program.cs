using CRUD.Entities.Database;

namespace CRUD
{
    class Program
    {
        public static void Main()
        {
            Connection connection = new("127.0.0.1", "root", "", "bank");

            //Read read = new(connection);
            //read.Execute("SELECT * FROM users");
            //read.Execute("table", "WHERE column=@column_value", "@column_value=123");

            //Create create = new(connection);
            //create.Execute("users", new() { { "nome", "Cleverson" }, { "salario", "4500.00" } });

            //Update update = new(connection);
            //update.Execute("users", new() { { "nome", "Editado!" } }, "WHERE id=@id", "id=3");

            //Delete delete = new(connection);
            //delete.Execute("users", "WHERE nome = @nome", "nome=Teste Create1");

        }
    }
}