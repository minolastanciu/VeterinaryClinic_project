using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using WebApplication1.DataBase;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimaleController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AnimaleController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select * from
                            dbo.Animale
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VeterinaryClinicAppCon");

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {

                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Animale ani)
        {
            string query = @"
                           insert into dbo.Animale
                            (Nume,Specie,DataNasterii,Rasa)
                           values (@Nume,@Specie,@DataNasterii,@Rasa)
                            ";

            //DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VeterinaryClinicAppCon");
            //SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Nume", ani.Nume);
                    myCommand.Parameters.AddWithValue("@Specie", ani.Specie);
                    myCommand.Parameters.AddWithValue("@DataNasterii", ani.DataNasterii);
                    myCommand.Parameters.AddWithValue("@Rasa", ani.Rasa);
                    myCommand.ExecuteNonQuery();  // Folosește ExecuteNonQuery pentru INSERT
                   // myReader = myCommand.ExecuteReader();
                    //table.Load(myReader);
                    //myReader.Close();
                    //myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut("{id}")]  // Adaugă {id} în ruta
        public JsonResult Put(int id, [FromBody] Animale ani)
        {
            string query = @"
       UPDATE dbo.Animale
       SET Nume = @Nume,
           Specie = @Specie,
           DataNasterii = @DataNasterii,
           Rasa = @Rasa
       WHERE IDAnimal = @IDAnimal";

            using (SqlConnection myCon = new SqlConnection(_configuration.GetConnectionString("VeterinaryClinicAppCon")))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IDAnimal", id);  // Folosește ID-ul din URL
                    myCommand.Parameters.AddWithValue("@Nume", ani.Nume);
                    myCommand.Parameters.AddWithValue("@Specie", ani.Specie);
                    myCommand.Parameters.AddWithValue("@DataNasterii", ani.DataNasterii);
                    myCommand.Parameters.AddWithValue("@Rasa", ani.Rasa);
                    myCommand.ExecuteNonQuery();
                }
            }

            return new JsonResult("Updated Successfully");
        }




        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from dbo.Animale
                            where IDAnimal=@IDAnimal
                            ";

            //DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VeterinaryClinicAppCon");
            //SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IDAnimal", id);
                    myCommand.ExecuteNonQuery();
                    //myReader = myCommand.ExecuteReader();
                    //table.Load(myReader);
                    //myReader.Close();
                   // myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

    }
}
