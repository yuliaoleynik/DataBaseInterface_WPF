using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BDInterfaceLAB2
{
    public class TeacherCard : BaseViewModel
    {
        private int tc_id;
        private int tc_teacher_id;
        private DateTime tc_date_of_creation;
        private DateTime tc_date_of_expiration;

        public int TC_ID
        {
            get { return tc_id; }
            set
            {
                tc_id = value;
                OnPropertyChanged("TC_ID");
            }
        }
        public int TC_Teacher_ID
        {
            get { return tc_teacher_id; }
            set
            {
                tc_teacher_id = value;
                OnPropertyChanged("TC_Teacher_ID");
            }
        }
        public DateTime TC_Date_of_Creation
        {
            get { return tc_date_of_creation; }
            set
            {
                tc_date_of_creation = value;
                OnPropertyChanged("TC_Date_of_Creation");
            }
        }
        public DateTime TC_Date_of_Expiration
        {
            get { return tc_date_of_expiration; }
            set
            {
                tc_date_of_expiration = value;
                OnPropertyChanged("TC_Date_of_Expiration");
            }
        }
    }
}
