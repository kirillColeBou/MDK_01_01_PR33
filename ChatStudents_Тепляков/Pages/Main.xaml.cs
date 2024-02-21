using ChatStudents_Тепляков.Classes;
using ChatStudents_Тепляков.Classes.Common;
using ChatStudents_Тепляков.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace ChatStudents_Тепляков.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Users SelectedUser = null;
        public UsersContext usersContext = new UsersContext();
        public MessagesContext messagesContext = new MessagesContext();
        public DispatcherTimer Timer = new DispatcherTimer() { Interval = new TimeSpan(0,0,3) };
        public Main()
        {
            InitializeComponent();
            LoadUsers();
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        public void LoadUsers()
        {
            foreach(Users user in usersContext.Users)
            {
                if (user.Id != MainWindow.Instance.LoginUser.Id)
                    parentUsers.Children.Add(new Pages.Items.User(user, this));
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (SelectedUser != null) SelectUser(SelectedUser);
        }

        public void SelectUser(Users User)
        {
            SelectedUser = User;
            Chat.Visibility = Visibility.Visible;
            imgUser.Source = BitmapFromArrayByte.LoadImage(User.Photo);
            FIO.Content = User.ToFIO();
            parentMessages.Children.Clear();
            var userMessages = messagesContext.Messages.Where(x => x.UserFrom == MainWindow.Instance.LoginUser.Id && x.UserTo == User.Id).OrderByDescending(x => x.DateSending).FirstOrDefault();
            if (userMessages != null) parentMessages.Children.Add(new Pages.Items.Message(userMessages, usersContext.Users.Where(x => x.Id == userMessages.UserFrom).First()));
        }

        private void Send(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Messages message = new Messages(MainWindow.Instance.LoginUser.Id, SelectedUser.Id, Message.Text, DateTime.Now);
                messagesContext.Messages.Add(message);
                messagesContext.SaveChanges();
                parentMessages.Children.Add(new Pages.Items.Message(message, MainWindow.Instance.LoginUser));
                Message.Text = "";
            }
        }
    }
}
