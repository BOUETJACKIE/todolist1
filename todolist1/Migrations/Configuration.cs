using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using todolist1.Data;
using System.Data.Entity.Migrations;



namespace todolist1.Migrations
{
    public class Configuration : DbMigrationsConfiguration<TodoListDBContext>

    {
        public Configuration()
        { AutomaticMigrationsEnabled = false;
        }
    }
}