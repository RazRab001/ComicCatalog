using ReviewInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using ComicAnalizer;

namespace ComicCatalog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            comboBox.Items.Add(ReviewAutors.TornadoCom);
            comboBox.Items.Add(ReviewAutors.TruReview);
            comboBox.Items.Add(ReviewAutors.AAmazing);
            Analizer.Load();
            UpdateText();
        }

        private void UpdateText()
        {
            listBox.Items.Clear();
            foreach(Comic comic in Analizer.Catalog)
            {
                listBox.Items.Add(comic.ToString());
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(textNum.Text, out int value))
            {
                Analizer.AddComic(textName.Text, value);
                UpdateText();
            }
            else
            {
                textNum.Text = "Wrong value";
            }
        }
        private void SortNameButton_Click(object sender, RoutedEventArgs e)
        {
            Analizer.Catalog.Sort(new SortByName());
            UpdateText();
        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Analizer.Save();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (Analizer.RemoveComic(textName.Text))
            {
                if (int.TryParse(textNum.Text, out int value))
                {
                    Analizer.RemoveComic(value);
                }
            }
            UpdateText();
        }
        private void SortIsuueButton_Click(object sender, RoutedEventArgs e)
        {
            Analizer.Catalog.Sort(new SortByIssue());
            UpdateText();
        }

        private void ShowReview_Button(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
            foreach (var comic in Analizer.ShowReview((ReviewAutors)comboBox.SelectedItem))
            {
                listBox.Items.Add(comic);
            }
        }
    }
}
