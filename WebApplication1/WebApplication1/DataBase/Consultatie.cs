using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DataBase
{
    public class Consultatie
    {
        public int IDProgramare { get; set; }
        public int IDAnimal { get; set; }
        public int IDMedic { get; set; }
        public DateTime Data { get; set; }
    }
}
