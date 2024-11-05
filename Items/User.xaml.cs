using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChatStudents_Kurlishuk.Classes.Common;
using ChatStudents_Kurlishuk.Models;
using ChatStudents_Kurlishuk.Pages;

namespace ChatStudents_Kurlishuk.Items
{
    /// <summary>
    /// Логика взаимодействия для User.xaml
    /// </summary>
    public partial class User : UserControl
    {
        Users user;
        Pages.Main main;
        public User(Users user, Main main)
        {
            InitializeComponent();
            this.user = user;
            this.main = main;
            imgUser.Source = BitmapFromArrayByte.LoadImage(user.Photo);
            FIO.Content = user.FIO();
        }
        private void SelectChat(object sender, System.Windows.Input.MouseButtonEventArgs e) =>
            main.SelectUser(user);
    }
}
