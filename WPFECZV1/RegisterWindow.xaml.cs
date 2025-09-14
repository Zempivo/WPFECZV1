using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using WPFECZV1.Models;

namespace WPFECZV1
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Password) ||
                string.IsNullOrWhiteSpace(txtSurname.Text) ||
                string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                lblMessage.Text = "Все поля обязательны для заполнения";
                return false;
            }

            if (txtPassword.Password != txtConfirmPassword.Password)
            {
                lblMessage.Text = "Пароли не совпадают";
                return false;
            }

            if (txtPassword.Password.Length < 6)
            {
                lblMessage.Text = "Пароль должен содержать минимум 6 символов";
                return false;
            }

            if (!PhoneValidator.IsValidPhone(txtPhone.Text))
            {
                lblMessage.Text = "Некорректный формат телефона. Пример: +79991234567";
                return false;
            }

            return true;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                bool userExists = App.Context.Users.Any(u => u.Login == txtLogin.Text);
                if (userExists)
                {
                    lblMessage.Text = "Пользователь с таким логином уже существует";
                    return;
                }

                txtPhone.Text = PhoneValidator.FormatPhone(txtPhone.Text);

                var userRole = App.Context.Roles.FirstOrDefault(r => r.Name == "user");
                if (userRole == null)
                {
                    lblMessage.Text = "Ошибка: роль 'user' не найдена в системе";
                    return;
                }

                var newUser = new User
                {
                    Login = txtLogin.Text,
                    Password = txtPassword.Password,
                    Surname = txtSurname.Text,
                    Name = txtName.Text,
                    Phone = txtPhone.Text,
                    RoleId = userRole.Id,
                    RegistrationDate = DateOnly.FromDateTime(DateTime.Now)
                };

                App.Context.Users.Add(newUser);
                App.Context.SaveChanges();

                MessageBox.Show("Регистрация прошла успешно! Теперь вы можете войти в систему.",
                    "Успешная регистрация", MessageBoxButton.OK, MessageBoxImage.Information);

                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Ошибка при регистрации: {ex.Message}";
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}