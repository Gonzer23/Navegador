using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Laboratorio_de_Repaso__6
{
    public partial class Form3 : Form
    {
        string rutaEmpleados = "empleados.txt";
        string rutaRegistro = "registro.txt";

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            CargarNombres();
        }
        private void CargarNombres()
        {
            comboBox1.Items.Clear();

            if (!File.Exists(rutaEmpleados)) return;

            foreach (var linea in File.ReadAllLines(rutaEmpleados))
            {
                string[] datos = linea.Split(',');
                comboBox1.Items.Add(datos[1]); 
            }
        }

        
        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.ShowDialog();
            this.Close();
        }

        private void horasTrabajadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null) return;

            string nombreSeleccionado = comboBox1.SelectedItem.ToString();

            if (!File.Exists(rutaEmpleados) || !File.Exists(rutaRegistro))
                return;

            var empleado = File.ReadAllLines(rutaEmpleados)
                .Select(l => l.Split(','))
                .FirstOrDefault(d => d[1] == nombreSeleccionado);

            if (empleado == null) return;

            string numEmpleado = empleado[0];
            double sueldoHora = double.Parse(empleado[2]);

            var lista = File.ReadAllLines(rutaRegistro)
                .Select(l => l.Split(','))
                .Where(r => r[0] == numEmpleado)
                .Select(r => new
                {
                    Nombre = nombreSeleccionado,
                    Mes = r[1],
                    SueldoHora = sueldoHora,
                    SueldoMensual = double.Parse(r[3])
                })
                .ToList();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = lista;
        }
    } 
}