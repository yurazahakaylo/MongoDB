using System.Windows;

namespace WpfSocialNetwork
{
    /// <summary>
    /// Interaction logic for General.xaml
    /// </summary>
    public partial class General : Window
    {
        string loginUsername;

        public General(string username)
        {
            InitializeComponent();
            loginUsername = username;
        }

        public General()
        {
            InitializeComponent();
        }

        private void MyProfile(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile(loginUsername, loginUsername)
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            profile.Show();
            Close();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            SearchUser search = new SearchUser(loginUsername)
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            search.Show();
            Close();
        }

        private void ChangePassword(object sender, RoutedEventArgs e)
        {
            ChangePassword changePassword = new ChangePassword(loginUsername)
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            changePassword.Show();
            Close();

        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
