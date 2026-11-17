using System;
using System.Collections.Generic;
using System.Text;
using Core.Interfaces;

namespace Core.Models;

public class Shoping : ISerializable
{
    public int Id { get; set; }
    public Visitor Visitor { get; set; }
    public int VisitorId { get; set; }
    public Book Book { get; set; }
    public int BookId { get; set; }
    public DateOnly DateOfBuying { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(), VisitorId.ToString(), BookId.ToString(), DateOfBuying.ToString("dd/MM/yyyy"), Rating.ToString(), Comment
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        if (values.Length != 6)
        {
            throw new ArgumentException("An address must contain 6 fields.");
        }

        Id = int.Parse(values[0]);
        VisitorId = int.Parse(values[1]);
        BookId = int.Parse(values[2]);
        DateOfBuying = DateOnly.Parse(values[3]);
        Rating = int.Parse(values[4]);
        Comment = values[5];
    }
}
