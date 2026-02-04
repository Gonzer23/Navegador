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

namespace Navegador
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();


            openFileDialog1.InitialDirectory = @"""C:\Users\Dilan\source\repos\Navegador\bin\Debug\historial.txt""";

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
