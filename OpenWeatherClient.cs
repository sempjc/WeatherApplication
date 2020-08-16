using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherApplication
{
    public class OpenWeatherClient : IGetWeatherInfo
    {
        #region Properties
        private readonly string _ApiKey;
        private const string url = "api.openweathermap.org";
        private const string endpoint = "data/2.5/weather";
        private readonly Uri _BaseUrl;
        #endregion

        #region Constructor
        public OpenWeatherClient(string apiKey)
        {
            _ApiKey = apiKey 
                ?? throw new ArgumentNullException(nameof(apiKey));

            _BaseUrl = new Uri(url);
        }
        #endregion

        #region Private Methods
        private string BuildGetWeatherURlPath(
            string zipcode,
            string country)
        {
            if (zipcode is null
                || country is null) return null;

            var p = new List<string>();

            AddParams(zipcode, ref p);
            
            AddParams(country, ref p);
            
            string pStr = ConvertParamsListToString(p);

            var requestUrl =
                endpoint + $"?q={pStr}&appId={_ApiKey}";

            return requestUrl;
        }

        private string ConvertParamsListToString(List<string> qParams)
        {
            string p = string.Join(",", qParams);
            return p;
        }

        private static void AddParams(string zipcode, ref List<string> qParams)
        {
            if (!string.IsNullOrWhiteSpace(zipcode)) qParams.Add(zipcode);
        }

        private async Task<HttpResponseMessage> GetWeatherFromOpenWeatherAPI(
            string zipcode = null, 
            string country = null)
        {
            using HttpClient hc = new HttpClient
            {
                BaseAddress = _BaseUrl
            };

            var requestUrl = BuildGetWeatherURlPath(zipcode, country);

            return await hc.GetAsync(requestUrl);
        }
        #endregion

        #region Public Methods
        public async Task<OpenWeatherResponse> 
            GetWeatherByZipCodeAndCountryAsync<OpenWeatherResponse>(
            string zipcode,
            string country)
        {
            try
            {
                using (var res = 
                    await GetWeatherFromOpenWeatherAPI(zipcode, country))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        var data = await res.Content.ReadAsStringAsync();

                        return JsonConvert
                            .DeserializeObject<OpenWeatherResponse>(data);
                    }
                }

                return default;
            }
            catch
            {
                return default;
            }
        }
        #endregion
    }
}
