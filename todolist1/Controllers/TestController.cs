using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml.Linq;

namespace todolist1.Controllers
{
    public class TestModel
    {
        public int id { get; set; }
        public string Commentaire { get; set; }
    }
    public class TestController : ApiController
    {
        // GET: api/test
        public List<TestModel> GetTest()
        {
            List<TestModel> liste = new List<TestModel>();
            /*liste.Add(new TestModel { id = 42, Commentaire = "la réponse" });
            liste.Add(new TestModel { id = 39, Commentaire = "températeur actuelle" });
            liste.Add(new TestModel { id = 28, Commentaire = "au hasard" });
            liste.Add(new TestModel { id = 19, Commentaire = "gel" });*/

            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));


            /*return (from x in doc.Descendants("Test")
                    select new TestModel
                    { id = int.Parse(x.Element("id").Value),
                        Commentaire = x.Element("Commentaire").Value }).ToList();*/

            var elements = doc.Root.Elements();
            foreach (var items in elements)
            {
                var test = new TestModel
                {
                    id = int.Parse(items.Element("id").Value),
                    Commentaire = items.Element("Commentaire").Value
                };
                liste.Add(test);
            }

            return liste;

        }
        //Get: api/test/42
        [ResponseType(typeof(TestModel))]
        public IHttpActionResult GetTest(int id)
        {
            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));

            //TestModel test = null;

            /*var elements = doc.Root.Elements();
            foreach (var items in elements)
            {

                if (int.Parse(items.Element("id").Value) == id)
                {
                    test = new TestModel
                    {
                        id = int.Parse(items.Element("id").Value),
                        Commentaire = items.Element("Commentaire").Value
                    };
                }*/


            var test = doc.Descendants("Test").SingleOrDefault(
                x => int.Parse(x.Element("id").Value) == id);



            if (test == null)

            {
                return NotFound();
            }
            return Ok(new TestModel
            {
                id = int.Parse(test.Element("id").Value),
                Commentaire = test.Element("commentaire").Value
            });
        }

        [ResponseType(typeof(TestModel))]
        public IHttpActionResult PostTest(TestModel test)
        {//recupérer le doc xml
            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));


            //chercher la valeur max des id des éléments "TEST"

            var testfinal = doc.Descendants("Test").Max(
                x => int.Parse(x.Element("id").Value));
            //créer variable nouvel id=max+1
            test.id = ++testfinal;
            // création de la variable xelement
            var test3 = new XElement("Test");
            //créer xelement "id" et "commentaire", leur ajouter des valeurs
            test3.Add(new XElement("id", test.id));
            test3.Add(new XElement("Commentaire", test.Commentaire));
            //ajouter le nouvel élément ds le fichier xml

            doc.Element("Tests").Add(test3);
            //sauvegarder le doc
            doc.Save(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));
            //renvoyer code 201 avec élément enregistré 

            return CreatedAtRoute("DefaultApi", new { id = test.id }, test);
        }

        /* if (test.id != 0)
         {
             return BadRequest();
         }
         test.id = 101;
         return CreatedAtRoute("DefaultApi", new { id = test.id }, test);
     }*/
        [ResponseType(typeof(TestModel))]
        public IHttpActionResult PutTest(int id, TestModel test)
        {//recupérer le doc xml
            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));
            //tester l'id et id de test et retourner 401 si faux
            if (test.id != id)
            {
                return BadRequest();
            }
            // recherche de l'élément en fct de l'id 
            var idtest = doc.Descendants("Test").SingleOrDefault(
                x => int.Parse(x.Element("id").Value) == id);

            if (idtest == null)
            {
                return BadRequest();
            }
            // modifier les valeurs avec celles du test 
            idtest.Element("Commentaire").Value = test.Commentaire;

            //sauvegarder le doc
            doc.Save(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));

            //retourner le code 204

            return StatusCode(HttpStatusCode.NoContent);
        }
        [ResponseType(typeof(TestModel))]
        public IHttpActionResult DeleteTest(int id)
        {//recupérer le doc xml
            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));

            // recherche de l'élément en fct de l'id 

            var idsup = doc.Descendants("Test").SingleOrDefault(
                x => int.Parse(x.Element("id").Value) == id);

            idsup.Remove();
            //sauvegarder le doc
            doc.Save(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));



            return Ok("élément supprimé");




        }
    }
}


//post: api/test


