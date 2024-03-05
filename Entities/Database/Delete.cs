namespace CRUD.Entities.Database
{
    class Delete : AConnectionExecuter
    {
        public Delete(Connection connection) : base(connection) { }

        public void Execute(string table, string condition, string conditionBinds = "")
        {
            string queryToExecute = $"DELETE FROM {table} {condition}";
            // Utilização de base para garantir que não sera chamado o mesmo metodo da classe Delete
            base.Execute(queryToExecute, conditionBinds);
        }
    }
}
