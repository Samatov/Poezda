using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MMT_v._2._1
{
    public partial class Form1 : Form
    {
        int hourInt; int i = 0, j, count = 0;
        int minInt;
        int secInt;
        int X, Y;
        string hour, min, sec, realtime;
        bool stop = true;
        int timeBegginHour = 08, timeBegginMin = 00, timeEndHour = 16, timeEndMin = 00;
        ListBox list = null;
        PictureBox pictureBoxs = null;
        string[] ArrTrain = { "427", "429" };
        int[] coordinates = new int[] { 50, 48, 107, 48, 177, 48, 244, 48, 337,48,420,48,505,48,596,688,781,940};
        //string[] timeStops = new string[] { "08:30", "09:00", "09:30", "10:00" };
        string[] timeStops = new string[] { "08", "00", "08", "23", "08", "50", "09", "20", "11", "45", "13", "30","14","05", "14", "15", "16", "00" };
        
       

        public Form1()
        {
            InitializeComponent();
            
          

        /*   if (hourInt >= timeBegginHour)
           {
               if (hourInt > timeBegginHour)
               {
                   MessageBox.Show(" выехал");
                   getcoordinate();
                   draw();
               }
               if (hourInt == timeBegginHour)
               {
                   if (minInt >= timeBegginMin)
                   {
                       MessageBox.Show(" выехал");
                       getcoordinate();
                       draw();
                   }
                   else
                       MessageBox.Show("Не выехал");


               }
           }
           else
               MessageBox.Show("Не выехал");*/

          // timer1.Start(); 
            

        }



        
        void proverka()
        {

        }

        void getcoordinate()
        {
            getTime();
            int i = 0;
            count = 0;
            string stops1, stops2;
            int stops1Int, stops2Int;
            string realtime = hour + min;
            int realtimeInt = int.Parse(realtime);



            int lenght = timeStops.Length;
            while ((stop))
            {
                stops1 = timeStops[i] + timeStops[i + 1];
                stops2 = timeStops[i + 2] + timeStops[i + 3];
                stops1Int = int.Parse(stops1);
                stops2Int = int.Parse(stops2);
                if ((realtimeInt >= stops1Int) & (realtimeInt < stops2Int))
                {
                    stop = false;
                }
                if ((realtimeInt >= stops1Int) & (realtimeInt >= stops2Int))
                {
                    i = i + 2;
                    count = count + 2;
                    stop = true;
                }




            }
            stop = false;
            X = coordinates[count];
            Y = coordinates[count + 1];

        }

        void draw()
        {
            if (pictureBoxs != null)
            {
                pictureBox1.Controls.Remove(pictureBoxs);
                pictureBoxs = null;
            }
            pictureBoxs = new PictureBox();
            pictureBoxs.Size = new Size(22, 22);
            
            // pictureBox1.Refresh();
            pictureBoxs.Load("seat.jpg");
           
            pictureBoxs.BackColor = Color.Transparent;
            pictureBoxs.Location = new Point(X,Y);
            // i = i + 2;
            pictureBox1.Controls.Add(pictureBoxs);
        }

        void getTime()
        {
            
            hour = DateTime.Now.ToString("HH");
            min = DateTime.Now.ToString("mm");
            sec = DateTime.Now.ToString("ss");
            textBox1.Text = hour;
            textBox2.Text = min;

           // string realtime = DateTime.Now.ToString("HH:mm");
            hourInt = int.Parse(hour);
            minInt = int.Parse(min);
            secInt = int.Parse(sec);
        }

        private void button1_Click(object sender, EventArgs e)
        {

           // getTime();
            proverka();
            getcoordinate();
            draw();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
            textBox1.Enabled = true;
            textBox2.Enabled = true;
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
        }

       
        
        

       


       
    }
    
 

    
}
