using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nuevoProyecto.Models;
namespace nuevoProyecto.Data
{
    public class Singleton
    {
        #region Variables Singleton
        private static Singleton _instance = null;
        public static Singleton Instance
        {
            get
            {
                if (_instance == null) _instance = new Singleton();
                return _instance;
            }
        }
        public Dictionary<string, ArbolB> DiB = new Dictionary<string, ArbolB>();
        public Dictionary<string, List<Global>> DiBPlus = new Dictionary<string, List<Global>>();
        public bool Ini = true;
        public LinkedList<Global> ListaVehiculos = new LinkedList<Global>();
        public List<Global> SelectLista = new List<Global>();
        public string[] PalabrasReservadas = new string[9];

        public string[] PalabrasCustom = new string[9];
        public void Palabras_Reservadas()
        {
            PalabrasReservadas[0] = "Select";
            PalabrasReservadas[1] = "From";
            PalabrasReservadas[2] = "Delete";
            PalabrasReservadas[3] = "Where";
            PalabrasReservadas[4] = "Create Table";
            PalabrasReservadas[5] = "Drop Table";
            PalabrasReservadas[6] = "Insert Into";
            PalabrasReservadas[7] = "Value";
            PalabrasReservadas[8] = "Go";
            PalabrasCustom[0] = "Select";
            PalabrasCustom[1] = "From";
            PalabrasCustom[2] = "Delete";
            PalabrasCustom[3] = "Where";
            PalabrasCustom[4] = "Create Table";
            PalabrasCustom[5] = "Drop Table";
            PalabrasCustom[6] = "Insert Into";
            PalabrasCustom[7] = "Value";
            PalabrasCustom[8] = "Go";
        }
        #endregion
        public void Input(string Linea)
        {
            string captura = Linea;
            string[] arreglo = captura.Split(' ');
            foreach (string Palabra in arreglo)
            {
                string Concatenada = Palabra + " " + arreglo[1];
                
                    if (Palabra == PalabrasCustom[0])
                    {
                        try
                        {
                            if (arreglo[arreglo.Length - 1] == PalabrasCustom[8])
                            {

                                string Task = "";
                                string Tabla = "";
                                string Id = "";
                                int n = 1;
                                //Selec-> From
                                while (arreglo[n] != PalabrasCustom[1])
                                {
                                    Task = Task + " " + arreglo[n];
                                    n++;
                                }
                                n++;
                                //From-> where
                                Tabla = arreglo[n];
                                n = n + 2;
                                //Where-> Go
                                try
                                {

                                    Id = arreglo[n + 2];

                                }
                                catch (Exception)
                                {

                                    throw;
                                }
                                SelectLista = new List<Global>();
                                //Select(Task, Tabla, Id, DiBPlus[Tabla]);
                            }
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }// Select
                    if (Concatenada == PalabrasCustom[2])
                    {

                       // Delete_From(arreglo[2], int.Parse(arreglo[arreglo.Length - 2]));

                    }//Delete From <Tabla> Where Id//Aplicar delete del arbol
                    if (Concatenada == PalabrasCustom[4])// create Table
                    {
                        try
                        {
                        var ubicacion1 = captura.IndexOf('(');
                        
                        captura = captura.Remove(0, ubicacion1+1);
                        var ubicacion2 = captura.LastIndexOf(')');
                        captura = captura.Substring(0, ubicacion2);


                            Creat_Table(arreglo[2], SplitCreate(captura));
                            
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }// Creat Table 
                    if (Concatenada == PalabrasCustom[5])
                    {
                        try
                        {
                            DiB.Remove(arreglo[2]);
                            DiBPlus.Remove(arreglo[2]);
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }// Drop Table
                    if (Concatenada == PalabrasCustom[6]) //Insert 
                    {
                        string Key = arreglo[2];//llave para el diccionario
                       // Global Nuevo = LlenarObjeto(arreglo[3], arreglo[5], Key);
                        try
                        {

                          //  Insert_Into(Nuevo);

                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    } //Insert Into
            }
        }

        public void Creat_Table(string llave,Dictionary<string,string> Variables )
        {   //Agregar Arbol B
            var Nuevo = new ArbolB();
            Nuevo.Variables = Variables;
            DiB.Add(llave, Nuevo);
            //Agregar Arbol B+
            var ListaNueva = new List<Global>();
            DiBPlus.Add(llave, ListaNueva);
        }
        public Dictionary<string,string> SplitCreate(string Texto)
        {
            var Diccionario = new Dictionary<string, string>();
            //separar en dos vectores
            var auxvector = Texto.Split(',');
            for (int i = 0; i < auxvector.Length-1; i++)
            {
                var AuxSpEspacio = auxvector[i].Split(' ');
                Diccionario.Add(AuxSpEspacio[0], AuxSpEspacio[1]);
            }

            return Diccionario;//devolver diccionario
        }

        public List<TreeView> TreeView()
        {
            var Devolver = new List<TreeView>();
            foreach (var item in DiB)
            {
                var Nuevo = new TreeView()
                {
                    Tabla = item.Key
                };
                Devolver.Add(Nuevo);
            }
            return Devolver;
        }
    }
}