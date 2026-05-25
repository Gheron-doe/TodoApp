using LiteDB;

namespace TodoAppDL
{
    public class DatabaseConnection
    {
        private readonly LiteDatabase _database;

        public DatabaseConnection()
        {
            _database = new LiteDatabase("Todo.db");
        }

        public ILiteCollection<T> GetCollection<T>()
        { return _database.GetCollection<T>(); }
    }

}
