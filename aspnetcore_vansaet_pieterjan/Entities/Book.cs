﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcore_vansaet_pieterjan.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual List<AuthorBook> Authors { get; set; }
        public string ISBN { get; set; }
        public DateTime CreationDate { get; set; }
        public Genre Genre { get; set; }
    }
}
