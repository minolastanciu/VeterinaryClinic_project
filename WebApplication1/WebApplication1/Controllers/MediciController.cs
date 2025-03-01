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
    public class MediciController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public MediciController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select * from
                            dbo.Medici
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
        public JsonResult Post(Medici med)
        {
            string query = @"
                           insert into dbo.Medici
                            (Nume, Specializare)
                           values (@Nume,@Specializare)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VeterinaryClinicAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Nume", med.Nume);
                    myCommand.Parameters.AddWithValue("@Specializare", med.Specializare);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Medici med)
        {
            string query = @"
                           update dbo.Medici
                           set Nume = @Nume,
                           set Specializare = @Specializare,
                            where IDMedic=@IDMedic
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VeterinaryClinicAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IDMedic", med.IDMedic);
                    myCommand.Parameters.AddWithValue("@Nume", med.Nume);
                    myCommand.Parameters.AddWithValue("@Specializare", med.Specializare);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from dbo.Medici
                            where IDMedic=@IDMedic
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VeterinaryClinicAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IDMedic", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

    }
}
