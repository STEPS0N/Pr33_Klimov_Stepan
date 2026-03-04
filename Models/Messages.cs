using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatStudents_Klimov.Models
{
    public class Messages
    {
        public int Id { get; set; }
        public int UserForm { get; set; }
        public int UserTo { get; set; }
        public string Message { get; set; }

        public Messages(int UserForm, int UserTo, string Message)
        {
            this.UserForm = UserForm;
            this.UserTo = UserTo;
            this.Message = Message;
        }
    }
}
