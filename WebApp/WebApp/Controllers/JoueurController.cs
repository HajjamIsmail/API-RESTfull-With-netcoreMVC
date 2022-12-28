using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics.Metrics;
using WebApp.Models;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

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
                    ViewBag.message = equipe.ToList();
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

        public ActionResult Edit(int id)
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
                    ViewBag.message = equipe.ToList();
                }
                else //web api sent error response 
                {
                    //log response status here..
                    equipe = Enumerable.Empty<Equipe>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            /////////////////////////////
            Joueur joueur = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7253/api/Joueurs/");
                //HTTP GET
                var responseTask = client.GetAsync(string.Format("{0}", id.ToString()));
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Joueur>();
                    readTask.Wait();
                    joueur = readTask.Result;
                }
            }

            return View(joueur);
        }

        [HttpPost]
        public ActionResult Edit(Joueur joueur)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7253/api/Joueurs");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Joueur>("Joueurs", joueur);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(joueur);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7253/api/Joueurs/");
                //HTTP DELETE
                var deleteTask = client.DeleteAsync(string.Format("{0}", id.ToString()));
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return RedirectToAction("Index");
        }
    }
}
