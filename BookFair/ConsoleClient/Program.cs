using ConsoleClient.Controllers;
using ConsoleClient.MenuBar;
using Core.DAO;
using Core.Models;

try
{
    AddressDAO addressDao = new();
    AuthorDAO authorDao = new();
    BookDAO bookDao = new();
    VisitorDAO visitorDao = new();
    PublisherDAO publisherDao = new();
    ShopingDAO shoppingDao = new();
    
    foreach (Shoping shopping in shoppingDao.GetAllShopings())
    {
        Visitor? visitor = visitorDao.GetVisitorById(shopping.VisitorId);
        Book? book = bookDao.GetBookById(shopping.BookId);

        if (visitor != null && book != null)
        {
            shopping.Visitor = visitor;
            shopping.Book = book;
            visitor.ListOfBoughtBooks.Add(shopping);
        }
    }

    foreach (Visitor visitor in visitorDao.GetAllVisitors())
    {
        visitor.Address = addressDao.GetAddressById(visitor.AddressId);
    }

    foreach (Visitor visitor in visitorDao.GetAllVisitors())
    {
        visitor.WishList.Clear();

        foreach (int bookId in visitor.WishListBookIds)
        {
            Book? book = bookDao.GetBookById(bookId);

            if (book != null)
            {
                visitor.WishList.Add(book);
            }
        }
    }


    Menu[] menus =
    {
        new AddressController(addressDao, authorDao, visitorDao),
        new AuthorController(authorDao, addressDao, publisherDao),
        new BookController(bookDao),
        new VisitorController(visitorDao, addressDao),
        new PublisherController(publisherDao),
        new ShoppingController(shoppingDao, visitorDao, bookDao)
    };

    new MainMenu(menus).Run();
}
catch (EndOfStreamException)
{
    Console.WriteLine("\nInput closed. Application stopped.");
}
