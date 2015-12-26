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
        List<TrainControl> list_ctrl_train = new List<TrainControl>();
        int k = 0;

        public MMT(List<Train> trains, List<Distanation> marshrut)
        {
            
            list_poezdov = trains;
            marshruts = marshrut;
            //list_ostanovok = marshrut.os;
            InitializeComponent();
          /*  Opacity = 0;
            Timer timer = new Timer();
            timer.Tick += new EventHandler((sender, e) =>
            {
                if ((Opacity += 0.02d) == 1) timer.Stop();
            });
            timer.Interval = 3;
            timer.Start();*/

        }

        private void MMT_Load(object sender, EventArgs e)
        {
            DateTime time = new DateTime();
            
            create_ctrl();
           // create_ctrl2();

            textBox1.Text = DateTime.Now.ToString("HH");
            textBox2.Text = DateTime.Now.ToString("mm");
            textBox3.Text = "1";
            time = time.AddHours(int.Parse(textBox1.Text));
            time = time.AddMinutes(int.Parse(textBox2.Text));
            t = time;

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

        private void button1_Click(object sender, EventArgs e)
        {
          //  timer1.Interval = 2000;

            dataGridView1.Visible = false;
            dataGridView2.Visible = false;

            DateTime time = new DateTime();
            time = time.AddHours(int.Parse(textBox1.Text));
            time = time.AddMinutes(int.Parse(textBox2.Text));
            t = time;
            
            
            list_ctrl_train.Clear();
            
            
            for(int i = 0;i<list_poezdov.Count;i++)
            {
               for (int j=0; j<marshruts.Count;j++)
               {
                   if(list_poezdov[i].ID==marshruts[j].ID)
                   {
                       list_poezdov[i].last_ostanovka(marshruts[j], time);
                   }
               }
            }
            pictureBox1.Controls.Clear();
            create_ctrl();

            



        }
        public void create_ctrl()
        {
            bool flag1 = true;
            bool flag2 = true;
            bool flag3 = true;
            int count = 0;
            int i = 0, q = 0;
            while (flag1)
            {


                while (flag2)
                {

                    while (flag3)
                    {
                        if (list_poezdov[i].ID == marshruts[q].ID)
                        {
                            list_ostanovok = marshruts[q].Ostanovki;
                            flag3 = false;
                            q = 0;

                        }
                        else
                        {
                            q++;
                        }
                    }


                    if (list_poezdov[i].last_stops == list_ostanovok[count].Name)
                    {
                        if (list_ctrl_train.Count == 0)
                        {
                            TrainControl tr = new TrainControl(list_ostanovok[count].X - 10+list_poezdov[i].X_din, list_ostanovok[count].Y - 22+list_poezdov[i].Y_din);
                            pictureBox1.Controls.Add(tr);
                            tr.coll_train.Add(list_poezdov[i]);
                            i++;
                            flag2 = false;
                            list_ctrl_train.Add(tr);

                            tr.Click += new System.EventHandler(tr_Click);
                            tr.MouseEnter += new System.EventHandler(tr_MouseEnter);
                            tr.MouseLeave += new System.EventHandler(tr_MouseLeave);
                        }
                        else
                        {
                            for (int k = 0; k < list_ctrl_train.Count; k++)
                            {
                                TrainControl test_tr;
                                test_tr = list_ctrl_train[k];
                                if (i < list_poezdov.Count)
                                {
                                    if ((test_tr.ctrl_x == (list_ostanovok[count].X - 10 + list_poezdov[i].X_din)) & (test_tr.ctrl_y == (list_ostanovok[count].Y - 22 + list_poezdov[i].Y_din)))
                                    {
                                        test_tr.coll_train.Add(list_poezdov[i]);
                                        i++;
                                        flag2 = false;

                                    }
                                }
                            }

                            if (flag2 == true)
                            {
                                TrainControl tr = new TrainControl(list_ostanovok[count].X - 10 + list_poezdov[i].X_din, list_ostanovok[count].Y - 22 + list_poezdov[i].Y_din);
                                tr.Click += new System.EventHandler(tr_Click);
                                tr.MouseEnter += new System.EventHandler(tr_MouseEnter);
                                tr.MouseLeave += new System.EventHandler(tr_MouseLeave);
                                pictureBox1.Controls.Add(tr);
                                tr.coll_train.Add(list_poezdov[i]);
                                i++;
                                flag2 = false;
                                list_ctrl_train.Add(tr);

                            }

                        }

                    }
                    else
                    {
                        count++;
                    }

                }

                if (i == list_poezdov.Count)
                {
                    flag1 = false;
                    flag2 = false;
                    flag3 = false;
                }
                else
                {
                    flag2 = true;
                    flag3 = true;
                    count = 0;
                }

            }

        }

      
        private void tr_Click(object sender, EventArgs e)
        {
            dataGridView2.Visible = false;
            
            TrainControl poezd = (TrainControl)sender;

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows.RemoveAt(i);
                i--;
            }

            
            for (int i = 0; i < poezd.coll_train.Count; i++)
            {
                dataGridView1.Rows.Add();
                if (poezd.coll_train[i].motion)
                {

                    dataGridView1.Visible = true;

                    dataGridView1.Rows[i].Cells[0].Value = poezd.coll_train[i].Nomer;
                    dataGridView1.Rows[i].Cells[1].Value = "В пути";
                    dataGridView1.Rows[i].Cells[2].Value = poezd.coll_train[i].last_stops;
                    dataGridView1.Rows[i].Cells[3].Value = poezd.coll_train[i].proc;
                    dataGridView1.Rows[i].Cells[4].Value = poezd.coll_train[i].X_din;
                    dataGridView1.Rows[i].Cells[5].Value = poezd.coll_train[i].Y_din;

                }
                else
                {
                    dataGridView1.Visible = true;
          
                    dataGridView1.Rows[i].Cells[0].Value = poezd.coll_train[i].Nomer;
                    dataGridView1.Rows[i].Cells[1].Value = "Cтоит";
                    dataGridView1.Rows[i].Cells[2].Value = poezd.coll_train[i].last_stops;
                    dataGridView1.Rows[i].Cells[3].Value = poezd.coll_train[i].proc;
                    dataGridView1.Rows[i].Cells[4].Value = poezd.coll_train[i].X_din;
                    dataGridView1.Rows[i].Cells[5].Value = poezd.coll_train[i].Y_din;
            

                }
            }

            dataGridView1.Location = new Point(poezd.ctrl_x, 40 + poezd.ctrl_y);



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

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            MessageBox.Show("ddd");
        }
      

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void MMT_FormClosed(object sender, FormClosedEventArgs e)
        {
            DateTime time = DateTime.Now;
            timer1.Stop();

         /*    int  hour;
            hour = int.Parse(time.ToString("HH"));
           int  min;
            min = int.Parse(time.ToString("mm"));
            time = time.AddHours(hour);
           time = time.AddMinutes(min);*/
            list_ctrl_train.Clear();


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
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Controls.Clear();
            list_ctrl_train.Clear();

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
            
              create_ctrl();
           // create_ctrl2();

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Interval = 500;
             timer1.Start();
             dataGridView1.Visible = false;
             dataGridView2.Visible = false;

           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
    }


}
