using Npgsql;
using System;
using System.Collections;
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
using System.Windows.Threading;

namespace Poisson_distribution
{
    /// <summary>
    /// Логика взаимодействия для Simulation_Window.xaml
    /// </summary>
    public partial class Simulation_Window : Window
    {

        Random random = new Random();

        int count_potok;
        private int count_people = 0;

        Generation_queue queue;
        private DispatcherTimer MegaTimer;
        private DispatcherTimer peopleTimer;
        private DispatcherTimer queueTimer;
        private DispatcherTimer delayTimer;
        private DispatcherTimer deleteTimer1;
        private DispatcherTimer deleteTimer2;
        private DispatcherTimer deleteTimer3;
        private int secondsElapsed = 0;
        public Simulation_Window() {
            queue = new Generation_queue();
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            Loaded += Simulation_Window_Loaded;

            MegaTimer = new DispatcherTimer();
            MegaTimer.Tick += MegaTimer_Tick;

            peopleTimer = new DispatcherTimer();
            peopleTimer.Tick += PeopleTimer_Tick;

            queueTimer = new DispatcherTimer();
            queueTimer.Tick += QueueTimer_Tick;

            delayTimer = new DispatcherTimer();
            delayTimer.Tick += DelayTimer_Tick;

            deleteTimer1 = new DispatcherTimer();
            deleteTimer1.Tick += DeleteTimer1_Tick;

            deleteTimer2 = new DispatcherTimer();
            deleteTimer2.Tick += DeleteTimer2_Tick;

            deleteTimer3 = new DispatcherTimer();
            deleteTimer3.Tick += DeleteTimer3_Tick;         
        }
        public static long Fact(long n) {
            if (n == 0)
                return 1;
            else
                return n * Fact(n - 1);
        }
        private async void MegaTimer_Tick(object sender, EventArgs e) {
            secondsElapsed++;
            if (secondsElapsed > 20 && queue.queue.Count < 1) {
                peopleTimer.Stop();
                count_people = -1;
                if (Queue1.Text == "0" && Queue2.Text == "0" && Queue3.Text == "0" &&
                Delay1.Text == "0" && Delay2.Text == "0" && Delay3.Text == "0") {                  
                    await DataBase.DeleteMan(Convert.ToInt32(Sink.Text));
                    Sink.Text = Convert.ToString(Convert.ToInt32(Sink.Text) - 1);
                    MegaTimer.Stop();
                    queueTimer.Stop();
                    delayTimer.Stop();
                    deleteTimer1.Stop();
                    deleteTimer2.Stop();
                    deleteTimer3.Stop();
                    count_people = 0;
                    count_potok = 0;
                    secondsElapsed = 0;
                    Choice_Panel.Visibility = Visibility.Visible;
                    Results results = new Results(Convert.ToInt32(Count_Potok.Text));
                    results.Show();
                    this.Close();
                }
            }     
        }
        private async void PeopleTimer_Tick(object sender, EventArgs e) {
            await Add_people();
        }
        private async void QueueTimer_Tick(object sender, EventArgs e) {
            await Add_que();
        }
        private async void DelayTimer_Tick(object sender, EventArgs e) {
            await Add_delay();
        }
        private async void DeleteTimer1_Tick(object sender, EventArgs e) {
            await Out_delay1();
        }
        private async void DeleteTimer2_Tick(object sender, EventArgs e) {
            await Out_delay2();
        }
        private async void DeleteTimer3_Tick(object sender, EventArgs e) {
            await Out_delay3();
        }
        private async void Simulation_Window_Loaded(object sender, RoutedEventArgs e) {
            await Start_Table();
        }
        private async Task Start_Table() {
            await DataBase.StartCreateTable();
        }
        private async Task Start_queue(DateTime? start_time_queue, DateTime? finish_time_queue, DateTime? start_time_delay, DateTime? finish_time_delay) {
            await DataBase.DoInsertInTable(start_time_queue, finish_time_queue, start_time_delay, finish_time_delay);
        }
        private async Task Delete_delay1(int id, DateTime? time) {
            await DataBase.StartDeleteDelay(id, time);
            Sink.Text = Convert.ToString(Convert.ToInt32(Sink.Text) + 1);
            queue.delete_delay(queue.in_delaychik1[0].Key[0], 1, time);
            Delay1.Text = "0";
        }
        private async Task Delete_delay2(int id, DateTime? time) {          
            await DataBase.StartDeleteDelay(id, time);
            Sink.Text = Convert.ToString(Convert.ToInt32(Sink.Text) + 1);
            queue.delete_delay(queue.in_delaychik2[0].Key[0], 2, time);
            Delay2.Text = "0";
        }
        private async Task Delete_delay3(int id, DateTime? time) {
            await DataBase.StartDeleteDelay(id, time);
            Sink.Text = Convert.ToString(Convert.ToInt32(Sink.Text) + 1);
            queue.delete_delay(queue.in_delaychik3[0].Key[0], 3, time);
            Delay3.Text = "0";
        }
        private async Task Start_delay1(int id, DateTime? time) {
            await DataBase.StartInsertInDelay(id, time);
            Queue1.Text = Convert.ToString(Convert.ToInt32(Queue1.Text) - 1);                      
            queue.insert_in_delay(queue.in_queue1[0].Key[0], 1, time);
            Delay1.Text = Convert.ToString(Convert.ToInt32(Delay1.Text) + 1);
        }
        private async Task Start_delay2(int id, DateTime? time) {
            await DataBase.StartInsertInDelay(id, time);
            Queue2.Text = Convert.ToString(Convert.ToInt32(Queue2.Text) - 1);                      
            queue.insert_in_delay(queue.in_queue2[0].Key[0], 2, time);
            Delay2.Text = Convert.ToString(Convert.ToInt32(Delay2.Text) + 1);
        }
        private async Task Start_delay3(int id, DateTime? time) {
            await DataBase.StartInsertInDelay(id, time);
            Queue3.Text = Convert.ToString(Convert.ToInt32(Queue3.Text) - 1);                      
            queue.insert_in_delay(queue.in_queue3[0].Key[0], 3, time);
            Delay3.Text = Convert.ToString(Convert.ToInt32(Delay3.Text) + 1);
        }
        private async Task Start_in_queue1(int id, DateTime? time) {
            await DataBase.StartInsertInQueue(id, time);
            queue.insert_in_queue(queue.queue[0].Key, 1, time);
            
            Source.Text = Convert.ToString(Convert.ToInt32(Source.Text) - 1);        
            Queue1.Text = Convert.ToString(Convert.ToInt32(Queue1.Text) + 1);
        }
        private async Task Start_in_queue2(int id, DateTime? time) {
            await DataBase.StartInsertInQueue(id, time);
            queue.insert_in_queue(queue.queue[0].Key, 2, time);
            
            Source.Text = Convert.ToString(Convert.ToInt32(Source.Text) - 1);                
            Queue2.Text = Convert.ToString(Convert.ToInt32(Queue2.Text) + 1);
        }
        private async Task Start_in_queue3(int id, DateTime? time) {
            await DataBase.StartInsertInQueue(id, time);
            queue.insert_in_queue(queue.queue[0].Key, 3, time);
            
            Source.Text = Convert.ToString(Convert.ToInt32(Source.Text) - 1);                     
            Queue3.Text = Convert.ToString(Convert.ToInt32(Queue3.Text) + 1);
        }
        private async void Start_Click(object sender, RoutedEventArgs e) {
            if(Count_Potok.Text != "") {
                int shablon;
                int minValue;
                int maxValue;
                int mean;
                int stdDev;
                if (Int32.TryParse(TextBox1.Text, out shablon) &&
                    Int32.TryParse(TextBox2.Text, out shablon) &&
                    Int32.TryParse(TextBox3.Text, out shablon) &&
                    Int32.TryParse(TextBox4.Text, out shablon)) {
                    minValue = Convert.ToInt32(TextBox1.Text);
                    maxValue = Convert.ToInt32(TextBox2.Text);
                    mean = Convert.ToInt32(TextBox3.Text);
                    stdDev = Convert.ToInt32(TextBox4.Text);

                    if (minValue > 0 && minValue <= 10 &&
                        maxValue > 0 && maxValue <= 10 &&
                        mean > 0 && mean <= 10 &&
                        stdDev > 0 && stdDev <= 10 &&
                        minValue < maxValue && mean < stdDev) {
                        Direct_Queue_1.Visibility = Visibility.Visible;
                        Direct_Delay_1.Visibility = Visibility.Visible;
                        Direct_Sink_1.Visibility = Visibility.Visible;
                        Direct_Queue_2.Visibility = Visibility.Visible;
                        Direct_Delay_2.Visibility = Visibility.Visible;
                        Direct_Sink_2.Visibility = Visibility.Visible;
                        Direct_Queue_3.Visibility = Visibility.Visible;
                        Direct_Delay_3.Visibility = Visibility.Visible;
                        Direct_Sink_3.Visibility = Visibility.Visible;
                        Rectangle_1.Visibility = Visibility.Visible;
                        Rectangle_2.Visibility = Visibility.Visible;
                        Rectangle_3.Visibility = Visibility.Visible;
                        Rectangle_Delay_1.Visibility = Visibility.Visible;
                        Rectangle_Delay_2.Visibility = Visibility.Visible;
                        Rectangle_Delay_3.Visibility = Visibility.Visible;
                        Queue1.Visibility = Visibility.Visible;
                        Queue2.Visibility = Visibility.Visible;
                        Queue3.Visibility = Visibility.Visible;
                        Delay1.Visibility = Visibility.Visible;
                        Delay2.Visibility = Visibility.Visible;
                        Delay3.Visibility = Visibility.Visible;

                        Label_Delay_1.Visibility = Visibility.Visible;
                        Label_Delay_2.Visibility = Visibility.Visible;
                        Label_Delay_3.Visibility = Visibility.Visible;
                        Label_Queue_1.Visibility = Visibility.Visible;
                        Label_Queue_2.Visibility = Visibility.Visible;
                        Label_Queue_3.Visibility = Visibility.Visible;

                        count_potok = Count_Potok.SelectedIndex + 1;

                        if (count_potok == 1) {
                            Direct_Queue_2.Visibility = Visibility.Hidden;
                            Direct_Delay_2.Visibility = Visibility.Hidden;
                            Direct_Sink_2.Visibility = Visibility.Hidden;
                            Direct_Queue_3.Visibility = Visibility.Hidden;
                            Direct_Delay_3.Visibility = Visibility.Hidden;
                            Direct_Sink_3.Visibility = Visibility.Hidden;
                            Rectangle_2.Visibility = Visibility.Hidden;
                            Rectangle_3.Visibility = Visibility.Hidden;
                            Rectangle_Delay_2.Visibility = Visibility.Hidden;
                            Rectangle_Delay_3.Visibility = Visibility.Hidden;
                            Queue2.Visibility = Visibility.Hidden;
                            Queue3.Visibility = Visibility.Hidden;
                            Delay2.Visibility = Visibility.Hidden;
                            Delay3.Visibility = Visibility.Hidden;
                            Label_Delay_2.Visibility = Visibility.Hidden;
                            Label_Delay_3.Visibility = Visibility.Hidden;
                            Label_Queue_2.Visibility = Visibility.Hidden;
                            Label_Queue_3.Visibility = Visibility.Hidden;
                        }
                        if (count_potok == 2) {
                            Direct_Queue_1.Visibility = Visibility.Hidden;
                            Direct_Delay_1.Visibility = Visibility.Hidden;
                            Direct_Sink_1.Visibility = Visibility.Hidden;
                            Rectangle_1.Visibility = Visibility.Hidden;
                            Rectangle_Delay_1.Visibility = Visibility.Hidden;
                            Queue1.Visibility = Visibility.Hidden;
                            Delay1.Visibility = Visibility.Hidden;
                            Label_Delay_1.Visibility = Visibility.Hidden;
                            Label_Queue_1.Visibility = Visibility.Hidden;
                        }

                        queue.queue.Clear();
                        queue.in_queue1.Clear();
                        queue.in_queue2.Clear();
                        queue.in_queue3.Clear();
                        queue.in_delaychik1.Clear();
                        queue.in_delaychik2.Clear();
                        queue.in_delaychik3.Clear();
                        queue.out_delaychik.Clear();

                        Queue1.Text = "0";
                        Queue2.Text = "0";
                        Queue3.Text = "0";
                        Delay1.Text = "0";
                        Delay2.Text = "0";
                        Delay3.Text = "0";
                        Source.Text = "0";
                        Sink.Text = "0";
                        count_people = 0;

                        double randomNumber1 = random.NextDouble();
                        int valueFromNormalDist1 = Convert.ToInt32(mean + randomNumber1 * stdDev);

                        double randomNumber2 = random.NextDouble();
                        int valueFromUniformDist2 = Convert.ToInt32(minValue + randomNumber2 * (maxValue - minValue));

                        MegaTimer.Interval = TimeSpan.FromSeconds(1);
                        peopleTimer.Interval = TimeSpan.FromSeconds(random.Next(Convert.ToInt32(minValue), valueFromUniformDist2));
                        queueTimer.Interval = TimeSpan.FromSeconds(1);
                        delayTimer.Interval = TimeSpan.FromSeconds(1);
                        deleteTimer1.Interval = TimeSpan.FromSeconds(random.Next(Convert.ToInt32(mean), valueFromNormalDist1));
                        deleteTimer2.Interval = TimeSpan.FromSeconds(random.Next(Convert.ToInt32(mean), valueFromNormalDist1));
                        deleteTimer3.Interval = TimeSpan.FromSeconds(random.Next(Convert.ToInt32(mean), valueFromNormalDist1));

                        MegaTimer.Start();
                        peopleTimer.Start();
                        queueTimer.Start();
                        delayTimer.Start();
                        deleteTimer1.Start();
                        deleteTimer2.Start();
                        deleteTimer3.Start();
                        Choice_Panel.Visibility = Visibility.Hidden;
                        await Start_Table();
                    }
                    else {
                        MessageBox.Show(" The value in the left cell should be less than in the right one\n" +
                            " The values in the cell must not be less than 1 and more than 10 ");
                    }
                }
                else {
                    MessageBox.Show(" Only numbers should be entered ");
                }
            }
            else {
                MessageBox.Show(" Select the number of threads ");
            }
        }
        private async Task Add_people() {
            queue.add_queue(count_people, null);
            await Start_queue(null, null, null, null);
            Source.Text = Convert.ToString(Convert.ToInt32(Source.Text) + 1);
            count_people++;           
        }
        private async Task Add_que() {
            if (queue.queue.Count > 0) {
                if (count_potok == 1) {
                    DateTime tik = DateTime.Now;
                    await Start_in_queue1(queue.queue[0].Key, tik);
                }
                else if (count_potok == 2) {
                    if (Convert.ToInt32(Queue2.Text) == Math.Min(Convert.ToInt32(Queue2.Text), Convert.ToInt32(Queue3.Text))) {
                        DateTime tik = DateTime.Now;
                        await Start_in_queue2(queue.queue[0].Key, tik);
                    }
                    else if (Convert.ToInt32(Queue3.Text) == Math.Min(Convert.ToInt32(Queue2.Text), Convert.ToInt32(Queue3.Text))) {
                        DateTime tik = DateTime.Now;
                        await Start_in_queue3(queue.queue[0].Key, tik);
                    }
                }
                else {
                    if (Convert.ToInt32(Queue1.Text) == Math.Min(Convert.ToInt32(Queue1.Text), Math.Min(Convert.ToInt32(Queue2.Text), Convert.ToInt32(Queue3.Text)))) {
                        DateTime tik = DateTime.Now;
                        await Start_in_queue1(queue.queue[0].Key, tik);
                    }
                    else if (Convert.ToInt32(Queue2.Text) == Math.Min(Convert.ToInt32(Queue1.Text), Math.Min(Convert.ToInt32(Queue2.Text), Convert.ToInt32(Queue3.Text)))) {
                        DateTime tik = DateTime.Now;
                        await Start_in_queue2(queue.queue[0].Key, tik);
                    }
                    else if (Convert.ToInt32(Queue3.Text) == Math.Min(Convert.ToInt32(Queue1.Text), Math.Min(Convert.ToInt32(Queue2.Text), Convert.ToInt32(Queue3.Text)))) {
                        DateTime tik = DateTime.Now;
                        await Start_in_queue3(queue.queue[0].Key, tik);
                    }
                }               
            }
        }
        private async Task Add_delay() {
            if (queue.in_queue1.Count > 0 && queue.in_queue1[0].Key[1] == 1 && Convert.ToInt32(Delay1.Text) < 1) {
                DateTime tik = DateTime.Now;
                await Start_delay1(queue.in_queue1[0].Key[0], tik);
            }
            else if (queue.in_queue2.Count > 0 && queue.in_queue2[0].Key[1] == 2 && Convert.ToInt32(Delay2.Text) < 1) {
                DateTime tik = DateTime.Now;
                await Start_delay2(queue.in_queue2[0].Key[0], tik);
            }
            else if (queue.in_queue3.Count > 0 &&  queue.in_queue3[0].Key[1] == 3 && Convert.ToInt32(Delay3.Text) < 1) {
                DateTime tik = DateTime.Now;
                await Start_delay3(queue.in_queue3[0].Key[0], tik);
            }
        }
        private async Task Out_delay1() {
            if (queue.in_delaychik1.Count > 0) {
                DateTime tik = DateTime.Now;
                await Delete_delay1(queue.in_delaychik1[0].Key[0], tik);
            }
        }
        private async Task Out_delay2() {
            if (queue.in_delaychik2.Count > 0) {
                DateTime tik = DateTime.Now;
                await Delete_delay2(queue.in_delaychik2[0].Key[0], tik);
            }
        }
        private async Task Out_delay3() {
            if (queue.in_delaychik3.Count > 0) {
                DateTime tik = DateTime.Now;
                await Delete_delay3(queue.in_delaychik3[0].Key[0], tik);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
