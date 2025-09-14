using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace WPFECZV1
{
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();
            LoadBooks();
            this.Closing += UserWindow_Closing;
        }

        private void UserWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Application.Current.Windows.Count <= 1)
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
            }
        }

        private void LoadBooks()
        {
            try
            {
                var books = App.Context.Books
                    .Include(b => b.Reader)
                    .ToList();
                booksGrid.ItemsSource = books;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadBooks();
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