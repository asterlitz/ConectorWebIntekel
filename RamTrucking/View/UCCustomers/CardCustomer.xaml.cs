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

namespace View.UCCustomers
{
    /// <summary>
    /// Interaction logic for CardCustomer.xaml
    /// </summary>
    public partial class CardCustomer : UserControl
    {
        public CardCustomer(Customer customer)
        {
            InitializeComponent();
            this.DataContext = customer;
        }      
    }
}
