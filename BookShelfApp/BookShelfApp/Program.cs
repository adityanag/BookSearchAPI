using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
            
            Console.WriteLine("*** Let's search the Google Books API with an ISBN ***");
            Console.Write("Enter ISBN Number: ");
            string isbn = Console.ReadLine();
            //string isbn = "1933354313";

            string jsonResponse = SearchGoogleBooks(isbn);

            JObject json = JObject.Parse(jsonResponse);

            Book newBook = new Book();
            newBook.Author = (string)json.SelectToken("items[0].volumeInfo.authors[0]");
            newBook.Title = (string)json.SelectToken("items[0].volumeInfo.title");

            Console.WriteLine("Title: {0}", newBook.Title);
            Console.WriteLine("Author: {0}", newBook.Author);
            Console.WriteLine("Press any key to end");
            Console.ReadKey();

        }

        private static string SearchGoogleBooks(string isbn)
        {
            string GoogleBooksApiKey = "AIzaSyBaf_7wmQ6Ve26VpFEocW7YHQCoRFV3prQ";

            WebRequest request = WebRequest.Create("https://www.googleapis.com/books/v1/volumes?q=+isbn:" + isbn + "&key=" + GoogleBooksApiKey);
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
