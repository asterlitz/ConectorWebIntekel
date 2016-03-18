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

namespace View.UCFacturas
{
    /// <summary>
    /// Interaction logic for Factura.xaml
    /// </summary>
    public partial class CardFactura : UserControl
    {
        private Factura factura;
        public CardFactura(Factura factura)
        {
            InitializeComponent();
            this.factura = factura;
            this.DataContext = factura;
            if (factura.Status.Equals(EstatusFactura.Guardada))
            {
                this.icon.Icon = FontAwesome.WPF.FontAwesomeIcon.Check;
                this.icon.Foreground = Brushes.Green;
            }
            if (factura.Status.Equals(EstatusFactura.Cancelada))
            {
                this.icon.Icon = FontAwesome.WPF.FontAwesomeIcon.Times;
                this.icon.Foreground = Brushes.Red;
            }
            if (factura.Status.Equals(EstatusFactura.SinEstatus))
            {
                this.icon.Icon = FontAwesome.WPF.FontAwesomeIcon.Exclamation;
                this.icon.Foreground = Brushes.Black;
            }
        }

        public Factura getFactura()
        {
            return factura;
        }
    }
}
