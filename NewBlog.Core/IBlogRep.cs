using NewBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBlog.Core
{
    public interface IBlogRep
    {
        IList<PostName> pnames(int pageN, int pageS);
        int TotalPos(bool chekishpub = true);

        IList<PostName> postsForcat(string categoryslu, int pageN, int pageS);
        int TotalPosForCat(string categoryslu);
        CategoryName Catagory(string categoryslu);
        IList<PostName> PostsFortag(string tagslug, int pageN, int pageS);
        int TatalPostsForTag(string tagslug);
        TagName Tag(string tagslu);

        PostName Post(int month, int year, string titleslu);



        IList<PostName> PostsForSearch(string search, int pageN, int pageS);
        int TotalPostsForSearch(string search);


        IList<CategoryName> Categorie();

        IList<TagName> tagss();

        IList<PostName> poss();

        IList<PostName> posts(int pageN, int pageS, string sordcol, bool orderByasc);

        int AddPost(PostName post);


        void EditPost(PostName post);
        void DeletePost(int id);

        IList<CategoryName> Categories();

        int TotalCategories();

        CategoryName Category(string categorySlug);

        CategoryName Category(int id);

        int AddCategory(CategoryName category);
        void EditCategory(CategoryName category);

        void DeleteCategory(int id);

        IList<TagName> Tags();

        int TotalTags();



        TagName Tag(int id);

        int AddTag(TagName tag);

        void EditTag(TagName tag);

        void DeleteTag(int id);

        PostName Post(int id);

    }
}
