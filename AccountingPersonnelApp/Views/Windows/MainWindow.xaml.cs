using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace AccountingPersonnelApp
{
    public partial class MainWindow : Window
    {
        ApplicationContext db;
        public MainWindow()
        {
            InitializeComponent();

            db = new ApplicationContext();
            db.Employees.Load();
            this.DataContext = db.Employees.Local.ToBindingList();
        }

        // добавление
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            EmployeeWindow employeeWindow = new EmployeeWindow(new Employee());
            if (employeeWindow.ShowDialog() == true)
            {
                Employee employee = employeeWindow.Employee;
                db.Employees.Add(employee);
                db.SaveChanges();
            }
        }
        // редактирование
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            // если ни одного объекта не выделено, выходим
            if (employeeList.SelectedItem == null) return;
            // получаем выделенный объект
            Employee employee = employeeList.SelectedItem as Employee;

            EmployeeWindow employeeWindow = new EmployeeWindow(new Employee
            {
                IdEmployee = employee.IdEmployee,
                FullName = employee.FullName,
                DateOfBirth = employee.DateOfBirth,
                Gender = employee.Gender,
                IdPosition = employee.IdPosition,
                IdDepartment = employee.IdDepartment
            });

            if (employeeWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                employee = db.Employees.Find(employeeWindow.Employee.IdEmployee);
                if (employee != null)
                {
                    employee.FullName = employeeWindow.Employee.FullName;
                    employee.DateOfBirth = employeeWindow.Employee.DateOfBirth;
                    employee.Gender = employeeWindow.Employee.Gender;
                    employee.IdPosition = employeeWindow.Employee.IdPosition;
                    employee.IdDepartment = employeeWindow.Employee.IdDepartment;
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
        // удаление
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // если ни одного объекта не выделено, выходим
            if (employeeList.SelectedItem == null) return;
            // получаем выделенный объект
            Employee employee = employeeList.SelectedItem as Employee;
            db.Employees.Remove(employee);
            db.SaveChanges();
        }
    }
}
