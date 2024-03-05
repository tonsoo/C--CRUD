using CRUD.Entities.Exceptions;

namespace CRUD.Entities.Database
{
    class Create : AConnectionExecuter
    {

        public Create(Connection connection) : base(connection) { }

        public void Execute(string table, Dictionary<string, object> createValues)
        {
            int valuesLength = createValues.Keys.ToArray().Length;

            if (valuesLength == 0)
            {
                throw new DatabaseBindException("The must be at leat one value to create.");
            }

            string queryToExecute = "";
            string binds = "";

            try
            {
                string valuesText = "";
                string insertText = "";

                foreach (var item in createValues)
                {
                    insertText += $"{item.Key},";
                    valuesText += $"@__Update_Bind__{item.Key},";
                    binds += $"@__Update_Bind__{item.Key}={item.Value}&";
                }

                binds = binds[..^1];

                // Criação da string da query seguindo o padrão
                // "INSERT INTO nome_da_tabela (valor1,valor2,valor3) VALUES ('Valor1','Valor2','Valor3')"
                // Variavel[..^1] para remover o ultimo caractere (,) de cada variavel
                queryToExecute = $"INSERT INTO {table} ({insertText[..^1]}) VALUES ({valuesText[..^1]})";
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Argument error: " + e.Message);
            }

            Execute(queryToExecute, binds);
        }
    }
}
