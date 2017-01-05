using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace BookShelfApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("*** Let's search the Google Books API with a search term ***");
            Console.Write("Enter Search Term: ");
            string searchTerm = Console.ReadLine();
            //string isbn = "1933354313";

            string jsonResponse = SearchGoogleBooks(searchTerm);

            JObject json = JObject.Parse(jsonResponse);

            Book newBook = new Book();
            newBook.ISBN = (string)json.SelectToken("items[0].volumeInfo.industryIdentifiers[0].identifier");
            newBook.Title = (string)json.SelectToken("items[0].volumeInfo.title");
            newBook.Author = (string)json.SelectToken("items[0].volumeInfo.authors[0]");
            newBook.Publisher = (string)json.SelectToken("items[0].volumeInfo.publisher");
            newBook.PublishedDate = (string)json.SelectToken("items[0].volumeInfo.publishedDate");
            newBook.Description = (string)json.SelectToken("items[0].volumeInfo.description");
            newBook.Category = (string)json.SelectToken("items[0].volumeInfo.categories[0]");
            newBook.ImageLink = (string)json.SelectToken("items[0].volumeInfo.imageLinks.thumbnail");

            //This next loop prints all properties of the object newBook. Useful to see what's in an object!
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(newBook))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(newBook);
                Console.WriteLine("{0} = {1}", name, value);
            }

            
            Console.WriteLine("Press any key to end");
            Console.ReadKey();

        }

        //This method searches the GoogleBooks API with a given search term
        private static string SearchGoogleBooks(string searchTerm)
        {
            string GoogleBooksApiKey = "AIzaSyBaf_7wmQ6Ve26VpFEocW7YHQCoRFV3prQ";

            WebRequest request = WebRequest.Create("https://www.googleapis.com/books/v1/volumes?q=" + searchTerm + "&key=" + GoogleBooksApiKey);
            WebResponse response = request.GetResponse();
            // Display the status.
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            
            
            // Clean up the streams and the response.
            reader.Close();
            response.Close();
            response.Close();
            return responseFromServer;
        }
    }
}
