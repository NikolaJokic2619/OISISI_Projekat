using Core.Models;
using Core.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAO
{
    public class ShopingDAO
    {
        private readonly List<Shoping> _shopings;
        private readonly Storage<Shoping> _storage;

        public ShopingDAO()
        {
            _storage = new Storage<Shoping>("shoping.csv");
            _shopings = _storage.Load();
        }

        private int GenerateID()
        {
            return _shopings.Count == 0 ? 0 : checked(_shopings.Max(a => a.Id) + 1);
        }

        public Shoping AddShoping(Shoping shoping)
        {
            shoping.Id = GenerateID();
            _shopings.Add(shoping);
            _storage.Save(_shopings);
            return shoping;
        }

        public Shoping UpdateShoping(Shoping shoping)
        {
            Shoping oldShoping = GetShopingById(shoping.Id);
            if (oldShoping == null)
            {
                return null;
            }

            oldShoping.Visitor = shoping.Visitor;
            oldShoping.VisitorId = shoping.VisitorId;
            oldShoping.Book = shoping.Book;
            oldShoping.BookId = shoping.BookId;
            oldShoping.DateOfBuying = shoping.DateOfBuying;
            oldShoping.Rating = shoping.Rating;
            oldShoping.Comment = shoping.Comment;

            _storage.Save(_shopings);

            return oldShoping;
        }

        public Shoping RemoveShoping(int id)
        {
            Shoping removingShoping = GetShopingById(id);
            if (removingShoping == null)
            {
                return null;
            }

            _shopings.Remove(removingShoping);
            _storage.Save(_shopings);
            return removingShoping;
        }

        public Shoping? GetShopingById(int id)
        {
            return _shopings.Find(s => s.Id == id);
        }

        public List<Shoping> GetAllShopings()
        {
            return _shopings;
        }

        public List<Shoping> GetAllShopings(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            IEnumerable<Shoping> shopings = _shopings;

            switch (sortCriteria)
            {
                case "Id":
                    shopings = _shopings.OrderBy(x => x.Id);
                    break;
                case "Visitor":
                    shopings = _shopings.OrderBy(x => x.Visitor?.Id);
                    break;
                case "VisitorId":
                    shopings = _shopings.OrderBy(x => x.VisitorId);
                    break;
                case "Book":
                    shopings = _shopings.OrderBy(x => x.Book?.Id);
                    break;
                case "BookId":
                    shopings = _shopings.OrderBy(x => x.BookId);
                    break;
                case "Rating":
                    shopings = _shopings.OrderBy(x => x.Rating);
                    break;
                case "Comment":
                    shopings = _shopings.OrderBy(x => x.Comment);
                    break;
            }

            if (sortDirection == SortDirection.Descending)
            {
                shopings = shopings.Reverse();
            }

            shopings = shopings.Skip((page - 1) * pageSize).Take(pageSize);

            return shopings.ToList();
        }
    }
}
