using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
using System.Drawing;

namespace Step_v0
{
    class Glades
    {
        public static int K = 0;
        Train t; Passanger pas; Bilet bil; Distanation dist;
        public static List<Train> trains = new List<Train>();
        public static List<Distanation> marshruti = new List<Distanation>();
        public static List<Passanger> passangers = new List<Passanger>();
        public static List<Bilet> bilets = new List<Bilet>();
        public static List<Train> current_trains = new List<Train>();


        public static List<TrainControl> list_ctrl_train = new List<TrainControl>();


        public void PoiskVsexPoezdov()
        {
            XmlDocument Doc = new XmlDocument();
            Doc.Load("Poezd.xml");
            XmlElement Root = Doc.DocumentElement;
            string nomer = "", type = "", id = "";
            int Hour, Min;
            foreach (XmlNode node in Root)
            {
                List<string> distance = new List<string>();
                List<DateTime> time = new List<DateTime>();
                List<Passanger> passangers = new List<Passanger>();
                if (node.Attributes.Count > 0)
                {
                    XmlNode attr = node.Attributes.GetNamedItem("name");
                    if (attr != null)
                        nomer = attr.Value;
                }
                foreach (XmlNode childnode in node.ChildNodes)
                {
                    if (childnode.Name == "type")
                    {
                        type = childnode.InnerText;
                    }
                    if (childnode.Name == "distanation")
                    {
                        id = childnode.InnerText;
                    }
                    if (childnode.Name == "time")
                    {
                        string[] info = childnode.InnerText.Split(' ');
                        for (int i = 0; i < info.Length; i++)
                        {
                            string[] info1 = (info[i].Split(':'));
                            Hour = int.Parse(info1[0]);
                            Min = int.Parse(info1[1]);
                            DateTime dt = new DateTime();
                            dt = dt.AddHours(Hour);
                            dt = dt.AddMinutes(Min);
                            time.Add(dt);
                        }
                    }
                }
                passangers = PoiskVsexPasagirov(nomer);
                t = new Train(nomer, type, id, time, passangers);
                trains.Add(t);
                distance = null;
                time = null;
                passangers = null;
            }
        }

        public static Stops Poiskostanovoki(string name_ostanovki)
        {
            Stops ost;
            XmlDocument Doc = new XmlDocument();
            Doc.Load("Ostanovki.xml");
            XmlElement Root = Doc.DocumentElement;
            string name = ""; int cor_x = 0, cor_y = 0;
            foreach (XmlNode node in Root)
            {
                if (node.Attributes.Count > 0)
                {
                    XmlNode attr = node.Attributes.GetNamedItem("name");
                    if (attr != null)
                        name = attr.Value;
                }
                if (name_ostanovki == name)
                {
                    foreach (XmlNode childnode in node.ChildNodes)
                    {
                        if (childnode.Name == "info")
                        {
                            string[] info = childnode.InnerText.Split(' ');
                            cor_x = int.Parse(info[0]);
                            cor_y = int.Parse(info[1]);
                        }
                    }
                    return ost = new Stops(name, cor_x, cor_y);
                }
            }
            return ost = new Stops("null", 0, 0);
        }

        public void PoiskVsexmarshrutov()
        {
            XmlDocument Doc = new XmlDocument();
            Doc.Load("Distanation.xml");
            XmlElement Root = Doc.DocumentElement;
            string start = "", end = "", id = "";
            foreach (XmlNode node in Root)
            {
                List<Stops> ostanovki = new List<Stops>();
                if (node.Attributes.Count > 0)
                {
                    XmlNode attr = node.Attributes.GetNamedItem("id");
                    if (attr != null)
                        id = attr.Value;
                }
                foreach (XmlNode childnode in node.ChildNodes)
                {
                    if (childnode.Name == "Start")
                    {
                        start = childnode.InnerText;
                    }
                    if (childnode.Name == "End")
                    {
                        end = childnode.InnerText;
                    }
                    if (childnode.Name == "stops")
                    {
                        string[] info = childnode.InnerText.Split(' ');
                        for (int i = 0; i < info.Length; i++)
                        {
                            ostanovki.Add(Poiskostanovoki(info[i]));
                        }
                    }
                }
                dist = new Distanation(id, start, end, ostanovki);
                marshruti.Add(dist);
                ostanovki = null;
            }
        }

        public static List<Passanger> PoiskVsexPasagirov(string nomer)
        {
            Passanger pas; Bilet bil;
            XmlDocument Doc = new XmlDocument();
            List<Passanger> passangers_for_current_train = new List<Passanger>();
            Doc.Load("Passanger.xml");
            XmlElement Root = Doc.DocumentElement;
            string n = "", s = "", nomer_poezda = "", nomer_vagona = "", mesto_nomer = "", mesto_bukva = "";
            foreach (XmlNode node in Root)
            {
                foreach (XmlNode childnode in node.ChildNodes)
                {
                    if (childnode.Name == "info")
                    {
                        string[] info = childnode.InnerText.Split(' ');
                        n = info[1]; s = info[0]; nomer_poezda = info[2]; nomer_vagona = info[3]; mesto_nomer = info[4]; mesto_bukva = info[5];
                    }
                }
                if (nomer == nomer_poezda)
                {
                    bil = new Bilet(nomer_poezda, nomer_vagona, mesto_nomer, mesto_bukva);
                    bilets.Add(bil);
                    pas = new Passanger(n, s, nomer_poezda, bilets);
                    passangers_for_current_train.Add(pas);

                }

            }
            return passangers_for_current_train;
        }

        public static int Proverka_poezdov(string s, List<Train> c_t)
        {
            int p = 0;
            for (int i = 0; i < c_t.Count; i++)
            {
                if (s == c_t[i].Nomer)
                {
                    p = 1;
                    return p;
                }
                else { p = 0; }
            }
            return p;
        }

          /////////////////////////////////////////////////
        public void create_ctrl()
        {
            bool flag1 = true;
            bool flag2 = true;
            bool flag3 = true;
            List<Stops> list_ostanovok=null;
            int count = 0;
            int i = 0, q = 0;
            while (flag1)
            {


                while (flag2)
                {

                    while (flag3)
                    {
                        if (current_trains[i].ID == marshruti[q].ID)
                        {
                            list_ostanovok = marshruti[q].Ostanovki;
                            flag3 = false;
                            q = 0;

                        }
                        else
                        {
                            q++;
                        }
                    }


                    if (current_trains[i].last_stops == list_ostanovok[count].Name)
                    {
                        if (list_ctrl_train.Count == 0)
                        {
                            TrainControl tr = new TrainControl(list_ostanovok[count].X - 10 + current_trains[i].X_din, list_ostanovok[count].Y - 22 + current_trains[i].Y_din);
                           // pictureBox1.Controls.Add(tr);
                            tr.coll_train.Add(current_trains[i]);
                            i++;
                            flag2 = false;
                            list_ctrl_train.Add(tr);

                          // tr.Click += new System.EventHandler(tr_Click);
                           // tr.MouseEnter += new System.EventHandler(tr_MouseEnter);
                           // tr.MouseLeave += new System.EventHandler(tr_MouseLeave);
                        }
                        else
                        {
                            for (int k = 0; k < list_ctrl_train.Count; k++)
                            {
                                TrainControl test_tr;
                                test_tr = list_ctrl_train[k];
                                if (i < current_trains.Count)
                                {
                                    if ((test_tr.ctrl_x == (list_ostanovok[count].X - 10 + current_trains[i].X_din)) & (test_tr.ctrl_y == (list_ostanovok[count].Y - 22 + current_trains[i].Y_din)))
                                    {
                                        test_tr.coll_train.Add(current_trains[i]);
                                        i++;
                                        flag2 = false;

                                    }
                                }
                            }

                            if (flag2 == true)
                            {
                                TrainControl tr = new TrainControl(list_ostanovok[count].X - 10 + current_trains[i].X_din, list_ostanovok[count].Y - 22 + current_trains[i].Y_din);
                               
                               // tr.Click += new System.EventHandler(tr_Click);
                              //  tr.MouseEnter += new System.EventHandler(tr_MouseEnter);
                              //  tr.MouseLeave += new System.EventHandler(tr_MouseLeave);
                              //  pictureBox1.Controls.Add(tr);
                                tr.coll_train.Add(current_trains[i]);
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

                if (i == current_trains.Count)
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
        /////////////////////////////////////////////////////////////////
       


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
      



    }
}
