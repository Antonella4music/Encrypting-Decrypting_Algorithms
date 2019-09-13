using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void criptareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // hide main form
           // this.Hide();

            // show other form
            Cezar_Criptare form2 = new Cezar_Criptare();
            form2.ShowDialog();

            // close application
            //this.Close();
        }

        private void decriptareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // hide main form
            //this.Hide();

            // show other form
            Cezar_Decriptare form2 = new Cezar_Decriptare();
            form2.ShowDialog();

            // close application
           // this.Close();
        }

        private void criptareToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Playfair_Criptare f = new Playfair_Criptare();
            f.Show();

        }

        private void decriptareToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Playfair_Decriptare f = new Playfair_Decriptare();
            f.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Student: Berches Antonela \ngrupa 3141b\nCalculatoare");
        }

        private void criptareCuFisierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CezarFisier f = new CezarFisier();
            f.Show();
        }

        private void criptareCuFisierToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PlayFairFisier f = new PlayFairFisier();
            f.Show();
        }

        private void criptareToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            HomofonicCriptare f = new HomofonicCriptare();
            f.Show();
        }

        private void criptareToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ADFGVXCriptare f = new ADFGVXCriptare();
            f.Show();
        }

        private void decriptareToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ADFGVXDecriptare f = new ADFGVXDecriptare();
            f.Show();
        }

        private void decriptareToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            HomofonicDecriptare f = new HomofonicDecriptare();
            f.Show();
        }

        private void enigmaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnigmaCriptare f = new EnigmaCriptare();
            f.Show();
        }
    }
}
