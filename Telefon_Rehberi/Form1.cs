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
using System.Text.RegularExpressions;

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

            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";


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

                    if(textBox4.Text.Length <10)
                    {
                        MessageBox.Show("Telefon numarası 10 haneli olmalı ve 0'la başlamamalı", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        bag.Open();
                        komut.Connection = bag;
                        komut.CommandText = "insert into Tablo1 (Isim,Soyisim,Telefon,Adres) values ('" + isim + "','" + soyisim + "','" + telefon + "','" + adres + "')";
                        komut.ExecuteNonQuery();
                        MessageBox.Show("Kişi Kayıt Edildi!");
                        bag.Close();

                        

                        listele();

                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                    }

                    
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Tekrar Deneyiniz.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string secili;
            secili = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            try
            {
                DialogResult onay;
                onay = MessageBox.Show("Bu işlemi yapmak istediğinize emin misiniz? ", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (onay == DialogResult.Yes)
                {
                    string isim, soyisim, telefon, adres;


                    isim = textBox2.Text;
                    soyisim = textBox3.Text;
                    telefon = textBox4.Text;
                    adres = textBox5.Text;


                    if (textBox4.Text.Length < 10)
                    {
                        MessageBox.Show("Telefon numarası 10 haneli olmalı ve 0'la başlamamalı", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        bag.Open();
                        komut.Connection = bag;
                        komut.CommandText = "update Tablo1 set Isim = '" + isim + "',Soyisim = '" + soyisim + "',Telefon = '" + telefon + "' where Kimlik=" + secili;
                        komut.ExecuteNonQuery();
                        MessageBox.Show("Kişi Güncellendi!");
                        bag.Close();

                        listele();
                    }
                   
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Tekrar Deneyiniz");
            }


        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string secili;
            secili = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            try
            {
                DialogResult onay;
                onay = MessageBox.Show("Silmek istediğinize emin misiniz? ", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (onay == DialogResult.Yes)
                {
                    bag.Open();
                    komut.Connection = bag;

                    komut.CommandText = "delete from Tablo1 where Kimlik=" + secili;
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Kişi Silindi!");
                    bag.Close();

                    listele();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string input = textBox4.Text;
                string pattern = "^[0-9]*$"; // Sadece rakamları kabul eden düzenli ifade
                if (!Regex.IsMatch(input, pattern))
                {
                    textBox4.Text = ""; // Metni temizle
                }

                if (textBox4.Text.Length > 10)
                {
                    textBox4.Text = textBox4.Text.Substring(0, 10); // 11 karakterden fazlasını kes
                    textBox4.SelectionStart = textBox4.Text.Length; // Cursor'ü sona taşı
                }
            }
            catch
            {
                MessageBox.Show("Telefon numarası 10 haneli olmalı ve 0'la başlamamalı","HATA",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
        }


        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true; // Olayın işlenmesini durdur
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true; // Olayın işlenmesini durdur
            }
        }
    }
    }
