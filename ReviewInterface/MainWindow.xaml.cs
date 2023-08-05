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

namespace ReviewInterface
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Review.AddReview(new Review() {
                Autor = (ReviewAutors)comboBox.SelectedItem,
                Issue = int.Parse(issueText.Text),
                Value = (decimal)valueSlider.Value}
            );
        }

        private void DeleteAll_Button(object sender, RoutedEventArgs e)
        {
            Review.Reviews.Clear();
            Review.Save();
        }
    }
}
