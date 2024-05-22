using Aspose.Cells;
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
using System.Diagnostics;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using static Npgsql.Replication.PgOutput.Messages.RelationMessage;
using System.Collections.ObjectModel;
using System.Collections;
using System.Runtime.Intrinsics.Arm;
using Newtonsoft.Json.Linq;

namespace Poisson_distribution
{
    /// <summary>
    /// Логика взаимодействия для My_data.xaml
    /// </summary>
    public partial class My_data : Window
    {
        List<string> tempStartQueue = new List<string>();
        List<string> tempFinishQueue = new List<string>();
        List<string> tempStartDelay = new List<string>();
        List<string> tempFinishDelay = new List<string>();
        public ObservableCollection<MyData> Data { get; set; }
        public My_data() {
            InitializeComponent();
            Data = new ObservableCollection<MyData>();
            TableTime.ItemsSource = Data;
        }
        private void Button_Click(object sender, RoutedEventArgs e) {
            try {
                // Загрузить файл Excel
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                if (openFileDialog.ShowDialog() == true) {
                    string filePath = openFileDialog.FileName;
                    Workbook wb = new Workbook(filePath);

                    // Получить все рабочие листы
                    WorksheetCollection collection = wb.Worksheets;

                    // Перебрать все рабочие листы
                    for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++) {

                        // Получить рабочий лист, используя его индекс
                        Worksheet worksheet = collection[worksheetIndex];

                        // Печать имени рабочего листа
                        System.Diagnostics.Trace.WriteLine("Worksheet: " + worksheet.Name);

                        // Получить количество строк и столбцов
                        int rows = worksheet.Cells.MaxDataRow + 1;
                        int cols = worksheet.Cells.MaxDataColumn + 1;

                        for (int i = 0; i < cols; i++) {
                            for (int j = 0; j < rows; j++) {
                                var cellValue = worksheet.Cells[j, i].Value;
                                if (cellValue != null && cellValue.ToString() == "start_time_queue") {
                                    for (int k = j + 1; k < rows; k++) {
                                        var value = worksheet.Cells[k, i].Value;
                                        if (value != null && DateTime.TryParse(value.ToString(), out DateTime dateValue)) {
                                            tempStartQueue.Add(value.ToString());
                                            System.Diagnostics.Trace.WriteLine(value.ToString());
                                        }
                                        else { j = k; break; }
                                    }
                                }
                                if (cellValue != null && cellValue.ToString() == "finish_time_queue") {
                                    for (int k = j + 1; k < rows; k++) {
                                        var value = worksheet.Cells[k, i].Value;
                                        if (value != null && DateTime.TryParse(value.ToString(), out DateTime dateValue)) {
                                            tempFinishQueue.Add(value.ToString());
                                            System.Diagnostics.Trace.WriteLine(value.ToString());
                                        }
                                        else { j = k; break; }
                                    }
                                }
                                if (cellValue != null && cellValue.ToString() == "start_time_delay") {
                                    for (int k = j + 1; k < rows; k++) {
                                        var value = worksheet.Cells[k, i].Value;
                                        if (value != null && DateTime.TryParse(value.ToString(), out DateTime dateValue)) {
                                            tempStartDelay.Add(value.ToString());
                                            System.Diagnostics.Trace.WriteLine(value.ToString());
                                        }
                                        else { j = k; break; }
                                    }
                                }
                                if (cellValue != null && cellValue.ToString() == "finish_time_delay") {
                                    for (int k = j + 1; k < rows; k++) {
                                        var value = worksheet.Cells[k, i].Value;
                                        if (value != null && DateTime.TryParse(value.ToString(), out DateTime dateValue)) {
                                            tempFinishDelay.Add(value.ToString());
                                            System.Diagnostics.Trace.WriteLine(value.ToString());
                                        }
                                        else { j = k; break; }
                                    }
                                }
                            }
                            System.Diagnostics.Trace.WriteLine(" ");
                        }
                    }
                    int maxCount = Math.Max(Math.Max(tempStartQueue.Count, tempFinishQueue.Count), Math.Max(tempStartDelay.Count, tempFinishDelay.Count));
                    for (int m = 0; m < maxCount; m++) {
                        MyData data = new MyData { Number = m + 1 }; // Нумерация строк с 1
                        if (m < tempStartQueue.Count) {
                            data.start_time_queue = tempStartQueue[m];
                        }
                        if (m < tempFinishQueue.Count) {
                            data.finish_time_queue = tempFinishQueue[m];
                        }
                        if (m < tempStartDelay.Count) {
                            data.start_time_delay = tempStartDelay[m];
                        }
                        if (m < tempFinishDelay.Count) {
                            data.finish_time_delay = tempFinishDelay[m];
                        }
                        Data.Add(data);
                    }
                }
                else {
                    MessageBox.Show("Вы не выбрали файл или это не файл excel");
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }
        private async Task Start_queue() {
            await DataBase.StartCreateTable();
            for (int i = 0; i < tempStartQueue.Count; i++) {
                await DataBase.DoInsertInTable(Convert.ToDateTime(tempStartQueue[i]), Convert.ToDateTime(tempFinishQueue[i]),
                    Convert.ToDateTime(tempStartDelay[i]), Convert.ToDateTime(tempFinishDelay[i]));
            }
        }
        private async void Button_Click_1(object sender, RoutedEventArgs e) {
            if(tempStartQueue.Count > 0) {
                if (Count_Potok.Text != "") {
                    await Start_queue();
                    Results results = new Results(Convert.ToInt32(Count_Potok.Text));
                    results.Show();
                    this.Close();
                }
                else {
                    MessageBox.Show("Select the number of threads");
                }
            }
            else {
                MessageBox.Show("You have not uploaded the data");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void TableTime_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e) {
            if (e.EditAction == DataGridEditAction.Commit) // Проверка, что редактирование было завершено
    {
                DataGrid grid = sender as DataGrid;
                if (grid != null) {
                    DataGridCell editedCell = e.Column.GetCellContent(e.Row).Parent as DataGridCell;
                    if (editedCell != null) {
                        TextBox textBox = editedCell.Content as TextBox;
                        string editedValuestring = textBox.Text;

                        int columnIndex = e.Column.DisplayIndex; // Номер столбца
                        int rowIndex = e.Row.GetIndex(); // Номер строки

                        DateTime editedDateTime;
                        if (DateTime.TryParse(editedValuestring, out editedDateTime)) // Проверка успешного преобразования в DateTime
                        {
                            // Определение, в какой список добавить значение и какой элемент заменить
                            if (columnIndex == 1) // Предположим, что нужно добавить значение в tempStartQueue
                            {
                                if (rowIndex < tempStartQueue.Count) {
                                    tempStartQueue[rowIndex] = editedDateTime.ToString(); // Замена элемента в списке
                                }
                                else {
                                    tempStartQueue.Add(editedDateTime.ToString()); // Добавление нового элемента в список
                                }
                            }
                            else if (columnIndex == 2 || columnIndex == 3) // Предположим, что нужно добавить значение в другой список
                            {
                                if (rowIndex < tempFinishQueue.Count) {
                                    tempFinishQueue[rowIndex] = editedDateTime.ToString();
                                    tempStartDelay[rowIndex] = editedDateTime.ToString();// Замена элемента в списке
                                }
                                else {
                                    tempFinishQueue.Add(editedDateTime.ToString()); // Добавление нового элемента в список
                                    tempStartDelay[rowIndex] = editedDateTime.ToString();
                                }
                            }
                            else if (columnIndex == 4) // Предположим, что нужно добавить значение в другой список
                            {
                                if (rowIndex < tempFinishDelay.Count) {
                                    tempFinishDelay[rowIndex] = editedDateTime.ToString(); // Замена элемента в списке
                                }
                                else {
                                    tempFinishDelay.Add(editedDateTime.ToString()); // Добавление нового элемента в список
                                }
                            }
                        }
                        else {
                            MessageBox.Show("Невозможно преобразовать значение в тип DateTime.");
                        }
                    }
                }
            }
        }
    }
    public class MyData
    {
        public int Number { get; set; }
        public string start_time_queue { get; set; }
        public string finish_time_queue { get; set; }
        public string start_time_delay { get; set; }
        public string finish_time_delay { get; set; }
    }
 }
   