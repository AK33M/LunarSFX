using LunarSFX.Core.Objects;
using System.Data.Entity;

namespace LunarSFX.Core.Mappings
{
    public class PostMap
    {
        private DbModelBuilder _modelBuilder;

        public PostMap(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Map()
        {
            _modelBuilder.Entity<Post>()
                        .HasKey(x => x.Id);

            _modelBuilder.Entity<Post>()
                        .Property(x => x.Title)
                        .IsRequired()
                        .HasMaxLength(500);

            _modelBuilder.Entity<Post>()
                        .Property(x => x.ShortDescription)
                        .IsRequired()
                        .HasMaxLength(5000);

            _modelBuilder.Entity<Post>()
                       .Property(x => x.Description)
                       .IsRequired()
                       .HasMaxLength(5000);

            _modelBuilder.Entity<Post>()
                        .Property(x => x.Meta)
                        .IsRequired()
                        .HasMaxLength(1000);

            _modelBuilder.Entity<Post>()
                       .Property(x => x.UrlSlug)
                       .IsRequired()
                       .HasMaxLength(200);

            _modelBuilder.Entity<Post>()
                        .Property(x => x.Published)
                        .IsRequired();

            _modelBuilder.Entity<Post>()
                        .Property(x => x.PostedOn)
                        .IsRequired();

            _modelBuilder.Entity<Post>()
                        .Property(x => x.Modified);

            _modelBuilder.Entity<Post>()
                        .HasRequired(x => x.Category)
                        .WithMany(x => x.Posts)
                        .HasForeignKey(x => x.CategoryId);

            _modelBuilder.Entity<Post>()
                        .HasMany(x => x.Tags)
                        .WithMany(x => x.Posts)
                        .Map(mc =>
                        {
                            mc.ToTable("PostTagMap");
                            mc.MapLeftKey("PostId");
                            mc.MapRightKey("TagId");
                        });
        }
    }
}
