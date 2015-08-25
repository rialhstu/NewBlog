using FluentNHibernate.Mapping;
using NewBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBlog.Core.MapClass
{
    public class TMap : ClassMap<TagName>
    {
        public TMap()
        {
            Id(x => x.Id);

            Map(x => x.Name).Length(50).Not.Nullable();
            Map(x => x.UrlName).Length(50).Not.Nullable();
            Map(x => x.Description).Length(200);

            HasManyToMany(x => x.posts).Cascade.All().Inverse().Table("PostTagName");



        }
    }
}
