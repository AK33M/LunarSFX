using LunarSFX.Core.Objects;
using System.Data.Entity;

namespace LunarSFX.Core.Mappings
{
    public class TagMap
    {
        private DbModelBuilder _modelBuilder;

        public TagMap(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Map()
        {
            _modelBuilder.Entity<Tag>()
                  .HasKey(x => x.Id);

            _modelBuilder.Entity<Tag>()
                        .Property(x => x.Name)
                        .IsRequired()
                        .HasMaxLength(50);

            _modelBuilder.Entity<Tag>()
                        .Property(x => x.UrlSlug)
                        .IsRequired()
                        .HasMaxLength(50);

            _modelBuilder.Entity<Tag>()
                       .Property(x => x.Description)
                       .HasMaxLength(200);
        }
    }
}
