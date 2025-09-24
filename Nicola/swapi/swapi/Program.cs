using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Numerics;

namespace swapi
{
   internal class Program
    {
        static async Task Main(string[] args)
        {
            // 1.
            var moviesJson = await GetApi("films");
            var moviesResult = JsonSerializer.Deserialize<FilmResult>(moviesJson);
            var movies = moviesResult.results;

            var movieTitle = movies.Where(m => m.title.Length == movies.Max(m2 => m2.title.Length)).Select(r => r.title);
            Console.Write("Le titre le plus long : "); 
            Extension.Write(movieTitle);

            // 2.
            var listOfPeopleJson = await GetApi("people");
            var listOfPeople = JsonSerializer.Deserialize<PeopleResult>(listOfPeopleJson);
            var peoples = listOfPeople.results;

            var groupedPeople = peoples.GroupBy(p => p.name);
            var people = groupedPeople.Where(g => g.Count() == groupedPeople.Max(g => g.Count())).Select(r => r.Key);
            Console.Write("Personnage qui apprait le plus souvent : ");
            Extension.Write(people);

            Console.ReadLine();
        }

       static async Task<string> GetApi(string query)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("https://swapi.dev/api/" + query);
                    response.EnsureSuccessStatusCode();

                    string responseContent = await response.Content.ReadAsStringAsync();    

                    Console.WriteLine(responseContent.ToString());
                    return responseContent;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine(e.ToString());
                    return e.ToString();
                }
            }

        }
       
    }

    class FilmResult
    {
        public int count { get; set; }
        public List<Film> results { get; set; }
    }
    class Film
    {
        public string title { get; set; }
    }

    class PeopleResult
    {
        public int count { get; set; }
        public List<People> results { get; set; }
    }

    class People
    {
        public string name { get; set; }
    }

    public static class Extension
    {
        public static void Write (this IEnumerable<object> target)
        {
            target.ToList().ForEach(item => Console.WriteLine(item));
        }
    }
}