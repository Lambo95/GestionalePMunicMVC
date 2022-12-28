using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionalePMunicMVC.Models
{
    public class Anagrafica
    {
        public int IdAnagrafica { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public string CAP { get; set; }
        public string CF { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataViolazione { get; set; }
        public double Importo { get; set; }
        public int Punti { get; set; }
        public string Descrizione { get; set; }

        public static List<Anagrafica> AnagraficaList() 
        {
            List<Anagrafica> lAnagrafica = new List<Anagrafica>();

            SqlConnection c = Shared.GetConnectionDb();
            c.Open();
            SqlDataReader r = Shared.GetReaderAfterSQL("Select * From Anagrafica", c);

            if(r.HasRows)
            {
                while(r.Read())
                {
                    Anagrafica a = new Anagrafica();
                    a.IdAnagrafica = Convert.ToInt32(r["idanagrafica"]);
                    a.Cognome = r["Cognome"].ToString();
                    a.Nome = r["Nome"].ToString();
                    a.Indirizzo = r["Indirizzo"].ToString();
                    a.Citta = r["Città"].ToString();
                    a.CAP = r["CAP"].ToString();
                    a.CF = r["Cod_Fisc"].ToString();
                    lAnagrafica.Add(a);
                }
            }
            c.Close();
            return lAnagrafica;
        }

        public static void NuovaAnagrafica(Anagrafica a)
        {
            SqlConnection c = Shared.GetConnectionDb();
            c.Open();
            SqlCommand command = new SqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "AggiungiAnagrafica";

            command.Parameters.AddWithValue("Cognome", a.Cognome);
            command.Parameters.AddWithValue("Nome", a.Nome);
            command.Parameters.AddWithValue("Indirizzo", a.Indirizzo);
            command.Parameters.AddWithValue("Città", a.Citta);
            command.Parameters.AddWithValue("CAP", a.CAP);
            command.Parameters.AddWithValue("Cod_Fisc", a.CF);
            command.Connection = c;
            command.ExecuteNonQuery();
           
            c.Close();

        }

        public static List<SelectListItem> AnagraficaListView()
        {
            List<SelectListItem> lselectList = new List<SelectListItem>();

            SqlConnection c = Shared.GetConnectionDb();
            c.Open();
            SqlDataReader r = Shared.GetReaderAfterSQL("Select * From Anagrafica", c);

            if (r.HasRows)
            {

                while (r.Read())
                {
                    SelectListItem s = new SelectListItem
                    {
                        Value = r["idanagrafica"].ToString(),
                        Text = r["Cognome"].ToString() + " " + r["Nome"].ToString(),
                    };

                    lselectList.Add(s);
                }
            }
            c.Close();
            return lselectList;
            ;
        }

        public static Anagrafica GetAnagrafica(int id)
        {
            SqlConnection c = Shared.GetConnectionDb();
            c.Open();
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("idanagrafica", id);
            command.CommandText = "Select * From Anagrafica where idanagrafica = @idanagrafica";
            command.Connection = c;

            SqlDataReader r = command.ExecuteReader();
            Anagrafica a = new Anagrafica();

            if (r.HasRows)
            {
                while (r.Read())
                {
                    
                    a.IdAnagrafica = Convert.ToInt32(r["idanagrafica"]);
                    a.Cognome = r["Cognome"].ToString();
                    a.Nome = r["Nome"].ToString();
                    a.Indirizzo = r["Indirizzo"].ToString();
                    a.Citta = r["Città"].ToString();
                    a.CAP = r["CAP"].ToString();
                    a.CF = r["Cod_Fisc"].ToString();
                }
            }

            c.Close();
            return a;
        }
       

        public static List<Anagrafica> ListVerbaliMaggiori10Punti()
        {
            List<Anagrafica> lAnagrafica = new List<Anagrafica>();

            SqlConnection c = Shared.GetConnectionDb();
            c.Open();

            SqlDataReader r = Shared.GetReaderAfterSQL("select Cognome, Nome, Indirizzo, DataViolazione, Importo, DecurtamentoPunti from Anagrafica inner join verbale on Anagrafica.idanagrafica = verbale.idanagrafica where DecurtamentoPunti>10", c);

            if (r.HasRows)
            {
                while (r.Read())
                {
                    Anagrafica a = new Anagrafica();
                    a.Cognome = r["Cognome"].ToString();
                    a.Nome = r["Nome"].ToString();
                    a.Indirizzo = r["Indirizzo"].ToString();
                    a.DataViolazione = Convert.ToDateTime(r["DataViolazione"].ToString());
                    a.Importo = Convert.ToDouble(r["Importo"]);
                    a.Punti = Convert.ToInt32(r["DecurtamentoPunti"]);
                    lAnagrafica.Add(a);
                }
            }

            c.Close();
            return lAnagrafica;

        }

        public static List<Anagrafica> _ListTrasgressoriMaggioriDi400Euro()
        {
            List<Anagrafica> lAnagrafica = new List<Anagrafica>();

            SqlConnection c = Shared.GetConnectionDb();
            c.Open();

            SqlDataReader r = Shared.GetReaderAfterSQL("select Cognome, Nome, Indirizzo, DataViolazione, Importo, DecurtamentoPunti from Anagrafica inner join verbale on Anagrafica.idanagrafica = verbale.idanagrafica where Importo > 400", c);

            if (r.HasRows)
            {
                while (r.Read())
                {
                    Anagrafica a = new Anagrafica();
                    a.Cognome = r["Cognome"].ToString();
                    a.Nome = r["Nome"].ToString();
                    a.Indirizzo = r["Indirizzo"].ToString();
                    a.DataViolazione = Convert.ToDateTime(r["DataViolazione"].ToString());
                    a.Importo = Convert.ToDouble(r["Importo"]);
                    a.Punti = Convert.ToInt32(r["DecurtamentoPunti"]);
                    lAnagrafica.Add(a);
                }
            }

            c.Close();
            return lAnagrafica;

        }

        public static List<Anagrafica> SearchMode(string ricerca)
        {
            List<Anagrafica> lAnagrafica = new List<Anagrafica>();

            SqlConnection c = Shared.GetConnectionDb();
            c.Open();

            SqlDataReader r = Shared.GetReaderAfterSQL("select Cognome, Nome, Indirizzo, Città, CAP, Cod_Fisc, DataViolazione, Importo, DecurtamentoPunti from Anagrafica inner join verbale on Anagrafica.idanagrafica = verbale.idanagrafica where (Nome like '%"+ ricerca + "%' or Cognome like '%" + ricerca + "%' or Città like '%" + ricerca + "%' or Importo like '%" + ricerca + "%' or DecurtamentoPunti like '%" + ricerca + "%')" , c);

            if (r.HasRows)
            {
                while (r.Read())
                {
                    Anagrafica a = new Anagrafica();
                    a.Cognome = r["Cognome"].ToString();
                    a.Nome = r["Nome"].ToString();
                    a.Indirizzo = r["Indirizzo"].ToString();
                    a.Citta = r["Città"].ToString();
                    a.CAP = r["CAP"].ToString();
                    a.CF = r["Cod_Fisc"].ToString();
                    a.DataViolazione = Convert.ToDateTime(r["DataViolazione"].ToString());
                    a.Importo = Convert.ToDouble(r["Importo"]);
                    a.Punti = Convert.ToInt32(r["DecurtamentoPunti"]);
                    lAnagrafica.Add(a);
                }
            }

            c.Close();
            return lAnagrafica;

        }

        //public static Anagrafica GetAnagraficaViolazioni(int id)
        //{
        //    SqlConnection c = Shared.GetConnectionDb();
        //    c.Open();
        //    SqlCommand command = new SqlCommand();
        //    command.Parameters.AddWithValue("idanagrafica", id);
        //    command.CommandText = "select Cognome, Nome, Indirizzo, Città, descrizione from Anagrafica inner join Tipo_Violazione on Anagrafica.idanagrafica = Tipo_Violazioni.idanagrafica ";
        //    command.Connection = c;

        //    SqlDataReader r = command.ExecuteReader();
        //    Anagrafica a = new Anagrafica();

        //    if (r.HasRows)
        //    {
        //        while (r.Read())
        //        {

        //            a.IdAnagrafica = Convert.ToInt32(r["idanagrafica"]);
        //            a.Cognome = r["Cognome"].ToString();
        //            a.Nome = r["Nome"].ToString();
        //            a.Indirizzo = r["Indirizzo"].ToString();
        //            a.Citta = r["Città"].ToString();
        //           a.Descrizione = r["descrizione"].ToString();
        //        }
        //    }

        //    c.Close();
        //    return a;
        //}
    }
}