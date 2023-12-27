using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace TestApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public void Create([FromForm] ExamCreateDTO dto)
        {
            var httpRequestForm = HttpContext.Request.Form;
            var createDto = dto;
        }
    }

    public class ExamCreateDTO : NamedObjectDTO
    {

        public ExamMode Mode { get; set; }      //Online, Offline

        public string? Process { get; set; } //Free text

        public IEnumerable<ReservationQuota> ReservationQuota { get; set; }


        public IEnumerable<string>? SamplePapersDisplayNames { get; set; }
        public IEnumerable<IFormFile>? SamplePapersFormFiles { get; set; }
    }

    public class NamedObjectDTO
    {
        public uint Id { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        public NamedObjectDTO()
        {
        }
    }

    public enum ExamMode
    {
        Offline = 1,
        Online
    }

    public class ReservationQuota
    {
        public string Category { get; set; }
        public short Reservation { get; set; }
    }
}
