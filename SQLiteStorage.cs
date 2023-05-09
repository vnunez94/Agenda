using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;

namespace AppAgenda
{
    public class SQLiteStorage : IPersistencia
    {
        private readonly string _connectionString;

        public SQLiteStorage(string connectionString)
        {
            _connectionString = connectionString;
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                CREATE TABLE IF NOT EXISTS contactos (
                    Id INTEGER PRIMARY KEY,
                    Nombre TEXT NOT NULL,
                    Telefono TEXT NOT NULL
                );
            ";
            command.ExecuteNonQuery();
        }

        public List<Contacto> GetContactos()
        {
            var contactos = new List<Contacto>();

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM contactos;";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var contacto = new Contacto
                {
                    Id = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Telefono = reader.GetString(2)
                    
                };
                contactos.Add(contacto);
            }

            return contactos;
        }

        public void GuardarContacto(Contacto contacto)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO contactos (Nombre, Telefono) VALUES (@nombre, @telefono);";
            command.Parameters.AddWithValue("@nombre", contacto.Nombre);
            command.Parameters.AddWithValue("@telefono", contacto.Telefono);
            command.ExecuteNonQuery();
        }

        public void EliminarContacto(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM contactos WHERE Id = @id;";
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
        public void EliminarContacto(Contacto contacto)
        {
            EliminarContacto(contacto.Id);
        }
    }
}
