using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

//BiesConnectionString

namespace bies
{
    public class DataBase
    {
        private readonly string DB = Properties.Settings.Default.BiesConnectionString;
        private SqlConnection con;

        public void GetDataSource(DevExpress.XtraGrid.GridControl gc)
        {

            try
            {
                Connect();
                var request = String.Format(@"SELECT  TOP (200) userID, login, password, fio, groupID, isActive 
FROM  users");
                var myData = new SqlDataAdapter(request, con);
                var _ds = new DataSet();
                myData.Fill(_ds);
                gc.DataSource = _ds.Tables[0];
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ошибка GetDataSource error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                Disconnect();
            }
        }

        public SqlConnection Connect() //OK
        {
            try
            {
                if (con == null)
                {
                    con = new SqlConnection { ConnectionString = DB };
                }
                if (con.State == ConnectionState.Closed) con.Open();
            }
            catch
            {
                return null;
            }

            return con;

        }

        public void Disconnect() //OK
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }

        public bool ExecuteSql(string s) //OK
        {
            bool result;
            try
            {
                var cmd = new SqlCommand(s, Connect());
                result = cmd.ExecuteNonQuery() > 0;
            }

            catch (SqlException)
            {
                return false;
            }
            finally
            {
                Disconnect();
            }
            return result;
        }

        





    }
}
