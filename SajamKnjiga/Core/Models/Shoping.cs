using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models;

public class Shoping
{
    public int Id { get; set; }
    public Visitor Visitor { get; set; }
    public int VisitorId { get; set; }
    public Book Book { get; set; }
    public int BookId { get; set; }
    private DateOnly dateOfBuying { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(), VisitorId.ToString(), BookId.ToString(), dateOfBuying.ToString("dd/MM/yyyy"), Rating.ToString(), Comment
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        if (values.Length != 6)
        {
            throw new ArgumentException("An address must contain 4 fields.");
        }

        Id = int.Parse(values[0]);
        VisitorId = int.Parse(values[1]);
        BookId = int.Parse(values[2]);
        dateOfBuying = DateOnly.Parse(values[3]);
        Rating = int.Parse(values[4]);
        Comment = values[5];
    }
}
