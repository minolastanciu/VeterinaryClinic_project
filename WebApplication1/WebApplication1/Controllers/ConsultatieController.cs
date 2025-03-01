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
    public class ConsultatieController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ConsultatieController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select * from
                            dbo.Consultatie
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
        public JsonResult Post(Consultatie con)
        {
            string query = @"
                           insert into dbo.Consultatie
                            (Data)
                           values (@Data)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VeterinaryClinicAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Data", con.Data);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Consultatie con)
        {
            string query = @"
                           update dbo.Consultatie
                           set Data = @Data,
                            where IDProgramare=@IDProgramare
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VeterinaryClinicAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IDProgramare", con.IDProgramare);
                    myCommand.Parameters.AddWithValue("@Data", con.Data);
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
                           delete from dbo.Consultatie
                            where IDProgramare=@IDProgramare
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VeterinaryClinicAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IDProgramare", id);

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
