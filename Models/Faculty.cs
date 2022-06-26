using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace BDInterfaceLAB2
{
    public class Faculty : BaseViewModel
    {
        private int f_id;
        private string f_name;

        public int F_ID
        {
            get { return f_id; }
            set
            {
                f_id = value;
                OnPropertyChanged("F_ID");
            }
        }
        public string F_Name
        {
            get { return f_name; }
            set
            {
                f_name = value;
                OnPropertyChanged("F_Name");
            }

        }
    }
}
