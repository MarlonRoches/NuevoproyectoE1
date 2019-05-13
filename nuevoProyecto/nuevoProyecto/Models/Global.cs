using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nuevoProyecto.Models
{
    public class Global
    {
        public string Id { get; set; }
        public string Tabla { get; set; }
        public Dictionary<string, string> Variables = new Dictionary<string, string>();
        public string Int1 { get; set; }
        public string Int2 { get; set; }
        public string Int3 { get; set; }
        public string VarChar1 { get; set; }
        public string VarChar2 { get; set; }
        public string VarChar3 { get; set; }
        public string DT1 { get; set; }
        public string DT2 { get; set; }
        public string DT3 { get; set; }

       public Global()
        {
            Id = "";
            Tabla = "";
            Int1 = "";
            Int2 ="";
            Int3 = "";
            VarChar1 ="";
            VarChar2= "";
            VarChar3 ="";
            DT1="";
            DT2 ="";
            DT3 ="";
            Variables = new Dictionary<string, string>();
        }



        public int CompareTo(object obj)
        {
            var comparable = (Global)obj;
            return Id.CompareTo(comparable.Id);
        }


    }
}