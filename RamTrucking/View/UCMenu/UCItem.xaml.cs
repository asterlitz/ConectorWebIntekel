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
using FontAwesome.WPF;

namespace View.UCMenu
{
    /// <summary>
    /// Interaction logic for Item.xaml
    /// </summary>
    public partial class UCItem : UserControl
    {       
        public UCItem(string textName, FontAwesomeIcon icon, string textBadges)
        {
            InitializeComponent();
            this.IsSelect = false;
            this.txtName.Text = textName;
            this.txtBadges.Text = textBadges;
            this.icon.Icon = icon;
        }
        public void select()
        {
            this.itemPanel.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#71AFE8"));
            this.txtName.Foreground = Brushes.White;
            this.icon.Foreground = Brushes.White;
            this.IsSelect = true;
        }
        public void unselect()
        {
            this.itemPanel.Background = Brushes.Transparent;
            this.txtName.Foreground = Brushes.Black;
            this.icon.Foreground = Brushes.Black;
            this.IsSelect = false;
        }

        private void itemPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            this.itemPanel.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#71AFE8"));       
        }

        private void itemPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsSelect)
            {
                unselect();
            }
        }
        public bool IsSelect { get; set; }
    }
}
