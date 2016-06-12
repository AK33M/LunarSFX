using Newtonsoft.Json;
using System.Collections.Generic;

namespace LunarSFX.Core.Objects
{
    public class Category
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string UrlSlug { get; set; }
        public virtual string Description { get; set; }
        [JsonIgnore]
        public virtual IList<Post> Posts { get; set; }
    }
}