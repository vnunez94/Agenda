
using System.Collections.Generic;

namespace AppAgenda
{
    public interface IPersistencia 
    {
        List<Contacto> GetContactos();
        void GuardarContacto(Contacto contacto);
        void EliminarContacto(int id);
        void EliminarContacto(Contacto contacto);
    }
}

