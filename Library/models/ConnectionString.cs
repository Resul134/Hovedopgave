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
            ConnectionStr = "Server=tcp:simonshndb.database.windows.net,1433;Initial Catalog=WorkioDB;Persist Security Info=False;User ID=Grethe;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }
    }
}
