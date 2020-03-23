/*****************************************************************************************************
 *    Grupo Curso C# (lunes 6PM 10PM): Genesis, Jose, Gabriel y Eddy.
 *    Marzo 20, 2020.
 *    Proyecto Final: Pagina Internet Banking por Consola.
 *   
 *    Descripcion:
 *    Este proyecto maneja tres clientes de un internet banking, inicializados como "Juan", "Maria" y
 *    "Pedro", con sus respectivas claves y sus cuentas con valor cero, manejando los procesos de
 *    Depositos, Retiros y Transferencias. Ya que es un internet banking inusual (con depositos y
 *    retiros), lo manejamos como tal, permitiendo que el usuario cambie de cliente cuando quiera, y
 *    pueda realizar los depositos iniciales que le paresca, para luego realizar retiros, y ademas, 
 *    las transferencias entre ellos, y cuando lo desee puede salir del sistema.
 *    
 *****************************************************************************************************/
using System;

namespace ProjectoBanco
{
     public class Banco
     {
        // Inicializacion de atributos para los tres clientes con acceso
        private readonly string[] _codigos = { "01", "02", "03" };
        private readonly string[] _usuarios = { "Juan", "Maria", "Pedro" };
        public double[] valores = { 0.00, 0.00, 0.00 };

        // Declaracion de variables de captura de los datos de alcance global
        public string clave;
        public string nombre;
        public double monto;
        public int indice;

        // Propiedades de campos privados
        public string Codigos { get; set; }
        public string Usuarios { get; set; }

        public void Entrar()  // Metodo para validar entrada al internet banking
        {
            int op = 1;  // Contador para controlar que pase de 3 oportunudades de ingresar
            bool result = false;  // Inicializar variable que valida el do - while
            
            Console.Clear();
            Console.WriteLine("\n\tIngreso a la pagina -- Digite su Usuario");
            Console.WriteLine("\n\t   Solo permite tres intentos.");
            Console.WriteLine("\n_________________________________________________________");

            // Captura de los datos de Cliente
            do
            {
                if (op <= 3)
                {
                     Banco b2 = new Banco();  // Instanciacion de objeto de la clase Banco
                     Console.Write("\nUsuario: ");
                     nombre = Console.ReadLine();
                     Console.Write("\nClave: ");
                     clave = Console.ReadLine();

                     for (int i = 0; i < b2._codigos.Length; i++)  // Buche for para recorrer el arreglo Codigo
                     {
                         if (nombre == b2._usuarios[i] && clave == b2._codigos[i]) // si es verdadero el registro existe
                         {
                            indice = i; // se guarda el valor del indice correspondiente al cliente en el arreglo
                            i = b2._codigos.Length; // se le asigna el ultimo valor al indice para salir del for
                            Console.Write("\nAcceso Correcto.. ");
                            Console.ReadKey();
                            Seleccionar(); // Si coincide el usuario capturado se llama al metodo con el menu de opciones
                            result = true; // se le asigna true a la variable de control para finalizar el Do - While
                         }
                     }
                     if (result == false) // si la variable continua siendo false es por que el usuario no existe
                     { 
                        Console.WriteLine("\nUsuario y/o Clave ** Incorrecto **");
                        Console.ReadKey();
                     }
                }
                else
                {
                     Console.WriteLine("\n\t\t\tAgoto el numero de intentos.");
                     Console.ReadKey();
                     result = true; // Se le asigna true a la variable result ya que agoto las 3 oportunidades
                }
                op++;
            } while (result == false);
        }
        public void Seleccionar()   // Metodo que presenta el menu de opciones a seleccionar en la pagina internet banking
        {
            string seleccion;  // Declaracion de variable local para controlar el D0 - While de selecciones
            string opciones =
  @"    _____________________________________________________       
   |                       M E N U                       |
   |_____________________________________________________|
   |   d  -->   Depositos                                |
   |                                                     |
   |   r  -->   Retiros                                  |
   |                                                     | 
   |   t  -->   Trasferencias terceros                   |
   |                                                     |
   |   c  -->   Consultar otro cliente                   |
   |                                                     |
   |   f  -->   Finalizar                                |
   |_____________________________________________________|";
            do
            {
                Console.Clear(); // Limpiar la pantalla para desplegar el menu
                Console.WriteLine("\n ");
                Console.WriteLine(opciones);  // Esta variable ya inicializada Muestra el grafico con el menu de opciones
                Console.Write("\n\tElija su opcion: ");
                seleccion = Console.ReadLine();  
                switch (seleccion)  // Utilizamos la condicional Switch para determinar la opcion del menu
                {
                     case "d":
                        {
                            Console.Clear();
                            Console.WriteLine("\n\t\tDepositos");
                            Console.WriteLine("__________________________________________");
                            Console.Write("\nIngrese valor deposito: ");
                            monto = Double.Parse(Console.ReadLine());
                            Depositar(monto);     // llama al metodo depositar con el parametro del valor a depositar
                            Console.WriteLine($"\n\n\nEl cliente: {this._usuarios[indice]} --> Tiene disponible: RD$  {this.valores[indice]}");
                        }
                        break;
                    case "r":
                        {
                            Console.Clear();
                            string op2;  // Declaracion varible local que controla la confirmacion de los retiros de la cuenta del cliente
                            Console.WriteLine("\n\t\tRetiros");
                            Console.WriteLine("__________________________________________");
                            Console.WriteLine($"\nCantidad disponible en su cuenta: RD$ {this.valores[indice]}");
                            Console.Write("\nIngrese retiro: ");
                            monto = Double.Parse(Console.ReadLine());
                            if (monto <= this.valores[indice])  // Condicional IF que evalua que el retiro no sea mayor al balance disponible
                            {
                                // Muestra primero el monto a retirar y el disponible
                                Console.WriteLine($"\nTiene disponible: RD$ {this.valores[indice]}");
                                Console.WriteLine($"\nEl Monto a Retirar es: RD$ {monto}");
                                Console.Write("\nConfirma este Retiro ? [ S/N ]: ");
                                op2 = Console.ReadLine();
                                if (op2 == "S" || op2 == "s")
                                {
                                    Retirar(monto);  // si esta de acuerdo con el retiro llama al metodo correspondiente con el parametro monto
                                    Console.WriteLine($"\n\n\nEl cliente: {this._usuarios[indice]} --> Tiene disponible: RD$  {this.valores[indice]}");
                                }
                                else if (op2 == "N" || op2 == "n")
                                {
                                    Console.Write("\nRetiro Cancelado... ");  // si declina la operacion vuelve al menu
                                    Console.ReadKey();
                                }
                                    else
                                    {
                                        Console.Write("\nDebe elegir s/n... ");  // Si op2 es incorrecta anula la operacion
                                        Console.ReadKey();
                                    }
                            }
                            else
                            {
                                Console.Write("\nNO tiene fondos suficientes.. "); // Declina la operacion si el monto es superior al disponible
                                Console.ReadKey();
                            }
                            Console.Clear();
                        }
                        break;
                    case "t":
                        {
                            Console.Clear();
                            Console.WriteLine("\n\t\tTransferencias a terceros");
                            Console.WriteLine($"\nCantidad disponible: RD$ {this.valores[indice]}");
                            Console.WriteLine("__________________________________________");
                            Console.Write("\n\nIngrese cantidad: ");
                            monto = double.Parse(Console.ReadLine());
                            if (monto <= this.valores[indice])  // Condicional IF que evalua que la transferencia no sea mayor al balance disponible
                            {
                                string op2;
                                Console.Write("\nIngrese nombre de beneficiario: ");
                                string benef = Console.ReadLine();
                                for (int f = 0; f < this._usuarios.Length; f++)  // Ciclo for que busca el cliente que recibira la transferencia
                                {
                                    if (this._usuarios[f] == benef)
                                    {
                                        Console.WriteLine($"\nUsted tiene disponible: RD$ {this.valores[indice]}");
                                        Console.WriteLine($"\nBeneficiario: {this._usuarios[f]}  <==>  Monto a transferir: RD$ {monto}");
                                        Console.Write("\nConfirma este pago ? [ S/N ]: ");
                                        op2 = Console.ReadLine();
                                        if (op2 == "S" || op2 == "s")
                                        {
                                            this.valores[f] += monto;
                                            Transferir(monto);
                                            Console.WriteLine($"\n\n\nEl cliente: {this._usuarios[indice]} --> Tiene disponible: RD$  {this.valores[indice]}");
                                        }
                                        else if (op2 == "N" || op2 == "n")
                                        {
                                            Console.Write("\nPago Cancelado... "); // si no esta de acuerdo con la transferencia la puede cancelar
                                            Console.ReadKey();
                                        }
                                        else
                                        {
                                            Console.Write("\nDebe elegir s/n... ");
                                            Console.ReadKey();
                                        }
                                    }
                                    else if (f == this._usuarios.Length) // Determinar si el cliente beneficiario existe
                                    {
                                        // Si no encuentra el nombre en el arreglo, cuando termina el for regresa al menu
                                        Console.WriteLine("\nNombre Beneficiario no Existe"); 
                                        Console.Write("Presione cualquier tecla... ");
                                        Console.ReadKey();
                                    }
                                }
                            }
                            else
                            {
                                // si no cumple esta condicion, no se puede realizar la operacion
                                Console.Write("\nCantidad NO disponible.. ");
                                Console.ReadKey();
                                Console.Clear();
                            }
                            Console.Clear();
                        }
                        break;
                    case "c":
                        {
                            // Esta opcion llama al metodo entrar para ingresar otro de los 3 clientes permitidos
                            Console.Write("\nCambiar de cliente.. ");
                            Console.ReadKey();
                            Entrar();
                        }
                        break;
                    case "f":
                        {
                            string op3; // variable para determinar si el usuario desea salir del programa
                            Console.Write("\nDesea Salir [ S/N ] ==> ");
                            op3 = Console.ReadLine();
                            if (op3 == "S" || op3 == "s")
                            {
                                Console.Write("\nSalida del Internet Banking .. ");
                                Console.ReadKey();
                            }
                            else if (op3 == "N" || op3 == "n")
                                {
                                     Console.Write("Regresar al menu.. ");
                                     Console.ReadKey();
                                     seleccion = " "; // Determina que regrese al menu de opciones
                                }
                                else
                                {
                                    Console.Write("\nDebe elegir s/n...");
                                    Console.ReadKey();
                                }
                            Console.Clear();
                        }
                        break;
                    default:
                        {
                            Console.Write("\nOpcion Incorrecta.. ");
                            Console.ReadKey();
                        }
                        break;
                }
                
            } while (seleccion != "f");
        }
        public double Depositar(double v)
        { 
            this.valores[indice] += v;  // Suma a la cuenta del cliente el valor depositado
            Console.WriteLine("\nTiene en su cuenta: RD$ " + this.valores[indice]);
            Console.ReadKey();
            Console.Clear();
            return this.valores[indice]; 
        }
        public double Retirar(double v)
        {
            this.valores[indice] -= v;  // resta el valor retirado de la cuenta del cliente, luego de confirmado
            Console.WriteLine("\nTiene en su cuenta: RD$ " + this.valores[indice]);
            Console.WriteLine("__________________________________________");
            Console.Write("\nRetiro confirmado... ");
            Console.ReadKey();
            return this.valores[indice];
        }
        public double Transferir(double v)
        {
            this.valores[indice] -= v;  // resta el valor a transferir de la cuenta del cliente, luego de confirmado
            Console.WriteLine("\nTiene en su cuenta: RD$ " + this.valores[indice]);
            Console.WriteLine("__________________________________________");
            Console.Write("\nPago realizado .. ");
            Console.ReadKey();
            return this.valores[indice];
        }
        static void Main()
        {
            Banco b2 = new Banco();  // Instaciacion de objeto para llamar al metodo que inicia el proceso
            b2.Entrar();
        }
     }
}
