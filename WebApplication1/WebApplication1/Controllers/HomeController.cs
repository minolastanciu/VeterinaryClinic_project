using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // 1. Informații despre un animal și medicul său după nume (variabil)
        [HttpGet("AnimalMedic")]
        public JsonResult GetAnimalMedic([FromQuery] string numeAnimal)
        {
            string query = @"
                SELECT A.Nume AS NumeAnimal, A.Specie, A.Rasa, A.DataNasterii,
                       M.Nume AS NumeMedic, M.Specializare
                FROM Animale A
                INNER JOIN Consultatie C ON A.IDAnimal = C.IDAnimal
                INNER JOIN Medici M ON C.IDMedic = M.IDMedic
                WHERE A.Nume LIKE '%' + @NumeAnimal + '%'";

            return ExecuteQuery(query, new SqlParameter("@NumeAnimal", numeAnimal));
        }

        // 2. Animale care au primit un medicament (variabil)
        [HttpGet("AnimaleMedicament")]
        public JsonResult GetAnimaleMedicament([FromQuery] string medicament)
        {
            string query = @"
                SELECT A.Nume AS NumeAnimal, A.Specie, A.Rasa, IM.Tratament
                FROM Animale A
                INNER JOIN Consultatie C ON A.IDAnimal = C.IDAnimal
                INNER JOIN InregistrareMedicala IM ON C.IDProgramare = IM.IDProgramare
                WHERE IM.MedicamentePrescrise LIKE '%' + @Medicament + '%'";
            return ExecuteQuery(query, new SqlParameter("@Medicament", medicament));
        }

        // 3. Medici cu cele mai multe animale tratate
        [HttpGet("MediciMaxAnimale")]
        public JsonResult GetMediciMaxAnimale()
        {
            string query = @"
                SELECT M.Nume, M.Specializare, COUNT(DISTINCT A.IDAnimal) AS NumarAnimale
                FROM Medici M
                INNER JOIN Consultatie C ON M.IDMedic = C.IDMedic
                INNER JOIN Animale A ON A.IDAnimal = C.IDAnimal
                GROUP BY M.Nume, M.Specializare
                ORDER BY COUNT(DISTINCT A.IDAnimal) DESC";
            return ExecuteQuery(query);
        }

        // 4. Servicii și diagnostic pentru animale de tip "Caine"
        [HttpGet("ServiciiCaini")]
        public JsonResult GetServiciiCaini()
        {
            string query = @"
                SELECT A.Nume, A.Rasa, F.Servicii, I.Diagnostic
                FROM Animale A
                INNER JOIN Consultatie C ON A.IDAnimal = C.IDAnimal
                INNER JOIN InregistrareMedicala I ON C.IDProgramare = I.IDProgramare
                INNER JOIN Factura F ON F.IDInregistrare = I.IDInregistrare
                WHERE A.Specie = 'Caine'";
            return ExecuteQuery(query);
        }

        // 5. Medici cu cele mai multe programări
        [HttpGet("MediciProgramariDesc")]
        public JsonResult GetMediciProgramariDesc()
        {
            string query = @"
                SELECT M.Nume, M.Specializare, COUNT(*) AS NrProgramari
                FROM Medici M
                JOIN Consultatie C ON M.IDMedic = C.IDMedic
                GROUP BY M.Nume, M.Specializare
                ORDER BY NrProgramari DESC";
            return ExecuteQuery(query);
        }

        // 6. Medici care au prescris "Fizioterapie"
        [HttpGet("MediciTratamentFizioterapie")]
        public JsonResult GetMediciTratamentSomn()
        {
            string query = @"
                SELECT M.Nume, M.Specializare
                FROM Medici M
                JOIN Consultatie C ON M.IDMedic = C.IDMedic
                JOIN InregistrareMedicala I ON C.IDProgramare = I.IDProgramare
                WHERE I.Tratament = 'Fizioterapie'
                ORDER BY M.Nume ASC";
            return ExecuteQuery(query);
        }

        // 7. Medici care nu au consultat animale (complex)
        [HttpGet("MediciFaraConsultatii")]
        public JsonResult GetMediciFaraConsultatii()
        {
            string query = @"
                SELECT M.Nume, M.Specializare
                FROM Medici M
                WHERE NOT EXISTS (
                    SELECT 1 FROM Consultatie C WHERE C.IDMedic = M.IDMedic
                )";
            return ExecuteQuery(query);
        }

        // 8. Diagnostic care apare de X ori (variabil)
        [HttpGet("DiagnosticNumarApariții")]
        public JsonResult GetDiagnosticNumarAparitii([FromQuery] int diagnosticCount)
        {
            string query = @"
                SELECT A.Nume, A.Specie, A.Rasa, I.Diagnostic, I.Tratament
                FROM Animale A
                JOIN Consultatie C ON A.IDAnimal = C.IDAnimal
                JOIN InregistrareMedicala I ON C.IDProgramare = I.IDProgramare
                WHERE I.Diagnostic IN (
                    SELECT I1.Diagnostic
                    FROM InregistrareMedicala I1
                    GROUP BY I1.Diagnostic
                    HAVING COUNT(*) = @DiagnosticCount
                )";
            return ExecuteQuery(query, new SqlParameter("@DiagnosticCount", diagnosticCount));
        }

        // 9. Animale cu mai multe diagnostice (complex)
        [HttpGet("AnimaleMultipleDiagnostice")]
        public JsonResult GetAnimaleMultipleDiagnostice()
        {
            string query = @"
                SELECT A.Nume, A.Specie, A.Rasa, 
                       (SELECT COUNT(*) 
                        FROM InregistrareMedicala I
                        INNER JOIN Consultatie C ON I.IDProgramare = C.IDProgramare
                        WHERE C.IDAnimal = A.IDAnimal) AS NrDiagnostice
                FROM Animale A
                WHERE (SELECT COUNT(*) 
                       FROM InregistrareMedicala I
                       INNER JOIN Consultatie C ON I.IDProgramare = C.IDProgramare
                       WHERE C.IDAnimal = A.IDAnimal) > 2
                ORDER BY NrDiagnostice DESC";
            return ExecuteQuery(query);
        }

        // 10. Animalele fără stăpân (complex, cu subcerere)
        [HttpGet("AnimaleFaraStapan")]
        public JsonResult GetAnimaleFaraStapan()
        {
            string query = @"
        SELECT A.Nume, A.Specie, A.Rasa
        FROM Animale A
        WHERE A.IDStapan IS NULL
        AND NOT EXISTS (
            SELECT 1
            FROM Consultatie C
            WHERE C.IDAnimal = A.IDAnimal
        )
        AND A.IDAnimal IN (
            SELECT DISTINCT IDAnimal
            FROM Animale
            WHERE Specie IN (
                SELECT Specie
                FROM Animale
                GROUP BY Specie
                HAVING COUNT(*) > 1
            )
        )";
            return ExecuteQuery(query);
        }


        // Helper pentru execuția query-urilor
        private JsonResult ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VeterinaryClinicAppCon");

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    if (parameters != null)
                    {
                        myCommand.Parameters.AddRange(parameters);
                    }

                    SqlDataReader myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
    }
}
