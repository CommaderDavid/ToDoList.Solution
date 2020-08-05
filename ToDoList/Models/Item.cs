namespace ToDoList.Models
{
    public class Item
    {
        // We add set; to the line below.
        public string Description { get; set; }

        public Item(string description)
        {
            Description = description;
        }
    }
}