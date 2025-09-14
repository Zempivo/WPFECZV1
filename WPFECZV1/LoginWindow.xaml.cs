using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using WPFECZV1.Models;

namespace WPFECZV1
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            this.Closing += LoginWindow_Closing;
        }

        private void LoginWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Application.Current.Windows.Count <= 1)
            {
                Application.Current.Shutdown();
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Заполните все поля";
                return;
            }

            var user = App.Context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Login == login && u.Password == password);

            if (user != null)
            {
                if (user.Role.Name == "admin")
                {
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                }
                else
                {
                    UserWindow userWindow = new UserWindow();
                    userWindow.Show();
                }
                this.Close();
            }
            else
            {
                lblMessage.Text = "Неверный логин или пароль";
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }
    }
}