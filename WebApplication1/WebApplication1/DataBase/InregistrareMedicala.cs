using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DataBase
{
    public class InregistrareMedicala
    {
        public int IDInregistrare {  get; set; }
        public int IDProgramare { get; set; }
        public DateTime Data {  get; set; }
        public string Diagnostic { get; set; } = "DIAGNOSTIC";
        public string Tratament { get; set; } = "TRATAMENT";
        public string MedicamentePrescrise { get; set; } = "MEDICAMENTE";
    }
}
