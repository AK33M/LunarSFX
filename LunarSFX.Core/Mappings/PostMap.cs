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
    public class PostMap//: ClassMap<Post>
    {
        private DbModelBuilder _modelBuilder;

        //public PostMap()
        //{
        //    Id(x => x.Id);
        //    Map(x => x.Title)
        //        .Length(500)
        //        .Not.Nullable();
        //    Map(x => x.ShortDescription)
        //        .Length(5000)
        //        .Not.Nullable();
        //    Map(x => x.Description)
        //        .Length(5000)
        //        .Not.Nullable();
        //    Map(x => x.Meta)
        //        .Length(1000)
        //        .Not.Nullable();
        //    Map(x => x.UrlSlug)
        //        .Length(200)
        //        .Not.Nullable();
        //    Map(x => x.Published)
        //        .Not.Nullable();
        //    Map(x => x.PostedOn)
        //        .Not.Nullable();
        //    Map(x => x.Modified);
        //    References(x => x.Category)
        //        .Column("Category")
        //        .Not.Nullable();
        //    HasManyToMany(x => x.Tags)
        //        .Table("PostTagMap");
        //}

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
