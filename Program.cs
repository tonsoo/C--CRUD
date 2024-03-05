using CRUD.Entities.Database;

namespace CRUD
{
    class Program
    {
        public static void Main()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            Connection connection = new("127.0.0.1", "root", "", "bank");

            //read = new(connection);
            //read.Execute("users", "WHERE salario LIKE @salario", "@salario=1300.%");

            //Create create = new(connection);
            //create.Execute("users", new() { { "nome", "Cleverson" }, { "salario", "4500.00" } });

            //Update update = new(connection);
            //update.Execute("users", new() { { "nome", "Editado!" } }, "WHERE id=@id", "id=3");

            //Delete delete = new(connection);
            //delete.Execute("users", "WHERE nome = @nome", "nome=Teste Create1");

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            Console.WriteLine();
            Console.WriteLine("Execution time: " + elapsedMs + "ms");
        }
    }
}