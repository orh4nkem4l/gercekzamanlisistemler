using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace klima_projesi
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            baglanti_kontrol();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            baglanti.Close();

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

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd;
            string komut;
            komut = "select * from giris where k_adi='" + textBox1.Text.Trim() + "' and sifre='" + 
                textBox2.Text.Trim() + "'";
            cmd = new MySqlCommand(komut, baglanti);
            MySqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                MessageBox.Show("Başarılı!");
                rd.Close();
                this.Hide();
                Form1 form1 = new Form1();
                form1.Show();
            }
            else
            {
                rd.Close();
                MessageBox.Show("Başarısız!");
            }
            
        }
    }
}
