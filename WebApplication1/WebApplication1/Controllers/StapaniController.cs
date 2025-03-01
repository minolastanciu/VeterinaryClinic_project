using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using WebApplication1.DataBase;

[Route("api/[controller]")]
[ApiController]
public class StapaniController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public StapaniController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public JsonResult Get()
    {
        string query = "SELECT * FROM dbo.Stapani";
        using (SqlConnection myCon = new SqlConnection(_configuration.GetConnectionString("VeterinaryClinicAppCon")))
        {
            myCon.Open();
            using (SqlCommand myCommand = new SqlCommand(query, myCon))
            {
                SqlDataReader reader = myCommand.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                reader.Close();
                return new JsonResult(table);
            }
        }
    }

    [HttpPost]
    public JsonResult Post(Stapani stapan)
    {
        string query = "INSERT INTO dbo.Stapani (Nume, Prenume, Telefon, Email) VALUES (@Nume, @Prenume, @Telefon, @Email)";
        using (SqlConnection myCon = new SqlConnection(_configuration.GetConnectionString("VeterinaryClinicAppCon")))
        {
            myCon.Open();
            using (SqlCommand myCommand = new SqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@Nume", stapan.Nume);
                myCommand.Parameters.AddWithValue("@Prenume", stapan.Prenume);
                myCommand.Parameters.AddWithValue("@Telefon", stapan.Telefon);
                myCommand.Parameters.AddWithValue("@Email", stapan.Email);
                myCommand.ExecuteNonQuery();
            }
        }
        return new JsonResult("Added Successfully");
    }

    [HttpPut("{id}")]
    public JsonResult Put(int id, [FromBody] Stapani stapan)
    {
        string query = "UPDATE dbo.Stapani SET Nume=@Nume, Prenume=@Prenume, Telefon=@Telefon, Email=@Email WHERE IDStapan=@IDStapan";
        using (SqlConnection myCon = new SqlConnection(_configuration.GetConnectionString("VeterinaryClinicAppCon")))
        {
            myCon.Open();
            using (SqlCommand myCommand = new SqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@IDStapan", id);
                myCommand.Parameters.AddWithValue("@Nume", stapan.Nume);
                myCommand.Parameters.AddWithValue("@Prenume", stapan.Prenume);
                myCommand.Parameters.AddWithValue("@Telefon", stapan.Telefon);
                myCommand.Parameters.AddWithValue("@Email", stapan.Email);
                myCommand.ExecuteNonQuery();
            }
        }
        return new JsonResult("Updated Successfully");
    }

    [HttpDelete("{id}")]
    public JsonResult Delete(int id)
    {
        string query = "DELETE FROM dbo.Stapani WHERE IDStapan=@IDStapan";
        using (SqlConnection myCon = new SqlConnection(_configuration.GetConnectionString("VeterinaryClinicAppCon")))
        {
            myCon.Open();
            using (SqlCommand myCommand = new SqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@IDStapan", id);
                myCommand.ExecuteNonQuery();
            }
        }
        return new JsonResult("Deleted Successfully");
    }
}
