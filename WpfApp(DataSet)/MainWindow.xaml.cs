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

namespace WpfApp_DataSet_
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=dac55;Integrated Security=True";
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            

            SqlDataAdapter da = new SqlDataAdapter();
            cmd.CommandText = "select * from Employees";
            da.SelectCommand = cmd;
            da.Fill(ds, "Emps");

            cmd.CommandText = "select * from Departments";
            da.SelectCommand = cmd;
            da.Fill(ds, "Deps");
            //add primary key to emps datatable
            DataColumn[] arrcols = new DataColumn[1];
            arrcols[0] = ds.Tables["Emps"].Columns["Empno"];
            ds.Tables["Emps"].PrimaryKey = arrcols;
            //add a foriegn key 
            ds.Relations.Add(ds.Tables["Deps"].Columns["Deptno"], ds.Tables["Emps"].Columns["Deptno"]);
            
            //not null constraint
            ds.Tables["Deps"].Columns["Deptno"].AllowDBNull = false;
            ds.Tables["Emps"].Columns["Empno"].AllowDBNull = false;

            gride.ItemsSource = ds.Tables["Emps"].DefaultView;
            gridd.ItemsSource = ds.Tables["Deps"].DefaultView;


            cn.Close();

        }

        private void btnupdate_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=dac55;Integrated Security=True";
            cn.Open();

            SqlCommand cmdUpdate = new SqlCommand();
            cmdUpdate.Connection = cn;
            cmdUpdate.CommandType = CommandType.Text;
            cmdUpdate.CommandText = "update Employees set Name=@Name,Basic=@Basic,Deptno=@Deptno where Empno=@Empno";

            cmdUpdate.Parameters.Add(new SqlParameter { ParameterName = "@Name", SourceColumn = "Name", SourceVersion = DataRowVersion.Current });
            cmdUpdate.Parameters.Add(new SqlParameter { ParameterName = "@Deptno", SourceColumn = "Deptno", SourceVersion = DataRowVersion.Current });
            cmdUpdate.Parameters.Add(new SqlParameter { ParameterName = "@Basic", SourceColumn = "Basic", SourceVersion = DataRowVersion.Current });
            cmdUpdate.Parameters.Add(new SqlParameter { ParameterName = "@Empno", SourceColumn = "Empno", SourceVersion = DataRowVersion.Original });



            SqlCommand cmdDelete = new SqlCommand();
            cmdDelete.Connection = cn;
            cmdDelete.CommandType = CommandType.Text;
            cmdDelete.CommandText = "delete from Employees where Empno=@Empno";

            cmdDelete.Parameters.Add(new SqlParameter { ParameterName = "@Empno", SourceColumn = "Empno", SourceVersion = DataRowVersion.Original});

            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = cn;
            cmdInsert.CommandType = CommandType.Text;
            cmdInsert.CommandText = "Insert into Employees values(@Empno,@Name,@Basic,@Deptno)";

            cmdInsert.Parameters.Add(new SqlParameter { ParameterName = "@Name", SourceColumn = "Name", SourceVersion = DataRowVersion.Current });
            cmdInsert.Parameters.Add(new SqlParameter { ParameterName = "@Deptno", SourceColumn = "Deptno", SourceVersion = DataRowVersion.Current });
            cmdInsert.Parameters.Add(new SqlParameter { ParameterName = "@Basic", SourceColumn = "Basic", SourceVersion = DataRowVersion.Current });
            cmdInsert.Parameters.Add(new SqlParameter { ParameterName = "@Empno", SourceColumn = "Empno", SourceVersion = DataRowVersion.Current });


            SqlCommand cmddinsert = new SqlCommand();
            cmddinsert.Connection = cn;
            cmddinsert.CommandType = CommandType.Text;
            cmddinsert.CommandText="Insert into Departments values( @Deptno,@DeptName)";
            cmddinsert.Parameters.Add(new SqlParameter { ParameterName = "@Deptno", SourceColumn = "Deptno", SourceVersion = DataRowVersion.Current });
            cmddinsert.Parameters.Add(new SqlParameter { ParameterName = "@DeptName", SourceColumn = "DeptName", SourceVersion = DataRowVersion.Current });



            SqlDataAdapter da = new SqlDataAdapter();
            
            da.UpdateCommand = cmdUpdate;
            da.DeleteCommand = cmdDelete;
            da.InsertCommand = cmdInsert;
            da.UpdateCommand = cmddinsert;

            da.Update(ds,"Emps");
            da.Update(ds, "Deps");

            cn.Close();
        }
    }
}
