using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BookStore.DTO
{
    class Author
    {
        private int id;
        private string name;
        private DateTime dob;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public DateTime Dob { get => dob; set => dob = value; }
        Author(int id, string name, DateTime dob)
        {
            this.Id = id;
            this.Name = name;
            this.Dob = dob;
        }
    }
}
