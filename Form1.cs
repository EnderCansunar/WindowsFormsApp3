using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace WindowsFormsApp3
{
    public partial class Form1 : MaterialSkin.Controls.MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        db _ec1 = new db();

        private void Form1_Load(object sender, EventArgs e)
        {


            _ec1.baglantiinvise.Close();

            string sql1 = "Select * FROM invisehastalar";
            string sql2 = "select * FROM invisetablo";
            DataTable paketbilgisi = new DataTable();
            DataTable hastalardatası = new DataTable();
            MySqlDataAdapter hastalaradapter = new MySqlDataAdapter();
            MySqlDataAdapter hastabilgisiadapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand
            {
                CommandText = sql1,
                Connection = _ec1.baglantiinvise
            };
            MySqlCommand command1 = new MySqlCommand
            {
                CommandText = sql2,
                Connection = _ec1.baglantiinvise
            };
            hastabilgisiadapter.SelectCommand = command1;
            hastabilgisiadapter.Fill(paketbilgisi);
            dataGridView3.DataSource= paketbilgisi;
            //comboBox1.ValueMember = "paketucreti";
            //comboBox1.DisplayMember = "paketadi";
            //comboBox1.DataSource = paketbilgisi;
            this.dataGridView3.Columns["paketucreti"].Visible = false;
            dataGridView4.DataSource = paketbilgisi;
            this.dataGridView4.Columns["paketucreti"].Visible = false;

            hastalaradapter.SelectCommand = command;
            hastalaradapter.Fill(hastalardatası);
            dataGridView1.DataSource = hastalardatası;
            _ec1.baglantiinvise.Close();
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;
            richTextBox2.MaxLength = 11;// Tc 
            richTextBox9.MaxLength = 11;// Tel
            richTextBox12.MaxLength = 11;// baba tel
            richTextBox16.MaxLength = 11;// anne tel
            textBox6.MaxLength = 11;//tc ara
            richTextBox24.MaxLength = 11;// hastabilgileri TC
            richTextBox5.MaxLength = 11;// hastabilgileri Tel
            richTextBox30.MaxLength = 11;// hastabilgileri baba tel
            //richTextBox33.MaxLength = 11;// hastabilgileri anne tel
            richTextBox18.Text = "0";//tedaviücreti
                                     // dateTimePicker1.
            MySqlCommand komut3 = new MySqlCommand();
            komut3.CommandText = " select invisehastalar.iDosyano from invisehastalar order by invisehastalar.iDosyaNo desc limit 1 ";
            komut3.Connection = _ec1.baglantiinvise;
            _ec1.baglantiinvise.Open();
            komut3.ExecuteNonQuery();  //ExecuteNonQuery();
            MySqlDataReader dosyanosoku = komut3.ExecuteReader();
            if (dosyanosoku.Read())
            {
                richTextBox1.Text = ((Convert.ToInt32(dosyanosoku["iDosyaNo"]) + 1)).ToString();
            }
            else
            {
                richTextBox1.Text = "veri cekilemedi";
            }


            _ec1.baglantiinvise.Close();

            
        }

        private void MaterialRaisedButton8_Click(object sender, EventArgs e)//hasta kayıt butonu
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand eklem = new MySqlCommand("insert into invisehastalar  (idosyano,itc,iadi,isoyadi,idogumtarihi,itelefon,iisyeri,imeslegi,iReferans,iadresi,ibabatel,iannetel,ibabaadi,ianneadi,ibabameslegi,iannemeslegi,iKayittarihi,itoplamucret,iikametyeri,inotlar,ipaketbilgisi,ipaketucreti,eposta) values ('" + richTextBox1.Text + "','" + richTextBox2.Text + "','" + richTextBox3.Text + "','" + richTextBox4.Text + "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + richTextBox9.Text + "','" + richTextBox7.Text + "','" + richTextBox6.Text + "','" + richTextBox8.Text + "','" + richTextBox15.Text + "','" + richTextBox12.Text + "','" + richTextBox16.Text + "','" + richTextBox10.Text + "','" + richTextBox13.Text + "','" + richTextBox11.Text + "','" + richTextBox14.Text + "','" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "','" + richTextBox18.Text + "' ,'" + richTextBox35.Text + "','" + richTextBox36.Text + "','" + materialLabel75.Text + "','" + textBox5.Text + "','" + richTextBox94.Text + "')", _ec1.baglantiinvise);
                object sonucm1 = null;
                sonucm1 = eklem.ExecuteNonQuery();

                MySqlCommand eklem1 = new MySqlCommand("insert into iborclar (iTc,iborcmiktari,itarih,iborcno) values ('" + richTextBox2.Text + "','" + richTextBox18.Text + "','" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "','" + richTextBox1.Text + "')", _ec1.baglantiinvise);
                object sonucm2 = null;
                sonucm2 = eklem1.ExecuteNonQuery();
                string bosa;
                bosa = "0";
               // MessageBox.Show(bosa);
                MySqlCommand eklem2 = new MySqlCommand("insert into iodemeler  ( iodememiktari, itc,iborcno, iodemetarihi) values ('" + bosa.ToString() + "','" + richTextBox2.Text + "','" + richTextBox1.Text + "','" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "')", _ec1.baglantiinvise);
                object sonucm3 = null;
                sonucm3 = eklem2.ExecuteNonQuery();

                if (sonucm1 != null & sonucm2 != null & sonucm3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme eklenemedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                _ec1.baglantiinvise.Close();

                string sqlinvise = "SELECT * FROM invisehastalar";
                DataTable datainvise = new DataTable();
                MySqlDataAdapter adapterinvise = new MySqlDataAdapter();
                MySqlCommand commandinvise = new MySqlCommand();

                commandinvise.CommandText = sqlinvise;
                commandinvise.Connection = _ec1.baglantiinvise;
                adapterinvise.SelectCommand = commandinvise;
                adapterinvise.Fill(datainvise);
                dataGridView1.DataSource = datainvise;


            }

            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void MaterialRaisedButton1_Click(object sender, EventArgs e)
        {
            textBox5.Text = "".ToString();
            materialLabel75.Text = "".ToString();
            foreach (Control item in tabPage1.Controls)
            {
                if (item is RichTextBox)
                {
                    RichTextBox txt = item as RichTextBox;
                    txt.Clear();
                }
                dateTimePicker1.Value = DateTime.Today;
                dateTimePicker2.Value = DateTime.Today;
                _ec1.baglantiinvise.Close();
                MySqlCommand komut3 = new MySqlCommand();
                komut3.CommandText = " select invisehastalar.iDosyano from invisehastalar order by invisehastalar.iDosyaNo desc limit 1 ";
                komut3.Connection = _ec1.baglantiinvise;
                _ec1.baglantiinvise.Open();
                komut3.ExecuteNonQuery();  //ExecuteNonQuery();
                MySqlDataReader dosyanosoku = komut3.ExecuteReader();
                if (dosyanosoku.Read())
                {
                    richTextBox1.Text = ((Convert.ToInt32(dosyanosoku["iDosyaNo"]) + 1)).ToString();
                }
                else
                {
                    richTextBox1.Text = "veri cekilemedi";
                }

                richTextBox18.Text = "0";
            }
        }

        private void DataGridView1_DoubleClick(object sender, EventArgs e)
        {

            materialLabel52.Text = "".ToString();
            materialLabel54.Text = "".ToString();
            materialLabel53.Text = "".ToString();
            materialLabel56.Text = "".ToString();
            materialLabel57.Text = "".ToString();
            materialLabel58.Text = "".ToString();
            materialLabel59.Text = "".ToString();
            textBox8.Text = "".ToString();
            materialLabel76.Text="".ToString();

            textBox6.Text= dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox8.Text= dataGridView1.CurrentRow.Cells[21].Value.ToString();
            materialLabel76.Text= dataGridView1.CurrentRow.Cells[20].Value.ToString();
            richTextBox25.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            richTextBox24.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            richTextBox23.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            richTextBox22.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            richTextBox5.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            richTextBox21.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            richTextBox20.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            richTextBox19.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            richTextBox34.Text = dataGridView1.CurrentRow.Cells[16].Value.ToString();
            richTextBox32.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();
            richTextBox21.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            richTextBox30.Text = dataGridView1.CurrentRow.Cells[118].Value.ToString();
            richTextBox29.Text = dataGridView1.CurrentRow.Cells[13].Value.ToString();
            richTextBox28.Text = dataGridView1.CurrentRow.Cells[15].Value.ToString();
            richTextBox38.Text = dataGridView1.CurrentRow.Cells[119].Value.ToString();
            richTextBox31.Text = dataGridView1.CurrentRow.Cells[14].Value.ToString();
            richTextBox91.Text = dataGridView1.CurrentRow.Cells[120].Value.ToString();
            richTextBox92.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
            richTextBox93.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            // richTextBox26.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
            //richTextBox27.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            richTextBox39.Text = dataGridView1.CurrentRow.Cells[19].Value.ToString();
            dateTimePicker3.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[5].Value.ToString());
            dateTimePicker4.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[4].Value.ToString());










            richTextBox40.Text = dataGridView1.CurrentRow.Cells[68].Value.ToString();
            richTextBox41.Text = dataGridView1.CurrentRow.Cells[66].Value.ToString();
            richTextBox42.Text = dataGridView1.CurrentRow.Cells[64].Value.ToString();
            richTextBox43.Text = dataGridView1.CurrentRow.Cells[62].Value.ToString();
            richTextBox44.Text = dataGridView1.CurrentRow.Cells[60].Value.ToString();
            richTextBox45.Text = dataGridView1.CurrentRow.Cells[58].Value.ToString();
            richTextBox46.Text = dataGridView1.CurrentRow.Cells[56].Value.ToString();
            richTextBox47.Text = dataGridView1.CurrentRow.Cells[54].Value.ToString();
            richTextBox48.Text = dataGridView1.CurrentRow.Cells[52].Value.ToString();
            richTextBox49.Text = dataGridView1.CurrentRow.Cells[50].Value.ToString();
            richTextBox50.Text = dataGridView1.CurrentRow.Cells[48].Value.ToString();
            richTextBox51.Text = dataGridView1.CurrentRow.Cells[46].Value.ToString();
            richTextBox52.Text = dataGridView1.CurrentRow.Cells[44].Value.ToString();
            richTextBox53.Text = dataGridView1.CurrentRow.Cells[42].Value.ToString();
            richTextBox54.Text = dataGridView1.CurrentRow.Cells[40].Value.ToString();
            richTextBox55.Text = dataGridView1.CurrentRow.Cells[38].Value.ToString();
            richTextBox56.Text = dataGridView1.CurrentRow.Cells[36].Value.ToString();
            richTextBox57.Text = dataGridView1.CurrentRow.Cells[34].Value.ToString();
            richTextBox58.Text = dataGridView1.CurrentRow.Cells[32].Value.ToString();
            richTextBox59.Text = dataGridView1.CurrentRow.Cells[30].Value.ToString();
            richTextBox60.Text = dataGridView1.CurrentRow.Cells[28].Value.ToString();
            richTextBox61.Text = dataGridView1.CurrentRow.Cells[26].Value.ToString();
            richTextBox62.Text = dataGridView1.CurrentRow.Cells[24].Value.ToString();
            richTextBox63.Text = dataGridView1.CurrentRow.Cells[22].Value.ToString();
            richTextBox64.Text = dataGridView1.CurrentRow.Cells[116].Value.ToString();
            richTextBox65.Text = dataGridView1.CurrentRow.Cells[114].Value.ToString();
            richTextBox66.Text = dataGridView1.CurrentRow.Cells[112].Value.ToString();
            richTextBox67.Text = dataGridView1.CurrentRow.Cells[110].Value.ToString();
            richTextBox68.Text = dataGridView1.CurrentRow.Cells[108].Value.ToString();
            richTextBox69.Text = dataGridView1.CurrentRow.Cells[106].Value.ToString();
            richTextBox70.Text = dataGridView1.CurrentRow.Cells[104].Value.ToString();
            richTextBox71.Text = dataGridView1.CurrentRow.Cells[102].Value.ToString();
            richTextBox72.Text = dataGridView1.CurrentRow.Cells[100].Value.ToString();
            richTextBox73.Text = dataGridView1.CurrentRow.Cells[98].Value.ToString();
            richTextBox74.Text = dataGridView1.CurrentRow.Cells[96].Value.ToString();
            richTextBox75.Text = dataGridView1.CurrentRow.Cells[94].Value.ToString();
            richTextBox76.Text = dataGridView1.CurrentRow.Cells[92].Value.ToString();
            richTextBox77.Text = dataGridView1.CurrentRow.Cells[90].Value.ToString();
            richTextBox78.Text = dataGridView1.CurrentRow.Cells[88].Value.ToString();
            richTextBox79.Text = dataGridView1.CurrentRow.Cells[86].Value.ToString();
            richTextBox80.Text = dataGridView1.CurrentRow.Cells[84].Value.ToString();
            richTextBox81.Text = dataGridView1.CurrentRow.Cells[82].Value.ToString();
            richTextBox82.Text = dataGridView1.CurrentRow.Cells[80].Value.ToString();
            richTextBox83.Text = dataGridView1.CurrentRow.Cells[78].Value.ToString();
            richTextBox84.Text = dataGridView1.CurrentRow.Cells[76].Value.ToString();
            richTextBox85.Text = dataGridView1.CurrentRow.Cells[74].Value.ToString();
            richTextBox86.Text = dataGridView1.CurrentRow.Cells[72].Value.ToString();
            richTextBox87.Text = dataGridView1.CurrentRow.Cells[70].Value.ToString();

            if (dataGridView1.CurrentRow.Cells[23].Value.ToString() == "")
            {
                dateTimePicker28.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker28.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[23].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[25].Value.ToString() == "")
            {
                dateTimePicker5.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker5.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[25].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[27].Value.ToString() == "")
            {
                dateTimePicker7.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker7.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[27].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[29].Value.ToString() == "")
            {
                dateTimePicker6.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker6.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[29].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[31].Value.ToString() == "")
            {
                dateTimePicker9.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker9.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[31].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[33].Value.ToString() == "")
            {
                dateTimePicker8.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker8.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[33].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[35].Value.ToString() == "")
            {
                dateTimePicker11.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker11.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[35].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[37].Value.ToString() == "")
            {
                dateTimePicker10.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker10.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[37].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[39].Value.ToString() == "")
            {
                dateTimePicker13.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker14.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[39].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[41].Value.ToString() == "")
            {
                dateTimePicker12.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker12.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[41].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[43].Value.ToString() == "")
            {
                dateTimePicker15.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker15.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[43].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[45].Value.ToString() == "")
            {
                dateTimePicker14.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker14.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[45].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[47].Value.ToString() == "")
            {
                dateTimePicker27.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker27.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[47].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[49].Value.ToString() == "")
            {
                dateTimePicker26.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker26.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[49].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[51].Value.ToString() == "")
            {
                dateTimePicker25.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker25.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[51].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[53].Value.ToString() == "")
            {
                dateTimePicker24.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker24.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[53].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[55].Value.ToString() == "")
            {
                dateTimePicker23.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker23.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[55].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[57].Value.ToString() == "")
            {
                dateTimePicker22.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker22.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[57].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[59].Value.ToString() == "")
            {
                dateTimePicker21.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker21.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[59].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[61].Value.ToString() == "")
            {
                dateTimePicker20.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker20.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[61].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[63].Value.ToString() == "")
            {
                dateTimePicker19.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker19.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[63].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[65].Value.ToString() == "")
            {
                dateTimePicker18.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker18.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[65].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[67].Value.ToString() == "")
            {
                dateTimePicker17.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker17.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[67].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[69].Value.ToString() == "")
            {
                dateTimePicker16.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker16.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[69].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[71].Value.ToString() == "")
            {
                dateTimePicker40.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker40.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[71].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[73].Value.ToString() == "")
            {
                dateTimePicker39.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker39.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[73].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[75].Value.ToString() == "")
            {
                dateTimePicker38.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker38.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[75].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[77].Value.ToString() == "")
            {
                dateTimePicker37.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker37.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[77].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[79].Value.ToString() == "")
            {
                dateTimePicker36.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker36.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[79].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[81].Value.ToString() == "")
            {
                dateTimePicker35.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker35.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[81].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[83].Value.ToString() == "")
            {
                dateTimePicker34.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker34.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[83].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[85].Value.ToString() == "")
            {
                dateTimePicker33.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker33.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[85].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[87].Value.ToString() == "")
            {
                dateTimePicker32.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker32.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[87].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[89].Value.ToString() == "")
            {
                dateTimePicker31.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker31.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[89].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[91].Value.ToString() == "")
            {
                dateTimePicker30.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker30.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[91].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[93].Value.ToString() == "")
            {
                dateTimePicker29.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker29.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[93].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[95].Value.ToString() == "")
            {
                dateTimePicker62.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker62.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[95].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[97].Value.ToString() == "")
            {
                dateTimePicker61.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker61.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[97].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[99].Value.ToString() == "")
            {
                dateTimePicker60.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker60.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[99].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[101].Value.ToString() == "")
            {
                dateTimePicker59.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker59.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[101].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[103].Value.ToString() == "")
            {
                dateTimePicker58.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker58.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[103].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[105].Value.ToString() == "")
            {
                dateTimePicker57.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker57.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[105].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[107].Value.ToString() == "")
            {
                dateTimePicker56.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker56.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[107].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[109].Value.ToString() == "")
            {
                dateTimePicker55.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker55.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[109].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[111].Value.ToString() == "")
            {
                dateTimePicker54.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker54.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[111].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[113].Value.ToString() == "")
            {
                dateTimePicker53.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker53.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[113].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[115].Value.ToString() == "")
            {
                dateTimePicker52.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker52.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[115].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[117].Value.ToString() == "")
            {
                dateTimePicker51.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker51.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[117].Value.ToString());

            }
            _ec1.baglantiinvise.Close();






        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            if (textBox1.Text.Trim() == "")
            {
                _ec1.baglantiinvise.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM invisehastalar", _ec1.baglantiinvise);
                DataTable data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
                _ec1.baglantiinvise.Close();
            }
            else
            {
                _ec1.baglantiinvise.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM invisehastalar where iAdi like '%" + textBox1.Text + "%'", _ec1.baglantiinvise);
                DataTable data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
                _ec1.baglantiinvise.Close();
            }
        } // ad ile arama hasta bilgileri

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            if (textBox2.Text.Trim() == "")
            {
                _ec1.baglantiinvise.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM invisehastalar", _ec1.baglantiinvise);
                DataTable data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
                _ec1.baglantiinvise.Close();
            }
            else
            {
                _ec1.baglantiinvise.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM invisehastalar where iSoyadi like '%" + textBox2.Text + "%'", _ec1.baglantiinvise);
                DataTable data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
                _ec1.baglantiinvise.Close();
            }
        }//soyad ile arama hasta bilgileri

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            if (textBox3.Text.Trim() == "")
            {
                _ec1.baglantiinvise.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM invisehastalar", _ec1.baglantiinvise);
                DataTable data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
                _ec1.baglantiinvise.Close();
            }
            else
            {
                _ec1.baglantiinvise.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM invisehastalar where iTc like '%" + textBox3.Text + "%'", _ec1.baglantiinvise);
                DataTable data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
                _ec1.baglantiinvise.Close();
            }
        }//tc arama hasta bilgileri

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            if (textBox4.Text.Trim() == "")
            {
                _ec1.baglantiinvise.Open();
                MySqlDataAdapter adapter4 = new MySqlDataAdapter("SELECT * FROM invisehastalar", _ec1.baglantiinvise);
                DataTable data4 = new DataTable();
                adapter4.Fill(data4);
                dataGridView1.DataSource = data4;
                _ec1.baglantiinvise.Close();
            }
            else
            {
                _ec1.baglantiinvise.Open();
                MySqlDataAdapter adapter4 = new MySqlDataAdapter("SELECT * FROM invisehastalar where idosyano like '%" + textBox4.Text + "%'", _ec1.baglantiinvise);
                DataTable data4 = new DataTable();
                adapter4.Fill(data4);
                dataGridView1.DataSource = data4;
                _ec1.baglantiinvise.Close();
            }
        }//dosya no ile arama hasta bilgileri

        private void TextBox6_TextChanged(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            materialLabel52.Text = "".ToString();
            materialLabel54.Text = "".ToString();
            materialLabel53.Text = "".ToString();
            materialLabel56.Text = "".ToString();
            materialLabel57.Text = "".ToString();
            materialLabel58.Text = "".ToString();
            materialLabel59.Text = "".ToString();

            if (textBox6.Text.Trim() == "")
            {
                _ec1.baglantiinvise.Open();
                DataTable databorclar = new DataTable();
                MySqlDataAdapter adapterborclar = new MySqlDataAdapter();
                MySqlCommand commandborclar = new MySqlCommand
                {
                    CommandText = "select idosyano , invisehastalar.itc, iadi, isoyadi, iborcmiktari, iodememiktari, iodemetarihi, iodemeler.iodemeyialan, iodemeler.iodemeno from invisehastalar join iborclar on invisehastalar.itc=iborclar.itc join iodemeler on iborclar.iborcno=iodemeler.iborcno where invisehastalar.itc= '" + textBox6.Text + "'",
                    Connection = _ec1.baglantiinvise

                };
                adapterborclar.SelectCommand = commandborclar;
                adapterborclar.Fill(databorclar);
                dataGridView2.DataSource = databorclar;
                _ec1.baglantiinvise.Close();
                materialLabel52.Text = "".ToString();
                materialLabel54.Text = "".ToString();
                materialLabel53.Text = "".ToString();
                materialLabel56.Text = "".ToString();
                materialLabel57.Text = "".ToString();
                materialLabel58.Text = "".ToString();
                materialLabel59.Text = "".ToString();


            }
            else
            {
                _ec1.baglantiinvise.Open();
                DataTable databorclar = new DataTable();
                MySqlDataAdapter adapterborclar = new MySqlDataAdapter();
                MySqlCommand commandborclar = new MySqlCommand
                {

                    CommandText = "select idosyano , invisehastalar.itc, iadi, isoyadi, iborcmiktari, iodememiktari, iodemetarihi, iodemeler.iodemeyialan, iodemeler.iodemeno from invisehastalar join iborclar on invisehastalar.itc=iborclar.itc join iodemeler on iborclar.iborcno=iodemeler.iborcno where invisehastalar.itc= '" + textBox6.Text + "'",
                    // CommandText = "select	odemeler.borcno	, odemeler.odememiktari , odemeler.odemetarihi, hastalar.adi	, hastalar.soyadi, hastalar.tc  from odemeler , borclar , hastalar where odemeler.borcno = borclar.borcno and borclar.tc= '" + textBox6.Text + "' and borclar.tc = hastalar.tc",
                    //CommandText = "select dosya_no , hastalar.tc, adi, soyadi, borcm, odememiktari,odemetarihi from hastalar join borclar on hastalar.tc=borclar.tc join odemeler on borclar.tc=odemeler.tc  where hastalar.tc= '" + textBox6.Text + "'",

                    Connection = _ec1.baglantiinvise

                };
                adapterborclar.SelectCommand = commandborclar;
                adapterborclar.Fill(databorclar);
                dataGridView2.DataSource = databorclar;
                _ec1.baglantiinvise.Close();


            }//tc ile ödeme sorgusu

        }

        private void MaterialFlatButton9_Click(object sender, EventArgs e)
        {
            try
            {

                decimal borc = 0;
                decimal odeme = 0;
                _ec1.baglantiinvise.Open();
                MySqlCommand cmd = new MySqlCommand("select sum(iborclar.iborcmiktari) as borctoplami from iborclar , invisehastalar where    iborclar.itc= '" + textBox6.Text + "' and iborclar.itc = invisehastalar.itc", _ec1.baglantiinvise);
                int Count = Convert.ToInt32(cmd.ExecuteScalar());
                if (Count != 0)
                {
                    materialLabel56.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                    materialLabel57.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                    materialLabel58.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                    materialLabel59.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
                    MySqlDataReader oku = cmd.ExecuteReader();


                    while (oku.Read())
                    {
                        borc = Convert.ToDecimal(oku["borctoplami"]);

                    }

                }

                _ec1.baglantiinvise.Close();
                _ec1.baglantiinvise.Open();
                cmd = new MySqlCommand("select sum(iodemeler.iodememiktari) as odemetoplami from iodemeler, iborclar , invisehastalar where iborclar.itc = iodemeler.itc and  iborclar.itc= '" + textBox6.Text + "' and iborclar.itc = invisehastalar.itc", _ec1.baglantiinvise);
                Count = Convert.ToInt32(cmd.ExecuteScalar());
                if (Count != 0)
                {
                    MySqlDataReader oku = cmd.ExecuteReader();
                    while (oku.Read())
                    {
                        odeme = Convert.ToDecimal(oku["odemetoplami"]);

                    }
                }

                _ec1.baglantiinvise.Close();
                materialLabel52.Text = (borc).ToString();
                materialLabel54.Text = (odeme).ToString();
                materialLabel53.Text = (borc - odeme).ToString();

            }
            catch
            {
                MessageBox.Show("Tc yanlış veya hatalı Lütfen Tekrar deneyiniz");
                materialLabel52.Text = "".ToString();
                materialLabel54.Text = "".ToString();
                materialLabel53.Text = "".ToString();
            }
        }

        private void MaterialRaisedButton6_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle = new MySqlCommand("update invisehastalar SET invisehastalar.itc= '" + richTextBox24.Text + "', iadi='" + richTextBox23.Text + "',isoyadi='" + richTextBox22.Text + "', idogumtarihi='" + dateTimePicker3.Value.ToString("yyyy-MM-dd") + "', ikayittarihi='" + dateTimePicker4.Value.ToString("yyyy-MM-dd") + "' , itelefon='" + richTextBox5.Text + "',iisyeri='" + richTextBox20.Text + "',eposta='" + richTextBox91.Text + "',imeslegi='" + richTextBox21.Text + "',ireferans='" + richTextBox19.Text + "',iadresi='" + richTextBox93.Text + "',ibabatel='" + richTextBox30.Text + "',iannetel='" + richTextBox38.Text + "', ibabaadi='" + richTextBox32.Text + "', ianneadi='" + richTextBox29.Text + "', ibabameslegi= '" + richTextBox28.Text + "', iannemeslegi='" + richTextBox31.Text + "', itoplamucret='" + richTextBox34.Text + "', iikametyeri='" + richTextBox92.Text + "', inotlar= '" + richTextBox39.Text + "', ipaketbilgisi= '" + materialLabel76.Text + "', ipaketucreti= '" + textBox8.Text + "' where invisehastalar.idosyano='" + richTextBox25.Text + "'", _ec1.baglantiinvise);
                MySqlCommand guncelle1 = new MySqlCommand("update iborclar set iborclar.iborcmiktari='" + richTextBox34.Text + "', iborclar.itc= '" + richTextBox24.Text + "' where iborclar.iborcno = '" + richTextBox25.Text + "'", _ec1.baglantiinvise);
                MySqlCommand guncellex = new MySqlCommand("update iodemeler set iodemeler.itc='" + richTextBox24.Text + "' where iodemeler.iborcno = '" + richTextBox25.Text + "'", _ec1.baglantiinvise);

                object sonuc = null;
                object sonuc1 = null;
                object sonucx = null;
                sonuc = guncelle.ExecuteNonQuery();
                sonuc1 = guncelle1.ExecuteNonQuery();
                sonucx = guncellex.ExecuteNonQuery();
                if (sonuc != null & sonuc1 != null & sonucx != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
                MySqlCommand commandborclar = new MySqlCommand
                {
                    CommandText = "select idosyano, invisehastalar.itc, iadi, isoyadi, iborclar.iborcmiktari, iodememiktari, iodemetarihi, iodemeler.iodemeyialan, iodemeler.iodemeno from  invisehastalar join iborclar on invisehastalar.itc=iborclar.itc join iodemeler on iborclar.iborcno=iodemeler.iborcno where invisehastalar.itc= '" + textBox6.Text + "'  ",
                    Connection = _ec1.baglantiinvise

                };
                string sql = "SELECT * FROM invisehastalar";
                DataTable data = new DataTable();
                DataTable databorclar = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand();
                MySqlDataAdapter adapterborclar = new MySqlDataAdapter
                {
                    SelectCommand = commandborclar
                };
                command.CommandText = sql;
                command.Connection = _ec1.baglantiinvise;
                adapter.SelectCommand = command;
                adapter.Fill(data);
                adapterborclar.Fill(databorclar);
                dataGridView1.DataSource = data;
                dataGridView2.DataSource = databorclar;
            }

            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MaterialRaisedButton9_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            if (materialLabel56.Text.Trim() == "")
            {
                MessageBox.Show("TC arama yap veya Tc girilmemiş(Dosya no olmamış olabilir kontrol et)");

            }
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand odemeyap = new MySqlCommand("insert into iodemeler  (iBorcno, itc, iodememiktari, iodemetarihi,iodemeler.iodemeyialan) values ('" + materialLabel56.Text + "','" + materialLabel57.Text + "','" + textBox10.Text + "','" + dateTimePicker48.Value.ToString("yyyy-MM-dd") + "','" + textBox11.Text + "')", _ec1.baglantiinvise);
                object sonucodemeyap = null;
                sonucodemeyap = odemeyap.ExecuteNonQuery();
                if (sonucodemeyap != null)
                {
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    {

                        decimal borc = 0;
                        decimal odeme = 0;
                        _ec1.baglantiinvise.Close();
                        _ec1.baglantiinvise.Open();
                        MySqlCommand cmd = new MySqlCommand("select sum(iborclar.iborcmiktari) as borctoplami from iborclar , invisehastalar where    iborclar.itc= '" + textBox6.Text + "' and iborclar.itc = invisehastalar.itc", _ec1.baglantiinvise);
                        int Count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (Count != 0)
                        {
                            materialLabel56.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                            materialLabel57.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                            materialLabel58.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                            materialLabel59.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
                            MySqlDataReader oku = cmd.ExecuteReader();


                            while (oku.Read())
                            {
                                borc = Convert.ToDecimal(oku["borctoplami"]);

                            }

                        }

                        _ec1.baglantiinvise.Close();
                        _ec1.baglantiinvise.Open();

                        cmd = new MySqlCommand("select sum(iodemeler.iodememiktari) as odemetoplami from iodemeler, iborclar ,invisehastalar where iborclar.itc = iodemeler.itc and  iborclar.itc= '" + textBox6.Text + "' and iborclar.itc = invisehastalar.itc", _ec1.baglantiinvise);
                        Count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (Count != 0)
                        {
                            MySqlDataReader oku = cmd.ExecuteReader();
                            while (oku.Read())
                            {
                                odeme = Convert.ToDecimal(oku["odemetoplami"]);

                            }
                        }

                        _ec1.baglantiinvise.Close();
                        materialLabel52.Text = (borc).ToString();
                        materialLabel54.Text = (odeme).ToString();
                        materialLabel53.Text = (borc - odeme).ToString();

                    }

                }


                else
                    MessageBox.Show("Sisteme eklenemedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
                MySqlCommand commandborclar = new MySqlCommand
                {
                    CommandText = "SELECT*from borclar ",
                    Connection = _ec1.baglantiinvise

                };
            }

            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            textBox10.Clear();
            textBox11.Clear();

            if (textBox6.Text.Trim() == "")//ababa
            {
                _ec1.baglantiinvise.Close();
                _ec1.baglantiinvise.Open();
                DataTable databorclar = new DataTable();
                MySqlDataAdapter adapterborclar = new MySqlDataAdapter();
                MySqlCommand commandborclar = new MySqlCommand
                {
                    CommandText = "select idosyano , invisehastalar.itc, iadi, isoyadi, iborcmiktari, iodememiktari, iodemetarihi, iodemeler.iodemeyialan from invisehastalar join iborclar on invisehastalar.itc=iborclar.itc join iodemeler on iborclar.iborcno=iodemeler.iborcno where invisehastalar.itc= '" + textBox6.Text + "'",
                    Connection = _ec1.baglantiinvise

                };
                adapterborclar.SelectCommand = commandborclar;
                adapterborclar.Fill(databorclar);
                dataGridView2.DataSource = databorclar;
                _ec1.baglantiinvise.Close();
                materialLabel52.Text = "".ToString();
                materialLabel54.Text = "".ToString();
                materialLabel53.Text = "".ToString();
                materialLabel56.Text = "".ToString();
                materialLabel57.Text = "".ToString();
                materialLabel58.Text = "".ToString();
                materialLabel59.Text = "".ToString();


            }
            else
            {
                _ec1.baglantiinvise.Close();
                _ec1.baglantiinvise.Open();
                DataTable databorclar = new DataTable();
                MySqlDataAdapter adapterborclar = new MySqlDataAdapter();
                MySqlCommand commandborclar = new MySqlCommand
                {

                    CommandText = "select idosyano , invisehastalar.itc, iadi, isoyadi, iborcmiktari, iodememiktari, iodemetarihi, iodemeler.iodemeyialan, iodemeno from invisehastalar join iborclar on invisehastalar.itc=iborclar.itc join iodemeler on iborclar.iborcno=iodemeler.iborcno where invisehastalar.itc= '" + textBox6.Text + "'",
                    // CommandText = "select	odemeler.borcno	, odemeler.odememiktari , odemeler.odemetarihi, hastalar.adi	, hastalar.soyadi, hastalar.tc  from odemeler , borclar , hastalar where odemeler.borcno = borclar.borcno and borclar.tc= '" + textBox6.Text + "' and borclar.tc = hastalar.tc",
                    //CommandText = "select dosya_no , hastalar.tc, adi, soyadi, borcm, odememiktari,odemetarihi from hastalar join borclar on hastalar.tc=borclar.tc join odemeler on borclar.tc=odemeler.tc  where hastalar.tc= '" + textBox6.Text + "'",

                    Connection = _ec1.baglantiinvise

                };
                adapterborclar.SelectCommand = commandborclar;
                adapterborclar.Fill(databorclar);
                dataGridView2.DataSource = databorclar;
                _ec1.baglantiinvise.Close();


            }
        }

        private void RichTextBox18_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
        }

        private void RichTextBox34_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
        }

        private void TextBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
        }

        private void TextBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
        }

        private void RichTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
        }

        private void RichTextBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
        }

        private void RichTextBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
        }

        private void RichTextBox16_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s1= '" + richTextBox63.Text + "',t1= '" + dateTimePicker28.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s2= '" + richTextBox62.Text + "',t2= '" + dateTimePicker5.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s3= '" + richTextBox61.Text + "',t3= '" + dateTimePicker7.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s4= '" + richTextBox60.Text + "',t4= '" + dateTimePicker6.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s5= '" + richTextBox59.Text + "',t5= '" + dateTimePicker9.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s6= '" + richTextBox58.Text + "',t6= '" + dateTimePicker8.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s7= '" + richTextBox57.Text + "',t7= '" + dateTimePicker11.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s8= '" + richTextBox56.Text + "',t8= '" + dateTimePicker10.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s9= '" + richTextBox55.Text + "',t9= '" + dateTimePicker13.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s10= '" + richTextBox54.Text + "',t10= '" + dateTimePicker12.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s11= '" + richTextBox53.Text + "',t11= '" + dateTimePicker15.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s12= '" + richTextBox52.Text + "',t12= '" + dateTimePicker14.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button26_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s13= '" + richTextBox51.Text + "',t13= '" + dateTimePicker27.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button25_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s14= '" + richTextBox50.Text + "',t14= '" + dateTimePicker26.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button24_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s15= '" + richTextBox49.Text + "',t15= '" + dateTimePicker25.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button23_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s16= '" + richTextBox48.Text + "',t16= '" + dateTimePicker24.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s17= '" + richTextBox47.Text + "',t17= '" + dateTimePicker23.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s18= '" + richTextBox46.Text + "',t18= '" + dateTimePicker22.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s19= '" + richTextBox45.Text + "',t19= '" + dateTimePicker21.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s20= '" + richTextBox44.Text + "',t20= '" + dateTimePicker20.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s21= '" + richTextBox43.Text + "',t21= '" + dateTimePicker19.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s22= '" + richTextBox42.Text + "',t22= '" + dateTimePicker18.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s23= '" + richTextBox41.Text + "',t23= '" + dateTimePicker17.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s24= '" + richTextBox40.Text + "',t24= '" + dateTimePicker16.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button38_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s25= '" + richTextBox87.Text + "',t25= '" + dateTimePicker40.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button37_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s26= '" + richTextBox86.Text + "',t26= '" + dateTimePicker39.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button36_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s27= '" + richTextBox85.Text + "',t27= '" + dateTimePicker38.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button35_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s28= '" + richTextBox84.Text + "',t28= '" + dateTimePicker37.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button34_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s29= '" + richTextBox83.Text + "',t29= '" + dateTimePicker36.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button33_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s30= '" + richTextBox82.Text + "',t30= '" + dateTimePicker35.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button32_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s31= '" + richTextBox81.Text + "',t31= '" + dateTimePicker34.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button31_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s32= '" + richTextBox80.Text + "',t32= '" + dateTimePicker33.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button30_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s33= '" + richTextBox79.Text + "',t33= '" + dateTimePicker32.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button29_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s34= '" + richTextBox78.Text + "',t34= '" + dateTimePicker31.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button28_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s35= '" + richTextBox77.Text + "',t35= '" + dateTimePicker30.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button27_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s36= '" + richTextBox76.Text + "',t36= '" + dateTimePicker29.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button54_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s37= '" + richTextBox75.Text + "',t37= '" + dateTimePicker62.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button53_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s38= '" + richTextBox74.Text + "',t38= '" + dateTimePicker61.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button52_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s39= '" + richTextBox73.Text + "',t39= '" + dateTimePicker60.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button51_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s40= '" + richTextBox72.Text + "',t40= '" + dateTimePicker59.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button50_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s41= '" + richTextBox71.Text + "',t41= '" + dateTimePicker58.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button49_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s42= '" + richTextBox70.Text + "',t40= '" + dateTimePicker57.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button48_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s43= '" + richTextBox69.Text + "',t43= '" + dateTimePicker56.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button47_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s44= '" + richTextBox68.Text + "',t44= '" + dateTimePicker55.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button46_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s45= '" + richTextBox67.Text + "',t45= '" + dateTimePicker54.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button45_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s46= '" + richTextBox66.Text + "',t46= '" + dateTimePicker53.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button44_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s47= '" + richTextBox65.Text + "',t47= '" + dateTimePicker52.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button43_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update invisehastalar SET s48= '" + richTextBox64.Text + "',t48= '" + dateTimePicker51.Value.ToString("yyyy-MM-dd") + "'  where invisehastalar.itc='" + richTextBox24.Text + "'", _ec1.baglantiinvise);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MaterialRaisedButton3_Click(object sender, EventArgs e)//biten hastalar
        {
            _ec1.baglantiinvise.Close();
            string sql1 = "Select * from invise.invisehastalar where ibitistarihi is not null";
            DataTable data1 = new DataTable();
            MySqlDataAdapter adapter1 = new MySqlDataAdapter();
            MySqlCommand command1 = new MySqlCommand
            {
                CommandText = sql1,
                Connection = _ec1.baglantiinvise
            };

            adapter1.SelectCommand = command1;
            _ec1.baglantiinvise.Open();
            adapter1.Fill(data1);
            dataGridView1.DataSource = data1;
            _ec1.baglantiinvise.Close();
        }

        private void MaterialRaisedButton4_Click(object sender, EventArgs e)//devam eden hastalar
        {
            _ec1.baglantiinvise.Close();
            string sql1 = "Select * from invise.invisehastalar where ibitistarihi is null";
            DataTable data1 = new DataTable();
            MySqlDataAdapter adapter1 = new MySqlDataAdapter();
            MySqlCommand command1 = new MySqlCommand
            {
                CommandText = sql1,
                Connection = _ec1.baglantiinvise
            };

            adapter1.SelectCommand = command1;
            _ec1.baglantiinvise.Open();
            adapter1.Fill(data1);
            dataGridView1.DataSource = data1;
            _ec1.baglantiinvise.Close();
        }

        private void MaterialRaisedButton5_Click(object sender, EventArgs e)//tüm hastalar
        {
            _ec1.baglantiinvise.Close();
            string sql2 = "SELECT * FROM invisehastalar ";
            DataTable data2 = new DataTable();
            MySqlDataAdapter adapter2 = new MySqlDataAdapter();
            MySqlCommand command2 = new MySqlCommand
            {
                CommandText = sql2,
                Connection = _ec1.baglantiinvise
            };

            adapter2.SelectCommand = command2;
            _ec1.baglantiinvise.Open();
            adapter2.Fill(data2);
            dataGridView1.DataSource = data2;
            _ec1.baglantiinvise.Close();
        }

        private void MaterialRaisedButton7_Click(object sender, EventArgs e)
        {
            materialLabel76.Text = "".ToString();
            foreach (Control item in tabPage2.Controls)
            {
                if (item is RichTextBox)
                {
                    RichTextBox txt = item as RichTextBox;
                    txt.Clear();
                }
                if (item is TextBox)
                {
                    TextBox txt = item as TextBox;
                    txt.Clear();
                }
            }
        }

        private void DataGridView3_DoubleClick(object sender, EventArgs e)
        {
            materialLabel75.Text = "".ToString();
            materialLabel75.Text = dataGridView3.CurrentRow.Cells[0].Value.ToString();
            textBox5.Text= dataGridView3.CurrentRow.Cells[1].Value.ToString();
        }

        

        private void DataGridView4_DoubleClick(object sender, EventArgs e)
        {
            materialLabel76.Text = "".ToString();
            materialLabel76.Text = dataGridView3.CurrentRow.Cells[0].Value.ToString();
            textBox8.Text = dataGridView3.CurrentRow.Cells[1].Value.ToString();
        }
         

        private void TextBox8_TextChanged_1(object sender, EventArgs e)
        {
                textBox8.PasswordChar = '*';
            
        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.PasswordChar = '*';
        }

        private void MaterialFlatButton1_Click(object sender, EventArgs e)//rapor şifre
        {
            if (textBox9.Text.ToString() == "Ender")

            {
                materialFlatButton2.Visible = true;
                materialFlatButton5.Visible = true;
                materialFlatButton4.Visible = true;
                dataGridView5.Visible = true;
                materialLabel80.Visible = true;
                materialLabel77.Visible = true;
                materialLabel78.Visible = true;
                dateTimePicker43.Visible = true;
                dateTimePicker44.Visible = true;
                dateTimePicker45.Visible = true;
                dateTimePicker46.Visible = true;
                dateTimePicker47.Visible = true;
                materialLabel81.Visible = true;
                dateTimePicker42.Visible = true;
                dateTimePicker41.Visible = true;
                materialFlatButton3.Visible = true;
                materialLabel79.Visible = true;
                materialLabel82.Visible = true;
                dataGridView6.Visible = true;
                materialLabel83.Visible = true;
                textBox12.Visible = true;
                materialFlatButton6.Visible = true;
                materialFlatButton7.Visible = true;
            }
            else
            {
                MessageBox.Show("Yanlış Şifre");
                materialFlatButton2.Visible = false;
                materialFlatButton5.Visible = false;
                materialFlatButton4.Visible = false;
                dataGridView5.Visible = false;
                materialLabel80.Visible = false;
                materialLabel77.Visible = false;
                materialLabel78.Visible = false;
                dateTimePicker43.Visible = false;
                dateTimePicker44.Visible = false;
                dateTimePicker45.Visible = false;
                dateTimePicker46.Visible = false;
                dateTimePicker47.Visible = false;
                materialLabel81.Visible = false;
                dateTimePicker42.Visible = false;
                dateTimePicker41.Visible = false;
                materialFlatButton3.Visible = false;
                materialLabel79.Visible = false;
                materialLabel82.Visible = false;
                dataGridView6.Visible = false;
                materialLabel83.Visible = false;
                textBox12.Visible = false;
                materialFlatButton6.Visible = false;
                materialFlatButton7.Visible = false;
            }
            textBox9.Clear();
        }

        private void TextBox9_TextChanged(object sender, EventArgs e)
        {
            textBox9.PasswordChar = '*';
        }

        private void MaterialFlatButton2_Click(object sender, EventArgs e)
        {
            dataGridView5.DataSource = "";
            try
            {

                decimal borc1 = 0;


                _ec1.baglantiinvise.Open();

                MySqlCommand cmd1 = new MySqlCommand("select sum(iodemeler.iodememiktari) as odemetoplami from iodemeler where iodemeler.iodemetarihi= '" + dateTimePicker43.Value.ToString("yyyy-MM-dd") + "'", _ec1.baglantiinvise);
                MySqlCommand cmdtablo = new MySqlCommand("select idosyano ,  iadi, isoyadi, invisehastalar.itc, itoplamucret,  ( select  sum(iodememiktari) from iodemeler where iodemeler.iborcno=iborclar.iborcno and iodemeler.iborcno  IN (select iodemeler.iborcno from iodemeler where iodemetarihi='" + dateTimePicker43.Value.ToString("yyyy-MM-dd") + "'group by idosyano ) ) as Toplamodeme , sum(iodememiktari) as bugunodenen, (itoplamucret- ( select  sum(iodememiktari) from iodemeler where iodemeler.iborcno=iborclar.iborcno and iodemeler.iborcno  IN (select iodemeler.iborcno from iodemeler where iodemetarihi='" + dateTimePicker43.Value.ToString("yyyy-MM-dd") + "'group by idosyano ) )) as Kalan, iodemetarihi, iodemeler.iodemeyialan from invisehastalar join iborclar on invisehastalar.itc=iborclar.itc join iodemeler on iborclar.iborcno=iodemeler.iborcno where iodemeler.iodemetarihi='" + dateTimePicker43.Value.ToString("yyyy-MM-dd") + "' group by idosyano", _ec1.baglantiinvise);
                DataTable tabloverisi = new DataTable();

                MySqlDataAdapter tabloverisi1 = new MySqlDataAdapter();
                tabloverisi1.SelectCommand = cmdtablo;
                tabloverisi1.Fill(tabloverisi);
                dataGridView5.DataSource = tabloverisi;
                int Count = Convert.ToInt32(cmd1.ExecuteScalar());
                if (Count != 0)
                {

                    MySqlDataReader oku = cmd1.ExecuteReader();



                    while (oku.Read())
                    {
                        borc1 = Convert.ToDecimal(oku["odemetoplami"]);
                        materialLabel80.Text = borc1.ToString();

                    }

                }
                _ec1.baglantiinvise.Close();
            }

            catch
            {
                MessageBox.Show("Bu tarihte ödeme bilgisi bulunamadı. Tarihte ödeme olduğundan eminiseniz Lütfen ilgiliye haber veriniz");

            }
            _ec1.baglantiinvise.Close();
        }

        private void MaterialFlatButton3_Click(object sender, EventArgs e)
        {
            dataGridView5.DataSource = "";
            try
            {

                decimal borc2 = 0;


                _ec1.baglantiinvise.Open();
                MySqlCommand cmd2 = new MySqlCommand("select sum(iodemeler.iodememiktari) as odemetoplami from iodemeler where iodemeler.iodemetarihi between '" + dateTimePicker42.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker41.Value.ToString("yyyy-MM-dd") + "'", _ec1.baglantiinvise);
                //MySqlCommand cmdtablo = new MySqlCommand("select dosya_no , hastalar.tc, adi, soyadi, borcm, odememiktari, odemetarihi from hastalar join borclar on hastalar.tc=borclar.tc join odemeler on borclar.borcno=odemeler.borcno where odemeler.odemetarihi between '" + dateTimePicker41.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker42.Value.ToString("yyyy-MM-dd") + "'", _ec.baglanti);
                MySqlCommand cmdtablo = new MySqlCommand(" SELECT iodemeler.iborcno , invisehastalar.iadi, invisehastalar.isoyadi,iborcmiktarı, ( select  sum(iodememiktari) from iodemeler where iodemeler.iborcno=iborclar.iborcno and iodemeler.iborcno  IN (select iodemeler.iborcno from iodemeler where iodemetarihi between '" + dateTimePicker42.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker41.Value.ToString("yyyy-MM-dd") + "' group by iodemeler.iborcno ) ) as Toplamodeme , ( select  sum(iodememiktari) from iodemeler where iodemeler.iborcno=iborclar.iborcno and iodemetarihi between '" + dateTimePicker42.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker41.Value.ToString("yyyy-MM-dd") + "'  group by iodemeler.iborcno ) as odemeler , iborcmiktarı-  ( select  sum(iodememiktari) from iodemeler where iodemeler.iborcno=iborclar.iborcno and iodemeler.iborcno  IN (select iodemeler.iborcno from iodemeler where iodemetarihi between '" + dateTimePicker42.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker41.Value.ToString("yyyy-MM-dd") + "' group by iodemeler.iborcno ) ) as Kalan , iodemeler.iodemeyialan FROM iodemeler , iborclar, invisehastalar where iodemeler.iborcno=iborclar.iborcno and iodemeler.itc=invisehastalar.itc and iodemetarihi between '" + dateTimePicker42.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker41.Value.ToString("yyyy-MM-dd") + "' GROUP BY iborcno", _ec1.baglantiinvise);
                DataTable tabloverisi = new DataTable();

                MySqlDataAdapter tabloverisi1 = new MySqlDataAdapter();
                tabloverisi1.SelectCommand = cmdtablo;
                tabloverisi1.Fill(tabloverisi);
                dataGridView5.DataSource = tabloverisi;
                int Count1 = Convert.ToInt32(cmd2.ExecuteScalar());
                if (Count1 != 0)
                {

                    MySqlDataReader oku = cmd2.ExecuteReader();


                    while (oku.Read())
                    {
                        borc2 = Convert.ToDecimal(oku["odemetoplami"]);
                        materialLabel77.Text = borc2.ToString();
                    }

                }
                _ec1.baglantiinvise.Close();
            }

            catch
            {
                MessageBox.Show("Bu tarihte ödeme bilgisi bulunamadı. Tarihte ödeme olduğundan eminiseniz Lütfen ilgiliye haber veriniz");
                materialLabel63.Text = "";
            }
            _ec1.baglantiinvise.Close();
        
    }

        private void MaterialFlatButton4_Click(object sender, EventArgs e)
        {
            try
            {

                decimal borc3 = 0;
                decimal odeme3 = 0;
                _ec1.baglantiinvise.Close();
                _ec1.baglantiinvise.Open();
                MySqlCommand cmd3 = new MySqlCommand("select sum(invisehastalar.itoplamucret) as toplamucret from invisehastalar where invisehastalar.ikayittarihi  between '" + dateTimePicker44.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker45.Value.ToString("yyyy-MM-dd") + "' ", _ec1.baglantiinvise);
                int Count = Convert.ToInt32(cmd3.ExecuteScalar());
                if (Count != 0)
                {

                    MySqlDataReader oku = cmd3.ExecuteReader();


                    while (oku.Read())
                    {
                        borc3 = Convert.ToDecimal(oku["toplamucret"]);
                        materialLabel78.Text = borc3.ToString();


                    }

                }

                _ec1.baglantiinvise.Close();
                _ec1.baglantiinvise.Open();
                cmd3 = new MySqlCommand("select sum(iodemeler.iodememiktari) as odemetoplami1 from iodemeler where iodemeler.iodemetarihi between '" + dateTimePicker44.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker45.Value.ToString("yyyy-MM-dd") + "' ", _ec1.baglantiinvise);
                Count = Convert.ToInt32(cmd3.ExecuteScalar());
                if (Count != 0)
                {
                    MySqlDataReader oku = cmd3.ExecuteReader();
                    while (oku.Read())
                    {
                        odeme3 = Convert.ToDecimal(oku["odemetoplami1"]);
                       materialLabel79.Text = odeme3.ToString();
                    }
                }

                _ec1.baglantiinvise.Close();

                materialLabel82.Text = (borc3 - odeme3).ToString();

            }
            catch
            {
                MessageBox.Show("Bu tarihte ödeme bilgisi bulunamadı. Tarihte ödeme olduğundan eminiseniz Lütfen ilgiliye haber veriniz");
                materialLabel82.Text = "";

            }
        }

        private void MaterialFlatButton5_Click(object sender, EventArgs e)
        {
            try
            {

                decimal hastasayisi = 0;

                _ec1.baglantiinvise.Close();
                _ec1.baglantiinvise.Open();
                MySqlCommand cmd5 = new MySqlCommand("select count(invisehastalar.ikayittarihi) as hastasayisi from invisehastalar where invisehastalar.ikayittarihi between '" + dateTimePicker46.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker47.Value.ToString("yyyy-MM-dd") + "'", _ec1.baglantiinvise);

                MySqlCommand cmdtablo = new MySqlCommand("select idosyano , invisehastalar.itc, iadi, isoyadi, iborcmiktari, iodememiktari, iodemetarihi, invisehastalar.ikayittarihi from invisehastalar join iborclar on invisehastalar.itc=iborclar.itc join iodemeler on iborclar.iborcno=iodemeler.iborcno where invisehastalar.ikayittarihi between '" + dateTimePicker46.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker47.Value.ToString("yyyy-MM-dd") + "' group by idosyano", _ec1.baglantiinvise);
                DataTable tabloverisi = new DataTable();

                MySqlDataAdapter tabloverisi1 = new MySqlDataAdapter();
                tabloverisi1.SelectCommand = cmdtablo;
                tabloverisi1.Fill(tabloverisi);
                dataGridView5.DataSource = tabloverisi;

                int Count5 = Convert.ToInt32(cmd5.ExecuteScalar());
                if (Count5 != 0)
                {

                    MySqlDataReader oku = cmd5.ExecuteReader();


                    while (oku.Read())
                    {
                        hastasayisi = Convert.ToDecimal(oku["hastasayisi"]);
                        materialLabel81.Text = hastasayisi.ToString();
                    }

                }
                _ec1.baglantiinvise.Close();
            }

            catch (Exception hata)
            {
                // MessageBox.Show("Bu tarihte kişi bilgisi bulunamadı. Tarihte kişi olduğundan eminiseniz Lütfen ilgiliye haber veriniz");
                MessageBox.Show(hata.ToString());
                materialLabel65.Text = "";
            }
            _ec1.baglantiinvise.Close();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)

            {

                label17.Visible = true;
                label18.Visible = true;
                label19.Visible = true;
                label20.Visible = true;
                label21.Visible = true;
                textBox7.Visible = true;
                button39.Visible = true;

            }
            else
            {
                label17.Visible = false;
                label18.Visible = false;
                label19.Visible = false;
                label20.Visible = false;
                label21.Visible = false;
                textBox7.Visible = false;
                button39.Visible = false;

            }
        }

        private void Button39_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelley = new MySqlCommand("update iodemeler set iodemeler.iodememiktari='" + textBox7.Text + "' where iodemeler.iborcno = '" + label21.Text + "' and iodemeler.iodemeno='" + label19.Text + "'", _ec1.baglantiinvise);


                object sonucy = null;
                sonucy = guncelley.ExecuteNonQuery();
                if (sonucy != null)
                {
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    MySqlCommand ekranyenile = new MySqlCommand("select idosyano, invisehastalar.itc, iadi, isoyadi,iborcmiktari, iodememiktari, iodemetarihi, iodemeler.iodemeyialan, iodemeler.iodemeno from invisehastalar join iborclar on invisehastalar.itc=iborclar.itc join iodemeler on iborclar.iborcno=iodemeler.iborcno where invisehastalar.itc= '" + textBox6.Text + "'", _ec1.baglantiinvise);

                    MySqlDataAdapter ekran1 = new MySqlDataAdapter();
                    DataTable data1 = new DataTable();


                    ekran1.SelectCommand = ekranyenile;
                    _ec1.baglantiinvise.Close();
                    _ec1.baglantiinvise.Open();
                    ekran1.Fill(data1);
                    dataGridView2.DataSource = data1;
                    _ec1.baglantiinvise.Close();

                }

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();

            }

            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }//odemelerde odeme miktarını güncelleme

        private void DataGridView2_DoubleClick(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox7.Clear();
                label21.Text = "";
                label19.Text = "";
                textBox7.Text = dataGridView2.CurrentRow.Cells[5].Value.ToString();
                label21.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                label19.Text = dataGridView2.CurrentRow.Cells[8].Value.ToString();
            }
            if (checkBox1.Checked == false)
            {
                textBox7.Clear();
                label21.Text = "";
                label19.Text = "";

            }
        }

        private void MaterialFlatButton6_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();

            string sql2 = "select * FROM invisetablo";
            DataTable paketbilgisi = new DataTable();
            MySqlDataAdapter hastabilgisiadapter = new MySqlDataAdapter();
            MySqlCommand command1 = new MySqlCommand
            {
                CommandText = sql2,
                Connection = _ec1.baglantiinvise
            };
            hastabilgisiadapter.SelectCommand = command1;
            hastabilgisiadapter.Fill(paketbilgisi);
            dataGridView6.DataSource = paketbilgisi;
        }

        private void DataGridView6_DoubleClick(object sender, EventArgs e)
        {
            textBox12.Text = dataGridView6.CurrentRow.Cells[1].Value.ToString();
            materialLabel83.Text = dataGridView6.CurrentRow.Cells[0].Value.ToString();
        }

        private void MaterialFlatButton7_Click(object sender, EventArgs e)
        {
            _ec1.baglantiinvise.Close();
            try
            {

                _ec1.baglantiinvise.Open();
                MySqlCommand guncelley1 = new MySqlCommand("update invisetablo set invisetablo.paketucreti='"+textBox12.Text+"' where invisetablo.paketadi = '"+ materialLabel83.Text+"'",_ec1.baglantiinvise);


                object sonucy = null;
                sonucy = guncelley1.ExecuteNonQuery();
                if (sonucy != null)
                {
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _ec1.baglantiinvise.Close();

                    string sql2 = "select * FROM invisetablo";
                    DataTable paketbilgisi = new DataTable();
                    MySqlDataAdapter hastabilgisiadapter = new MySqlDataAdapter();
                    MySqlCommand command1 = new MySqlCommand
                    {
                        CommandText = sql2,
                        Connection = _ec1.baglantiinvise
                    };
                    hastabilgisiadapter.SelectCommand = command1;
                    hastabilgisiadapter.Fill(paketbilgisi);
                    dataGridView6.DataSource = paketbilgisi;
                    textBox12.Clear();
                    materialLabel83.Text = "Paket".ToString();

                }

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec1.baglantiinvise.Close();

            }

            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    }
    
    
    

  
    

