using AccountingPersonnelApp.Commands;
using AccountingPersonnelApp.Models;
using System.Collections.Generic;
using System.Windows;

namespace AccountingPersonnelApp.ViewModels
{
    public class EmployeeWindowVM : EmployeeVM, IEmployee
    {
        public EmployeeWindowVM(Employee e) : base(e)
        {

        }

        RelayCommand acceptCommand;
        public RelayCommand AcceptCommand
        {
            get
            {
                return acceptCommand ??
                  (acceptCommand = new RelayCommand((o) =>
                  {
                      if(o is Window window)
                      {
                          window.DialogResult = true;
                      }
                  }));
            }
        }

        public IEnumerable<Position> Positions
        {
            get => PositionList.Positions;
        }

        public IEnumerable<Department> Departments
        {
            get => DepartmentList.Departments;
        }

        public string[] Genders{ get; } = { "Мужской", "Женский" };
    }
}
