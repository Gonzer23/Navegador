using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Laboratorio_de_Repaso__6
{
    public partial class Form2 : Form
    {
        string rutaEmpleados = "empleados.txt";
        string rutaRegistro = "registro.txt";

        List<Registro> registros = new List<Registro>();

        public Form2()
        {
            InitializeComponent();
        }

        internal class Registro
        {
            public string NumEmpleado { get; set; }
            public DateTime Mes { get; set; }
            public int Horas { get; set; }

            [Browsable(false)] 
            public double SueldoMensual { get; set; }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            CargarCombo();
            CargarRegistros();
            ActualizarGrid();
        }

        private void CargarCombo()
        {
            comboBox1.Items.Clear();

            if (File.Exists(rutaEmpleados))
            {
                foreach (var linea in File.ReadAllLines(rutaEmpleados))
                {
                    string[] datos = linea.Split(',');
                    comboBox1.Items.Add(datos[0]);
                }
            }
        }

        private double ObtenerSueldoHora(string numEmpleado)
        {
            foreach (var linea in File.ReadAllLines(rutaEmpleados))
            {
                string[] datos = linea.Split(',');
                if (datos[0] == numEmpleado)
                    return double.Parse(datos[2]);
            }
            return 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string num = comboBox1.SelectedItem.ToString();
            int horas = int.Parse(textBox2.Text);
            DateTime mes = monthCalendar1.SelectionStart;

            double sueldoHora = ObtenerSueldoHora(num);
            double sueldoMensual = sueldoHora * horas; 

            Registro nuevo = new Registro
            {
                NumEmpleado = num,
                Mes = mes,
                Horas = horas,
                SueldoMensual = sueldoMensual 
            };

            registros.Add(nuevo);
            GuardarArchivo();
            ActualizarGrid();

            MessageBox.Show("Registro guardado");
            Limpiar();
        }

        private void GuardarArchivo()
        {
            using (StreamWriter sw = new StreamWriter(rutaRegistro, false))
            {
                foreach (var r in registros)
                {
                    sw.WriteLine($"{r.NumEmpleado},{r.Mes.ToShortDateString()},{r.Horas},{r.SueldoMensual}");
                }
            }
        }

        private void CargarRegistros()
        {
            if (!File.Exists(rutaRegistro)) return;

            foreach (var linea in File.ReadAllLines(rutaRegistro))
            {
                string[] datos = linea.Split(',');

                registros.Add(new Registro
                {
                    NumEmpleado = datos[0],
                    Mes = DateTime.Parse(datos[1]),
                    Horas = int.Parse(datos[2]),
                    SueldoMensual = double.Parse(datos[3])
                });
            }
        }

        private void ActualizarGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = registros;
        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(); 
            form1.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }
        private void Limpiar ()
        {
            comboBox1.SelectedIndex = -1;
            textBox2.Clear();
            monthCalendar1.SetDate(DateTime.Today);
        }
    }
}