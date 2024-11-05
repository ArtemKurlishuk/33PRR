using ChatStudents_Kurlishuk.Models;
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

namespace ChatStudents_Kurlishuk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance;
        public Users LoginUser = null;

        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            OpenPages(new Pages.Login());
        }

        public void OpenPages(Page page) =>
            Frame.Navigate(page);
    }
}
