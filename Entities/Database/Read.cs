namespace CRUD.Entities.Database
{
    class Read : AConnectionExecuter
    {
        private Action<Dictionary<string, string>> LastDisplayCall;
        public Read(Connection connection) : base(connection) { }

        public override void Execute(string query)
        {
            SetCommand(query);

            Execute();
        }

        public void LastDisplay()
        {
            if(LastDisplayCall == null)
            {
                return;
            }

            Display(LastDisplayCall);
        }

        public void ExecuteLastCommand()
        {
            Execute();
        }

        public void Execute(string table, string condition, string? binds = "")
        {
            string finalQuery = $"SELECT * FROM {table} {condition}";

            SetCommand(finalQuery);
            BindValues(binds);

            Execute();
        }

        public void Display(Action<Dictionary<string, string>> functionCall)
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
    }
}
