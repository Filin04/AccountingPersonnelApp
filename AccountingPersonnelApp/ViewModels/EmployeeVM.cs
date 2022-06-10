using AccountingPersonnelApp.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using AccountingPersonnelApp.Commands;


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

        public string GenderName
        {
            get => (int)Employee.Gender == 0 ? "Мужской" : "Женский";
            set
            {
                Employee.Gender = value == "Мужской" ? Gender.Man : Gender.Women;
            }
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

        public string DopInfo
        {
            get
            {
                switch (Position.IdPosition)
                {
                    case 1: break;
                    case 2: break;
                    case 3: break;
                    case 4:
                        var headFullName = EmployeeList.Employees.ToList().Where(s=> s.IdDepartment == Department.IdDepartment && s.IdPosition == 2).ToList();

                        if(headFullName.Count == 1)
                        {
                            return $"Руководитель: {headFullName[0].FullName}";
                        }
                        else
                        {
                            string heads = "";
                            for (int i = 0; i < headFullName.Count; i++)
                            {
                                heads += headFullName[i].FullName;
                                heads += (i >= headFullName.Count-1) ? "" : ", ";
                            }
                            return $"Руководители: {heads}";
                        }
                    default:
                        goto case 4;
                }
                return "";
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
