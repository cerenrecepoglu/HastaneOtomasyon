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

namespace E_Hastanem_Projesi
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);
        }
        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            this.Text = "E-HASTANEM";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox5.Text != textBox6.Text)
            {
                MessageBox.Show("Lütfen aynı şifreyi giriniz");
            }
            else if (textBox1.Text==""|| textBox2.Text == "" ||textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Lütfen boş alanları doldurunuz");
            }
            else if (textBox3.Text.Length!=11)
            {
                MessageBox.Show("Lütfen TC alanına 11 rakam giriniz");
            }
            else
            {
                string yol = ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=hastane.mdb");
                OleDbConnection baglanti = new OleDbConnection(yol);
                baglanti.Open();
                String ekleme = "insert into kullanici(ad,soyad,tc_no,kullanici_adi,sifre)values(@ad,@soyad,@tc_no,@kullanic_adi,@sifre)";
                OleDbCommand komut = new OleDbCommand(ekleme, baglanti);
                komut.Parameters.AddWithValue("@ad", textBox1.Text);
                komut.Parameters.AddWithValue("@soyad", textBox2.Text);
                komut.Parameters.AddWithValue("@tc_no", textBox3.Text);
                komut.Parameters.AddWithValue("@kullanici_adi", textBox4.Text);
                komut.Parameters.AddWithValue("@sifre", textBox5.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kaydınız Oluşturuldu");
                this.Hide();
                Form1 f = new Form1();
                f.Show();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }
        int parola = 0;
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            String sifre;
            sifre = textBox5.Text;
            int kucukharf = 0;
            int buyukharf = 0;
            int rakam = 0;
            int sembolskor = 0;

            int azkaraktersayisi = sifre.Length - Regex.Replace(sifre, "[a-z]", "").Length;
            kucukharf = Math.Min(2, azkaraktersayisi) * 10;

            int AZkaraktersayisi = sifre.Length - Regex.Replace(sifre, "[A-Z]", "").Length;
            buyukharf = Math.Min(2, AZkaraktersayisi) * 10;

            int rakamsayisi = sifre.Length - Regex.Replace(sifre, "[0-9]", "").Length;
            rakam = Math.Min(2, rakamsayisi) * 10;

            int sembol = sifre.Length - azkaraktersayisi - AZkaraktersayisi - rakamsayisi;
            sembolskor = Math.Min(2, sembol) * 10;

            parola = kucukharf + buyukharf + rakam + sembolskor;

            if (sifre.Length == 9)
            {
                parola += 10;
            }
            else if (sifre.Length == 10)
            {
                parola += 20;
            }

            label8.Text = "%" + Convert.ToString(parola);
            progressBar1.Value = parola;
        }
    }
}
