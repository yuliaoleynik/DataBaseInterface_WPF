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
    class BooksVM : BaseViewModel
    {
        private RelayCommand _saveCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _addCommand;

        private Book selectedBook;
        public Book SelectedBook
        {
            get => selectedBook;
            set
            {
                selectedBook = value;
                OnPropertyChanged("SelectedBook");
            }
        }
        private ObservableCollection<Book> books;
        public ObservableCollection<Book> BooksData
        {
            get => books;
            set
            {
                books = value;
                OnPropertyChanged("BooksData");
            }
        }

        public BooksVM()
        {
            BooksData = new ObservableCollection<Book>();
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
                    , (param) => BooksData.Count > 0));
            }
        }

        public void GetAll()
        {
            BooksData = new ObservableCollection<Book>();

            string sqlExpression = "SELECT * FROM Books";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Book addBook;

                while (reader.Read())
                {
                    addBook = new Book()
                    {
                        B_Name = reader["b_Name"].ToString(),
                        B_ID = int.Parse(reader["b_ID"].ToString()),
                        B_Author_ID = int.Parse(reader["b_Author_ID"].ToString()),
                        B_Publisher_ID = int.Parse(reader["b_Publisher_ID"].ToString()),
                        B_Publication_Date = DateTime.Parse(reader["b_publication_date"].ToString())
                    };
                    BooksData.Add(addBook);
                }
            }
        }

        public void AddData()
        {
            Book newBook = new Book();
            BooksData.Insert(0, newBook);
            SelectedBook = newBook;
        }

        public override void SaveData(object savedElm, string sqlExpressionAdd = null, string sqlExpressionUpdate = null, bool checkID = false)
        {
            Book savedBook = savedElm as Book;
            sqlExpressionAdd = $"INSERT INTO Books(b_Name, b_Author_ID, b_publication_date, b_Publisher_ID) VALUES ('{savedBook.B_Name}', {savedBook.B_Author_ID}," +
                $"' {savedBook.B_Publication_Date}', {savedBook.B_Publisher_ID})";

            sqlExpressionUpdate = $"UPDATE Books SET b_Name='{savedBook.B_Name}', b_Author_ID={savedBook.B_Author_ID}, b_publication_date='{savedBook.B_Publication_Date}'," +
                $"b_Publisher_ID={savedBook.B_Publisher_ID} WHERE b_ID={savedBook.B_ID}";

            if (savedBook != null && savedBook.B_Name != null && savedBook.B_Publication_Date != null && savedBook.B_Author_ID != 0 && savedBook.B_Publisher_ID != 0)
            {
                checkID = savedBook.B_ID <= 0;
                base.SaveData(savedBook, sqlExpressionAdd, sqlExpressionUpdate, checkID);
            }
            else { MessageBox.Show("Data is not filled correct."); }

            GetAll();
        }

        public override void RemoveData(object deleteElm, string sqlExpressionDelete = null)
        {
            Book deleteBook = deleteElm as Book;

            if (deleteBook != null)
            {
                sqlExpressionDelete = $"DELETE FROM Books WHERE b_ID={deleteBook.B_ID}";
                base.RemoveData(deleteBook, sqlExpressionDelete);
            }
            else { MessageBox.Show("Data is not filled."); }

            GetAll();
        }
    }
}
