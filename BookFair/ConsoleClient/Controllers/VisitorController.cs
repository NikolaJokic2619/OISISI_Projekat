using ConsoleClient.MenuBar;
using ConsoleClient.Utils;
using Core.DAO;
using Core.Models;
using System.Net;

namespace ConsoleClient.Controllers
{
    class VisitorController : Menu
    {
        private readonly VisitorDAO _visitorsDAO;
        private readonly AddressDAO _addressDAO;

        public VisitorController(VisitorDAO visitorDAO, AddressDAO addressDAO)
        {
            _visitorsDAO = visitorDAO;
            _addressDAO = addressDAO;
        }

        public string Title => "Visitors";

        public void RunMenu()
        {
            while (true)
            {
                ShowMenu();
                string? input = Console.ReadLine()?.Trim();

                switch (input)
                {
                    case "1":
                        ShowAllVisitors();
                        break;
                    case "2":
                        AddVisitors();
                        break;
                    case "3":
                        UpdateVisitors();
                        break;
                    case "4":
                        RemoveVisitors();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }
            }
        }

        public void ShowMenu()
        {
            Console.WriteLine($"\n--- {Title} ---");
            Console.WriteLine("1 - Show all visitors");
            Console.WriteLine("2 - Add a new visitor");
            Console.WriteLine("3 - Update a visitor");
            Console.WriteLine("4 - Remove a visitor");
            Console.WriteLine("0 - Exit");
            Console.WriteLine("Choose an option: ");
        }

        public void ShowAllVisitors()
        {
            List<Visitor> visitors = _visitorsDAO.GetAllVisitors();

            if (visitors.Count == 0)
            {
                Console.WriteLine("No visitors found");
                return;
            }

            foreach (Visitor visitor in visitors)
            {
                PrintVisitor(visitor);
            }
        }

        public static void PrintVisitor(Visitor visitor)
        {
            Console.WriteLine($" VISITOR #{visitor.Id}");

            Console.WriteLine($" Name:              {visitor.Name}");
            Console.WriteLine($" Surname:           {visitor.Surname}");
            Console.WriteLine($" Date of birth:     {visitor.DateOfBirth:dd/MM/yyyy}");
            Console.WriteLine($" Address ID:        {visitor.AddressId}");
            Console.WriteLine($" Telephone:         {visitor.TelephoneNumber}");
            Console.WriteLine($" Email:             {visitor.Email}");

            Console.WriteLine(" MEMBERSHIP INFORMATION");

            Console.WriteLine($" Membership card:   {visitor.NumberOfMembershipCard}");
            Console.WriteLine($" Membership year:   {visitor.CurrentYearOfMembership}");
            Console.WriteLine($" Status:            {(visitor.Status == Status.P ? "Regular" : "VIP")}");
            Console.WriteLine($" Average rating:    {visitor.AverageRating:F2}");

            if (visitor.ListOfBoughtBooks.Count == 0)
            {
                Console.WriteLine("No bought books.");
            }
            else
            {
                foreach (Shoping shopping in visitor.ListOfBoughtBooks)
                {
                    Console.WriteLine($" Book ID: {shopping.BookId}");
                }
            }

            Console.WriteLine("WISH LIST");

            if (visitor.WishList.Count == 0)
            {
                Console.WriteLine("Wish list is empty.");
            }
            else
            {
                foreach (Book book in visitor.WishList)
                {
                    Console.WriteLine($" Book ID: {book.Id}");
                }
            }
        }

        public Visitor? InputVisitor()
        {
            Console.WriteLine("\n--- Enter visitor information ---");

            int addressId = ConsoleInput.ReadInt("Address ID: ");
            Address? address = _addressDAO.GetAddressById(addressId);

            if (address is null)
            {
                Console.WriteLine("Address does not exist!");
                return null;
            }

            return new Visitor
            {
                Name = ConsoleInput.ReadRequiredText("Name: "),
                Surname = ConsoleInput.ReadRequiredText("Surname: "),
                DateOfBirth = DateOnly.Parse(ConsoleInput.ReadRequiredText("Date of birth (dd/mm/yyyy): ")),
                Address = address,
                AddressId = address.Id,
                TelephoneNumber = ConsoleInput.ReadRequiredText("Telephone: "),
                Email = ConsoleInput.ReadRequiredText("Email: "),
                NumberOfMembershipCard = ConsoleInput.ReadRequiredText("Membership card number: "),
                CurrentYearOfMembership = ConsoleInput.ReadInt("Current membership year: "),
                Status = ConsoleInput.ReadRequiredText("Status (P/B): ").ToUpper() switch
                {
                    "P" => Status.P,
                    "B" => Status.B,
                    _ => throw new ArgumentException("Invalid status.")
                    // _ - ANY OTHER character besides P and B
                },
                AverageRating = 0.0
            };


        }
        public void AddVisitors()
        {
            Visitor? visitor = InputVisitor();

            if (visitor is null)
                return;

            Visitor addedVisitor = _visitorsDAO.AddVisitor(visitor);
            Console.WriteLine($"ID of the current added visitor: {addedVisitor.Id}");
        }

        public void UpdateVisitors()
        {
            int id = ConsoleInput.ReadInt("Visitor ID: ");
            Visitor? visitor = _visitorsDAO.GetVisitorById(id);

            if (visitor is null)
            {
                Console.WriteLine("Visitor does not exists!");
                return;
            }

            Console.WriteLine("Current visitor:");
            PrintVisitor(visitor);

            Console.WriteLine("Enter all new values:");
            Visitor? updatedVisitor = InputVisitor();

            if (updatedVisitor is null)
                return;

            updatedVisitor.Id = id;
            updatedVisitor.AverageRating = visitor.AverageRating;
            updatedVisitor.ListOfBoughtBooks = visitor.ListOfBoughtBooks;
            updatedVisitor.WishList = visitor.WishList;
            updatedVisitor.WishListBookIds = visitor.WishListBookIds;

            _visitorsDAO.UpdateVisitor(updatedVisitor);
            Console.WriteLine("Visitor updated.");

        }

        public void RemoveVisitors()
        {
            int id = ConsoleInput.ReadInt("Visitor ID: ");
            Visitor removedVisitor = _visitorsDAO.RemoveVisitor(id);

            if (removedVisitor is null)
            {
                Console.WriteLine("Visitor not found.");
                return;
            }

            Console.WriteLine("Visitor removed.");

        }
    }
}
