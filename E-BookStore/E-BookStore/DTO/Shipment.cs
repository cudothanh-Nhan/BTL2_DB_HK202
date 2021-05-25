using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BookStore.DTO
{
    class Shipment
    {
        private int shipVal;
        private string shipName;

        public int ShipVal { get => shipVal; set => shipVal = value; }
        public string ShipName { get => shipName; set => shipName = value; }
    }
}
