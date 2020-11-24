using System;
using System.Collections.Generic;
using System.Text;

namespace Library.models
{
    public class Comment
    {
        public int ID { get; set; }
        public int TaskID { get; set; }
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public String Message { get; set; }
    }
}
