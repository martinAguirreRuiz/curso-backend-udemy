using Backend.Controllers;

namespace Backend.Services
{
    public class People2Service : IPeopleService
    {
        public bool Validate(People person)
        {
            if (string.IsNullOrEmpty(person.Name) || person.Name.Length < 3)
            {
                return false;
            }
            return true;
        }
    }
}
