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
    public partial class HomofonicDecriptare : Form
    {
        public HomofonicDecriptare()
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

        private string PregatireText(string CT)
        {
            string[] CTM = new string[CT.Length];

            if (string.IsNullOrEmpty(CT))
                MessageBox.Show("Introdu textul criptat!");
            else if (alfabet(CT))
            {
                MessageBox.Show("Textul criptat nu trebuie sa contina litere!");
                textBox3.Text = "";
            }
            else if (CT.ToUpper().Split(separator).Length % 2 != 0)
                MessageBox.Show("Numerele trebuie sa fie doua cate doua!");
            else
                CTM = CT.ToUpper().Split(separator);

            string sir = "";
            for (int i = 0; i < CTM.Length; i++)
                sir += CTM[i];


            CT = Regex.Replace(sir, @"\t|\n|\r", "");

            return CT;
        }

        private string PregatireCheie(string K)
        {
            string[] KM = new string[K.Length];
            string cheie = "";

            if (K.Length != 4)
            {
                MessageBox.Show("Cheia trebuie sa aiba o lungime fixa de 4 caractere!");
                textBox2.Text = "";
            }
            else
            {
                if (string.IsNullOrEmpty(K))
                    MessageBox.Show("Introdu o cheie!");
                else if (!alfabet(K))
                {
                    MessageBox.Show("Cheia nu trebuie sa contina cifre!");
                    textBox2.Text = "";
                }

                else
                    KM = K.ToUpper().Split(separator);

                for (int i = 0; i < KM.Length; i++)
                    cheie += KM[i];

                cheie = new string(cheie.ToCharArray().Distinct().ToArray());

                if (cheie.Length != 4)
                {
                    MessageBox.Show("Cheia trebuie sa aiba o lungime fixa de 4 caractere distincte!");
                    cheie = "";
                }
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

        public string decriptareACAHomophonic(string[,] matrice, string text)
        {
            string result = "";

            int position = 0;
            string first = "";

            for (int k = 0; k < text.Length; k += 2)
            {
                first = text[k].ToString() + text[k + 1].ToString();

                for (int i = 0; i < matrice.GetLength(0); i++)
                    for (int j = 0; j < matrice.GetLength(1); j++)
                        if (matrice[i, j] == first)
                        {
                            position = j;
                            result += alphabet[position];
                        }
            }
            return result;
        }

        public string returneazaCheie(string valoare, Dictionary<string, string> hash)
        {
            string cheie = "";
            for (int i = 0; i < hash.Count; i++)
                if (hash.ElementAt(i).Value.ToString().Contains(valoare))
                {
                    cheie = hash.ElementAt(i).Key.ToString();
                }

            return cheie;
        }

        public string decriptareHomophonic(string text)
        {
            string result = "";

            string sirE = "00,06,13,32,52,53,71,72,83,93,94";
            string sirT = "14,16,30,31,43,58,73,79,84";
            string sirO = "11,15,25,41,42,57,78,85";
            string sirI = "03,10,34,35,54,56,77,86";
            string sirA = "18,19,20,36,55,62,76,87";
            string sirN = "02,37,38,59,61,69,70";
            string sirR = "09,26,39,60,75,88";
            string sirS = "17,28,63,74,89";
            string sirH = "04,08,27,64";
            string sirL = "21,40,65,82";
            string sirD = "05,29,66";
            string sirU = "07,22,91";
            string sirC = "23,44,92";
            string sirM = "33,51,80";
            string sirP = "12,50";
            string sirY = "49,68";
            string sirF = "24,45";
            string sirG = "01,96";
            string sirW = "81,98";
            string sirB = "48,97";
            string sirV = "99";
            string sirK = "67";
            string sirX = "47";
            string sirJ = "95";
            string sirQ = "90";
            string sirZ = "46";

            Dictionary<string, string> hash = new Dictionary<string, string>();
            hash.Add("A", sirA);
            hash.Add("B", sirB);
            hash.Add("C", sirC);
            hash.Add("D", sirD);
            hash.Add("E", sirE);
            hash.Add("F", sirF);
            hash.Add("G", sirG);
            hash.Add("H", sirH);
            hash.Add("I", sirI);
            hash.Add("J", sirJ);
            hash.Add("K", sirK);
            hash.Add("L", sirL);
            hash.Add("M", sirM);
            hash.Add("N", sirN);
            hash.Add("O", sirO);
            hash.Add("P", sirP);
            hash.Add("Q", sirQ);
            hash.Add("R", sirR);
            hash.Add("S", sirS);
            hash.Add("T", sirT);
            hash.Add("U", sirU);
            hash.Add("V", sirV);
            hash.Add("W", sirW);
            hash.Add("X", sirX);
            hash.Add("Y", sirY);
            hash.Add("Z", sirZ);

            for (int i = 0; i < text.Length; i += 2)
            {
                string value = text[i].ToString() + text[i + 1].ToString();
                string cheie = returneazaCheie(value, hash);
                result += cheie;
            }
            return result;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true) //criptare ACA Homophonic
            {
                textBox1.Text = PregatireText(textBox1.Text);
                textBox2.Text = PregatireCheie(textBox2.Text);
                string[,] matrice = GenerareMatriceAlfabet(textBox2.Text);
                textBox3.Text = decriptareACAHomophonic(matrice, textBox1.Text);
            }
            else if (checkBox1.Checked == true) //criptare Homophonic freq
            {
                textBox1.Text = PregatireText(textBox1.Text);
                textBox3.Text = decriptareHomophonic(textBox1.Text);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
                textBox1.Enabled = true;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";

                button1.Enabled = true;
            }
            else if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
                textBox1.Enabled = false;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";

                button1.Enabled = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                checkBox1.Checked = false;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                button1.Enabled = true;
            }
            else if (checkBox1.Checked == true)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                checkBox2.Checked = false;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                button1.Enabled = false;
            }
        }
    }
}
