using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ItemsController : Controller
    {
        // this makes a Controller property that is the database called _db
        // _db.Items is the Items table
        // _db.Categories is the Categories table
        private readonly ToDoListContext _db;

        // constructor to create a Controller object each time the controller is invoked by loading a URL
        public ItemsController(ToDoListContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View(_db.Items.OrderBy(item => item.DueDate).ToList());
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Item item, int CategoryId)
        {
            _db.Items.Add(item);
            if (CategoryId != 0)
            {
                _db.CategoryItem.Add(new CategoryItem() { CategoryId = CategoryId, ItemId = item.ItemId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
            List<Category> allCategories = _db.Categories.ToList();
            //Creates list from categories database
            List<Category> thisCategories = thisItem.Categories.Select(x => x.Category).ToList();
            List<Category> newList = allCategories.Where(x => !(thisCategories.Any(a => a.Name == x.Name))).ToList();
            //SHould make it where x looks for duplacits in the names of CategoryItem and Category and keep them from being shown in the dropdown list
            ViewBag.CategoryId = new SelectList(newList, "CategoryId", "Name");
            return View(thisItem);
        }

        [HttpPost]
        public ActionResult Edit(Item item, int CategoryId)
        {
            if (CategoryId != 0)
            {
                _db.CategoryItem.Add(new CategoryItem() { CategoryId = CategoryId, ItemId = item.ItemId });
            }
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddCategory(int id)
        {
            Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
            ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
            return View(thisItem);
        }

        [HttpPost]
        public ActionResult AddCategory(Item item, int CategoryId)
        {
            if (CategoryId != 0)
            {
                _db.CategoryItem.Add(new CategoryItem() { CategoryId = CategoryId, ItemId = item.ItemId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Item thisItem = _db.Items
              .Include(item => item.Categories)
                .ThenInclude(join => join.Category)
              .FirstOrDefault(item => item.ItemId == id);
            return View(thisItem);
        }

        [HttpPost]
        public ActionResult Details(Item item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Details");
        }

        public ActionResult Delete(int id)
        {
            Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
            return View(thisItem);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
            _db.Items.Remove(thisItem);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteCategory(int joinId)
        {
            CategoryItem joinEntry = _db.CategoryItem.FirstOrDefault(entry => entry.CategoryItemId == joinId);
            _db.CategoryItem.Remove(joinEntry);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}