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

namespace View.UCItems
{
    /// <summary>
    /// Interaction logic for ListItems.xaml
    /// </summary>
    public partial class ListItems : UserControl
    {
        private ControllerItem controller;
        private TaskbarItemInfo taskbarItemInfo;
        private List<ItemQB> itemFromQB = null;
        private List<ItemQB> itemFromDataBase = null;

        public ListItems(TaskbarItemInfo taskbarItemInfo)            
        {            
            InitializeComponent();
            this.taskbarItemInfo = taskbarItemInfo;
            Dispatcher.BeginInvoke(new Action(() => load()), DispatcherPriority.ContextIdle, null);
        }

        private void load()
        {
            MessageBoxResult result = MessageBox.Show("¿Desea cargar los datos?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result.Equals(MessageBoxResult.Yes))
            {
                controller = new ControllerItem();
                showItemsFromDatabase();
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
                itemFromQB = controller.loadItemsFromQB().ToList();
                itemFromDataBase = controller.loadItemsFromDatabase().ToList();
            });
            endProccess();
            updateDatabase();
        }
        private void endProccess()
        {
            this.Cursor = Cursors.Arrow;
            this.taskbarItemInfo.ProgressState = TaskbarItemProgressState.None;
        }

        private void startProccess()
        {
            this.Cursor = Cursors.Wait;
            this.taskbarItemInfo.ProgressState = TaskbarItemProgressState.Normal;
        }
        private void updateDatabase()
        {
            if (itemFromQB.Count != itemFromDataBase.Count)
            {
                MessageBoxResult result = MessageBox.Show("Existen diferencias, ¿Desea Actualizar la base de datos?", "Intekel", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    saveItemsInDatabase(itemFromQB);
                }
            }
            else
            {
                MessageBox.Show("Las bases de datos estan sincronizadas", "Intekel", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }      

        private async void saveItemsInDatabase(List<ItemQB> items)       
        {
            startProccess();
            await Task.Run(() =>
            {
                int actual = 1;
                foreach (var item in items)
                {                    
                    controller.registerItemInDatabase(item);
                    actual = addItemToUI(actual, item);
                }
            });
            endProccess();
            MessageBox.Show("items: " + items.Count);
        }
        private async void showItemsFromDatabase()
        {
            startProccess();
            await Task.Run(() =>
            {                
                itemFromDataBase= controller.loadItemsFromDatabase().ToList();
                int actual = 1;
                foreach (var item in itemFromDataBase)
                {
                    actual = addItemToUI(actual, item);
                }                
            });
            endProccess();
            MessageBox.Show("items: " + itemFromDataBase.Count);
        }

        private int addItemToUI(int actual, ItemQB i)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                CardItem card = new CardItem(i);
                this.mainContent.Children.Add(card);
                taskbarItemInfo.ProgressValue = (double)actual++ / itemFromDataBase.Count;

            });
            System.Threading.Thread.Sleep(10);
            return actual;
        }
    }
}
