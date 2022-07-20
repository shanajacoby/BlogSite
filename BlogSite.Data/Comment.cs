using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Data
{
    public class Comment
    {
        public int Id { get; set; }
        public int BlogPostId { get; set; }
        public string CommentText { get; set; }
        public string Commenter { get; set; }
        public DateTime Date { get; set; }
    }
}
