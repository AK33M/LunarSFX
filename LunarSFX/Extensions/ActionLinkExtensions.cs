using LunarSFX.Core.Objects;
using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace LunarSFX.Extensions
{
    public static class ActionLinkExtensions
    {
        public static MvcHtmlString PostLink(this HtmlHelper helper, Post post)
        {
            return helper.ActionLink(post.Title, "Post", "Blog",
                new
                {
                    year = post.PostedOn.Year,
                    month = post.PostedOn.Month,
                    title = post.UrlSlug
                },
                new
                {
                    title = post.Title
                });
        }

        public static MvcHtmlString CategoryLink(this HtmlHelper helper,
            Category category)
        {
            return helper.ActionLink(string.Format("@{0}",category.Name), "Category", "Blog",
                new
                {
                    category = category.UrlSlug
                },
                new
                {
                    title = string.Format("See all posts in {0}", category.Name)
                });
        }

        public static MvcHtmlString TagLink(this HtmlHelper helper, Tag tag)
        {
            return helper.ActionLink(string.Format("#{0} ", tag.Name), "Tag", "Blog", new { tag = tag.UrlSlug },
                new
                {
                    title = string.Format("See all posts in {0}", tag.Name)
                });
        }
    }
}
