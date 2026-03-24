using Microsoft.AspNetCore.Mvc;

using TPLOCAL1.Models;
using System.Text.RegularExpressions;

//Subject is find at the root of the project and the logo in the wwwroot/ressources folders of the solution
//--------------------------------------------------------------------------------------
//Careful, the MVC model is a so-called convention model instead of configuration,
//The controller must imperatively be name with "Controller" at the end !!!
namespace TPLOCAL1.Controllers
{
    public class HomeController : Controller
    {
        //methode "naturally" call by router
        public ActionResult Index(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                //retourn to the Index view (see routing in Program.cs)
                return View();
            else
            {
                //Call different pages, according to the id pass as parameter
                switch (id)
                {
                    case "OpinionList":
                        // Récupération de la liste des avis à partir du fichier XML
                        OpinionList opinions = new OpinionList();
                        opinions.GetAvis("XlmFile/DataAvis.xml");
                        // Si la vue attend un modèle, il faut le passer en paramètre à View()
                        return View(id, opinions);
                    case "Form":
                        //TODO : call the Form view with data model empty
                        FormInfos formInfos = new();
                        return View(id, formInfos);
                    default:
                        //retourn to the Index view (see routing in Program.cs)
                        return View();
                }
            }
        }


        //methode to send datas from form to validation page
        [HttpPost]
        public ActionResult ValidationFormulaire(/*model*/FormInfos formInfos)
        {
            //TODO : test if model's fields are set
            //if not, display an error message and stay on the form page
            //else, call ValidationForm with the datas set by the user
            formInfos.Nom = Request.Form["nom"];
            formInfos.Prenom = Request.Form["prenom"];
            formInfos.Sexe = Request.Form["sexe"];
            formInfos.Adresse = Request.Form["adresse"];
            formInfos.CodePostal = Request.Form["codePostal"];
            System.Text.RegularExpressions.Regex codePostalRegex = new System.Text.RegularExpressions.Regex(@"^(\w{5})$");
            formInfos.Ville = Request.Form["ville"];            
            formInfos.AdresseMail = Request.Form["mail"];
            System.Text.RegularExpressions.Regex mailRegex = new System.Text.RegularExpressions.Regex(@"^([\w]+)@([\w]+)\.([\w]+)$");            
            formInfos.DateDebut = Request.Form["dateDebut"];
            formInfos.FormationSuivie = Request.Form["formation"];
            formInfos.AvisCobol = Request.Form["avisCobol"];
            formInfos.AvisCSharp = Request.Form["avisCSharp"];

            //validation de l'adresse mail
            if (!mailRegex.IsMatch(formInfos.AdresseMail))
            {
                System.Console.WriteLine("Adresse mail invalide");
                return View("Form");
            }
            //validation du code postal
            else if (!codePostalRegex.IsMatch(formInfos.CodePostal))
            {
                System.Console.WriteLine("Code postal invalide");
                return View("Form");
            }
            //si tout est bon on passe à la page de validation
            else
            {
                return View("Validation", model: formInfos);
            }
        }
    }
}