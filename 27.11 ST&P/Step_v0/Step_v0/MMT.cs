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

        public MMT(List<Train> trains,List<Stops> ostanovki)
        {
            list_poezdov = trains;
            list_ostanovok = ostanovki;
            InitializeComponent();
            Opacity = 0;
            Timer timer = new Timer();
            timer.Tick += new EventHandler((sender, e) =>
            {
                if ((Opacity += 0.02d) == 1) timer.Stop();
            });
            timer.Interval = 3;
            timer.Start();
           
        }

        private void MMT_Load(object sender, EventArgs e)
        {
            create_ctrl();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TrainControl tr = new TrainControl(100 , 100);
           

         /*   for(int i=0; i<list_poezdov.Count;i++)
            {
                flag1 = true;
                //count = 0;
                for (int count = 0; count < list_ostanovok.Count;count++ )
                    if (list_poezdov[i].last_stops == list_ostanovok[count].Name)
                    {
                        TrainControl tr = new TrainControl(list_ostanovok[count].X, list_ostanovok[count].Y);
                        pictureBox1.Controls.Add(tr);
                        flag1 = false;
                    }
                    //count++;
                    
               
            }

          /*  while(flag)
            {
                while (flag1)
                {
                    if (list_poezdov[i].last_stops == list_ostanovok[count].Name)
                    {
                        TrainControl tr = new TrainControl(list_ostanovok[count].X, list_ostanovok[count].Y);
                        pictureBox1.Controls.Add(tr);
                        flag1 = false;
                    }
                    count++;
                }
                i++;
                if(i == list_poezdov.Count-1)
                {
                    flag = false;
                }
            }*/

        }
        public void create_ctrl()
        {
            bool flag1 = true;
            bool flag2 = true;
            int count = 0;
            int i = 0;
            while (flag1)
            {

                //count = 0;
                //  for (int count = 0; count < list_ostanovok.Count; count++)
                while (flag2)
                {
                    if (list_poezdov[i].last_stops == list_ostanovok[count].Name)
                    {
                        TrainControl tr = new TrainControl(list_ostanovok[count].X, list_ostanovok[count].Y);
                        pictureBox1.Controls.Add(tr);
                        i++;
                        flag2 = false;

                    }
                    else
                    {
                        count++;
                    }


                }
                //count++;
                if (i == list_poezdov.Count)
                {
                    flag1 = false;
                    flag2 = false;
                }
                else
                {
                    flag2 = true;
                    count = 0;
                }

            }

        }



    }
}
