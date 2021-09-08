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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        OleDbConnection baglantii = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0;Data Source=doktorlar.mdb");
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);
        }
        private void Form7_Load(object sender, EventArgs e)
        {
            this.Text = "YÖNETİCİ İŞLEMLERİ";
        }
        private void button5_Click(object sender, EventArgs e)
        {
            bool aramadurumu = false;
            if (textBox1.Text.Length != 11)
            {
                MessageBox.Show("Lütfen TC alanına 11 rakam giriniz");
            }
            else
            {
                baglantii.Open();
                OleDbCommand sorgum = new OleDbCommand("select * from doctor where tc_no='" + textBox1.Text + "'", baglantii);
                OleDbDataReader okumam = sorgum.ExecuteReader();

                while(okumam.Read())
                {
                    aramadurumu = true;
                    try
                    {
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\resimler\\" + okumam.GetValue(0).ToString() + ".png");
                    }
                    catch 
                    {
                        MessageBox.Show("resim yok");
                       
                    }
                    textBox2.Text = okumam.GetValue(1).ToString();
                    textBox3.Text = okumam.GetValue(2).ToString();
                    if (okumam.GetValue(3).ToString() == "Kadın")
                    {
                        radioButton1.Checked = true;
                    }
                    else
                    {
                        radioButton2.Checked = true;
                    }
                    comboBox1.Text = okumam.GetValue(4).ToString();
                    comboBox2.Text = okumam.GetValue(5).ToString();
                    break;
                }
                if(aramadurumu==false)
                {
                    MessageBox.Show("Aranan Kayıt bulunamadı");
                    pictureBox1.Image = null;
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    comboBox1.SelectedItem = "";
                    comboBox2.SelectedItem = "";
                }
                baglantii.Close();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            this.Hide();
            frm1.Show();

        }
    }
}

   
