// The model for the book. Very simple, but contains everything I needed to describe a book. 

namespace BookShelfApp
{
    class Book
    {
        public Book(){}


        //Constructor so I can use the object initializer syntax in the for loop.
        public Book(string isbn, string title, string author, string publisher, string publisheddate, string description, string category, string imagelink)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
            Publisher = publisher;
            PublishedDate = publisheddate;
            Description = description;
            Category = category;
            ImageLink = imagelink;

        }

        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string PublishedDate { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ImageLink { get; set; }

    }
}
