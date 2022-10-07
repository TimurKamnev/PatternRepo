using HW39.Books;
using HW39.Interfaces;
using HW39.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HW39.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Book> db;

        public HomeController()
        {
            db = new SQLBookRepository();
        }

        public ActionResult Index()
        {
            return View(db.GetBookList());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Create(book);
                db.Save();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public ActionResult Edit(int id)
        {
            Book book = db.GetBook(id);
            return View(book);
        }
        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Update(book);
                db.Save();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Book b = db.GetBook(id);
            return View(b);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}