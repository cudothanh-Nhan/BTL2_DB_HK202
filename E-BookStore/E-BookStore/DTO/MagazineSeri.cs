using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BookStore.DTO
{
    class MagazineSeri
    {
        private int Id;
        private string publisher;
        private string name;
        public MagazineSeri()
        {

        }
        public MagazineSeri(int id, string publisher, string name)
        {
            Id1 = id;
            this.Publisher = publisher;
            this.Name = name;
        }

        public int Id1 { get => Id; set => Id = value; }
        public string Publisher { get => publisher; set => publisher = value; }
        public string Name { get => name; set => name = value; }
    }
}
