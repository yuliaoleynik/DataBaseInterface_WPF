using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Configuration;
using System.Data;
using System.Windows;

namespace BDInterfaceLAB2
{
    public class AuthorsVM : BaseViewModel
    {
        private RelayCommand _saveCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _addCommand;

        private Author selectedAuthor;
        public Author SelectedAuthor
        {
            get => selectedAuthor;
            set
            {
                selectedAuthor = value;
                OnPropertyChanged("SelectedAuthor");
            }
        }
        private ObservableCollection<Author> authors;
        public ObservableCollection<Author> AuthorsData
        {
            get => authors;
            set
            {
                authors = value;
                OnPropertyChanged("AuthorsData");
            }
        }

        public AuthorsVM()
        {
            AuthorsData = new ObservableCollection<Author>();
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
                    ,null));
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
                    ,(param) => AuthorsData.Count > 0));
            }
        }

        public void GetAll()
        {
            AuthorsData = new ObservableCollection<Author>();

            string sqlExpression = "SELECT * FROM Authors";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Author addAuthor;

                while (reader.Read())
                {
                    addAuthor = new Author()
                    {
                        A_FirstName = reader["a_FirstName"].ToString(),
                        A_LastName = reader["a_LastName"].ToString(),
                        A_ID = int.Parse(reader["a_ID"].ToString())
                    };
                    AuthorsData.Add(addAuthor);
                }
            }
        }

        public void AddData()
        {
            Author newAuthor = new Author();
            authors.Insert(0, newAuthor);
            selectedAuthor = newAuthor;
        }

        public override void SaveData(object savedElm, string sqlExpressionAdd = null, string sqlExpressionUpdate = null, bool checkID = false)
        {
            Author savedAuthor = savedElm as Author;
            sqlExpressionAdd = $"INSERT INTO Authors(a_FirstName, a_LastName) VALUES ('{savedAuthor.A_FirstName}', '{savedAuthor.A_LastName}')";
            sqlExpressionUpdate = $"UPDATE Authors SET a_FirstName='{savedAuthor.A_FirstName}', a_LastName='{savedAuthor.A_LastName}' WHERE a_ID={savedAuthor.A_ID}";

            if (savedAuthor != null && savedAuthor.A_FirstName != null && savedAuthor.A_LastName != null)
            {
                checkID = savedAuthor.A_ID <= 0;
                base.SaveData(savedAuthor, sqlExpressionAdd, sqlExpressionUpdate, checkID);
            }
            else { MessageBox.Show("Data is not filled correct."); }

            GetAll();
        }

        public override void RemoveData(object deleteElm, string sqlExpressionDelete = null)
        {
            Author deleteAuthor = deleteElm as Author;

            if (deleteAuthor != null)
            {
                sqlExpressionDelete = $"DELETE FROM Authors WHERE a_ID={deleteAuthor.A_ID}";
                base.RemoveData(deleteAuthor, sqlExpressionDelete);
            }
            else { MessageBox.Show("Data is not filled."); }

            GetAll();
        }
    }
}

