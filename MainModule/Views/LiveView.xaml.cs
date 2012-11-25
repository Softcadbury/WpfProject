using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MainModule.Views
{
    /// <summary>
    /// Interaction logic for LiveView.xaml
    /// </summary>
    public partial class LiveView : UserControl
    {
        public LiveView()
        {
            InitializeComponent();
            DataContext = new ViewModels.LiveViewModel()
            {
                HeartChart = this.HeartChart,
                TempChart = this.TempChart
            };
        }

        private void UserControlLoaded(object sender, RoutedEventArgs e)
        {
            ((ViewModels.LiveViewModel)DataContext).UserControlLoaded();
        }
    }
}