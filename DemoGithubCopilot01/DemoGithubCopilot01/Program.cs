// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


// Esta aplicacion tiene el objetivo de pedir los datos de un usuario y mostrarlos en pantalla
// Se pide el nombre, apellido y edad del usuario, y el email 
// Debe validar que el email tenga un formato correcto
// Luego se muestra un mensaje en pantalla con los datos ingresados.
// Ejemplo: "Hola, mi nombre es Juan Perez y tengo 30 años"
// Se debe validar que la edad ingresada sea un número entero mayor a 0
// Si la edad no es válida, se debe mostrar un mensaje de error y pedir nuevamente la edad
// Se debe utilizar un bucle para pedir la edad hasta que sea válida
// Se debe utilizar una función para validar la edad
// Se debe utilizar una función para mostrar el mensaje en pantalla
// Se debe utilizar una función principal para ejecutar el programa
// Se deben utilizar comentarios para explicar el código
// Se deben utilizar nombres de variables y funciones descriptivos
// Se deben utilizar buenas prácticas de programación
// Se debe utilizar el espacio de nombres adecuado

// Función para mostrar el mensaje con los datos ingresados con colores
void MostrarMensaje(string nombre, string apellido, int edad, string email)
{
    // Cambiar el color del texto para el nombre
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write(nombre);

    // Cambiar el color del texto para el apellido
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.Write($" {apellido}");

    // Cambiar el color del texto para la edad
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write($", tengo {edad} años");

    // Cambiar el color del texto para el email
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($" y mi email es {email}.");

    // Restablecer el color predeterminado
    Console.ResetColor();
}

// Función para validar el formato del email
bool ValidarEmail(string email)
{
    // Utilizar una expresión regular para validar el formato del email
    var regex = new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    return regex.IsMatch(email);
}

// Función para pedir una edad válida
int PedirEdadValida()
{
    int edad;
    Console.Write("Ingrese su edad: ");
    // Utilizar un bucle para pedir la edad hasta que sea válida
    while (!int.TryParse(Console.ReadLine(), out edad) || edad <= 0)
    {
        Console.Write("Edad inválida. Ingrese una edad válida (número entero mayor a 0): ");
    }
    return edad;
}

// Función para pedir un dato al usuario
static string PedirDato(string mensaje)
{
    string dato;
    do
    {
        Console.Write(mensaje);
        dato = Console.ReadLine();
    } while (string.IsNullOrWhiteSpace(dato));
    return dato;
}

// Pedir el nombre del usuario
string nombre = PedirDato("Ingrese su nombre: ");
// Pedir el apellido del usuario
string apellido = PedirDato("Ingrese su apellido: ");
// Pedir el email del usuario
string email = PedirDato("Ingrese su email: ");
// Validar el formato del email
while (!ValidarEmail(email))
{
    Console.Write("Email inválido. Ingrese un email válido: ");
    email = Console.ReadLine();
}
// Pedir la edad del usuario
int edad = PedirEdadValida();
// Mostrar el mensaje con los datos ingresados
MostrarMensaje(nombre, apellido, edad, email);

// Fin del programa
// Gracias por usar este programa
// ¡Hasta luego!
// Autor: Pedro Hernández
// Fecha: 2024-06-15
// Versión: 1.0
// Licencia: MIT
// Repositorio:




