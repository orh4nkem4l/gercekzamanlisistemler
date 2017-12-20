using System;

using System.Windows.Forms;
using System.IO.Ports;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace klima_projesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            serialPort1.PortName = "COM3";
            serialPort1.BaudRate = 9600;
            serialPort1.DtrEnable = true;
            serialPort1.Open();
            
            baglanti_kontrol();


        }
        MySqlConnection baglanti;
        public bool baglanti_kontrol()
        {
            try
            {
                baglanti = new MySqlConnection("Server=localhost;Database=klima;Uid=root;Pwd='mysql';");
                baglanti.Open();
                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                DateTime dt;
                MySqlCommand cmd;
                string komut;
                string[] degerler = serialPort1.ReadTo("\n").Split('-');
                string sicaklik = degerler[1];
                label3.Text = degerler[0];
                label4.Text = sicaklik;
                dt = DateTime.Now;
                string tarih = dt.ToString();


                komut = "insert into sicaklik(derece,tarih_saat) values('" + sicaklik + "','" + tarih + "');";
                cmd = new MySqlCommand(komut, baglanti);
                cmd.ExecuteNonQuery();
                komut = "insert into mesafe(uzaklik,tarih_saat) values('" + degerler[0] + "','" + tarih + "');";
                cmd = new MySqlCommand(komut, baglanti);
                cmd.ExecuteNonQuery();

                System.Threading.Thread.Sleep(100);
            }
            catch { }


        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
            baglanti.Close();
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.Write("s" + textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Write("u" + textBox2.Text);
        }


    }
}
