using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DataBase
{
    public class Stapani
    {
        public int IDStapan {  get; set; }
        public string Nume { get; set; } = "NUME";
        public string Prenume { get; set; } = "PRENUME";
        public int Telefon { get; set; }
        public string Email { get; set; } = "EMAIL";
    }
}
