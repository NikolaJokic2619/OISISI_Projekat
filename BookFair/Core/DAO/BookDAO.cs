using Core.Models;
using Core.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAO
{
    public class BookDAO
    {
        private readonly List<Book> _books;
        private readonly Storage<Book> _storage;

        public BookDAO()
        {
            _storage = new Storage<Book>("book.csv");
            _books = _storage.Load();
        }

        private int GenerateID()
        {
            return _books.Count == 0 ? 0 : checked(_books.Max(a => a.Id) + 1);
        }

        public Book AddBook(Book book)
        {
            book.Id = GenerateID();
            _books.Add(book);
            _storage.Save(_books);
            return book;
        }

        public Book UpdateBook(Book book)
        {
            Book oldBook = GetBookById(book.Id);
            if (oldBook == null)
            {
                return null;
            }

            oldBook.Isbn = book.Isbn;
            oldBook.Title = book.Title;
            oldBook.Genre = book.Genre;
            oldBook.PublicationYear = book.PublicationYear;
            oldBook.Price = book.Price;
            oldBook.PageCount = book.PageCount;
            oldBook.Authors = book.Authors;
            oldBook.Publisher = book.Publisher;
            oldBook.PublisherId = book.PublisherId;
            oldBook.PurchasedBy = book.PurchasedBy;
            oldBook.WishlistedBy = book.WishlistedBy;

            _storage.Save(_books);

            return oldBook;
        }

        public Book RemoveBook(int id)
        {
            Book removingBook = GetBookById(id);
            if (removingBook == null)
            {
                return null;
            }

            _books.Remove(removingBook);
            _storage.Save(_books);
            return removingBook;
        }

        public Book? GetBookById(int id)
        {
            return _books.Find(v => v.Id == id);
        }

        public List<Book> GetAllBooks()
        {
            return _books;
        }

        public List<Book> GetAllBooks(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            IEnumerable<Book> books = _books;

            switch (sortCriteria)
            {
                case "Isbn":
                    books = _books.OrderBy(x => x.Isbn);
                    break;
                case "Title":
                    books = _books.OrderBy(x => x.Title);
                    break;
                case "Genre":
                    books = _books.OrderBy(x => x.Genre);
                    break;
                case "PublicationYear":
                    books = _books.OrderBy(x => x.PublicationYear);
                    break;
                case "Price":
                    books = _books.OrderBy(x => x.Price);
                    break;
                case "PageCount":
                    books = _books.OrderBy(x => x.PageCount);
                    break;
                case "PublisherId":
                    books = _books.OrderBy(x => x.PublisherId);
                    break;
            }

            if (sortDirection == SortDirection.Descending)
            {
                books = books.Reverse();
            }

            books = books.Skip((page - 1) * pageSize).Take(pageSize);

            return books.ToList();
        }
    }
}
