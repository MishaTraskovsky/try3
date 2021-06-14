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
        public PageRCEdit(ResidentialComplex _currentComplex)
        {
            InitializeComponent();

            currentComplex = _currentComplex;
            if(currentComplex != null)
            {
                TBoxName.Text = currentComplex.Name;
                TBoxCity.Text = currentComplex.City;
                TBoxBCost.Text = currentComplex.BuildingCost.ToString();
                TBoxComplexValue.Text = currentComplex.ComplexValueAdded.ToString();
                StatusBox.SelectedItem = currentComplex.Status.ToString();
            }

            StatusBox.Items.Add("план");
            StatusBox.Items.Add("строительство");
            StatusBox.Items.Add("реализация");
        }

        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (string.IsNullOrWhiteSpace(TBoxName.Text))
                stringBuilder.AppendLine("Некорректное название.");
            if (string.IsNullOrWhiteSpace(TBoxCity.Text))
                stringBuilder.AppendLine("Некорректный город.");
            if (!int.TryParse(TBoxBCost.Text, out int intCost) || intCost < 0)
                stringBuilder.AppendLine("Некорректная стоимость. Введите положительное число.");
            if (!int.TryParse(TBoxComplexValue.Text, out int intValue) || intValue < 0)
                stringBuilder.AppendLine("Некорректная добавочная стоимость. Введите положительное число.");
            if (StatusBox.SelectedItem == null)
                stringBuilder.AppendLine("Укажите статус.");

            if (stringBuilder.Length > 0)
            {
                MessageBox.Show(stringBuilder.ToString(), "Ошибка в заполнении", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (currentComplex != null)
            {
                currentComplex = new ResidentialComplex();
                currentComplex.Name = TBoxName.Text;
                currentComplex.City = TBoxCity.Text;
                currentComplex.Status = StatusBox.SelectedItem.ToString();
                currentComplex.BuildingCost = intCost;
                currentComplex.ComplexValueAdded = intValue;
                try
                {
                    App1Entities.GetContext().ResidentialComplex.Add(currentComplex);
                    App1Entities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно обновлены!", "Обновление данных", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Ошибка подключения к БД", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (currentComplex == null)
            {
                currentComplex = new ResidentialComplex();
                currentComplex.Name = TBoxName.Text;
                currentComplex.City = TBoxCity.Text;
                currentComplex.Status = StatusBox.SelectedItem.ToString();
                currentComplex.BuildingCost = intCost;
                currentComplex.ComplexValueAdded = intValue;
                currentComplex.IsDeleted = false;

                try
                {
                    App1Entities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно добавлены!", "Добавление данных", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Ошибка подключения к БД", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            ManagerFrame.MainFrame.Navigate(new PageRC());
        }

        private void BtnBackClick(object sender, RoutedEventArgs e)
        {
            ManagerFrame.MainFrame.GoBack();
        }
    }
}
