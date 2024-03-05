namespace CRUD.Entities.Database
{
    interface IConnectionExecuter
    {
        public void Execute(string query);
    }
}
