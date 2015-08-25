using FluentNHibernate.Mapping;
using NewBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBlog.Core.MapClass
{
    public class PMap : ClassMap<PostName>
    {
        public PMap()
        {
            Id(x => x.Id);

            Map(x => x.TitleName)
              .Length(500)
              .Not.Nullable();

            Map(x => x.ShortDescription)
              .Length(5000)
              .Not.Nullable();

            Map(x => x.Description).Length(5000).Not.Nullable();

            Map(x => x.Meta).Length(1000).Not.Nullable();

            Map(x => x.UrlName).Length(200).Not.Nullable();

            Map(x => x.Published).Not.Nullable();

            Map(x => x.PostedDate).Not.Nullable();

            Map(x => x.Modified);

            References(x => x.cagegory).Column("CategoryName").Not.Nullable();

            HasManyToMany(x => x.tags).Table("PostTagName");



        }

    }
}
