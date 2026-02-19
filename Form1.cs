using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Windows.Forms;

namespace Navegador
{
    public partial class Form1 : Form
    {
        List<Direccion> historial = new List<Direccion>();
        string rutaHistorial = "historial.txt";

        public Form1()
        {
            InitializeComponent();
        }

        private void Leer()
        {
            string nombreArchivo = @"historial.txt";
            FileStream stream = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            while (reader.Peek() > -1)
            {
                string linea = reader.ReadLine();
                comboBox1.Items.Add(linea);
            }
            reader.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webView21.Source = new Uri("https://www.google.com");
            CargarHistorial();
            Leer();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            Navegar();
        }
        private void Navegar()
        {
            string texto = comboBox1.Text.Trim();
            if (string.IsNullOrEmpty(texto))
                return;

            string urlFinal;

            if (!texto.Contains("."))
            {
                string busqueda = WebUtility.UrlEncode(texto);
                urlFinal = $"https://www.google.com/search?q={busqueda}";
            }
            else
            {
                if (!texto.StartsWith("http://") && !texto.StartsWith("https://"))
                    texto = "https://" + texto;

                urlFinal = texto;
            }

            webView21.Source = new Uri(urlFinal);

            var existente = historial.FirstOrDefault(d => d.Url == urlFinal);

            if (existente != null)
            {
                existente.Veces++;
                existente.FechaAccesso = DateTime.Now;
            }
            else
            {
                Direccion direccion = new Direccion
                {
                    Url = urlFinal,
                    Veces = 1,
                    FechaAccesso = DateTime.Now
                };

                historial.Add(direccion);
            }
            GuardarHistorial(urlFinal);

            if (!comboBox1.Items.Contains(urlFinal))
                comboBox1.Items.Insert(0, urlFinal);

            while (comboBox1.Items.Count > 10)
                comboBox1.Items.RemoveAt(10);
        }


        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Navegar();
                e.SuppressKeyPress = true;
            }
        }

        private void homeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            webView21.Source = new Uri("https://www.google.com");
        }

        private void siguienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (webView21.CanGoForward)
                webView21.GoForward();
        }

        private void anteriorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (webView21.CanGoBack)
                webView21.GoBack();
        }
        private void GuardarHistorial(string url)
        {

            FileStream stream = new FileStream("historial.txt", FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);
            foreach (var direccion in historial)
            {
                writer.WriteLine(direccion.Url);
                writer.WriteLine(direccion.Veces);
                writer.WriteLine(direccion.FechaAccesso);
            }

            writer.Close();
            File.AppendAllText(rutaHistorial, url + Environment.NewLine);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
                webView21.Source = new Uri(comboBox1.SelectedItem.ToString());
        }

        private void CargarHistorial()
        {
            if (!File.Exists(rutaHistorial))
                return;

            string[] lineas = File.ReadAllLines(rutaHistorial);

            var ultimas = lineas.Reverse().Take(10);

            comboBox1.Items.Clear();
            foreach (string url in ultimas)
                comboBox1.Items.Add(url);
        }

        private void historialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }
    }
}
