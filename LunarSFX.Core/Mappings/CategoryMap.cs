using FluentNHibernate.Mapping;
using LunarSFX.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace LunarSFX.Core.Mappings
{
    public class CategoryMap//: ClassMap<Category>
    {
        private DbModelBuilder _modelBuilder;

        //public CategoryMap()
        //{
        //    Id(x => x.Id);
        //    Map(x => x.Name)
        //        .Length(50)
        //        .Not.Nullable();
        //    Map(x => x.UrlSlug)
        //        .Length(50)
        //        .Not.Nullable();
        //    Map(x => x.Description)
        //        .Length(200);
        //    HasMany(x => x.Posts)
        //        .Inverse()
        //        .Cascade.All()
        //        .KeyColumn("Category");
        //}

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
