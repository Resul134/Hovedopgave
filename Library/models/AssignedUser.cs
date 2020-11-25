using System;
using System.Collections.Generic;
using System.Text;

namespace Library.models
{
    public class AssignedUser
    {
        public int ID { get; set; }
        public int TaskID { get; set; }
        public int UserID { get; set; }
        public bool Accepted { get; set; }
    }
}
