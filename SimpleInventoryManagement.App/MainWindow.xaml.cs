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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleInventoryManagement.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isLoggedIn = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isLoggedIn)
            {
                var loginDialog = new LoginDialog();
                if (loginDialog.ShowDialog() == true)
                {
                    isLoggedIn = true;
                    ChangeToLogoutState();
                    UserTextBlock.Text = loginDialog.Username;
                    MainFrame.Content = new InventoryTransactionPage();
                    MainFrame.Background = Brushes.Transparent;
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    isLoggedIn = false;
                    ChangeToLoginState();
                    UserTextBlock.Text = string.Empty;
                    MainFrame.Content = null;
                    MainFrame.Background = Brushes.LightGray;
                }
            }
        }

        private void ChangeToLogoutState()
        {
            LoginImage.Source = new BitmapImage(new Uri("Images/logout_icon.png", UriKind.Relative));

            LoginTextBlock.Text = "Logout";
            LoginTextBlock.Foreground = Brushes.Red;
        }

        private void ChangeToLoginState()
        {
            LoginImage.Source = new BitmapImage(new Uri("Images/login_icon.png", UriKind.Relative));
            LoginTextBlock.Text = "Login";
            LoginTextBlock.Foreground = Brushes.Black;
        }
    }
}
