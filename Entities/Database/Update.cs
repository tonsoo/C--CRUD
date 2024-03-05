using CRUD.Entities.Exceptions;

namespace CRUD.Entities.Database
{
    class Update : AConnectionExecuter
    {
        public Update(Connection connection) : base(connection) { }

        public void Execute(string table, Dictionary<string, object> updateValues, string condition, string conditionBinds = "")
        {
            int valuesLength = updateValues.Keys.ToArray().Length;

            if(valuesLength == 0)
            {
                throw new DatabaseBindException("The must be at leat one value to update.");
            }

            string queryToExecute = "";

            // Define binds como "chaveParaBind&" caso conditionBinds não seja uma string vazia
            string binds = conditionBinds == "" ? "" : $"{conditionBinds}&";

            try
            {
                string updateText = "";

                foreach (var item in updateValues)
                {
                    binds += $"@__Update_Bind__{item.Key}={item.Value}&";
                    updateText += $"{item.Key}=@__Update_Bind__{item.Key},";
                }

                // Remover ultimo "&" ou "," da string de bind ou update
                binds = binds[..^1];
                updateText = updateText[..^1];

                queryToExecute = $"UPDATE {table} SET {updateText} {condition}";
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Argument error: " + e.Message);
            }

            Execute(queryToExecute, binds);
        }
    }
}
