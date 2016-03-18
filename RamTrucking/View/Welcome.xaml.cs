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
using System.Windows.Shapes;
using Domain;
using Domain.Concrete;
using Domain.Entities;
using View.Controllers;

namespace View
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : Window
    {
        private JsonRepositoryFacturas repository;
        public Welcome()
        {
            InitializeComponent();                       
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            Empresa empresa = SingletonEmpresaSeleccionada.getEmpresaSeleccionada();
            if (empresa != null)
            {
                repository = new JsonRepositoryFacturas(empresa.UrlWebServiceFacturas);
                dgFacturas.ItemsSource = repository.Facturas;
                MessageBox.Show("Leyendo...");
            }
        }
    }
}
