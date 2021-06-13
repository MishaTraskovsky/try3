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
using WpfApp1.Data;
using WpfApp1;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageRC.xaml
    /// </summary>
    public partial class PageRC : Page
    {
        public PageRC()
        {
            InitializeComponent();

            try
            {
                var entities = App1Entities.GetContext().
                    ResidentialComplex.Where(r => !r.IsDeleted).
                    OrderBy(q => q.Name).ThenBy(w => w.Status).ToList();
                DGreedMain.ItemsSource = entities;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка подключения к БД", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            StatusBox.Items.Add("План");
            StatusBox.Items.Add("Строительство");
            StatusBox.Items.Add("Реализация");
        }

        private void StatusBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CityBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnClearFilters_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
