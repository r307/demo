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

namespace WpfAppdataset
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataSet ds = new DataSet();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=dac55;Integrated Security=True;Pooling=False";
            //cn.ConnectionString=
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Employees";

            
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds, "Emps");
            

            cmd.CommandText = "select * from Departments";
            da.Fill(ds, "Deps");

            DataColumn[] arrCols = new DataColumn[1];
            arrCols[0] = ds.Tables["Emps"].Columns["EmpNo"];
            ds.Tables["Emps"].PrimaryKey = arrCols;

            //adding a foreign key constraint
            ds.Relations.Add(ds.Tables["Deps"].Columns["DeptNo"],
                ds.Tables["Emps"].Columns["DeptNo"]);

            //not null constraint (Col level constraints)
            ds.Tables["Deps"].Columns["DeptNo"].AllowDBNull = false;
            dgEmps.ItemsSource = ds.Tables["Emps"].DefaultView;
            dgDeps.ItemsSource = ds.Tables["Deps"].DefaultView;
            cn.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=dac55;Integrated Security=True;Pooling=False";
            cn.Open();

            SqlCommand cmdUpdate = new SqlCommand();
            cmdUpdate.Connection = cn;
            cmdUpdate.CommandType = CommandType.Text;
            cmdUpdate.CommandText = "update Employees set Name=@Name,Basic=@Basic,Deptno=@Deptno where Empno=@Empno";

            cmdUpdate.Parameters.Add(new SqlParameter { ParameterName = "@Name", SourceColumn = "Name", SourceVersion = DataRowVersion.Current });
            cmdUpdate.Parameters.Add(new SqlParameter { ParameterName = "@Deptno", SourceColumn = "Deptno", SourceVersion = DataRowVersion.Current });
            cmdUpdate.Parameters.Add(new SqlParameter { ParameterName = "@Basic", SourceColumn = "Basic", SourceVersion = DataRowVersion.Current });
            cmdUpdate.Parameters.Add(new SqlParameter { ParameterName = "@Empno", SourceColumn = "Empno", SourceVersion = DataRowVersion.Original });

            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = cn;
            cmdInsert.CommandType = CommandType.Text;
            cmdInsert.CommandText = "Insert into Employees values(@Empno,@Name,@Basic,@Deptno)";

            cmdInsert.Parameters.Add(new SqlParameter { ParameterName = "@Name", SourceColumn = "Name", SourceVersion = DataRowVersion.Current });
            cmdInsert.Parameters.Add(new SqlParameter { ParameterName = "@Deptno", SourceColumn = "Deptno", SourceVersion = DataRowVersion.Current });
            cmdInsert.Parameters.Add(new SqlParameter { ParameterName = "@Basic", SourceColumn = "Basic", SourceVersion = DataRowVersion.Current });
            cmdInsert.Parameters.Add(new SqlParameter { ParameterName = "@Empno", SourceColumn = "Empno", SourceVersion = DataRowVersion.Current });

            SqlCommand cmdDelete = new SqlCommand();
            cmdDelete.Connection = cn;
            cmdDelete.CommandType = CommandType.Text;
            cmdDelete.CommandText = "delete from Employees where Empno=@Empno";
            
            cmdDelete.Parameters.Add(new SqlParameter { ParameterName = "@Empno", SourceColumn = "Empno", SourceVersion = DataRowVersion.Original });
            SqlDataAdapter da = new SqlDataAdapter();
            da.UpdateCommand = cmdUpdate;
            da.DeleteCommand= cmdDelete;
            da.InsertCommand = cmdInsert;
            da.Update(ds, "Emps");


        }

        private void btnview_Click(object sender, RoutedEventArgs e)
        {
            DataView dv = new DataView(ds.Tables["Emps"]);
            dv.RowFilter = "DeptNo=" + txtDeptNo.Text;
            dv.Sort = "";
            dgEmps.ItemsSource = dv;

            //ds.Tables["Emps"].DefaultView.RowFilter = "DeptNo=" + txtDeptNo.Text;
        }
    }
}
