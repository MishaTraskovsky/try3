using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1;
using WpfApp1.Data;
namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageWelcome.xaml
    /// </summary>
    public partial class PageWelcome : Page
    {
        public PageWelcome()
        {
            InitializeComponent();
        }

        private void BtnHouseListClick(object sender, RoutedEventArgs e)
        {
            ManagerFrame.MainFrame.Navigate(new PageHouse());
        }

        private void BtnRCListClick(object sender, RoutedEventArgs e)
        {
            ManagerFrame.MainFrame.Navigate(new PageRC());
        }
    }
}
