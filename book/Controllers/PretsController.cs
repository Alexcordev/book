using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;
using book.Models;
using static System.DateTime;

namespace book.Controllers
{
    
    public class PretsController : Controller
    {
      public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult Liste(int id)
        {
            return View();
        }

        public ActionResult ListerPrets()
        {
            String ligne;

            Pret unPret = new Pret();
            String[] attributs;
            Char delimiteur = ',';
            var listePrets = new List<Pret> { };
            
            if (System.IO.File.Exists(Server.MapPath(@"~/App_Data/prets.txt")))
            {
                // Lecture ligne par ligne 
                StreamReader ficPrets =
                    new StreamReader(Server.MapPath(@"~/App_Data/prets.txt"));
                while ((ligne = ficPrets.ReadLine()) != null)
                {
                    
                    attributs = ligne.Split(delimiteur);
                    unPret = new Pret()
                    {
                        
                        Code = attributs[0], Prenom = attributs[1], Courriel = attributs[2],
                        Livre1 = attributs[3], Livre2 = attributs[4], Livre3 = attributs[5], DateEmprunt = attributs[6], DateRetour = attributs[7]
                        
                    };
                    listePrets.Add(unPret);
                    
                }

                ficPrets.Close();
            }

            return View(listePrets);
        }

        public ActionResult Ajouter()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Save(FormCollection collection)
        {
            int taille = collection.Count;
            String ligne = "";
            try
            {
                using (StreamWriter ficPrets =
                    new StreamWriter(Server.MapPath(@"~/App_Data/prets.txt"), true))
                {
                    ligne += Convert.ToString(collection["Code"]) + ",";
                    ligne += Convert.ToString(collection["Prenom"]) + ",";
                    ligne += Convert.ToString(collection["Courriel"]) + ",";
                    ligne += Convert.ToString(collection["Livre1"]) + ",";
                    ligne += Convert.ToString(collection["Livre2"]) + ",";
                    ligne += Convert.ToString(collection["Livre3"]) + ",";
                    ligne += Convert.ToString(collection["DateEmprunt"]) + ",";
                    ligne += Convert.ToString(collection["DateRetour"]);
                    
                    ficPrets.WriteLine(ligne);
                }

                return RedirectToAction("ListerPrets");
                
            }
            catch
            {
                return Content("ERREUR"); //avec les messages d'erreur appropriés
            }
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Editer(FormCollection collection)
        {
            try
            {
                string code = Convert.ToString(collection["Code"]);
                bool trouve = false;
                string chemin = Server.MapPath(@"~/App_Data/prets.txt");
                using (StreamReader fichier = new StreamReader(chemin, true))
                {
                    string ligne;
                    string[] attributs = null;
                    char delimiteur = ',';
                    Pret unPret = new Pret();
                    while ((ligne = fichier.ReadLine()) != null && !trouve)
                    {
                        attributs = ligne.Split(delimiteur);
                        if (String.Equals(attributs[0], code))
                        {
                            trouve = true;
                            unPret = new Pret()
                            {
                                Code = attributs[0], Prenom = attributs[1], Courriel = attributs[2],
                                Livre1 = attributs[3], Livre2 = attributs[4], Livre3 = attributs[5], DateEmprunt = attributs[6], DateRetour = attributs[7]
                            };

                        }

                    }

                    fichier.Close();
                    if (trouve)
                    {
                        Effacer(collection);
                        return View(unPret);
                    }
                    else
                    {
                        return View("Error");
                    }
                }

            }
            catch
            {
                return View("Error");
            }
        }


        public ActionResult Modifier(Pret unPret)
        {
            return View(unPret);
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Effacer(FormCollection collection)
        {
            string ligne = "";
            string chemin = Server.MapPath(@"~/App_Data/prets.txt");

            string code = Convert.ToString(collection["Code"]);
            try
            {
                bool trouve = false;
                using (StreamReader fichier = new StreamReader(chemin, true))
                {
                    string[] elems = null;
                    char delimiteur = ',';
                    using (StreamWriter fichierTemp =
                        new StreamWriter(Server.MapPath(@"~/App_Data/pretsTemp.txt"), true))
                    {
                        while ((ligne = fichier.ReadLine()) != null)
                        {
                            elems = ligne.Split(delimiteur);
                            if (!String.Equals(elems[0], code))
                            {
                                fichierTemp.WriteLine(ligne);
                            }
                            else
                            {
                                trouve = true;
                            }
                        }

                        fichierTemp.Close();
                    }

                    fichier.Close();
                }

                System.IO.File.Delete(chemin);
                System.IO.File.Move(Server.MapPath(@"~/App_Data/pretsTemp.txt"), chemin);
                if (!trouve)
                {

                    return View("Error");
                }

                return RedirectToAction("ListerPrets");
            }
            catch (System.IO.IOException e)
            {
                
                return Content("Erreur");

            }
        }

        
    }

}