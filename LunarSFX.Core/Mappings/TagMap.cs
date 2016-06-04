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
    public class TagMap//: ClassMap<Tag>
    {
        private DbModelBuilder _modelBuilder;

        //public TagMap()
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
        //    HasManyToMany(x => x.Posts)
        //        .Cascade.All().Inverse()
        //        .Table("PostTagMap");
        //}

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
