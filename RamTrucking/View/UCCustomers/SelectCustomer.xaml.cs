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
using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;

namespace View.UCCustomers
{
    /// <summary>
    /// Interaction logic for SelectCustomer.xaml
    /// </summary>
    public partial class SelectCustomer : Window
    {
        private Controllers.ControllerCustomer controller;        
        public SelectCustomer()
        {
            InitializeComponent();
            controller = new Controllers.ControllerCustomer();
            loadFromDB();
        }
        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {            
            if (this.lbCustomers.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un elemento para continuar", "Intekel", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            this.CustomerSelect = (Customer)this.lbCustomers.SelectedItem; 
            this.DialogResult = true;                        
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;            
        }    

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string name = this.txtSearch.Text;
            this.lbCustomers.ItemsSource = this.controller.loadCustomersFromDatabase().Where(c => c.Name.ToLower().Contains(name.ToLower()));
        }        
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            loadFromDB();
            this.txtSearch.Text = "";
        }

        private void loadFromDB()
        {
            this.lbCustomers.ItemsSource = controller.loadCustomersFromDatabase().ToList();
        }
        public Customer CustomerSelect { get; set; }
        
    }
}
