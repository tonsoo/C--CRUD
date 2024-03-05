namespace CRUD.Entities.Exceptions
{
    class DatabaseBindException : Exception
    {
        public DatabaseBindException(string? message = "") : base(message) { }
    }
}
