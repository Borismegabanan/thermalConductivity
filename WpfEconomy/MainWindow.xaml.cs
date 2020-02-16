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
using LiveCharts;
using LiveCharts.Wpf;

namespace WpfEconomy
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SeriesCollection SeriesCollection { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            IEnumerable<double> ie= new List<double>() { 4, 6, 5, 2, 4 };
            ChartCreate(ie);
            DataContext = this;
        }

        /// <summary>
        /// Create the chart, add value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        public void ChartCreate<T>(IEnumerable<T> values)
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<T>(values)
                },
            };

            
            
        }

    }
}
