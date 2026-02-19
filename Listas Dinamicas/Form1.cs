using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Listas_Dinamicas
{
    public partial class Form1 : Form
    {
        List<int> numeros = new List<int>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            int numero = Convert.ToInt16(textBox1.Text);
            numeros.Add(numero);
        }

        private void button2_Click(object sender, EventArgs e)
        {
             listBox1.DataSource = numeros;
            listBox1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < numeros.Count; i++)
            //{
            //  int numero = numeros[i];
            //listBox1.Items.Add(numero);
            //}

            foreach (int numero in numeros)
            {
                listBox1.Items.Add(numero);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            numeros.Sort();
            listBox1.DataSource = null;
            listBox1.DataSource = numeros;
            listBox1.Refresh();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            numeros.Sort();
            numeros.Reverse();
        }
    }
}
