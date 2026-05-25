using TodoAppBL.Interfaces;
using TodoAppBL.Messages;
using TodoAppBL.Models;

namespace TodoAppBL.Services
{
    public class PersonService(IPersonRepository personRepository, MessageService messageService)
    {
        public MessageService MessageService { get; } = messageService;
        private IPersonRepository _repository { get; } = personRepository;

        public List<Person> GetAll()
        {
            return _repository.GetAll();
        }
        public Person? GetById(string id)
        {
            return _repository.GetById(id);
        }

        public void Add(Person person)
        {
            ArgumentNullException.ThrowIfNull(person);

            var now = DateTime.Now;

            if (_repository.Exists(person)) return;

            person.CreatedAt = now;
            person.ModifiedAt = now;
            _repository.Add(person);
            MessageService.Send(new PersonCreatedMessage(person));
        }

        public void Update(Person person)
        {
            ArgumentNullException.ThrowIfNull(person);

            person.ModifiedAt = DateTime.Now;

            _repository.Update(person);
            MessageService.Send(new PersonUpdatedMessage(person));
        }


        public bool Delete(Person person)
        {
            ArgumentNullException.ThrowIfNull(person);

            if (!_repository.Exists(person)) return false;
            var deleted = _repository.Delete(person);
            if(deleted) MessageService.Send(new PersonDeletedMessage(person.Id));
            return deleted;
        }
    }
}
