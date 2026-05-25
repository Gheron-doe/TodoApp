using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAppBL.Interfaces;
using TodoAppBL.Models;

namespace TodoAppDL
{
    public class PersonRepository : IPersonRepository
    {
        private List<Person> _people;
        public List<Person> GetAll()
        {
            return _people;
        }

        public Person? GetById(string personId)
        {
            return _people.FirstOrDefault(x => x.Id == personId);
        }
        public bool Exists(Person person)
        {
            return _people.Contains(person);
        }
        public void Add(Person person)
        {
            _people.Add(person);
        }

        public void Update(Person person)
        {
            int index = _people.FindIndex(p => p.Id == person.Id);
            _people[index] = person;
        }

        public bool Delete(Person person)
        {
            _people.Remove(person);
            return true;
        }
    }
}
