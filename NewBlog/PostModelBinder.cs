using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewBlog.Core.Entities;
using Ninject;
using NewBlog.Core;

namespace NewBlog
{
    public class PostModelBinder : DefaultModelBinder
    {
        private readonly IKernel _kernel;

       


    }
}