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
            ConnectionStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hovedopgave;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
    }
}
