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
    public partial class Playfair_Decriptare : Form
    {
        public Playfair_Decriptare()
        {
            InitializeComponent();
        }
        public char[] separator = {' ', ',', '.', ';', '@', '#', '(', ')', '_', '$', '/', '*', '?', '!',  '%', '^', '&', ':', '<', '>', '~', '`', '"','*', '+', '-', '=', '{', '}', '[', ']'};

        public bool alfabet(string textIntrodus)
        {
            string[] cifre = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            for (int i = 0; i < cifre.Length; i++)
                if (textIntrodus.Contains(cifre[i]))
                    return false;
            return true;
        }

        private string decriptare(int start, int increm, string text, string[,] matrice)
        {
            string rez = "";
            char x, y;
            string a = "", b = "";
            int x_coloana = 0, x_linie = 0, y_coloana = 0, y_linie = 0;
            String temp = text.Substring(start, increm);
            x = temp[0]; y = temp[1];

            if (x_coloana != 5)
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        if (matrice[i, j] == x.ToString())
                        {
                            x_linie = i;
                            x_coloana = j;
                            break;
                        }
            if (y_coloana != 5)
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        if (matrice[i, j] == y.ToString())
                        {
                            y_linie = i;
                            y_coloana = j;
                            break;
                        }
            if ((x_coloana != y_coloana) && (x_linie != y_linie)) //liniii si coloane diferite
            {
                a += matrice[x_linie, y_coloana];
                b += matrice[y_linie, x_coloana];
                rez = a + b;
            }
            else if (x_linie == y_linie) //aceeasi linie
            {
                x_coloana = x_coloana - 1;
                y_coloana = y_coloana - 1;

                if (x_coloana < 0) x_coloana = 4;
                if (y_coloana < 0) y_coloana = 4;

                a += matrice[x_linie, x_coloana];
                b += matrice[y_linie, y_coloana];
                rez = a + b;
            }
            else if (x_coloana == y_coloana) //aceeasi coloana
            {
                x_linie = x_linie - 1;
                y_linie = y_linie - 1;

                if (x_linie < 0) x_linie = 4;
                if (y_linie < 0) y_linie = 4;

                a += matrice[x_linie, x_coloana];
                b += matrice[y_linie, y_coloana];
                rez = a + b;
            }
            return rez;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //prelucrare cheie
            string cheieTxt = textBox1.Text;
            string[] cheieMat = new string[cheieTxt.Length];
            if (string.IsNullOrEmpty(cheieTxt))
                MessageBox.Show("Cheie neintrodusa!");
            else if (!alfabet(cheieTxt))
            {
                MessageBox.Show("Cheia nu poate contine cifre!");
                textBox1.Text = "";
            }
            else
                cheieMat = cheieTxt.ToUpper().Split(separator);

            string cheie = "";
            for (int c = 0; c < cheieMat.Length; c++)
                cheie += cheieMat[c];

            cheie = new string(cheie.ToCharArray().Distinct().ToArray());  //fara dubluri in cheie

            //prelucrare text introdus
            string text = textBox2.Text;
            string[] plaintext = new string[text.Length];

            if (string.IsNullOrEmpty(text))
                MessageBox.Show("Plaintext neintrodus!");
            else if (!alfabet(text))
            {
                MessageBox.Show("Plaintextul contine cifre!");
                textBox1.Text = "";
            }
            else
                plaintext = text.ToUpper().Split(separator);

            string sirulMeu = "";
            for (int k = 0; k < plaintext.Length; k++)
                sirulMeu += plaintext[k];

            if (sirulMeu.Contains('J'))
                sirulMeu = sirulMeu.Replace('J', 'I');
            text = Regex.Replace(sirulMeu, @"\t|\n|\r", "");

            //creare matrice
            string[,] matrice = new string[5, 5];
            string alfabetStr = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
            char[] arrayAlf = alfabetStr.ToCharArray();
            int i = 0, j = 0;
            string str = "";

            str += cheie;
            for (int k = 0; k < arrayAlf.Length; k++)
                if (!str.Contains(arrayAlf[k]))
                    str += arrayAlf[k].ToString();

            for (int k = 0; k < str.Length; k++)
            {
                matrice[i, j] = str[k].ToString();
                j++;
                if (j == 5)
                {
                    j = 0;
                    i++;
                }
            }

            //decriptare
            string[,] matriceaMea = matrice;
            string mesaj_decriptat = "";
            int startI = 0;
            int endI = 2;//ia cate 2 caractere
            int incrementare = 2;

            while (endI <= text.Length)
            {
                mesaj_decriptat += decriptare(startI, incrementare, text, matriceaMea);
                startI = endI;
                endI += 2;
            }

            textBox3.Text = mesaj_decriptat;

        }
    }
}
