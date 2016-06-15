using LunarSFX.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarSFX.Core.Repositories
{
    public interface IBlogRepository
    {
        IList<Post> Posts(int pageNo, int pageSize);
        IList<Post> Posts(int pageNo, int pageSize, string sortColumn, bool sortByAscending);
        int TotalPosts(bool checkIsPublished = true);
        IList<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize);
        int TotalPostsForCategory(string categorySlug);
        Category Category(string categorySlug);
        IList<Post> PostsForTag(string tagSlug, int pageNo, int pageSize);
        int TotalPostsForTag(string tagSlug);
        Tag Tag(string tagSlug);
        IList<Post> PostsForSearch(string search, int pageNo, int pageSize);
        int TotalPostsForSearch(string search);
        Post Post(int year, int month, string titleSlug);
        IList<Category> Categories();
        IList<Tag> Tags();
        int AddPost(Post post);
    }
}
