using System.Collections.Generic;
using System;

namespace ToDoList.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool ItemComplete { get; set; }
        public virtual ICollection<CategoryItem> Categories { get; }

        public Item()
        {
            this.Categories = new HashSet<CategoryItem>();
        }
    }
}