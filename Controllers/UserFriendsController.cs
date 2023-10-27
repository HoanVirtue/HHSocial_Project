using Clone_Main_Project_0710.DataSession;
using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Repository;
using HHSocialNetwork_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;

namespace Clone_Main_Project_0710.Controllers
{
    public class UserFriends : Controller
    {
        private readonly SocialContext _context;
        public UserFriends(SocialContext context)
        {
            _context=context;
        }
        [HttpGet]
        public async Task<IActionResult>  ConfirmAcceptFriendsRequest (Guid senderId ,string type)
        {   
            
            if (type== "accept"){
            //xu ly xac nhan       
             Guid userId = Guid.Parse(HttpContext.Session.GetString(SessionData.USERID_SESS)); 
             var friendrequest =await _context.UserFriends.FirstOrDefaultAsync(fr =>fr.TargetId ==userId &&fr.SourceId==senderId&& !fr.IsFriend);
             if(friendrequest != null && userId !=null){
                    friendrequest.IsFriend=true;
                    friendrequest.UpdatedAt=DateTime.Now;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("ProfileEdit","Users");
             }

            }
            else if(type=="delete"){
            // xu ly xoa
    
            }
            return View();
        }
    }
}