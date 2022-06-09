using AccountingPersonnelApp.Commands;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AccountingPersonnelApp.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        ApplicationContext db;
        RelayCommand addEmployeeCommand;
        RelayCommand editEmployeeCommand;
        RelayCommand deleteEmployeeCommand;

        IEnumerable<Employee> employees;
        IEnumerable<Department> departments { get; set; }
        IEnumerable<Position> positions { get; set; }

        Employee selectedEmployee;
        string selectedDepartment;
        string selectedPosition;

        public MainWindowVM()
        {
            db = new ApplicationContext();

            db.Departments.LoadAsync();
            departments = db.Departments.Local.ToBindingList();

            db.Positions.LoadAsync();
            positions = db.Positions.Local.ToBindingList();

            db.Employees.Load();
            Employees = db.Employees.Local.ToBindingList(); 
        }

        #region fields
        public IEnumerable<Employee> Employees
        {
            get => employees; set
            {
                employees = value;
                OnPropertyChanged("Employees");
            }
        }

        public IEnumerable<string> Departments
        {
            get
            {
                var list =  departments?.Select(s => s.Title).ToList();
                list.Insert(0, "Выбрать все");
                return list;
            }
        }

        public IEnumerable<string> Positions
        {
            get
            {
                var list = positions?.Select(s => s.Title).ToList();
                list.Insert(0, "Выбрать все");
                return list;
            }
        }

        public Employee SelectedEmployee
        {
            get => selectedEmployee; set
            {
                if(value != null)
                    selectedEmployee = value;
                OnPropertyChanged("SelectedEmployee");
            }
        }

        public string SelectedDepartment
        {
            get => selectedDepartment; set
            {
                selectedDepartment = value;
                OnPropertyChanged("SelectedDepartment");
            }
        }

        public string SelectedPosition
        {
            get => selectedPosition; set
            {
                selectedPosition = value;
                OnPropertyChanged("SelectedPosition");
            }
        }
        #endregion

        #region Command
        public RelayCommand AddEmployeeCommand
        {
            get
            {
                return addEmployeeCommand ??
                  (addEmployeeCommand = new RelayCommand((o) =>
                  {
                      EmployeeWindow employeeWindow = new EmployeeWindow(new Employee());
                      if (employeeWindow.ShowDialog() == true)
                      {
                          Employee employee = employeeWindow.Employee;
                          db.Employees.Add(employee);
                          db.SaveChanges();
                      }
                  }));
            }
        }

        public RelayCommand EditEmployeeCommand
        {
            get
            {
                return editEmployeeCommand ??
                  (editEmployeeCommand = new RelayCommand((o) =>
                  {
                      if (SelectedEmployee == null) return;
                      EmployeeWindow employeeWindow = new EmployeeWindow(new Employee
                      {
                          IdEmployee = SelectedEmployee.IdEmployee,
                          FullName = SelectedEmployee.FullName,
                          DateOfBirth = SelectedEmployee.DateOfBirth,
                          Gender = SelectedEmployee.Gender,
                          IdPosition = SelectedEmployee.IdPosition,
                          IdDepartment = SelectedEmployee.IdDepartment
                      });

                      if (employeeWindow.ShowDialog() == true)
                      {
                          SelectedEmployee = db.Employees.Find(employeeWindow.Employee.IdEmployee);
                          if (SelectedEmployee != null)
                          {
                              SelectedEmployee.FullName = employeeWindow.Employee.FullName;
                              SelectedEmployee.DateOfBirth = employeeWindow.Employee.DateOfBirth;
                              SelectedEmployee.Gender = employeeWindow.Employee.Gender;
                              SelectedEmployee.IdPosition = employeeWindow.Employee.IdPosition;
                              SelectedEmployee.IdDepartment = employeeWindow.Employee.IdDepartment;
                              db.Entry(SelectedEmployee).State = EntityState.Modified;
                              db.SaveChanges();
                          }
                      }
                  }));
            }
        }

        public RelayCommand DeleteEmployeeCommand
        {
            get
            {
                return deleteEmployeeCommand ??
                  (deleteEmployeeCommand = new RelayCommand((o) =>
                  {
                      if (SelectedEmployee == null) return;
                      db.Employees.Remove(SelectedEmployee);
                      db.SaveChanges();
                  }));
            }
        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
