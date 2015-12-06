﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Step_v0
{
    class Passanger
    {
        public string Surname, Name, Nomer_poezda, Nomer_Vagona, Mesto_Nomer, Mesto_Bukva;
        public List<Bilet> Bilets= new List<Bilet>();
        public Passanger(string n, string s, string nomer_poezda, List<Bilet> bilets)
        {
            Name = n;
            Surname = s;
            Nomer_poezda = nomer_poezda;
            Bilets = bilets;
        }
        public void Vivod(ref DataGridView vivod_pas, ref int i)
        {
            vivod_pas.Rows.Add();
            vivod_pas.Rows[i].Cells[0].Value = Surname;
            vivod_pas.Rows[i].Cells[1].Value = Name;
            vivod_pas.Rows[i].Cells[2].Value = Nomer_poezda;
            vivod_pas.Visible = true;
        }
    }
  }