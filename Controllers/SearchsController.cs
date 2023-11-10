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
        public async Task<IActionResult> All(string key = "Thảo")
        {
            HttpContext.Session.SetString(SessionData.USERID_SESS, userIdTest.ToString());
            HttpContext.Session.SetString(SessionData.USER_EMAIL_SESS, "hoan@gmail.com");

            string emailSess = HttpContext.Session.GetString(SessionData.USER_EMAIL_SESS);
            if (emailSess == null)
            {
                return RedirectToAction("Index", "Users");
            }

            List<ProfileView> people = await _searchContext.GetPeopleByKeySearch(key);
            SearchView view = new SearchView()
            {
                People = people
            };
            return View(view);
        }
    }
}