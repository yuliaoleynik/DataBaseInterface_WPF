using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BDInterfaceLAB2
{
    public class StudentCard : BaseViewModel
    {
        private int sc_id;
        private int sc_student_id;
        private DateTime sc_date_of_creation;
        private DateTime sc_date_of_expiration;        

        public int SC_ID
        {
            get { return sc_id; }
            set
            {
                sc_id = value;
                OnPropertyChanged("SC_ID");
            }
        }
        public int SC_Student_ID
        {
            get { return sc_student_id; }
            set
            {
                sc_student_id = value;
                OnPropertyChanged("SC_Date_of_Creation");
            }
        }
        public DateTime SC_Date_of_Creation
        {
            get { return sc_date_of_creation; }
            set
            {
                sc_date_of_creation = value;
                OnPropertyChanged("SC_Date_of_Creation");
            }
        }
        public DateTime SC_Date_of_Expiration
        {
            get { return sc_date_of_expiration; }
            set
            {
                sc_date_of_expiration = value;
                OnPropertyChanged("SC_Date_of_Expiration");
            }
        }
    }
}
