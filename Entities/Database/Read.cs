namespace CRUD.Entities.Database
{
    class Read : AConnectionExecuter
    {

        public Read(Connection connection) : base(connection) { }

        public override void Execute(string query)
        {
            SetCommand(query);

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
