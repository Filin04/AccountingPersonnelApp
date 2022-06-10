using System;
using System.Collections.Generic;

namespace AccountingPersonnelApp.Models
{
    public static class DepartmentList
    {
        private static IEnumerable<Department> departments;

        public static IEnumerable<Department> Departments { get => departments; set => departments = value; }
    }
}
