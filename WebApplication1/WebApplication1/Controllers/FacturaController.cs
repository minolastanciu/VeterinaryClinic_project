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
    public class FacturaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public FacturaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select * from
                            dbo.Factura
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
        public JsonResult Post(Factura fac)
        {
            string query = @"
                           insert into dbo.Factura
                            (Data, Servicii, Suma)
                           values (@Data,@Servicii,@Suma)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VeterinaryClinicAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Data", fac.Data);
                    myCommand.Parameters.AddWithValue("@Servicii", fac.Servicii);
                    myCommand.Parameters.AddWithValue("@Suma", fac.Suma);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Factura fac)
        {
            string query = @"
                           update dbo.Factura
                           set Data = @Data,
                               Servicii = @Servicii,
                               Suma = @Suma,
                            where IDFactura=@IDFactura
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VeterinaryClinicAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IDFactura", fac.IDFactura);
                    myCommand.Parameters.AddWithValue("@Data", fac.Data);
                    myCommand.Parameters.AddWithValue("@Servicii", fac.Servicii);
                    myCommand.Parameters.AddWithValue("@Suma", fac.Suma);
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
                           delete from dbo.Factura
                            where IDFactura=@IDFactura
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VeterinaryClinicAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IDFactura", id);

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
