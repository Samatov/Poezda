using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Step_v0
{
    public partial class TrainControl : UserControl
    {
        public List<Train> list_numbers = new List<Train>();
        public int x_ctrl, y_ctrl;
        public TrainControl()
        {
            InitializeComponent();
        }
    }
}
