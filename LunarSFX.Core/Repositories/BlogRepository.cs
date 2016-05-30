using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LunarSFX.Core.Objects;
using NHibernate;
using NHibernate.Linq;

namespace LunarSFX.Core.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private ISession _session;

        public BlogRepository(ISession session)
        {
            _session = session;
        }

        public Category Category(string categorySlug)
        {
            return _session.Query<Category>()
             .FirstOrDefault(t => t.UrlSlug.Equals(categorySlug));
        }

        public IList<Post> Posts(int pageNo, int pageSize)
        {
            var posts = _session.Query<Post>()
                              .Where(p => p.Published)
                              .OrderByDescending(p => p.PostedOn)
                              .Skip(pageNo * pageSize)
                              .Take(pageSize)
                              .Fetch(p => p.Category)
                              .ToList();

            var postIds = posts.Select(p => p.Id).ToList();

            return _session.Query<Post>()
                  .Where(p => postIds.Contains(p.Id))
                  .OrderByDescending(p => p.PostedOn)
                  .FetchMany(p => p.Tags)
                  .ToList();
        }

        public IList<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize)
        {
            var posts = _session.Query<Post>()
                      .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                      .OrderByDescending(p => p.PostedOn)
                      .Skip(pageNo * pageSize)
                      .Take(pageSize)
                      .Fetch(p => p.Category)
                      .ToList();

            var postIds = posts.Select(p => p.Id).ToList();

            return _session.Query<Post>()
                          .Where(p => postIds.Contains(p.Id))
                          .OrderByDescending(p => p.PostedOn)
                          .FetchMany(p => p.Tags)
                          .ToList();
        }

        public int TotalPosts()
        {
            return _session.Query<Post>()
                            .Where(p => p.Published)
                            .Count();
        }

        public int TotalPostsForCategory(string categorySlug)
        {
            return _session.Query<Post>()
                 .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                 .Count();
        }
        public IList<Post> PostsForTag(string tagSlug, int pageNo, int pageSize)
        {
            var posts = _session.Query<Post>()
                              .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                              .OrderByDescending(p => p.PostedOn)
                              .Skip(pageNo * pageSize)
                              .Take(pageSize)
                              .Fetch(p => p.Category)
                              .ToList();

            var postIds = posts.Select(p => p.Id).ToList();

            return _session.Query<Post>()
                          .Where(p => postIds.Contains(p.Id))
                          .OrderByDescending(p => p.PostedOn)
                          .FetchMany(p => p.Tags)
                          .ToList();
        }

        public int TotalPostsForTag(string tagSlug)
        {
            return _session.Query<Post>()
                        .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                        .Count();
        }

        public Tag Tag(string tagSlug)
        {
            return _session.Query<Tag>()
                        .FirstOrDefault(t => t.UrlSlug.Equals(tagSlug));
        }
        public IList<Post> PostsForSearch(string search, int pageNo, int pageSize)
        {
            var posts = _session.Query<Post>()
                                  .Where(p => p.Published && (p.Title.Contains(search) || p.Category.Name.Equals(search) || p.Tags.Any(t => t.Name.Equals(search))))
                                  .OrderByDescending(p => p.PostedOn)
                                  .Skip(pageNo * pageSize)
                                  .Take(pageSize)
                                  .Fetch(p => p.Category)
                                  .ToList();

            var postIds = posts.Select(p => p.Id).ToList();

            return _session.Query<Post>()
                  .Where(p => postIds.Contains(p.Id))
                  .OrderByDescending(p => p.PostedOn)
                  .FetchMany(p => p.Tags)
                  .ToList();
        }

        public int TotalPostsForSearch(string search)
        {
            return _session.Query<Post>()
                    .Where(p => p.Published && (p.Title.Contains(search) || p.Category.Name.Equals(search) || p.Tags.Any(t => t.Name.Equals(search))))
                    .Count();
        }
    }
}
