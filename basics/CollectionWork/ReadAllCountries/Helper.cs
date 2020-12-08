using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAllCountries
{
    class Helper
    {
        public static void ReadAllCountriesFromFile()
        {
            string filePath = @"C:\temp\Pop by Largest Final.csv";
            CsvReader reader = new CsvReader(filePath);

            List<Country> countries = reader.ReadAllCountries();

            // This is the code that inserts and then subsequently removes Lilliput.
            // Comment out the RemoveAt to see the list with Lilliput in it.
            Country lilliput = new Country("Lilliput", "LIL", "Somewhere", 2_000_000);
            int lilliputIndex = countries.FindIndex(x => x.Population < 2_000_000);
            countries.Insert(lilliputIndex, lilliput);
            countries.RemoveAt(lilliputIndex);

            foreach (Country country in countries)
            {
                Console.WriteLine($"{PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
            }
            Console.WriteLine($"{countries.Count} countries");
        }

        public static void SearchCountryByCode()
        {
            string filePath = @"C:\temp\Pop by Largest Final.csv";
            CsvReader reader = new CsvReader(filePath);

            var countries = reader.ReadAllCountriesInDictionary();

            Console.WriteLine("Which country code do you want to look up?");
            string userInput = Console.ReadLine();

            bool countryExists = countries.TryGetValue(userInput, out Country country);
            if (!countryExists)
                Console.WriteLine($"Sorry there is no country with code {userInput}");
            else
                Console.WriteLine($"{country.Name} has population {PopulationFormatter.FormatPopulation(country.Population)}");
        }
    }
}
