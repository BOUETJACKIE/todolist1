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

            db.Categories.Add(category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category.ID }, category);
        }


        public List<Category> GetCategories()
        {


            return db.Categories.ToList();

        }

        public IHttpActionResult GetCategory (int id)
        {
            var category = db.Categories.Find(id);

            if ( category == null)

            {
                return NotFound();
            }
            return Ok (category);

        }
        public IHttpActionResult PutCategory(int id, Category category)
        {

           
            if (id!=category.ID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)

            {
                return BadRequest(ModelState);
            }

                db.Entry(category).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();

            }
            catch(Exception)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
                
        }
        public IHttpActionResult DeleteCategory(int id)
        {
            var cateSup = db.Categories.Find(id);
            if (cateSup == null)
                return NotFound();

            db.Categories.Remove(cateSup);
            db.SaveChanges();



            return Ok("élément supprimé");
        }
        
        // réécriture de la méthode dispose pour libérer en mémoire le db context 
        // et donc la connexion
        // methode dispose appelée lorsque le IIs n'utilise plus le controller 
        protected override void Dispose(bool disposing)
        {
            db.Dispose();//libère le db context
            base.Dispose(disposing);
        }
    }
}
