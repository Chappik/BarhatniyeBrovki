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

namespace BarhatniyeBrovki
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Menu.Background = new SolidColorBrush(Color.FromRgb(225, 228, 255));
            Vazh.Foreground = new SolidColorBrush(Color.FromRgb(255, 74, 109));
            Vazhh.Foreground = new SolidColorBrush(Color.FromRgb(255, 74, 109));

        }

        //Переход на другую страницу

        private void PerehodOnZapis(object sender, RoutedEventArgs e)
        {

            Zapic zap = new Zapic();
            zap.Show();
            this.Close();
        }

        private void perehod(object sender, RoutedEventArgs e)
        {
            Window1 w = new Window1();
            w.Show();
            this.Close();
        }
    }
}
