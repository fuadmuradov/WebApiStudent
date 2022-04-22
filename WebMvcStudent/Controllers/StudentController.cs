using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebMvcStudent.Models;

namespace WebMvcStudent.Controllers
{
    public class StudentController : Controller
    {
        public string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6Imphc29uX2FkbWluIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoiamFzb24uYWRtaW5AZW1haWwuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZ2l2ZW5uYW1lIjoiSmFzb24iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zdXJuYW1lIjoiQnJ5YW50IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW5pc3RyYXRvciIsImV4cCI6MTY1MDU0MTY5NSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzMjgvIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzMjgvIn0.u2pUZF2PTGnkIKmu7agpj6Wwss07V_oIKX-fYH65xDQ";
            public async Task<IActionResult> StudentGet()
        {
            List<StudentGetDto> students = new List<StudentGetDto>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.GetAsync("https://localhost:44328/api/students"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    students = JsonConvert.DeserializeObject<List<StudentGetDto>>(apiResponse);
                }
            }
            return View(students);
        }


        public async Task<IActionResult> StudentCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> StudentCreate(StudentPostDto studentPost)
        {
            StudentGetDto student = new StudentGetDto();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                StringContent content = new StringContent(JsonConvert.SerializeObject(studentPost), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:44328/api/students", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    student = JsonConvert.DeserializeObject<StudentGetDto>(apiResponse);
                }

            }

            return RedirectToAction(nameof(StudentGet), "Student");
        }


        public async Task<IActionResult> StudentEdit(int id)
        {
            StudentGetDto student = new StudentGetDto();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44328/api/students/get/"+id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    student = JsonConvert.DeserializeObject<StudentGetDto>(apiResponse);
                }
            }

            StudentPostDto studentPostDto = new StudentPostDto()
            {
                FullName = student.Fullname,
                GroupId = student.GroupId
            };

            TempData["studentId"] = id;
            return View(studentPostDto);
        }

        [HttpPost]
        public async Task<IActionResult> StudentEdit( StudentPostDto studentPost)
        {

            int id = Convert.ToInt32(TempData["studentId"]);
            StudentGetDto student = new StudentGetDto();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                StringContent content = new StringContent(JsonConvert.SerializeObject(studentPost), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44328/api/students/Update/" + id.ToString(), content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    student = JsonConvert.DeserializeObject<StudentGetDto>(apiResponse);
                }
            }
           return RedirectToAction(nameof(StudentGet), "Student");
        }




        public async Task<IActionResult> StudentDelete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.DeleteAsync("https://localhost:44328/api/students/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction(nameof(StudentGet), "Student");
        }

    }
}
