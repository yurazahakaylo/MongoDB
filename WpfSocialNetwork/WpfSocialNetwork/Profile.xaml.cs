using Model;
using Repository;
using Service;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace WpfSocialNetwork
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        bool isAnyPosts = false;
        int indexOfPost = 0;
        UserRepository userRepository;
        PostService postService;
        UserService userService;
        User user;
        List<Post> posts;

        Post currentPost;

        string personNickname;
        string loginUsername;

        public Profile(string loginUsername, string visitUsername)
        {
            InitializeComponent();
            personNickname = visitUsername;
            this.loginUsername = loginUsername;

            if (loginUsername == visitUsername)
            {
                followButton.Visibility = Visibility.Hidden;
                unfollowButton.Visibility = Visibility.Hidden;
            }
            else
            {
                newPost.Visibility = Visibility.Hidden;
                editPost.Visibility = Visibility.Hidden;
                deletePost.Visibility = Visibility.Hidden;
            }

            userRepository = new UserRepository();
            userService = new UserService();
            postService = new PostService();
            
            user = userService.GetUser(visitUsername);
            
            Email.Text = user.Email;
            Username.Text = user.Nickname;

            currentPost = new Post();
            posts = new List<Post>();

            posts = postService.GetPosts(visitUsername);

            if (posts != null && posts.Count > 0)
            {
                currentPost = posts[indexOfPost];
                Main.Content = currentPost.Text;
                isAnyPosts = true;
            }
            else
            {
                Main.Content = "There is no posts";
            }
            if (!isAnyPosts)
            {
                addComment.Visibility = Visibility.Hidden;
                seeComment.Visibility = Visibility.Hidden;
            }

        }

        
        private void Posts(object sender, RoutedEventArgs e)
        {
            posts = postService.GetPosts(personNickname);
            if (isAnyPosts)
            {
                if (indexOfPost > 0)
                {
                    indexOfPost--;
                    currentPost = posts[indexOfPost];
                    Main.Content = currentPost.Text;
                }
                else
                {
                    currentPost = posts[indexOfPost];
                    Main.Content = currentPost.Text;
                }
            }
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            posts = postService.GetPosts(personNickname);
            if (posts != null && posts.Count > 0)
            {
                currentPost = posts[indexOfPost];
                Main.Content = currentPost.Text;
                isAnyPosts = true;
                addComment.Visibility = Visibility.Visible;
                seeComment.Visibility = Visibility.Visible;
            }
            else
            {
                isAnyPosts = false;
                addComment.Visibility = Visibility.Hidden;
                seeComment.Visibility = Visibility.Hidden;
                Main.Content = "There is no posts";
            }
        }

        private void NextPost(object sender, RoutedEventArgs e)
        {
            posts = postService.GetPosts(personNickname);
            if (isAnyPosts)
            {
                indexOfPost++;
                if (indexOfPost < posts.Count)
                {
                    currentPost = posts[indexOfPost];
                    Main.Content = currentPost.Text;
                }
                else
                {
                    indexOfPost--;
                    currentPost = posts[indexOfPost];
                    Main.Content = currentPost.Text;
                }
            }
        }


        private void Follow(object sender, RoutedEventArgs e)
        {
            if (!userService.CheckAlreadyFollow(loginUsername, personNickname))
            {
                userRepository.Following(userService.NicknameRead(), personNickname);
                userRepository.Follow(personNickname, userService.NicknameRead());
                followButton.Background = Brushes.Green;
            }
        }

        private void Unfollow(object sender, RoutedEventArgs e)
        {
            userRepository.Unfollow(loginUsername, personNickname);
            Color color = (Color)ColorConverter.ConvertFromString("#0288d1");
            SolidColorBrush brush = new SolidColorBrush(color);
            followButton.Background = brush;
        }
        

        private void AddComment(object sender, RoutedEventArgs e)
        {
            NewComment com = new NewComment(currentPost.Id, loginUsername)
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            com.ShowDialog();
        }


        private void AddNewPost(object sender, RoutedEventArgs e)
        {
            ChangePost newPost = new ChangePost(personNickname, "add")
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            newPost.ShowDialog();
        }

        private void SeeComment(object sender, RoutedEventArgs e)
        {
            ViewComments comments = new ViewComments(currentPost)
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            comments.ShowDialog();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            General general = new General(loginUsername);
            general.Show();
            Close();
        }

        private void EditPost(object sender, RoutedEventArgs e)
        {
            ChangePost editPost = new ChangePost(personNickname,"edit", currentPost)
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            editPost.ShowDialog();
        }

        private void DeletePost(object sender, RoutedEventArgs e)
        {
            if (isAnyPosts)
            {
                bool temp = postService.DeletePost(currentPost.Id);
                if (temp)
                {
                    MessageBox.Show("Done");
                }
                else
                {
                    MessageBox.Show("Post was not deleted");
                }
            }
        }

        private void AddComment_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
