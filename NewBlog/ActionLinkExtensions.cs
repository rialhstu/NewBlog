using NewBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace NewBlog
{
    public static class ActionLinkExtensions
    {
        public static MvcHtmlString PostLink(this HtmlHelper help, PostName pname)
        {
            return help.ActionLink(pname.TitleName, "Post", "Blognew",
                new
                {
                    year = pname.PostedDate.Year,
                    month = pname.PostedDate.Month,
                    title = pname.UrlName
                },
                new
                {
                    title = pname.TitleName

                }
                );


        }
        public static MvcHtmlString CategoryLink(this HtmlHelper help, CategoryName category)
        {
            return help.ActionLink(category.Name, "Category", "Blognew",
                new
                {
                    category = category.UrlName

                },
            new
            {
                title = string.Format("See All Posts in {0}", category.Name)

            });


        }

        public static MvcHtmlString TagLink(this HtmlHelper help, TagName tag)
        {
            return help.ActionLink(tag.Name, "Tag", "Blognew",
                new
                {
                    tag = tag.UrlName

                },
            new
            {
                title = string.Format("See All Posts in {0}", tag.Name)

            });


        }



    }
}