using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Core.Models
{
    public enum Status { P, B } // P-redovan B-vip
    public class Visitor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Address Address { get; set; }
        public int AddressId { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }
        public string NumberOfMembershipCard { get; set; }
        public int CurrentYearOfMembership { get; set; }
        public Status Status { get; set; }
        public double AverageRating { get; set; }
        public List<Shoping> ListOfBoughtBooks { get; set; } = new List<Shoping>();
        public List<Book> WishList { get; set; }



        public string[] ToCSV()
        {
            string StatusString;
            if (Status == Status.P)
                StatusString = "P";
            else
                StatusString = "B";

            string[] csvValues =
            {
                Id.ToString(),
                Name,
                Surname,
                DateOfBirth.ToString("dd/MM/yyyy"),
                AddressId.ToString(),
                TelephoneNumber,
                Email,
                NumberOfMembershipCard,
                CurrentYearOfMembership.ToString(),
                StatusString,
                AverageRating.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            if (values.Length != 11)
            {
                throw new ArgumentException("An address must contain 4 fields.");
            }

            Id = int.Parse(values[0]);
            Name = values[1];
            Surname = values[2];
            DateOfBirth = DateOnly.Parse(values[3]);
            AddressId = int.Parse(values[4]);
            TelephoneNumber = values[5];
            Email = values[6];
            NumberOfMembershipCard = values[7];
            CurrentYearOfMembership = int.Parse(values[8]);
            if (values[9] == "P")
                Status = Status.P;
            else
                Status = Status.B;
            AverageRating = double.Parse(values[10]);
        }
    }
}
