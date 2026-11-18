using ConsoleClient.MenuBar;
using ConsoleClient.Utils;
using Core.DAO;
using Core.Models;

namespace ConsoleClient.Controllers;

// Source: OISISI exercises 3 - CRUDExample
internal class AuthorController : Menu
{
    private readonly AuthorDAO _authorDao;
    private readonly AddressDAO _addressDao;
    private readonly PublisherDAO _publisherDao;

    public AuthorController(AuthorDAO authorDao, AddressDAO addressDao, PublisherDAO publisherDao)
    {
        _authorDao = authorDao;
        _addressDao = addressDao;
        _publisherDao = publisherDao;
    }

    public string Title => "Authors";

    public void RunMenu()
    {
        while (true)
        {
            ShowMenu();
            switch (Console.ReadLine()?.Trim())
            {
                case "1":
                    ShowAllAuthors();
                    break;
                case "2":
                    AddAuthor();
                    break;
                case "3":
                    UpdateAuthor();
                    break;
                case "4":
                    RemoveAuthor();
                    break;
                case "0":
                case null:
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    private void ShowMenu()
    {
        Console.WriteLine($"\n--- {Title} ---");
        Console.WriteLine("1 - Show all authors");
        Console.WriteLine("2 - Add author");
        Console.WriteLine("3 - Update author");
        Console.WriteLine("4 - Remove author");
        Console.WriteLine("0 - Back");
        Console.Write("Choose an option: ");
    }

    private void ShowAllAuthors()
    {
        List<Author> authors = _authorDao.GetAllAuthors();
        if (authors.Count == 0)
        {
            Console.WriteLine("No authors found.");
            return;
        }

        foreach (Author author in authors)
        {
            PrintAuthor(author);
        }
    }

    private void AddAuthor()
    {
        Author? author = InputAuthor(null);
        if (author is null)
        {
            return;
        }

        Author addedAuthor = _authorDao.AddAuthor(author);
        Console.WriteLine($"Author added. ID: {addedAuthor.Id}");
    }

    private void UpdateAuthor()
    {
        int id = ConsoleInput.ReadInt("Author ID: ");
        Author? current = _authorDao.GetAuthorById(id);
        if (current is null)
        {
            Console.WriteLine("Author not found.");
            return;
        }

        Console.WriteLine("Current author:");
        PrintAuthor(current);
        Console.WriteLine("Enter all new values:");

        Author? updatedAuthor = InputAuthor(current);
        if (updatedAuthor is null)
        {
            return;
        }

        _authorDao.UpdateAuthor(updatedAuthor);
        Console.WriteLine("Author updated.");
    }

    private void RemoveAuthor()
    {
        int id = ConsoleInput.ReadInt("Author ID: ");
        if (_publisherDao.GetAllPublishers().Any(publisher => publisher.ChiefAuthorId == id))
        {
            Console.WriteLine("This author is a publisher's chief author. Change that assignment before removing the author.");
            return;
        }

        Author? removedAuthor = _authorDao.RemoveAuthor(id);
        Console.WriteLine(removedAuthor is null ? "Author not found." : "Author removed.");
    }

    private Author? InputAuthor(Author? current)
    {
        Address? address = ConsoleInput.SelectRecord(
            "Address", "Add an address before adding or editing an author.",
            _addressDao.GetAllAddresses(), item => item.Id,
            item => $"{item.Street} {item.Number}, {item.City}, {item.Country}");
        if (address is null)
        {
            return null;
        }

        bool isChief = current is not null && _publisherDao.GetAllPublishers()
            .Any(publisher => publisher.ChiefAuthorId == current.Id);

        return new Author
        {
            Id = current?.Id ?? 0,
            Address = address,
            AddressId = address.Id,
            Name = ConsoleInput.ReadRequiredText("First name: "),
            Surname = ConsoleInput.ReadRequiredText("Last name: "),
            DateOfBirth = ConsoleInput.ReadDate("Date of birth (DD/MM/YYYY): "),
            TelephoneNumber = ConsoleInput.ReadRequiredText("Telephone number: "),
            Email = ConsoleInput.ReadRequiredText("Email: "),
            IdentityCardNumber = ConsoleInput.ReadRequiredText("Identity card number: "),
            YearsOfExperience = ConsoleInput.ReadIntInRange(
                isChief ? "Years of experience (at least 5 for chief authors): " : "Years of experience: ",
                isChief ? 5 : 0),
            Books = current?.Books ?? new List<Book>()
        };
    }

    private static void PrintAuthor(Author author)
    {
        Console.WriteLine(
            $"ID: {author.Id} | {author.Name} {author.Surname} | Born: {author.DateOfBirth:dd'/'MM'/'yyyy} | " +
            $"Address ID: {author.AddressId} | Phone: {author.TelephoneNumber} | Email: {author.Email} | " +
            $"Identity card: {author.IdentityCardNumber} | Experience: {author.YearsOfExperience} year(s)");
    }

}
