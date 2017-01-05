// The model for the book. Very simple, but contains everything I needed to describe a book. 

namespace BookShelfApp
{
    class Book
    {
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
