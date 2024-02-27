using ChatStudents_Тепляков.Classes;
using ChatStudents_Тепляков.Classes.Common;
using ChatStudents_Тепляков.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using System.Windows.Media;

namespace ChatStudents_Тепляков.Pages.Items
{
    /// <summary>
    /// Логика взаимодействия для User.xaml
    /// </summary>
    public partial class User : UserControl
    {
        public static User itemUser;
        MessagesContext messagesContext = new MessagesContext();
        UsersContext usersContext = new UsersContext();
        Users user;
        Main main;

        public User(Users user, Main main, bool online)
        {
            InitializeComponent();
            itemUser = this;
            this.user = user;
            this.main = main;
            LoadLastMessage();
            imgUser.Source = BitmapFromArrayByte.LoadImage(user.Photo);
            FIO.Content = user.ToFIO();
            if (online == true) imgOnline.Visibility = Visibility.Visible;
        }

        public void LoadLastMessage()
        {
            var FindlastMessageUser = messagesContext.Messages.Where(x => (x.UserFrom == user.Id && x.UserTo == MainWindow.Instance.LoginUser.Id) || (x.UserFrom == MainWindow.Instance.LoginUser.Id && x.UserTo == user.Id)).OrderByDescending(x => x.DateSending).FirstOrDefault();
            if (FindlastMessageUser != null)
                if (MainWindow.Instance.LoginUser.Id == FindlastMessageUser.UserFrom) lastSendingMessage.Content = "To: " + FindlastMessageUser.Message;
                else lastSendingMessage.Content = "From: " + FindlastMessageUser.Message;
        }

        private void SelectChat(object sender, MouseButtonEventArgs e) => main.SelectUser(user);
    }
}
