using TodoAppBL.Models;

namespace TodoAppBL.Messages
{
    public class PersonUpdatedMessage
    {
        public Person Person { get; }

        public PersonUpdatedMessage(Person person)
        {
            Person = person;
        }
    }
}
