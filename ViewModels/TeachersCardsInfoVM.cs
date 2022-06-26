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
    class TeachersCardsInfoVM : BaseViewModel
    {
        private RelayCommand _saveCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _addCommand;

        private TeacherCardInfo selectedTeacherCardInfo;
        public TeacherCardInfo SelectedTeacherCardInfo
        {
            get => selectedTeacherCardInfo;
            set
            {
                selectedTeacherCardInfo = value;
                OnPropertyChanged("SelectedTeacherCardInfo");
            }
        }
        private ObservableCollection<TeacherCardInfo> teacherCardsInfo;
        public ObservableCollection<TeacherCardInfo> TeacherCardsInfoData
        {
            get => teacherCardsInfo;
            set
            {
                teacherCardsInfo = value;
                OnPropertyChanged("TeacherCardsInfoData");
            }
        }

        public TeachersCardsInfoVM()
        {
            TeacherCardsInfoData = new ObservableCollection<TeacherCardInfo>();
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
                    , (param) => TeacherCardsInfoData.Count > 0));
            }
        }

        public void GetAll()
        {
            TeacherCardsInfoData = new ObservableCollection<TeacherCardInfo>();

            string sqlExpression = "SELECT * FROM Teachers_cards_Information";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                TeacherCardInfo addTeacherCardInfo;

                while (reader.Read())
                {
                    addTeacherCardInfo = new TeacherCardInfo()
                    {
                        Tci_Book_ID = int.Parse(reader["tci_Book_ID"].ToString()),
                        Tci_ID = int.Parse(reader["tci_ID"].ToString()),
                        Tci_receive_date = DateTime.Parse(reader["tci_Receive_date"].ToString()),
                        Tci_return_date = DateTime.Parse(reader["tci_Return_date"].ToString()),
                        Tci_deadline_date = DateTime.Parse(reader["tci_Deadline_date"].ToString()),
                        Tci_serving_librarian_id = int.Parse(reader["tci_Serving_librarian_ID"].ToString()),
                        Tci_teacher_card_ID = int.Parse(reader["tci_Teacher_card_ID"].ToString())
                    };
                    TeacherCardsInfoData.Add(addTeacherCardInfo);
                }
            }
        }

        public ObservableCollection<TeacherCardInfo> GetTeacherCardInfos(int teacherCardID)
        {
            var sampleTeacherCardInfo = new ObservableCollection<TeacherCardInfo> { };
            for (int i = 0; i < teacherCardsInfo.Count; i++)
            {
                if (teacherCardsInfo[i].Tci_teacher_card_ID == teacherCardID)
                { sampleTeacherCardInfo.Add(teacherCardsInfo[i]); }
            }
            return sampleTeacherCardInfo;
        }

        public void AddData()
        {
            TeacherCardInfo newTeacherCardInfo = new TeacherCardInfo();
            TeacherCardsInfoData.Insert(0, newTeacherCardInfo);
            SelectedTeacherCardInfo = newTeacherCardInfo;
        }

        public override void SaveData(object savedElm, string sqlExpressionAdd = null, string sqlExpressionUpdate = null, bool checkID = false)
        {
            TeacherCardInfo saveTeacherCardInfo = savedElm as TeacherCardInfo;

            sqlExpressionAdd = $"INSERT INTO Teachers_cards_Information(tci_Serving_librarian_ID, tci_Teacher_card_ID, tci_Book_ID, tci_Receive_date, tci_Return_date, tci_Deadline_date) VALUES ({saveTeacherCardInfo.Tci_serving_librarian_id}, {saveTeacherCardInfo.Tci_teacher_card_ID}," +
                $" {saveTeacherCardInfo.Tci_Book_ID}, '{saveTeacherCardInfo.Tci_receive_date}', '{saveTeacherCardInfo.Tci_return_date}', '{saveTeacherCardInfo.Tci_deadline_date}')";

            sqlExpressionUpdate = $"UPDATE Teachers_cards_Information SET tci_Serving_librarian_ID={saveTeacherCardInfo.Tci_serving_librarian_id}, tci_Teacher_card_ID={saveTeacherCardInfo.Tci_teacher_card_ID}, tci_Book_ID={saveTeacherCardInfo.Tci_Book_ID} ," +
                $" tci_Receive_date='{saveTeacherCardInfo.Tci_receive_date}', tci_Return_date='{saveTeacherCardInfo.Tci_return_date}', tci_Deadline_date='{saveTeacherCardInfo.Tci_deadline_date}' WHERE tci_ID={saveTeacherCardInfo.Tci_ID}";

            if (saveTeacherCardInfo != null && saveTeacherCardInfo.Tci_receive_date != null && saveTeacherCardInfo.Tci_return_date != null && saveTeacherCardInfo.Tci_teacher_card_ID != 0 && saveTeacherCardInfo.Tci_serving_librarian_id != 0)
            {
                checkID = saveTeacherCardInfo.Tci_ID <= 0;
                base.SaveData(saveTeacherCardInfo, sqlExpressionAdd, sqlExpressionUpdate, checkID);
            }
            else { MessageBox.Show("Data is not filled correct."); }

            GetAll();
        }

        public override void RemoveData(object deleteElm, string sqlExpressionDelete = null)
        {
            TeacherCardInfo  deleteTeacherCardInfo = deleteElm as TeacherCardInfo;

            if (deleteTeacherCardInfo != null)
            {
                sqlExpressionDelete = $"DELETE FROM Teachers_cards_Information WHERE tci_ID={deleteTeacherCardInfo.Tci_ID}";
                base.RemoveData(deleteTeacherCardInfo, sqlExpressionDelete);
            }
            else { MessageBox.Show("Data is not filled."); }

            GetAll();
        }
    }
}
