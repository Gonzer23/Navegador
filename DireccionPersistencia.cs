using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navegador
{
    internal class DireccionPersistencia
    {
        string fileHistorial = "historial.txt";
        string fileHistorialJSON = "historial.json";

        public void GuardarTxt(List<Direccion> direcciones)
        {
            FileStream stream = new FileStream(fileHistorial, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);
            foreach (var Direccion in direcciones)
            {
                writer.WriteLine(Direccion.Url);
                writer.WriteLine(Direccion.Veces);
                writer.WriteLine(Direccion.FechaAccesso);
            }
            writer.Close();
        }

        public List<Direccion> CargarTxt()
        {
            List<Direccion> direcciones = new List<Direccion>();
            if (File.Exists(fileHistorial))
            {
                FileStream stream = new FileStream(fileHistorial, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);
                while (!reader.EndOfStream)
                {
                    Direccion direccion = new Direccion
                    {
                        Url = reader.ReadLine(),
                        Veces = int.Parse(reader.ReadLine()),
                        FechaAccesso = DateTime.Parse(reader.ReadLine())
                    };
                    direcciones.Add(direccion);
                }
                reader.Close();
            }
            return direcciones;
        }

        public List<Direccion> CargarJson()
        {
            List<Direccion> direcciones = new List<Direccion>();
            if (File.Exists(fileHistorialJSON))
            {
                string json = File.ReadAllText(fileHistorialJSON);
                direcciones = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Direccion>>(json);
            }
            return direcciones;
        }
    }
}
