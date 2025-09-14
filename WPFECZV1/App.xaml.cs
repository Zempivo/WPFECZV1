using System;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using WPFECZV1.Models;

namespace WPFECZV1
{
    public partial class App : Application
    {
        public static Wpfeczv1Context Context { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                Context = new Wpfeczv1Context();
                Context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}");
                Shutdown();
            }
        }
    }
}