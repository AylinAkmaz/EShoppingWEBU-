using System.Net;
using EShoppingWEBUI.Core.DTO;
using EShoppingWEBUI.Core.Result;
using EShoppingWEBUI.Helper.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace EShoppingWEBUI.WEBUI.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    public class CategoryController : Controller
    {
        [HttpGet("/User/Kategori")]
        public async Task<IActionResult> Index()
        {
            var url = "http://localhost:5256/Categories";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer" + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);


            var responseObject = JsonConvert.DeserializeObject<APIResult<List<CategoryDTO>>>(restResponse.Content);

            var categories = responseObject.Data;

            return View(categories);
        }



        [HttpGet("/User/Kategori{categoryGUID}")]
        public async Task<IActionResult> GetCategory(Guid categoryGUID)
        {
            var url = "http://localhost:5256/Category/" + categoryGUID;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer" + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);


            var responseObject = JsonConvert.DeserializeObject<APIResult<CategoryDTO>>(restResponse.Content);

            var category = responseObject.Data;

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return Json(new { success = true, data = responseObject.Data });
            }
            else if (restResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                return Json(new { success = false, HataBilgisi = responseObject.HataBilgisi });
            }
            else
            {
                return Json(new { success = false, HataBilgisi = responseObject.HataBilgisi });
            }
        }

    }
}

