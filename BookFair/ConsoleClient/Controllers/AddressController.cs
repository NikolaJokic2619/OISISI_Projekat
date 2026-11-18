using ConsoleClient.MenuBar;
using ConsoleClient.Utils;
using Core.DAO;
using Core.Models;

namespace ConsoleClient.Controllers;

//Source: OISISI exercises 3 - CRUDExample
internal class AddressController : Menu
{
    private readonly AddressDAO _addressDao;
    private readonly AuthorDAO _authorDao;
    private readonly VisitorDAO _visitorDao;

    public AddressController(AddressDAO addressDao, AuthorDAO authorDao, VisitorDAO visitorDao)
    {
        _addressDao = addressDao;
        _authorDao = authorDao;
        _visitorDao = visitorDao;
    }

    public string Title => "Addresses";

    public void RunMenu()
    {
        while (true)
        {
            ShowMenu();
            string? input = Console.ReadLine()?.Trim();

            switch (input)
            {
                case "1":
                    ShowAllAddresses();
                    break;
                case "2":
                    AddAddress();
                    break;
                case "3":
                    UpdateAddress();
                    break;
                case "4":
                    RemoveAddress();
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
        Console.WriteLine("1. Show all addresses");
        Console.WriteLine("2. Add address");
        Console.WriteLine("3. Update address");
        Console.WriteLine("4. Remove address");
        Console.WriteLine("0. Back");
        Console.Write("Choose an option: ");
    }

    private void ShowAllAddresses()
    {
        List<Address> addresses = _addressDao.GetAllAddresses();

        if (addresses.Count == 0)
        {
            Console.WriteLine("No addresses found.");
            return;
        }

        foreach (Address address in addresses)
        {
            PrintAddress(address);
        }
    }

    private static void PrintAddress(Address address)
    {
        Console.WriteLine(
            $"ID: {address.Id} | {address.Street} {address.Number} | " +
            $"{address.City}, {address.Country}");
    }

    private static Address InputAddress()
    {
        return new Address
        {
            Street = ConsoleInput.ReadRequiredText("Street: "),
            Number = ConsoleInput.ReadRequiredText("Number: "),
            City = ConsoleInput.ReadRequiredText("City: "),
            Country = ConsoleInput.ReadRequiredText("Country: ")
        };
    }

    private void AddAddress()
    {
        Address addedAddress = _addressDao.AddAddress(InputAddress());
        Console.WriteLine($"Address added. ID: {addedAddress.Id}");
    }

    private void UpdateAddress()
    {
        int id = ConsoleInput.ReadInt("Address ID: ");
        Address? existingAddress = _addressDao.GetAddressById(id);

        if (existingAddress is null)
        {
            Console.WriteLine("Address not found.");
            return;
        }

        Console.WriteLine("Current address:");
        PrintAddress(existingAddress);

        Console.WriteLine("Enter all new values:");
        Address updatedAddress = InputAddress();
        updatedAddress.Id = id;

        _addressDao.UpdateAddress(updatedAddress);
        Console.WriteLine("Address updated.");
    }

    private void RemoveAddress()
    {
        int id = ConsoleInput.ReadInt("Address ID: ");
        if (_authorDao.GetAllAuthors().Any(author => author.AddressId == id)
            || _visitorDao.GetAllVisitors().Any(visitor => visitor.AddressId == id))
        {
            Console.WriteLine("An author or visitor uses this address. Reassign them before removing the address.");
            return;
        }

        Address? removedAddress = _addressDao.RemoveAddress(id);

        if (removedAddress is null)
        {
            Console.WriteLine("Address not found.");
            return;
        }

        Console.WriteLine("Address removed.");
    }
}
