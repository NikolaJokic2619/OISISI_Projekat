using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Publisher
    {
        public string Password { get; set; }
        public string NameOfThePublisher { get; set; }
        public string ChiefPublisher { get; set; }
        public List<Author> ListOfAuthors { get; set; } = new List<Author>();
        public List<Book> ListOfBooks { get; set; } = new List<Book>();
    }
}
