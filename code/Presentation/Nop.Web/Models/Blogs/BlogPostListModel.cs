﻿using System.Collections.Generic;
using Nop.Web.Framework.Models;
using Nop.Web.Models.Media;

namespace Nop.Web.Models.Blogs
{
    public partial record BlogPostListModel : BaseNopModel
    {
        public BlogPostListModel()
        {   
            PagingFilteringContext = new BlogPagingFilteringModel();
            BlogPosts = new List<BlogPostModel>();
        }

        public int WorkingLanguageId { get; set; }
        public BlogPagingFilteringModel PagingFilteringContext { get; set; }
        public IList<BlogPostModel> BlogPosts { get; set; }
    }
}