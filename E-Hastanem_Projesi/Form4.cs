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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        OleDbConnection baglantii = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0;Data Source=hastaneislemleri.mdb");
        private void Form4_Load(object sender, EventArgs e)
        {
            this.Text = "RANDEVU BÖLÜMÜ";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Lütfen boş alanları doldurunuz");
            }
            else if (textBox1.Text.Length != 11)
            {
                MessageBox.Show("Lütfen TC alanına 11 rakam giriniz");
            }
            else if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                MessageBox.Show("Lütfen boş alanları doldurunuz");
            }
            else if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 || comboBox3.SelectedIndex==-1)
            {
                MessageBox.Show("Lütfen boş alanları doldurunuz");
            }
            else
            {
                string yol = ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=hastaneislemleri.mdb");
                OleDbConnection baglanti = new OleDbConnection(yol);
                baglanti.Open();
                String ekleme = "insert into islemler(tc_no,ad,soyad,cinsiyet,bölüm,doktor,saat,tarih)values(@tc_no,@ad,@soyad,@cinsiyet,@bölüm,@doktor,@saat,@tarih)";
                OleDbCommand komut = new OleDbCommand(ekleme, baglanti);
                komut.Parameters.AddWithValue("@tc_no", textBox1.Text);
                komut.Parameters.AddWithValue("@ad", textBox2.Text);
                komut.Parameters.AddWithValue("@soyad", textBox3.Text);

                string cinsiyet = "";
                if (radioButton1.Checked == true)
                {
                    cinsiyet = radioButton1.Text;
                }
                else if (radioButton2.Checked == true)
                {
                    cinsiyet = radioButton2.Text;
                }
                komut.Parameters.AddWithValue("@cinsiyet", cinsiyet);

                string bölüm = "";
                if (comboBox1.SelectedItem.ToString() == "Çocuk Sağlığı ve Hastalıkları")
                {
                    comboBox2.Items.Add("Leyla Pakman");
                    comboBox2.Items.Add("Ali Gürsoy");
                    bölüm = comboBox1.SelectedItem.ToString();
                }
                else if (comboBox1.SelectedItem.ToString() == "Genel Cerrahi")
                {
                    comboBox2.Items.Add("Kerem Bülbül");
                    comboBox2.Items.Add("Fadime Sevgül");
                    bölüm = comboBox1.SelectedItem.ToString();
                }
                else if (comboBox1.SelectedItem.ToString() == "Göz Hastalıkları")
                {
                    comboBox2.Items.Add("Osman Şıpsevdi");
                    comboBox2.Items.Add("Aylin Usta");
                    bölüm = comboBox1.SelectedItem.ToString();
                }
                komut.Parameters.AddWithValue("@bölüm", bölüm);

                string doktor = "";
                if (comboBox2.SelectedItem.ToString() == "Leyla Pakman")
                {
                    comboBox1.Items.Add("Çocuk Sağlığı ve Hastalıkları");
                    doktor = comboBox2.SelectedItem.ToString();
                }
                else if (comboBox2.SelectedItem.ToString() == "Ali Gürsoy")
                {
                    comboBox1.Items.Add("Çocuk Sağlığı ve Hastalıkları");
                    doktor = comboBox2.SelectedItem.ToString();
                }
                else if (comboBox2.SelectedItem.ToString() == "Kerem Bülbül")
                {
                    comboBox1.Items.Add("Genel Cerrahi");
                    doktor = comboBox2.SelectedItem.ToString();
                }
                else if (comboBox2.SelectedItem.ToString() == "Osman Şıpsevdi")
                {
                    comboBox1.Items.Add("Göz Hastalıkları");
                    doktor = comboBox2.SelectedItem.ToString();
                }
                else if (comboBox2.SelectedItem.ToString() == "Aylin Usta")
                {
                    comboBox1.Items.Add("Göz Hastalıkları");
                    doktor = comboBox2.SelectedItem.ToString();
                }
                else if (comboBox2.SelectedItem.ToString() == "Fadime Sevgül")
                {
                    comboBox1.Items.Add("Genel Cerrahi");
                    doktor = comboBox2.SelectedItem.ToString();
                }
                komut.Parameters.AddWithValue("@doktor", doktor);

                string saat = "";
                saat = comboBox3.SelectedItem.ToString();   
                komut.Parameters.AddWithValue("@saat", saat);
                comboBox3.Items.RemoveAt(comboBox3.SelectedIndex);

                DateTime tarih = DateTime.Now;
                tarih = dateTimePicker1.Value;
                komut.Parameters.AddWithValue("@tarih", tarih);

                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Randevu Oluşturuldu");

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                comboBox1.SelectedItem = null;
                comboBox2.SelectedItem = null;
                comboBox3.SelectedItem = null;
                dateTimePicker1.Value = DateTime.Now;
            }
        }
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Çocuk Sağlığı ve Hastalıkları")
            {
                if (comboBox2.Text != "Leyla Pakman" || comboBox2.Text != "Ali Gürsoy")
                {
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add("Leyla Pakman");
                    comboBox2.Items.Add("Ali Gürsoy");
                }
            }
            else if (comboBox1.Text == "Genel Cerrahi")
            {
                if (comboBox2.Text != "Kerem Bülbül" || comboBox2.Text != "Fadime Sevgül")
                {
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add("Kerem Bülbül");
                    comboBox2.Items.Add("Fadime Sevgül");
                }
            }
            else if (comboBox1.Text == "Göz Hastalıkları")
            {
                if (comboBox2.Text != "Osman Şıpsevdi" || comboBox2.Text != "Aylin Usta")
                {
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add("Osman Şıpsevdi");
                    comboBox2.Items.Add("Aylin Usta");
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Leyla Pakman")
            {
                if (comboBox1.Text != "Çocuk Sağlığı ve Hastalıkları")
                {
                    comboBox1.Items.Clear();
                    comboBox1.Items.Add("Çocuk Sağlığı ve Hastalıkları");
                }
            }
            else if (comboBox2.Text == "Ali Gürsoy")
            {
                if (comboBox1.Text != "Çocuk Sağlığı ve Hastalıkları")
                {
                    comboBox1.Items.Clear();
                    comboBox1.Items.Add("Çocuk Sağlığı ve Hastalıkları");
                }
            }
            else if (comboBox2.Text == "Kerem Bülbül")
            {
                if (comboBox1.Text != "Genel Cerrahi")
                {
                    comboBox1.Items.Clear();
                    comboBox1.Items.Add("Genel Cerrahi");
                }
            }
            else if (comboBox2.Text == "Osman Şıpsevdi")
            {
                if (comboBox1.Text != "Göz Hastalıkları")
                {
                    comboBox1.Items.Clear();
                    comboBox1.Items.Add("Göz Hastalıkları");
                }
            }
            else if (comboBox2.Text == "Aylin Usta")
            {
                if (comboBox1.Text != "Göz Hastalıkları")
                {
                    comboBox1.Items.Clear();
                    comboBox1.Items.Add("Göz Hastalıkları");
                }
            }
            else if (comboBox2.Text == "Fadime Sevgül")
            {
                if (comboBox1.Text != "Genel Cerrahi")
                {
                    comboBox1.Items.Clear();
                    comboBox1.Items.Add("Genel Cerrahi");
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem = null;
            comboBox3.SelectedItem = null;
            dateTimePicker1.Value = DateTime.Now;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            this.Hide();
            frm3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Lütfen boş alanları doldurunuz");
            }
            else if (textBox1.Text.Length != 11)
            {
                MessageBox.Show("Lütfen TC alanına 11 rakam giriniz");
            }
            else if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                MessageBox.Show("Lütfen boş alanları doldurunuz");
            }
            else if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 || comboBox3.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen boş alanları doldurunuz");
            }
            else
            {

                
                string cinsiyet = "";
                if (radioButton1.Checked == true)
                {
                    cinsiyet = radioButton1.Text;
                }
                else if (radioButton2.Checked == true)
                {
                    cinsiyet = radioButton2.Text;
                }

                string bölüm = "";
                if (comboBox1.SelectedItem.ToString() == "Çocuk Sağlığı ve Hastalıkları")
                {
                    comboBox2.Items.Add("Leyla Pakman");
                    comboBox2.Items.Add("Ali Gürsoy");
                    bölüm = comboBox1.SelectedItem.ToString();
                }
                else if (comboBox1.SelectedItem.ToString() == "Genel Cerrahi")
                {
                    comboBox2.Items.Add("Kerem Bülbül");
                    comboBox2.Items.Add("Fadime Sevgül");
                    bölüm = comboBox1.SelectedItem.ToString();
                }
                else if (comboBox1.SelectedItem.ToString() == "Göz Hastalıkları")
                {
                    comboBox2.Items.Add("Osman Şıpsevdi");
                    comboBox2.Items.Add("Aylin Usta");
                    bölüm = comboBox1.SelectedItem.ToString();
                }

                string doktor = "";
                if (comboBox2.SelectedItem.ToString() == "Leyla Pakman")
                {
                    comboBox1.Items.Add("Çocuk Sağlığı ve Hastalıkları");
                    doktor = comboBox2.SelectedItem.ToString();
                }
                else if (comboBox2.SelectedItem.ToString() == "Ali Gürsoy")
                {
                    comboBox1.Items.Add("Çocuk Sağlığı ve Hastalıkları");
                    doktor = comboBox2.SelectedItem.ToString();
                }
                else if (comboBox2.SelectedItem.ToString() == "Kerem Bülbül")
                {
                    comboBox1.Items.Add("Genel Cerrahi");
                    doktor = comboBox2.SelectedItem.ToString();
                }
                else if (comboBox2.SelectedItem.ToString() == "Osman Şıpsevdi")
                {
                    comboBox1.Items.Add("Göz Hastalıkları");
                    doktor = comboBox2.SelectedItem.ToString();
                }
                else if (comboBox2.SelectedItem.ToString() == "Aylin Usta")
                {
                    comboBox1.Items.Add("Göz Hastalıkları");
                    doktor = comboBox2.SelectedItem.ToString();
                }
                else if (comboBox2.SelectedItem.ToString() == "Fadime Sevgül")
                {
                    comboBox1.Items.Add("Genel Cerrahi");
                    doktor = comboBox2.SelectedItem.ToString();
                }

                DateTime tarih = DateTime.Now;
                tarih = dateTimePicker1.Value;

                string saat = "";
                saat = comboBox3.SelectedItem.ToString();           
                
                baglantii.Open();
                OleDbCommand güncelleme = new OleDbCommand("update islemler set ad='" + textBox2.Text + "',soyad='" + textBox3.Text + "',cinsiyet='" + cinsiyet + "',bölüm='" + bölüm + "',doktor='" + doktor + "',tarih='" + tarih + "',saat='" + saat + "' where tc_no='"+textBox1.Text+"'",baglantii);
                comboBox3.Items.RemoveAt(comboBox3.SelectedIndex);
                güncelleme.ExecuteNonQuery();
                baglantii.Close();
                MessageBox.Show("Randevunuz Güncellendi");

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                comboBox1.SelectedItem = null;
                comboBox2.SelectedItem = null;
                comboBox3.SelectedItem = null;
                dateTimePicker1.Value = DateTime.Now;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool aramadurumu = false;
            if (textBox1.Text.Length != 11)
            {
                MessageBox.Show("Lütfen TC alanına 11 rakam giriniz");
            }
            else
            {
                baglantii.Open();
                OleDbCommand sorgum = new OleDbCommand("select * from islemler where tc_no='" + textBox1.Text + "'", baglantii);
                OleDbDataReader okumam = sorgum.ExecuteReader();

                while (okumam.Read())
                {
                    aramadurumu = true;
                    
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
                    dateTimePicker1.Text = okumam.GetValue(6).ToString();
                    comboBox3.Text = okumam.GetValue(7).ToString();
                    break;
                }
                if (aramadurumu == false)
                {
                    MessageBox.Show("Aranan Kayıt bulunamadı");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    comboBox1.SelectedItem = "";
                    comboBox2.SelectedItem = "";
                    comboBox3.SelectedItem = "";
                }
                baglantii.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bool aramadurumu = false;
            if (textBox1.Text.Length != 11)
            {
                MessageBox.Show("Lütfen TC alanına 11 rakam giriniz");
            }
            else
            {
                baglantii.Open();
                OleDbCommand sorgum = new OleDbCommand("select * from islemler where tc_no='" + textBox1.Text + "'", baglantii);
                OleDbDataReader okumam = sorgum.ExecuteReader();

                while (okumam.Read())
                {
                    aramadurumu = true;

                    OleDbCommand silme = new OleDbCommand("delete from islemler where tc_no='" + textBox1.Text+"'", baglantii);
                    silme.ExecuteNonQuery();
                    MessageBox.Show("Randevunuz iptal edilmiştir");
                }
                if (aramadurumu == false)
                {
                    MessageBox.Show("Aranan Kayıt bulunamadı");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    comboBox1.SelectedItem = "";
                    comboBox2.SelectedItem = "";
                    comboBox3.SelectedItem = "";
                }
                baglantii.Close();
            }
        }
    }
}
