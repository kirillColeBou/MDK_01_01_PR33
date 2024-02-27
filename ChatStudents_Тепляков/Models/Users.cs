using System;
using System.Collections.Generic;
using System.Text;

namespace ChatStudents_Тепляков.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public byte[] Photo { get; set; }
        public DateTime Online { get; set; }

        public Users(string Lastname, string Firstname, string Surname, byte[] Photo, DateTime Online)
        {
            this.Lastname = Lastname;
            this.Firstname = Firstname;
            this.Surname = Surname;
            this.Photo = Photo;
            this.Online = Online;
        }

        public string ToFIO()
        {
            return $"{Lastname} {Firstname} {Surname}";
        }
    }
}
