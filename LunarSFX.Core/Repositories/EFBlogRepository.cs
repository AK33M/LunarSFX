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

        public int AddPost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
            return post.Id;            
        }

        public void EditPost(Post post)
        {
            _context.Entry(post).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        public void DeletePost(int id)
        {
            var post = Post(id);
            _context.Entry(post).State = System.Data.Entity.EntityState.Deleted;
            _context.SaveChanges();
        }

        public Post Post(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == id);
        }

        public IList<Category> Categories()
        {
            return _context.Categories.OrderBy(p => p.Name).ToList();
        }

        public Category Category(string categorySlug)
        {
            return _context.Categories.FirstOrDefault(t => t.UrlSlug.Equals(categorySlug));
        }

        public Category Category(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public int AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category.Id;
        }

        public Post Post(int year, int month, string titleSlug)
        {
            return _context.Posts.Where(p => p.PostedOn.Year == year && p.PostedOn.Month == month && p.UrlSlug.Equals(titleSlug)).Single();
        }

        public IList<Post> Posts(int pageNo, int pageSize)
        {
            return _context.Posts.Where(x => x.Published).OrderByDescending(x => x.PostedOn).Skip(pageNo * pageSize).Take(pageSize).ToList();
        }

        public IList<Post> Posts(int pageNo, int pageSize, string sortColumn, bool sortByAscending)
        {
            IList<Post> posts;
            IList<int> postIds;

            switch (sortColumn)
            {
                case "Title":
                    if (sortByAscending)
                    {
                        posts = _context.Posts
                                        .OrderBy(p => p.Title)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                        //.Fetch(p => p.Category)
                                        .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _context.Posts
                                          .Where(p => postIds.Contains(p.Id))
                                          .OrderBy(p => p.Title)
                                         // .FetchMany(p => p.Tags)
                                          .ToList();
                    }
                    else
                    {
                        posts = _context.Posts
                                        .OrderByDescending(p => p.Title)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                        //.Fetch(p => p.Category)
                                        .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _context.Posts
                                          .Where(p => postIds.Contains(p.Id))
                                          .OrderByDescending(p => p.Title)
                                          //.FetchMany(p => p.Tags)
                                          .ToList();
                    }
                    break;
                case "Published":
                    if (sortByAscending)
                    {
                        posts = _context.Posts
                                        .OrderBy(p => p.Published)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                        //.Fetch(p => p.Category)
                                        .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _context.Posts
                                          .Where(p => postIds.Contains(p.Id))
                                          .OrderBy(p => p.Published)
                                          //.FetchMany(p => p.Tags)
                                          .ToList();
                    }
                    else
                    {
                        posts = _context.Posts
                                        .OrderByDescending(p => p.Published)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                       // .Fetch(p => p.Category)
                                        .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _context.Posts
                                          .Where(p => postIds.Contains(p.Id))
                                          .OrderByDescending(p => p.Published)
                                         // .FetchMany(p => p.Tags)
                                          .ToList();
                    }
                    break;
                case "PostedOn":
                    if (sortByAscending)
                    {
                        posts = _context.Posts
                                        .OrderBy(p => p.PostedOn)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                       // .Fetch(p => p.Category)
                                        .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _context.Posts
                                          .Where(p => postIds.Contains(p.Id))
                                          .OrderBy(p => p.PostedOn)
                                         // .FetchMany(p => p.Tags)
                                          .ToList();
                    }
                    else
                    {
                        posts = _context.Posts
                                        .OrderByDescending(p => p.PostedOn)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                       // .Fetch(p => p.Category)
                                        .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _context.Posts
                                        .Where(p => postIds.Contains(p.Id))
                                        .OrderByDescending(p => p.PostedOn)
                                       // .FetchMany(p => p.Tags)
                                        .ToList();
                    }
                    break;
                case "Modified":
                    if (sortByAscending)
                    {
                        posts = _context.Posts
                                        .OrderBy(p => p.Modified)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                       // .Fetch(p => p.Category)
                                        .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _context.Posts
                                          .Where(p => postIds.Contains(p.Id))
                                          .OrderBy(p => p.Modified)
                                          //.FetchMany(p => p.Tags)
                                          .ToList();
                    }
                    else
                    {
                        posts = _context.Posts
                                        .OrderByDescending(p => p.Modified)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                        //.Fetch(p => p.Category)
                                        .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _context.Posts
                                          .Where(p => postIds.Contains(p.Id))
                                          .OrderByDescending(p => p.Modified)
                                        //  .FetchMany(p => p.Tags)
                                          .ToList();
                    }
                    break;
                case "Category":
                    if (sortByAscending)
                    {
                        posts = _context.Posts
                                        .OrderBy(p => p.Category.Name)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                        //.Fetch(p => p.Category)
                                        .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _context.Posts
                                          .Where(p => postIds.Contains(p.Id))
                                          .OrderBy(p => p.Category.Name)
                                         // .FetchMany(p => p.Tags)
                                          .ToList();
                    }
                    else
                    {
                        posts = _context.Posts
                                        .OrderByDescending(p => p.Category.Name)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                       // .Fetch(p => p.Category)
                                        .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _context.Posts
                                          .Where(p => postIds.Contains(p.Id))
                                          .OrderByDescending(p => p.Category.Name)
                                         // .FetchMany(p => p.Tags)
                                          .ToList();
                    }
                    break;
                default:
                    posts = _context.Posts
                                    .OrderByDescending(p => p.PostedOn)
                                    .Skip(pageNo * pageSize)
                                    .Take(pageSize)
                                    //.Fetch(p => p.Category)
                                    .ToList();

                    postIds = posts.Select(p => p.Id).ToList();

                    posts = _context.Posts
                                      .Where(p => postIds.Contains(p.Id))
                                      .OrderByDescending(p => p.PostedOn)
                                     // .FetchMany(p => p.Tags)
                                      .ToList();
                    break;
            }

            return posts;
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

        public Tag Tag(int id)
        {
            return _context.Tags.FirstOrDefault(t => t.Id == id);
        }

        public int TotalPosts(bool checkIsPublished = true)
        {
            return _context.Posts
                            .Where(p => !checkIsPublished || p.Published)
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
