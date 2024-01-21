using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private IPeopleService _peopleService;

        public PeopleController([FromKeyedServices("peopleService")] IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        [HttpGet("all")]
        public List<People> GetPeople() => Repository.People;

        //[HttpGet("{id}")]
        //public People Get(int id) => Repository.People.First(p => p.Id == id);

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var people = Repository.People.FirstOrDefault(p => p.Id == id);

            if (people == null)
            {
                return NotFound();
            }
            return Ok(people);
        }

        [HttpPost]
        public IActionResult Add(People person)
        {
            if (_peopleService.Validate(person))
            {
                Repository.People.Add(person);
                return NoContent();
            }
            return BadRequest();
        }

        [HttpGet("search/{search}")]
        public List<People> Get(string search) => Repository.People.Where(p => p.Name.ToUpper().Contains(search.ToUpper())).ToList();
    }
    public class Repository
    {
        public static List<People> People = new List<People>
        {
            new People
            {
                Id = 1, Name = "Martin", BirthDate = new DateTime(2002, 09, 16)
            },
            new People
            {
                Id = 2, Name = "Martina", BirthDate = new DateTime(2003, 02, 12)
            },
            new People
            {
                Id = 3, Name = "Octa", BirthDate = new DateTime(2003, 03, 03)
            },
            new People
            {
                Id = 4, Name = "Raspi", BirthDate = new DateTime(2003, 05, 28)
            },
        };
    }
    public class People
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
