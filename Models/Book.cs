using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BDInterfaceLAB2
{
    public class Book : BaseViewModel
    {
        private int b_id;
        private string b_name;
        private int b_author_id;
        private int b_publisher_id;
        private DateTime b_publication_date;

        public int B_ID
        {
            get { return b_id; }
            set
            {
                b_id = value;
                OnPropertyChanged("B_ID");
            }
        }
        public string B_Name
        {
            get { return b_name; }
            set
            {
                b_name = value;
                OnPropertyChanged("B_Name");
            }
        }
        public int B_Author_ID
        {
            get { return b_author_id; }
            set
            {
                b_author_id = value;
                OnPropertyChanged("B_Author_ID");
            }
        }
        public int B_Publisher_ID
        {
            get { return b_publisher_id; }
            set
            {
                b_publisher_id = value;
                OnPropertyChanged("B_Publisher_ID");
            }
        }
        public DateTime B_Publication_Date
        {
            get { return b_publication_date; }
            set
            {
                b_publication_date = value;
                OnPropertyChanged("B_Publication_Date");
            }
        }
    }
}
