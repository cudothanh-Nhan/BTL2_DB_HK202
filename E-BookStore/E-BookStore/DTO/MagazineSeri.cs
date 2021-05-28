using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BookStore.DTO
{
    class MagazineSeri
    {
        private int id;
        private string publisher;
        private string name;
        public MagazineSeri()
        {

        }
        public MagazineSeri(int id, string publisher, string name)
        {
            this.id = id;
            this.publisher = publisher;
            this.name = name;
        }

        public int Id { get => id; set => id = value; }
        public string Publisher { get => publisher; set => publisher = value; }
        public string Name { get => name; set => name = value; }
    }
}
