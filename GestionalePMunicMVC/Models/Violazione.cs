using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionalePMunicMVC.Models
{
    public class Violazione
    {
        public int IdViolazione { get; set; }
        public string Descrizione { get; set; }

        public static List<Violazione> ViolazioneList()
        {
            List<Violazione> lViolazione = new List<Violazione>();

            SqlConnection c = Shared.GetConnectionDb();
            c.Open();
            SqlDataReader r = Shared.GetReaderAfterSQL("Select * From Tipo_Violazione", c);

            if (r.HasRows)
            {
                while (r.Read())
                {
                    Violazione v = new Violazione();
                    v.IdViolazione = Convert.ToInt32(r["idviolazione"]);
                    v.Descrizione = r["descrizione"].ToString();
                    lViolazione.Add(v);
                }
            }
            c.Close();
            return lViolazione;
            ;
        }

        public static List<SelectListItem> ViolazioneListView()
        {
            List<SelectListItem> lselectList = new List<SelectListItem>();

            SqlConnection c = Shared.GetConnectionDb();
            c.Open();
            SqlDataReader r = Shared.GetReaderAfterSQL("Select * From Tipo_Violazione", c);

            if (r.HasRows)
            {

                while (r.Read())
                {
                    SelectListItem s = new SelectListItem
                    {
                        Value = r["idviolazione"].ToString(),
                        Text = r["descrizione"].ToString()
                    };

                lselectList.Add(s);
                }
            }
            c.Close();
            return lselectList;
            ;
        }

        public static void NuovaViolazione(Violazione v)
        {
            SqlConnection c = Shared.GetConnectionDb();
            c.Open();
            SqlCommand command = new SqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "AggiungiViolazione";

            command.Parameters.AddWithValue("descrizione", v.Descrizione);

            command.Connection = c;
            command.ExecuteNonQuery();

            c.Close();
        }
    }
}