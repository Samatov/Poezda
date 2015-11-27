﻿using System.IO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace Step_v0
{
    public class Train
    {
        int x, y;
        public string Nomer, Type;
        public List<DateTime> Time;
        public List<string> Distance;


        public Train(string n, string t, List<string> ostanovki, List<DateTime> Vreme_ostanovkok)
        {
            Nomer = n;
            Type = t;
            Distance = ostanovki;
            Time = Vreme_ostanovkok;
            
        }


        public void Vivod(ref ListBox vivod, ref TextBox t4, ref TextBox t3, ref TextBox nomer) {
            string str = nomer.Text;
            List<string> collection = new List<string>();
            bool flag = true;
            if (str == "")
            {
                flag = false;
                vivod.Items.Add(Nomer + ' ' + Type + ' ' +Distance[0]+' ' + Distance[Distance.Count-1]+ ' '
                    + Time[0].ToString("HH:mm") + ' ' + Time[Time.Count-1].ToString("HH:mm"));
                if (flag == true)
                {
                    // collection = Train.PoiskPoezda(str);
                    if (collection[0].Contains("null"))
                    {
                        vivod.Items.Add("Совпадений не найдено");
                    }
                    else
                    {
                        //info = collection[i].Split('{');
                        // vivod.Items.Add(info[i]);
                        // marsh = info[0].Split(' ');
                        //t4.Text = marsh[2];
                        //t3.Text = marsh[3];
                    }
                }
                vivod.Visible = true;
            }
        }
    }
}

