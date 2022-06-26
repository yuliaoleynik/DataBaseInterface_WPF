using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BDInterfaceLAB2
{
    public class Teacher : BaseViewModel
    {
        private int t_id;
        private string t_firstname;
        private string t_lastname;
        private int t_faculty_id;
        private string t_phone;

        public int T_ID
        {
            get { return t_id; }
            set
            {
                t_id = value;
                OnPropertyChanged("T_ID");
            }
        }
        public string T_FirstName
        {
            get { return t_firstname; }
            set
            {
                t_firstname = value;
                OnPropertyChanged("T_FirstName");
            }
        }
        public string T_LastName
        {
            get { return t_lastname; }
            set
            {
                t_lastname = value;
                OnPropertyChanged("T_LastName");
            }
        }
        public string T_Phone
        {
            get { return t_phone; }
            set
            {
                t_phone = value;
                OnPropertyChanged("T_Phone");
            }
        }
        public int T_Faculty_ID
        {
            get { return t_faculty_id; }
            set
            {
                t_faculty_id = value;
                OnPropertyChanged("T_Faculty_ID");
            }
        }
    }
}
