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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Guardar(string fileName, string texto)
        {
            FileStream stream = new FileStream(fileName, FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(texto);
            writer.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Guardar(@"archivo.txt", textBox1.Text);
            MessageBox.Show("El archivo se ha guardado correctamente");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            
            openFileDialog1.InitialDirectory =  @"c:\";
            
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                
                string fileName = openFileDialog1.FileName;

                
                FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);
                
                while (reader.Peek() > -1)
                {
                    richTextBox1.AppendText(reader.ReadLine());
                }
                reader.Close();
            }
        }
    }
}
