using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DataBase
{
    public class Factura
    {
        public int IDFactura { get; set; }
        public int IDInregistrare { get; set; }
        public DateTime Data {  get; set; }
        public string Servicii { get; set; } = "SERVICII";
        public int Suma { get; set; } 
    }
}
