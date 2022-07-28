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

namespace DevAddIns
{
    /// <summary>
    /// Interaction logic for UserControl55.xaml
    /// </summary>
    public partial class UserControl55 : UserControl
    {
        public UserControl55()
        {
            InitializeComponent();
        }

        private void ribbon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
               
        }

        private void ribbonButton_Click(object sender, RoutedEventArgs e)
        {
            if(ribbon.SelectedItem != ribbonButton)
            {
                ribbon.SelectedItem = ribbonButton;
            }
            else
            {

            }
        }

        private void ribbonButton1_Click(object sender, RoutedEventArgs e)
        {
            if (ribbon.SelectedItem != ribbonButton1)
            {
                ribbon.SelectedItem = ribbonButton1;
            }
            else
            {

            }
        }
    }
}
