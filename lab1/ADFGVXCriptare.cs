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
    public partial class ADFGVXCriptare : Form
    {
        public ADFGVXCriptare()
        {
            InitializeComponent();
        }

        public char[] separator = { ' ', ',', '.', ';', '@', '#', '(', ')', '_', '$', '/', '*', '?', '!', '%', '^', '&', ':', '<', '>', '~', '`', '"', '*', '+', '-', '=', '{', '}', '[', ']' };
        public string initial = "ADFGVX";

        public bool alfabet(string textIntrodus)
        {
            string[] cifre = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            for (int i = 0; i < cifre.Length; i++)
                if (textIntrodus.Contains(cifre[i]))
                    return false;
            return true;

        }

        static string MatriceOrdonata(string[,] matrice, string cheie)
        {
            string sir = "";

            char[] str = cheie.ToCharArray();
            str = str.OrderBy(d => d).ToArray();

            int q = 0;

            while (q < str.Length)
            {
                string car = str[q].ToString();
                for (int i = 0; i < matrice.GetLength(0); i++)
                    if (car.Equals(matrice[i, 0]))
                        for (int j = 1; j < matrice.GetLength(1); j++)
                            sir += matrice[i, j];
                q++;
            }

            return sir;

        }

        private string PregatireText(string textPT)
        {
            string[] textPTM = new string[textPT.Length];

            if (string.IsNullOrEmpty(textPT))
                MessageBox.Show("Nu ai introdus nici un text. Insereaza ceva");
            else
                textPTM = textPT.ToUpper().Split(separator);

            string sir = "";
            for (int i = 0; i < textPTM.Length; i++)
                sir += textPTM[i];

            textPT = Regex.Replace(sir, @"\t|\n|\r", "");
            return textPT;
        }

        private string PregatireCheie(string textK)
        {
            string[] textKM = new string[textK.Length];
            if (string.IsNullOrEmpty(textK))
                MessageBox.Show("Introdu un text!");
            else if (!alfabet(textK))
            {
                MessageBox.Show("Textul contine cifre!");
                textK = "";
            }

            else
                textKM = textK.ToUpper().Split(separator);

            string cheie = "";
            for (int i = 0; i < textKM.Length; i++)
                cheie += textKM[i];

            cheie = new string(cheie.ToCharArray().Distinct().ToArray());
            return cheie;
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

        private string[,] GenerareMatriceCheieTranspozitie(string cheie2, string textPreliminar)
        {

            int nl = cheie2.Length;
            double ncD = Math.Ceiling(Convert.ToDouble(textPreliminar.Length) / (double)nl);
            int nc = Convert.ToInt32(ncD);

            string[,] matrice = new string[cheie2.Length, nc + 1];
            char[] txt = textPreliminar.ToCharArray();

            for (int k = 0; k < cheie2.Length; k++)
                matrice[k, 0] = cheie2[k].ToString();

            int i = 0, j = 1;

            for (int k = 0; k < txt.Length; k++)
            {
                matrice[i, j] = txt[k].ToString(); i++;
                if (i == matrice.GetLength(0)) { i = 0; j++; }
                if (j == matrice.GetLength(1)) { j = 1; i++; }
            }

            Random q = new Random();

            for (int x = 0; x < matrice.GetLength(0); x++)
                for (int y = 0; y < matrice.GetLength(1); y++)
                    if (matrice[x, y] == null)
                        matrice[x, y] = initial[q.Next(initial.Length)].ToString();

            return matrice;
        }

        private string pregatireTextPreliminar(string text, string[,] matriceSubstitutie)
        {
            string result = "";
            int x = 0;
            while (x < text.Length)
            {
                string car = text.ElementAt(x).ToString().ToUpper();
                for (int i = 0; i < matriceSubstitutie.GetLength(0); i++)
                    for (int j = 0; j < matriceSubstitutie.GetLength(0); j++)
                        if (car.Equals(matriceSubstitutie[i, j]))
                            result += initial.ElementAt(i).ToString() + initial.ElementAt(j).ToString();
                x++;
            }
            return result;
        }

        private void btnCriptare_Click(object sender, EventArgs e)
        {
            string txtPreluat = "", txtFinal = "";
            string text = textBox1.Text;
            string txt = PregatireText(text);
            textBox1.Text = txt;

            string key1 = textBox2.Text;
            string cheie1 = PregatireCheie(key1);
            textBox2.Text = cheie1;
            string[,] matrice1 = GenerareMatriceCheieSubstitutie(cheie1);
            txtPreluat = pregatireTextPreliminar(txt, matrice1);

            string cheie2 = PregatireCheie(textBox3.Text);
            textBox3.Text = cheie2;
            string[,] matrice2 = GenerareMatriceCheieTranspozitie(cheie2, txtPreluat);
            txtFinal = MatriceOrdonata(matrice2, cheie2);

            textBox4.Text = txtFinal;
        }
    }
}
