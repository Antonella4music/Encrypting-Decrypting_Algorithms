using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace lab1
{
    public partial class CezarFisier : Form
    {
        public CezarFisier()
        {
            InitializeComponent();
        }

        public string ourPath;

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
            textBox1.Text = Path.GetFileName(openDialog.FileName);

        }

        private string citireFisier()
        {
            string contents = "";
            using (StreamReader sr = new StreamReader(ourPath))
            {
                contents = sr.ReadToEnd();

            }

            return contents;
        }

        private void encryptBtn_Click(object sender, EventArgs e)
        {
            string mesajInput = "";

            try
              {
                //citire din fisier
                string line = null;
                /*TextReader readFile = new StreamReader(ourPath);
                line = readFile.ReadToEnd();*/
                line = citireFisier();
                                 
                  string sir = line.ToUpper();
                  byte[] PTbytes = Encoding.ASCII.GetBytes(sir);
                  int n = int.Parse(textBox2.Text);

                  try
                  {
                      foreach (byte b in PTbytes)
                      {
                          //sumez byte-ul din PT cu byte-ul lui N
                          int sum = b + n;
                          //verific daca se ajunge la Z, sa se reia de la capat
                          if (sum > 90)
                              sum -= 26;

                          char i = Convert.ToChar(sum);
                        mesajInput += i.ToString();
   
                    }
                  }
                  catch (Exception) { MessageBox.Show("Error"); }

                  //readFile.Close();
                 // readFile = null;
              }
              catch (IOException ex) { MessageBox.Show(ex.ToString()); }

            ///scriere in fisier cyphertext
            StreamWriter fout = new StreamWriter("ciphertext.txt");
            fout.Write(mesajInput);
            fout.Close();
            MessageBox.Show("CipherText scris in fisier!");

        }

        private void decryptBtn_Click(object sender, EventArgs e)
        {
            string mesajOutput = "";
            try
            {
                string line = null;
                /*TextReader readFile = new StreamReader(ourPath);
                line = readFile.ReadToEnd();*/
                line = citireFisier();

              //  TextReader readFile = new StreamReader(ourPath);
               // line = readFile.ReadToEnd();

            string sir = line.ToUpper();
            string tmp = Regex.Replace(sir, @"[0-9'""&:;-]", string.Empty);

            byte[] PTbytes = Encoding.ASCII.GetBytes(tmp);
            int n = int.Parse(textBox2.Text);

            try
            {
                foreach (byte b in PTbytes)
                {
                    //sumez byte-ul din PT cu byte-ul lui N
                    int sum = b - n;
                    //verific daca se ajunge la Z, sa se reia de la capat
                    if (sum < 65)
                        sum += 26;

                    char i = Convert.ToChar(sum);
                    mesajOutput += i.ToString();
                }
            }
            catch (Exception) { MessageBox.Show("Error"); }

            //readFile.Close();
           // readFile = null;
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
            }

            ///scriere in fisier cyphertext
            StreamWriter fout = new StreamWriter("decriptat.txt");
            fout.Write(mesajOutput);
            fout.Close();
            MessageBox.Show("Mesaj decriptat scris in fisier!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
