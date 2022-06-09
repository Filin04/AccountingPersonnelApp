using System.Windows;

namespace AccountingPersonnelApp
{
    public partial class EmployeeWindow : Window
    {
        public Employee Employee { get; private set; }

        public EmployeeWindow(Employee e)
        {
            InitializeComponent();
            Employee = e;
            this.DataContext = Employee;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
