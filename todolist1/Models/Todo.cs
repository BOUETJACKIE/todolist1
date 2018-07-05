using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace todolist1.Models
{   [Table(name:"Todos")]
    public class Todo
    {
        public int ID { get; set; }
        //
        public string Name { get; set; }
        public bool Done { get; set; }
        public DateTime DeadLineDate { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }

        [ForeignKey("CategoryID")]
        public Category Category { get; set; }
        public int CategoryID { get; set; }


    }

}