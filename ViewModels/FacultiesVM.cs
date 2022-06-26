using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;


namespace BDInterfaceLAB2
{
    public class FacultiesVM : BaseViewModel
    {
        private RelayCommand _saveCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _addCommand;

        private Faculty selectedFaculty;
        public Faculty SelectedFaculty
        {
            get => selectedFaculty;
            set
            {
                selectedFaculty = value;
                OnPropertyChanged("SelectedFaculty");
            }
        }
        private ObservableCollection<Faculty> faculties;
        public ObservableCollection<Faculty> FacultiesData
        {
            get => faculties;
            set
            {
                faculties = value;
                OnPropertyChanged("FacultiesData");
            }
        }

        public FacultiesVM()
        {
            FacultiesData = new ObservableCollection<Faculty>();
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
                    , (param) => FacultiesData.Count > 0));
            }
        }

        public void GetAll()
        {
            FacultiesData = new ObservableCollection<Faculty>();

            string sqlExpression = "SELECT * FROM Faculties";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Faculty addFaculty;

                while (reader.Read())
                {
                    addFaculty = new Faculty()
                    {
                        F_Name = reader["f_Name"].ToString(),
                        F_ID = int.Parse(reader["f_ID"].ToString())
                    };
                    FacultiesData.Add(addFaculty);
                }
            }
        }

        public void AddData()
        {
            Faculty newFaculty = new Faculty();
            FacultiesData.Insert(0, newFaculty);
            SelectedFaculty = newFaculty;
        }

        public override void SaveData(object savedElm, string sqlExpressionAdd = null, string sqlExpressionUpdate = null, bool checkID = false)
        {
            Faculty savedFaculty = savedElm as Faculty;
            checkID = savedFaculty.F_ID <= 0;

            sqlExpressionAdd = $"INSERT INTO Faculties(f_Name) VALUES ('{savedFaculty.F_Name}')";
            sqlExpressionUpdate = $"UPDATE Faculties SET f_Name='{savedFaculty.F_Name}' WHERE f_ID={savedFaculty.F_ID}";

            if (savedFaculty.F_Name != null && savedFaculty != null)
            {
                base.SaveData(savedFaculty, sqlExpressionAdd, sqlExpressionUpdate, checkID);
            }
            else { MessageBox.Show("Data is not filled."); }
            
            GetAll();
        }

        public override void RemoveData(object deleteElm, string sqlExpressionDelete = null)
        {
            Faculty deleteFaculty = deleteElm as Faculty;

            if (deleteFaculty != null)
            {
                sqlExpressionDelete = $"DELETE FROM Faculties WHERE f_ID={deleteFaculty.F_ID}";
                base.RemoveData(deleteFaculty, sqlExpressionDelete);
            }
            else { MessageBox.Show("Data is not filled."); }

            GetAll();
        }
    }
}
