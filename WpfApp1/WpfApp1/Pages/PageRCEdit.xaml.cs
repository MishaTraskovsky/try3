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
    /// Логика взаимодействия для PageRCEdit.xaml
    /// </summary>
    public partial class PageRCEdit : Page
    {
        ResidentialComplex currentComplex;

        //инициализация компонентов и построение элементов при разных условиях
        public PageRCEdit(ResidentialComplex _currentComplex)
        {
            InitializeComponent();

            currentComplex = _currentComplex;

            StatusBox.Items.Add("план");
            StatusBox.Items.Add("строительство");
            StatusBox.Items.Add("реализация");

            
            if(currentComplex != null)
            {
                try
                {
                    if (currentComplex.House.Count > 0)
                    {
                        var currentHouse = App1Entities.GetContext().House.
                            Where(q => q.ResidentialComplexID 
                            == currentComplex.ID).FirstOrDefault();

                        var currentAppart = App1Entities.GetContext()
                            .Apartment.Where(w => w.HouseID 
                            == currentHouse.ID).ToList();

                        if (currentAppart.Where(q => q.IsSold == true).
                            Count() > 0)
                            StatusBox.Items.Remove("план");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Ошибка подключения к БД",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }

                TBoxName.Text = currentComplex.Name;
                TBoxCity.Text = currentComplex.City;
                TBoxBCost.Text = currentComplex.BuildingCost.ToString();
                TBoxComplexValue.Text = currentComplex.ComplexValueAdded.
                    ToString();
                StatusBox.SelectedItem = currentComplex.Status.ToString();
            }
            else
            {
                currentComplex = new ResidentialComplex();
            }
            DataContext = currentComplex;
        }

        //обработка клика по кнопке "Сохранить"
        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {

            StringBuilder stringBuilder = new StringBuilder();
            if (string.IsNullOrWhiteSpace(currentComplex.Name))
                stringBuilder.AppendLine("Некорректное название.");
            if (string.IsNullOrWhiteSpace(currentComplex.City))
                stringBuilder.AppendLine("Некорректный город.");
            if (!int.TryParse(TBoxBCost.Text, out int intCost) || intCost < 0)
                stringBuilder.AppendLine(
                    "Некорректная стоимость. Введите положительное число.");
            if (!int.TryParse(TBoxComplexValue.Text, out int intValue) 
                || intValue < 0)
                stringBuilder.AppendLine("Некорректная добавочная " +
                    "стоимость. Введите положительное число.");
            if (StatusBox.SelectedItem == null)
                stringBuilder.AppendLine("Укажите статус.");

            if (stringBuilder.Length > 0)
            {
                MessageBox.Show(stringBuilder.ToString(), "Ошибка в заполнении"
                    , MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            currentComplex.Status = StatusBox.SelectedItem.ToString();

            try
            {
                if (currentComplex.ID == 0)
                    App1Entities.GetContext().ResidentialComplex.Add(
                        currentComplex);
                App1Entities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка подключения к БД", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            ManagerFrame.MainFrame.Navigate(new PageRC());
        }

        //Обработка клика по кнопке "Назад"
        private void BtnBackClick(object sender, RoutedEventArgs e)
        {
            ManagerFrame.MainFrame.GoBack();
        }
    }
}
