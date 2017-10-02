using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcore_vansaet_pieterjan.Models
{
    public class BookDetailModel
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTime CreatedOn => DateTime.Now;
    }
}
