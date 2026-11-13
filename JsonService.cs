using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ContactosApp1
{
    public static class JsonService
    {
        private static readonly string Ruta = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "contactos.json");

        public static List<Contacto> Cargar()
        {
            if (!File.Exists(Ruta)) return new List<Contacto>();
            string json = File.ReadAllText(Ruta);
            return JsonSerializer.Deserialize<List<Contacto>>(json) ?? new List<Contacto>();
        }

        public static void Guardar(IEnumerable<Contacto> lista)
        {
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(Ruta, JsonSerializer.Serialize(lista, opciones));
        }
    }
}
