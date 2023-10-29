using System;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21534421_HW03.Models
{
    public class CombinedViewModel2
    {
        public IPagedList<author> Author { get; set; }
        public IPagedList<borrow> Borrow { get; set; }
        public IPagedList<type> Type { get; set; }
    }
}