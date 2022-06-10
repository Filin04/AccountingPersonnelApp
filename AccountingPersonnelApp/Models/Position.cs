using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace AccountingPersonnelApp
{
    public class Position : INotifyPropertyChanged
    {
        private int idPosition;
        private string title;

        [Key]
        public int IdPosition { get => idPosition; set => idPosition = value; }
        public string Title
        {
            get => title; set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
