﻿using System.Security.Claims;
using System.Web.Mvc;

namespace LunarSFX.Controllers
{
    public abstract class AppController : Controller
    {
        public AppUserPrincipal CurrentUser
        {
            get
            {
                return new AppUserPrincipal(this.User as ClaimsPrincipal);          
            }
        }
    }
}