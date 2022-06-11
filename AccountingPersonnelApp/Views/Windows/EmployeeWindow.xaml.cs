using AccountingPersonnelApp.Models;
using AccountingPersonnelApp.ViewModels;
using System.Windows;

namespace AccountingPersonnelApp
{
    public partial class EmployeeWindow : Window
    {
        public IEmployee EmployeeVM { get; private set; }

        public EmployeeWindow(Employee e, bool viewMode)
        {
            InitializeComponent();

            if (viewMode)
            {
                EmployeeVM = new EmployeeWindowViewingModeVM(e) ;
            }
            else
            {
                EmployeeVM = new EmployeeWindowVM(e);
            }

            this.DataContext = EmployeeVM;
        }
    }
}
