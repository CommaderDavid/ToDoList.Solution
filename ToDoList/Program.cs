using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ToDoList 
{
    public class Program 
    {
        public static void Main (string[] args) 
        {
            var host = new WebHostBuilder ()
                .UseKestrel ()
                .UseContentRoot (Directory.GetCurrentDirectory ())
                .UseIISIntegration ()
                .UseStartup<Startup> ()
                .Build ();

            host.Run ();
        }
    }
}
// using System;
// using System.Collections.Generic;
// using ToDoList.Models;

// namespace ToDoList {
//     public class Program {
//         public static void Main () {
//             Console.WriteLine ("Welcome to your To Do List!");

//             Console.WriteLine ("Would you like to add or view your List? (Add/View)");
//             string editToDoList = Console.ReadLine ().ToLower ();

//             if (editToDoList == "add") {
//                 Console.WriteLine ("What do you wish to add?");
//                 string description = Console.ReadLine ();
//                 Item item = new Item (description);
//                 Main ();
//             } else if (editToDoList == "view") {
//                 Console.WriteLine ("Your to do list:");
//                 List<Item> items = Item.GetAll ();
//                 foreach (Item toDo in items) {
//                     Console.WriteLine (toDo.Description);
//                 }
//                 Console.WriteLine ("");
//                 Main ();
//             } else {
//                 Console.WriteLine ("You don't have anything on your to do list. Try again");
//                 Main ();
//             }
//         }
//     }
// }