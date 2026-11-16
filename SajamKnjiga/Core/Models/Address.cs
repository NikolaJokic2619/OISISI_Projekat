using Core.Interfaces;

namespace Core.Models;

public class Address : ISerializable
{
    public string Street { get; set; }
    public string Number { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Street, Number, City, Country
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        if (values.Length != 4)
        {
            throw new ArgumentException("An address must contain 4 fields.");
        }

        Street = values[0];
        Number = values[1];
        City = values[2];
        Country = values[3];
    }
}

