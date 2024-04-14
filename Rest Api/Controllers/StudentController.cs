using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Rest_Api.Models;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.OleDb;
using System.Web.Configuration;

namespace Rest_Api.Controllers
{
    public class StudentController : ApiController
    {
        MySqlConnection com = new MySqlConnection(WebConfigurationManager.ConnectionStrings["bağlantı"].ToString());
        
        [HttpGet]
        public IEnumerable<model> ÖğrencileriListele()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("select * from ögrenci ", com);
            DataTable dt = new DataTable();
            da.Fill(dt);



            List<model> Ogrenciler = new List<model> { };

            foreach (DataRow row in dt.Rows)
            {
                

                int OgrenciNo = Convert.ToInt32(row["OgrenciNo"]);
                string ad  = (string)row["ad"];

                model YeniOgrenci = new model();


                YeniOgrenci.Ad =ad;
                YeniOgrenci.OgrenciNo =OgrenciNo;

                
                Ogrenciler.Add(YeniOgrenci);
            }

            return Ogrenciler;

        }

        [HttpGet]
        public model ÖğrenciBul(int id)
        {
            model YeniÖgrenci = new model();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from ögrenci where OgrenciNo ='" + id + "' ", com);
            DataTable dt = new DataTable();
            da.Fill(dt);



            foreach (DataRow row in dt.Rows)
            {
                string ad = (string)row["ad"];
                int OgrenciNo = Convert.ToInt32(row["OgrenciNo"]);




                YeniÖgrenci.Ad =ad;
                YeniÖgrenci.OgrenciNo =OgrenciNo;

            }

            return YeniÖgrenci;
        }

        [HttpPost]
        public void ÖğrenciEkle([FromBody] model ÖğrenciBilgileri)
        {
            com.Open();
            MySqlCommand cmd = new MySqlCommand("insert into ögrenci(OgrenciNo,ad) Values('" + ÖğrenciBilgileri.OgrenciNo + "','" + ÖğrenciBilgileri.Ad + "') ", com);
            cmd.ExecuteNonQuery();
            com.Close();
        }

        [HttpPut]
        public void ÖğrenciGüncelle(int id, [FromUri]model value)
        {
            com.Open();
            MySqlCommand cmd = new MySqlCommand("update ögrenci set ad=@Ad where OgrenciNo = @OgrenciNo ", com);
            cmd.Parameters.AddWithValue("@Ad", value.Ad);
            cmd.Parameters.AddWithValue("@OgrenciNo", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            com.Close();

        }

        [HttpDelete]
        public void ÖğrenciSil(int id)
        {
            com.Open();
            MySqlCommand cmd = new MySqlCommand("delete from ögrenci  where OgrenciNo ='" + id + "' ", com);
            cmd.ExecuteNonQuery();
            com.Close();
        }
    }
}