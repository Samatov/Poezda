using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Step_v0
{
    public partial class MMT : Form
    {
        Train testTR;
        List<Train> list_poezdov;
        List<Stops> list_ostanovok;
        List<Distanation> marshruts;
        Distanation marsh;
        DateTime t;
      //  List<TrainControl> list_ctrl_train = new List<TrainControl>();
        Glades glades = new Glades();


        public MMT(List<Train> trains, List<Distanation> marshrut)
        {
            list_poezdov = trains;
            marshruts = marshrut;
            //list_ostanovok = marshrut.os;
            InitializeComponent();
            Opacity = 0;
            Timer timer = new Timer();
            timer.Tick += new EventHandler((sender, e) =>
            {
                if ((Opacity += 0.02d) == 1) timer.Stop();
            });
            timer.Interval = 3;
            timer.Start();
            // dataGridView1.AllowUserToAddRows = false;

        }

        private void MMT_Load(object sender, EventArgs e)
        {
            DateTime time = new DateTime();
            textBox1.Text = DateTime.Now.ToString("HH");
            textBox2.Text = DateTime.Now.ToString("mm");
            textBox3.Text = "1";
            time = time.AddHours(int.Parse(textBox1.Text));
            time = time.AddMinutes(int.Parse(textBox2.Text));
           // t = time;
            glades.create_ctrl();
            load_event(Glades.list_ctrl_train);
            for (int i = 0; i < list_poezdov.Count; i++)
            {
                for (int j = 0; j < marshruts.Count; j++)
                {
                    if (list_poezdov[i].ID == marshruts[j].ID)
                    {
                        list_poezdov[i].last_ostanovka(marshruts[j], t);
                    }
                }
            }
         
        }


        public void load_event(List<TrainControl> list_ctrl)
    {
        TrainControl tr;
            for(int i=0; i<list_ctrl.Count();i++)
            {
                
                tr = list_ctrl[i];
                pictureBox1.Controls.Add(tr);
                tr.Click += new System.EventHandler(tr_Click1);
                tr.MouseEnter += new System.EventHandler(tr_MouseEnter);
                tr.MouseLeave += new System.EventHandler(tr_MouseLeave);

                    //new System.EventHandler(tr_Click);
            }
    }




        
      

        private void tr_Click1(object sender, EventArgs e)
        {
          //  listBox1.Visible = false;
            TrainControl poezd = (TrainControl)sender;
            // poezd.Location = new Point(20, 20);
            //  poezd.ctrl_x = 20;

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows.RemoveAt(i);
                i--;
            }


            //dataGridView1.Rows.Add();
         //   listBox1.Items.Clear();
            for (int i = 0; i < poezd.coll_train.Count; i++)
            {
                dataGridView1.Rows.Add();
                if (poezd.coll_train[i].motion)
                {

                    dataGridView1.Visible = true;

                    // listBox1.Items.Add("Поезд № " + poezd.coll_train[i].Nomer + ' ' + "В пути");

                    //  dataGridView1.Size = new Size(319, 58 * i);
                    dataGridView1.Rows[i].Cells[0].Value = poezd.coll_train[i].Nomer;
                    dataGridView1.Rows[i].Cells[1].Value = "В пути";
                    dataGridView1.Rows[i].Cells[2].Value = poezd.coll_train[i].last_stops;
                    
                    // dataGridView1.AllowUserToAddRows = false;

                }
                else
                {
                    dataGridView1.Visible = true;
                    //   dataGridView1.Size = new Size(319, 58 * i);
                    //listBox1.Items.Add("Поезд № " + poezd.coll_train[i].Nomer + ' ' + "Стоит");


                    dataGridView1.Rows[i].Cells[0].Value = poezd.coll_train[i].Nomer;
                    dataGridView1.Rows[i].Cells[1].Value = "Cтоит";
                    dataGridView1.Rows[i].Cells[2].Value = poezd.coll_train[i].last_stops;
                   
                    //  dataGridView1.AllowUserToAddRows = false;

                }
            }

            dataGridView1.Location = new Point(poezd.ctrl_x, 40 + poezd.ctrl_y);
            // dataGridView1.AllowUserToAddRows = false;
            //   dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count-1);


            // listBox1.Size = new Size(115, 75);
            //  listBox1.Visible = true;
            // listBox1.Location = new Point(poezd.ctrl_x, 40 + poezd.ctrl_y);


        }


        private void tr_MouseEnter(object sender, EventArgs e)
        {
            TrainControl poezd = (TrainControl)sender;
            poezd.Size = new Size(30, 30);
        }
        private void tr_MouseLeave(object sender, EventArgs e)
        {
            TrainControl poezd = (TrainControl)sender;
            poezd.Size = new Size(21, 23);
        }



  

       

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;

            DateTime time = new DateTime();
            time = time.AddHours(int.Parse(textBox1.Text));
            time = time.AddMinutes(int.Parse(textBox2.Text));
            t = time;


           Glades.list_ctrl_train.Clear();


            for (int i = 0; i < list_poezdov.Count; i++)
            {
                for (int j = 0; j < marshruts.Count; j++)
                {
                    if (list_poezdov[i].ID == marshruts[j].ID)
                    {
                        list_poezdov[i].last_ostanovka(marshruts[j], time);
                    }
                }
            }
            pictureBox1.Controls.Clear();
            glades.create_ctrl();
            load_event(Glades.list_ctrl_train);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Controls.Clear();
           Glades.list_ctrl_train.Clear();

            //   DateTime time = new DateTime();
            // time = time.AddHours(int.Parse(textBox1.Text));
            // time = time.AddMinutes(int.Parse(textBox2.Text));
            t = t.AddMinutes(int.Parse(textBox3.Text));

            textBox1.Text = t.Hour.ToString();
            textBox2.Text = t.Minute.ToString();




            for (int i = 0; i < list_poezdov.Count; i++)
            {
                for (int j = 0; j < marshruts.Count; j++)
                {
                    if (list_poezdov[i].ID == marshruts[j].ID)
                    {
                        list_poezdov[i].last_ostanovka(marshruts[j], t);
                    }
                }
            }

            glades.create_ctrl();
            load_event(Glades.list_ctrl_train);
            dataGridView2.RefreshEdit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
        /////////////////////////////////////////////////////////
     

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            for (int i2 = 0; i2 < dataGridView2.RowCount; i2++)
            {


                dataGridView2.Rows.RemoveAt(i2);
                i2--;

            }


            string s = "";
            string id = "";
            int d = 427;
            int i = 0, count = 0;
            bool f = false, f2 = false;
            Distanation marh;
            DateTime dt2 = new DateTime();
            DateTime realtime = new DateTime();
            int hour, min;
            hour = int.Parse(DateTime.Now.ToString("HH"));
            min = int.Parse(DateTime.Now.ToString("mm"));
            realtime = realtime.AddHours(hour);
            realtime = realtime.AddMinutes(min);
            dataGridView2.Visible = true;

            int index = dataGridView1.CurrentCell.RowIndex;
            string number = "";
            if (dataGridView1.Rows[index].Cells[0].Value != null)
            {
                number = dataGridView1.Rows[index].Cells[0].Value.ToString();
                f = true;
            }


            while (f)
            {
                if (number == list_poezdov[i].Nomer)
                {
                    id = list_poezdov[i].ID;

                    f = false;
                    f2 = true;

                }
                else
                {
                    i++;
                }
            }


            while (f2)
            {
                if (id == marshruts[count].ID)
                {
                    //  for (int k = 0; k < marshruts[count].Ostanovki.Count; k++)
                    //  {
                    // listBox1.Items.Add(marshruts[count].Ostanovki[k].Name);

                    for (int dddd = 0; dddd < marshruts[count].Ostanovki.Count; dddd++)
                    {
                        dataGridView2.Rows.Add();
                        dataGridView2.Rows[dddd].Cells[0].Value = marshruts[count].Ostanovki[dddd].Name;
                        s = list_poezdov[i].Time[dddd].ToString("HH:mm");
                        if (t >= list_poezdov[i].Time[dddd])
                        {
                            dataGridView2.Rows[dddd].Cells[1].Value = s;
                            dataGridView2.Rows[dddd].DefaultCellStyle.BackColor = Color.LightGreen;
                        }
                        else
                        {

                            dataGridView2.Rows[dddd].Cells[1].Value = s;
                            dataGridView2.Rows[dddd].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                            // dataGridView2.DefaultCellStyle.BackColor = Color.Green;
                        }
                        f2 = false;
                        f = false;
                    }



                    // }
                }
                else
                {
                    count++;

                }
            }

            dataGridView1.Rows[index].Selected = true;
            dataGridView1.RowsDefaultCellStyle.SelectionForeColor = Color.Red;

            // listBox1.Items.Add(index);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
        }


    }


}
