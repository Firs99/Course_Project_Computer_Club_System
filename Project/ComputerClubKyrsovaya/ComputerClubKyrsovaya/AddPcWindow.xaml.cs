using System.Windows;

namespace ComputerClubKyrsovaya
{
    /// <summary>
    /// Interaction logic for AddPcWindow.xaml
    /// </summary>
    public partial class AddPcWindow : Window
    {
        public AddPcWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ComputerClub db = new ComputerClub();
            PC pc = new PC();
            pc.Name = TextBoxName.Text;
            pc.StatusId = 1;
            db.PCs.Add(pc);
            db.SaveChanges();
            DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
