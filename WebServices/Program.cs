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
                Console.WriteLine("JSON Response: \n_____________________________________________________________________________________________");
                Console.WriteLine(response);

                AnimeQuote result = JsonSerializer.Deserialize<AnimeQuote>(response);
                Console.WriteLine("_____________________________________________________________________________________________\n");

                Console.WriteLine("After Deserialization:");
                Console.WriteLine("_____________________________________________________________________________________________");
                Console.WriteLine(value: $"Anime Character Name: {result.data.character.name}");
                Console.WriteLine(value: $"Anime Name: {result.data.anime.name}");
                Console.WriteLine(value: $"Quote: {result.data.content}");
                Console.WriteLine("_____________________________________________________________________________________________\n\n\n");
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

            Console.WriteLine("_________________________________________ CAT FACT OF THE DAY _______________________________");
            Console.WriteLine(myFact.fact);
            Console.WriteLine($"Length of Fact: {myFact.length} characters");
            Console.WriteLine("_____________________________________________________________________________________________\n\n\n");
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
        }
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
