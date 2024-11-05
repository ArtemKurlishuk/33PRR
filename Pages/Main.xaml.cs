using ChatStudents_Kurlishuk.Classes.Common;
using ChatStudents_Kurlishuk.Classes;
using ChatStudents_Kurlishuk.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Linq;


namespace ChatStudents_Kurlishuk.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Users SelectedUser = null;
        public UsersContext usersContext = new UsersContext();
        public MessagesContext messagesContext = new MessagesContext();
        /// <summary>
        /// Таймер для обновления сообщений
        /// </summary>
        public DispatcherTimer Timer = new DispatcherTimer() { Interval = new System.TimeSpan(0, 0, 3) };
        public TimeSpan time = new TimeSpan(0, 0, 0, 30);
        public int count = 0;

        public Main()
        {
            InitializeComponent();
            LoadUsers();
            // Подписываемся на событие выполнения
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        public void LoadUsers()
        {
            ParentUsers.Children.Clear();
            usersContext = new UsersContext();
            foreach (Users user in usersContext.Users)
            {
                if (user.Id != MainWindow.Instance.LoginUser.Id)
                {
                    try
                    {
                        var UserLastMessage = messagesContext.Messages.Where(x =>
                        (x.UserFrom == user.Id && x.UserTo == MainWindow.Instance.LoginUser.Id) ||
                        (x.UserFrom == MainWindow.Instance.LoginUser.Id && x.UserTo == user.Id)).OrderBy(x => x.Id).Last().Message;
                        user.LastMessage = UserLastMessage;
                    }
                    catch { user.LastMessage = "..."; }
                    ParentUsers.Children.Add(new Items.User(user, this));
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Если выбран пользовательский диалог
            if (SelectedUser != null)
            {
                // Обновляем сообщения
                SelectUser(SelectedUser);
            }
            if (time > TimeSpan.Zero)
            {
                time = time - TimeSpan.FromSeconds(3);
            }
            else
            {
                MainWindow.Instance.LoginUser.Status = false;
                usersContext.Users.Where(x => x.Id == MainWindow.Instance.LoginUser.Id).First().Status = false;
                usersContext.SaveChanges();
            }
            LoadUsers();
        }

        public void SelectUser(Users User)
        {
            SelectedUser = User;
            Chat.Visibility = Visibility.Visible;
            imgUser.Source = BitmapFromArrayByte.LoadImage(User.Photo);
            FIO.Content = User.FIO();
            ParentMasseges.Children.Clear();
            // Перебираем сообщения, которые:
            // отправил выбранный пользователь авторизованному
            // или отправил авторизованный пользователь выбранному
            // сортируем по ID
            foreach (Messages Message in messagesContext.Messages.Where(x =>
                (x.UserFrom == User.Id && x.UserTo == MainWindow.Instance.LoginUser.Id) ||
                (x.UserFrom == MainWindow.Instance.LoginUser.Id && x.UserTo == User.Id)).OrderBy(x => x.Id))
            {
                ParentMasseges.Children.Add(new Items.Message(Message, usersContext.Users.Where(x => x.Id == Message.UserFrom).First()));
            }
        }

        private void Send(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Создаём сообщение, где мы - отправитель, а получатель - выбранный диалог
                Messages message = new Messages(
                    MainWindow.Instance.LoginUser.Id,
                    SelectedUser.Id,
                    Message.Text
                    );
                // Заносим в контекст
                messagesContext.Add(message);
                messagesContext.SaveChanges();
                ParentMasseges.Children.Add(new Items.Message(message, MainWindow.Instance.LoginUser));
                Message.Text = "";
                MainWindow.Instance.LoginUser.Status = true;
                usersContext.Users.Where(x => x.Id == MainWindow.Instance.LoginUser.Id).First().Status = true;
                usersContext.SaveChanges(); 
                LoadUsers();
                time += TimeSpan.FromSeconds(30);   
            }
        }
    }
}
