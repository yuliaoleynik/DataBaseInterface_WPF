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
    class LibrariansVM : BaseViewModel
    {
        private RelayCommand _saveCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _addCommand;

        private Librarian selectedLibrarian;
        public Librarian SelectedLibrarian
        {
            get => selectedLibrarian;
            set
            {
                selectedLibrarian = value;
                OnPropertyChanged("SelectedLibrarian");
            }
        }
        private ObservableCollection<Librarian> librarians;
        public ObservableCollection<Librarian> LibrariansData
        {
            get => librarians;
            set
            {
                librarians = value;
                OnPropertyChanged("LibrariansData");
            }
        }

        public LibrariansVM()
        {
            LibrariansData = new ObservableCollection<Librarian>();
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
                    , (param) => LibrariansData.Count > 0));
            }
        }

        public void GetAll()
        {
            LibrariansData = new ObservableCollection<Librarian>();

            string sqlExpression = "SELECT * FROM Librarians";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Librarian addLibrarian;

                while (reader.Read())
                {
                    addLibrarian = new Librarian()
                    {
                        L_ID = int.Parse(reader["l_ID"].ToString()),
                        L_FirstName = reader["l_FirstName"].ToString(),
                        L_LastName = reader["l_LastName"].ToString(),
                        L_Phone = reader["l_Phone"].ToString()
                    };
                    LibrariansData.Add(addLibrarian);
                }
            }
        }

        public void AddData()
        {
            Librarian newLibrarian = new Librarian();
            LibrariansData.Insert(0, newLibrarian);
            SelectedLibrarian = newLibrarian;
        }

        public override void SaveData(object savedElm, string sqlExpressionAdd = null, string sqlExpressionUpdate = null, bool checkID = false)
        {
            Librarian savedLibrarian = savedElm as Librarian;
            checkID = savedLibrarian.L_ID <= 0;

            sqlExpressionAdd = $"INSERT INTO Librarians(l_FirstName, l_LastName, l_Phone) VALUES ('{savedLibrarian.L_FirstName}', '{savedLibrarian.L_LastName}', '{savedLibrarian.L_Phone}')";
            sqlExpressionUpdate = $"UPDATE Librarians SET l_FirstName='{savedLibrarian.L_FirstName}', l_LastName='{savedLibrarian.L_LastName}', l_Phone='{savedLibrarian.L_Phone}' WHERE l_ID={savedLibrarian.L_ID}";

            if (savedLibrarian != null && savedLibrarian.L_FirstName != null && savedLibrarian.L_LastName != null && savedLibrarian.L_Phone != null)
            {
                base.SaveData(savedLibrarian, sqlExpressionAdd, sqlExpressionUpdate, checkID);
            }
            else { MessageBox.Show("Data is not filled."); }
            
            GetAll();
        }

        public override void RemoveData(object deleteElm, string sqlExpressionDelete = null)
        {
            Librarian deleteLibrarian = deleteElm as Librarian;

            if (deleteLibrarian != null)
            {
                sqlExpressionDelete = $"DELETE FROM Librarians WHERE l_ID={deleteLibrarian.L_ID}";
                base.RemoveData(deleteLibrarian, sqlExpressionDelete);
            }
            else { MessageBox.Show("Data is not filled correct."); }

            GetAll();
        }
    }
}
