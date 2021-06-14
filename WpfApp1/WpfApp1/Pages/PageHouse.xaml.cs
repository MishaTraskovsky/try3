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
    /// Логика взаимодействия для PageHouse.xaml
    /// </summary>
    public partial class PageHouse : Page
    {
        public PageHouse()
        {
            InitializeComponent();

            //int countOfSoldApart = App1Entities.GetContext().Apartment
            DGreedHouse.ItemsSource = App1Entities.GetContext().House.Where(q => !q.IsDeleted).ToList();

            var HouseList = App1Entities.GetContext().House.Where(q => !q.IsDeleted).ToList();
            int notSoldApart;
            int soldApart;
            for (int i = 0; i < HouseList.Count; i++)
            {
                var apart = App1Entities.GetContext().Apartment.Where(q => q.HouseID == HouseList[i].ID).Count();
                soldApart = App1Entities.GetContext().Apartment.Where(q => q.HouseID == HouseList[i].ID && q.IsSold).Count();
                notSoldApart = apart - soldApart;
                
            }
        }
    }
}
