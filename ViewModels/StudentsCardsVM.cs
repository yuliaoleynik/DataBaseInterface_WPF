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
    class StudentsCardsVM : BaseViewModel
    {
        private RelayCommand _saveCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _addCommand;

        private StudentCard selectedStudentCard;
        public StudentCard SelectedStudentCard
        {
            get => selectedStudentCard;
            set
            {
                selectedStudentCard = value;
                OnPropertyChanged("SelectedStudentCard");
            }
        }
        private ObservableCollection<StudentCard> studentsCards;
        public ObservableCollection<StudentCard> StudentCardsData
        {
            get => studentsCards;
            set
            {
                studentsCards = value;
                OnPropertyChanged("StudentCardsData");
            }
        }

        public StudentsCardsVM()
        {
            StudentCardsData = new ObservableCollection<StudentCard>();
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
                    , (param) => StudentCardsData.Count > 0));
            }
        }

        public void GetAll()
        {
            StudentCardsData = new ObservableCollection<StudentCard>();

            string sqlExpression = "SELECT * FROM Student_cards";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                StudentCard addStudentCard;

                while (reader.Read())
                {
                    addStudentCard = new StudentCard()
                    {
                        SC_Date_of_Creation = DateTime.Parse(reader["sc_Date_of_creation"].ToString()),
                        SC_ID = int.Parse(reader["sc_ID"].ToString()),
                        SC_Student_ID = int.Parse(reader["sc_Student_ID"].ToString()),
                        SC_Date_of_Expiration = DateTime.Parse(reader["sc_Date_of_expiration"].ToString())
                    };
                    StudentCardsData.Add(addStudentCard);
                }
            }
        }

        public void AddData()
        {
            StudentCard newStudentCard = new StudentCard();
            StudentCardsData.Insert(0, newStudentCard);
            SelectedStudentCard = newStudentCard;
        }

        public override void SaveData(object savedElm, string sqlExpressionAdd = null, string sqlExpressionUpdate = null, bool checkID = false)
        {
            StudentCard saveStudentCard = savedElm as StudentCard;

            sqlExpressionAdd = $"INSERT INTO Student_cards(sc_Date_of_creation, sc_Student_ID, sc_Date_of_expiration) VALUES ('{saveStudentCard.SC_Date_of_Creation}', {saveStudentCard.SC_Student_ID}," +
                $" '{saveStudentCard.SC_Date_of_Expiration}')";

            sqlExpressionUpdate = $"UPDATE Student_cards SET sc_Date_of_creation='{saveStudentCard.SC_Date_of_Creation}', sc_Date_of_expiration='{saveStudentCard.SC_Date_of_Expiration}' WHERE sc_ID={saveStudentCard.SC_ID}";

            if (saveStudentCard != null && saveStudentCard.SC_Date_of_Expiration != null && saveStudentCard.SC_Date_of_Creation != null && saveStudentCard.SC_Student_ID != 0)
            {
                checkID = saveStudentCard.SC_ID <= 0;
                base.SaveData(saveStudentCard, sqlExpressionAdd, sqlExpressionUpdate, checkID);
            }
            else { MessageBox.Show("Data is not filled."); }

            GetAll();
        }

        public override void RemoveData(object deleteElm, string sqlExpressionDelete = null)
        {
            StudentCard deleteStudentCard = deleteElm as StudentCard;

            if (deleteStudentCard != null)
            {
                sqlExpressionDelete = $"DELETE FROM Student_cards WHERE sc_ID={deleteStudentCard.SC_ID}";
                base.RemoveData(deleteStudentCard, sqlExpressionDelete);
            }
            else { MessageBox.Show("Data is not filled correct."); }

            GetAll();
        }
    }
}
