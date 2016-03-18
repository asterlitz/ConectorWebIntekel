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
using Domain.Entities;

namespace View.UCItems
{
    /// <summary>
    /// Interaction logic for SelectItem.xaml
    /// </summary>
    public partial class SelectItem : Window
    {
        private Controllers.ControllerItem controller;
        public SelectItem()
        {
            InitializeComponent();
            controller = new Controllers.ControllerItem();
            loadFromDB();
        }      
        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (this.lbItems.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un elemento para continuar", "Intekel", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            this.ItemSelect = (ItemQB)this.lbItems.SelectedItem;
            this.DialogResult = true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            this.lbItems.ItemsSource = filterByName(this.txtSearch.Text).ToList();
        }
        public IEnumerable<ItemQB> filterByName(string name)
        {
            return this.controller.loadItemsFromDatabase().Where(i => i.Name.ToLower().Contains(name.ToLower()));
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            loadFromDB();
            this.txtSearch.Text = "";
        }
        private void loadFromDB()
        {
            this.lbItems.ItemsSource = controller.loadItemsFromDatabase().ToList();
        }
        public ItemQB ItemSelect { get; set; }
    }
}
