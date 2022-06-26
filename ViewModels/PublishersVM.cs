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
    class PublishersVM : BaseViewModel
    {
        private RelayCommand _saveCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _addCommand;

        private Publisher selectedPublisher;
        public Publisher SelectedPublisher
        {
            get => selectedPublisher;
            set
            {
                selectedPublisher = value;
                OnPropertyChanged("SelectedPublisher");
            }
        }
        private ObservableCollection<Publisher> publishers;
        public ObservableCollection<Publisher> PublishersData
        {
            get => publishers;
            set
            {
                publishers = value;
                OnPropertyChanged("PublishersData");
            }
        }

        public PublishersVM()
        {
            PublishersData = new ObservableCollection<Publisher>();
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
                    , (param) => PublishersData.Count > 0));
            }
        }

        public void GetAll()
        {
            PublishersData = new ObservableCollection<Publisher>();

            string sqlExpression = "SELECT * FROM Publishers";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Publisher addPublisher;

                while (reader.Read())
                {
                    addPublisher = new Publisher()
                    {
                        P_Name = reader["p_Name"].ToString(),
                        P_ID = int.Parse(reader["p_ID"].ToString())
                    };
                    PublishersData.Add(addPublisher);
                }
            }
        }

        public void AddData()
        {
            Publisher newPublisher = new Publisher();
            publishers.Insert(0, newPublisher);
            selectedPublisher = newPublisher;
        }

        public override void SaveData(object savedElm, string sqlExpressionAdd = null, string sqlExpressionUpdate = null, bool checkID = false)
        {
            Publisher savedPublisher = savedElm as Publisher;
            checkID = savedPublisher.P_ID <= 0;

            sqlExpressionAdd = $"INSERT INTO Publishers(p_Name) VALUES ('{savedPublisher.P_Name}')";
            sqlExpressionUpdate = $"UPDATE Publishers SET p_Name='{savedPublisher.P_Name}' WHERE p_ID={savedPublisher.P_ID}";

            if (savedPublisher.P_Name != null && savedPublisher != null)
            {
                base.SaveData(savedPublisher, sqlExpressionAdd, sqlExpressionUpdate, checkID);
            }
            else { MessageBox.Show("Data is not filled."); }

            GetAll();
        }

        public override void RemoveData(object deleteElm, string sqlExpressionDelete = null)
        {
            Publisher deletePublisher = deleteElm as Publisher;

            if (deletePublisher != null)
            {
                sqlExpressionDelete = $"DELETE FROM Publishers WHERE p_ID={deletePublisher.P_ID}";
                base.RemoveData(deletePublisher, sqlExpressionDelete);
            }
            else { MessageBox.Show("Data is not filled."); }

            GetAll();
        }
    }
}
