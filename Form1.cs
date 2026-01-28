using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Navegador
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webView21.Source = new Uri("https://www.google.com");
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
                {
                    texto = "https://" + texto;
                }
                urlFinal = texto;
            }


            webView21.Source = new Uri(urlFinal);


            if (!comboBox1.Items.Contains(texto))
            {
                comboBox1.Items.Add(texto);
            }
        }

        private void homeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            webView21.Source = new Uri("https://www.google.com");
        }

        private void siguienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (webView21.CanGoForward)
            {
                webView21.GoForward();
            }
        }

        private void anteriorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (webView21.CanGoBack)
            {
                webView21.GoBack();
            }
        }
    }
}