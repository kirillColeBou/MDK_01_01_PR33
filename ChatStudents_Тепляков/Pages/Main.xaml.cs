using ChatStudents_Тепляков.Classes;
using ChatStudents_Тепляков.Classes.Common;
using ChatStudents_Тепляков.Models;
using ChatStudents_Тепляков.Pages.Items;
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
            foreach (Users user in usersContext.Users)
            {
                if (user.Id != MainWindow.Instance.LoginUser.Id)
                    parentUsers.Children.Add(new Pages.Items.User(user, this));
            }
        }

        public void UpdateUsers()
        {
            parentUsers.Children.Clear();
            LoadUsers();
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
            foreach (Messages Message in messagesContext.Messages.Where(x => (x.UserFrom == User.Id && x.UserTo == MainWindow.Instance.LoginUser.Id) || (x.UserFrom == MainWindow.Instance.LoginUser.Id && x.UserTo == User.Id)))
                parentMessages.Children.Add(new Pages.Items.Message(Message, usersContext.Users.Where(x => x.Id == Message.UserFrom).First()));
        }

        private void Send(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Messages message = new Messages(MainWindow.Instance.LoginUser.Id, SelectedUser.Id, Message.Text, DateTime.Now);
                messagesContext.Messages.Add(message);
                messagesContext.SaveChanges();
                parentMessages.Children.Add(new Pages.Items.Message(message, MainWindow.Instance.LoginUser));
                Items.User.itemUser.LoadLastMessage();
                UpdateUsers();
                Message.Text = "";
            }
        }
    }
}
