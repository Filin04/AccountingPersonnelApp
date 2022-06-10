using System.Windows;

namespace AccountingPersonnelApp.ViewModels
{
    public class EmployeeWindowViewingModeVM : EmployeeWindowVM
    {
        public EmployeeWindowViewingModeVM(Employee e) : base(e)
        {
            MessageBox.Show("Режим просмотра");
        }
    }
}
