using MongoDB.Bson;
using Service;
using System.Windows;

namespace WpfSocialNetwork
{
    /// <summary>
    /// Interaction logic for NewComment.xaml
    /// </summary>
    public partial class NewComment : Window
    {
        PostService services;
        UserService userService;
        ObjectId postId;
        string username;

        public NewComment(ObjectId id , string username)
        {
            InitializeComponent();
            services = new PostService();
            userService = new UserService();
            this.username = username;
            postId = id;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            services.AddComment(Text.Text, postId, userService.GetUser(username));
            Close();
        }
    }
}
