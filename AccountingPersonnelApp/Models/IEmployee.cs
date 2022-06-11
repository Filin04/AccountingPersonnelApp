using AccountingPersonnelApp.Commands;
using System.Collections.Generic;

namespace AccountingPersonnelApp.Models
{
    public interface IEmployee
    {
        Employee Employee { get; set; }
        RelayCommand AcceptCommand { get; }
        IEnumerable<Position> Positions { get; }
        IEnumerable<Department> Departments { get; }
        string[] Genders { get; }
    }
}
