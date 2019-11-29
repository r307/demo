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
using WpfAppdataset.DataSet1TableAdapters;

namespace WpfAppdataset
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        DataSet1 ds = new DataSet1();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DepartmentsTableAdapter daDeps = new DepartmentsTableAdapter();
            daDeps.Fill(ds.Departments);

            EmployeesTableAdapter daEmps = new EmployeesTableAdapter();
            daEmps.Fill(ds.Employees);

            dgemps.ItemsSource = ds.Employees;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EmployeesTableAdapter daEmps = new EmployeesTableAdapter();

            daEmps.Update(ds.Employees);

        }
    }
}
