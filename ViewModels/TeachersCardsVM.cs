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
    class TeachersCardsVM : BaseViewModel
    {
        private RelayCommand _saveCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _addCommand;

        private TeacherCard selectedTeacherCard;
        public TeacherCard SelectedTeacherCard
        {
            get => selectedTeacherCard;
            set
            {
                selectedTeacherCard = value;
                OnPropertyChanged("SelectedTeacherCard");
            }
        }
        private ObservableCollection<TeacherCard> teacherCards;
        public ObservableCollection<TeacherCard> TeacherCardsData
        {
            get => teacherCards;
            set
            {
                teacherCards = value;
                OnPropertyChanged("TeacherCardsData");
            }
        }

        public TeachersCardsVM()
        {
            TeacherCardsData = new ObservableCollection<TeacherCard>();
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
                    , (param) => TeacherCardsData.Count > 0));
            }
        }

        public void GetAll()
        {
            TeacherCardsData = new ObservableCollection<TeacherCard>();

            string sqlExpression = "SELECT * FROM Teacher_cards";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                TeacherCard addTeacherCard;

                while (reader.Read())
                {
                    addTeacherCard = new TeacherCard()
                    {
                        TC_Date_of_Creation = DateTime.Parse(reader["tc_Date_of_creation"].ToString()),
                        TC_ID = int.Parse(reader["tc_ID"].ToString()),
                        TC_Teacher_ID = int.Parse(reader["tc_Teacher_ID"].ToString()),
                        TC_Date_of_Expiration = DateTime.Parse(reader["tc_Date_of_expiration"].ToString())
                    };
                    TeacherCardsData.Add(addTeacherCard);
                }
            }
        }

        public void AddData()
        {
            TeacherCard newTeacherCard = new TeacherCard();
            TeacherCardsData.Insert(0, newTeacherCard);
            SelectedTeacherCard = newTeacherCard;
        }

        public override void SaveData(object savedElm, string sqlExpressionAdd = null, string sqlExpressionUpdate = null, bool checkID = false)
        {
            TeacherCard saveTeacherCard = savedElm as TeacherCard;

            sqlExpressionAdd = $"INSERT INTO Teacher_cards(tc_Date_of_creation, tc_Teacher_ID, tc_Date_of_expiration) VALUES ('{saveTeacherCard.TC_Date_of_Creation}', {saveTeacherCard.TC_Teacher_ID}," +
                $" '{saveTeacherCard.TC_Date_of_Expiration}')";

            sqlExpressionUpdate = $"UPDATE Teacher_cards SET tc_Date_of_creation='{saveTeacherCard.TC_Date_of_Creation}', tc_Date_of_expiration='{saveTeacherCard.TC_Date_of_Expiration}' WHERE tc_ID={saveTeacherCard.TC_ID}";

            if (saveTeacherCard != null && saveTeacherCard.TC_Date_of_Expiration != null && saveTeacherCard.TC_Date_of_Creation != null && saveTeacherCard.TC_Teacher_ID != 0)
            {
                checkID = saveTeacherCard.TC_ID <= 0;
                base.SaveData(saveTeacherCard, sqlExpressionAdd, sqlExpressionUpdate, checkID);
            }
            else { MessageBox.Show("Data is not filled correct."); }

            GetAll();
        }

        public override void RemoveData(object deleteElm, string sqlExpressionDelete = null)
        {
            TeacherCard deleteTeacherCard = deleteElm as TeacherCard;

            if (deleteTeacherCard != null)
            {
                sqlExpressionDelete = $"DELETE FROM Teacher_cards WHERE tc_ID={deleteTeacherCard.TC_ID}";
                base.RemoveData(deleteTeacherCard, sqlExpressionDelete);
            }
            else { MessageBox.Show("Data is not filled."); }

            GetAll();
        }
    }
}
