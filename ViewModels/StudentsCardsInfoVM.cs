using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Windows;

namespace BDInterfaceLAB2
{
    class StudentsCardsInfoVM : BaseViewModel
    {
        private RelayCommand _saveCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _addCommand;

        private StudentCardInfo selectedStudentCardInfo;
        public StudentCardInfo SelectedStudentCardInfo
        {
            get => selectedStudentCardInfo;
            set
            {
                selectedStudentCardInfo = value;
                OnPropertyChanged("SelectedStudentCardInfo");
            }
        }
        private ObservableCollection<StudentCardInfo> studentsCardsInfo;
        public ObservableCollection<StudentCardInfo> StudentsCardsInfoData
        {
            get => studentsCardsInfo;
            set
            {
                studentsCardsInfo = value;
                OnPropertyChanged("StudentsCardsInfoData");
            }
        }

        public StudentsCardsInfoVM()
        {
            StudentsCardsInfoData = new ObservableCollection<StudentCardInfo>();
            GetAll();
        }

        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??
                    (_addCommand = new RelayCommand(param => AddData(), null));
            }
        }
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ??
                    (_saveCommand = new RelayCommand(param =>
                    {
                        SaveData(param);
                    }
                    , null));
            }
        }
        public RelayCommand DeleteCommand
        {
            get
            {
                return _deleteCommand ??
                    (_deleteCommand = new RelayCommand(param =>
                    {
                        RemoveData(param);
                    }
                    , (param) => StudentsCardsInfoData.Count > 0));
            }
        }

        public void GetAll()
        {
            StudentsCardsInfoData = new ObservableCollection<StudentCardInfo>();

            string sqlExpression = "SELECT * FROM Students_cards_Information";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                StudentCardInfo addStudentCardInfo;

                while (reader.Read())
                {
                    addStudentCardInfo = new StudentCardInfo()
                    {
                        Sci_book_ID = int.Parse(reader["sci_Book_ID"].ToString()),
                        Sci_ID = int.Parse(reader["sci_ID"].ToString()),
                        Sci_receive_date = DateTime.Parse(reader["sci_Receive_date"].ToString()),
                        Sci_return_date = DateTime.Parse(reader["sci_Return_date"].ToString()),
                        Sci_deadline_date = DateTime.Parse(reader["sci_Deadline_date"].ToString()),
                        Sci_serving_librarian_id = int.Parse(reader["sci_Serving_librarian_ID"].ToString()),
                        Sci_student_card_ID = int.Parse(reader["sci_Student_card_ID"].ToString())
                    };
                    StudentsCardsInfoData.Add(addStudentCardInfo);
                }
            }
        }

        public ObservableCollection<StudentCardInfo> GetStudentCardInfos(int studentCardID)
        {
            var sampleStudentCardInfo = new ObservableCollection<StudentCardInfo> { };
            for(int i = 0; i < studentsCardsInfo.Count; i++)
            {
                if (studentsCardsInfo[i].Sci_student_card_ID == studentCardID)
                { sampleStudentCardInfo.Add(studentsCardsInfo[i]); }
            }
            return sampleStudentCardInfo;
        }

        public void AddData()
        {
            StudentCardInfo newStudentCardInfo = new StudentCardInfo();
            StudentsCardsInfoData.Insert(0, newStudentCardInfo);
            SelectedStudentCardInfo = newStudentCardInfo;
        }

        public override void SaveData(object savedElm, string sqlExpressionAdd = null, string sqlExpressionUpdate = null, bool checkID = false)
        {
            StudentCardInfo saveStudentCardInfo = savedElm as StudentCardInfo;

            sqlExpressionAdd = $"INSERT INTO Students_cards_Information(sci_Serving_librarian_ID, sci_Student_card_ID, sci_Book_ID, sci_Receive_date, sci_Return_date, sci_Deadline_date) VALUES ({saveStudentCardInfo.Sci_serving_librarian_id}, {saveStudentCardInfo.Sci_student_card_ID}," +
                $" {saveStudentCardInfo.Sci_book_ID}, '{saveStudentCardInfo.Sci_receive_date}', '{saveStudentCardInfo.Sci_return_date}', '{saveStudentCardInfo.Sci_deadline_date}')";

            sqlExpressionUpdate = $"UPDATE Students_cards_Information SET sci_Serving_librarian_ID={saveStudentCardInfo.Sci_serving_librarian_id}, sci_Student_card_ID={saveStudentCardInfo.Sci_student_card_ID}, sci_Book_ID={saveStudentCardInfo.Sci_book_ID} ," +
                $" sci_Receive_date='{saveStudentCardInfo.Sci_receive_date}' , sci_Return_date='{saveStudentCardInfo.Sci_return_date}', sci_Deadline_date='{saveStudentCardInfo.Sci_deadline_date}' WHERE sci_ID={saveStudentCardInfo.Sci_ID}";

            if (saveStudentCardInfo != null && saveStudentCardInfo.Sci_receive_date != null && saveStudentCardInfo.Sci_return_date != null && saveStudentCardInfo.Sci_student_card_ID != 0 && saveStudentCardInfo.Sci_serving_librarian_id != 0)
            {
                checkID = saveStudentCardInfo.Sci_ID <= 0;
                base.SaveData(saveStudentCardInfo, sqlExpressionAdd, sqlExpressionUpdate, checkID);
            }
            else { MessageBox.Show("Data is not filled."); }

            GetAll();
        }

        public override void RemoveData(object deleteElm, string sqlExpressionDelete = null)
        {
            StudentCardInfo deleteStudentCardInfo = deleteElm as StudentCardInfo;

            if (deleteStudentCardInfo != null)
            {
                sqlExpressionDelete = $"DELETE FROM Students_cards_Information WHERE sci_ID={deleteStudentCardInfo.Sci_ID}";
                base.RemoveData(deleteStudentCardInfo, sqlExpressionDelete);
            }
            else { MessageBox.Show("Data is not filled correct."); }

            GetAll();
        }
    }
}
