using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Shell;
using FontAwesome.WPF;
using View.UCCustomers;
using View.UCFacturas;
using View.UCItems;
using View.UCMenu;

namespace View
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>    
    public partial class Dashboard : Window
    {
        private List<UCItem> ucItems = new List<UCItem>();
        private UCItem ucItemSelect = null;      
        public Dashboard()
        {
            InitializeComponent();            
            createMenu();            
        }              
        private void createMenu()
        {
            addItem("Facturas", FontAwesomeIcon.StickyNote, "");
            addItem("F. Guardadas", FontAwesomeIcon.Database, "");
            addItem("Cancelar Facturas", FontAwesomeIcon.TimesCircle, "");
            addItem("Customers", FontAwesomeIcon.Users, "");
            addItem("Items", FontAwesomeIcon.Tags, "");            
        }
        private void addItem(string name, FontAwesomeIcon ico, string badges)
        {
            UCItem item = new UCItem(name, ico, badges);
            item.MouseDown+=item_MouseDown;
            item.Tag = ucItems.Count + 1;
            ucItems.Add(item);
            this.menu.Children.Add(item);
        }

        private void item_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UCItem item = (UCItem)sender;

            if (this.ucItemSelect != null)
            {
                ucItemSelect.unselect();
            }
            this.ucItemSelect = item;
            this.ucItemSelect.select();

            showUC(item);
        }
        private void showUC(UCItem item)
        {
            contentPanel.Children.Clear();
            if ((int)item.Tag == 1)
                contentPanel.Children.Add(new ListFacturas(this.TaskbarItemInfo));
            if ((int)item.Tag == 2)
                MessageBox.Show("No implementado");
            if ((int)item.Tag == 3)
                MessageBox.Show("No implementado");
            if ((int)item.Tag == 4)
                contentPanel.Children.Add(new ListCustomers(this.TaskbarItemInfo));
            if ((int)item.Tag == 5)
                contentPanel.Children.Add(new ListItems(this.TaskbarItemInfo));
        }
    }
}
