using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using todolist1.Models;

namespace todolist1.Data
{
    public class TodoListDBContext : DbContext
    {
        public  TodoListDBContext(): base("TodoList")
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Todo> Todos { get; set; }

    }
}