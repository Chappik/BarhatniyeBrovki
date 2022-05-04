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

namespace BarhatniyeBrovki
{
    /// <summary>
    /// Логика взаимодействия для Perehod.xaml
    /// </summary>
    public partial class Perehod : Window
    {
        public Perehod()
        {
            InitializeComponent();
        }
        //Переход на другую страницу

        private void LogOut(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == "0000")
            {
                Admin ad = new Admin();
                ad.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка ввода пароля");
            }
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
