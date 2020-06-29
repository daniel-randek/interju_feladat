using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterjuFeladat
{
    class CountryDBContext
    {
        string cs;

        public CountryDBContext(String connections)
        {
            this.cs = connections;
        }

        //A databaseFiller a deszerializált adatokat feltölti az adott connectionstring-en elérhető adatbázisba
        public void databaseFiller(List<Country> data)
        {
            using var con = new NpgsqlConnection(cs);
            con.Open();
            var sql = "SELECT version()";
            
            using var cmd = new NpgsqlCommand(sql, con);
            var version = cmd.ExecuteScalar().ToString();
            Console.WriteLine($"PostgreSQL version: {version}");

            try
            {
                cmd.CommandText = "DROP TABLE IF EXISTS Countries";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE Countries(id SERIAL PRIMARY KEY, 
                    Name VARCHAR(255),
                    Capital VARCHAR(255),
                    Region VARCHAR(255),
                    Population int8,
                    TopLevelDomain VARCHAR(10),
                    Area float4,
                    Subregion VARCHAR(255),
                    NativeName VARCHAR(255))";
                cmd.ExecuteNonQuery();

                foreach (var country in data)
                {
                    cmd.CommandText = "INSERT INTO Countries(Name, Capital, Region, Population, TopLevelDomain, Area, Subregion, NativeName) VALUES(@name, @capital, @region, @population, @topLevelDomain, @area, @subregion, @nativeName)";
                    cmd.Parameters.AddWithValue("name", country.name);
                    cmd.Parameters.AddWithValue("capital", country.capital);
                    cmd.Parameters.AddWithValue("region", country.region);
                    cmd.Parameters.AddWithValue("population", country.population);
                    cmd.Parameters.AddWithValue("topLevelDomain", country.topLevelDomain);
                    cmd.Parameters.AddWithValue("area", (country.area != null) ? country.area : 0);
                    cmd.Parameters.AddWithValue("subregion", country.subregion);
                    cmd.Parameters.AddWithValue("nativeName", country.nativeName);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }

                cmd.CommandText = "SELECT COUNT(*)FROM Countries";
                Int64 count = (Int64)cmd.ExecuteScalar();
                Console.Write("{0}\n", count);
                con.Close();
            }
            catch(Exception e)
            {
                con.Close();
                Console.WriteLine(e);
            }
        }
    }
}
