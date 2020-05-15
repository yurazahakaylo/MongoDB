using Service;
using System.Windows;

namespace WpfSocialNetwork
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        UserService services;
        string loginUsername;
        public ChangePassword(string loginUsername)
        {
            InitializeComponent();
            services = new UserService();
            this.loginUsername = loginUsername;
        }

        private void Change(object sender, RoutedEventArgs e)
        {
            bool temp = services.UpdatePassword(loginUsername, oldPwd.Password, newPwd.Password);
            if (temp == true)
            {
                MessageBox.Show("Done");
            }
            else
            {
                MessageBox.Show("Password was not changed");
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
