using System;
using System.Collections.Generic;
using System.Text;

namespace ChatStudents_Тепляков.Models
{
    public class Messages
    {
        public int Id { get; set; }
        public int UserFrom { get; set; }
        public int UserTo { get; set; }
        public string Message { get; set; }
        public DateTime DateSending { get; set; }

        public Messages(int UserFrom, int UserTo, string Message, DateTime DateSending)
        {
            this.UserFrom = UserFrom;
            this.UserTo = UserTo;
            this.Message = Message;
            this.DateSending = DateSending;
        }
    }
}
