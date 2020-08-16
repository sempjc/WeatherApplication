using System.Threading.Tasks;

namespace WeatherApplication
{
    public interface IGetWeatherInfo
    {
        public Task<T> GetWeatherByZipCodeAndCountryAsync<T>(string zipcode, string country);
    }
}
