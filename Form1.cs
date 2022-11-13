using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Priklad3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileStream stream = new FileStream(@"..\..\cisla.dat", FileMode.Create, FileAccess.Write);
            BinaryWriter pis = new BinaryWriter(stream);

            foreach (string line in textBox1.Lines)
            {
                double cislo = Convert.ToDouble(line);
                pis.Write(cislo);
            }
            pis.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream stream = new FileStream(@"..\..\cisla.dat", FileMode.Open, FileAccess.Read);
                BinaryReader cti = new BinaryReader(stream);
                try
                {
                    cti.BaseStream.Position = 0;
                    int n = Convert.ToInt32(textBox2.Text);
                    double soucet = 0;
                    int pocet = 0;

                    while (pocet < n)
                    {
                        double cislo = cti.ReadDouble();
                        soucet += cislo;
                        pocet++;

                    }

                    if (n == 0) 
                    {
                        throw new DivideByZeroException();
                    }
                    double prumer = soucet / n;
                    MessageBox.Show("PRUMER CISEL je: " + prumer.ToString());

                }

                catch (FormatException)
                {
                    MessageBox.Show("NEPLATNA DATA!");
                }
                catch (DivideByZeroException)
                {
                    MessageBox.Show("VYPOCET NELZE PROVEST!");
                }
                catch (EndOfStreamException)
                {
                    MessageBox.Show("V CISLA.DAT NENI TOLIK CISEL!");
                }
                finally
                {
                    cti.Close();
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("CISLA.DAT NEEXISTUJE!");
            }
        }
    }
}
