using BlogSite.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSite.Web.Controllers
{
    public class AdminController : Controller
    {
        private string _connectionString = "Data Source=.\\sqlexpress;Initial Catalog=BlogPost;Integrated Security=True";
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitPost(BlogPost post)
        {
            BlogSiteRepository db = new BlogSiteRepository(_connectionString);
            post.Date = DateTime.Now;
            db.SubmitPost(post);
            return Redirect($"/home/viewblog?id={post.Id}");
        }
    }
}
