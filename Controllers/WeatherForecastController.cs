using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;
using Newtonsoft.Json;

namespace WebIOThub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private const string DeviceConnectionString =
            "HostName=iothubcrud.azure-devices.net;DeviceId=firstDevice;SharedAccessKey=nDSpC8HKB/UP4ZsJT6NZPnyyZjkXvD3yTAIoTDXJGQA=";
        static string connectionString = "HostName=iothubcrud.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=0fERMGbBRP/B9VBjMpBmcwwWzAvEPWoNwAIoTORBOE4=";
        static string deviceId = "secondDevice";
        static RegistryManager registryManager;
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

        //[HttpGet(Name = "GetDevices")]
        //public async Task<List<string>> GetDevices()
        //{

        //    List<string> deviceNames = new List<string>();

        //    RegistryManager registryManager =
        //      RegistryManager.CreateFromConnectionString(connectionString);
        //    try
        //    {

        //        var query = registryManager.CreateQuery("SELECT * FROM devices", 100);
        //        while (query.HasMoreResults)
        //        {
        //            var page = await query.GetNextAsTwinAsync();
        //            foreach (var twin in page)
        //            {
        //                deviceNames.Add(JsonConvert.SerializeObject(twin.DeviceId));
        //            }
        //        }
        //    }
        //    catch (DeviceAlreadyExistsException dvcEx)
        //    {
        //        Console.WriteLine("Error : {0}", dvcEx);
        //    }
        //    return deviceNames;


        //}

        //private static async Task GetDeviceIdAsync()
        //{
        //    RegistryManager registryManager =
        //      RegistryManager.CreateFromConnectionString(connectionString);
        //    try
        //    {

        //        var query = registryManager.CreateQuery("SELECT * FROM devices", 100);
        //        while (query.HasMoreResults)
        //        {
        //            var page = await query.GetNextAsTwinAsync();
        //            foreach (var twin in page)
        //            {
        //                Console.WriteLine(JsonConvert.SerializeObject(twin.DeviceId));
        //            }
        //        }
        //    }
        //    catch (DeviceAlreadyExistsException dvcEx)
        //    {
        //        Console.WriteLine("Error : {0}", dvcEx);
        //    }

        //}
    }
}
