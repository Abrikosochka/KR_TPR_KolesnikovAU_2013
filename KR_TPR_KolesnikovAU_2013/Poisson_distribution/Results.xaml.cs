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
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Poisson_distribution
{
    /// <summary>
    /// Логика взаимодействия для Results.xaml
    /// </summary>
    public partial class Results : Window
    {
        double average_time;
        int count_potok;       
        double intesiv_potok_zayvok;
        double intesiv_potok_obsl;
        double prived_intensiv_potok;
        double kanal_free;
        double vse_kanal_free;
        double abdsolut_propusk;        
        double nominal_proizvod;
        double factich_proizvod;

        int Fact(int n) {
            if (n == 1) return 1;

            return n * Fact(n - 1);
        }
        public Results(int potok) { 
            InitializeComponent();
            count_potok = potok;
        }
        private void Retry_Click(object sender, RoutedEventArgs e) {
            MainWindow simulation_Window = new MainWindow();
            simulation_Window.Show();
            this.Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e) {
            average_time = await DataBase.StartCountIntensiv();
            intesiv_potok_zayvok = (await DataBase.StartCountPeople())/60.0;
            intesiv_potok_obsl = 1.00 / (average_time);
            prived_intensiv_potok = intesiv_potok_zayvok / intesiv_potok_obsl;
            kanal_free = intesiv_potok_obsl / (intesiv_potok_zayvok + intesiv_potok_obsl);
            vse_kanal_free = 1 / (1 + prived_intensiv_potok + Math.Pow(prived_intensiv_potok, 2) / 2 +
                + Math.Pow(prived_intensiv_potok, count_potok / (Fact(count_potok) * (1 - prived_intensiv_potok / count_potok))));
            abdsolut_propusk = kanal_free * intesiv_potok_zayvok;
            nominal_proizvod = intesiv_potok_zayvok / average_time;
            factich_proizvod = kanal_free / nominal_proizvod;


            Average_time.Text = " " + average_time.ToString("F3");
            Intesiv_potok_zayvok.Text = " " + intesiv_potok_zayvok.ToString("F3");
            Intensiv_potok_obslu.Text = " " + intesiv_potok_obsl.ToString("F3");
            Prived_intensiv_potok.Text = " " + prived_intensiv_potok.ToString("F3");
            Kanal_free.Text = " " + kanal_free.ToString("F3");
            Vse_kanal_Free.Text = " " + vse_kanal_free.ToString("F3");
            Abdsolut_propusk.Text = " " + abdsolut_propusk.ToString("F3");
            Nominal_proizvod.Text = " " + nominal_proizvod.ToString("F3");
            Factich_proizvod.Text = " " + factich_proizvod.ToString("F3");

            if(vse_kanal_free > 0.700) {
                Answer.Text = "Возможно вам стоит сократить количество потоков";
            }
            else if (vse_kanal_free < 0.500) {
                Answer.Text = "Возможно вам стоит увеличить количество потоков";
            }
            else {
                Answer.Text = "Все работает замечательно!";
            }
        }
    }
}
