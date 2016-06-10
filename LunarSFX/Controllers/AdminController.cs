﻿using LunarSFX.Core.Repositories;
using LunarSFX.Extensions;
using LunarSFX.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }

    public class BlogRepository
    {
    }
}