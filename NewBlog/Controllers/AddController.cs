using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NewBlog.Models;
using NewBlog.Providers;
using NewBlog.Providers;
using NewBlog.Core;
using Newtonsoft.Json;
using NewBlog.Core.Entities;
using System.Text;

namespace NewBlog.Controllers
{
    [Authorize]
    public class AddController : Controller
    {
        private readonly IAuthProv _authProvider;
        private readonly IBlogRep repo;

        public AddController(IAuthProv authProvider, IBlogRep _repo = null)
        {
            
            _authProvider = authProvider;
            repo = _repo;
           
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (_authProvider.IsLogged)
                return RedirectToUrl(returnUrl);

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

       

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult Login(LoginView model, string returnUrl)
        {
            if (ModelState.IsValid && _authProvider.Login(model.UserName, model.Password))
            {
                return RedirectToUrl(returnUrl);
            }

            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        public ActionResult Manage()
        {
            return View();
        }

        public ActionResult Logout()
        {
            _authProvider.Logout();

            return RedirectToAction("Login", "Admin");
        }

        public ActionResult RedirectToUrl(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Manage");
            }
        }

        public ContentResult Posts(Jquel mod)
        {
            var pst = repo.posts(mod.page-1, mod.rows, mod.sidex, mod.sort == "asc");

            var totalpos = repo.TotalPos(false);

            return Content(JsonConvert.SerializeObject(new
            {
                page = mod.page,
                records = totalpos,
                rows = pst,
                total = Math.Ceiling(Convert.ToDouble(totalpos)/  mod.rows)
            }, new CustomDateTimeConverter()), "application/json");
        }

            [HttpPost, ValidateInput(false)]
    public ContentResult AddPost(PostName post)
    {
      string json;

      ModelState.Clear();

      if (TryValidateModel(post))
      {
        var id = repo.AddPost(post);

        json = JsonConvert.SerializeObject(new
        {
          id = id,
          success = true,
          message = "Post added successfully."
        });
      }
      else
      {
        json = JsonConvert.SerializeObject(new
        {
          id = 0,
          success = false,
          message = "Failed to add the post."
        });
      }

      return Content(json, "application/json");
    }

    /// <summary>
    /// Edit an existing post.
    /// </summary>
    /// <param name="post"></param>
    /// <returns></returns>
    [HttpPost, ValidateInput(false)]
    public ContentResult EditPost(PostName post)
    {
      string json;

      ModelState.Clear();

      if (TryValidateModel(post))
      {
         repo.EditPost(post);
        json = JsonConvert.SerializeObject(new
        {
          id = post.Id,
          success = true,
          message = "Changes saved successfully."
        });
      }
      else
      {
        json = JsonConvert.SerializeObject(new
        {
          id = 0,
          success = false,
          message = "Failed to save the changes."
        });
      }

      return Content(json, "application/json");
    }

    /// <summary>
    /// Delete an existing post.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    public ContentResult DeletePost(int id)
    {
      repo.DeletePost(id);

      var json = JsonConvert.SerializeObject(new
      {
        success = true,
        message = "Post deleted successfully."
      });

      return Content(json, "application/json");
    }

   

    /// <summary>
    /// Return all the categories.
    /// </summary>
    /// <returns></returns>
    public ContentResult Categories()
    {
      var categories = repo.Categories();

      return Content(JsonConvert.SerializeObject(new
      {
        page = 1,
        records = categories.Count,
        rows = categories,
        total = 1
      }), "application/json");
    }

    /// <summary>
    /// Add new category.
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    [HttpPost]
    public ContentResult AddCategory([Bind(Exclude = "Id")]CategoryName category)
    {
      string json;

      if (ModelState.IsValid)
      {
        var id = repo.AddCategory(category);
        json = JsonConvert.SerializeObject(new
        {
          id = id,
          success = true,
          message = "Category added successfully."
        });
      }
      else
      {
        json = JsonConvert.SerializeObject(new
        {
          id = 0,
          success = false,
          message = "Failed to add the category."
        });
      }

      return Content(json, "application/json");
    }

    /// <summary>
    /// Edit an existing category.
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    [HttpPost]
    public ContentResult EditCategory(CategoryName category)
    {
      string json;

      if (ModelState.IsValid)
      {
        repo.EditCategory(category);
        json = JsonConvert.SerializeObject(new
        {
          id = category.Id,
          success = true,
          message = "Changes saved successfully."
        });
      }
      else
      {
        json = JsonConvert.SerializeObject(new
        {
          id = 0,
          success = false,
          message = "Failed to save the changes."
        });
      }

      return Content(json, "application/json");
    }

    /// <summary>
    /// Delete an existing category.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    public ContentResult DeleteCategory(int id)
    {
      repo.DeleteCategory(id);

      var json = JsonConvert.SerializeObject(new
      {
        success = true,
        message = "Category deleted successfully."
      });

      return Content(json, "application/json");
    }

    /// <summary>
    /// Return html required to create category dropdown in jQGrid popup.
    /// </summary>
    /// <returns></returns>
    public ContentResult GetCategoriesHtml()
    {
      var categories = repo.Categories().OrderBy(s => s.Name);

      var sb = new StringBuilder();
      sb.AppendLine(@"<select>");

      foreach (var category in categories)
      {
        sb.AppendLine(string.Format(@"<option value=""{0}"">{1}</option>", category.Id, category.Name));
      }

      sb.AppendLine("<select>");
      return Content(sb.ToString(), "text/html");
    }

    

    
    /// <summary>
    /// Return all the tags as JSON.
    /// </summary>
    /// <returns></returns>
    public ContentResult Tags()
    {
      var tags = repo.Tags();

      return Content(JsonConvert.SerializeObject(new
      {
        page = 1,
        records = tags.Count,
        rows = tags,
        total = 1
      }), "application/json");
    }

    /// <summary>
    /// Add a new tag.
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    [HttpPost]
    public ContentResult AddTag([Bind(Exclude = "Id")]TagName tag)
    {
      string json;

      if (ModelState.IsValid)
      {
        var id = repo.AddTag(tag);
        json = JsonConvert.SerializeObject(new
        {
          id = id,
          success = true,
          message = "Tag added successfully."
        });
      }
      else
      {
        json = JsonConvert.SerializeObject(new
        {
          id = 0,
          success = false,
          message = "Failed to add the tag."
        });
      }

      return Content(json, "application/json");
    }

    /// <summary>
    /// Edit an existing tag.
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    [HttpPost]
    public ContentResult EditTag(TagName tag)
    {
      string json;

      if (ModelState.IsValid)
      {
        repo.EditTag(tag);
        json = JsonConvert.SerializeObject(new
        {
          id = tag.Id,
          success = true,
          message = "Changes saved successfully."
        });
      }
      else
      {
        json = JsonConvert.SerializeObject(new
        {
          id = 0,
          success = false,
          message = "Failed to save the changes."
        });
      }

      return Content(json, "application/json");
    }

    /// <summary>
    /// Delete an existing tag.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    public ContentResult DeleteTag(int id)
    {
      repo.DeleteTag(id);

      var json = JsonConvert.SerializeObject(new
      {
        success = true,
        message = "Tag deleted successfully."
      });

      return Content(json, "application/json");
    }

    /// <summary>
    /// Return html required to create tag dropdown in jQGrid popup.
    /// </summary>
    /// <returns></returns>
    public ContentResult GetTagsHtml()
    {
      var tags = repo.Tags().OrderBy(s => s.Name);

      var sb = new StringBuilder();
      sb.AppendLine(@"<select multiple=""multiple"">");

      foreach (var tag in tags)
      {
        sb.AppendLine(string.Format(@"<option value=""{0}"">{1}</option>", tag.Id, tag.Name));
      }

      sb.AppendLine("<select>");
      return Content(sb.ToString(), "text/html");
    }

   
    public ActionResult GoToPost(int id)
    {
      var post = repo.Post(id);
      return RedirectToRoute(new { controller = "BlogNew", action = "Post", year = post.PostedDate.Year, month = post.PostedDate.Month, title = post.UrlName });
    }
         

    }
}

