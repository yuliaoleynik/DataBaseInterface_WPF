using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BDInterfaceLAB2
{
    class StudentCardInfo : BaseViewModel
    {
        private int sci_id;
        private int sci_student_card_id;
        private int sci_book_ID;
        private int sci_serving_librarian_id;
        private DateTime sci_receive_date;
        private DateTime sci_return_date;
        private DateTime sci_deadline_date;

        public int Sci_ID
        {
            get { return sci_id; }
            set
            {
                sci_id = value;
                OnPropertyChanged("Sci_ID");
            }
        }
        public int Sci_student_card_ID
        {
            get { return sci_student_card_id; }
            set
            {
                sci_student_card_id = value;
                OnPropertyChanged("Sci_student_card_ID");
            }
        }
        public int Sci_book_ID
        {
            get { return sci_book_ID; }
            set
            {
                sci_book_ID = value;
                OnPropertyChanged("Sci_book_ID");
            }
        }
        public int Sci_serving_librarian_id
        {
            get { return sci_serving_librarian_id; }
            set
            {
                sci_serving_librarian_id = value;
                OnPropertyChanged("Sci_serving_librarian_id");
            }
        }
        public DateTime Sci_receive_date
        {
            get { return sci_receive_date; }
            set
            {
                sci_receive_date = value;
                OnPropertyChanged("Sci_receive_date");
            }
        }
        public DateTime Sci_return_date
        {
            get { return sci_return_date; }
            set
            {
                sci_return_date = value;
                OnPropertyChanged("Sci_return_date");
            }
        }
        public DateTime Sci_deadline_date
        {
            get { return sci_deadline_date; }
            set
            {
                sci_deadline_date = value;
                OnPropertyChanged("Sci_deadline_date");
            }
        }
    }
}
