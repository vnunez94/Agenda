using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AppAgenda
{
    public class FileStorage : IPersistencia
    {
        private readonly string _filePath;
    public FileStorage(string filePath)
    {
        _filePath = filePath;
    }

    public List<Contacto> GetContactos()
    {
        var contactos = new List<Contacto>();
        int id = 0;

        if (File.Exists(_filePath))
        {
            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 2)
                {
                    var contacto = new Contacto(id++, parts[1], parts[2]);
                    contactos.Add(contacto);
                }
            }
        }

        return contactos;
    }

    public void GuardarContacto(Contacto contacto)
    {
        var line = $"{contacto.Nombre},{contacto.Telefono}";
        File.AppendAllLines(_filePath, new[] { line });
    }

    public void EliminarContacto(int index)
    {
        if (!File.Exists(_filePath))
            return;

        var lines = File.ReadAllLines(_filePath).ToList();
        if (index >= 0 && index < lines.Count)
        {
            lines.RemoveAt(index);
            File.WriteAllLines(_filePath, lines);
        }
    }
    public void EliminarContacto(Contacto contacto)
    {
        if (!File.Exists(_filePath))
            return;

        var lines = File.ReadAllLines(_filePath).ToList();
        var line = $"{contacto.Nombre},{contacto.Telefono}";
        if (lines.Contains(line))
        {
            lines.Remove(line);
            File.WriteAllLines(_filePath, lines);
        }
    }
}
}