using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NMS.WEB.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NMS.WEB.Controllers
{
    public class NoteController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<VmNote> notes = new List<VmNote>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:37757/api/Note/GetAll"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    notes = JsonConvert.DeserializeObject<List<VmNote>>(apiResponse);
                }
            }
            return View(notes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VmNote model)
        {
            VmNote receivedReservation = new VmNote();
            model.Id = Guid.NewGuid();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:37757/api/Note/Create", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedReservation = JsonConvert.DeserializeObject<VmNote>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:37757/api/Note/Delete/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
