using ChatStudents_Тепляков.Classes;
using ChatStudents_Тепляков.Classes.Common;
using ChatStudents_Тепляков.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Linq;

namespace ChatStudents_Тепляков.Pages.Items
{
    /// <summary>
    /// Логика взаимодействия для User.xaml
    /// </summary>
    public partial class User : UserControl
    {
        MessagesContext messagesContext = new MessagesContext();
        Users user;
        Main main;

        public User(Users user, Main main)
        {
            InitializeComponent();
            this.user = user;
            this.main = main;
            imgUser.Source = BitmapFromArrayByte.LoadImage(user.Photo);
            FIO.Content = user.ToFIO();
            UserOnline();
        }

        public void UserOnline()
        {
            var lastUserMessage = messagesContext.Messages.Where(x => x.UserTo == MainWindow.Instance.LoginUser.Id).OrderByDescending(x => x.DateSending).FirstOrDefault();
            if (lastUserMessage != null)
                if (lastUserMessage.DateSending.AddMinutes(5) >= DateTime.Now)
                    imgOnline.Visibility = Visibility.Visible;
        }

        private void SelectChat(object sender, MouseButtonEventArgs e) => main.SelectUser(user);
    }
}
