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
                Console.WriteLine("JSON Response: \n________________________________________________________________________________________________________________________");
                Console.WriteLine(response);

                AnimeQuote result = JsonSerializer.Deserialize<AnimeQuote>(response);
                Console.WriteLine("________________________________________________________________________________________________________________________\n");

                Console.WriteLine("After Deserialization:");
                Console.WriteLine("________________________________________________________________________________________________________________________");
                Console.WriteLine(value: $"Anime Character Name: {result.data.character.name}");
                Console.WriteLine(value: $"Anime Name: {result.data.anime.name}");
                Console.WriteLine(value: $"Quote: {result.data.content}");
                Console.WriteLine("________________________________________________________________________________________________________________________\n\n\n");
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

            Console.WriteLine("_____________________________________________________ CAT FACT OF THE DAY ______________________________________________");
            Console.WriteLine(myFact.fact);
            Console.WriteLine($"Length of Fact: {myFact.length} characters");
            Console.WriteLine("________________________________________________________________________________________________________________________\n\n\n");
            #endregion

            #region Joke Example
            using HttpClient clientJoke = new HttpClient();

            Console.WriteLine("Randomized Joke:");
            string responseJoke = await clientJoke.GetStringAsync("https://official-joke-api.appspot.com/random_joke");

            RandomJokes myJoke = JsonSerializer.Deserialize<RandomJokes>(responseJoke);

            Console.WriteLine("_________________________________________________________ JOKE OF THE DAY ______________________________________________");
            Console.WriteLine($"Joke Type: {myJoke.type}");
            Console.WriteLine($"Joke: {myJoke.setup}");
            Console.WriteLine($"Punchline: 2{myJoke.punchline}");
            Console.WriteLine("________________________________________________________________________________________________________________________\n\n\n");
            #endregion

            #region Product Example
            using HttpClient clientProduct = new HttpClient();
            Console.Write("Choose product from 1-20: ");
            string id = Console.ReadLine();
            var urlProduct = "https://fakestoreapi.com/products/" + id;
            try
            {
                // Test API reachability
                string responseProduct = await clientProduct.GetStringAsync(urlProduct);
                Product product = JsonSerializer.Deserialize<Product>(responseProduct);
                Console.WriteLine("_____________________________________________________ PRODUCT DETAILS ______________________________________________");
                Console.WriteLine($"Product ID: {product.id}");
                Console.WriteLine($"Title: {product.title}");
                Console.WriteLine($"Price: ${product.price}");
                Console.WriteLine($"Description: {product.description}");
                Console.WriteLine($"Category: {product.category}");
                Console.WriteLine("____________________________________________________________________________________________________________________\n\n\n");

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

            #region Simulate Download Example
            static async Task simulateDownload()
            {
                Console.WriteLine("\n\n\nStarting file download...\n");
                System.Threading.Thread.Sleep(5000); // Simulate a 5-second download

                Console.WriteLine("File download completed!");
                
            }
            #endregion

            #region Sequential vs Multitasking Example
            Console.WriteLine("Choose 1 for Sequential Way or 2 for Multitasking Way:");
            var choice = Console.ReadKey(true).KeyChar;
            if (choice == '1')
            {
                Console.WriteLine("Running Sequentially ... ");
                await SequentialWay();
            }
            else if (choice == '2')
            {
                Console.WriteLine("Running Multitasking ... ");
                await MultitaskingWay();
            }
            else
            {
                Console.WriteLine("Invalid choice. Please select 1 or 2.");
            }

            #region Sequential Way

            static async Task SequentialWay()
            {
                simulateDownload();
                showTime(5);
            }

            #endregion

            #region Mulittasking Way

            static async Task MultitaskingWay()
            {
                Task downloadTask = Task.Run(() => simulateDownload());
                await showTime(5);
                await downloadTask;

            }

            #endregion
            #endregion

            #region Show Time Example
            static async Task showTime(int seconds)
            {
                for (int i = 0; i < seconds; i++)
                {
                    Console.WriteLine($"Time elapsed: {i + 1} seconds");
                    await Task.Delay(1000); // Wait for 1 second
                }
            }
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
