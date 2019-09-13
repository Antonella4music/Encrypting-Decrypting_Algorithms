using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace lab1
{
    public partial class PlayFairFisier : Form
    {
        public PlayFairFisier()
        {
            InitializeComponent();
        }
        public char[] separator = { ' ', ',', '.', ';', '@', '#', '(', ')', '_', '$', '/', '*', '?', '!', '%', '^', '&', ':', '<', '>', '~', '`', '"', '*', '+', '-', '=', '{', '}', '[', ']' };
        public string ourPath;

        public bool alfabet(string textIntrodus)
        {
            string[] cifre = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            for (int i = 0; i < cifre.Length; i++)
                if (textIntrodus.Contains(cifre[i]))
                    return false;
            return true;
        }

        private string criptare(int start, int increm, string text, string[,] matrice)
        { 
            string rezultat = "";
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
            if ((x_coloana != y_coloana) && (x_linie != y_linie)) //linii  si coloane diferite
            {
                a += matrice[x_linie, y_coloana];
                b += matrice[y_linie, x_coloana];
                rezultat = a + b;
            }
            else if (x_linie == y_linie) //aceeasi linie
            {
                x_coloana = (x_coloana + 1) % 5;
                y_coloana = (y_coloana + 1) % 5;

                a += matrice[x_linie, x_coloana];
                b += matrice[y_linie, y_coloana];
                rezultat = a + b;
            }
            else if (x_coloana == y_coloana) //aceeasi coloana
            {
                x_linie = (x_linie + 1) % 5;
                y_linie = (y_linie + 1) % 5;

                a += matrice[x_linie, x_coloana];
                b += matrice[y_linie, y_coloana];
                rezultat = a + b;
            }
            return rezultat;
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

        private void button2_Click(object sender, EventArgs e) //DECRIPTARE
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

            //citire din fisier
            string line = null;
            TextReader readFile = new StreamReader(ourPath);
            line = readFile.ReadToEnd();

            //prelucrare text introdus
            string text = line;
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

            ///scriere in fisier cyphertext
            StreamWriter fout = new StreamWriter("decriptatPlayfair.txt");
            fout.Write(mesaj_decriptat);
            fout.Close();
            MessageBox.Show("Mesaj decriptat scris in fisier!");

            // textBox3.Text = mesaj_decriptat;
        }

        private void button1_Click(object sender, EventArgs e) //CRIPTARE
        {
            //citire din fisier
                string line = null;
                TextReader readFile = new StreamReader(ourPath);
                line = readFile.ReadToEnd();

                //1. pregătirea textului ce urmează a fi criptat;
                string text = line;
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

                //2. alegearea cheii şi prelucrarea acesteia;
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

                //3. construirea matricei de criptare;
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

                //4. construirea mesajului criptat.
                string ciphertext = "";
                string[,] matriceaMea = matrice;
                int startI = 0, endI = 2;//cate 2 caractere
                int incrementare = 2;

                while (endI <= text.Length)
                {
                    ciphertext += criptare(startI, incrementare, text, matriceaMea);
                    startI = endI;
                    endI += 2;
                }

                ///scriere in fisier cyphertext
                StreamWriter fout = new StreamWriter("criptatPlayfair.txt");
                fout.Write(ciphertext);
                fout.Close();
                MessageBox.Show("Mesaj decriptat scris in fisier!");

        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            //Create a OpenFileDialog instance to use
            OpenFileDialog openDialog = new OpenFileDialog();
            //We select what types are permitted to open
            openDialog.Filter = "Text Files (*.txt)|*.txt";
            //A title for the dialogbox
            openDialog.Title = "Open textfile";

            //If the user press cancel then end this void
            if (openDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            //Create a filestream
            FileStream fStr;
            try
            {
                //Set filestream to the result of the pick of the user
                fStr = new FileStream(openDialog.FileName, FileMode.Open, FileAccess.Read);

                //Create a streamreader, sr, to read the file
                StreamReader sr = new StreamReader(fStr);
                //While the end of the file has not been reached...
                while (sr.Peek() >= 0)
                {
                    //Create a 'line' that contains the current line of the textfile
                    string line = sr.ReadLine();

                }
                //Close the file so other modules can access it
                sr.Close();

                //Set public variable ourPath to the current path
                ourPath = Path.GetDirectoryName(openDialog.FileName);
                ourPath = ourPath + "\\" + Path.GetFileName(openDialog.FileName);

                //If something goes wrong, tell the user
            }
            catch (Exception)
            {
                MessageBox.Show("Error opening file", "Error message");
            }
            textBox2.Text = Path.GetFileName(openDialog.FileName);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
