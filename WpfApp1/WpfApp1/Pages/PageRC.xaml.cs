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
        string status = String.Empty;
        string city = String.Empty;

        public PageRC()
        {
            InitializeComponent();

            StatusBox.Items.Add("план");
            StatusBox.Items.Add("строительство");
            StatusBox.Items.Add("реализация");

            try
            {
                CityBox.ItemsSource = App1Entities.GetContext().
                    ResidentialComplex.Where(q => !q.IsDeleted).
                    Select(w => w.City).Distinct().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка подключения к БД", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StatusBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
            {
                status = StatusBox.SelectedItem.ToString();
                Filtration();
            }
        }

        private void CityBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
            {
                city = CityBox.SelectedItem.ToString();
                Filtration();
            }
        }

        private void BtnClearFilters_Click(object sender, RoutedEventArgs e)
        {
            status = string.Empty;
            city = string.Empty;
            StatusBox.SelectedItem = null;
            CityBox.SelectedItem = null;

            try
            {
                DGreedMain.ItemsSource = App1Entities.GetContext().
                    ResidentialComplex.Where(r => !r.IsDeleted).
                    OrderBy(q => q.Name).ThenBy(w => w.Status).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка подключения к БД", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Filtration()
        {
            try
            {
                if (status != string.Empty && city != string.Empty)
                {
                    DGreedMain.ItemsSource = App1Entities.GetContext().
                        ResidentialComplex.Where(q =>
                        q.Status == status &&
                        q.City == city &&
                        !q.IsDeleted).ToList();
                }
                if(status != string.Empty && city == string.Empty)
                {
                    DGreedMain.ItemsSource = App1Entities.GetContext().
                        ResidentialComplex.Where(q =>
                        q.Status == status &&
                        !q.IsDeleted).ToList();
                }
                if(status == string.Empty && city != string.Empty)
                {
                    DGreedMain.ItemsSource = App1Entities.GetContext().
                        ResidentialComplex.Where(q =>
                        q.City == city &&
                        !q.IsDeleted).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка подключения к БД", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            ManagerFrame.MainFrame.Navigate(new PageRCEdit(null));
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (DGreedMain.SelectedItem != null)
                ManagerFrame.MainFrame.Navigate(new PageRCEdit(
                    (ResidentialComplex)DGreedMain.SelectedItem));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                App1Entities.GetContext().ChangeTracker.Entries().ToList().
                    ForEach(q => q.Reload());
                DGreedMain.ItemsSource = App1Entities.GetContext().
                    ResidentialComplex.Where(r => !r.IsDeleted).
                    OrderBy(q => q.Name).ThenBy(w => w.Status).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка подключения к БД", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
