using AccountingPersonnelApp.Commands;
using AccountingPersonnelApp.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;

namespace AccountingPersonnelApp.ViewModels
{
    public class MainWindowVM : DependencyObject, INotifyPropertyChanged
    {
        ApplicationContext db;
        RelayCommand addEmployeeCommand;
        RelayCommand editEmployeeCommand;
        RelayCommand deleteEmployeeCommand;

        IEnumerable<EmployeeVM> employees { get;}
        IEnumerable<Department> departments { get; set; }
        IEnumerable<Position> positions { get; set; }

        EmployeeVM selectedEmployee;

        public MainWindowVM()
        {
            db = new ApplicationContext();

            db.Departments.LoadAsync();
            departments = DepartmentList.Departments = db.Departments.Local.ToBindingList();

            db.Positions.LoadAsync();
            positions = PositionList.Positions = db.Positions.Local.ToBindingList();

            db.Employees.Load();
            employees = db.Employees.Local.ToBindingList().Select(s=>new EmployeeVM(s));

            Employees = CollectionViewSource.GetDefaultView(employees);
            Employees.Filter = FilterEmployees;
        }

        #region fields

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

        public EmployeeVM SelectedEmployee
        {
            get => selectedEmployee; 
            set
            {
                if(value != null)
                {
                    selectedEmployee = value;
                }
                OnPropertyChanged("SelectedEmployee");
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
                          Employee employee = employeeWindow.EmployeeVM.Employee;
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
                      if (SelectedEmployee == null)
                      {
                          return;
                      }
                      EmployeeWindow employeeWindow = new EmployeeWindow(new Employee
                      { 
                                IdEmployee = SelectedEmployee.Employee.IdEmployee,
                                FullName = SelectedEmployee.Employee.FullName,
                                DateOfBirth = SelectedEmployee.Employee.DateOfBirth,
                                Gender = SelectedEmployee.Employee.Gender,
                                IdPosition = SelectedEmployee.Employee.IdPosition,
                                IdDepartment = SelectedEmployee.Employee.IdDepartment
                      });
                      if (employeeWindow.ShowDialog() == true)
                      {
                          SelectedEmployee.Employee = db.Employees.Find(employeeWindow.EmployeeVM.Employee.IdEmployee);
                          if (SelectedEmployee != null)
                          {
                              SelectedEmployee.Employee.FullName = employeeWindow.EmployeeVM.Employee.FullName;
                              SelectedEmployee.Employee.DateOfBirth = employeeWindow.EmployeeVM.Employee.DateOfBirth;
                              SelectedEmployee.Employee.Gender = employeeWindow.EmployeeVM.Employee.Gender;
                              SelectedEmployee.Employee.IdPosition = employeeWindow.EmployeeVM.Employee.IdPosition;
                              SelectedEmployee.Employee.IdDepartment = employeeWindow.EmployeeVM.Employee.IdDepartment;
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
                      db.Employees.Remove(SelectedEmployee.Employee);
                      db.SaveChanges();
                  }));
            }
        }
        #endregion


        #region Filter
        public string FilterEmployee
        {
            get { return (string)GetValue(FilterEmployeeProperty); }
            set { SetValue(FilterEmployeeProperty, value); }
        }

        public static readonly DependencyProperty FilterEmployeeProperty =
            DependencyProperty.Register("FilterEmployee", typeof(string), typeof(MainWindowVM), new PropertyMetadata("", Filter_Changed));

        private static void Filter_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as MainWindowVM;
            if (current != null)
            {
                current.Employees.Filter = null;
                current.Employees.Filter = current.FilterEmployees;
            }
        }

        public ICollectionView Employees
        {
            get { return (ICollectionView)GetValue(EmployeesProperty); }
            set { SetValue(EmployeesProperty, value); }
        }

        public static readonly DependencyProperty EmployeesProperty =
            DependencyProperty.Register("Employees", typeof(ICollectionView), typeof(EmployeeVM), new PropertyMetadata(null));

        private bool FilterEmployees(object obj)
        {
            bool result = true;
            EmployeeVM current = obj as EmployeeVM;
            if (FilterEmployee != "Выбрать все" && !string.IsNullOrWhiteSpace(FilterEmployee) && current != null && current.Position.ToString() != FilterEmployee && current.Department.ToString() != FilterEmployee)
            {
                result = false;
            }
            return result;
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
