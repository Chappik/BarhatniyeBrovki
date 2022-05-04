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
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace BarhatniyeBrovki
{
    /// <summary>
    /// Логика взаимодействия для Rename.xaml
    /// </summary>
    public partial class Rename : Window
    {
        //Подключение к базе данных

        static String connectionString = "server=LAPTOP-GJ0TPTB9\\SQLEXPRESS01; Initial Catalog=BarhatniyeBrovki;Integrated Security=SSPI";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataSet ds;
        Service srv;

        //конструктор который берет id услуги
        public int id { get; set; }

        public Rename(int id = 0)
        {

            
            //this.Resources.Add(srv, "service");
            InitializeComponent();
            this.id = id;

            FillList();

            //вывод услуги в текстовые поля для редактирования из базы
            TitleText.Text = srv.Title;
            CostTetxt.Text = Convert.ToString(srv.Cost);
            DurText.Text = Convert.ToString(srv.DurationInSeconds);
            ImgText.Text = srv.MainImagePath;


        }
        //Метод который выводит информацию из базы и записывает полям класса значения
        public void FillList()
        {
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("SELECT * FROM Service WHERE ID = " + id + "", con);
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
                        Cost = Convert.ToDecimal(dr[2].ToString()),
                        DurationInSeconds = Convert.ToInt32(dr[3].ToString()),
                        Description = dr[4].ToString(),
                        Discount = Convert.ToDouble(dr[5].ToString()),
                        MainImagePath = Convert.ToString(dr[6].ToString())
                    });
                    srv = new Service
                    {
                        ID = Convert.ToInt32(dr[0].ToString()),
                        Title = dr[1].ToString(),
                        Cost = Convert.ToDecimal(dr[2].ToString()),
                        DurationInSeconds = Convert.ToInt32(dr[3].ToString()),
                        Description = dr[4].ToString(),
                        Discount = Convert.ToDouble(dr[5].ToString()),
                        MainImagePath = Convert.ToString(dr[6].ToString())
                    };
                }
                //lstBox.ItemsSource = co1;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
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
        // Запрос на редактирование товара в базе
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                Service cr = new Service();
                cr.Title = Convert.ToString(TitleText.Text.ToString());
                cr.Cost = Convert.ToDecimal(CostTetxt.Text.ToString());
                cr.DurationInSeconds = Convert.ToInt32(DurText.Text.ToString());
                cr.MainImagePath = Convert.ToString(ImgText.Text.ToString());
                con = new SqlConnection(connectionString);
                con.Open();

                cmd = new SqlCommand("UPDATE Service SET Title = '" + cr.Title + "', Cost = '" + cr.Cost + "', DurationInSeconds = " + cr.DurationInSeconds + ", MainImagePath = '" + cr.MainImagePath + "' WHERE ID= " + id + " ", con);
                adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adapter.Fill(ds, "Service");
                ds = null;
                adapter.Dispose();
                con.Close();
                con.Dispose();


                System.Windows.MessageBox.Show("Услуга успешно обновлена!", "Уведомление");
                Admin zap = new Admin();
                zap.Show();
                this.Close();
            }
            catch
            {
                System.Windows.MessageBox.Show("Проверьте правильность ввода", "Ошибка");

            }

        }
        //Переход на другую страницу
        private void Button_ClickNazad(object sender, RoutedEventArgs e)
        {
            Admin zap = new Admin();
            zap.Show();
            this.Close();
        }
        //объявление переменной для работы с ней
        private string imgName;
        //метод который получает фотку и её путь по нажатию на кнопку
        private void add_img(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*jpg|All Files (*.*)|*.*";
            ofd.ShowDialog();
            string imgPath = ofd.FileName;

            string[] splitter = imgPath.Split('\\');

            imgName = @"C:\Users\Mvideo\source\repos\BarhatniyeBrovki\Images\" + splitter[splitter.Length - 1];

            var di = new DirectoryInfo(@"C:\Users\Mvideo\source\repos\BarhatniyeBrovki\Images\");


            System.IO.File.Copy(imgPath, imgName, true);
            ImgText.Text = imgPath;

            System.Windows.MessageBox.Show("ok!");
        }
    }
}
