using System;
using System.Collections.Generic;
using System.Text;

namespace ChatStudents_Kurlishuk.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public byte[] Photo { get; set; }
        public bool Status { get; set; }
        public string LastMessage { get; set; }

        public Users(string Lastname, string Firstname, string Surname, byte[] Photo, bool Status)
        {
            this.Lastname = Lastname;
            this.Firstname = Firstname;
            this.Surname = Surname;
            this.Photo = Photo;
            this.Status = Status;
        }

        public string FIO()
        {
            return $"{Lastname} {Firstname} {Surname}";
        }
    }
}
