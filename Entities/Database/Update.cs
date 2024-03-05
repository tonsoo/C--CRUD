namespace CRUD.Entities.Database
{
    class Update : AConnectionExecuter
    {
        public Update(Connection connection) : base(connection) { }

        public override void Execute(string query)
        {
            SetCommand(query);

            Execute();
        }

        public void Execute(string table, Dictionary<string, string> updateValues, string condition, string? conditionBinds = "")
        {
            string finalQuery = "";
            string binds = conditionBinds == "" ? "" : $"{conditionBinds}&";

            try
            {
                int length = updateValues.Keys.ToArray().Length;
                string updateText = "";

                int i = 0;
                foreach (var item in updateValues)
                {
                    binds += $"@__Update_Bind__{item.Key}={item.Value}&";
                    updateText += $"{item.Key}=@__Update_Bind__{item.Key}";

                    if (i < length - 1)
                    {
                        updateText += ",";
                    }
                    i++;
                }

                binds = binds[..^1];

                finalQuery = $"UPDATE {table} SET {updateText} {condition}";
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Argument error: " + e.Message);
            }

            SetCommand(finalQuery);
            BindValues(binds);

            Execute();
        }
    }
}
