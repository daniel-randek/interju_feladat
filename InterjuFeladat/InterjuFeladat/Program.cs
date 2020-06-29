using System;

namespace InterjuFeladat
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            //A webapi feladata az adatok letöltése
            WebApi webApi = new WebApi("https://restcountries.eu/rest/v2/all");
            var countries = await webApi.GetCountries();

            //countryDBContext felelős az adatok feltöltéséért, itt kell megváltoztatni a connection stringet
            CountryDBContext countryDBContext = new CountryDBContext("Host=localhost;Username=postgres;Password=dsaasd;Database=r_r_feladat");
            countryDBContext.databaseFiller(countries);
        }
    }
}
