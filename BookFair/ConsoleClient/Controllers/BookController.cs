using ConsoleClient.MenuBar;
using ConsoleClient.Utils;
using Core.DAO;
using Core.Models;

namespace ConsoleClient.Controllers
{
    class BookController : Menu
    {
        private readonly BookDAO _booksDAO;

        public BookController(BookDAO bookDAO)
        {
            _booksDAO = bookDAO;
        }

        public string Title => "Books";

        public void RunMenu()
        {
            while (true)
            {
                ShowMenu();
                string? input = Console.ReadLine()?.Trim();

                switch (input)
                {
                    case "1":
                        ShowAllBooks();
                        break;
                    case "2":
                        AddBooks();
                        break;
                    case "3":
                        UpdateBooks();
                        break;
                    case "4":
                        RemoveBooks();
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
            Console.WriteLine("1 - Show all books");
            Console.WriteLine("2 - Add a new book");
            Console.WriteLine("3 - Update a book");
            Console.WriteLine("4 - Remove a book");
            Console.WriteLine("0 - Exit");
            Console.WriteLine("Choose an option: ");
        }

        public void ShowAllBooks()
        {
            List<Book> books = _booksDAO.GetAllBooks();

            if (books.Count == 0)
            {
                Console.WriteLine("No books found");
                return;
            }

            foreach (Book book in books)
            {
                PrintBook(book);
            }
        }

        public static void PrintBook(Book book)
        {
            Console.WriteLine($" BOOK #{book.Id}");

            Console.WriteLine($" Title:             {book.Title}");
            Console.WriteLine($" ISBN:              {book.Isbn}");
            Console.WriteLine($" Publication year:  {book.PublicationYear}");
            Console.WriteLine($" Price:             {book.Price:F2}");
            Console.WriteLine($" Publisher ID:      {book.PublisherId}");
        }

        public static Book InputBook()
        {
            Console.WriteLine("\n--- Enter book information ---");

            return new Book
            {
                Title = ConsoleInput.ReadRequiredText("Title: "),
                Isbn = ConsoleInput.ReadRequiredText("ISBN: "),
                PublicationYear = ConsoleInput.ReadInt("Publication year: "),
                Price = decimal.Parse(
                    ConsoleInput.ReadRequiredText("Price: ")),
                PublisherId = ConsoleInput.ReadInt("Publisher ID: ")
            };
        }

        public void AddBooks()
        {
            Book book = _booksDAO.AddBook(InputBook());
            Console.WriteLine($"ID of the added book: {book.Id}");
        }

        public void UpdateBooks()
        {
            int id = ConsoleInput.ReadInt("Book ID: ");
            Book? book = _booksDAO.GetBookById(id);

            if (book is null)
            {
                Console.WriteLine("Book does not exist!");
                return;
            }

            Console.WriteLine("Current book:");
            PrintBook(book);

            Console.WriteLine("Enter all new values:");
            Book updatedBook = InputBook();
            updatedBook.Id = id;

            _booksDAO.UpdateBook(updatedBook);
            Console.WriteLine("Book updated.");
        }

        public void RemoveBooks()
        {
            int id = ConsoleInput.ReadInt("Book ID: ");
            Book? removedBook = _booksDAO.RemoveBook(id);

            if (removedBook is null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            Console.WriteLine("Book removed.");
        }
    }
}