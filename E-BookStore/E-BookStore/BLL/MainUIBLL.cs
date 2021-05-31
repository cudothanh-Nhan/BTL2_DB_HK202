using E_BookStore.DAO;
using E_BookStore.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BookStore.BLL
{
    class MainUIBLL
    {
        MainUIDAO dao;
        public MainUIBLL()
        {
            SqlConnectBLL sql = new SqlConnectBLL();
            dao = new MainUIDAO(sql.host, sql.port, sql.database, sql.username, sql.password);
        }
        public List<Book> getallBookUI()
        {
            return dao.getallBookUI();
        }
        public List<Magazine> getallMagaUI()
        {
            return dao.getallMagaUI();
        }
    }
}
