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
    public partial class EnigmaCriptare : Form
    {
        public EnigmaCriptare()
        {
            InitializeComponent();
        }

        public string[] reflectorB = { "AY", "BR", "CU", "DH", "EQ", "FS", "GL", "IP", "JX", "KN", "MO", "TZ", "VW" };
        public string[] reflectorC = { "AF", "BV", "CP", "DJ", "EI", "GO", "HY", "KR", "LZ", "MX", "NW", "TQ", "SU" };

        public string[] rotor1 = { "E", "K", "M", "F", "L", "G", "D", "Q", "V", "Z", "N", "T", "O", "W", "Y", "H", "X", "U", "S", "P", "A", "I", "B", "R", "C", "J" };
        public string[] rotor2 = { "A", "J", "D", "K", "S", "I", "R", "U", "X", "B", "L", "H", "W", "T", "M", "C", "Q", "G", "Z", "N", "P", "Y", "F", "V", "O", "E" };
        public string[] rotor3 = { "B", "D", "F", "H", "J", "L", "C", "P", "R", "T", "X", "V", "Z", "N", "Y", "E", "I", "W", "G", "A", "K", "M", "U", "S", "Q", "O" };

        public char[] separator = { ' ', ',', '.', ';', '@', '#', '(', ')', '_', '$', '/', '*', '?', '!', '%', '^', '&', ':', '<', '>', '~', '`', '"', '*', '+', '-', '=', '{', '}', '[', ']' };
        public string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

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
                MessageBox.Show("Introdu plaintextul!");
            else if (!alfabet(PT))
            {
                MessageBox.Show("Plaintextul nu trebuie sa contina cifre!");
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

        public string[,] CreareMatrice(string[] reflector)
        {
            string[,] matrice = new string[4, 26];

            for (int i = 0; i < alphabet.Length; i++)
            {
                matrice[0, i] = alphabet[i].ToString();
                matrice[1, i] = rotor1[i].ToString();
                matrice[2, i] = rotor2[i].ToString();
                matrice[3, i] = rotor3[i].ToString();
            }
            return matrice;
        }

        public string tur(string caracter, string[,] matrice, string[] reflector)
        {
            string refl1 = "";
            int poz1 = 0, poz2 = 0, poz3 = 0;
            string r1 = "", r2 = "", r3 = "", refl = "";

            for (int j = 0; j < matrice.GetLength(1); j++)
                if (caracter == alphabet[j].ToString())
                {
                    poz3 = j;
                    r3 = matrice[3, poz3];
                    for (int k = 0; k < matrice.GetLength(1); k++)
                        if (r3 == matrice[0, k])
                        {
                            poz2 = k;
                            r2 = matrice[2, poz2];
                            for (int l = 0; l < matrice.GetLength(1); l++)
                                if (r2 == matrice[0, l])
                                {
                                    poz1 = l;
                                    r1 = matrice[1, poz1];
                                    for (int p = 0; p < reflector.Length; p++)
                                        if (reflector[p].Contains(r1))
                                        {
                                            refl = reflector[p];
                                            refl1 = refl.Replace(r1, ""); 
                                        }
                                }
                        }
                }
            return refl1;
        }

        public string retur(string refl, string[,] matrice, string[] reflector)
        {
            int poz1 = 0, poz2 = 0, poz3 = 0;
            string r1 = "", r2 = "", r3 = "";

            for (int j = 0; j < matrice.GetLength(1); j++)
                if (refl == matrice[1, j])
                {
                    poz1 = j;
                    r1 = matrice[0, poz1];
                    for (int k = 0; k < matrice.GetLength(1); k++)
                        if (r1 == matrice[2, k])
                        {
                            poz2 = k;
                            r2 = matrice[0, poz2];
                            for (int l = 0; l < matrice.GetLength(1); l++)
                                if (r2 == matrice[3, l])
                                {
                                    poz3 = l;
                                    r3 = matrice[0, poz3];
                                }
                        }
                }
            return r3;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] reflector = new string[reflectorB.Length];
            textBox1.Text = PregatireText(textBox1.Text);

            if (checkBox1.Checked == true) reflector = reflectorB;
            else if (checkBox2.Checked == true) reflector = reflectorC;

            string[,] matrice = CreareMatrice(reflector);

            string result = "", refl = "", partial = "";
            string PT = textBox1.Text;
            for (int i = 0; i < PT.Length; i++)
            {
                refl = tur(PT[i].ToString(), matrice, reflector);
                partial = retur(refl, matrice, reflector);
                result += partial;
            }

            textBox2.Text = result;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
                textBox1.Enabled = true;
                textBox1.Text = "";
                textBox2.Text = "";

                button1.Enabled = true;
            }
            else if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
                textBox1.Enabled = false;
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
                textBox1.Text = "";
                textBox2.Text = "";

                button1.Enabled = true;
            }
            else if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
                textBox1.Enabled = false;
                textBox1.Text = "";
                textBox2.Text = "";

                button1.Enabled = false;
            }
        }
    }
}
