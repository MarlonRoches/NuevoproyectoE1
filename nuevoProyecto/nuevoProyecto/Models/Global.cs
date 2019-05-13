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
        string[] Nombres = new string[9];
        public string Int1 { get; set; }
        public string Int2 { get; set; }
        public string Int3 { get; set; }
        public string S1 { get; set; }
        public string S2 { get; set; }
        public string S3 { get; set; }
        public string DT1 { get; set; }
        public string DT2 { get; set; }
        public string DT3 { get; set; }

        public int CompareTo(object obj)
        {
            var comparable = (Global)obj;
            return Id.CompareTo(comparable.Id);
        }


        public void Asignarvalores(string[] Variables, string[] Datos)
        {

        }
    }
}