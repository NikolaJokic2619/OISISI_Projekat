namespace Core.Models;

public class Book
{
    public int Id { get; set; }
    public string Isbn { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public int PublicationYear { get; set; }
    public decimal Price { get; set; }
    public int PageCount { get; set; }
    public List<Author> Authors { get; set; } = new();
    public Publisher Publisher { get; set; }
    public int PublisherId { get; set; }
    public List<Visitor> PurchasedBy { get; set; } = new();
    public List<Visitor> WishlistedBy { get; set; } = new();

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(), Isbn, Title, Genre, PublicationYear.ToString(), Price.ToString(), PageCount.ToString(), PublisherId.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        if (values.Length != 8)
        {
            throw new ArgumentException("An address must contain 4 fields.");
        }

        Id = int.Parse(values[0]);
        Isbn = values[1];
        Title = values[2];
        Genre = values[3];
        PublicationYear = int.Parse(values[4]);
        Price = decimal.Parse(values[5]);
        PageCount = int.Parse(values[6]);
        PublisherId = int.Parse(values[7]);
    }
}
