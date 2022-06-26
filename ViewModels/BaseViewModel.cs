using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Data.SqlClient;
using System.Configuration;

namespace BDInterfaceLAB2
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public BaseViewModel() { }
        public virtual void RemoveData(object deleteElm, string sqlExpressionDelete = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlExpressionDelete, connection);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Data successfully deleted.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while saving. " + ex.InnerException);
            }
        }
        public virtual void SaveData(object saveElm, string sqlExpressionAdd = null, string sqlExpressionUpdate = null, bool checkID = false)
        {
            try
            {
                if (checkID)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(sqlExpressionAdd, connection);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("New data successfully saved.");
                }
                else
                {                 
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(sqlExpressionUpdate, connection);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Data successfully updated.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while saving. " + ex.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
	}	
}
