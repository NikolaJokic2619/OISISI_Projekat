
using ConsoleClient.MenuBar;
using ConsoleClient.Utils;
using Core.DAO;
using Core.Models;

namespace ConsoleClient.Controllers
{
    class PublisherController : Menu
    {
        private readonly PublisherDAO _publishersDAO;

        public PublisherController(PublisherDAO publisherDAO)
        {
            _publishersDAO = publisherDAO;
        }

        public string Title => "Publishers";

        public void RunMenu()
        {
            while (true)
            {
                ShowMenu();
                string? input = Console.ReadLine()?.Trim();

                switch (input)
                {
                    case "1":
                        ShowAllPublishers();
                        break;
                    case "2":
                        AddPublishers();
                        break;
                    case "3":
                        UpdatePublishers();
                        break;
                    case "4":
                        RemovePublishers();
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
            Console.WriteLine("1 - Show all publishers");
            Console.WriteLine("2 - Add a new publisher");
            Console.WriteLine("3 - Update a publisher");
            Console.WriteLine("4 - Remove a publisher");
            Console.WriteLine("0 - Exit");
            Console.WriteLine("Choose an option: ");
        }

        public void ShowAllPublishers()
        {
            List<Publisher> publishers = _publishersDAO.GetAllPublishers();

            if (publishers.Count == 0)
            {
                Console.WriteLine("No publishers found");
                return;
            }

            foreach (Publisher publisher in publishers)
            {
                PrintPublisher(publisher);
            }
        }

        public static void PrintPublisher(Publisher publisher)
        {
            Console.WriteLine($" PUBLISHER #{publisher.Id}");

            Console.WriteLine($" Name:              {publisher.NameOfThePublisher}");
            Console.WriteLine($" Chief author ID:   {publisher.ChiefAuthorId}");
            Console.WriteLine($" Number of authors: {publisher.ListOfAuthors?.Count ?? 0}");
            Console.WriteLine($" Number of books:   {publisher.ListOfBooks?.Count ?? 0}");
        }

        public static Publisher InputPublisher()
        {
            Console.WriteLine("\n--- Enter publisher information ---");

            return new Publisher
            {
                NameOfThePublisher = ConsoleInput.ReadRequiredText("Publisher name: "),
                Password = ConsoleInput.ReadRequiredText("Password: "),
                ChiefAuthorId = ConsoleInput.ReadInt("Chief author ID: ")
            };
        }

        public void AddPublishers()
        {
            Publisher publisher = _publishersDAO.AddPublisher(InputPublisher());
            Console.WriteLine($"ID of the added publisher: {publisher.Id}");
        }

        public void UpdatePublishers()
        {
            int id = ConsoleInput.ReadInt("Publisher ID: ");
            Publisher? publisher = _publishersDAO.GetPublisherById(id);

            if (publisher is null)
            {
                Console.WriteLine("Publisher does not exist!");
                return;
            }

            Console.WriteLine("Current publisher:");
            PrintPublisher(publisher);

            Console.WriteLine("Enter all new values:");
            Publisher updatedPublisher = InputPublisher();
            updatedPublisher.Id = id;

            _publishersDAO.UpdatePublisher(updatedPublisher);
            Console.WriteLine("Publisher updated.");
        }

        public void RemovePublishers()
        {
            int id = ConsoleInput.ReadInt("Publisher ID: ");
            Publisher? removedPublisher = _publishersDAO.RemovePublisher(id);

            if (removedPublisher is null)
            {
                Console.WriteLine("Publisher not found.");
                return;
            }

            Console.WriteLine("Publisher removed.");
        }
    }
}
