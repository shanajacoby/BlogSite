using BlogSite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWMar2.Models
{
    public class ViewBlogViewModel
    {
        public BlogPost Post { get; set; }
        public List<Comment> Comments { get; set; }
        public string LastCommenterName { get; set; }
    }
}
