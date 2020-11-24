using System;
using System.Collections.Generic;
using System.Text;

namespace Library.models
{
    public class Rating
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int TaskID { get; set; }
        public DateTime Date { get; set; }
        public int rating { get; set; }
        public string Message { get; set; }
    }
}
