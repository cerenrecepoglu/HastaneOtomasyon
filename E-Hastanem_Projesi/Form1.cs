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

namespace E_Hastanem_Projesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0;Data Source=hastane.mdb");
        OleDbConnection baglanti2 = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0;Data Source=calisanlar.mdb");

        public static string tc_no, adi, soyadi;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "E-HASTANEM";
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 ff = new Form2();
            this.Hide();
            ff.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox1.Text==""||textBox1.Text=="")
            {
                MessageBox.Show("Lütfen alanları doldurun");
            }
            else if(radioButton1.Checked==false&&radioButton2.Checked==false)
            {
                MessageBox.Show("Lütfen geçerli bir seçenek seçiniz");
            }
            else
            {
                baglanti.Open();
                OleDbCommand sorgu = new OleDbCommand("select * from kullanici", baglanti);
                OleDbDataReader okuma = sorgu.ExecuteReader();

                baglanti2.Open();
                OleDbCommand sorgu2 = new OleDbCommand("select * from kullanici", baglanti2);
                OleDbDataReader okuma2 = sorgu2.ExecuteReader();

                while (okuma.Read())
                {
                    if (radioButton1.Checked == true)
                    {
                        if (okuma["kullanici_adi"].ToString() == textBox1.Text && okuma["sifre"].ToString() == textBox2.Text)
                        {
                            tc_no = okuma.GetValue(0).ToString();
                            adi = okuma.GetValue(0).ToString();
                            soyadi = okuma.GetValue(0).ToString();
                            this.Hide();
                            Form3 frm3 = new Form3();
                            frm3.Show();
                            break;
                        }                   
                    }
                }
                baglanti.Close();
                while (okuma2.Read())
                {
                    if (radioButton2.Checked == true)
                    {
                        if (okuma2["kullanici_adi"].ToString() == textBox1.Text && okuma2["sifre"].ToString() == textBox2.Text)
                        {
                            tc_no = okuma2.GetValue(0).ToString();
                            adi = okuma2.GetValue(0).ToString();
                            soyadi = okuma2.GetValue(0).ToString();
                            this.Hide();
                            Form7 frm7 = new Form7();
                            frm7.Show();
                            break;
                        }
                    }
                }
                baglanti2.Close();
            }      
        }  
    } 
}


