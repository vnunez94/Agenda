
namespace AppAgenda
{
    public class Contacto
    {
        private int id;
        private string nombre;
        private string telefono;

        public string Nombre { 
                    get { return nombre; } 
                    set { nombre = value;}
                     }
        public string Telefono 
        {   get{ return telefono;} 
            set{telefono = value;} 
         }
         public int Id { 
                    get { return id; } 
                    set { id = value;}
                     }

        public Contacto() {
            nombre = "";
            telefono = "";
         }

        public Contacto(string nombre, string telefono)
        {
            this.nombre = nombre;
            this.telefono = telefono;
        }
        public Contacto(int id,string nombre, string telefono):this(nombre,telefono)
        {
            this.id = id;
        }
        
    }
}
