using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewBlog.Core;
using NewBlog.Core.Entities;

namespace NewBlog.Models
{
    public class WidgetView
    {
        public WidgetView(IBlogRep repo)
        {
            Categorie = repo.Categorie();

        }

    public IList<CategoryName> Categorie { get; private set; }
    }
}