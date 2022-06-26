using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BDInterfaceLAB2
{
    public class Student : BaseViewModel
    {
        private int s_id;
        private string s_firstname;
        private string s_lastname;
        private int s_group_id;
        private string s_phone;

        public int S_ID
        {
            get { return s_id; }
            set
            {
                s_id = value;
                OnPropertyChanged("S_ID");
            }
        }
        public string S_FirstName
        {
            get { return s_firstname; }
            set
            {
                s_firstname = value;
                OnPropertyChanged("S_FirstName");
            }
        }
        public string S_LastName
        {
            get { return s_lastname; }
            set
            {
                s_lastname = value;
                OnPropertyChanged("S_LastName");
            }
        }
        public string S_Phone
        {
            get { return s_phone; }
            set
            {
                s_phone = value;
                OnPropertyChanged("S_Phone");
            }
        }
        public int S_Group_ID
        {
            get { return s_group_id; }
            set
            {
                s_group_id = value;
                OnPropertyChanged("S_Group_ID");
            }
        }
    }
}
