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
using System.Windows.Shell;
using System.Windows.Threading;
using Domain.Entities;
using View.Controllers;

namespace View.UCCustomers
{
    /// <summary>
    /// Interaction logic for ListCustomers.xaml
    /// </summary>
    public partial class ListCustomers : UserControl
    {
        private ControllerCustomer controller;
        private TaskbarItemInfo taskbarCustomerInfo;
        private List<Customer> CustomerFromQB = null;
        private List<Customer> CustomerFromDataBase = null;

        public ListCustomers(TaskbarItemInfo taskbarCustomerInfo)
        {
            InitializeComponent();
            this.taskbarCustomerInfo = taskbarCustomerInfo;
            controller = new ControllerCustomer();
            Dispatcher.BeginInvoke(new Action(() => load()), DispatcherPriority.ContextIdle, null); 
            
        }
        private void load()
        {
            MessageBoxResult result = MessageBox.Show("¿Desea cargar los datos?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result.Equals(MessageBoxResult.Yes))
            {          
                showCustomersFromDatabase();
            }
        }         
        private void btnCheckDiferenceCustomer_Click(object sender, RoutedEventArgs e)
        {
            cargarListas();            
        }
        private async void cargarListas()
        {
            startProccess();
            await Task.Run(() =>
            {                
                CustomerFromQB = controller.loadCustomersFromQB().ToList();
                CustomerFromDataBase = controller.loadCustomersFromDatabase().ToList();                
            });
            endProccess();
            updateDatabase();
        }
        private void endProccess()
        {
            this.Cursor = Cursors.Arrow;
            this.taskbarCustomerInfo.ProgressState = TaskbarItemProgressState.None;
        }

        private void startProccess()
        {
            this.Cursor = Cursors.Wait;
            this.taskbarCustomerInfo.ProgressState = TaskbarItemProgressState.Normal;
        }
        private void updateDatabase()
        {
            if (CustomerFromQB.Count != CustomerFromDataBase.Count)
            {
                MessageBoxResult result = MessageBox.Show("Existen diferencias, ¿Desea Actualizar la base de datos?", "Intekel", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    saveCustomersInDatabase(CustomerFromQB);
                }
            }
            else
            {
                MessageBox.Show("Las bases de datos estan sincronizadas", "Intekel", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async void saveCustomersInDatabase(List<Customer> Customers)
        {
            startProccess();
            await Task.Run(() =>
            {
                int actual = 1;
                foreach (var Customer in Customers)
                {
                    controller.registerCustomerInDatabase(Customer);
                    actual = addCustomerToUI(actual, Customer);
                }
            });
            endProccess();
            MessageBox.Show("Customers: " + Customers.Count);
        }
        private async void showCustomersFromDatabase()
        {
            startProccess();
            await Task.Run(() =>
            {
                CustomerFromDataBase = controller.loadCustomersFromDatabase().ToList();
                int actual = 1;
                foreach (var Customer in CustomerFromDataBase)
                {
                    actual = addCustomerToUI(actual, Customer);
                }
            });
            endProccess();
            MessageBox.Show("Customers: " + CustomerFromDataBase.Count);
        }

        private int addCustomerToUI(int actual, Customer i)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                CardCustomer card = new CardCustomer(i);
                this.mainContent.Children.Add(card);
                taskbarCustomerInfo.ProgressValue = (double)actual++ / CustomerFromDataBase.Count;

            });
            System.Threading.Thread.Sleep(10);
            return actual;
        }
    }
}
