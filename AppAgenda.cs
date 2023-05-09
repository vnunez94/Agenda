using System;
using System.Collections.Generic;

namespace AppAgenda
{
    public class AppAgenda
    {
        private readonly IPersistencia _storage;

        public AppAgenda()
        {
            // Obtener la ruta del directorio donde se encuentra la aplicación en ejecución
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            // Utilice SQLiteStorage o FileStorage según sus necesidades
            _storage = new SQLiteStorage($"Data Source={appPath}contactos.db");
            //_storage = new FileStorage($"{appPath}contactos.txt");
        }

        public void Iniciar()
        {
            char seleccion;
            do
            {
                Console.Clear();
                Console.WriteLine("--------Bienvenido a la Agenda--------");
                Console.WriteLine("Seleccione una función");
                Console.WriteLine("1) Listar contactos");
                Console.WriteLine("2) Agregar contactos");
                Console.WriteLine("3) Eliminar contactos");
                Console.WriteLine("4) Salir");

                seleccion = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (seleccion)
                {
                    case '1':
                        Listar(_storage.GetContactos());
                        break;
                    case '2':
                        Agregar();
                        break;
                    case '3':
                        Eliminar();
                        break;
                    case '4':
                        Environment.Exit(0);
                        break;
                }
            } while (seleccion != '4');
        }

        private void Listar(List<Contacto> contactos)
        {
            Console.WriteLine("\n---LISTA DE CONTACTOS---\n");

            if (contactos.Count == 0)
            {
                Console.WriteLine("No hay contactos");
            }
            else
            {
                for (int i = 0; i < contactos.Count; i++)
                {
                    Console.WriteLine("------------------------------------------------");
                    Console.WriteLine($"No.      : {contactos[i].Id}");
                    Console.WriteLine($"Nombre   : {contactos[i].Nombre}");
                    Console.WriteLine($"Telefono : {contactos[i].Telefono}");
                    Console.WriteLine("------------------------------------------------");
                }

                Console.WriteLine($"\n\nTotal contactos: {contactos.Count}\n");
            }

            Console.ReadLine();
        }

        private void Agregar()
        {
            Console.WriteLine("Introduzca un nombre:");
            string nombre = Console.ReadLine();
            Console.WriteLine("Introduzca un teléfono:");
            string telefono = Console.ReadLine();
            Contacto contacto = new Contacto(nombre, telefono);

            _storage.GuardarContacto(contacto);
        }

        private void Eliminar()
        {
            List<Contacto> contactos = _storage.GetContactos();
            Listar(contactos);
      
            Console.WriteLine("¿Qué contacto desea eliminar?");
            int idx;  
            int.TryParse(Console.ReadLine(), out  idx);
       
            Contacto contacto = contactos.Find(c => c.Id == idx);
            if (contacto != null)
            {
                _storage.EliminarContacto(contacto.Id);
                Console.WriteLine("Contacto eliminado.");
            }
            else
            {
                Console.WriteLine("Id inválido.");
            }
            Console.ReadLine();
        }
}
}
