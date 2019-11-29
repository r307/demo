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
using WpfApptypedataset.DataSet1TableAdapters;

namespace WpfApptypedataset
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
        DataSet1 ds = new DataSet1();
        private void Btnfill_Click(object sender, RoutedEventArgs e)
        {
            DepartmentsTableAdapter dsDeps = new DepartmentsTableAdapter();
            dsDeps.Fill(ds.Departments);

            EmployeesTableAdapter dsEmps = new EmployeesTableAdapter();
            dsEmps.Fill(ds.Employees);

            dgemps.ItemsSource = ds.Employees;
            dgdeps.ItemsSource = ds.Departments;

        }

        private void btnupdate_Click(object sender, RoutedEventArgs e)
        {
            EmployeesTableAdapter dsEmps = new EmployeesTableAdapter();
            dsEmps.Update(ds.Employees);
            
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
