using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;
using NHibernate.Criterion;
using NHibernate.Transform;
using NewBlog.Core.Entities;

namespace NewBlog.Core
{
    public class BlogRep : IBlogRep
    {
        private readonly ISession _sess;

        public BlogRep(ISession sec)
        {
            _sess = sec;
        }

        public IList<PostName> pnames(int pageN, int pageS)
        {
            var query = _sess.Query<PostName>()
                            .Where(p => p.Published)
                            .OrderByDescending(p => p.PostedDate)
                            .Skip(pageN * pageS)
                            .Take(pageS)
                            .Fetch(p => p.cagegory);

            query.FetchMany(p => p.tags).ToFuture();

            return query.ToFuture().ToList();
        }

        public int TotalPos(bool chekishpub = true)
        {
            return _sess.Query<PostName>().Where(p => chekishpub || p.Published == true).Count();

        }
        public IList<PostName> postsForcat(string categoryslu, int pageN, int pageS)
        {
            var query = _sess.Query<PostName>().Where(p => p.Published && p.cagegory.UrlName.Equals(categoryslu)).OrderByDescending(p => p.Published).Skip(pageN * pageS).Take(pageS).Fetch(p => p.tags);

            query.FetchMany(p => p.tags).ToFuture().ToList();

            return query.ToFuture().ToList();


        }

        public int TotalPosForCat(string categoryslu)
        {
            return _sess.Query<PostName>().Where(p => p.Published && p.cagegory.UrlName.Equals(categoryslu)).Count();

        }

        public CategoryName Catagory(string categoryslu)
        {

            return _sess.Query<CategoryName>().FirstOrDefault(t => t.UrlName.Equals(categoryslu));

        }

        public IList<PostName> PostsFortag(string tagslug, int pageN, int pageS)
        {

            var query = _sess.Query<PostName>().Where(p => p.Published && p.tags.Any(t => t.UrlName.Equals(tagslug))).OrderByDescending(p => p.PostedDate).Skip(pageN * pageS).Take(pageS).Fetch(p => p.cagegory);

            query.FetchMany(p => p.tags).ToFuture().ToList();

            return query.ToFuture().ToList();

        }
        public int TatalPostsForTag(string tagslug)
        {

            return _sess.Query<PostName>().Where(p => p.Published && p.tags.Any(t => t.UrlName.Equals(tagslug))).Count();
        }

        public TagName Tag(string tagslu)
        {

            return _sess.Query<TagName>().FirstOrDefault(t => t.UrlName.Equals(tagslu));
        }

        public PostName Post(int year, int month, string titleslu)
        {
            var query = _sess.Query<PostName>().Where(p => p.PostedDate.Year == year && p.PostedDate.Month == month && p.UrlName.Equals(titleslu)).Fetch(p => p.cagegory);

            query.FetchMany(p => p.tags).ToFuture();

            return query.ToFuture().Single();


        }

        public IList<CategoryName> Categorie()
        {

            return _sess.Query<CategoryName>().OrderBy(p => p.Name).ToList();
        }
        public IList<TagName> tagss()
        {
            return _sess.Query<TagName>().OrderBy(p => p.Name).ToList();

        }

        public IList<PostName> poss()
        {
            return _sess.Query<PostName>().OrderBy(p => p.TitleName).ToList();

        }

        public IList<PostName> PostsForSearch(string search, int pageN, int pageS)
        {
            var query = _sess.Query<PostName>().Where(p => p.Published && p.TitleName.Contains(search) || p.cagegory.UrlName.Equals(search) || p.tags.Any(t => t.UrlName.Equals(search))).OrderByDescending(p => p.PostedDate).Skip(pageN * pageS).Take(pageS).Fetch(x => x.cagegory);

            query.FetchMany(p => p.tags).ToFuture().ToList();

            return query.ToFuture().ToList();

        }

        public int TotalPostsForSearch(string search)
        {

            return _sess.Query<PostName>().Where(p => p.Published && p.TitleName.Contains(search) || p.cagegory.UrlName.Equals(search) || p.tags.Any(t => t.UrlName.Equals(search))).Count();

        }

        public IList<PostName> posts(int pageN, int pageS, string sordcol, bool orderByasc)
        {
            IQueryable<PostName> q;

            switch (sordcol)
            {
                case "TitleName":
                    if (orderByasc)

                        q = _sess.Query<PostName>().OrderBy(p => p.TitleName).Skip(pageN * pageS).Take(pageS).Fetch(x => x.cagegory);

                    else
                        q = _sess.Query<PostName>().OrderByDescending(p => p.TitleName).Skip(pageN * pageS).Take(pageS).Fetch(x => x.cagegory);
                    break;
                case "Published":
                    if (orderByasc)
                        q = _sess.Query<PostName>().OrderBy(p => p.Published).Skip(pageN * pageS).Take(pageS).Fetch(x => x.cagegory);
                    else
                        q = _sess.Query<PostName>().OrderByDescending(p => p.Published).Skip(pageN * pageS).Take(pageS).Fetch(x => x.cagegory);
                    break;
                case "PostedDate":
                    if (orderByasc)
                        q = _sess.Query<PostName>().OrderBy(p => p.PostedDate).Skip(pageN * pageS).Take(pageS).Fetch(x => x.cagegory);
                    else
                        q = _sess.Query<PostName>().OrderByDescending(p => p.PostedDate).Skip(pageN * pageS).Take(pageS).Fetch(x => x.cagegory);
                    break;
                case "Modified":
                    if (orderByasc)
                        q = _sess.Query<PostName>().OrderBy(p => p.Modified).Skip(pageN * pageS).Take(pageS).Fetch(x => x.cagegory);
                    else
                        q = _sess.Query<PostName>().OrderByDescending(p => p.Modified).Skip(pageN * pageS).Take(pageS).Fetch(x => x.cagegory);
                    break;
                case "cagegory":
                    if (orderByasc)
                        q = _sess.Query<PostName>().OrderBy(p => p.cagegory).Skip(pageN * pageS).Take(pageS).Fetch(x => x.cagegory);
                    else
                        q = _sess.Query<PostName>().OrderByDescending(p => p.cagegory).Skip(pageN * pageS).Take(pageS).Fetch(x => x.cagegory);
                    break;
                default:

                    q = _sess.Query<PostName>().OrderByDescending(p => p.PostedDate).Skip(pageN * pageS).Take(pageS).Fetch(x => x.cagegory);
                    break;


            }
            q.FetchMany(x => x.tags).ToFuture();
            return q.ToFuture().ToList();


        }

        public int AddPost(PostName post)
        {
            using (var tran = _sess.BeginTransaction())
            {
                _sess.Save(post);
                tran.Commit();
                return post.Id;
            }
        }



        /// <summary>
        /// Update an existing post.
        /// </summary>
        /// <param name="post"></param>
        public void EditPost(PostName post)
        {
            using (var tran = _sess.BeginTransaction())
            {
                _sess.SaveOrUpdate(post);
                tran.Commit();
            }
        }

        /// <summary>
        /// Delete the post permanently from database.
        /// </summary>
        /// <param name="id"></param>
        public void DeletePost(int id)
        {
            using (var tran = _sess.BeginTransaction())
            {
                var post = _sess.Get<PostName>(id);
                if (post != null) _sess.Delete(post);
                tran.Commit();
            }
        }

        /// <param name="id">Post unique id</param>
        /// <returns></returns>
        public PostName Post(int id)
        {
            return _sess.Get<PostName>(id);
        }



        /// <summary>
        /// Return all the categories.
        /// </summary>
        /// <returns></returns>
        public IList<CategoryName> Categories()
        {
            return _sess.Query<CategoryName>().OrderBy(p => p.Name).ToList();
        }

        /// <summary>
        /// Return total no. of categories.
        /// </summary>
        /// <returns></returns>
        public int TotalCategories()
        {
            return _sess.Query<CategoryName>().Count();
        }

        /// <summary>
        /// Return category based on url slug.
        /// </summary>
        /// <param name="categorySlug">Category's url slug</param>
        /// <returns></returns>
        public CategoryName Category(string categorySlug)
        {
            return _sess.Query<CategoryName>().FirstOrDefault(t => t.UrlName.Equals(categorySlug));
        }

        /// <summary>
        /// Return category based on id.
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns></returns>
        public CategoryName Category(int id)
        {
            return _sess.Query<CategoryName>().FirstOrDefault(t => t.Id == id);
        }

        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public int AddCategory(CategoryName category)
        {
            using (var tran = _sess.BeginTransaction())
            {
                _sess.Save(category);
                tran.Commit();
                return category.Id;
            }
        }

        /// <summary>
        /// Update an existing category.
        /// </summary>
        /// <param name="category"></param>
        public void EditCategory(CategoryName category)
        {
            using (var tran = _sess.BeginTransaction())
            {
                _sess.SaveOrUpdate(category);
                tran.Commit();
            }
        }

        /// <summary>
        /// Delete a category permanently.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCategory(int id)
        {
            using (var tran = _sess.BeginTransaction())
            {
                var category = _sess.Get<CategoryName>(id);
                _sess.Delete(category);
                tran.Commit();
            }
        }

        /// <summary>
        /// Return all the tags.
        /// </summary>
        /// <returns></returns>
        public IList<TagName> Tags()
        {
            return _sess.Query<TagName>().OrderBy(p => p.Name).ToList();
        }

        /// <summary>
        /// Return total no. of tags.
        /// </summary>
        /// <returns></returns>
        public int TotalTags()
        {
            return _sess.Query<TagName>().Count();
        }

        /// <summary>
        /// Return tag based on url slug.
        /// </summary>
        /// <param name="tagSlug"></param>
        /// <returns></returns>


        /// <summary>
        /// Return tag based on unique id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TagName Tag(int id)
        {
            return _sess.Query<TagName>().FirstOrDefault(t => t.Id == id);
        }

        /// <summary>
        /// Add a new tag.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public int AddTag(TagName tag)
        {
            using (var tran = _sess.BeginTransaction())
            {
                _sess.Save(tag);
                tran.Commit();
                return tag.Id;
            }
        }

        /// <summary>
        /// Edit an existing tag.
        /// </summary>
        /// <param name="tag"></param>
        public void EditTag(TagName tag)
        {
            using (var tran = _sess.BeginTransaction())
            {
                _sess.SaveOrUpdate(tag);
                tran.Commit();
            }
        }

        /// <summary>
        /// Delete an existing tag permanently.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteTag(int id)
        {
            using (var tran = _sess.BeginTransaction())
            {
                var tag = _sess.Get<TagName>(id);
                _sess.Delete(tag);
                tran.Commit();
            }
        }

    }
}
