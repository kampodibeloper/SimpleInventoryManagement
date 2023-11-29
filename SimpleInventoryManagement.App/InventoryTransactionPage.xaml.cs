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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleInventoryManagement.App
{
    /// <summary>
    /// Interaction logic for InventoryTransactionPage.xaml
    /// </summary>
    public partial class InventoryTransactionPage : Page
    {
        private readonly MyInventoryManagementDataContext dataContext;
        private ObservableCollection<Inventory> inventoryItems;
        public Inventory SelectedInventoryItem { get; set; }
        public InventoryTransactionPage()
        {
            InitializeComponent();

            dataContext = new MyInventoryManagementDataContext();
            inventoryItems = new ObservableCollection<Inventory>(dataContext.Inventories);
            InventoryListView.ItemsSource = inventoryItems;

            InventoryListView.SelectionChanged += InventoryListView_SelectionChanged;
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                Inventory newItem = new Inventory
                {
                    ItemName = ItemNameTextBox.Text,
                    Quantity = int.Parse(QuantityTextBox.Text),
                    Price = decimal.Parse(PriceTextBox.Text),
                    Manufacturer = ManufacturerTextBox.Text,
                    Description = DescriptionTextBox.Text
                };

                // Add to the database
                dataContext.Inventories.InsertOnSubmit(newItem);
                dataContext.SubmitChanges();

                // Refresh the ObservableCollection
                inventoryItems.Add(newItem);

                ClearInputFields();
            }
        }

        private void UpdateItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput() && SelectedInventoryItem != null)
            {
                SelectedInventoryItem.ItemName = ItemNameTextBox.Text;
                SelectedInventoryItem.Quantity = int.Parse(QuantityTextBox.Text);
                SelectedInventoryItem.Price = decimal.Parse(PriceTextBox.Text);
                SelectedInventoryItem.Manufacturer = ManufacturerTextBox.Text;
                SelectedInventoryItem.Description = DescriptionTextBox.Text;

                dataContext.SubmitChanges();

                inventoryItems = new ObservableCollection<Inventory>(dataContext.Inventories);
                InventoryListView.ItemsSource = inventoryItems;

                ClearInputFields();
            }
        }

        private bool ValidateInput()
        {
            return !string.IsNullOrWhiteSpace(ItemNameTextBox.Text)
                   && int.TryParse(QuantityTextBox.Text, out _)
                   && decimal.TryParse(PriceTextBox.Text, out _);
        }

        private void ClearInputFields()
        {
            ItemNameTextBox.Text = string.Empty;
            QuantityTextBox.Text = string.Empty;
            PriceTextBox.Text = string.Empty;
            ManufacturerTextBox.Text = string.Empty;
            DescriptionTextBox.Text = string.Empty;
        }

        private void InventoryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedInventoryItem = (Inventory)InventoryListView.SelectedItem;
        }

        private void InventoryListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedInventoryItem != null)
            {
                ItemNameTextBox.Text = SelectedInventoryItem.ItemName;
                QuantityTextBox.Text = SelectedInventoryItem.Quantity.ToString();
                PriceTextBox.Text = SelectedInventoryItem.Price.ToString();
                ManufacturerTextBox.Text = SelectedInventoryItem.Manufacturer;
                DescriptionTextBox.Text = SelectedInventoryItem.Description;
            }
        }

        private void FilterItems(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                InventoryListView.ItemsSource = inventoryItems;
            }
            else
            {
                var filteredItems = inventoryItems
                    .Where(item =>
                        item.ItemName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        item.Manufacturer.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        item.Description.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                InventoryListView.ItemsSource = filteredItems;
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterItems(SearchTextBox.Text);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            FilterItems(SearchTextBox.Text);
        }
    }
}
