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
    public class LessonController : ApiController
    {
        MySqlConnection com = new MySqlConnection(WebConfigurationManager.ConnectionStrings["bağlantı"].ToString());

        [HttpGet]
        public IEnumerable<lesson> DersleriiListele()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("select * from ders ", com);
            DataTable dt = new DataTable();
            da.Fill(dt);



            List<lesson> Dersler = new List<lesson> { };

            foreach (DataRow row in dt.Rows)
            {
                

                int dersid = Convert.ToInt32(row["dersid"]);
                string kod = (string)row["kod"];
                string ad = (string)row["ad"];

               lesson YeniDers = new lesson();


                YeniDers.ad = ad;
                YeniDers.dersid = dersid;

                YeniDers.kod =kod;

                
                Dersler.Add(YeniDers);
            }

            return Dersler;

        }
        [HttpGet]
        public lesson DersBul(int id)
        {
            lesson YeniDers = new lesson();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from ders where dersid ='" + id + "' ", com);
            DataTable dt = new DataTable();
            da.Fill(dt);


            

            foreach (DataRow row in dt.Rows)
            {
                

                int dersid = Convert.ToInt32(row["dersid"]);
                string kod = (string)row["kod"];
                string ad = (string)row["ad"];
                


                YeniDers.ad = ad;
                YeniDers.dersid = dersid;

                YeniDers.kod = kod;
                
            }

            return YeniDers;

        }

      

        [HttpPost]
        public void DersEkle([FromBody] lesson DersBilgileri)
        {
            com.Open();
            MySqlCommand cmd = new MySqlCommand("insert into ders(dersid,kod,ad) Values('" + DersBilgileri.dersid + "','" + DersBilgileri.kod + "','" + DersBilgileri.ad + "') ", com);
            cmd.ExecuteNonQuery();
            com.Close();
        }

        [HttpPut]
        public void DersGüncelle(int id, [FromUri]lesson value)
        {
            com.Open();
            MySqlCommand cmd = new MySqlCommand("update ders set ad=@Ad,kod=@kod where dersid = @dersid ", com);
            cmd.Parameters.AddWithValue("@kod", value.kod);
            cmd.Parameters.AddWithValue("@Ad", value.ad);
            cmd.Parameters.AddWithValue("@dersid", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            com.Close();

        }

        [HttpDelete]
        public void DersSil(int id)
        {
            com.Open();
            MySqlCommand cmd = new MySqlCommand("delete from ders  where dersid ='" + id + "' ", com);
            cmd.ExecuteNonQuery();
            com.Close();
        }
    }
}