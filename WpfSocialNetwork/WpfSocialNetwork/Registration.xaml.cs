using Service;
using System.Windows;

namespace WpfSocialNetwork
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        UserService service;
        public Registration()
        {
            InitializeComponent();
            service = new UserService();
        }


        private void Confirm(object sender, RoutedEventArgs e)
        {
            if (Username.Text != "")
            {
                if (service.CheckIndentityOfNickname(Username.Text))
                {
                    if (Password.Password.ToString() == ConfirmPassword.Password.ToString() && Password.Password.ToString() != "")
                    {
                        try
                        {
                            service.InsertUser(Username.Text, Email.Text, Password.Password.ToString());
                            service.NicknameWrite(Username.Text);
                            General general = new General(Username.Text)
                            {
                                WindowStartupLocation = WindowStartupLocation.CenterScreen
                            };
                            general.Show();
                            Close();
                        }
                        catch
                        {
                            MessageBox.Show("Smth bad happened");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Incorrect confirm password");
                    }
                }
                else
                {
                    MessageBox.Show("Such username already exists");
                }

            }
            else
            {
                MessageBox.Show("Please, fill all fields");
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();
        }
    }
}
