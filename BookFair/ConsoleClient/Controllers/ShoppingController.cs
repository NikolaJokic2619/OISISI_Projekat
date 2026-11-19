using ConsoleClient.MenuBar;
using ConsoleClient.Utils;
using Core.DAO;
using Core.Models;

namespace ConsoleClient.Controllers;

// Source: OISISI exercises 3 - CRUDExample
internal class ShoppingController : Menu
{
    private readonly ShopingDAO _shoppingDao;
    private readonly VisitorDAO _visitorDao;
    private readonly BookDAO _bookDao;

    public ShoppingController(ShopingDAO shoppingDao, VisitorDAO visitorDao, BookDAO bookDao)
    {
        _shoppingDao = shoppingDao;
        _visitorDao = visitorDao;
        _bookDao = bookDao;
    }

    public string Title => "Purchases";

    public void RunMenu()
    {
        while (true)
        {
            ShowMenu();
            switch (Console.ReadLine()?.Trim())
            {
                case "1":
                    ShowAllPurchases();
                    break;
                case "2":
                    AddPurchase();
                    break;
                case "3":
                    UpdatePurchase();
                    break;
                case "4":
                    RemovePurchase();
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
        Console.WriteLine("1 - Show all purchases");
        Console.WriteLine("2 - Add purchase");
        Console.WriteLine("3 - Update purchase");
        Console.WriteLine("4 - Remove purchase");
        Console.WriteLine("0 - Back");
        Console.Write("Choose an option: ");
    }

    private void ShowAllPurchases()
    {
        List<Shoping> purchases = _shoppingDao.GetAllShopings();
        if (purchases.Count == 0)
        {
            Console.WriteLine("No purchases found.");
            return;
        }

        foreach (Shoping purchase in purchases)
        {
            PrintPurchase(purchase);
        }
    }

    private void AddPurchase()
    {
        Shoping? purchase = InputPurchase(null);
        if (purchase is null)
        {
            return;
        }

        Shoping addedPurchase = _shoppingDao.AddShoping(purchase);
        RefreshVisitorPurchases();
        Console.WriteLine($"Purchase added. ID: {addedPurchase.Id}");
    }

    private void UpdatePurchase()
    {
        int id = ConsoleInput.ReadInt("Purchase ID: ");
        Shoping? current = _shoppingDao.GetShopingById(id);
        if (current is null)
        {
            Console.WriteLine("Purchase not found.");
            return;
        }

        Console.WriteLine("Current purchase:");
        PrintPurchase(current);
        Console.WriteLine("Enter all new values:");

        Shoping? updatedPurchase = InputPurchase(current);
        if (updatedPurchase is null)
        {
            return;
        }

        _shoppingDao.UpdateShoping(updatedPurchase);
        RefreshVisitorPurchases();
        Console.WriteLine("Purchase updated.");
    }

    private void RemovePurchase()
    {
        int id = ConsoleInput.ReadInt("Purchase ID: ");
        Shoping? removedPurchase = _shoppingDao.RemoveShoping(id);
        RefreshVisitorPurchases();
        Console.WriteLine(removedPurchase is null ? "Purchase not found." : "Purchase removed.");
    }

    private Shoping? InputPurchase(Shoping? current)
    {
        List<Visitor> visitors = _visitorDao.GetAllVisitors();
        List<Book> books = _bookDao.GetAllBooks();
        if (visitors.Count == 0 || books.Count == 0)
        {
            Console.WriteLine("Add a visitor and a book before adding or editing a purchase.");
            return null;
        }

        Visitor? visitor = ConsoleInput.SelectRecord(
            "Visitor", "No visitors found.", visitors, item => item.Id,
            item => $"{item.Name} {item.Surname} ({item.NumberOfMembershipCard})");
        if (visitor is null)
        {
            return null;
        }

        Book? book = ConsoleInput.SelectRecord(
            "Book", "No books found.", books, item => item.Id, item => $"{item.Title} ({item.Isbn})");
        if (book is null)
        {
            return null;
        }

        return new Shoping
        {
            Id = current?.Id ?? 0,
            Visitor = visitor,
            VisitorId = visitor.Id,
            Book = book,
            BookId = book.Id,
            DateOfBuying = ConsoleInput.ReadDate("Purchase date (DD/MM/YYYY): "),
            Rating = ConsoleInput.ReadIntInRange("Rating (1-5): ", 1, 5),
            Comment = ConsoleInput.ReadText("Comment (optional): ", allowEmpty: true)
        };
    }

    private static void PrintPurchase(Shoping purchase)
    {
        Console.WriteLine(
            $"ID: {purchase.Id} | Visitor ID: {purchase.VisitorId} | Book ID: {purchase.BookId} | " +
            $"Purchased: {purchase.DateOfBuying:dd'/'MM'/'yyyy} | Rating: {purchase.Rating} | Comment: {purchase.Comment}");
    }

    private void RefreshVisitorPurchases()
    {
        foreach (Visitor visitor in _visitorDao.GetAllVisitors())
        {
            visitor.ListOfBoughtBooks.Clear();
        }

        foreach (Shoping purchase in _shoppingDao.GetAllShopings())
        {
            Visitor? visitor = _visitorDao.GetVisitorById(purchase.VisitorId);
            Book? book = _bookDao.GetBookById(purchase.BookId);

            purchase.Visitor = visitor!;
            purchase.Book = book!;

            if (visitor != null && book != null)
            {
                visitor.ListOfBoughtBooks.Add(purchase);
            }
        }
    }
}
