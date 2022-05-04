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
using System.Collections;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace BarhatniyeBrovki
{
    /// <summary>
    /// Логика взаимодействия для Zapic.xaml
    /// </summary>
    public partial class Zapic : Window
    {
        //Подключение к базе данных
        static String connectionString = "server=LAPTOP-GJ0TPTB9\\SQLEXPRESS01; Initial Catalog=BarhatniyeBrovki;Integrated Security=SSPI";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataSet ds;

        public Zapic()
        {
           
            InitializeComponent();
            Vazh.Foreground = new SolidColorBrush(Color.FromRgb(255, 74, 109));
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
                    }) ;

                }
                lstBox.ItemsSource = co1;

                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("SELECT COUNT(*) FROM Service", con);
                adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adapter.Fill(ds, "Service");
                string name = cmd.ExecuteScalar().ToString();
                count.Text = name;
                ds = null;
                adapter.Dispose();
                con.Close();
                con.Dispose();

            }
            catch (Exception ex)
            {

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
            public int Count { get; set; }
        }
       
        //Переход на другую страницу
        private void PerehodOnAdmin(object sender, RoutedEventArgs e)
        {
            Perehod per = new Perehod();
            per.Show();
            this.Close();
        }
        //Запрос на поиск товаров 
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string intext = text.Text.ToString();
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("SELECT * FROM Service WHERE Title LIKE '" + intext.ToString() + "%' OR Cost LIKE '" + intext.ToString() + "%' ", con);
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
                        Description = dr[4].ToString() ,
                        Discount = Convert.ToDouble(dr[5].ToString()),
                        MainImagePath = Convert.ToString(dr[6].ToString())
                    }) ;

                }
                lstBox.ItemsSource = co1;
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("SELECT COUNT(*) FROM Service WHERE Title LIKE '" + intext.ToString() + "%' OR Cost LIKE '" + intext.ToString() + "%' ", con);
                adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adapter.Fill(ds, "Service");
                string name = cmd.ExecuteScalar().ToString();
                filtr.Text = name;
                ds = null;
                adapter.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ds = null;
                adapter.Dispose();
                con.Close();
                con.Dispose();
            }

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            //берем значение из комбобокса
            ComboBox cmBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)cmBox.SelectedItem;
            string intext = selectedItem.Content.ToString();


            try
            {
                if(intext == "Без фильтров")
                {
                    intext = "0";
                }
                else if (intext == "до 5%")
                    intext = "0.05";
                else if (intext == "от 10% до 15%")
                    intext = "0.1";
                else if (intext == "от 20% до 25%")
                    intext = "0.2";
                else if (intext == "до 30%")
                    intext = "0.3";


                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("SELECT * FROM Service WHERE Discount LIKE '" + intext.ToString() + "%'", con);
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

                }
                lstBox.ItemsSource = co1;



                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("SELECT COUNT(*) FROM Service WHERE Discount LIKE '" + intext.ToString() + "%'", con);
                adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adapter.Fill(ds, "Service");
                string name = cmd.ExecuteScalar().ToString();
                
                filtr.Text = name;
                ds = null;
                adapter.Dispose();
                con.Close();
                con.Dispose();


            }
            catch (Exception ex)
            {

            }
            finally
            {
                ds = null;
                adapter.Dispose();
                con.Close();
                con.Dispose();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
