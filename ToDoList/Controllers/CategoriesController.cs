using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class CategoriesController : Controller
    {
        // this makes a Controller property that is the database called _db
        // _db.Items is the Items table
        // _db.Categories is the Categories table
        private readonly ToDoListContext _db;

        // constructor to create a Controller object each time the controller is invoked by loading a URL
        public CategoriesController(ToDoListContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            // pulls all data from the Category table of your database. Turns it into a List for ease of use
            List<Category> model = _db.Categories.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Category thisCategory = _db.Categories
              .Include(category => category.Items)
                .ThenInclude(join => join.Item)
              .FirstOrDefault(category => category.CategoryId == id);
            return View(thisCategory);
        }

        public ActionResult Edit(int id)
        {
            Category thisCategory = _db.Categories.FirstOrDefault(category => category.CategoryId == id);
            return View(thisCategory);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            _db.Entry(category).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //         public ActionResult Delete(int id)
        //         {
        //             Category thisCategory = _db.Categories.FirstOrDefault(category => category.CategoryId == id);
        //             return View(thisCategory);
        //         }

        //         [HttpPost, ActionName("Delete")]
        //         public ActionResult DeleteConfirmed(int id)
        //         {
        //             Category thisCategory = _db.Categories.FirstOrDefault(category => category.CategoryId == id);
        //             _db.Categories.Remove(thisCategory);
        //             _db.SaveChanges();
        //             return RedirectToAction("Index");
        //         }
    }
}