using System;
using System.Collections.Generic;
using System.Text;
using Core.Interfaces;

namespace Core.Models;

public class Publisher : ISerializable
{
    public int Id { get; set; }
    public string Password { get; set; }
    public string NameOfThePublisher { get; set; }
    public Author ChiefPublisher { get; set; }
    public int ChiefAuthorId { get; set; }
    public List<Author> ListOfAuthors { get; set; } = new List<Author>();
    public List<Book> ListOfBooks { get; set; } = new List<Book>();


    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(), Password, NameOfThePublisher, ChiefAuthorId.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        if (values.Length != 4)
        {
            throw new ArgumentException("An address must contain 4 fields.");
        }

        Id = int.Parse(values[0]);
        Password = values[1];
        NameOfThePublisher = values[2];
        ChiefAuthorId = int.Parse(values[3]);
    }
}
