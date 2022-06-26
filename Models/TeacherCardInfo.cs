using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BDInterfaceLAB2
{
    public class TeacherCardInfo : BaseViewModel
    {
        private int tci_id;
        private int tci_teacher_card_id;
        private int tci_book_ID;
        private int tci_serving_librarian_id;
        private DateTime tci_receive_date;
        private DateTime tci_return_date;
        private DateTime tci_deadline_date;

        public int Tci_ID
        {
            get { return tci_id; }
            set
            {
                tci_id = value;
                OnPropertyChanged("Tci_ID");
            }
        }
        public int Tci_teacher_card_ID
        {
            get { return tci_teacher_card_id; }
            set
            {
                tci_teacher_card_id = value;
                OnPropertyChanged("Tci_teacher_card_ID");
            }
        }
        public int Tci_Book_ID 
        { 
            get { return tci_book_ID; }
            set
            {
                tci_book_ID = value;
                OnPropertyChanged("Tci_Book_ID");
            }
        }
        public int Tci_serving_librarian_id
        {
            get { return tci_serving_librarian_id; }
            set
            {
                tci_serving_librarian_id = value;
                OnPropertyChanged("Tci_serving_librarian_id");
            }
        }
        public DateTime Tci_receive_date
        {
            get { return tci_receive_date; }
            set
            {
                tci_receive_date = value;
                OnPropertyChanged("Tci_receive_date");
            }
        }
        public DateTime Tci_return_date
        {
            get { return tci_return_date; }
            set
            {
                tci_return_date = value;
                OnPropertyChanged("Tci_return_date");
            }
        }
        public DateTime Tci_deadline_date
        {
            get { return tci_deadline_date; }
            set
            {
                tci_deadline_date = value;
                OnPropertyChanged("Tci_deadline_time");
            }
        }
    }
}
