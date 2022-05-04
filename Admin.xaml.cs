using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
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
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        //Подключение к базе данных
        static String connectionString = "server=LAPTOP-GJ0TPTB9\\SQLEXPRESS01; Initial Catalog=BarhatniyeBrovki;Integrated Security=SSPI";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataSet ds;

        public Admin()
        {
           
            InitializeComponent();
            //При инициализации выводим наш метод с с выводом в лист
            FillList();
            Menu.Background = new SolidColorBrush(Color.FromRgb(225, 228, 255));
        }

        
        //Запрос на вывод всех услуг из базы
        public void FillList()
        {
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("SELECT * FROM Service", con);
                adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adapter.Fill(ds, "Service");
                Service co = new Service();
                IList<Service> co1 = new List<Service>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    co1.Add(new Service
                    {
                        ID = Convert.ToInt32(dr[0].ToString()),
                        Title = dr[1].ToString(),
                        Cost = Convert.ToDecimal(dr[2].ToString().Trim()),

                        DurationInSeconds = Convert.ToInt32(dr[3].ToString()),
                        Description = dr[4].ToString(),
                        Discount = Convert.ToDouble(dr[5].ToString()),
                        MainImagePath = Convert.ToString(dr[6].ToString())
                    });

                }
                lstBox.ItemsSource = co1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка со стороны сервера");
            }
            finally
            {
                ds = null;
                adapter.Dispose();
                con.Close();
                con.Dispose();
            }
        }
        //Класс для обращение к базе и присваивание имен полям

        public class Service
        {
            public int ID { get; set; }
            public string Title { get; set; }
            public decimal Cost { get; set; }
            public int DurationInSeconds { get; set; }
            public string Description { get; set; }
            public double Discount { get; set; }
            public string MainImagePath { get; set; }
        }                                                      


        private void PerehodOnUslug(object sender, RoutedEventArgs e)
        {
           
        }

        
        //Переход на другую страницу
        private void PerehodOnZapis(object sender, RoutedEventArgs e)
        {
            Zapic zap = new Zapic();
            zap.Show();
            this.Close();
        }


        //Переход на другую страницу
        private void Add(object sender, RoutedEventArgs e)
        {
            add ad = new add();
            ad.Show();
            this.Close();
            
        }
        //Функция удаления из базы
        private void del(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Удалить услугу",
                  "Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    int a = Convert.ToInt32((sender as Button).Uid);
                    con = new SqlConnection(connectionString);
                    con.Open();
                    cmd = new SqlCommand("DELETE FROM Service WHERE ID = " + a + "", con);
                    adapter = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    adapter.Fill(ds, "Service");
                    ds = null;
                    adapter.Dispose();
                    con.Close();
                    con.Dispose();


                    

                }
                else
                {
                    MessageBox.Show("Ошибка со стороны сервера");
                }
            }

            catch(Exception ex)
            {

            }
           

            
        }
        //Переход на другую страницу
        private void Rename(object sender, RoutedEventArgs e)
        {
            int a = Convert.ToInt32((sender as Button).Uid);

            Rename ad = new Rename(a);
            ad.Show();
            this.Close();
        }

        private void refresh(object sender, RoutedEventArgs e)
        {
            Admin a = new Admin();
            a.Show();
            this.Close();
        }
    }
}
