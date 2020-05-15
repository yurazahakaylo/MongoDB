using Model;
using Repository;
using Service;
using System.Collections.Generic;
using System.Windows;

namespace WpfSocialNetwork
{
    /// <summary>
    /// Interaction logic for ViewComments.xaml
    /// </summary>
    public partial class ViewComments : Window
    {
        Post currentPost;
        PostService postService;
        PostRepository postRepository;
        List<Comment> comments;

        bool isAnyComment = false;
        int indexOfComment = 0;
        Comment currentComment;

        public ViewComments(Post post)
        {
            InitializeComponent();
            currentPost = post;
            postService = new PostService();
            postRepository = new PostRepository();

            comments = new List<Comment>();

            comments = postService.GetPostsComment(currentPost.Id);
            
            if (comments != null && comments.Count > 0)
            {
                currentComment = comments[indexOfComment];
                Main.Content = currentComment.Text;
                isAnyComment = true;
            }
            else
            {
                Main.Content = "There is no comments";
            }
        }

        private void Comments(object sender, RoutedEventArgs e)
        {
            comments = postService.GetPostsComment(currentPost.Id);
            if (isAnyComment)
            {
                if (indexOfComment > 0)
                {
                    indexOfComment--;
                    currentComment = comments[indexOfComment];
                    Main.Content = currentComment.Text;
                }
                else
                {
                    currentComment = comments[indexOfComment];
                    Main.Content = currentPost.Text;
                }
            }
        }

        private void NextComment(object sender, RoutedEventArgs e)
        {
            comments = postService.GetPostsComment(currentPost.Id);
            if (isAnyComment)
            {
                indexOfComment++;
                if (indexOfComment < comments.Count)
                {
                    currentComment = comments[indexOfComment];
                    Main.Content = currentPost.Text;
                }
                else
                {
                    indexOfComment--;
                    currentComment = comments[indexOfComment];
                    Main.Content = currentPost.Text;
                }
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
