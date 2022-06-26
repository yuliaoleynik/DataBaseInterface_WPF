using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BDInterfaceLAB2
{
    public class Publisher : BaseViewModel
    {
        private int p_id;
        private string p_name;

        public int P_ID
        {
            get { return p_id; }
            set
            {
                p_id = value;
                OnPropertyChanged("P_ID");
            }
        }
        public string P_Name
        {
            get { return p_name; }
            set
            {
                p_name = value;
                OnPropertyChanged("P_Name");
            }

        }
    }
}
