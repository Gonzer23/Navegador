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
using System.Windows.Forms.ComponentModel.Com2Interop;

namespace LIstas_Dinamicas_de_Objetos
{

    public partial class Form1 : Form
    {
        List<Persona> personas = new List<Persona>();

        internal class Persona
        {
            string dpi;
            string nombre;
            string apellido;
            DateTime fechaNacimiento; 
            public string Dpi { get => dpi; set => dpi = value; }
            public string Nombre { get => nombre; set => nombre = value; }
            public string Apellido { get => apellido; set => apellido = value; }
            public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FileStream stream = new FileStream("personas.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Persona persona = new Persona();
                persona.Dpi = reader.ReadLine();
                persona.Nombre = reader.ReadLine();
                persona.Apellido = reader.ReadLine();
                persona.FechaNacimiento = DateTime.Parse(reader.ReadLine());
                personas.Add(persona);
            }
            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Persona persona = new Persona();
            persona.Dpi= textBox1.Text;
            persona.Nombre = textBox2.Text;
            persona.Apellido = textBox3.Text;
            persona.FechaNacimiento = monthCalendar1.SelectionStart;

            personas.Add(persona);
            limpiar();
        }

        private void limpiar()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            monthCalendar1.SetDate(DateTime.Now);
        }

        private void Mostrar()
        {
            dataGridView1 .DataSource = null;
            dataGridView1.DataSource = personas;
            dataGridView1.Refresh();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Mostrar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string dpi = textBox4.Text;
            personas.RemoveAll(p => p.Dpi == dpi);

            comboBox1.DataSource = null;
            comboBox1.DataSource = "Nombre";
            comboBox1.SelectedValue = "Dpi";
            comboBox1.DataSource = personas;
            Mostrar();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            personas = personas .OrderBy(p => p.Apellido).ToList();
            Mostrar();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FileStream stream = new FileStream("personas.txt", FileMode.OpenOrCreate, FileAccess.Write);

            StreamWriter writer = new StreamWriter(stream);

            foreach (var p in personas)
            {
                writer.WriteLine(p.Dpi);
                writer.WriteLine(p.Nombre);
                writer.WriteLine(p.Apellido);
                writer.WriteLine(p.FechaNacimiento);
            }
            writer.Close();
            MessageBox.Show("Archivo guardado con Exito.txt");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            personas = personas.OrderByDescending(p => p.Apellido).ToList();
            Mostrar();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            String dpi = textBox4.Text;
            Persona personaEncontrada =personas.Find(p => p.Dpi == textBox4.Text);

            if (personaEncontrada != null)
            {
                MessageBox.Show($"Persona encontrada: {personaEncontrada.Nombre} {personaEncontrada.Apellido}");
            }
            else
            {
                MessageBox.Show("Persona no encontrada");
            }
        }
    }
}
