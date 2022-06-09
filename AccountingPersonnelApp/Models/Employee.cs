using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace AccountingPersonnelApp
{
    public class Employee : INotifyPropertyChanged
    {
        private int idEmployee; 
        private string fullName;
        private string dateOfBirth;
        private int gender;
        private int idPosition;
        private int idDepartment;

        [Key]
        public int IdEmployee { get => idEmployee; set => idEmployee = value; }

        public string FullName
        {
            get => fullName; set
            {
                fullName = value;
                OnPropertyChanged("FullName");
            }
        }
        public DateTime DateOfBirth
        {
            get
            {
                DateTime.TryParse(dateOfBirth, out DateTime result);
                return result;
            }

            set
            {
                dateOfBirth = value.ToString("yyyy-MM-dd");
                OnPropertyChanged("DateOfBirth");
            }
        }
        public int Gender
        {
            get => gender; set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }
        public int IdPosition
        {
            get => idPosition; set
            {
                idPosition = value;
                OnPropertyChanged("IdPosition");
            }
        }
        public int IdDepartment
        {
            get => idDepartment; set
            {
                idDepartment = value;
                OnPropertyChanged("IdDepartment");
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
