using TodoAppBL.Models;

namespace TodoAppBL.Interfaces
{
    public interface IPersonRepository
    {
        List<Person> GetAll();
        Person? GetById(string id);
        bool Exists(Person person);
        void Add(Person person);
        void Update(Person person);
        bool Delete(Person person);
    }
}
