using Core.Models;
using Core.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAO
{
    public class AddressDAO
    {
        private readonly List<Address> _addresses;
        private readonly Storage<Address> _storage;

        public AddressDAO()
        {
            _storage = new Storage<Address>("address.txt");
            _addresses = _storage.Load();
        }

        private int GenerateID()
        {
            if (_addresses.Count == 0)
            {
                return 0;
            }
            return _addresses[^1].Id + 1;
        }

        public Address AddAddress(Address address)
        {
            address.Id = GenerateID();
            _addresses.Add(address);
            _storage.Save(_addresses);
            return address;
        }

        public Address UpdateAddress(Address address)
        {
            Address oldAddress = GetAddressById(address.Id);
            if (oldAddress == null)
            {
                return null;
            }

            oldAddress.Street = address.Street;
            oldAddress.Number = address.Number;
            oldAddress.City = address.City;
            oldAddress.Country = address.Country;
            
            _storage.Save(_addresses);

            return oldAddress;
        }

        public Address RemoveAddress(int id)
        {
            Address removingAddress = GetAddressById(id);
            if (removingAddress == null)
            {
                return null;
            }

            _addresses.Remove(removingAddress);
            _storage.Save(_addresses);
            return removingAddress;
        }

        private Address GetAddressById(int id)
        {
            return _addresses.Find(v => v.Id == id);
        }

        public List<Address> GetAllAddresses()
        {
            return _addresses;
        }

        public List<Address> GetAllAddresses(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            IEnumerable<Address> addresses = _addresses ;

            switch (sortCriteria)
            {
                case "Id":
                    addresses = _addresses.OrderBy(x => x.Id);
                    break;
                case "Street":
                    addresses = _addresses.OrderBy(x => x.Street);
                    break;
                case "Number":
                    addresses = _addresses.OrderBy(x => x.Number);
                    break;
                case "City":
                    addresses = _addresses.OrderBy(x => x.City);
                    break;
                case "Country":
                    addresses = _addresses.OrderBy(x => x.Country);
                    break;
            }

            if (sortDirection == SortDirection.Descending)
            {
                addresses = addresses.Reverse();
            }

            addresses = addresses.Skip((page - 1) * pageSize).Take(pageSize);

            return addresses.ToList();
        }
    }
}
