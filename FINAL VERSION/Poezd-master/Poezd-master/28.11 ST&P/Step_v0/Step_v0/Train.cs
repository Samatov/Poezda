using System.IO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace Step_v0
{
    public class Train
    {
        public string Nomer, Type,ID;
        public List<DateTime> Time;
        public List<Passanger> passanger;
        public string last_stops;
        public  bool motion;
        public double S;
        public int proc;
        public int X_din = 0, Y_din = 0;

        public Train(string n, string t, string id, List<DateTime> Vreme_ostanovkok, List<Passanger> pass) {
            Nomer = n;
            Type = t;
            ID = id;
            Time = Vreme_ostanovkok;
            passanger = pass;
        }


        public void Vivod(ref TextBox t4, ref TextBox t3,ref DataGridView grid_vivod,ref int i,ref string start,ref string stop) {
            if (Form1.K > 0)
            {
                grid_vivod.Rows.Add();
            }
            Form1.K++;
            grid_vivod.Rows[i].Cells[0].Value =last_stops;
            grid_vivod.Rows[i].Cells[1].Value = Nomer;
            grid_vivod.Rows[i].Cells[2].Value = Type;
            grid_vivod.Rows[i].Cells[3].Value = start;
            grid_vivod.Rows[i].Cells[4].Value = stop;
            grid_vivod.Rows[i].Cells[5].Value = Time[0].ToString("HH:mm");
            grid_vivod.Rows[i].Cells[6].Value = Time[Time.Count - 1].ToString("HH:mm");
            grid_vivod.Visible = true;       
        }

        public void rachet_S(Stops stop1, Stops stop2, DateTime t_stop1, DateTime t_stop2, DateTime rt)
        {
            int put = 1, x2, y2;
            float result, real_time_int, B, a, b, c, K1, K2, K;



            int Hour, min, procent_puti;
            put = (int)Math.Round(Math.Sqrt((stop2.X - stop1.X) * (stop2.X - stop1.X) + (stop2.Y - stop1.Y) * (stop2.Y - stop1.Y)));
            Hour = t_stop2.Hour - t_stop1.Hour;
            min = t_stop2.Minute - t_stop1.Minute;
            result = 60 * Hour + min;
            Hour = t_stop2.Hour - rt.Hour;
            min = t_stop2.Minute - rt.Minute;
            real_time_int = 60 * Hour + min;
            procent_puti = (int)Math.Round(real_time_int * 100 / result);

            proc = 100 - procent_puti;
            S = put * proc / 100;//прйденный путь в px

            ///расчет кординатi
            ///
            if (stop2.Y == stop1.Y)
            {
                if (stop2.X > stop1.X)
                {
                    X_din = (int)S;
                    Y_din = 0;
                }
                else
                {
                    X_din = -(int)S;
                    Y_din = 0;
                }

            }
            if (stop2.X == stop1.X)
            {
                if (stop2.Y > stop1.Y)
                {
                    X_din = 0;
                    Y_din = (int)S;
                }
                else
                {
                    Y_din = -(int)S;
                    X_din = 0;
                }
            }
            if ((stop2.X != stop1.X) & (stop2.Y != stop1.Y))
            {
                K1 = stop2.Y - stop1.Y;
                K2 = stop2.X - stop1.X;
                K = (K1) / (K2);
                B = stop1.Y - K * stop1.X;
                x2 = stop1.X;
                y2 = stop1.Y;

                // a = 1 + (K * K);
                // b = -2 * x2 + 2 * K * B - 2 * y2 * K;
                //  c = (x2 * x2) - (S * S) +(y2*y2) - (2 * y2 * B) + (B * B);

                x2 = (int)(stop1.X + S * K2 / put);
                y2 = (int)(stop1.Y + S * K1 / put);

                X_din = x2 - stop1.X;
                Y_din = y2 - stop1.Y;

            }


        }



        public void last_ostanovka(Distanation marshrut, DateTime time)
        {
            int count = 0, i = 0;
            int hour, min;
            Boolean flag = true;
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            DateTime realtime = new DateTime();
            hour = int.Parse(time.ToString("HH"));
            min = int.Parse(time.ToString("mm"));
            realtime = realtime.AddHours(hour);
            realtime = realtime.AddMinutes(min);

            if (realtime <= Time[0])//поезl не выехал
            {
                motion = false;
                last_stops = marshrut.Start;
                flag = false;
                count = 0;
                S = 0;
                X_din = 0;
                Y_din = 0;

            }
            if (realtime >= Time[Time.Count - 1])//поезд приехал
            {
                last_stops = marshrut.End;
                flag = false;
                count = marshrut.Ostanovki.Count - 1;
                motion = false;
                S = 100;
                X_din = 0;
                Y_din = 0;
            }
            while (flag)
            {
                dt1 = Time[i];
                dt2 = Time[i + 1];
                if ((realtime > dt1) & (realtime >= dt2))
                {
                    i++;
                    count++;
                }
                if ((realtime >= dt1) & (realtime < dt2))
                {
                    if (realtime == dt1)
                        motion = false;
                    else
                        motion = true;

                    last_stops = marshrut.Ostanovki[count].Name;
                    flag = false;
                    rachet_S(marshrut.Ostanovki[count], marshrut.Ostanovki[count + 1], dt1, dt2, time);
                }

            }
        }   


    }
}

