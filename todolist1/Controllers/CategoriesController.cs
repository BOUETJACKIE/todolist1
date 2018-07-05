using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using todolist1.Data;
using todolist1.Models;

namespace todolist1.Controllers
{
    public class CategoriesController : ApiController


    {
        // ouverture de la connexion à a bd
        private TodoListDBContext db = new TodoListDBContext();


        [ResponseType(typeof(Category))]
        public IHttpActionResult PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            return Ok();
        }

        // réécriture de la méthode dispose pour libérer en mémoire le db context 
        // et donc la connexion
        // methode dispose appelée lorsque le IIs n'utilise plus le controller 
        protected override void Dispose(bool disposing)
        {
            this.db.Dispose();//libère le db context
            base.Dispose(disposing);
        }
    }
}
