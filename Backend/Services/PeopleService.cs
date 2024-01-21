using Backend.Controllers;

namespace Backend.Services
{
    public class PeopleService : IPeopleService
    {
        public bool Validate(People person)
        {
            if (string.IsNullOrEmpty(person.Name))
            {
                return false;
            }
            return true;
        }
    }
}
