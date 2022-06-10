using System.Collections.Generic;

namespace AccountingPersonnelApp.Models
{
    class EmployeeList
    {
        private static IEnumerable<Employee> employees;

        public static IEnumerable<Employee> Employees { get => employees; set => employees = value; }
    }
}
