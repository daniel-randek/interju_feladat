using System;

namespace InterjuFeladat
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            WebApi webApi = new WebApi("https://restcountries.eu/rest/v2/all");
            var countries = await webApi.GetCountries();
            CountryDBContext countryDBContext = new CountryDBContext("Host=localhost;Username=postgres;Password=dsaasd;Database=r_r_feladat");
            countryDBContext.databaseFiller(countries);
        }
    }
}
