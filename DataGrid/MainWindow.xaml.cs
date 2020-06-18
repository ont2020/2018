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
using MySql.Data.MySqlClient;
using System.Data;

namespace DataGrid
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection conn;
        DataRowView a;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            conn = new MySqlConnection("server = localhost; uid = root; pwd = щтекщще; database = amonic");
            conn.Open();
            Load();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            conn.Close();
            Close();
        }

        private void Load()
        {
            MySqlCommand comm = new MySqlCommand($"select id, name, surname, tip, email, status from users", conn);
            MySqlDataReader read = comm.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(read);
            dataGrid.ItemsSource = table.DefaultView;
            read.Close();
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MySqlCommand comm;
            DataRowView a = dataGrid.SelectedItem as DataRowView;
            if (a["status"].ToString() == "1")
            {
                comm = new MySqlCommand($"update users set status = 0 where id = " + a["id"].ToString(), conn);
            }
            else
            {
                comm = new MySqlCommand($"update users set status = 1 where id = " + a["id"].ToString(), conn);
            }
            MySqlDataReader read = comm.ExecuteReader();
            read.Close();
            Load();
        }
    }
}
