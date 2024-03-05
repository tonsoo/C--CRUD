namespace CRUD.Entities.Database
{
    class Read : AConnectionExecuter
    {
        private Action<Dictionary<string, object>>? LastDisplayCall;
        public Read(Connection connection) : base(connection) { }
        
        public void Execute(string table, string condition, string binds = "")
        {
            string queryToExecute = $"SELECT * FROM {table} {condition}";
            // Utilização de base para garantir que não sera chamado o mesmo metodo da classe Read
            base.Execute(queryToExecute, binds);
        }

        public void Display(Action<Dictionary<string, object>> functionCall)
        {
            if(LastDisplayCall != functionCall)
            {
                LastDisplayCall = functionCall;
            }

            if (!ResultStatus)
            {
                return;
            }

            Result.ForEach(x =>
            {
                functionCall.Invoke(x);
            });
        }

        public void LastDisplay()
        {
            if (LastDisplayCall == null)
            {
                return;
            }

            Display(LastDisplayCall);
        }

        public void ExecuteLastCommand()
        {
            Execute(Command.CommandText);
        }
    }
}
