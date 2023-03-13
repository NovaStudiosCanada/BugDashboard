using BugDashboard.src;
using System;
using System.Collections.Generic;
using System.IO;
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
    
    public partial class MainWindow : Window
    {
        //Backend\\
        private readonly JuiceHandler juiceHandler = new JuiceHandler();

        //Page Vars\\
        private int currentJuiceIndex = 1;

        public MainWindow()
        {
            InitializeComponent();

            //Get Juice ID's\\
            List<string> juiceIds = new List<string>
            {
                "123456",
                "654321"
            };

            //Get/Set Sessions\\
            juiceHandler.GetAllSessions(juiceIds);
        }

        private void B_Review_Checked(object sender, RoutedEventArgs e)
        {
            LoadReviewsData();
            ShowPage(G_Review);
        }

        private void B_Home_Clicked(object sender, RoutedEventArgs e)
        {
            ShowPage(G_Home);
        }
        private void ReviewLeftUp(object sender, MouseButtonEventArgs e)
        {
            if (currentJuiceIndex > 1)
            {
                currentJuiceIndex--;
                LoadReviewsData();
            }
        }

        private void ReviewRightUp(object sender, MouseButtonEventArgs e)
        {
            if (currentJuiceIndex < juiceHandler.CurrentSessionCount())
            {
                currentJuiceIndex++;
                LoadReviewsData();
            }
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

        private void LoadReviewsData()
        {

            //Set Text\\
            int currentSessionCount = juiceHandler.CurrentSessionCount();
            Review_TotalSessions.Text = currentJuiceIndex + "/" + currentSessionCount;

            //Load Sesssion Index\\
            SetReviewData(juiceHandler.GetJuiceSession(currentJuiceIndex - 1).directory);
        }

        private void SetReviewData(DirectoryInfo juiceDir)
        {

            //Get Juice Folder Files\\
            List<FileInfo> files = juiceHandler.GetJuiceFiles(juiceDir);

            //Clear Tree View\\
            ReviewTreeView.Items.Clear();

            //Show on Tree View\\
            foreach (FileInfo file in files)
            {
                TreeViewItem newItem = new TreeViewItem
                {
                    Header = file,
                    Foreground = new SolidColorBrush(Colors.White),
                    Margin = new Thickness(0, 10, 0, 0),                         
                };

                newItem.MouseLeftButtonUp += new MouseButtonEventHandler(OnTreeClicked);
                
                ReviewTreeView.Items.Add(newItem);
            }

            //Set Initial Content\\
            ReviewContent.Text = juiceHandler.ReadFile(files[0]);

        }

        private void OnTreeClicked(object sender, MouseButtonEventArgs e)
        {
            //Selected File\\
            var selection = e.Source as TreeViewItem;
            var fileClicked = selection.Header as FileInfo;

            //Set Content to selected File\\
            ReviewContent.Text = juiceHandler.ReadFile(fileClicked);
            
        }

    }
}
