using ComputerClubKyrsovaya.Helpers;
namespace ComputerClubKyrsovaya.Model
{
    public class Computers :ObservebledObject  //Класс для работы с Компьютерами
    {


        public double selectedTime;
        public double SelectedTime
        {
            get { return selectedTime; }
            set
            {
                if (selectedTime != value)
                {
                    selectedTime = value;
                    OnPropertyChanged("SelectedTime");
                }
            }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private int statusId;

        public int StatusId
        {
            get { return statusId; }
            set
            {
                if (statusId != value)
                {
                    statusId = value;
                    OnPropertyChanged("StatusId");
                }
            }
        }
    }
}
