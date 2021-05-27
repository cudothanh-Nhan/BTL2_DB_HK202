using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BookStore.BLL
{
    class SqlConnectBLL
    {
        public string host;
        public int port;
        public string database;
        public string username;
        public string password;

        public SqlConnectBLL()
        {
            this.host = "localhost";
            this.port = 3306;
            this.database = "db";
            this.username = "root";
            this.password = "anhentai";
        }
    }
}
