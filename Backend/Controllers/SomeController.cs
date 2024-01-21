using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeController : ControllerBase
    {
        [HttpGet("sync")]
        public IActionResult Get()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();

            Thread.Sleep(1000);
            Console.WriteLine("Conexión a DB terminada");

            Thread.Sleep(1000);
            Console.WriteLine("Envío de mail realizado");

            Console.WriteLine("Todo terminó");

            stopwatch.Stop();

            return Ok(stopwatch.Elapsed);
        }

        [HttpGet("async")]
        public async Task<IActionResult> GetAsync()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var task1 = new Task(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Conexión a DB terminada");
                
            });

            var task2 = new Task(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Envío de mail terminado");

            });

            task1.Start();
            task2.Start();

            Console.WriteLine("Hago otra cosa");
            Console.WriteLine("Hago otra cosa más");
            Console.WriteLine("Hago otra cosa más todavía");

            await task1;
            await task2;

            Console.WriteLine("Todo terminó");

            stopwatch.Stop();

            return Ok(stopwatch.Elapsed);
        }
    }
}
