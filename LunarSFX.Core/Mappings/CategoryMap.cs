using LunarSFX.Core.Objects;
using System.Data.Entity;

namespace LunarSFX.Core.Mappings
{
    public class CategoryMap
    {
        private DbModelBuilder _modelBuilder;

        public CategoryMap(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Map()
        {
            _modelBuilder.Entity<Category>()
                  .HasKey(x => x.Id);

            _modelBuilder.Entity<Category>()
                        .Property(x => x.Name)
                        .IsRequired()
                        .HasMaxLength(50);

            _modelBuilder.Entity<Category>()
                        .Property(x => x.UrlSlug)
                        .IsRequired()
                        .HasMaxLength(50);

            _modelBuilder.Entity<Category>()
                       .Property(x => x.Description)
                       .HasMaxLength(200);
        }
    }
}
