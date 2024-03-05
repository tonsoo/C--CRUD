namespace CRUD.Entities.Database
{
    class Delete : AConnectionExecuter
    {
        public Delete(Connection connection) : base(connection) { }

        public override void Execute(string query)
        {
            SetCommand(query);

            Execute();
        }

        public void Execute(string table, string condition, string? conditionBinds = "")
        {
            string finalQuery = $"DELETE FROM {table} {condition}";

            SetCommand(finalQuery);
            BindValues(conditionBinds);

            Execute();
        }
    }
}
