using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ChatStudents_Kurlishuk.Models;
using ChatStudents_Kurlishuk.Classes;
using Microsoft.Win32;


namespace ChatStudents_Kurlishuk.Pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public string srcUserImage = "";
        UsersContext usersContext = new UsersContext();
        public Login()
        {
            InitializeComponent();
        }

        private void SelectPhoto(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Vibor foto";
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "JPG FIles (*.jpg)|*jpg|PNG Files (*.png)|*.png|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                imgUser.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                srcUserImage = openFileDialog.FileName;
            }
        }
        public bool ChechEmpty(string Pattern, string Input)
        {
            Match m = Regex.Match(Input, Pattern);
            return m.Success;
        }

        private void Continue(object sender, RoutedEventArgs e)
        {
            if (!ChechEmpty("^[А-ЯёЁ][а-яА-ЯёЁ]*$", LastName.Text))
            {
                MessageBox.Show("Укажите фамилию");
                return;
            }
            if (!ChechEmpty("^[А-ЯёЁ][а-яА-ЯёЁ]*$", FirstName.Text))
            {
                MessageBox.Show("Укажите имя");
                return;
            }
            if (!ChechEmpty("^[А-ЯёЁ][а-яА-ЯёЁ]*$", SurName.Text))
            {
                MessageBox.Show("Укажите Отчество");
                return;
            }
            if (String.IsNullOrEmpty(srcUserImage))
            {
                MessageBox.Show("Укажите Фото");
                return;
            }
            if (usersContext.Users.Where(x => x.Firstname == FirstName.Text && x.Lastname == LastName.Text && x.Surname == SurName.Text).Count() > 0)
            {
                MainWindow.Instance.LoginUser = usersContext.Users.Where(x => x.Firstname == FirstName.Text && x.Lastname == LastName.Text && x.Surname == SurName.Text).First();
                MainWindow.Instance.LoginUser.Photo = File.ReadAllBytes(srcUserImage);
                usersContext.SaveChanges();
            }
            else
            {
                usersContext.Users.Add(new Users(LastName.Text, FirstName.Text, SurName.Text, File.ReadAllBytes(srcUserImage), true));
                usersContext.SaveChanges();
                MainWindow.Instance.LoginUser = usersContext.Users.Where(x => x.Firstname == FirstName.Text && x.Lastname == LastName.Text && x.Surname == SurName.Text).First();
            }
            MainWindow.Instance.OpenPages(new Pages.Main());
        }
    }
}
