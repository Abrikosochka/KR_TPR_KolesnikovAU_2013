using System;
using System.Windows;
using System.Windows.Threading;

namespace Poisson_distribution
{ 
    public partial class MainWindow : Window
    {

        private DispatcherTimer timer;

        public MainWindow()
        {
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(60);
            
            timer.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Simulation_Window simulation_Window = new Simulation_Window();
            simulation_Window.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            My_data My_data_Window = new My_data();
            My_data_Window.Show();
            this.Close();
        }
    }
}