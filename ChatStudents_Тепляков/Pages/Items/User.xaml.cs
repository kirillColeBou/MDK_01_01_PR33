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
        Users user;
        Main main;

        public User(Users user, Main main)
        {
            InitializeComponent();
            itemUser = this;
            this.user = user;
            this.main = main;
            UserOnline();
            LoadLastMessage();
            imgUser.Source = BitmapFromArrayByte.LoadImage(user.Photo);
            FIO.Content = user.ToFIO();
        }

        public void UserOnline()
        {
            var lastUserMessage = messagesContext.Messages.Where(x => x.UserTo == MainWindow.Instance.LoginUser.Id).OrderByDescending(x => x.DateSending).FirstOrDefault();
            if (lastUserMessage != null)
                if (lastUserMessage.DateSending.AddMinutes(5) >= DateTime.Now)
                    imgOnline.Visibility = Visibility.Visible;
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
