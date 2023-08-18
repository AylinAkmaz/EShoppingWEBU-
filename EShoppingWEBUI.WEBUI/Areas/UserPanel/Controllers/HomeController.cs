using EShoppingWEBUI.Core.ViewModel;
using EShoppingWEBUI.Helper.SessionHelper;
using Microsoft.AspNetCore.Mvc;

namespace EShoppingWEBUI.WEBUI.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("/User/Anasayfa")]
        public IActionResult Index()
        {
            
            return View();

        }
    }
}
