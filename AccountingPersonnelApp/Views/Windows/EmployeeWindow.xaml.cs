using AccountingPersonnelApp.ViewModels;
using System.Windows;

namespace AccountingPersonnelApp
{
    public partial class EmployeeWindow : Window
    {
        public EmployeeWindowVM EmployeeVM { get; private set; }

        public EmployeeWindow(Employee e, bool viewMode)
        {
            InitializeComponent();

            EmployeeVM = viewMode ? new EmployeeWindowViewingModeVM(e) : new EmployeeWindowVM(e);
            this.DataContext = EmployeeVM;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
