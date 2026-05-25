using TodoAppBL.Models;

namespace TodoAppBL.Factories
{
    public static class PersonFactory
    {
        public static Person Create(string firstName, string lastName, DateTime birthDate, string? photoUrl = null)
        {
            var now = DateTime.Now;
            return new Person
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate,
                PhotoUrl = photoUrl,
                CreatedAt = now,
                ModifiedAt = now
            };
        }
    }
}
