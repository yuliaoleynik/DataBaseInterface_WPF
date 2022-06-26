using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BDInterfaceLAB2
{
    public class Group : BaseViewModel
    {
        private int g_id;
        private string g_name;
        private int g_faculty_id;

        public int G_ID
        {
            get { return g_id; }
            set
            {
                g_id = value;
                OnPropertyChanged("G_ID");
            }

        }
        public string G_Name
        {
            get { return g_name; }
            set
            {
                g_name = value;
                OnPropertyChanged("G_Name");
            }
        }
        public int G_Faculty_ID
        {
            get { return g_faculty_id; }
            set
            {
                g_faculty_id = value;
                OnPropertyChanged("G_Faculty_ID");
            }
        }
    }
}
