using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SalesTransactionApp.Models;
using SalesTransactionApp.Services.CoreServices;
using SalesTransactionApp.Services.Interface;
using System.Text;
using SalesTransact = SalesTransactionApp.Models.SalesTransact;

namespace SalesTransactionApp.Controllers
{
    public class SalesTransactController : Controller
    {
        private readonly HttpClient _httpClient;

       

        public SalesTransactController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("SalesApiClient");
        }


        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetStringAsync("api/sales");
            var transactions = JsonConvert.DeserializeObject<List<SalesTransactionViewModel>>(response);
            return View(transactions);
        }

        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(SalesTransactionViewModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("api/sales", content);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
       
        public async Task<IActionResult> Update(SalesTransactionViewModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"api/sales/{model.TransactionId}", content);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _httpClient.DeleteAsync($"api/sales/{id}");
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> Search(string itemName, DateTime? salesDate, string paymentType)
        {
            try
            {
                var queryString = $"?itemName={itemName}&salesDate={salesDate?.ToString("yyyy-MM-dd")}&paymentType={paymentType}";
                var response = await _httpClient.GetAsync($"api/sales/search{queryString}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var results = JsonConvert.DeserializeObject<IEnumerable<SalesTransact>>(jsonResponse);

                    return View(results); 
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
    }
}
