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
    class StudentsVM : BaseViewModel
    {
        private RelayCommand _saveCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _addCommand;

        private Student selectedStudent;
        public Student SelectedStudent
        {
            get => selectedStudent;
            set
            {
                selectedStudent = value;
                OnPropertyChanged("SelectedStudent");
            }
        }
        private ObservableCollection<Student> students;
        public ObservableCollection<Student> StudentsData
        {
            get => students;
            set
            {
                students = value;
                OnPropertyChanged("StudentsData");
            }
        }

        public StudentsVM()
        {
            StudentsData = new ObservableCollection<Student>();
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
                    , (param) => StudentsData.Count > 0));
            }
        }

        public void GetAll()
        {
            StudentsData = new ObservableCollection<Student>();

            string sqlExpression = "SELECT * FROM Students";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Student addStudent;

                while (reader.Read())
                {
                    addStudent = new Student()
                    {
                        S_ID = int.Parse(reader["s_ID"].ToString()),
                        S_FirstName = reader["s_FirstName"].ToString(),
                        S_LastName = reader["s_LastName"].ToString(),
                        S_Phone = reader["s_Phone"].ToString(),
                        S_Group_ID = int.Parse(reader["s_Group_Id"].ToString())
                    };
                    StudentsData.Add(addStudent);
                }
            }
        }

        public void AddData()
        {
            Student newStudent = new Student();
            StudentsData.Insert(0, newStudent);
            SelectedStudent = newStudent;
        }

        public override void SaveData(object savedElm, string sqlExpressionAdd = null, string sqlExpressionUpdate = null, bool checkID = false)
        {
            Student savedStudent = savedElm as Student;
            checkID = savedStudent.S_ID <= 0;

            sqlExpressionAdd = $"INSERT INTO Students(s_FirstName, s_LastName, s_Phone, s_Group_Id) VALUES " +
                $"('{savedStudent.S_FirstName}', '{savedStudent.S_LastName}', '{savedStudent.S_Phone}', {savedStudent.S_Group_ID})";

            sqlExpressionUpdate = $"UPDATE Students SET s_FirstName='{savedStudent.S_FirstName}', s_LastName='{savedStudent.S_LastName}', " +
                $"s_Phone='{savedStudent.S_Phone}', s_Group_Id={savedStudent.S_Group_ID} WHERE s_ID={savedStudent.S_ID}";

            if (savedStudent != null && savedStudent.S_FirstName != null && savedStudent.S_LastName != null && savedStudent.S_Phone != null && savedStudent.S_Group_ID != 0)
            {
                base.SaveData(savedStudent, sqlExpressionAdd, sqlExpressionUpdate, checkID);
            }
            else { MessageBox.Show("Data is not filled correct."); }

            GetAll();
        }

        public override void RemoveData(object deleteElm, string sqlExpressionDelete = null)
        {
            Student deleteStudent = deleteElm as Student;

            if (deleteStudent != null)
            {
                sqlExpressionDelete = $"DELETE FROM Students WHERE s_ID={deleteStudent.S_ID}";
                base.RemoveData(deleteStudent, sqlExpressionDelete);
            }
            else { MessageBox.Show("Data is not filled."); }

            GetAll();
        }
    }
}
