using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherApplication
{
    public class OpenWeatherResponse
    {

        [JsonProperty("timezone")]
        public string UTCdiff { get; private set; }

        [JsonProperty("name")]
        public string CityName { get; private set; }

        [JsonProperty("weather")]
        public List<Weather> Weahter { get; private set; }

        [JsonProperty("main")]
        public Main Main { get; private set; }
    }
}
