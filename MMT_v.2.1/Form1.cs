using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MMT_v._2._1
{
    public partial class Form1 : Form
    {

        int numberPoezd=0;
        List<Train1> listOfTrains = new List<Train1>();
        Train1 testPoezd = new Train1();
        string[] time_Stops;int[] coordinates;
          int timeBegginHour, timeBegginMin, timeEndHour, timeEndMin;
        string[] arr;
        int hourInt; int i = 0, j, count = 0;
        int minInt;
        int secInt;
        int X, Y,K;
        string hour, min, sec, realtime;
        bool stop = true, chek,flag=false;
       // int timeBegginHour = 08, timeBegginMin = 00, timeEndHour = 16, timeEndMin = 00;
        ListBox list = null;
        PictureBox pictureBoxs = null;
        string[] ArrTrain = { "427", "428","429"};
        //int[] coordinates = new int[] { 50, 48, 107, 48, 177, 48, 244, 48, 337,48,420,48,505,48,596,688,781,940};
        //string[] timeStops = new string[] { "08:30", "09:00", "09:30", "10:00" };
       // string[] time_Stops = new string[] { "08", "00", "08", "23", "08", "50", "09", "20", "11", "45", "13", "30","14","05", "14", "15", "16", "00" };
        
       

        public Form1()
        {
            InitializeComponent();

            Vivod_data.Items.Add("427 express Moscow Tyla 8:00 16:00");
            Vivod_data.Items.Add("429 express Moscow Tyla 6:00 14:00");
            Vivod_data.Items.Add("428 express Moscow Tyla 4:00 12:00");
            textBox1.Text = DateTime.Now.ToString("HH");
            textBox2.Text = DateTime.Now.ToString("mm");
            textBox1.Enabled = true;
            textBox2.Enabled = true;

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
            //string realtime = hour + min;
            string realtime = textBox1.Text + textBox2.Text;
            int realtimeInt = int.Parse(realtime);



            int lenght = time_Stops.Length;
            while ((stop))
            {
                stops1 = time_Stops[i] + time_Stops[i + 1];
                stops2 = time_Stops[i + 2] + time_Stops[i + 3];
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
            stop = true;
            X = coordinates[count];
            Y = coordinates[count + 1];

        }

        void draw()
        {
          
            Train1 poezd = new Train1();
           
            
            poezd.Click += new System.EventHandler(poezd_Click);
            poezd.MouseEnter += new System.EventHandler(poezd_MouseEnter);
            poezd.MouseLeave += new System.EventHandler(poezd_Leave);
            poezd.x = X;
            poezd.y = Y;


            if (K != 0)
            {
                for (int i = 0; i < listOfTrains.Count; i++)
                {
                    testPoezd = listOfTrains[i];
                    if ((testPoezd.x == poezd.x) & (testPoezd.y == poezd.y))
                    {
                        // testPoezd.number.Add(ArrTrain[numberPoezd]);
                        // listOfTrains.Add(poezd);

                        // testPoezd.Location = new Point(X - 11, Y - 22);
                        // testPoezd.Size = new Size(21, 21);
                        testPoezd.number.Add(ArrTrain[numberPoezd]);
                        flag = true;
                        ///numberPoezd++;
                        //poezd.Dispose();
                        //  K = 0;
                        // poezd.Dispose();
                        //  poezd.number.Add("s");
                    }

                    else
                    {

                        poezd.Location = new Point(X - 11, Y - 22);
                        poezd.Size = new Size(21, 21);
                        poezd.number.Add(ArrTrain[numberPoezd]);
                        //  poezd.number.Add("s");

                        poezd.Visible = true;


                        pictureBox1.Controls.Add(poezd);
                        //numberPoezd++;
                        //K++;

                    }

                    // listOfTrains.RemoveAt(listOfTrains.Count - 1);



                }
                // poezd = testPoezd;
            }
            if (K == 0)
            {

                poezd.Location = new Point(X - 11, Y - 22);
                poezd.Size = new Size(21, 21);
                poezd.number.Add(ArrTrain[numberPoezd]);
                //  poezd.number.Add("s");

                poezd.Visible = true;


               // pictureBox1.Controls.Add(poezd);
                numberPoezd++;
                
                listOfTrains.Add(poezd);
                K++;
            }
            else
            {

            }

          
            
            
          /*  pictureBoxs = new PictureBox();
            pictureBoxs.Size = new Size(22, 22);
            
            // pictureBox1.Refresh();
            pictureBoxs.Load("seat1.jpg");
           
            pictureBoxs.BackColor = Color.Transparent;
            pictureBoxs.Location = new Point(X-11,Y-22);
            // i = i + 2;
            pictureBox1.Controls.Add(pictureBoxs);*/
            //K++;
            //numberPoezd++;
            if(flag)
            {
                numberPoezd++;
                poezd = null;
                flag = false;
            }
        }

        private void poezd_Click(object sender, EventArgs e)
        {
            Train1 poezd = (Train1)sender;
           // UserControl1 us = (UserControl1)sender;
            //MessageBox.Show(us.name);
            poezd.Location = new Point(poezd.x - 30, poezd.y - 30);
            listBox1.Items.Clear();
            for (int i = 0; i < poezd.number.Count;i++ )
                listBox1.Items.Add("Поезд № "+poezd.number[i]);
            listBox1.Size = new Size(75, 75);
            listBox1.Visible = true;
            listBox1.Location = new Point(poezd.x,40+poezd.y);

        }

        private void poezd_MouseEnter(object sender, EventArgs e)
        {
            Train1 poezd = (Train1)sender;
            poezd.Size = new Size(30, 30);
          //  poezd.Location = new Point(poezd.x - 6, poezd.y - 11);
          //  testPoezd.Location = new Point(X - 11, Y - 22);
        }

        private void poezd_Leave(object sender, EventArgs e)
        {
            Train1 poezd = (Train1)sender;
            
            if(listBox1.Visible == false)
                poezd.Size = new Size(21, 21);
            
            
        }
            
       
        void getTime()
        {
            
            hour = DateTime.Now.ToString("HH");
            min = DateTime.Now.ToString("mm");
            sec = DateTime.Now.ToString("ss");
         //   textBox1.Text = hour;
            //textBox2.Text = min;

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

        private void button2_Click(object sender, EventArgs e)
        {
          
            string[] Arr_Train = new string[Vivod_data.Items.Count];
            for (int j = 0; j < Vivod_data.Items.Count; j++)
            {
                string stroka = Vivod_data.Items[j].ToString();
                arr = stroka.Split(' ');
                Arr_Train[j] = arr[0];   // Массив поездов(номера)
            }
            for (int i = 0; i < Arr_Train.Length; i++)
            {
                // Переключатель между поездами


                // Данные для передачи в MMT
                coordinates = Massive.Coordinates_fill(Arr_Train[i].Split('/'));
                time_Stops = Massive.Time_stops_fill(Arr_Train[i].Split('/'));
                timeBegginHour = int.Parse(time_Stops[0]); timeBegginMin = int.Parse(time_Stops[1]); timeEndHour = int.Parse(time_Stops[time_Stops.Length - 2]); timeEndMin = int.Parse(time_Stops[time_Stops.Length - 1]);


                proverka();
                getcoordinate();
                draw();
                //////////////////////////////////////////////////////////////////////////////////////////////////


                Vivod_ostanovok.Visible = true;
                for (int k = 0; k < coordinates.Length; k++)
                {
                    Vivod_ostanovok.Items.Add(coordinates[k]);
                }
                for (int k = 0; k < time_Stops.Length; k++)
                {
                    Vivod_ostanovok.Items.Add(time_Stops[k]);
                }
            }
          //  for (int k = 0; k < time_Stops.Length; k++)
           // listBox1.Items.Add(time_Stops[j]);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Visible = false;
            for (int i = 0; i < ArrTrain.Length; i++){
                pictureBox1.Controls.Remove(listOfTrains[i]);
            listOfTrains[i] = null;
        }
            numberPoezd = 0;
            listOfTrains.Clear();
            K = 0;
             
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Train1 poezd = (Train1)sender;
            listBox1.Visible = false;
           // poezd.Size = new Size(21, 21);
           
        }

       
        
        

       


       
    }

    public class Massive
    {

        public static int[] Coordinates_fill(string[] arr_train)
        {
            string[] arr; int[] coordinates = new int[0];
            StreamReader fs3 = new StreamReader("BaseStope.txt");
            StreamReader fs1 = new StreamReader("BaseTrain.txt");
            List<string> collection = new List<string>();
            while (true)
            {
                string s = fs1.ReadLine();
                if (s != null)
                {
                    if (s.IndexOf(arr_train[0]) > -1)
                    {
                        collection.Add(s);
                    }
                }
                else
                    break;
            }
            arr = collection[0].Split(' ');
            arr[arr.Length - 1] = "null";
            int j = 0;
            while (true)
            {
                string s = fs3.ReadLine();
                if (s != null)
                {
                    for (int i = 7; i < arr.Count(); i += 3)
                        if (s.IndexOf(arr[i]) > -1)
                        {
                            Array.Resize(ref coordinates, coordinates.Length + 2);
                            string[] info = s.Split(' ');
                            coordinates[j] = int.Parse(info[2]);
                            coordinates[j + 1] = int.Parse(info[4]);
                            j = j + 2;
                        }
                }
                else
                    break;
            }
            return coordinates;
        }

        public static string[] Time_stops_fill(string[] arr_train)
        {
            StreamReader fs1 = new StreamReader("BaseTrain.txt");
            List<string> collection = new List<string>();
            string[] arr; string[] time_stops = new string[0];
            while (true)
            {
                string s = fs1.ReadLine();
                if (s != null)
                {
                    if (s.IndexOf(arr_train[0]) > -1)
                    {
                        collection.Add(s);
                    }
                }
                else
                    break;
            }
            arr = collection[0].Split(' ');
            arr[arr.Length - 1] = "null";
            int k = 0;
            for (int j = 8; j < arr.Length; j += 3)
            {
                string[] time = (arr[j].Split(':'));
                Array.Resize(ref time_stops, time_stops.Length + 2);
                time_stops[k] = time[0].Replace("(", "");
                time_stops[k + 1] = time[1].Replace(")", "");
                k = k + 2;
            }
            return time_stops;
        }
    }

    
}
