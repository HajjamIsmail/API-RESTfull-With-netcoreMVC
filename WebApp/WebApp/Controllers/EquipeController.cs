using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class EquipeController : Controller
    {
        public ActionResult Index()
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
                }
                else //web api sent error response 
                {
                    //log response status here..
                    equipe = Enumerable.Empty<Equipe>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(equipe);
        }
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(Equipe equipe)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7253/api/Equipes");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Equipe>("Equipes", equipe);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(equipe);
        }
        public ActionResult Edit(int id)
        {
            Equipe equipe = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7253/api/Equipes/");
                //HTTP GET
                var responseTask = client.GetAsync(string.Format("{0}", id.ToString()));
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Equipe>();
                    readTask.Wait();
                    equipe = readTask.Result;
                }
            }

            return View(equipe);
        }

        [HttpPost]
        public ActionResult Edit(Equipe equipe)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7253/api/Equipes");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Equipe>("Equipes", equipe);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(equipe);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7253/api/Equipes/");
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
