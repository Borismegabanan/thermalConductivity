using System.Collections.Generic;
using System.Windows;
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
        public Plate2d _plate;

        public MainWindow()
        {
            InitializeComponent();
            
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
            DataContext = this;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (TextBox_N.Text == "" || TextBox_Tend.Text == "" || TextBox_R.Text == "" || TextBox_Lamda.Text == "" || TextBox_Ro.Text == "" ||
                TextBox_C.Text == "" || TextBox_T0.Text == "" || TextBox_Th.Text == "" )
            {
                MessageBox.Show("Введите значения");
            }
            else
            {

                _plate = new Plate2d(double.Parse(TextBox_N.Text), double.Parse(TextBox_Tend.Text), double.Parse(TextBox_R.Text), double.Parse(TextBox_Lamda.Text), double.Parse(TextBox_Ro.Text), double.Parse(TextBox_C.Text), double.Parse(TextBox_T0.Text), double.Parse(TextBox_Th.Text));
                ChartCreate<double>(_plate.Start());
            }
            
        }
    }
}
