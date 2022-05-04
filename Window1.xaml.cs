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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        static String connectionString = "server=LAPTOP-GJ0TPTB9\\SQLEXPRESS01; Initial Catalog=BarhatniyeBrovki;Integrated Security=SSPI";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataSet ds;

        public Window1()
        {
            InitializeComponent();

            System.Timers.Timer timer = new System.Timers.Timer(30000);
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Elapsed += new ElapsedEventHandler(timerTick);
            timer.Start();
            FillList();
        }

        private void PerehodOnAdmin(object sender, RoutedEventArgs e)
        {
            MainWindow zap = new MainWindow();
            zap.Show();
            this.Close();
        }
        public void FillList()
        {
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("SELECT ClientService.ID,ClientService.ClientID, ClientService.ServiceID, ClientService.StartTime, ClientService.Comment, Client.FirstName FROM ClientService,  Client WHERE Client.ID = ClientService.ClientID", con);
                adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adapter.Fill(ds, "ClientService");
                Services co = new Services();
                List<Services> co1 = new List<Services>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    co1.Add(new Services
                    {
                        ID = Convert.ToInt32(dr[0].ToString()),
                        ClientID = Convert.ToInt32(dr[1].ToString()),
                        ServiceID = Convert.ToInt32(dr[2].ToString()),
                        StartTime = Convert.ToDateTime(dr[3].ToString()),
                        Comment = dr[4].ToString(),
                        Name = dr[5].ToString()
                    });

                }
                lstBoxx.ItemsSource = co1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ds = null;
                adapter.Dispose();
                con.Close();
                con.Dispose();
            }
        }

        
        private void timerTick(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                lstBoxx.Visibility = Visibility.Visible;
                if (lstBoxx.SelectedItems != null)
                    lstBoxx.Items.Refresh();
            });
            

        }

        public class Services
        {
            public int ID { get; set; }
            public int ClientID { get; set; }
            public int ServiceID { get; set; }
            public System.DateTime StartTime { get; set; }
            public string Comment { get; set; }

            public string Name { get; set; }
        }
    }
}
