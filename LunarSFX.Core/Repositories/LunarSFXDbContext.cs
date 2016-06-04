using LunarSFX.Core.Mappings;
using LunarSFX.Core.Objects;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LunarSFX.Core.Repositories
{
    public class LunarSFXDbContext : DbContext
    {
        public LunarSFXDbContext() : base("name=LunarSFXDbConnString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LunarSFXDbContext, Migrations.Configuration>("LunarSFXDbConnString"));
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            var postMap = new PostMap(modelBuilder);
            postMap.Map();

            var categoryMap = new CategoryMap(modelBuilder);
            categoryMap.Map();

            var tagMap = new TagMap(modelBuilder);
            tagMap.Map();

            base.OnModelCreating(modelBuilder);
        }
    }
}
