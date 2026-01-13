using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace WebServices
{
    #region Catfacts
    public class CatFacts
    {
        public string fact { get; set; }
        public int length { get; set; }
    }
    #endregion

    #region Jokes
    public class RandomJokes
    {
        public string type { get; set; }
        public string setup { get; set; }
        public string punchline { get; set; }
    }
    #endregion

    internal class Program
    {
        static async Task Main()
        {
            #region Anime Quote Example
            using HttpClient client = new HttpClient();

            string url = "https://api.animechan.io/v1/quotes/random";
            var response = await client.GetStringAsync(url);


            try
            {
                Console.WriteLine("JSON Response: \n_________________________________________________________________________________________________________________");
                Console.WriteLine(response);

                AnimeQuote result = JsonSerializer.Deserialize<AnimeQuote>(response);
                Console.WriteLine("_________________________________________________________________________________________________________________________\n");

                Console.WriteLine("After Deserialization:");
                Console.WriteLine("_________________________________________________________________________________________________________________________");
                Console.WriteLine(value: $"Anime Character Name: {result.data.character.name}");
                Console.WriteLine(value: $"Anime Name: {result.data.anime.name}");
                Console.WriteLine(value: $"Quote: {result.data.content}");
                Console.WriteLine("_________________________________________________________________________________________________________________________\n\n\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while processing Anime Quote.");
                Console.WriteLine(ex.Message);
            }
            #endregion

            #region Cat Fact Example
            using HttpClient clientCat = new HttpClient();

            Console.WriteLine("Fetching a random cat fact:");
            string responseCat = await clientCat.GetStringAsync("https://catfact.ninja/fact");

            CatFacts myFact = JsonSerializer.Deserialize<CatFacts>(responseCat);

            Console.WriteLine("_____________________________________________________ CAT FACT OF THE DAY _______________________________________________");
            Console.WriteLine(myFact.fact);
            Console.WriteLine($"Length of Fact: {myFact.length} characters");
            Console.WriteLine("_________________________________________________________________________________________________________________________\n\n\n");
            #endregion

            #region Joke Example
            using HttpClient clientJoke = new HttpClient();

            Console.WriteLine("Randomized Joke:");
            string responseJoke = await clientJoke.GetStringAsync("https://official-joke-api.appspot.com/random_joke");

            RandomJokes myJoke = JsonSerializer.Deserialize<RandomJokes>(responseJoke);

            Console.WriteLine("__________________________________________________________ JOKE OF THE DAY _______________________________________________");
            Console.WriteLine($"Joke Type: {myJoke.type}");
            Console.WriteLine($"Joke: {myJoke.setup}");
            Console.WriteLine($"Punchline: ${myJoke.punchline}");
            Console.WriteLine("__________________________________________________________________________________________________________________________\n\n\n");
            #endregion

            #region Product Example
            using HttpClient clientProduct = new HttpClient();
            Console.WriteLine("Choose product from 1-20: ");
            string id = Console.ReadLine();
            var urlProduct = "https://fakestoreapi.com/products/" + id;
            try
            {
                // Test API reachability
                string responseProduct = await clientProduct.GetStringAsync(urlProduct);
                Product product = JsonSerializer.Deserialize<Product>(responseProduct);
                Console.WriteLine("_________________________________________ PRODUCT DETAILS __________________________________");
                Console.WriteLine($"Product ID: {product.id}");
                Console.WriteLine($"Title: {product.title}");
                Console.WriteLine($"Price: ${product.price}");
                Console.WriteLine($"Description: {product.description}");
                Console.WriteLine($"Category: {product.category}");
                Console.WriteLine("_____________________________________________________________________________________________\n\n\n");

            }
            catch (Exception ex) {
                Console.WriteLine("API is not reachable ");
                Console.WriteLine($"Error: {ex.Message}");
            }
            #endregion

            #region Async Tasks Example
            Task<string> toastTask = MakeToastAsync();
            Task<string> coffeeTask = MakeCoffeeAsync();

            string toast = await toastTask;
            string coffee = await coffeeTask;

            Console.WriteLine(toast);
            Console.WriteLine(coffee);

            #endregion
        }
        #region Async Methods
        static async Task<string> MakeToastAsync()
        {
            await Task.Delay(3000); // Simulate a delay for making toast
            return "Toast is ready!";

        }
        static async Task<string> MakeCoffeeAsync()
        {
            await Task.Delay(2000); // Simulate a delay for making coffee
            return "Coffee is ready!";
        }
        #endregion
    }

    #region Anime Quote Classes
    class AnimeQuote
    {
        public string status { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string content { get; set; }
        public Character character { get; set; }
        public Anime anime { get; set; }
        public string description { get; set; }
    }

    public class Character
    {
        public string name { get; set; }
        public string image_url { get; set; }
    }

    public class Anime
    {
        public string name { get; set; }
        public string image_url { get; set; }
    }
    #endregion

    #region Product Classes
    public class Product
    {
        public int id { get; set; }
        public string title { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public string category { get; set; }
    }
    #endregion

}
