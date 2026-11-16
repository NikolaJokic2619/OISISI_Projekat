namespace Core.Models;

public class Book
{
    public string Isbn { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public int PublicationYear { get; set; }
    public decimal Price { get; set; }
    public int PageCount { get; set; }
    public List<Author> Authors { get; set; } = new();
    public Publisher Publisher { get; set; }
    public List<Visitor> PurchasedBy { get; set; } = new();
    public List<Visitor> WishlistedBy { get; set; } = new();
}
