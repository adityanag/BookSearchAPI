using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;


namespace BookShelfApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int totalItems = 0; //set totalItems to 0 to start

            Console.WriteLine("--------- Let's search the Google Books API with a search term ----------");
            Console.Write("Enter Search Term: ");
            string searchTerm = Console.ReadLine();
            string jsonResponse = SearchGoogleBooks(searchTerm);

            JObject json = JObject.Parse(jsonResponse);

            if ((JArray)json["items"]!= null) //If there are Items, then set the number in the totalItems variable
            {
                JArray items = (JArray)json["items"];
                 totalItems = items.Count;
            }
            
            //If we didn't get any results, say that and exit. If we actually did get results, then process
            if (totalItems == 0)
            {
                Console.WriteLine("No results found");
                
            }

            else
            {
                List<Book> books = new List<Book>(); //create a list of books

                //The loop runs as many times as there are items. Max will be 20, since I'm forcing only 20 results to be returned
                // with maxResults in the search parameter string.
                for(int i = 0; i<totalItems;i++)
                {
                    //This adds each new book object with the object initialization syntax and the custom constructor. I'm using string interpolation (the $ before the string)
                    //to get the variable "i". Right now, I'm only pulling the first author name and the first category, hence the hardcoded 0 for those values.
                    books.Add(new Book(
                        (string)json.SelectToken($"items[{i}].volumeInfo.industryIdentifiers[0].identifier"),
                        (string)json.SelectToken($"items[{i}].volumeInfo.title"),
                        (string)json.SelectToken($"items[{i}].volumeInfo.authors[0]"),
                        (string)json.SelectToken($"items[{i}].volumeInfo.publisher"),
                        (string)json.SelectToken($"items[{i}].volumeInfo.publishedDate"),
                        (string)json.SelectToken($"items[{i}].volumeInfo.description"),
                        (string)json.SelectToken($"items[{i}].volumeInfo.categories[0]"),
                        (string)json.SelectToken($"items[{i}].volumeInfo.imageLinks.thumbnail")
                        ));
                }

                int count = 1; //initialize a counter to tell the user
                //Nested loops - for each book, print every property name and value. It will print the entire list.
                foreach (var book in books)
                {
                    
                    Console.WriteLine("Result {0}", count);
                    foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(book))
                    {
                        string name = descriptor.Name;
                        object value = descriptor.GetValue(book);
                        Console.WriteLine("{0} = {1}", name, value);
                    }
                    Console.WriteLine("--------------------------------------"); //separator for easy reading
                    count++; // keep count from 1 -> whatever the totalItems is, with a max of 20 of course.
                }

            }
            
            Console.WriteLine("Press any key to end");
            Console.ReadKey();

        }

        //This method searches the GoogleBooks API with a given search term. Only 20 results will be returned.
        private static string SearchGoogleBooks(string searchTerm)
        {
            string GoogleBooksApiKey = "AIzaSyCy4jqTONhRa3GXaZIfRMs1KJ4KE8Gjr-8";
            //maxResults is set to 20 so that we only get a small list
            WebRequest request = WebRequest.Create("https://www.googleapis.com/books/v1/volumes?q=" + searchTerm +"&maxResults=20&orderBy=relevance&projection=full&key=" + GoogleBooksApiKey);
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
