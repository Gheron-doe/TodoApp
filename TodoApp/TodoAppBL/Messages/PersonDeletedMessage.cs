using TodoAppBL.Models;

namespace TodoAppBL.Messages
{
    public class PersonDeletedMessage
    {
        public string PersonId { get; }

        public PersonDeletedMessage(string personId)
        {
            PersonId = personId;
        }
    }
}
