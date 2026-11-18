using ConsoleClient.Controllers;
using ConsoleClient.MenuBar;
using Core.DAO;

try
{
    AddressDAO addressDao = new();
    AuthorDAO authorDao = new();
    BookDAO bookDao = new();
    VisitorDAO visitorDao = new();
    PublisherDAO publisherDao = new();
    ShopingDAO shoppingDao = new();

    Menu[] menus =
    {
        new AddressController(addressDao, authorDao, visitorDao),
        new AuthorController(authorDao, addressDao, publisherDao),
        new BookController(bookDao, publisherDao, shoppingDao),
        new VisitorController(visitorDao, addressDao, shoppingDao),
        new PublisherController(publisherDao, authorDao, bookDao),
        new ShoppingController(shoppingDao, visitorDao, bookDao)
    };

    new MainMenu(menus).Run();
}
catch (EndOfStreamException)
{
    Console.WriteLine("\nInput closed. Application stopped.");
}
