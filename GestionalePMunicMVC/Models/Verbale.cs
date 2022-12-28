using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestionalePMunicMVC.Models
{
    public class Verbale
    {
        public int IdVerbale { get; set; }
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime DataViolazione { get; set; }
        public string IndirizzoViolazione { get; set; }
        public string NomeAgente { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataVerbale { get; set; }
        public double Importo { get; set; }
        public int Punti { get; set; }
        public int IdAnagrafica { get; set; }
        public int IdViolazione{ get; set; }

        public static void NuovoVerbale(Verbale v)
        {
            SqlConnection c = Shared.GetConnectionDb();
            c.Open();
            SqlCommand command = new SqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "AggiungiVerbale";

            command.Parameters.AddWithValue("DataViolazione", v.DataViolazione);
            command.Parameters.AddWithValue("IndirizzoViolazione", v.IndirizzoViolazione);
            command.Parameters.AddWithValue("Nominativo_Agente", v.NomeAgente);
            command.Parameters.AddWithValue("DataTrascrizioneVerbale",v.DataVerbale);
            command.Parameters.AddWithValue("Importo", v.Importo);
            command.Parameters.AddWithValue("DecurtamentoPunti", v.Punti);
            command.Parameters.AddWithValue("idanagrafica", v.IdAnagrafica);
            command.Parameters.AddWithValue("idviolazione",v.IdViolazione);

            command.Connection = c;
            command.ExecuteNonQuery();

            c.Close();

        }
    }
}