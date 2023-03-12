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

namespace BugDashboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void B_Review_Checked(object sender, RoutedEventArgs e)
        {
            ShowPage(G_Review);
        }

        private void B_Home_Clicked(object sender, RoutedEventArgs e)
        {
            ShowPage(G_Home);
        }


        private void ShowPage(Border show)
        {
            //All Pages\\
            Border[] pages = {G_Review, G_Home};

            //If not set Group Box then Hide\\
            foreach (Border page in pages)
            {          
                if (page.Name != show.Name)
                {
                    page.Visibility = Visibility.Collapsed;
                }else
                {
                    page.Visibility = Visibility.Visible;
                }
            }
        }


    }
}
