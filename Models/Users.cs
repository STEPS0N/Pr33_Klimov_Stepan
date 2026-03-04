using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatStudents_Klimov.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Surename { get; set; }
        public byte[] Photo { get; set; }

        public Users(string Lastname, string Firstname, string Surename, byte[] Photo)
        {
            this.Lastname = Lastname;
            this.Firstname = Firstname;
            this.Surename = Surename;
            this.Photo = Photo;
        }

        public string ToFIO()
        {
            return $"{Lastname} {Firstname} {Surename}";
        }
    }
}
