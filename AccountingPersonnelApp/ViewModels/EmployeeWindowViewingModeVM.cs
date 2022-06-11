using AccountingPersonnelApp.Commands;
using AccountingPersonnelApp.Models;
using System.Collections.Generic;
using System.Windows;

namespace AccountingPersonnelApp.ViewModels
{
    public class EmployeeWindowViewingModeVM : EmployeeVM, IEmployee
    {
        public EmployeeWindowViewingModeVM(Employee e) : base(e)
        {

        }

        public IEnumerable<Position> Positions
        {
            get => PositionList.Positions;
        }

        public IEnumerable<Department> Departments
        {
            get => DepartmentList.Departments;
        }

        public string[] Genders { get; } = { "Мужской", "Женский" };

        RelayCommand acceptCommand;
        public RelayCommand AcceptCommand
        {
            get
            {
                return acceptCommand ??
                  (acceptCommand = new RelayCommand((o) =>
                  {
                      if (o is Window window)
                      {
                          window.DialogResult = true;
                      }
                  }));
            }
        }

        RelayCommand viewEmployeeCommand;
        public RelayCommand ViewEmployeeCommand
        {
            get
            {
                return viewEmployeeCommand ??
                  (viewEmployeeCommand = new RelayCommand((o) =>
                  {
                      if (Employee == null)
                      {
                          return;
                      }

                      if(Employee.IdPosition > 1)
                      {
                          Employee.IdPosition --;
                          OnPropertyChanged("Position");
                      }
                  }));
            }
        }
    }
}
