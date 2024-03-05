namespace CRUD.Entities.Database
{
    class Create : AConnectionExecuter
    {

        public Create(Connection connection) : base(connection) { }

        public override void Execute(string query)
        {
            SetCommand(query);

            Execute();
        }

        public void Execute(string table, Dictionary<string, string> createValues)
        {
            string finalQuery = "";
            string binds = "";

            try
            {
                int length = createValues.Keys.ToArray().Length;
                string valuesText = "";
                string insertText = "";

                int i = 0;
                foreach (var item in createValues)
                {
                    insertText += $"{item.Key}";
                    valuesText += $"@__Update_Bind__{item.Key}";
                    binds += $"@__Update_Bind__{item.Key}={item.Value}";

                    if (i < length - 1)
                    {
                        insertText += ",";
                        valuesText += ",";
                        binds += "&";
                    }
                    i++;
                }

                insertText = $"({insertText})";
                valuesText = $"({valuesText})";

                finalQuery = $"INSERT INTO {table} {insertText} VALUES {valuesText}";
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Argument error: " + e.Message);
            }

            Console.WriteLine(finalQuery);
            SetCommand(finalQuery);
            BindValues(binds);

            Execute();
        }
    }
}
