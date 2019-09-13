using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace lab1
{
    public partial class ADFGVXDecriptare : Form
    {
        public ADFGVXDecriptare()
        {
            InitializeComponent();
        }

        public char[] separator = { ' ', ',', '.', ';', '@', '#', '(', ')', '_', '$', '/', '*', '?', '!', '%', '^', '&', ':', '<', '>', '~', '`', '"', '*', '+', '-', '=', '{', '}', '[', ']' };

        public bool alfabet(string textIntrodus)
        {
            string[] cifre = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            for (int i = 0; i < cifre.Length; i++)
                if (textIntrodus.Contains(cifre[i]))
                    return false;
            return true;

        }

        private string PregatireCheie(string textK)
        {
            string[] textKM = new string[textK.Length];
            if (string.IsNullOrEmpty(textK))
                MessageBox.Show("Introdu un text!");
            else if (!alfabet(textK))
            {
                MessageBox.Show("Textul introdus pentru cheie nu trebuie sa contina cifre!");
                textK = "";
            }

            else
                textKM = textK.ToUpper().Split(separator);

            string cheie = "";
            for (int i = 0; i < textKM.Length; i++)
                cheie += textKM[i];

            cheie = new string(cheie.ToCharArray().Distinct().ToArray());  //eliminare dubluri din cheie
            return cheie;
        }

        private string PregatireText(string textPT)
        {
            string[] textPTM = new string[textPT.Length];

            if (string.IsNullOrEmpty(textPT))
                MessageBox.Show("Textul introdus pentru criptare nu trebuie sa fie vid!");
            else
                textPTM = textPT.ToUpper().Split(separator);

            string sir = "";
            for (int i = 0; i < textPTM.Length; i++)
                sir += textPTM[i];

            textPT = Regex.Replace(sir, @"\t|\n|\r", "");
            return textPT;
        }

        private string[,] GenerareMatriceCheieSubstitutie(string cheie1)
        {
            string[,] matrice = new string[6, 6];
            string alfabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] arralf = alfabet.ToCharArray();

            int i = 0, j = 0;

            string str = "";

            str += cheie1;
            for (int k = 0; k < arralf.Length; k++)
                if (!str.Contains(arralf[k]))
                    str += arralf[k].ToString();

            for (int k = 0; k < str.Length; k++)
            {
                matrice[i, j] = str[k].ToString(); j++;
                if (j == 6) { j = 0; i++; }

            }

            return matrice;
        }

        private string[,] GenerareMatriceCheieTranspozitie(string cheie2, string textCodat)
        {
            int nl = cheie2.Length;
            double ncD = Math.Ceiling(Convert.ToDouble(textCodat.Length) / (double)nl);
            int nc = Convert.ToInt32(ncD);

            string[,] matrice = new string[cheie2.Length, nc + 1];       
            char[] txt = textCodat.ToCharArray();

            for (int k = 0; k < cheie2.Length; k++)
                matrice[k, 0] = cheie2[k].ToString();

            char[] str = cheie2.ToCharArray();
            str = str.OrderBy(d => d).ToArray();

            int q = 0, poz = 0, y = 0;

            while (q < str.Length)
            {
                string car = str[q].ToString();
                for (int i = 0; i < matrice.GetLength(0); i++)
                    if (car.Equals(matrice[i, 0]))
                    {
                        poz = i;
                        for (int j = 1; j < matrice.GetLength(1); j++)
                        {
                            matrice[poz, j] = txt[y].ToString();
                            y++;
                            if (y > txt.Length) y = 0;
                        }
                    }
                q++;
            }

            return matrice;
        }

        static int[,] inlocuireMatriceTranspozitie(string[,] matrice)
        {
            int[,] matriceInt = new int[matrice.GetLength(0), matrice.GetLength(1)];
            for (int i = 0; i < matrice.GetLength(0); i++)
                for (int j = 1; j < matrice.GetLength(1); j++)
                {
                    if (matrice[i, j] == "A") matriceInt[i, j] = 0;
                    if (matrice[i, j] == "D") matriceInt[i, j] = 1;
                    if (matrice[i, j] == "F") matriceInt[i, j] = 2;
                    if (matrice[i, j] == "G") matriceInt[i, j] = 3;
                    if (matrice[i, j] == "V") matriceInt[i, j] = 4;
                    if (matrice[i, j] == "X") matriceInt[i, j] = 5;
                }

            return matriceInt;
        }

        private void btnCriptare_Click(object sender, EventArgs e)
        {
            string txtk1 = PregatireCheie(textBox2.Text);
            string txtk2 = PregatireCheie(textBox3.Text);
            string txtct = PregatireText(textBox1.Text);
            textBox2.Text = txtk1;
            textBox3.Text = txtk2;
            textBox1.Text = txtct;
            string[,] matrSubs = GenerareMatriceCheieSubstitutie(textBox2.Text);
            string[,] matrTransp = GenerareMatriceCheieTranspozitie(textBox3.Text, textBox1.Text);

            string textDecriptat = "", txt = "";
            string[,] matriceSubstitutie = matrSubs;
            int[,] matriceTranspozitie = inlocuireMatriceTranspozitie(matrTransp);
            for (int j = 1; j < matriceTranspozitie.GetLength(1); j++)
                for (int i = 0; i < matriceTranspozitie.GetLength(0); i++)          
                    txt += matriceTranspozitie[i, j];

            for (int i = 0; i < txt.Length - 1; i += 2)
                textDecriptat += matriceSubstitutie[int.Parse(txt[i].ToString()), int.Parse(txt[i + 1].ToString())];

            textBox4.Text =  textDecriptat;

        }
    }
}
