using LunarSFX.Core.Objects;
using LunarSFX.Core.Repositories;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LunarSFX
{
    public class PostModelBinder : DefaultModelBinder
    {
        private readonly Container _container;

        public PostModelBinder(Container container)
        {
            _container = container;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var post = (Post)base.BindModel(controllerContext, bindingContext);

            var _blogRepository = _container.GetInstance<IBlogRepository>();

            if (post.Category != null)
            {
                post.Category = _blogRepository.Category(post.Category.Id);
                post.CategoryId = post.Category.Id;
            }

            var tags = bindingContext.ValueProvider.GetValue("Tags").AttemptedValue.Split(',');

            if (tags.Length > 0)
            {
                post.Tags = new List<Tag>();

                foreach (var tag in tags)
                {
                    post.Tags.Add(_blogRepository.Tag(int.Parse(tag.Trim())));
                }
            }

            if (bindingContext.ValueProvider.GetValue("oper").AttemptedValue.Equals("edit"))
                post.Modified = DateTime.UtcNow;
            else
                post.PostedOn = DateTime.UtcNow;

            return post;
        }
    }
}