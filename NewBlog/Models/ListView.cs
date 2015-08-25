using NewBlog.Core;
using NewBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewBlog.Models
{
    public class ListView
    {
        public ListView(IBlogRep repo, int p)
        {
            pnames = repo.pnames(p - 1, 10);
            TotalPos = repo.TotalPos();

        }

        public ListView(IBlogRep repo, string text, string type, int p)
        {
            switch(type)
            { 
                case"Tag":
            pnames = repo.PostsFortag(text,p-1,10);
            TotalPos = repo.TotalPosForCat(text);
            Tag = repo.Tag(text);
            break;
            default:
            pnames = repo.postsForcat(text, p-1,10);
            TotalPos = repo.TotalPosForCat(text);
            Category = repo.Catagory(text);
            break;
        }
        }
        
       
        public IList<PostName> pnames { get; private set; }
        public int TotalPos { get; private set; }

        public CategoryName Category { get; private set; }

        public TagName Tag { get; private set; }



    }
}