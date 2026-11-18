using Core.Models;
using Core.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAO
{
    public class PublisherDAO
    {
        private readonly List<Publisher> _publishers;
        private readonly Storage<Publisher> _storage;

        public PublisherDAO()
        {
            _storage = new Storage<Publisher>("publisher.csv");
            _publishers = _storage.Load();
        }

        private int GenerateID()
        {
            if (_publishers.Count == 0)
            {
                return 0;
            }
            return _publishers[^1].Id + 1;
        }

        public Publisher AddPublisher(Publisher publisher)
        {
            publisher.Id = GenerateID();
            _publishers.Add(publisher);
            _storage.Save(_publishers);
            return publisher;
        }

        public Publisher UpdatePublisher(Publisher publisher)
        {
            Publisher oldPublisher = GetPublisherById(publisher.Id);
            if (oldPublisher == null)
            {
                return null;
            }

            oldPublisher.Password = publisher.Password;
            oldPublisher.NameOfThePublisher = publisher.NameOfThePublisher;
            oldPublisher.ChiefPublisher = publisher.ChiefPublisher;
            oldPublisher.ChiefAuthorId = publisher.ChiefAuthorId;
            oldPublisher.ListOfAuthors = publisher.ListOfAuthors;
            oldPublisher.ListOfBooks = publisher.ListOfBooks;

            _storage.Save(_publishers);

            return oldPublisher;
        }

        public Publisher RemovePublisher(int id)
        {
            Publisher removingPublisher = GetPublisherById(id);
            if (removingPublisher == null)
            {
                return null;
            }

            _publishers.Remove(removingPublisher);
            _storage.Save(_publishers);
            return removingPublisher;
        }

        public Publisher? GetPublisherById(int id)
        {
            return _publishers.Find(p => p.Id == id);
        }

        public List<Publisher> GetAllPublishers()
        {
            return _publishers;
        }

        public List<Publisher> GetAllPublishers(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            IEnumerable<Publisher> publishers = _publishers;

            switch (sortCriteria)
            {
                case "Id":
                    publishers = _publishers.OrderBy(x => x.Id);
                    break;
                case "Password":
                    publishers = _publishers.OrderBy(x => x.Password);
                    break;
                case "NameOfThePublisher":
                    publishers = _publishers.OrderBy(x => x.NameOfThePublisher);
                    break;
                case "ChiefPublisher":
                    publishers = _publishers.OrderBy(x => x.ChiefPublisher?.Id);
                    break;
                case "ChiefAuthorId":
                    publishers = _publishers.OrderBy(x => x.ChiefAuthorId);
                    break;
                case "ListOfAuthors":
                    publishers = _publishers.OrderBy(x => x.ListOfAuthors.Count);
                    break;
                case "ListOfBooks":
                    publishers = _publishers.OrderBy(x => x.ListOfBooks.Count);
                    break;
            }

            if (sortDirection == SortDirection.Descending)
            {
                publishers = publishers.Reverse();
            }

            publishers = publishers.Skip((page - 1) * pageSize).Take(pageSize);

            return publishers.ToList();
        }
    }
}
