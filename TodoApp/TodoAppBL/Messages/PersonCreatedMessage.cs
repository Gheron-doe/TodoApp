using TodoAppBL.Models;

namespace TodoAppBL.Messages
{
    public class PersonCreatedMessage
    {
        public Person Person { get; }

        public PersonCreatedMessage(Person person)
        {
            Person = person;
        }
    }
}
