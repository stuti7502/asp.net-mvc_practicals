﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Practical19_DataAccessLayer.Model;
using Practical19_Views.Models;
using System.Diagnostics;
using System.Net;

namespace Practical19_Views.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("studentApi");
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllUser()
        {
            if (Request.Cookies["Email"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var token = Request.Cookies["userToken"].ToString();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync("User/Users");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = await response.Content.ReadAsStringAsync();
                List<RegisteredUser> users = JsonConvert.DeserializeObject<List<RegisteredUser>>(data);
                return View(users);
            }
            return View(new List<RegisteredUser>());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}