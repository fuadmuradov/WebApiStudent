using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebMvcStudent.Models;

namespace WebMvcStudent.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<FileGetDto> files = new List<FileGetDto>();
            using (var httpClient = new HttpClient())
            {
                //  httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.GetAsync("https://localhost:44328/api/files"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    files = JsonConvert.DeserializeObject<List<FileGetDto>>(apiResponse);
                }
            }
            return View(files);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Privacy(FilePostDto filePost)
        {
            using (var httpClient = new HttpClient())
            {
              //  StringContent content = new StringContent(JsonConvert.SerializeObject(filePost), Encoding.UTF8, "application/json");
                var form = new MultipartFormDataContent();
                using (var fileStream = filePost.Files.OpenReadStream())
                {
                    form.Add(new StreamContent(fileStream), "Files", filePost.Files.FileName);
                    using (var response = await httpClient.PostAsync("https://localhost:44328/api/files", form))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }

            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
