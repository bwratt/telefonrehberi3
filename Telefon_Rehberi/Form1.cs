using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Telefon_Rehberi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection bag = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0;Data Source=telefonrehberi.mdb");
        OleDbCommand komut = new OleDbCommand();

        DataTable veritablosu = new DataTable();

        public void listele()
        {
            try
            {
                veritablosu.Clear();
                OleDbDataAdapter siringa = new OleDbDataAdapter("select * from Tablo1", bag);
                siringa.Fill(veritablosu);
                dataGridView1.DataSource = veritablosu;


            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.ToString());

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            veritablosu.Clear();

            OleDbDataAdapter siringa = new OleDbDataAdapter("select * from Tablo1 where Isim like '" + textBox1.Text + "%'", bag);
            siringa.Fill(veritablosu);
            dataGridView1.DataSource = veritablosu;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult onay;
                onay = MessageBox.Show("Bu işlemi yapmak istediğinize emin misiniz? ", "DİKKAT", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (onay == DialogResult.Yes)
                {
                    string isim, soyisim, telefon, adres;
                    

                    isim = textBox2.Text;
                    soyisim = textBox3.Text;
                    telefon = textBox4.Text;
                    adres = textBox5.Text;

                    bag.Open();
                    komut.Connection = bag;
                    komut.CommandText = "insert into Tablo1 (Isim,Soyisim,Telefon,Adres) values ('" + isim + "','" + soyisim + "','" + telefon + "','" + adres + "')";
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Kişi Kayıt Edildi!");
                    bag.Close();

                    listele();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Tekrar Deneyiniz.");
            }
        }
    }
}
