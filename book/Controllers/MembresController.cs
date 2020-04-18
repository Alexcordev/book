using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using book.Models;

namespace book.Controllers
{
    public class MembresController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult liste(int id)
        {
            return View();
        }

        public ActionResult listerTout()
        {
            String ligne;

            Membre unMembre = new Membre();
            String[] attributs;
            Char delimiteur = ',';
            var listeMembres = new List<Membre> { };
            if (System.IO.File.Exists(Server.MapPath(@"~/App_Data/membres.txt")))
            {
                // Lecture ligne par ligne 
                StreamReader ficMembres =
                    new StreamReader(Server.MapPath(@"~/App_Data/membres.txt"));
                while ((ligne = ficMembres.ReadLine()) != null)
                {
                    attributs = ligne.Split(delimiteur);
                    unMembre = new Membre() { Image= attributs[0], Prenom = attributs[1], Courriel = attributs[2], Telephone = attributs[3], Commentaire = attributs[4] };
                    listeMembres.Add(unMembre);
                }

                ficMembres.Close();
            }
            return View(listeMembres);
        }

        public ActionResult Ajout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult enregistrer(FormCollection collection)
        {
            int taille = collection.Count;
            String ligne = "";
            string imageUrl = "https://www.pngitem.com/pimgs/m/516-5167304_transparent-background-white-user-icon-png-png-download.png";
            try
            {
               using (StreamWriter ficMembres =
               new StreamWriter(Server.MapPath(@"~/App_Data/membres.txt"), true))
               {
                   ligne += imageUrl + ",";
                   ligne += Convert.ToString(collection["Prenom"]) + ",";
                   ligne += Convert.ToString(collection["Courriel"]) + ",";
                   ligne += Convert.ToString(collection["Telephone"]) + ",";
                   ligne += Convert.ToString(collection["Commentaire"]);
                   ficMembres.WriteLine(ligne);
               }
               return RedirectToAction("listerTout");
               
            }
            catch
            {
               return Content("Il s'est produit une erreur");//avec les messages d'erreur appropriés
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
                string courriel = Convert.ToString(collection["Courriel"]);
                bool trouve = false;
                string chemin = Server.MapPath(@"~/App_Data/membres.txt");
                using (StreamReader fichier = new StreamReader(chemin, true))
                {
                    string ligne;                    
                    string[] attributs = null;
                    char delimiteur = ',';
                    Membre unMembre = new Membre();                    
                    while ((ligne = fichier.ReadLine()) != null && !trouve)
                    {
                        attributs = ligne.Split(delimiteur);
                        if (String.Equals(attributs[2], courriel))
                        {
                            trouve = true;                            
                            unMembre = new Membre() { Image = attributs[0], Prenom = attributs[1], Courriel = attributs[2], Telephone = attributs[3], Commentaire = attributs[4] };
                            
                        }
                            
                    }
                    fichier.Close();
                    if (trouve)
                    {
                        Effacer(collection);
                        return View(unMembre);                        
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


        public ActionResult Modifier(Membre unMembre)
        {
            return View(unMembre); 
        }     

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Effacer(FormCollection collection)
        {
            string ligne = "";
            string chemin = Server.MapPath(@"~/App_Data/membres.txt");
           
            string courriel = Convert.ToString(collection["Courriel"]);
            try
            {
                bool trouve = false;
                using (StreamReader fichier = new StreamReader(chemin, true))
                {                    
                    string[] elems = null;
                    char delimiteur = ',';
                    using (StreamWriter fichierTemp = new StreamWriter(Server.MapPath(@"~/App_Data/membresTemp.txt"), true))
                    {
                        while ((ligne = fichier.ReadLine()) != null)
                        {
                            elems = ligne.Split(delimiteur);
                            if (!String.Equals(elems[2], courriel))
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
                System.IO.File.Move(Server.MapPath(@"~/App_Data/membresTemp.txt"), chemin);
                if (!trouve)
                {
                    
                    return View("Error"); 
                }
                return RedirectToAction("listerTout");               
            }
            catch (IOException e)
            {
                return Content("Erreur");       
               
            }           
        }
    }
}