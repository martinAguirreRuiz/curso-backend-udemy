using Backend.Controllers;

namespace Backend.Services
{
    public interface IPeopleService
    {
        public bool Validate(People person);
    }
}
