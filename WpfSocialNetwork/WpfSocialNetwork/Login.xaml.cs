using Service;
using System.Windows;

namespace WpfSocialNetwork
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        UserService service;

        public Login()
        {
            InitializeComponent();
            service = new UserService();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LogIn(object sender, RoutedEventArgs e)
        {
            if (service.CheckPassword(Username.Text, Password.Password.ToString()))
            {

                service.NicknameWrite(Username.Text);
                General general = new General(Username.Text)
                {
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };
                general.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Incorect password");
            }
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration()
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            registration.Show();
            Close();
        }
    }
}
