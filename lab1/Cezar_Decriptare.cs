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
    public partial class Cezar_Decriptare : Form
    {
        public Cezar_Decriptare()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            string sir = textBox2.Text.ToUpper();
            string tmp = Regex.Replace(sir, @"[0-9'""&:;-]", string.Empty);

            byte[] PTbytes = Encoding.ASCII.GetBytes(tmp);
            int n = int.Parse(textBox1.Text);

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
                    textBox3.Text += i.ToString();
                }
            }
            catch (Exception) { MessageBox.Show("Error"); }
        }
    }
}
