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

namespace WpfDataSheet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btninsert_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=Rupesh;Integrated Security=True";
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            //cmd.CommandType = CommandType.Text;
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "insert into Employees values(1,'Sagar',50000,10)";
            //cmd.CommandText = "insert into Employees values(" + txtempno.Text + ",'" + txtname.Text + "'," + txtbasic.Text + "," + txtdeptno.Text + ")";
            //cmd.CommandText = "insert into Employees values(@Empno,@Name,@Basic,@Deptno)";
            cmd.CommandText = "InsertEmployee";
            cmd.Parameters.AddWithValue("@Empno", txtempno.Text);
            cmd.Parameters.AddWithValue("@Name", txtname.Text);
            cmd.Parameters.AddWithValue("@Basic",txtbasic.Text);
            cmd.Parameters.AddWithValue("@Deptno", txtdeptno.Text);
            cmd.ExecuteNonQuery();
            cn.Close();

        }

        private void Execute_Scalar_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=Rupesh;Integrated Security=True";
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select count(*) from Employees";
            lblcount.Content = cmd.ExecuteScalar();

            cn.Close();
            
        }

        private void btntransaction_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=Rupesh;Integrated Security=True";
            cn.Open();

            SqlTransaction t = cn.BeginTransaction();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.Transaction = t;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Employees values(5,'Hiren',67000,20)";

            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = cn;
            cmd2.Transaction = t;
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "insert into Employees values(6,'Juee',56000,30)";

            try
            {
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                t.Commit();
                MessageBox.Show("Commit");
            }
            catch
            {
                t.Rollback();
                MessageBox.Show("Rollback");
            }
            cn.Close();
            
        }

        private void btndelete_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=dac55;Integrated Security=True";
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteEmployee";
            cmd.Parameters.AddWithValue("@Empno", txtempno.Text);
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        private void btnupdate_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=Rupesh;Integrated Security=True";
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UpdateEmployee";
            cmd.Parameters.AddWithValue("@Empno", txtempno.Text);
            cmd.Parameters.AddWithValue("@Name",txtname.Text);
            cmd.Parameters.AddWithValue("@Basic", txtbasic.Text);
            cmd.Parameters.AddWithValue("@Deptno", txtdeptno.Text);
            cmd.ExecuteNonQuery();
            cn.Close();
            
        }
    }
}
