using System;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21534421_HW03.Models
{
    public class CombinedViewModel
    {
        public IPagedList<book> Book { get; set; }
        public IPagedList<student> Student { get; set; }
        public IPagedList<borrow> Borrow { get; set; }
    }
}