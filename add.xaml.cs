using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BarhatniyeBrovki
{
    /// <summary>
    /// Логика взаимодействия для add.xaml
    /// </summary>
    public partial class add : Window
    {
        //Подключение к базе данных

        static String connectionString = "server=LAPTOP-GJ0TPTB9\\SQLEXPRESS01; Initial Catalog=BarhatniyeBrovki;Integrated Security=SSPI";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataSet ds;


        public add()
        {
            InitializeComponent();

        }

        //Запрос на добавление в базу данных
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Service cr = new Service();
                cr.Title = Convert.ToString(TitleText.Text.ToString());
                cr.Cost = Convert.ToDecimal(CostTetxt.Text.ToString());
                cr.DurationInSeconds = Convert.ToInt32(DurText.Text.ToString());
                cr.Description = Convert.ToString(DescText.Text.ToString());
                cr.Discount = Convert.ToDouble(DiscountText.Text.ToString());
                cr.MainImagePath = Convert.ToString(ImgText.Text.ToString());



                //string a = Convert.ToString(cr.Discount / 100);









                // MessageBox.Show(cr.Title + cr.Cost + cr.DurationInSeconds.ToString() + cr.Description + cr.Discount + cr.MainImagePath);
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("INSERT INTO Service (Title, Cost, DurationInSeconds, Description, Discount, MainImagePath) VALUES ('" + cr.Title + "', " + cr.Cost + " , " + cr.DurationInSeconds + ", '" + cr.Description + "', '" + "0." + cr.Discount + "', '" + cr.MainImagePath + "')", con);
                adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adapter.Fill(ds, "Service");
                ds = null;
                adapter.Dispose();
                con.Close();
                con.Dispose();
                System.Windows.MessageBox.Show("Данные успешно добавленны");
            }

            catch
            {
                System.Windows.MessageBox.Show("Проверьте правильность ввода данных");
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

        
        //Переход на другую страницу
        private void Button_ClickNazad(object sender, RoutedEventArgs e)
        {
            Admin zap = new Admin();
            zap.Show();
            this.Close();
        }

        

        private void TitleText_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            if (int.TryParse(e.Text, out int i))
            {
                e.Handled = true;
            }

        }

       

        

        private void DurTetxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
        private string imgName;
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
