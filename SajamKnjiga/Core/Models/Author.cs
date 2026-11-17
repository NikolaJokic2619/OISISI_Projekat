using Core.Interfaces;


namespace Core.Models;

public class Author : ISerializable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Address Address { get; set; }
    public int AddressId { get; set; }
    public string TelephoneNumber { get; set; }
    public string Email { get; set; }
    public string IdentityCardNumber { get; set; }
    public int YearsOfExperience { get; set; }
    public List<Book> Books { get; set; } = new();

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(), Name, Surname, DateOfBirth.ToString("dd/MM/yyyy"), AddressId.ToString(), TelephoneNumber, Email, IdentityCardNumber, YearsOfExperience.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        if (values.Length != 9)
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
        IdentityCardNumber = values[7];
        YearsOfExperience = int.Parse(values[8]);
    }
}
