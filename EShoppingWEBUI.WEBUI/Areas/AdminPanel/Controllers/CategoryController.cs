using System.Net;
using EShoppingWEBUI.Core.DTO;
using EShoppingWEBUI.Core.Result;
using EShoppingWEBUI.Helper.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace EShoppingWEBUI.WEBUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CategoryController : Controller
    {
        [HttpGet("/Admin/Kategori")]
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

        [HttpPost("/Admin/AddCategory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(CategoryDTO category)
        {
            var url = "http://localhost:5256/AddCategory";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer" + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(category);
            request.AddBody(body, "application/json");
            RestResponse restResponse = await client.ExecuteAsync(request);


            var responseObject = JsonConvert.DeserializeObject<APIResult<CategoryDTO>>(restResponse.Content);

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

        [HttpGet("/Admin/Kategori{categoryGUID}")]
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

        [HttpPost("/Admin/UpdateCategory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCategory(CategoryDTO category)
        {
            var url = "http://localhost:5256/UpdateCategory";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer" + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(category);
            request.AddBody(body, "application/json");
            RestResponse restResponse = await client.ExecuteAsync(request);


            var responseObject = JsonConvert.DeserializeObject<APIResult<CategoryDTO>>(restResponse.Content);


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

        [HttpPost("/Admin/RemoveCategory/{categoryGUID}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveCategory(Guid categoryGUID)
        {

            var url = "http://localhost:5256/RemoveCategory" + categoryGUID;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer" + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);


            var responseObject = JsonConvert.DeserializeObject<APIResult<bool>>(restResponse.Content);


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
