using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SimpleInventoryManagement.App
{
    /// <summary>
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : Window
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        private readonly MyInventoryManagementDataContext dataContext;

        public LoginDialog()
        {
            InitializeComponent();

            dataContext = new MyInventoryManagementDataContext();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Username = TxtBoxUsername.Text;
            Password = PassBoxPassword.Password;

            if (ValidateUser(Username, Password))
            {
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateUser(string username, string password)
        {
            var user = dataContext.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            return user != null;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
