using Model;
using MongoDB.Bson;
using Service;
using System.Windows;

namespace WpfSocialNetwork
{
    /// <summary>
    /// Interaction logic for ChangePost.xaml
    /// </summary>
    public partial class ChangePost : Window
    {
        PostService services;
        string username;
        Post currentPost;

        public ChangePost(string username, string action)
        {
            InitializeComponent();
            services = new PostService();
            this.username = username;
            verifyAction(action);
        }

        public ChangePost(string username, string action, Post currentPost)
        {
            InitializeComponent();
            services = new PostService();
            Text.Text = currentPost.Text;
            this.username = username;
            this.currentPost = currentPost;
            verifyAction(action);
        }

        private void verifyAction(string action)
        {
            if (action == "add")
            {
                editButton.Visibility = Visibility.Hidden;
            }
            else
            {
                addButton.Visibility = Visibility.Hidden;
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            services.InsertPost(Text.Text, username);
            Close();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            services.EditPost(Text.Text, currentPost.Id);
            Close();
        }

    }
}
