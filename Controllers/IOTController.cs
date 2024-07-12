using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices.Common.Exceptions;
using Microsoft.Azure.Devices;
using Newtonsoft.Json;

namespace WebIOThub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[ApiExplorerSettings(GroupName = "v2")]
    public class IOTController : ControllerBase
    {
        private const string DeviceConnectionString =
           "HostName=iothubcrud.azure-devices.net;DeviceId=firstDevice;SharedAccessKey=nDSpC8HKB/UP4ZsJT6NZPnyyZjkXvD3yTAIoTDXJGQA=";
        static string connectionString = "HostName=iothubcrud.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=0fERMGbBRP/B9VBjMpBmcwwWzAvEPWoNwAIoTORBOE4=";
        static string deviceId = "secondDevice";
        static RegistryManager registryManager;

        private readonly ILogger<IOTController> _logger;

        public IOTController(ILogger<IOTController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetDevices")]
        public async Task<List<string>> GetDevices()
        {

            List<string> deviceNames = new List<string>();

            RegistryManager registryManager =
              RegistryManager.CreateFromConnectionString(connectionString);
            try
            {

                var query = registryManager.CreateQuery("SELECT * FROM devices", 100);
                while (query.HasMoreResults)
                {
                    var page = await query.GetNextAsTwinAsync();
                    foreach (var twin in page)
                    {
                        deviceNames.Add(JsonConvert.SerializeObject(twin.DeviceId));
                    }
                }
            }
            catch (DeviceAlreadyExistsException dvcEx)
            {
                Console.WriteLine("Error : {0}", dvcEx);
            }
            return deviceNames;


        }
    }
}
