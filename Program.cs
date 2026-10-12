using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casosemana9
{
    internal class Program
    {
        static int max_emp = 50;
        static string[] nombres = new string[max_emp];
        static string[] correo_emp = new string[max_emp];
        static string rutaArchivo = "empleado_datos.txt";
        static int cant_emp = 0;
        static public void Titulo()
        {
            Console.WriteLine("**********************");
            Console.WriteLine("SISTEMA DE EMPLEADOS");
            Console.WriteLine("**********************");
        }
        static public void Registro_empleado()
        {
            Console.WriteLine("**********************");
            Console.WriteLine("Registrar Empleados");
            Console.WriteLine("**********************");
            if (cant_emp >= max_emp)
            {
                Console.WriteLine("Alcanzo la capacidad maxima de datos");
                return;
            }
            Console.WriteLine("Ingresar Nombres y Apellidos: ");
            string nombres_emp = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(nombres_emp))
            {
                Console.WriteLine("Error, El nombre no puede estar vacio");
                return;
            }
            string nombres_Mayus = nombres_emp.ToUpper();
            string[] partes = nombres_Mayus.Split('.');
            string primer_nombre;
            if (partes.Length > 0)
                primer_nombre = partes[0];
            else
                primer_nombre = "usuario";
            string correo = $"{primer_nombre.ToLower()}@upn.edu.pe";
            nombres[cant_emp] = nombres_Mayus;
            correo_emp[cant_emp] = correo;
            cant_emp++;
        }
        static public void buscar_empleado()
        {
            Console.WriteLine("**********************");
            Console.WriteLine("BUSCAR EMPLEADO");
            Console.WriteLine("**********************");
            if (cant_emp == 0) 
            {
                Console.WriteLine("No hay registro de empleados");
                return;
            }
            Console.Write("ingresar nombre o apellido a buscar: ");
            string buscar = Console.ReadLine()?.Trim().ToUpper();
            if (string.IsNullOrEmpty(buscar))
                return;
            bool encontrado = false;
            for (int i = 0; i < cant_emp; i++) 
            {
                if (nombres[i].Contains(buscar)) 
                { 
                    Console.WriteLine("Encontrado:Nombres"+ nombres[i]+ "Correo"+ correo_emp[i]);
                    encontrado = true;
                }
            }
            if (!encontrado) 
            {
                Console.WriteLine("No se encuentran coincidencias en empleado");
            }
        }
        static public void ordenar() 
        {
            if (cant_emp <= 1)
            {
                Console.WriteLine("No hay suficientes registros para ordenar");
                return;
            }
            for (int i = 0;1< cant_emp-1;i++) 
            {
                for (int j = 0;j< cant_emp - 1 - i; j++) 
                {
                    if (string.Compare(nombres[j], nombres[j + 1]) > 0)
                    {
                        string temp_nom = nombres[j];
                        nombres[j] = nombres[j + 1];
                        nombres[j+1] = temp_nom;

                        string temp_correo = correo_emp[j];
                        correo_emp[j]= correo_emp[j+1];
                        correo_emp[j + 1] = temp_correo;
                    }
                }
            }
        }

        static public void mostrar() 
        {
            Console.WriteLine("************************");
            Console.WriteLine("LISTADO DE EMPLEADOS");
            Console.WriteLine("************************");
            if (cant_emp == 0) 
            { 
                Console.WriteLine("No hay registros para mostrar");
                return;
            }
            Console.WriteLine("nombres y apellidos\t\tCorreo");
            for (int i = 0; i< cant_emp; i++) 
            {
                Console.WriteLine(nombres[i] + "\t\t" + correo_emp[i]);
            }
        }
        static public void guardar_datos_archivo() 
        {
            Console.WriteLine("*******************************");
            Console.WriteLine("GUARDAR DATOS EN ARCHIVO");
            Console.WriteLine("*******************************");
            if (cant_emp == 0) 
            {
                Console.WriteLine("No hay registros para guardar");
                return;
            }
            using (StreamWriter escribir = new StreamWriter("Empleados.txt")) 
            {
                for(int i=0;1 < cant_emp; i++)
                {
                    escribir.WriteLine(nombres[i] + "," + correo_emp[i]);
                }
            }
            Console.WriteLine(cant_emp + "empleados guardados en archivos");
        }
        static public void leer_datos_archivos()
        {
            Console.WriteLine("******************************");
            Console.WriteLine("LEER DATOS DE ARCHIVOS");
            Console.WriteLine("******************************");
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine("No existe el archivo de empleados");
                return;
            }
            int num_linea = 1;
            using (StreamReader lector=new StreamReader(rutaArchivo))
            {
                string linea;
                while ((linea = lector.ReadLine())!= null) {
                    string[] partes = linea.Split(',');
                    if (partes.Length == 2)
                    {
                        Console.WriteLine(num_linea + ".-" + "nombres:" + partes[0] + "correo: " + partes[1]);
                        num_linea++;
                    }
                }
              
            }

        }
        static public void cargar_datos_archivo()
        {
            Console.WriteLine("******************************");
            Console.WriteLine("CARGAR DATOS DE ARCHIVOS");
            Console.WriteLine("******************************");
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine("No existe el archivo empleados");
                return;
            }
            string[] lineas = File.ReadAllLines(rutaArchivo);
            for(int i=0;i<lineas.Length && i < max_emp; i++)
            {
                string[] partes = lineas[i].Split(',');
                if (partes.Length == 2)
                {
                    nombres[i] = partes[0];
                    correo_emp[i] = partes[1];
                    cant_emp++;
                }
            }
        }
        static void Main(string[] args)
        {
            int opc=0;
            Titulo();
            while (opc!= 8) 
            {
                Console.Clear();
                Console.WriteLine("[1]Registrar Empleado");
                Console.WriteLine("[2]Buscar Empleado");
                Console.WriteLine("[3]ordenar alfabeticamente (a-z)");
                Console.WriteLine("[4]Mostrar Empleados");
                Console.WriteLine("[5]Guardar datos de archivo");
                Console.WriteLine("[6]Leer datos de archivo");
                Console.WriteLine("[7]Cargar datos de archivo");
                Console.WriteLine("[8]Salir");
                Console.Write("Ingresar opcion: ");
                if(!int.TryParse(Console.ReadLine(), out opc)) 
                {
                    Console.WriteLine("ingrese un valor numerico");
                    continue;
                }
                switch(opc) 
                {
                    case 1:
                        Registro_empleado(); break;
                    case 2:
                        buscar_empleado(); break;
                    case 3:
                        ordenar();break;
                    case 4:
                        mostrar(); break;
                    case 5:
                        guardar_datos_archivo(); break;
                    case 6:
                        leer_datos_archivos(); break;
                    case 7:
                        cargar_datos_archivo(); break; 
                    case 8:
                            Console.WriteLine("Gracias por usar el sistema..!!");
                            break;
                    default:
                        Console.WriteLine("Opcion incorrecta, elija del 1-5");
                        break;
                    }
                Console.ReadKey();
            }
        }
    }
}
