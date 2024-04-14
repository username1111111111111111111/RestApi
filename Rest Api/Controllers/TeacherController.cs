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
    public class TeacherController : ApiController
    {
        MySqlConnection com = new MySqlConnection(WebConfigurationManager.ConnectionStrings["bağlantı"].ToString());

        [HttpGet]
        public IEnumerable<teacher> ÖğretmenleriListele()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("select * from ögretmen ", com);
            DataTable dt = new DataTable();
            da.Fill(dt);



            List<teacher> Ogretmenler = new List<teacher> { };

            foreach (DataRow row in dt.Rows)
            {
                

                int ögretmenid = Convert.ToInt32(row["ögretmenid"]);
                string ad = (string)row["ad"];

                teacher YeniOgretmen = new teacher();


                YeniOgretmen.Öğretmeninadı = ad;
                YeniOgretmen.Öğretmenid =ögretmenid;

                
                Ogretmenler.Add(YeniOgretmen);
            }

            return Ogretmenler;

        }
        [HttpGet]
        public teacher ÖğretmenBul(int id)
        {
            teacher YeniOgretmen = new teacher();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from ögretmen where ögretmenid ='" + id + "' ", com);
            DataTable dt = new DataTable();
            da.Fill(dt);


            

            foreach (DataRow row in dt.Rows)
            {
                

                int ögretmenid = Convert.ToInt32(row["ögretmenid"]);
                string ad = (string)row["ad"];
                


                YeniOgretmen.Öğretmeninadı = ad;
                YeniOgretmen.Öğretmenid = ögretmenid;

                
            }

            return YeniOgretmen;

        }

       

        [HttpPost]
        public void ÖğretmenEkle([FromBody] teacher ÖğretmenBilgileri)
        {
            com.Open();
            MySqlCommand cmd = new MySqlCommand("insert into ögretmen(ögretmenid,ad) Values('" + ÖğretmenBilgileri.Öğretmenid + "','" + ÖğretmenBilgileri.Öğretmeninadı + "') ", com);
            cmd.ExecuteNonQuery();
            com.Close();
        }

        [HttpPut]
        public void ÖğretmenGüncelle(int id, [FromUri]teacher value)
        {
            com.Open();
            MySqlCommand cmd = new MySqlCommand("update ögretmen set ad=@Ad where ögretmenid = @id ", com);
            cmd.Parameters.AddWithValue("@Ad", value.Öğretmeninadı);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            com.Close();

        }


        [HttpDelete]
        public void ÖğretmenSil(int id)
        {
            com.Open();
            MySqlCommand cmd = new MySqlCommand("delete from ögretmen  where ögretmenid ='" + id + "' ", com);
            cmd.ExecuteNonQuery();
            com.Close();
        }
    }
}