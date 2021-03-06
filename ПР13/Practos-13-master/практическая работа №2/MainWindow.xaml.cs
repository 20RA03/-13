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
using LibMas;
using System.Data;
using System.IO;

namespace ПР13
{

    public partial class MainWindow : Window
    {
        public int[] generatedArray;
        public MainWindow()
        {
            InitializeComponent();
        }
        private DataTable res = new DataTable();


        public int[,] generatedMatrix;

        private void FillMatrix(object sender, RoutedEventArgs e)
        {
            res = new DataTable();
            NumbersTable2.ItemsSource = null;
            NumbersTable2.Items.Clear();
            NumbersTable2.Columns.Clear();
            generatedMatrix = Matrixs.FillMatrix(Convert.ToInt32(MatrixRows.Text), Convert.ToInt32(MatrixColumn.Text));

            for (int i = 0; i < generatedMatrix.GetLength(1); i++)
            {
                res.Columns.Add("column " + i.ToString(), typeof(string));
            }
            for (int i = 0; i < generatedMatrix.GetLength(0); i++)
            {

                DataRow row = res.NewRow();
                for (int j = 0; j < generatedMatrix.GetLength(1); j++)
                {
                    row[j] = generatedMatrix[i, j];
                }
                res.Rows.Add(row);
            }

            NumbersTable2.ItemsSource = res.DefaultView;
        }

        private void MaxColumnNamber(object sender, RoutedEventArgs e)
        {
            Tasks.MaxNumbersColumn(generatedMatrix, out string MaxNumber);
            MaxColumn.Text = Convert.ToString(MaxNumber);
        }

        private void saveMatrix(object sender, RoutedEventArgs e)
        {
            Matrixs.SaveMatrix1(generatedMatrix);
        }

        private void Loading(object sender, RoutedEventArgs e)
        {
            res = new DataTable();
            NumbersTable2.ItemsSource = null;
            NumbersTable2.Items.Clear();
            NumbersTable2.Columns.Clear();
            Matrixs.OpenMatrix(out int[,] savedMatrix);
            DataRow row = res.NewRow();

            for (int i = 0; i < savedMatrix.GetLength(1); i++)
            {
                res.Columns.Add("column " + i.ToString(), typeof(string));
            }
            for (int i = 0; i < savedMatrix.GetLength(0); i++)
            {

                DataRow row1 = res.NewRow();
                for (int j = 0; j < savedMatrix.GetLength(1); j++)
                {
                    row1[j] = savedMatrix[i, j];
                }
                res.Rows.Add(row1);
            }
            res.Rows.Add(row);
            NumbersTable2.ItemsSource = res.DefaultView;
        }

        private void FocusArray(object sender, MouseEventArgs e)
        {
            int _column = NumbersTable2.CurrentColumn.DisplayIndex;
            int _row = NumbersTable2.Items.IndexOf(NumbersTable2.CurrentItem);
            cellNumber.Text = $"[{_row + 1};{_column + 1}]";
        }

        private void Info(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Работу выполнил студент группы Ржевский Александр ИСП-34. Задание: Дана матрица размера M * N.В каждом ее столбце найти количество элементов,больших среднего арифметического всех элементов этого столбца", "Разработчик", MessageBoxButton.OK, MessageBoxImage.Information);

        }
        private void Clear(object sender, RoutedEventArgs e)
        {
            MatrixRows.Clear();
            MatrixColumn.Clear();
            MaxColumn.Clear();
            NumbersTable2.ItemsSource = null;
            NumbersTable2.Items.Clear();
            NumbersTable2.Columns.Clear();
        }
       
        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
