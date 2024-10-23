using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace jogodamemoria
{
    public partial class Form1 : Form
    {
        int sequencias, cliques, cartasencontradas, tagIndex;
        Image[] img = new Image[6];
        List<string> lista = new List<string>();
        int[] tags = new int[2];
        public Form1()
        {
            InitializeComponent();
            Inicio();
        }
        private void Inicio()
        {
            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                tagIndex = int.Parse(String.Format("{0}", item.Tag));
                img[tagIndex] = item.Image;
                item.Image = Properties.Resources.VERSO;
                item.Enabled = true;
            }
            posicoes();
        }
        private void posicoes()
        {
            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                Random rdn = new Random();
                int[] xP = { 12, 222, 434, 647, 859, 1072 };
                int[] yP = { 24, 330 };

            Repete:
                var x = xP[rdn.Next(0, xP.Length)];
                var y = yP[rdn.Next(0, yP.Length)];

                item.Location = new Point(x, y);
                string verificacao = x.ToString() + y.ToString();

                if (lista.Contains(verificacao))
                {
                    goto Repete;
                }
                else
                {
                    item.Location = new Point(x, y);
                    lista.Add(verificacao);
                }
            }
        }
        private void ImagensClick_Click(object sender, EventArgs e)
        {
            bool parEncontrado = false;

            PictureBox pic = (PictureBox)sender;
            cliques++;
            tagIndex = int.Parse(String.Format("{0}", pic.Tag));
            pic.Image = img[tagIndex];
            pic.Refresh();

            if (cliques == 1)
            {
                tags[0] = int.Parse(String.Format("{0}", pic.Tag));
            }
            else if (cliques == 2)
            {
                sequencias++;
                lbSequencias.Text = "Sequencias: " + sequencias.ToString();
                tags[1] = int.Parse(String.Format("{0}", pic.Tag));
                parEncontrado = ChecagemPares();
                Desvirar(parEncontrado);
            }

        }
        private bool ChecagemPares()
        {
            cliques = 0;
            if (tags[0] == tags[1]) { return true; } { return false; }
        }
        private void Desvirar(bool check)
        {
            Thread.Sleep(300);
            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                if (int.Parse(String.Format("{0}", item.Tag)) == tags[0] || int.Parse(String.Format("{0}", item.Tag)) == tags[1])
                {
                    if (check == true)
                    {
                        item.Enabled = false;
                        cartasencontradas++;
                    }
                    else
                    {
                        item.Image = Properties.Resources.VERSO;
                        item.Refresh();
                    }
                }
            }
            FinalJogo();
        }
        private void FinalJogo()
        {
            if (cartasencontradas == (img.Length * 2))
            {
                MessageBox.Show("Parabéns, você terminou o jogo com " + sequencias.ToString() + " sequencias.");
                DialogResult msg = MessageBox.Show("Deseja continuar o jogo?", "Caixa de pergunta", MessageBoxButtons.YesNo);

                if (msg == DialogResult.Yes)
                {
                    cliques = 0; sequencias = 0; cartasencontradas = 0;
                    lbSequencias.Text = "Sequencias: ";
                    lista.Clear();
                    Inicio();
                }
                else if (msg == DialogResult.No)
                {
                    MessageBox.Show("Obrigado por jogar!");
                    Application.Exit();
                }
            }
        }
    }
}
