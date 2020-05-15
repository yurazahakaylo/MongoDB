using Service;
using System.Windows;

namespace WpfSocialNetwork
{
    /// <summary>
    /// Interaction logic for SearchUser.xaml
    /// </summary>
    public partial class SearchUser : Window
    {
        UserService service;
        private string loginUsername;

        public SearchUser(string username)
        {
            loginUsername = username;
            InitializeComponent();
            service = new UserService();
        }

        private void Search(object sender, RoutedEventArgs e)
        {

            if (service.CheckIfUserIsInDatabase(searchUser.Text))
            {
                Profile profile = new Profile(loginUsername, searchUser.Text)
                {
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };
                profile.ShowDialog();
                Close();
            }
            else
            {
                MessageBox.Show("There is no such user");
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            General general = new General();
            general.Show();
            Close();
        }
    }
}
