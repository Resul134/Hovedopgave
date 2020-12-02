using System;
using System.Collections.Generic;
using System.Text;

namespace Library.models
{
    public class ConnectionString
    {
        public string ConnectionStr { get; set; }
        
        public ConnectionString()
        {
            ConnectionStr = "Data Source=DESKTOP-O7CILOQ\\SQLEXPRESS01;Initial Catalog=hovedopgave;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
    }
}
