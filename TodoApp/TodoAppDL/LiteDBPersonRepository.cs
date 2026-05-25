using LiteDB;
using TodoAppBL.Interfaces;
using TodoAppBL.Models;

namespace TodoAppDL
{
    public class LiteDBPersonRepository : IPersonRepository
    {
        private readonly DatabaseConnection _db;

        public LiteDBPersonRepository(DatabaseConnection db)
        {
            _db = db;
        }

        private ILiteCollection<Person> GetCollection()
        {
            return _db.GetCollection<Person>();
        }
        public List<Person> GetAll()
        {
            return GetCollection().FindAll().ToList();
        }

        public Person? GetById(string personId)
        {
            return GetCollection().FindById(personId);
        }
        public bool Exists(Person person)
        {
            return GetCollection().Exists(x => x.Id == person.Id);
        }
        public void Add(Person person)
        { 
            GetCollection().Insert(person);
        }

        public void Update(Person person)
        {
            GetCollection().Update(person);
        }

        public bool Delete(Person person)
        {
            return GetCollection().Delete(person.Id);
        }
    }
}
