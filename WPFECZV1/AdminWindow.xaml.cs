using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace WPFECZV1
{
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            LoadBooks();
            LoadUsers();
            this.Closing += AdminWindow_Closing;
        }

        private void AdminWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Application.Current.Windows.Count <= 1)
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
            }
        }

        private void LoadData()
        {
            try
            {
                LoadBooks();
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadBooks()
        {
            var books = App.Context.Books
                .Include(b => b.Reader)
                .ToList();
            booksGrid.ItemsSource = books;
        }

        private void LoadUsers()
        {
            var users = App.Context.Users
                .Include(u => u.Role)
                .ToList();
            usersGrid.ItemsSource = users;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите выйти?", "Выход",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
        }
    }
}