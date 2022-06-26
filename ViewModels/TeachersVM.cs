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
    class TeachersVM : BaseViewModel
    {
        private RelayCommand _saveCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _addCommand;

        private Teacher selectedTeacher;
        public Teacher SelectedTeacher
        {
            get => selectedTeacher;
            set
            {
                selectedTeacher = value;
                OnPropertyChanged("SelectedTeacher");
            }
        }
        private ObservableCollection<Teacher> teachers;
        public ObservableCollection<Teacher> TeachersData
        {
            get => teachers;
            set
            {
                teachers = value;
                OnPropertyChanged("TeachersData");
            }
        }

        public TeachersVM()
        {
            TeachersData = new ObservableCollection<Teacher>();
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
                    , (param) => TeachersData.Count > 0));
            }
        }

        public void GetAll()
        {
            TeachersData = new ObservableCollection<Teacher>();

            string sqlExpression = "SELECT * FROM Teachers";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Teacher addTeacher;

                while (reader.Read())
                {
                    addTeacher = new Teacher()
                    {
                        T_ID = int.Parse(reader["t_ID"].ToString()),
                        T_FirstName = reader["t_FirstName"].ToString(),
                        T_LastName = reader["t_LastName"].ToString(),
                        T_Phone = reader["t_Phone"].ToString(),
                        T_Faculty_ID = int.Parse(reader["t_Faculty_Id"].ToString())
                    };
                    TeachersData.Add(addTeacher);
                }
            }
        }

        public void AddData()
        {
            Teacher newTeacher = new Teacher();
            TeachersData.Insert(0, newTeacher);
            SelectedTeacher = newTeacher;
        }

        public override void SaveData(object savedElm, string sqlExpressionAdd = null, string sqlExpressionUpdate = null, bool checkID = false)
        {
            Teacher savedTeacher = savedElm as Teacher;
            checkID = savedTeacher.T_ID <= 0;

            sqlExpressionAdd = $"INSERT INTO Teachers(t_FirstName, t_LastName, t_Phone, t_Faculty_Id) VALUES " +
                $"('{savedTeacher.T_FirstName}', '{savedTeacher.T_LastName}', '{savedTeacher.T_Phone}', {savedTeacher.T_Faculty_ID})";

            sqlExpressionUpdate = $"UPDATE Teachers SET t_FirstName='{savedTeacher.T_FirstName}', t_LastName='{savedTeacher.T_LastName}', " +
                $"t_Phone='{savedTeacher.T_Phone}', t_Faculty_Id={savedTeacher.T_Faculty_ID} WHERE t_ID={savedTeacher.T_ID}";

            if (savedTeacher != null && savedTeacher.T_FirstName != null && savedTeacher.T_LastName != null && savedTeacher.T_Phone != null && savedTeacher.T_Faculty_ID != 0)
            {
                base.SaveData(savedTeacher, sqlExpressionAdd, sqlExpressionUpdate, checkID);
            }
            else { MessageBox.Show("Data is not filled correct."); }

            GetAll();
        }

        public override void RemoveData(object deleteElm, string sqlExpressionDelete = null)
        {
            Teacher deleteTeacher = deleteElm as Teacher;

            if (deleteTeacher != null)
            {
                sqlExpressionDelete = $"DELETE FROM Teachers WHERE t_ID={deleteTeacher.T_ID}";
                base.RemoveData(deleteTeacher, sqlExpressionDelete);
            }
            else { MessageBox.Show("Data is not filled."); }

            GetAll();
        }
    }
}
