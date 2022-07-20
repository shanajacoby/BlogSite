using BlogSite.Data;
using HWMar2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HWMar2.Controllers
{
    public class HomeController : Controller
    {

        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=BlogPost;Integrated Security=true;";
        public IActionResult Index()
        {
            var db = new BlogSiteRepository(_connectionString);
            List<BlogPost> posts = db.GetPosts();
            return View(new HomePageViewModel
            {
                Posts = posts
            });
        }

        public IActionResult ViewBlog(int id)
        {
            if (id == 0)
            {
                return Redirect("/");
            }
            var cookieValue = Request.Cookies["last-commenter-name"];
            var db = new BlogSiteRepository(_connectionString);
            BlogPost post=db.GetBlogPost(id);
            if (post == null)
            {
                return Redirect("/");
            }
            List<Comment> comments = db.GetComments(id);
            return View(new ViewBlogViewModel{
                Post = post,
                Comments= comments,
                LastCommenterName= cookieValue
            });
        }
        [HttpPost]
        public IActionResult AddComment(int postId, string name, string content)
        {
            
            Comment comment = new Comment { BlogPostId = postId, Commenter = name, CommentText = content};
            BlogSiteRepository db = new (_connectionString);
            db.SubmitComment(comment);
            Response.Cookies.Append("last-commenter-name", comment.Commenter);
            return Redirect($"/home/viewBlog?id={comment.BlogPostId}");
        }
        public ActionResult MostRecent()
        {
            var db = new BlogSiteRepository(_connectionString);
            int id = db.GetMostRecentId();
            return Redirect($"/home/viewblog?id={id}");
        }




    }
}
