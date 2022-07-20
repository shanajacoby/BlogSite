using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BlogSite.Data
{
    public class BlogSiteRepository
    {
        private string _connectionString;
        public BlogSiteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int SubmitPost(BlogPost post)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO BlogPost Values(@postText, @title, @date) SELECT SCOPE_IDENTITY()";
            cmd.Parameters.AddWithValue("@postText", post.PostText);
            cmd.Parameters.AddWithValue("@title", post.Title);
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
            connection.Open();
            return (int)(decimal)cmd.ExecuteScalar();
        }
        public void SubmitComment(Comment comment)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Comment Values( @blogPostId,  @text, @name, @date)";
            cmd.Parameters.AddWithValue("@name", comment.Commenter);
            cmd.Parameters.AddWithValue("@text", comment.CommentText);
            cmd.Parameters.AddWithValue("@blogPostId", comment.BlogPostId);
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public BlogPost GetBlogPost(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM BlogPost WHERE Id=@id";
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            return new BlogPost
            {
                Id = (int)reader["Id"],
                PostText = (string)reader["PostText"],
                Title = (string)reader["Title"],
                Date = (DateTime)reader["Date"]
            };
        }

        public List<Comment> GetComments(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "Select * from Comment Where BlogPostId=@id";
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            List<Comment> comments = new List<Comment>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Comment comment = new Comment
                {
                    Id = (int)reader["Id"],
                    Commenter = (string)reader["Commenter"],
                    CommentText = (string)reader["CommentText"],
                    BlogPostId = (int)reader["BlogPostId"],
                    Date = (DateTime)reader["Date"]
                };
                comments.Add(comment);
            }
            return comments;

        }
        public List<BlogPost> GetPosts()
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "Select * from BlogPost";
            //cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            List<BlogPost> posts = new List<BlogPost>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                BlogPost blogPost = new BlogPost
                {
                    Id = (int)reader["Id"],
                    PostText = (string)reader["PostText"],
                    Title = (string)reader["Title"],
                    Date = (DateTime)reader["Date"]
                };
                posts.Add(blogPost);
            }
            return posts;

        }
        public int GetMostRecentId()
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT Top 1 Id FROM BlogPost ORDER BY Date DESC";
            connection.Open();
            return (int)command.ExecuteScalar();
        }

    }

 

}
