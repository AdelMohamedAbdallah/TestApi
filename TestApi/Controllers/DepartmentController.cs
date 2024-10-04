using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TestApi.Models;


namespace TestApi.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly HttpClient http;

        public DepartmentController(HttpClient http)
        {
            this.http = http;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var response = await http.GetAsync("https://localhost:44349/api/Department/GetDepartments");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<IEnumerable<Department>>(content);
                return View(data);
            }
            return View($"Error: {response.StatusCode}");
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await http.GetAsync($"https://localhost:44349/api/Department/GetDepartmentById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<Department>(content);

                return View(data);
            }

            return View($"Error : {response.StatusCode}");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = JsonConvert.SerializeObject(department);

                    var responsedata = new StringContent(data, Encoding.UTF8, "application/json");

                    var response = await http.PostAsync("https://localhost:44349/api/Department/PostDepartment", responsedata);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetDepartments");
                    }
                    return View($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                return View($"Error : {ex.Message}");
            }


            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var response = await http.GetAsync($"https://localhost:44349/api/Department/GetDepartmentById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<Department>(content);

                return View(data);
            }

            return View($"Error : {response.StatusCode}");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = JsonConvert.SerializeObject(department);

                    var responsedata = new StringContent(data, Encoding.UTF8, "application/json");

                    var response = await http.PutAsync("https://localhost:44349/api/Department/PutDepartment", responsedata);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetDepartments");
                    }

                    return View($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                return View($"Error : {ex.Message}");
            }

            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await http.GetAsync($"https://localhost:44349/api/Department/GetDepartmentById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<Department>(content);

                return View(data);
            }

            return View($"Error : {response.StatusCode}");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = JsonConvert.SerializeObject(department);
                    var content = new StringContent(data, Encoding.UTF8, "application/json");
                    var response = await http.SendAsync(new HttpRequestMessage(HttpMethod.Delete, "https://localhost:44349/api/Department/DeleteDepartment")
                    {
                        Content = content
                    });

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetDepartments");
                    }
                    return View($"Error : {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                return View($"Error : {ex.Message}");
            }
            return View(department);
        }
    }
}
