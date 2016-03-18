using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Domain.Concrete;
using Domain.Entities;
using View.Controllers;
using View.UCCustomers;
using View.UCItems;

namespace View.UCFacturas
{
    /// <summary>
    /// Interaction logic for ListFacturas.xaml
    /// </summary>
    public partial class ListFacturas : UserControl
    {
        private TaskbarItemInfo taskbarItemInfo;
        private ControllerFactura controller;
        private Factura facturaSeleccionada;
        public ListFacturas()
        {
            InitializeComponent();
            controller = new ControllerFactura();
        }

        public ListFacturas(TaskbarItemInfo taskbarItemInfo)
            : this()
        {
            this.taskbarItemInfo = taskbarItemInfo;
        }

        private async void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            startProccess();
            mainContent.Children.Clear();
            await Task.Run(() => cargarFacturas());            
            endProccess();
        }

        private void cargarFacturas()
        {
            try
            {                
                if (SingletonEmpresaSeleccionada.getEmpresaSeleccionada() != null)
                {
                    List<Factura> facturas = controller.cargarFacturas(SingletonEmpresaSeleccionada.getEmpresaSeleccionada().UrlWebServiceFacturas).ToList();
                    mostrarFaturas(facturas);
                }
            }
            catch (System.Net.WebException)
            {
                MessageBox.Show("Los datos no sepudieron cargar del web service", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void mostrarFaturas(List<Factura> facturas)
        {            
            for (int i = 0; i < facturas.Count;i++ )
            {
                double progress = (double)(i+1) / facturas.Count;

                App.Current.Dispatcher.Invoke(() =>
                {
                    facturas[i] = this.controller.facturaToInvoice(facturas[i]);
                    addFacturaToGUI(facturas[i], progress);
                });
                Thread.Sleep(17);
            }            
        }

        private void addFacturaToGUI(Factura factura, double progress)
        {
            UserControl uc = new CardFactura(factura);
            uc.MouseLeftButtonDown += uc_MouseLeftButtonDown;
            mainContent.Children.Add(uc);
            this.taskbarItemInfo.ProgressValue = progress;
        }

        private void uc_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CardFactura card = (CardFactura)sender;
            facturaSeleccionada = card.getFactura();
            this.screenMessage.Visibility = Visibility.Collapsed;
            gridDetailFactura.DataContext = facturaSeleccionada;
        }      
        private void startProccess()
        {            
            this.Cursor = Cursors.Wait;
            this.taskbarItemInfo.ProgressState = TaskbarItemProgressState.Indeterminate;
        }
        private void endProccess()
        {
            this.Cursor = Cursors.Arrow;
            this.taskbarItemInfo.ProgressState = TaskbarItemProgressState.None;
        }

        private void btnSearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            SelectCustomer viewSelect = new SelectCustomer();
            if(viewSelect.ShowDialog()== true)
            {
                this.facturaSeleccionada.Customer = viewSelect.CustomerSelect;
            }
        }

        private void btnChangeItem_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            ItemFactura item = (ItemFactura)btn.DataContext;
            SelectItem viewSelect = new SelectItem();
            if (viewSelect.ShowDialog() == true)
            {
                item.ItemQB = viewSelect.ItemSelect;
                item.ItemName = viewSelect.ItemSelect.Name;
            }
        }

        private void btnSaveFactura_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.controller.saveFactura(facturaSeleccionada);
                MessageBox.Show("Factura guardada con exito", "Intekel", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancelFactura_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.controller.cancelFactura(this.facturaSeleccionada.Invoice, this.facturaSeleccionada.Customer);
                MessageBox.Show("Cancelado con exito");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }
        private void btnDeleteFactura_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.controller.deleteFactura(this.facturaSeleccionada.Invoice);
                MessageBox.Show("Cancelado con exito");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }
    }
}



