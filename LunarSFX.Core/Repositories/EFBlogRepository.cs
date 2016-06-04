using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LunarSFX.Core.Objects;

namespace LunarSFX.Core.Repositories
{
    public class EFBlogRepository : IBlogRepository
    {
        private readonly LunarSFXDbContext _context;

        public EFBlogRepository(LunarSFXDbContext context)
        {
            _context = context;
        }
        public IList<Category> Categories()
        {
            return _context.Categories.OrderBy(p => p.Name).ToList();
        }

        public Category Category(string categorySlug)
        {
            return _context.Categories.FirstOrDefault(t => t.UrlSlug.Equals(categorySlug));
        }

        public Post Post(int year, int month, string titleSlug)
        {
            return _context.Posts.Where(p => p.PostedOn.Year == year && p.PostedOn.Month == month && p.UrlSlug.Equals(titleSlug)).Single();
        }

        public IList<Post> Posts(int pageNo, int pageSize)
        {
            return _context.Posts.Where(x => x.Published).OrderByDescending(x => x.PostedOn).Skip(pageNo * pageSize).Take(pageSize).ToList();
        }

        public IList<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize)
        {
            var posts = _context.Posts.Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                .OrderByDescending(p => p.PostedOn)
                .Skip(pageNo * pageSize)
                      .Take(pageSize)
                      .ToList();

            var postIds = posts.Select(p => p.Id).ToList();

            return _context.Posts.Where(p => postIds.Contains(p.Id)).OrderByDescending(p => p.PostedOn).ToList();
        }

        public IList<Post> PostsForSearch(string search, int pageNo, int pageSize)
        {
            var posts = _context.Posts
                                  .Where(p => p.Published && (p.Title.Contains(search) || p.Category.Name.Equals(search) || p.Tags.Any(t => t.Name.Equals(search))))
                                  .OrderByDescending(p => p.PostedOn)
                                  .Skip(pageNo * pageSize)
                                  .Take(pageSize)
                                  .ToList();

            var postIds = posts.Select(p => p.Id).ToList();

            return _context.Posts
                  .Where(p => postIds.Contains(p.Id))
                  .OrderByDescending(p => p.PostedOn)
                  .ToList();
        }

        public IList<Post> PostsForTag(string tagSlug, int pageNo, int pageSize)
        {
            var posts = _context.Posts
                              .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                              .OrderByDescending(p => p.PostedOn)
                              .Skip(pageNo * pageSize)
                              .Take(pageSize)
                              .ToList();

            var postIds = posts.Select(p => p.Id).ToList();

            return _context.Posts
                          .Where(p => postIds.Contains(p.Id))
                          .OrderByDescending(p => p.PostedOn)
                          .ToList();
        }

        public Tag Tag(string tagSlug)
        {
            return _context.Tags
                        .FirstOrDefault(t => t.UrlSlug.Equals(tagSlug));
        }

        public IList<Tag> Tags()
        {
            return _context.Tags.OrderBy(p => p.Name).ToList();
        }

        public int TotalPosts()
        {
            return _context.Posts
                            .Where(p => p.Published)
                            .Count();
        }

        public int TotalPostsForCategory(string categorySlug)
        {
            return _context.Posts
                 .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                 .Count();
        }

        public int TotalPostsForSearch(string search)
        {
            return _context.Posts
                    .Where(p => p.Published && (p.Title.Contains(search) || p.Category.Name.Equals(search) || p.Tags.Any(t => t.Name.Equals(search))))
                    .Count();
        }

        public int TotalPostsForTag(string tagSlug)
        {
            return _context.Posts
                        .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                        .Count();
        }
    }
}
