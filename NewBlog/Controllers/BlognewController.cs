using NewBlog.Core;
using NewBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewBlog.Controllers
{
    public class BlognewController : Controller
    {
        private readonly IBlogRep repo;

        public BlognewController(IBlogRep rep)
        {

            repo = rep;
        }

        public ViewResult Posts(int p = 1)
        {
            ListView viewm = new ListView(repo, p);
            return View("List", viewm);

        }

        public ViewResult Tag(string tag, int p = 1)
        {
            var viewModel = new ListView(repo, tag, "Tag", p);

            if (viewModel.Tag == null)
                throw new HttpException(404, "Tag not found");

            ViewBag.Title = String.Format(@"Latest posts tagged on ""{0}""",
                viewModel.Tag.Name);
            return View("List", viewModel);
        }
        public ViewResult Category(string category, int p = 1)
        {
            var viewModel = new ListView(repo, category, "Category", p);

            if (viewModel.Category == null)
                throw new HttpException(404, "Category not found");

            ViewBag.Title = String.Format(@"Latest posts on category ""{0}""",
                viewModel.Category.Name);
            return View("List", viewModel);
        }

        
        
        
        
        
        public ViewResult Post(int year, int month, string title)
        {
            var post = repo.Post(year, month, title);

            if (post == null)
                throw new HttpException(404, "Post not found");

            if (post.Published == false && User.Identity.IsAuthenticated == false)
                throw new HttpException(401, "The post is not published");

            return View(post);


        }
       
        
        [ChildActionOnly]
        public PartialViewResult SideBars()
        {
            var weightview = new WidgetView(repo);
            return PartialView("SideBars", weightview);
            

        }

        




    }
}
