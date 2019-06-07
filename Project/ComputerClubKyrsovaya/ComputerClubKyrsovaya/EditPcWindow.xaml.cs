using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ComputerClubKyrsovaya
{
    /// <summary>
    /// Interaction logic for EditPcWindow.xaml
    /// </summary>
    public partial class EditPcWindow : Window
    {
        PC pc = null;
        public EditPcWindow(ref PC pc)
        {
            InitializeComponent();
            this.pc = pc;
            TextBoxName.Text = pc.Name;
            StatusComboBox.Items.Add("Ожидает");
            StatusComboBox.Items.Add("Работает");
            StatusComboBox.Items.Add("В Ремонт");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ComputerClub db = new ComputerClub();
            pc.Name = TextBoxName.Text;
            if(StatusComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Укажите Статус под именем.");
            }
            else if (StatusComboBox.SelectedIndex == 0)
            {
                pc.StatusId = 1;
            }
            else if (StatusComboBox.SelectedIndex == 1)
            {
                pc.StatusId = 2;
            }
            else if (StatusComboBox.SelectedIndex == 2)
            {
                pc.StatusId = 3;
            }
            else
                MessageBox.Show("Укажите Статус под именем.");

            db.SaveChanges();

            DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
