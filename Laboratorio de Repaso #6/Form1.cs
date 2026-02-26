using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Laboratorio_de_Repaso__6
{
    public partial class Form1 : Form
    {
        List<Empleado> empleados = new List<Empleado>();
        string ruta = "empleados.txt";

        public Form1()
        {
            InitializeComponent();
        }
        internal class Empleado
        {
            public string Numempleado { get; set; }
            public string Nombre { get; set; }
            public double Sueldo_hora { get; set; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(ruta))
            {
                using (StreamReader sr = new StreamReader(ruta))
                {
                    while (!sr.EndOfStream)
                    {
                        string linea = sr.ReadLine();
                        string[] datos = linea.Split(',');

                        if (datos.Length == 3)
                        {
                            Empleado emp = new Empleado();
                            emp.Numempleado = datos[0];
                            emp.Nombre = datos[1];
                            emp.Sueldo_hora = double.Parse(datos[2]);

                            empleados.Add(emp);

                        }
                    }
                }
            }
          ActualizarGrid();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Empleado nuevo = new Empleado();
                nuevo.Numempleado = textBox1.Text;
                nuevo.Nombre = textBox2.Text;
                nuevo.Sueldo_hora = double.Parse(textBox3.Text);

                empleados.Add(nuevo);
                using (StreamWriter sw = new StreamWriter(ruta, false))
                {
                    foreach (Empleado emp in empleados)
                    {
                        sw.WriteLine($"{emp.Numempleado},{emp.Nombre},{emp.Sueldo_hora}");
                    }
                }

                MessageBox.Show("Empleado guardado correctamente");

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
            }
            catch
            {
                MessageBox.Show("Error: Verifique los datos ingresados");
            }
        }

        private void ActualizarGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = empleados;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int indice = dataGridView1.CurrentRow.Index;

                empleados.RemoveAt(indice);
                using (StreamWriter sw = new StreamWriter(ruta, false))
                {
                    foreach (Empleado emp in empleados)
                    {
                        sw.WriteLine($"{emp.Numempleado},{emp.Nombre},{emp.Sueldo_hora}");
                    }
                }

                ActualizarGrid();
                MessageBox.Show("Empleado eliminado correctamente");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ActualizarGrid();
        }

        private void horasTrabajadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.ShowDialog();
        }
    }
}