using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Shoping
    {
        public int Id { get; set; }
        public Visitor Visitor { get; set; }
        public Book Book { get; set; }
        private DateOnly _dateOfBuying { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

    }
}
