using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;
using System.Windows;
using System.Collections.ObjectModel;


namespace BDInterfaceLAB2
{
    public class GroupsVM : BaseViewModel
    {
        private RelayCommand _saveCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _addCommand;

        private Group selectedGroup;
        public Group SelectedGroup
        {
            get => selectedGroup;
            set
            {
                selectedGroup = value;
                OnPropertyChanged("SelectedGroup");
            }
        }
        private ObservableCollection<Group> groups;
        public ObservableCollection<Group> GroupsData
        {
            get => groups;
            set
            {
                groups = value;
                OnPropertyChanged("GroupsData");
            }
        }

        public GroupsVM()
        {
            GroupsData = new ObservableCollection<Group>();
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
                    , (param) => GroupsData.Count > 0));
            }
        }

        public void GetAll()
        {
            GroupsData = new ObservableCollection<Group>();

            string sqlExpression = "SELECT * FROM Groups";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Group addGroup;

                while (reader.Read())
                {
                    addGroup = new Group()
                    {
                        G_ID = int.Parse(reader["g_ID"].ToString()),
                        G_Name = reader["g_Name"].ToString(),
                        G_Faculty_ID = int.Parse(reader["g_Faculty_ID"].ToString())
                    };
                    GroupsData.Add(addGroup);
                }
            }
        }

        public void AddData()
        {
            Group newGroup = new Group();
            GroupsData.Insert(0, newGroup);
            SelectedGroup = newGroup;
        }

        public override void SaveData(object savedElm, string sqlExpressionAdd = null, string sqlExpressionUpdate = null, bool checkID = false)
        {
            Group savedGroup = savedElm as Group;
            checkID = savedGroup.G_ID <= 0;

            sqlExpressionAdd = $"INSERT INTO Groups(g_Name, g_Faculty_ID) VALUES ('{savedGroup.G_Name}', {savedGroup.G_Faculty_ID})";
            sqlExpressionUpdate = $"UPDATE Groups SET g_Name='{savedGroup.G_Name}', g_Faculty_ID={savedGroup.G_Faculty_ID} WHERE g_ID={savedGroup.G_ID}";

            if (savedGroup.G_Name != null && savedGroup != null && savedGroup.G_Faculty_ID != 0)
            {
                base.SaveData(savedGroup, sqlExpressionAdd, sqlExpressionUpdate, checkID);
            }
            else { MessageBox.Show("Data is not filled."); }

            GetAll();
        }

        public override void RemoveData(object deleteElm, string sqlExpressionDelete = null)
        {
            Group deleteGroup = deleteElm as Group;

            if (deleteGroup != null)
            {
                sqlExpressionDelete = $"DELETE FROM Groups WHERE g_ID={deleteGroup.G_ID}";
                base.RemoveData(deleteGroup, sqlExpressionDelete);
            }
            else { MessageBox.Show("Data is not filled."); }

            GetAll();
        }
    }
}
