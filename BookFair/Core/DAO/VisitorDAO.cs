using Core.Models;
using Core.Storage;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;

// Source: Material taken from OISISI course lesson 3, CRUDExample
namespace Core.DAO
{
    public class VisitorDAO
    {
        private readonly List<Visitor> _visitors;
        private readonly Storage<Visitor> _storage;

        public VisitorDAO()
        {
            _storage = new Storage<Visitor>("visitor.txt");
            _visitors = _storage.Load();
        }

        private int GenerateID()
        {
            if (_visitors.Count == 0)
            {
                return 0;
            }
            return _visitors[^1].Id + 1;
        }

        public Visitor AddVisitor(Visitor visitor)
        {
            visitor.Id = GenerateID();
            _visitors.Add(visitor);
            _storage.Save(_visitors);
            return visitor;
        }

        public Visitor UpdateVisitor(Visitor visitor)
        {
            Visitor oldVisitor = GetVisitorById(visitor.Id);
            if (oldVisitor == null)
            {
                return null;
            }

            oldVisitor.Name = visitor.Name;
            oldVisitor.Surname = visitor.Surname;
            oldVisitor.DateOfBirth = visitor.DateOfBirth;
            oldVisitor.Address = visitor.Address;
            oldVisitor.TelephoneNumber = visitor.TelephoneNumber;
            oldVisitor.Email = visitor.Email;
            oldVisitor.NumberOfMembershipCard = visitor.NumberOfMembershipCard;
            oldVisitor.CurrentYearOfMembership = visitor.CurrentYearOfMembership;
            oldVisitor.Status = visitor.Status;
            oldVisitor.AverageRating = visitor.AverageRating;
            oldVisitor.ListOfBoughtBooks = visitor.ListOfBoughtBooks;

            _storage.Save(_visitors);

            return oldVisitor;
        }

        public Visitor RemoveVisitor(int id)
        {
            Visitor removingVisitor = GetVisitorById(id);
            if (removingVisitor == null)
            {
                return null;
            }

            _visitors.Remove(removingVisitor);
            _storage.Save(_visitors);
            return removingVisitor;
        }

        private Visitor GetVisitorById(int id)
        {
            return _visitors.Find(v => v.Id == id);
        }

        public List<Visitor> GetAllVisitors()
        {
            return _visitors;
        }

        public List<Visitor> GetAllVisitors(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            IEnumerable<Visitor> visitors = _visitors;

            switch (sortCriteria)
            {
                case "Id":
                    visitors = _visitors.OrderBy(x => x.Id);
                    break;
                case "Name":
                    visitors = _visitors.OrderBy(x => x.Name);
                    break;
                case "Surname":
                    visitors = _visitors.OrderBy(x => x.Name);
                    break;
                case "DateOfBirth":
                    visitors = _visitors.OrderBy(x => x.DateOfBirth);
                    break;
                case "Address":
                    visitors = _visitors.OrderBy(x => x.Address);
                    break;
                case "TelephoneNumber":
                    visitors = _visitors.OrderBy(x => x.TelephoneNumber);
                    break;
                case "Email":
                    visitors = _visitors.OrderBy(x => x.Email);
                    break;
                case "NumberOfMembershipCard":
                    visitors = _visitors.OrderBy(x => x.NumberOfMembershipCard);
                    break;
                case "CurrentYearOfMembership":
                    visitors = _visitors.OrderBy(x => x.CurrentYearOfMembership);
                    break;
                case "Status":
                    visitors = _visitors.OrderBy(x => x.Status);
                    break;
                case "AverageRating":
                    visitors = _visitors.OrderBy(x => x.AverageRating);
                    break;
                case "ListOfBoughtBooks":
                    visitors = _visitors.OrderBy(x => x.ListOfBoughtBooks);
                    break;
            }

            if (sortDirection == SortDirection.Descending)
            {
                visitors = visitors.Reverse();
            }

            visitors = visitors.Skip((page - 1) * pageSize).Take(pageSize);

            return visitors.ToList();
        }

    }
}
