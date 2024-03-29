using Clone_Main_Project_0710.Constant;
using Clone_Main_Project_0710.DataSession;
using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Models.ViewModels;
using Clone_Main_Project_0710.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Clone_Main_Project_0710.Controllers
{
    public class SearchsController : Controller
    {
        private SearchsRepository _searchContext;
        public SearchsController(SocialContext context)
        {
            _searchContext = new SearchsRepository(context);
        }
        private Guid userIdTest = new Guid("b2b36a90-0354-4f35-bf8a-d35ae7a42011");
        [HttpGet]
        public async Task<IActionResult> All(string key)
        {
            Guid userId = Guid.Parse(Request.Cookies[UsersCookiesConstant.CookieUserId]);

            string emailSess = Request.Cookies[UsersCookiesConstant.CookieEmail];
            if (emailSess == null)
            {
                return RedirectToAction("Index", "Users");
            }

            List<TypePersonView> people = await _searchContext.GetPeopleByKeySearch(userId, key);
            SearchView view = new SearchView()
            {
                KeySearch = key,
                People = people
            };
            return View(view);
        }
    }
}