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
    public partial class HomofonicCriptare : Form
    {
        public HomofonicCriptare()
        {
            InitializeComponent();
        }

        public char[] separator = { ' ', ',', '.', ';', '@', '#', '(', ')', '_', '$', '/', '*', '?', '!', '%', '^', '&', ':', '<', '>', '~', '`', '"', '*', '+', '-', '=', '{', '}', '[', ']' };
        public string alphabet = "ABCDEFGHIKLMNOPQRSTUVWXYZ";

        public bool alfabet(string textIntrodus)
        {
            string[] cifre = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            for (int i = 0; i < cifre.Length; i++)
                if (textIntrodus.Contains(cifre[i]))
                    return false;
            return true;

        }

        private string PregatireText(string PT)
        {
            string[] PTM = new string[PT.Length];

            if (string.IsNullOrEmpty(PT))
                MessageBox.Show("Introdu un text la criptare!");
            else if (!alfabet(PT))
            {
                MessageBox.Show("Textul de criptat contine cifre!");
                textBox1.Text = "";
            }
            else
                PTM = PT.ToUpper().Split(separator);

            string sir = "";
            for (int i = 0; i < PTM.Length; i++)
                sir += PTM[i];

            PT = Regex.Replace(sir, @"\t|\n|\r", "");
            return PT;
        }

        private string PregatireCheie(string K)
        {
            string[] KM = new string[K.Length];
            string cheie = "";

            if (string.IsNullOrEmpty(K))
                MessageBox.Show("Introdu o cheie!");
            else if (!alfabet(K))
            {
                MessageBox.Show("Cheia contine cifre!");
                textBox2.Text = "";
            }

            else
                KM = K.ToUpper().Split(separator);

            for (int i = 0; i < KM.Length; i++)
                cheie += KM[i];

            if (cheie.Length != 4)
            {
                MessageBox.Show("Cheia trebuie sa aiba 4 caractere distincte!");
                cheie = "";
            }

            return cheie;
        }

        public string[,] GenerareMatriceAlfabet(string cheie)
        {
            string[,] matrice = new string[4, 25];

            int poz = 0, contor = 1;
            bool ok = true;
            int p = 0;

            for (int i = 0; i < cheie.Length; i++)
                for (int j = 0; j < alphabet.Length; j++)  
                {
                    if (alphabet[j].ToString() == cheie[i].ToString())
                    {
                        poz = j;
                        p = j;
                        ok = true;
                        while (ok)
                        {

                            if (contor == 100) contor = 0;
                            matrice[i, poz] = contor.ToString();
                            contor++;
                            poz++;
                            if (poz == matrice.GetLength(1)) poz = 0;
                            if (p == poz)
                                ok = false; 
                        }
                    }
                }
            for (int i = 0; i < matrice.GetLength(0); i++)
                for (int j = 0; j < matrice.GetLength(1); j++)
                    if (matrice[i, j].Length == 1)
                        matrice[i, j] = "0" + matrice[i, j];

            return matrice;
        }

        public string criptareACAHomophonic(string[,] matrice, string text)
        {
            string result = "";

            int poz = 0;
            string partial = "";

            for (int i = 0; i < text.Length; i++)
                for (int j = 0; j < alphabet.Length; j++)
                    if (text[i].ToString() == alphabet[j].ToString())
                    {
                        poz = j;
                        Random rnd = new Random();
                        partial = matrice[rnd.Next(0, matrice.GetLength(0)), poz];
                        result += partial + " ";
                    }

            return result;
        }

        public string criptareHomophonic(string text)
        {
            string result = "";
 
            Dictionary<string, string> hash = new Dictionary<string, string>();
            hash.Add("E", "00,06,13,32,52,53,71,72,83,93,94");
            hash.Add("T", "14,16,30,31,43,58,73,79,84");
            hash.Add("O", "11,15,25,41,42,57,78,85");
            hash.Add("I", "03,10,34,35,54,56,77,86");
            hash.Add("A", "18,19,20,36,55,62,76,87");
            hash.Add("N", "02,37,38,59,61,69,70");
            hash.Add("R", "09,26,39,60,75,88");
            hash.Add("S", "17,28,63,74,89");
            hash.Add("H", "04,08,27,64");
            hash.Add("L", "21,40,65,82");
            hash.Add("D", "05,29,66");
            hash.Add("U", "07,22,91");
            hash.Add("C", "23,44,92");
            hash.Add("M", "33,51,80");
            hash.Add("P", "12,50");
            hash.Add("Y", "49,68");
            hash.Add("F", "24,45");
            hash.Add("G", "01,96");
            hash.Add("W", "81,98");
            hash.Add("B", "48,97");
            hash.Add("V", "99");
            hash.Add("K", "67");
            hash.Add("X", "47");
            hash.Add("J", "95");
            hash.Add("Q", "90");         
            hash.Add("Z", "46");

            for (int i = 0; i < text.Length; i++)
            {
                if (hash.ContainsKey(text[i].ToString()))
                {
                    string sir = "";
                    if (hash.TryGetValue(text[i].ToString(), out sir))
                    {
                        string[] vects = sir.Split(',');
                        int[] vect = new int[vects.Length];

                        for (int x = 0; x < vect.Length; x++)
                            vect[x] = int.Parse(vects[x]);

                        Random rnd = new Random();
                        int elem = vect[rnd.Next(0, vect.Length)];
                        string str = elem.ToString();
                        if (str.Length == 1) str = "0" + str;
                        result += str + " ";
                    }
                }
            }
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)  //criptare ACA Homophonic
            {
                string sir = PregatireText(textBox1.Text);
                if (sir.Contains('J'))
                    sir = sir.Replace('J', 'I');
                textBox1.Text = sir;
                textBox2.Text = PregatireCheie(textBox2.Text);
                string[,] matrice = GenerareMatriceAlfabet(textBox2.Text);
                textBox3.Text = criptareACAHomophonic(matrice, textBox1.Text);
            }
            else if (checkBox1.Checked == true)  //criptare Homophonic freq
            {
                textBox1.Text = PregatireText(textBox1.Text);
                textBox3.Text = criptareHomophonic(textBox1.Text);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
                textBox1.Enabled = true;
                textBox3.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";

                button1.Enabled = true;
            }
            else if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
                textBox1.Enabled = false;
                textBox3.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";

                button1.Enabled = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";

                button1.Enabled = true;
            }
            else if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";

                button1.Enabled = false;
            }
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            if (textBox2.Text.Length != 4)
            {
                MessageBox.Show("Cheia trebuie sa aiba o lungime fixa de 4 caractere!");
                textBox2.Text = "";
            }
        }
    }
}
