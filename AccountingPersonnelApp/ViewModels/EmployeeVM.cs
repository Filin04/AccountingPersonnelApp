using AccountingPersonnelApp.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AccountingPersonnelApp.ViewModels
{
    public class EmployeeVM : DependencyObject, INotifyPropertyChanged
    {
        private Employee employee;

        public EmployeeVM(Employee e)
        {
            employee = e;
        }

        public Employee Employee
        {
            get => employee; set
            {
                employee = value;
                OnPropertyChanged("Employee");
            }
        }

        public string FullName
        {
            get => Employee.FullName; set
            {
                Employee.FullName = value;
                OnPropertyChanged("FullName");
            }
        }

        public DateTime DateOfBirth
        {
            get => Employee.DateOfBirth;

            set
            {
                Employee.DateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }

        public bool gender
        {
            get => (int)Employee.Gender == 0 ? true  :false; 
            set
            {
                Employee.Gender = value == true ? Gender.Man: Gender.Women;
                OnPropertyChanged("Gender");
            }
        }

        public string GenderName
        {
            get => (int)Employee.Gender == 0 ? "Мужской" : "Женский";
        }

        public Position Position
        {
            get => PositionList.Positions.ToList().Find(s => s.IdPosition == Employee.IdPosition); 
            set
            {
                Employee.IdPosition = value.IdPosition;
                OnPropertyChanged("Position");
            }
        }

        public Department Department
        {
            get => DepartmentList.Departments.ToList().Find(s=>s.IdDepartment == Employee.IdDepartment); 
            set
            {
                Employee.IdDepartment = value.IdDepartment;
                OnPropertyChanged("Department");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
