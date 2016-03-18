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
    /// Interaction logic for CardItem.xaml
    /// </summary>        
    public partial class CardItem : UserControl
    {        
        public CardItem(ItemQB item)
        {
            InitializeComponent();
            this.DataContext = item;
        }
    }
}
