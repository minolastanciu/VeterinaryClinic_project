using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DataBase
{
    public class Animale
    {
        public int IDAnimal { get; set; }
        public int IDStapan { get; set; }
        public string Nume { get; set; } = "NUME";
        public string Specie { get; set; } = "SPECIE";
        public DateTime DataNasterii { get; set; }
        public string Rasa { get; set; } = "RASA";
    }
}
