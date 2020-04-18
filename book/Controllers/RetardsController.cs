using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using book.Models;
using System.Globalization;
using System.Linq;
using System.Net;

namespace book.Controllers
{
    public class RetardsController : Controller
    {
        
        public ActionResult Ajouter()
        {
            return View();
        }

        public ActionResult Modifier()
        {
            return View();
        }

        public ActionResult Afficher(FormCollection collection)
    {
        bool trouve = false;
        String uneLigne;
        Retard nouvRetard = new Retard();
        String[] unAttribut;
        Char unDelimiteur = ',';
        var listeEnRetard = new List<Retard> { };
        var tousLesRetards = new List<Retard> { };
        if (System.IO.File.Exists(Server.MapPath(@"~/App_Data/prets.txt")))
        {

            StreamReader fichierRetards =
                new StreamReader(Server.MapPath(@"~/App_Data/prets.txt"));
            while ((uneLigne = fichierRetards.ReadLine()) != null)
            {
                unAttribut = uneLigne.Split(unDelimiteur);
                nouvRetard = new Retard()
                {

                    Code = unAttribut[0], Prenom = unAttribut[1], Courriel = unAttribut[2],
                    Livre1 = unAttribut[3], Livre2 = unAttribut[4], Livre3 = unAttribut[5], DateEmprunt = unAttribut[6], DateRetour = unAttribut[7],

                };


                listeEnRetard.Add(nouvRetard);

            }
        }

        var aujourdhui = DateTime.Now.ToString("d");
            DateTime maintenant = DateTime.ParseExact(aujourdhui, "d", null);
                foreach (var retard in listeEnRetard)
                {
                    DateTime emprunt = DateTime.ParseExact(retard.DateEmprunt.Trim(), "d", null);
                    if ((maintenant - emprunt).TotalDays > 7)
                    {
                        try
                        {
                            
                                string line = " ";
                                using (StreamWriter retards =
                                    new StreamWriter(
                                        Server.MapPath(@"~/App_Data/retards.txt"), true))
                                {

                                    line += retard.Code + ",";
                                    line += retard.Prenom + ",";
                                    line += retard.Courriel + ",";
                                    line += retard.Livre1 + ",";
                                    line += retard.Livre2 + ",";
                                    line += retard.Livre3 + ",";
                                    line += retard.DateEmprunt + ",";
                                    bool isMatch = false;
                                    string[] lines =
                                        System.IO.File.ReadAllLines(
                                            Server.MapPath(@"~/App_Data/retards.txt")); // as you are not using it here
                                    for (int x = 0; x < lines.Length; x++)
                                    {
                                        Console.WriteLine(lines[x]);
                                        if (lines[x].Contains(retard.Code.Trim()) & lines[x].Contains(retard.Prenom.Trim()) & lines[x].Contains(retard.Courriel.Trim()) & lines[x].Contains(retard.Livre1.Trim()) & lines[x].Contains(retard.Livre2.Trim()) & lines[x].Contains(retard.Livre3.Trim()) & lines[x].Contains(retard.DateEmprunt.Trim()))
                                        {
                                            Console.WriteLine(retard.Code.Trim());
                                            Console.WriteLine("there is a match");
                                            isMatch = true;
                                            break; // exit loop if found
                                        }
                                    }

                                    if (isMatch == false)
                                    {
                                        retards.WriteLine(line);
                                        Console.WriteLine(retard.Code.Trim());
                                        Console.WriteLine("there is no match");
                                        
                                        
                                } 
                                tousLesRetards.Add(retard);
                                
                            }
                            
                        }
                        catch

                        {
                            return Content("ERREUR");
                        }
                    }
                }
                return View(tousLesRetards);
        }
        
        }
    
    }


