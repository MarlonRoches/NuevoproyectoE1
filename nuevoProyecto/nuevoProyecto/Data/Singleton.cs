using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nuevoProyecto.Models;
using System.IO;
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
        public List<Global> pruema = new List<Global>();


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
       
        }
        #endregion

        #region Listo
        public void Creat_Table(string llave, Dictionary<string, string> Variables)
        {   //Agregar Arbol B
            var Nuevo = new ArbolB();
            Nuevo.Variables = Variables;
            DiB.Add(llave, Nuevo);
            //Agregar Arbol B+
            var ListaNueva = new List<Global>();
            DiBPlus.Add(llave, ListaNueva);
        }
        public Dictionary<string, string> SplitCreate(string Texto)
        {
            var Diccionario = new Dictionary<string, string>();
            //separar en dos vectores
            var auxvector = Texto.Split(',');
            for (int i = 0; i < auxvector.Length ; i++)
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
        public void Insert_Into(Global Objeto)
        {

            try
            {//Push Arbol
                DiB[Objeto.Tabla].Insertar(DiB[Objeto.Tabla].Raiz, Objeto);
                //Push B+
                DiBPlus[Objeto.Tabla].Add(Objeto);
            }
            catch (Exception)
            {
                //Push Arbol
                var Nuevo = new ArbolB();
                DiB.Add(Objeto.Tabla, Nuevo);
                DiB[Objeto.Tabla].Insertar(DiB[Objeto.Tabla].Raiz, Objeto);
                //Push ArbolB+
                var listanueva = new List<Global>();
                DiBPlus.Add(Objeto.Tabla, listanueva);
                DiBPlus[Objeto.Tabla].Add(Objeto);

            }
        }
        internal Global LlenarObjeto(string Variables, string Valores, string Tabla)
        {
            var Objeto = new Global(); int n = 0;
            //split-------------------------------------------------------
            #region Split
            Variables = Variables.Substring(1, Variables.Length - 1);
            var arrayLlaves = Variables.Split(','); var ULTIMAPOS = arrayLlaves[arrayLlaves.Length - 1].Substring(0, arrayLlaves[arrayLlaves.Length - 1].Length - 1);
            arrayLlaves[arrayLlaves.Length - 1] = ULTIMAPOS;
            Valores = Valores.Substring(1, Valores.Length - 1);
            var arrayDatos = Valores.Split(',');
            for (int i = 0; i < arrayDatos.Length - 1; i++)
            {
                if (arrayDatos[i].Substring(0, 1) == "'")
                {
                    var aux = arrayDatos[i].Substring(1, arrayDatos[i].Length - 3);
                    arrayDatos[i] = aux;
                    var Thor = arrayLlaves[i - 1];

                }
            }
            ULTIMAPOS = arrayDatos[arrayDatos.Length - 1].Substring(0, arrayDatos[arrayDatos.Length - 1].Length - 1);
            arrayDatos[arrayDatos.Length - 1] = ULTIMAPOS;
            Objeto.Tabla = Tabla;
            var x = arrayDatos[0];
            Objeto.Id = x;
            #endregion

            /// asignar valores
            var Llaves = arrayLlaves;
            var Datos = arrayDatos;
            var diccionario = DiB[Tabla].Diccionario();
            int Contador = 0;

            if (DiBPlus.ContainsKey(Tabla) == true) //validacion de si existe o no
            {
                foreach (var variable in Llaves)
                {
                    var nombre = diccionario[variable];
                    if (diccionario[variable] == "int" ||
                        diccionario[variable] == "INT" ||
                        diccionario[variable] == "Int")
                    {
                        AsignarInt(Objeto, Datos[Contador]);
                    }
                    else if (diccionario[variable] == nombre && nombre != "DATETIME" && nombre != "INT")
                    {
                        AsignarVC(Objeto, Datos[Contador]);
                    }
                    else if (diccionario[variable] == "DateTime" ||
                             diccionario[variable] == "DATETIME" ||
                             diccionario[variable] == "datetime"
                             )
                    {
                        AsignarDT(Objeto, Datos[Contador]);
                    }
                    Contador++;
                }

            }

            return Objeto;
        }
        private void AsignarDT(Global objeto, string dato)
        {
            if (objeto.DT1 == "")
            {
                objeto.DT1 = dato;
            }
            else if (objeto.DT2 == "" && objeto.DT1 != "")
            {
                objeto.DT2 = dato;

            }
            else if (objeto.DT3 == "" && objeto.DT2 != "" && objeto.DT1 != "")
            {
                objeto.DT3 = dato;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        private void AsignarVC(Global objeto, string dato)
        {
            if (objeto.VarChar1 == "")
            {
                objeto.VarChar1 = dato;

            }
            else if (objeto.VarChar2 == "" && objeto.VarChar1 != "")
            {
                objeto.VarChar2 = dato;

            }
            else if (objeto.VarChar3 == "" && objeto.VarChar2 != "" && objeto.VarChar1 != "")
            {
                objeto.VarChar3 = dato;

            }
            else
            {
                throw new NotImplementedException();
            }
        }
        private void AsignarInt(Global objeto, string dato)
        {
            if (objeto.Int1 == "")
            {
                objeto.Int1 = dato;

            }
            else if (objeto.Int2 == "" && objeto.Int1 != "")
            {
                objeto.Int2 = dato;

            }
            else if (objeto.Int3 == "" && objeto.Int2 != "" && objeto.Int1 != "")
            {
                objeto.Int3 = dato;

            }
            else
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        public void Input(string captura)
        {
           
            string[] Arreglo = captura.Split(' ');
            foreach (string Palabra in Arreglo)
            {
                #region Listos             
                 string Concatenada = Palabra + " " + Arreglo[1];
                if (Concatenada == PalabrasCustom[4])// create Table
                {
                    try
                    {
                        var ubicacion1 = captura.IndexOf('(');

                        captura = captura.Remove(0, ubicacion1 + 1);
                        var ubicacion2 = captura.LastIndexOf(')');
                        captura = captura.Substring(0, ubicacion2);


                        Creat_Table(Arreglo[2], SplitCreate(captura));

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }// Creat Table  ------------------ LISTO -----------------------------
                if (Concatenada == PalabrasCustom[5])
                {
                    try
                    {
                        DiB.Remove(Arreglo[2]);
                        DiBPlus.Remove(Arreglo[2]);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }// Drop Table   ------------------ LISTO -----------------------------
                if (Concatenada == PalabrasCustom[6]) //Insert 
                {
                    string Key = Arreglo[2];//llave para el diccionario
                     Global Nuevo = LlenarObjeto(Arreglo[3], Arreglo[5], Key);
                    try
                    {
                        
                       Insert_Into(Nuevo);

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                } //Insert Into  ------------------ LISTO -----------------------------

                #endregion
                if (Palabra == PalabrasCustom[0])
                {
                   // try
                    //{
                        var split = Arreglo;
                        switch (split[1])
                        {
                            case "*": // * PENIENTE

                                if (split[split.Length - 4] == "where" || split[split.Length - 4] == "WHERE" || split[split.Length - 4] == "Where")
                                {

                                }
                                else
                                {

                                }
                                break;
                            default: // CAMPO
                                if (split[split.Length-4]=="where" || split[split.Length - 4] == "WHERE" || split[split.Length - 4] == "Where")
                                {//where
                                    var VarialesSelect = captura;
                                    var index1 = VarialesSelect.IndexOf(" ");
                                    var index2 = VarialesSelect.IndexOf("From"); //cortar hasta el from
                                    var indexigual = VarialesSelect.IndexOf("Where");
                                    VarialesSelect = VarialesSelect.Substring(index1+1,index2-8);
                                    var wheredato = captura;
                                    var where = wheredato.Substring(indexigual, wheredato.Length);///obtener despues del where
                                    var CampoABuscar = 0;
                                    

                                }
                                else//simple
                                {
                                }
                                break;
                        }
                   // }
                    //catch (Exception)
                    //{

                        //throw;
                   // }
                }// Select PENDIENTE
                if (Concatenada == PalabrasCustom[2])
                {

                    // Delete_From(arreglo[2], int.Parse(arreglo[arreglo.Length - 2]));

                }//Delete From <Tabla> Where Id//Aplicar delete del arbol PENDIENTE
              
                break;
            }
        }
        public void LlenarPalabrasReservadas(string operacion)
        {
            StreamReader File = new StreamReader(operacion);
            var linea = File.ReadLine();
            int n = 0;
            while (linea != null)
            {
                string[] Aux = linea.Split(',');
                PalabrasReservadas[n] = Aux[0];//pre coma
                PalabrasCustom[n] = Aux[1];//post coma
                n++;
                linea = File.ReadLine();
            }
        }


        #region SelectMetodos
        //Casos Select
        public void Star() //*
        {

        }
        public void StarWhere()//*Where
        {

        }

        public void Data()//campos
        {

        }
        public void DataWhere()//campos*
        {

        }
        #endregion

    }
}