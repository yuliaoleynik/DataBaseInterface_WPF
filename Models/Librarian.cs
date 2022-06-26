using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BDInterfaceLAB2
{
    public class Librarian : BaseViewModel
    {
        private int l_id;
        private string l_firstname;
        private string l_lastname;
        private string l_phone;

        public int L_ID
        {
            get { return l_id; }
            set
            {
                l_id = value;
                OnPropertyChanged("L_ID");
            }
        }
        public string L_FirstName
        {
            get { return l_firstname; }
            set
            {
                l_firstname = value;
                OnPropertyChanged("L_FirstName");
            }
        }
        public string L_LastName
        {
            get { return l_lastname; }
            set
            {
                l_lastname = value;
                OnPropertyChanged("L_LastName");
            }
        }
        public string L_Phone
        {
            get { return l_phone; }
            set
            {
                l_phone = value;
                OnPropertyChanged("L_Phone");
            }
        }
    }
}
