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
using System.Data.SqlClient;
using System.Data;

namespace question1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowGrid();
        }
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-6C27O5K;Initial Catalog=question1DB;Integrated Security=True");
        
        public void ShowGrid()
        {
            SqlCommand command = new SqlCommand("select * from question1Table", connection);
            DataTable dt = new DataTable();
            connection.Open();
            SqlDataReader sdr = command.ExecuteReader();
            dt.Load(sdr);
            connection.Close();
            DataGrid.ItemsSource = dt.DefaultView;
            

        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void ClearData()
        {
            Name_text.Clear();
            Age_text.Clear();
            Gender_text.Clear();
            Gmail_text.Clear();
            id.Clear();
        }

        private void ClearDataBtn(object sender, RoutedEventArgs e)
        {
            ClearData();
        }

        public bool isValid()
        {
            if(Name_text.Text == String.Empty)
            {
                MessageBox.Show("name is required");
                return false;
            }
            if(Age_text.Text == String.Empty)
            {
                MessageBox.Show("age is required");
                return false;
            }
            if(Gender_text.Text == String.Empty)
            {
                MessageBox.Show("gender is required");
                return false;
            }
            if(Gmail_text.Text == String.Empty)
            {
                MessageBox.Show("gmail is required");
                return false;
            }
            return true;
        }

        private void InsertDataBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                SqlCommand command = new SqlCommand("INSERT INTO question1Table VALUES(@Name, @Age, @Gender, @Gmail)",connection);
                command.CommandType= CommandType.Text;
                command.Parameters.AddWithValue("@Name", Name_text.Text);
                command.Parameters.AddWithValue("@Age", Age_text.Text);
                command.Parameters.AddWithValue("@Gender", Gender_text.Text);
                command.Parameters.AddWithValue("@Gmail", Gmail_text.Text);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                ShowGrid();
                MessageBox.Show("Registration Done");
                ClearData();
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateDataBtn(object sender, RoutedEventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("UPDATE question1Table SET Name = '"+ Name_text.Text+"', Age = '"+Age_text.Text+"', Gender = '"+Gender_text.Text+"', Gmail= '"+Gmail_text.Text+"'WHERE Id = '"+ id.Text+ " '", connection);
            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Record has been updated");
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
                ClearData();
                ShowGrid();
                connection.Close();
            }
        }

        private void DeleteDataBtn(object sender, RoutedEventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("DELETE FROM question1Table WHERE Id = "+ id.Text+ " ", connection);
            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Record has been deleted");
                connection.Close();
                ClearData();
                ShowGrid();
                connection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
