using System;
using System.Collections.Generic;

namespace ReadAllCountries
{
    class Program
    {
        static void Main(string[] args)
        {
			string filePath = @"C:\Pop by Largest Final.csv";
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

			Console.ReadKey();
		}
    }
}
