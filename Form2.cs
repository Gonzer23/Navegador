using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Navegador
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            CargarHistorial();
        }
        private void CargarHistorial()
        {
            string nombreArchivo = @"historial.txt";
            if (File.Exists(nombreArchivo))
            {
                FileStream stream = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);
                while (reader.Peek() > -1)
                {
                    string linea = reader.ReadLine();
                    richTextBox1.AppendText(linea + Environment.NewLine);
                }
                reader.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            eliminarUrl();
        }

        private void eliminarUrl()
        {
            string urlAEliminar = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(urlAEliminar))
                return;
            string nombreArchivo = @"historial.txt";
            if (File.Exists(nombreArchivo))
            {
                List<string> lineas = File.ReadAllLines(nombreArchivo).ToList();
                lineas.RemoveAll(linea => linea.Equals(urlAEliminar, StringComparison.OrdinalIgnoreCase));
                File.WriteAllLines(nombreArchivo, lineas);
                richTextBox1.Clear();
                CargarHistorial();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            CargarHistorial();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
