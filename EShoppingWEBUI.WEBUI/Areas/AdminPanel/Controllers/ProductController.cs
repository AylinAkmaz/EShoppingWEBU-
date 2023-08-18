using System.Net;
using EShoppingWEBUI.Core.DTO;
using EShoppingWEBUI.Core.Result;
using EShoppingWEBUI.Core.ViewModel;
using EShoppingWEBUI.Helper.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using RestSharp;

namespace EShoppingWEBUI.WEBUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("/Admin/Ürünler")]
        public async Task<IActionResult> Index()
        {
            ProductViewModel productViewModel = new()
            {
                Products = await GetProductList(),
                Categories = await GetCategoryList()
            };

            return View(productViewModel);
    
        }
        [HttpGet("/Admin/Ürün/{productGUID}")]
        public async Task<IActionResult>GetProduct(Guid productGUID)
        {
            var url = "http://localhost:5256/Product/" + productGUID;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer" + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);


            var responseObject = JsonConvert.DeserializeObject<APIResult<ProductDTO>>(restResponse.Content);
            
            var product = responseObject.Data;

            return Json(new { success= true, product });

        }


        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/AddProduct")]
        public async Task<IActionResult> AddProduct(ProductDTO productDTO, IFormFile productImage)
        {
            var category = await GetCategory(productDTO.CategoryGuid);


            if (productImage!=null)
            {
                string fileName= productImage.FileName.Split('.')[productImage.FileName.Split('.').Length-2]+"_"+Guid.NewGuid()+"."+ productImage.FileName.Split('.')[productImage.FileName.Split('.').Length - 1];
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "MediaUpload", fileName);

                using (var fileStream = new FileStream(uploadFolder, FileMode.Create))
                {
                    productImage.CopyTo(fileStream);
                };
                productDTO.FeaturedImage = fileName;

                var url = "http://localhost:5256/AddProduct";
                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer" + SessionManager.LoggedUser.Token);
                var body = JsonConvert.SerializeObject(productDTO);
                request.AddBody(body, "application/json");
                RestResponse restResponse = await client.ExecuteAsync(request);


                var responseObject = JsonConvert.DeserializeObject<APIResult<bool>>(restResponse.Content, new JsonSerializerSettings());


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



            return Json(new { });


        }



        public async Task<List<ProductDTO>> GetProductList()
        {
            var url = "http://localhost:5256/Products";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer" + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);


            var responseObject = JsonConvert.DeserializeObject<APIResult<List<ProductDTO>>>(restResponse.Content);
            var products = responseObject.Data;

            return products;
        }
        public async Task<List<CategoryDTO>> GetCategoryList()
        {
            var url = "http://localhost:5256/Categories";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer" + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);


            var responseObject = JsonConvert.DeserializeObject<APIResult<List<CategoryDTO>>>(restResponse.Content);
            var categories = responseObject.Data;

            return categories;
        }

        public async Task<CategoryDTO> GetCategory(Guid? categoryGuid)
        {
            var url = "http://localhost:5256/Category/" + categoryGuid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer" + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);


            var responseObject = JsonConvert.DeserializeObject<APIResult<CategoryDTO>>(restResponse.Content, new JsonSerializerSettings());

            var category = responseObject.Data;

            return category;
        }

    }

}
