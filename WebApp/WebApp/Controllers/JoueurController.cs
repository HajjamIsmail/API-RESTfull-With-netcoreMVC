using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics.Metrics;
using WebApp.Models;
using System.Linq;

namespace WebApp.Controllers
{
    public class JoueurController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<Joueur> joueur = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7253/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Joueurs");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Joueur>>();
                    readTask.Wait();
                    joueur = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    joueur = Enumerable.Empty<Joueur>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(joueur);
        }

        public ActionResult create()
        {
            IEnumerable<Equipe> equipe = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7253/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Equipes");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Equipe>>();
                    readTask.Wait();
                    equipe = readTask.Result;
                    List<Equipe> listE = equipe.ToList();
                    ViewBag.message = listE;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    equipe = Enumerable.Empty<Equipe>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View();
        }
        [HttpPost]
        public ActionResult create(Joueur joueur)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7253/api/Joueurs");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Joueur>("Joueurs", joueur);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(joueur);
        }
    }
}
