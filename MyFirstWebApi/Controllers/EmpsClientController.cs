using MyFirstWebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyFirstWebApi.Controllers
{
    public class EmpsClientController : Controller
    {
         HttpClient client;
         string url = "http://localhost:5667/api/Emps";
         public EmpsClientController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
        }
        // GET: EmpsClient
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetEmps");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var Employees = JsonConvert.DeserializeObject<List<Emp>>(responseData);
                return View(Employees);
            }
            return View();
        }
         public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "EmpId,EmpName,EmpAddress,EmpSal,EmpGender")] Emp emp)
        {
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url + "/PostEmp", emp);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
        // GET: /EmpCRUDUsingAjaxJQueryJSON/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetEmp/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Employee = JsonConvert.DeserializeObject<Emp>(responseData);

                return View(Employee);
            }
            return RedirectToAction("Error");
        }
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "EmpId,EmpName,EmpAddress,EmpSal,EmpGender")] Emp emp,int id)
        {
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/PutEmp/" + id, emp);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/DeleteEmp/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
    }
}