using LunarSFX.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LunarSFX.Controllers
{
    [Authorize(Roles ="SuperAdmin")]
    public class UserController : Controller
    {
        private readonly AppRoleManager _roleManager;
        private readonly AppUserManager _userManager;

        public UserController(AppRoleManager roleManager, AppUserManager userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ContentResult> EditUser(UserViewModel user)
        {
            string json;

            //user.Roles Not a list or rather a list of one comma seperated string
            var incRoles = user.Roles.FirstOrDefault().Split(',').ToArray();

            var deletedRoles = _userManager.GetRolesAsync(user.Id).Result.Except(incRoles);

            var addedRoles = incRoles.Except(_userManager.GetRolesAsync(user.Id).Result);

            var userDb = _userManager.FindByIdAsync(user.Id).Result;

            if (userDb != null)
            {
                userDb.Email = user.Email;
                userDb.UserName = user.UserName;
                await _userManager.RemoveFromRolesAsync(userDb.Id, deletedRoles.ToArray());
                await _userManager.AddToRolesAsync(userDb.Id, addedRoles.ToArray());
                await _userManager.UpdateAsync(userDb);
            }

            ModelState.Clear();

            if (ModelState.IsValid)
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = userDb.Id,
                    success = true,
                    message = "Changes saved successfully."
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to save the changes."
                });
            }

            return Content(json, "application/json");
        }

        [HttpPost]
        public ContentResult DeleteUser(string Id)
        {
            var user = _userManager.FindByIdAsync(Id).Result;

            _userManager.DeleteAsync(user);

            var json = JsonConvert.SerializeObject(new
            {
                id = 0,
                success = true,
                message = "User deleted successfully."
            });

            return Content(json, "application/json");
        }

        public async Task<ContentResult> Users()
        {
            var listofusers = new List<UserViewModel>();

            var users = _userManager.Users.ToList();

            foreach (var user in users)
            {
                listofusers.Add(new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = await _userManager.GetRolesAsync(user.Id)
                });
            }

            return Content(JsonConvert.SerializeObject(new
            {
                page = 1,
                records = listofusers.Count,
                rows = listofusers,
                total = 1
            }), "application/json");
        }

        public ContentResult GetRolesHtml()
        {
            var roles = _roleManager.Roles.ToList();

            var sb = new StringBuilder();
            sb.AppendLine(@"<select>");

            foreach (var role in roles)
            {
                sb.AppendLine(string.Format(@"<option value=""{0}"">{1}</option>",
                    role.Name, role.Name));
            }

            sb.AppendLine("</select>");
            return Content(sb.ToString(), "text/html");
        }
    }
}
