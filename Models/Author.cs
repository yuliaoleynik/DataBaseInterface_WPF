using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BDInterfaceLAB2
{
    public class Author : BaseViewModel
    {
        private string a_firstname;
        private string a_lastname;
        public int A_ID { get; set; }

        public string A_FirstName
        {
            get { return a_firstname; }
            set
            {
                a_firstname = value;
                OnPropertyChanged("A_FirstName");
            }
        }
        public string A_LastName
        {
            get { return a_lastname; }
            set
            {
                a_lastname = value;
                OnPropertyChanged("A_LastName");
            }
        }
    }
}
