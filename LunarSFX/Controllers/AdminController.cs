using LunarSFX.Core.Objects;
using LunarSFX.Core.Repositories;
using LunarSFX.Extensions;
using LunarSFX.Models;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace LunarSFX.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private IBlogRepository _blogRepository;

        public AdminController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
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

        [HttpPost]
        public ContentResult AddPost(Post post)
        {
            string json;

            if (ModelState.IsValid)
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
    }
}