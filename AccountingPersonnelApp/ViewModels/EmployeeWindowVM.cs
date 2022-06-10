using AccountingPersonnelApp.Models;
using System.Collections.Generic;

namespace AccountingPersonnelApp.ViewModels
{
    public class EmployeeWindowVM : EmployeeVM
    {
        public EmployeeWindowVM(Employee e) : base(e)
        {
            Employee = e;
        }

        public IEnumerable<Position> Positions
        {
            get => PositionList.Positions;
        }

        public IEnumerable<Department> Departments
        {
            get => DepartmentList.Departments;
        }
    }
}
