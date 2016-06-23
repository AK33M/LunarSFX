using LunarSFX.Core.Objects;
using LunarSFX.Core.Repositories;
using LunarSFX.Extensions;
using LunarSFX.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace LunarSFX.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private IBlogRepository _blogRepository;
        private readonly AppRoleManager _roleManager;
        private readonly AppUserManager _userManager;

        public AdminController(IBlogRepository blogRepository, AppRoleManager roleManager, AppUserManager userManager)
        {
            _blogRepository = blogRepository;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: Admin
        public ActionResult Manage()
        {
            return View();
        }

        public ContentResult Posts(JqInViewModel jqParams)
        {
            var posts = _blogRepository.Posts(jqParams.page - 1, jqParams.rows, jqParams.sidx, jqParams.sord == "asc");

            var totalPosts = _blogRepository.TotalPosts(false);

            return Content(JsonConvert.SerializeObject(new
            {
                page = jqParams.page,
                records = totalPosts,
                rows = posts,
                total = Math.Ceiling(Convert.ToDouble(totalPosts) / jqParams.rows)
            }, new CustomDateTimeConverter()), "application/Json");
        }

        public ContentResult Categories()
        {
            var categories = _blogRepository.Categories();

            return Content(JsonConvert.SerializeObject(new
            {
                page = 1,
                records = categories.Count,
                rows = categories,
                total = 1
            }), "application/json");
        }

        [HttpPost]
        public ContentResult AddCategory([Bind(Exclude = "Id")]Category category)
        {
            string json;

            if (ModelState.IsValid)
            {
                var id = _blogRepository.AddCategory(category);
                json = JsonConvert.SerializeObject(new
                {
                    id = id,
                    success = true,
                    message = "Category added successfully."
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to add the category."
                });
            }

            return Content(json, "application/json");
        }

        [HttpPost, ValidateInput(false)]
        public ContentResult AddPost(Post post)
        {
            string json;

            ModelState.Clear();

            if (TryValidateModel(post))
            {
                var id = _blogRepository.AddPost(post);

                json = JsonConvert.SerializeObject(new
                {
                    id = id,
                    success = true,
                    message = "Post added successfully."
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to add the post."
                });
            }

            return Content(json, "application/json");
        }

        [HttpPost, ValidateInput(false)]
        public ContentResult EditPost(Post post)
        {
            string json;

            ModelState.Clear();

            if (TryValidateModel(post))
            {
                _blogRepository.EditPost(post);
                json = JsonConvert.SerializeObject(new
                {
                    id = post.Id,
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
        public ContentResult DeletePost(int id)
        {
            _blogRepository.DeletePost(id);

            var json = JsonConvert.SerializeObject(new
            {
                id = 0,
                success = true,
                message = "Post deleted successfully."
            });

            return Content(json, "application/json");
        }

        [HttpPost]
        public ContentResult EditCategory(Category category)
        {
            string json;

            if (ModelState.IsValid)
            {
                _blogRepository.EditCategory(category);
                json = JsonConvert.SerializeObject(new
                {
                    id = category.Id,
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
        public ContentResult DeleteCategory(int id)
        {
            _blogRepository.DeleteCategory(id);

            var json = JsonConvert.SerializeObject(new
            {
                id = 0,
                success = true,
                message = "Category deleted successfully."
            });

            return Content(json, "application/json");
        }

        public ContentResult Tags()
        {
            var tags = _blogRepository.Tags();

            return Content(JsonConvert.SerializeObject(new
            {
                page = 1,
                records = tags.Count,
                rows = tags,
                total = 1
            }), "application/json");
        }

        [HttpPost]
        public ContentResult AddTag([Bind(Exclude = "Id")]Tag tag)
        {
            string json;

            if (ModelState.IsValid)
            {
                var id = _blogRepository.AddTag(tag);
                json = JsonConvert.SerializeObject(new
                {
                    id = id,
                    success = true,
                    message = "Tag added successfully."
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to add the tag."
                });
            }

            return Content(json, "application/json");
        }

        [HttpPost]
        public ContentResult EditTag(Tag tag)
        {
            string json;

            if (ModelState.IsValid)
            {
                _blogRepository.EditTag(tag);
                json = JsonConvert.SerializeObject(new
                {
                    id = tag.Id,
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
        public ContentResult DeleteTag(int id)
        {
            _blogRepository.DeleteTag(id);

            var json = JsonConvert.SerializeObject(new
            {
                id = 0,
                success = true,
                message = "Tag deleted successfully."
            });

            return Content(json, "application/json");
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ContentResult> EditUser(UserViewModel user)
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

        public async System.Threading.Tasks.Task<ContentResult> Users()
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

        public ContentResult GetCategoriesHtml()
        {
            var categories = _blogRepository.Categories();

            var sb = new StringBuilder();
            sb.AppendLine(@"<select>");

            foreach (var category in categories)
            {
                sb.AppendLine(string.Format(@"<option value=""{0}"">{1}</option>",
                    category.Id, category.Name));
            }

            sb.AppendLine("</select>");
            return Content(sb.ToString(), "text/html");
        }

        public ContentResult GetTagsHtml()
        {
            var tags = _blogRepository.Tags();

            var sb = new StringBuilder();
            sb.AppendLine(@"<select multiple=""multiple"">");

            foreach (var tag in tags)
            {
                sb.AppendLine(string.Format(@"<option value=""{0}"">{1}</option>",
                    tag.Id, tag.Name));
            }

            sb.AppendLine("</select>");
            return Content(sb.ToString(), "text/html");
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