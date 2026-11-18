using ConsoleClient.Controllers;
using ConsoleClient.MenuBar;
using Core.DAO;

try
{
    Menu[] menus =
    {
        new AddressController(new AddressDAO())
    };

    new MainMenu(menus).Run();
}
catch (EndOfStreamException)
{
    Console.WriteLine("\nInput closed. Application stopped.");
}