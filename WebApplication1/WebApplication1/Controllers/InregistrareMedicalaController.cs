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
    public class InregistrareMedicalaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public InregistrareMedicalaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select * from
                            dbo.InregistrareMedicala
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
        public JsonResult Post(InregistrareMedicala ing)
        {
            string query = @"
                           insert into dbo.InregistrareMedicala
                            (Data, Diagnostic, Tratament, MedicamentePrescrise)
                           values (@Data,@Diagnostic,@Tratament,@MedicamentePrescrise)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VeterinaryClinicAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Data", ing.Data);
                    myCommand.Parameters.AddWithValue("@Diagnostic", ing.Diagnostic);
                    myCommand.Parameters.AddWithValue("@Tratament", ing.Tratament);
                    myCommand.Parameters.AddWithValue("@MedicamentePrescrise", ing.MedicamentePrescrise);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(InregistrareMedicala ing)
        {
            string query = @"
                           update dbo.InregistrareMedicala
                           set Data = @Data,
                               Diagnostic = @Diagnostic,
                               Tratament = @Tratament,
                               MedicamentePrescrise = @MedicamentePrescrise
                            where IDInregistrare=@IDInregistrare
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VeterinaryClinicAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IDInregistrare", ing.IDInregistrare);
                    myCommand.Parameters.AddWithValue("@Data", ing.Data);
                    myCommand.Parameters.AddWithValue("@Diagnostic", ing.Diagnostic);
                    myCommand.Parameters.AddWithValue("@Tratament", ing.Tratament);
                    myCommand.Parameters.AddWithValue("@MedicamentePrescrise", ing.MedicamentePrescrise);
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
                           delete from dbo.InregistrareMedicala
                            where IDInregistrare=@IDInregistrare
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VeterinaryClinicAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IDInregistrare", id);

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
