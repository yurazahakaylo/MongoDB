using Model;
using MongoDB.Bson;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PostService
    {
        PostRepository repository;
        UserRepository userRepository;
        UserService userServices;
        public PostService()
        {
            repository = new PostRepository();
            userServices = new UserService();
            userRepository = new UserRepository();
        }

        public void InsertPost(string text, string username)
        {
            Post post = new Post();
            post.Text = text;
            //post.PostOwner = userRepository.GetId(userServices.NicknameRead());
            post.PostOwner = userRepository.GetId(username);
            post.Time = DateTime.Now;
            repository.Add(post);
        }

        public void EditPost(string newText, ObjectId postId)
        {
            try
            {
                repository.UpdatePost(postId, newText);
            }
            catch
            {

            }

        }

        public bool DeletePost(ObjectId postId)
        {
            try
            {
                repository.Delete(postId);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool AddComment(string text, ObjectId postId, User user)
        {

            Comment comment = new Comment();
            comment.Text = text;
            comment.CommentOwner = user.Id;
            comment.Time = DateTime.Now;
            try
            {
                repository.AddComment(comment, postId);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool DeleteComment(string text, ObjectId postId)
        {

            Comment comment = new Comment();
            comment.Text = text;
            comment.CommentOwner = userRepository.GetUser(userServices.NicknameRead()).Id;
            comment.Time = DateTime.Now;
            try
            {
                repository.DeleteComment(comment, postId);
                return true;
            }
            catch
            {
                return false;
            }

        }
        //
        public List<Post> GetNewPosts(DateTime lastLogin, List<string> following)
        {
            List<ObjectId> ids = new List<ObjectId>();
            if (following != null)
            {
                foreach (var el in following)
                {
                    ids.Add(userRepository.GetId(el));
                }
                return repository.GetNewPosts(lastLogin, ids);
            }

            return new List<Post>();

        }

        public List<Post> GetPosts(string nickname)
        {
            List<Post> posts = new List<Post>();
            try
            {
                posts = repository.GetPosts(userRepository.GetId(nickname));
                return posts;
            }
            catch
            {
                return posts;
            }
        }

        public Post GetPost(ObjectId postId)
        {
            Post post = new Post();
            try
            {
                post = repository.GetPost(postId);
                return post;
            }
            catch
            {
                return post;
            }

        }

        public List<Comment> GetPostsComment(ObjectId postId)
        {
            List<Comment> comments = new List<Comment>();
            comments = repository.GetComments(postId);
            return comments;
        }

        public List<string> GetPersonWhoComment(ObjectId postId)
        {
            List<Comment> comments = new List<Comment>();
            try
            {
                comments = repository.GetComments(postId);                
                if (comments != null)
                {
                    List<string> res = new List<string>();
                    foreach (var el in comments)
                    {
                        res.Add(userRepository.GetUser(el.CommentOwner).Nickname + "\n" + el.Text + "\n" + el.Time);

                    }
                    return res;
                }
            }
            catch
            {
                return new List<string>();
            }
            return new List<string>();
        }

    }
}
