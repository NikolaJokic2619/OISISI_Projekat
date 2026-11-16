namespace Core.Models;

public class Author
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Address Address { get; set; }
    public string TelephoneNumber { get; set; }
    public string Email { get; set; }
    public string IdentityCardNumber { get; set; }
    public int YearsOfExperience { get; set; }
    public List<Book> Books { get; set; } = new();
}
