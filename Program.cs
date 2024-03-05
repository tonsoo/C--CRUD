using CRUD.Entities.Database;

namespace CRUD
{
    class Program
    {
        public static void Main()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            Connection connection = new("127.0.0.1", "root", "", "bank");
            Read read = new(connection);

            //read.Execute("SELECT * FROM users");
            //read.Execute("users", "WHERE salario LIKE @salario", "@salario=1300.%");

            //Create create = new(connection);
            //create.Execute("users", new() { { "nome", "Cleverson" }, { "salario", "4500.00" } });
            //Console.WriteLine(create.ResultStatus);

            //Update update = new(connection);
            //update.Execute("users", new() { { "nome", "Editado!" } }, "WHERE id=@id", "id=3");

            read.Execute("SELECT * FROM users ORDER BY id ASC");

            ConsoleColor defaultColor = Console.ForegroundColor;
            read.Display(x =>
            {
                foreach(var item in x)
                {
                    Console.Write($"{item.Key}: {item.Value}; ");
                }

                Console.WriteLine();
                Console.ForegroundColor = Console.ForegroundColor == defaultColor ? ConsoleColor.DarkGray : defaultColor;
            });
            Console.ForegroundColor = defaultColor;

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            Console.WriteLine();
            Console.WriteLine("Execution time: " + elapsedMs + "ms");
        }
    }
}