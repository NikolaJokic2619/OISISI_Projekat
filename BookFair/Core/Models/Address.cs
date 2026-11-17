using Core.Interfaces;

namespace Core.Models;

public class Address : ISerializable
{
    public int Id { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(), Street, Number, City, Country
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        if (values.Length != 5)
        {
            throw new ArgumentException("An address must contain 4 fields.");
        }

        Id = int.Parse(values[0]);
        Street = values[1];
        Number = values[2];
        City = values[3];
        Country = values[4];
    }
}

