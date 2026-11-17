using Core.Models;
using Core.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAO
{
    public class AuthorDAO
    {
        private readonly List<Author> _authors;
        private readonly Storage<Author> _storage;

        public AuthorDAO()
        {
            _storage = new Storage<Author>("author.txt");
            _authors = _storage.Load();
        }

        private int GenerateID()
        {
            if (_authors.Count == 0)
            {
                return 0;
            }
            return _authors[^1].Id + 1;
        }

        public Author AddAuthor(Author author)
        {
            author.Id = GenerateID();
            _authors.Add(author);
            _storage.Save(_authors);
            return author;
        }

        public Author UpdateAuthor(Author author)
        {
            Author oldAuthor = GetAuthorById(author.Id);
            if (oldAuthor == null)
            {
                return null;
            }

            oldAuthor.Name = author.Name;
            oldAuthor.Surname = author.Surname;
            oldAuthor.DateOfBirth = author.DateOfBirth;
            oldAuthor.Address = author.Address;
            oldAuthor.AddressId = author.AddressId;
            oldAuthor.TelephoneNumber = author.TelephoneNumber;
            oldAuthor.Email = author.Email;
            oldAuthor.IdentityCardNumber = author.IdentityCardNumber;
            oldAuthor.YearsOfExperience = author.YearsOfExperience;
            oldAuthor.Books = author.Books;

            _storage.Save(_authors);

            return oldAuthor;
        }

        public Author RemoveAuthor(int id)
        {
            Author removingAuthor = GetAuthorById(id);
            if (removingAuthor == null)
            {
                return null;
            }

            _authors.Remove(removingAuthor);
            _storage.Save(_authors);
            return removingAuthor;
        }

        public Author? GetAuthorById(int id)
        {
            return _authors.Find(v => v.Id == id);
        }

        public List<Author> GetAllAuthors()
        {
            return _authors;
        }

        public List<Author> GetAllAuthors(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            IEnumerable<Author> authors = _authors;

            switch (sortCriteria)
            {
                case "Id":
                    authors = _authors.OrderBy(x => x.Id);
                    break;
                case "Name":
                    authors = _authors.OrderBy(x => x.Name);
                    break;
                case "Surname":
                    authors = _authors.OrderBy(x => x.Surname);
                    break;
                case "DateOfBirth":
                    authors = _authors.OrderBy(x => x.DateOfBirth);
                    break;
                case "AddressId":
                    authors = _authors.OrderBy(x => x.AddressId);
                    break;
                case "TelephoneNumber":
                    authors = _authors.OrderBy(x => x.TelephoneNumber);
                    break;
                case "Email":
                    authors = _authors.OrderBy(x => x.Email);
                    break;
                case "IdentityCardNumber":
                    authors = _authors.OrderBy(x => x.IdentityCardNumber);
                    break;
                case "YearsOfExperience":
                    authors = _authors.OrderBy(x => x.YearsOfExperience);
                    break;
            }

            if (sortDirection == SortDirection.Descending)
            {
                authors = authors.Reverse();
            }

            authors = authors.Skip((page - 1) * pageSize).Take(pageSize);

            return authors.ToList();
        }
    }
}
