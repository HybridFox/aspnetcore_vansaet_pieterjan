using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcore_vansaet_pieterjan.Models
{
    public class DictionaryModel
    {
        public Dictionary<int, BookDetailModel> Books = new Dictionary<int, BookDetailModel>
        {
            {1, new BookDetailModel {Author = "Pieterjan", ISBN = "78937A8973", Title = "Memes 4 Life" } },
            {2, new BookDetailModel {Author = "Pieterjan", ISBN = "78937A8973", Title = "Memes 5 Life" } },
            {3, new BookDetailModel {Author = "Pieterjan", ISBN = "78937A8973", Title = "Memes 6 Life" } },
        };
    }
}