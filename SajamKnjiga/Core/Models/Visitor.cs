using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Core.Models{
    public enum Status { P, B}
    public class Visitor{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Address Address { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }
        public string NumberOfMembershipCard { get; set; }
        public int CurrentYearOfMembership { get; set; }
        public Status Status { get; set; }
        public double AverageRating { get; set; }
        public List<Shoping> ListOfBoughtBooks { get; set; } = new List<Shoping>();
    };
    
}
