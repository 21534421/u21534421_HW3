using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using u21534421_HW03.Models;
using PagedList;

namespace u21534421_HW03.Controllers
{
    public class HomeController : Controller
    {

        private LibraryEntities db = new LibraryEntities();

        public async Task<ActionResult> CombinedIndex(int? page, int? page2, int? page3)
        {
            int pagesize = 10;
            int pagenumber = (page ?? 1);
            int pagenumber2 = (page2 ?? 1);
            int pagenumber3 = (page3 ?? 1);
            var x = await db.books.Include(b => b.author).Include(b => b.type).ToListAsync();
            var y = await db.students.ToListAsync();
            var z = await db.borrows.Include(b => b.book.author).Include(b => b.book.type).Include(b => b.student).ToListAsync();
            var viewModel = new CombinedViewModel
            {
                Book = x.ToPagedList(pagenumber, pagesize),
                Student = y.ToPagedList(pagenumber2, pagesize),
                Borrow = z.ToPagedList(pagenumber3, pagesize)
            };

            return View(viewModel);
        }

        public async Task<ActionResult> CombinedIndex2(int? page, int? page2, int? page3)
        {
            int pagesize = 10;
            int pagenumber = (page ?? 1);
            int pagenumber2 = (page2 ?? 1);
            int pagenumber3 = (page3 ?? 1);
            var x = await db.authors.ToListAsync();
            var y = await db.borrows.Include(b => b.book).Include(b => b.student).ToListAsync();
            var z = await db.types.ToListAsync();

            var viewModel2 = new CombinedViewModel2
            {

                Author = x.ToPagedList(pagenumber, pagesize),
                Borrow = y.ToPagedList(pagenumber2, pagesize),
                Type = z.ToPagedList(pagenumber3, pagesize)
            };

            return View(viewModel2);
        }

        public ActionResult Report()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public JsonResult ChartData()
        {
            var report = db.borrows.GroupBy(r => r.book.type.name).Select(group => new
            {
                TypeName = group.Key,
                Count = group.Count()
            }).ToList();

            var chartData = new
            {
                TypeName = report.Select(r => r.TypeName).ToArray(),
                Count = report.Select(r => r.Count).ToArray()
            };

            return Json(chartData, JsonRequestBehavior.AllowGet);
        }
    }
    
}