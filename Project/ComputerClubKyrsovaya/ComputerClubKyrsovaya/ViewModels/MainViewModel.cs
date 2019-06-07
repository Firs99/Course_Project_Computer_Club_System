using System;
using System.Linq;
using ComputerClubKyrsovaya.Helpers;
using ComputerClubKyrsovaya.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Data.Entity;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Data.Entity.Migrations;
using System.Windows.Threading;

namespace ComputerClubKyrsovaya
{
    public class MainViewModel :ObservebledObject //Основное представление при помощи MVVM
    {
        ComputerClub db = null;
        DispatcherTimer timer;

        public ObservableCollection<PC> pCs { set; get; } = new ObservableCollection<PC>();
        public ObservableCollection<double> Times { set; get; } = new ObservableCollection<double>();

        public double setTime;
        public double SetTime
        {
            get { return setTime; }
            set
            {
                if (setTime != value)
                {
                    setTime = value;
                    OnPropertyChanged("SetTime");
                }
            }
           
        }

        public PC selectedComputer;
        public PC SelectedComputer
        {
            get {return selectedComputer; }
            set
            {
                if(selectedComputer != value)
                {
                    selectedComputer = value;
                    OnPropertyChanged("SelectedComputer");
                }
            }
        }
       
        public MainViewModel()
        {
            try
            {
                db = new ComputerClub();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Times.Add(30);
            Times.Add(60);
            Times.Add(90);
            Times.Add(120);
        }
        
        public ICollectionView PCs
        {
            get
            {
                db.PCs.Load();
                return CollectionViewSource.GetDefaultView(db.PCs.Local); //Загрузка коллекции Компьютеров с БД в Список и его представление
            }
        }
        RelayCommand setSelectedTime;
        RelayCommand addPcCommand;
        RelayCommand editPcCommand;
        RelayCommand removePcCommand;

        RelayCommand setOrderPcCommand;

        public RelayCommand SetSelectedTime
        {
            get
            {
                return setSelectedTime ?? (setSelectedTime = new RelayCommand(
                    (obj) =>
                    {
                        PC pc =(PC)obj;
                        MessageBox.Show($"pc.SelectedTime");
                    }
                    ));
            }
        }
        public RelayCommand EditPcCommand
        {
            get
            {
                return editPcCommand ?? (editPcCommand = new RelayCommand(
                    (obj) =>
                    {
                        PC Pc = db.PCs.Where(p => p.Id == SelectedComputer.Id).FirstOrDefault();
                        EditPcWindow window = new EditPcWindow(ref Pc);
                        if (window.ShowDialog() == true)
                        {
                            db.SaveChanges();
                            PCs.Refresh();
                        }
                    }
                    ));
            }
        }
        public RelayCommand SetOrderPcCommand
        {
            get
            {
                return setOrderPcCommand ?? (setOrderPcCommand = new RelayCommand(
                    (obj) =>
                    {
                        Order order = new Order();
                        PC pc = db.PCs.Where(p => p.Id == SelectedComputer.Id).FirstOrDefault();
                        order.NamePC = SelectedComputer.Name;
                        order.DateStart = DateTime.Now;
                        //DateTime End = DateTime.Now.AddMinutes(Convert.ToDouble(pc.SelectedTime.Value));
                        order.DateEnd = DateTime.Now;
                        
                        pc.StatusId = 2; 
                        db.Orders.Add(order);
                        db.SaveChanges();
                        PCs.Refresh();
                    }
                    ));
            }
        }

        
        public RelayCommand RemovePcCommand
        {
            get
            {
                return removePcCommand ?? (removePcCommand = new RelayCommand(
              (obj) =>
              {
                  db.PCs.Remove(selectedComputer);
                  db.SaveChanges();
              },
              (obj) =>
              {
                  return db.PCs.Count() > 0;
              }
              ));
            }
        }
        public RelayCommand AddPcCommand
        {
            get
            {
                return addPcCommand ?? (addPcCommand = new RelayCommand(
                    (obj) =>
                    {
                        AddPcWindow window = new AddPcWindow();
                        if (window.ShowDialog() == true)
                        {
                            db.PCs.Load();
                        }
                    }
                    ));
            }
        }
        private int selectedIndex;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }
    }

    public class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = null;
            if ((int)value == 1)
            {
                str = "Ожидает"; 
            }
            if ((int)value == 2)
            {
                str = "В работе";
            }
            if ((int)value == 3)
            {
                str = "Требует ремонта";
            }
            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ImageConverter :Computers, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource source = null;
            Uri uri = null;
            {
                if((int)value == 1)
                {
                    uri = new Uri(@"Resources/enable_server.png", UriKind.Relative);
                }
                if ((int)value == 2)
                {
                    uri = new Uri(@"Resources/upload_server.png", UriKind.Relative);
                }
                if ((int)value == 3)
                {
                    uri= new Uri( @"Resources/desable_server.png", UriKind.Relative);
                }
            }
            source = new BitmapImage(uri);
            return source; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
