using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace bies.Entity
{
    public class Users
    {
        private int userID;
        private string login;
        private string password;
        private string fio;
        private int groupID;
        private string group;
        private bool isActive;
        private bool needChangPassword;

        #region fields---------------------
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Fio
        {
            get { return fio; }
            set { fio = value; }
        }

        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }

        public string Group
        {
            get { return group; }
            set { group = value; }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public bool NeedChangPassword
        {
            get { return needChangPassword; }
            set { needChangPassword = value; }
        }

        #endregion fields---------------------

        public bool FillUserFieldsByLogin(string _login)
        {
            DataBase db = new DataBase();
            bool result = false;
        

            try
            {
                SqlConnection con = db.Connect();
                var request = String.Format(@"SELECT userID, login, password, fio, groupID, ISNULL(isActive, 1) AS isActive, ISNULL(needChangPassword, 0) AS needChangPassword
FROM  users WHERE     (login = N'{0}')", utils.SetStringDB(_login));
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    return  false; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                DataTable dt = _ds.Tables[0];
                if (_ds.Tables[0].Rows.Count == 1)
                {
                    foreach (DataRow _dr in dt.Rows)
                    {
                        Login = _login;
                        UserID = Convert.ToInt32(_dr["userID"]);
                        Password = (_dr["password"]).ToString();
                        Fio = (_dr["fio"]).ToString();
                        GroupID = Convert.ToInt32(_dr["groupID"]);
                        IsActive = Convert.ToBoolean(_dr["isActive"]);
                        NeedChangPassword = Convert.ToBoolean(_dr["needChangPassword"]);
                    }
                }
                result = true;
                
            }
            catch (SqlException e)
            {
                result = false;
                MessageBox.Show(e.ToString(), "ошибка GetDataSource error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
            return result;
        }

        public bool FillUserFieldsByUserID(int ID)
        {
            DataBase db = new DataBase();
            bool result = false;


            try
            {
                SqlConnection con = db.Connect();
                var request = String.Format(@"SELECT userID, login, password, fio, groupID, ISNULL(isActive, 1) AS isActive, ISNULL(needChangPassword, 0) AS needChangPassword
FROM  users WHERE     (userID = '{0}')", ID);
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    return false; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                DataTable dt = _ds.Tables[0];
                if (_ds.Tables[0].Rows.Count == 1)
                {
                    foreach (DataRow _dr in dt.Rows)
                    {
                        Login = (_dr["login"]).ToString();
                        UserID = ID;
                        Password = (_dr["password"]).ToString();
                        Fio = (_dr["fio"]).ToString();
                        GroupID = Convert.ToInt32(_dr["groupID"]);
                        IsActive = Convert.ToBoolean(_dr["isActive"]);
                        NeedChangPassword = Convert.ToBoolean(_dr["needChangPassword"]);
                    }
                }
                result = true;

            }
            catch (SqlException e)
            {
                result = false;
                MessageBox.Show(e.ToString(), "ошибка GetDataSource error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
            return result;
        }

        public bool UpdatePassword(int _userid, string _pass)
        {
            DataBase db = new DataBase();
            var request = String.Format(@"UPDATE    users
SET              password = '{0}', needChangPassword = '0'
WHERE     (userID = '{1}')", _pass, _userid);
            return db.ExecuteSql(request);
        }


        public bool UpdateUser()
        {
            DataBase db = new DataBase();
            var request = String.Format(@"UPDATE    users
SET     login = '{0}', fio = '{1}', groupID = '{2}', isActive = '{3}', needChangPassword = '{4}'
WHERE     (userID = '{5}')", utils.SetStringDB(Login), utils.SetStringDB(Fio), GroupID, utils.SetBitDB(IsActive), utils.SetBitDB(NeedChangPassword), UserID);
            return db.ExecuteSql(request);
        }

        public bool ClearPass()
        {
            DataBase db = new DataBase();
            var request = String.Format(@"UPDATE    users SET   password = '{0}', needChangPassword = '{1}' WHERE     (userID = '{2}')", 
                utils.GetHashString(String.Empty), utils.SetBitDB(NeedChangPassword), UserID);
            return db.ExecuteSql(request);
        }

        public List<Users>  GetListAllUsers()
        {
            DataBase db = new DataBase();
            List<Users> result = new List<Users>();

            try
            {
                SqlConnection con = db.Connect();
                var request = @"SELECT     users.userID, users.login, users.fio, users.groupID, groups.name AS 'group', ISNULL(users.isActive, 1) AS isActive, ISNULL(users.needChangPassword, 0) 
                      AS needChangPassword FROM users INNER JOIN groups ON users.groupID = groups.groupID";
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 21");
                    return null; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                DataTable dt = _ds.Tables[0];
                foreach (DataRow _dr in dt.Rows)
                    {
                        Users _user = new Users();
                        _user.UserID = Convert.ToInt32(_dr["userID"]);
                        _user.Login = (_dr["login"]).ToString();
                        _user.Password = "********";
                        _user.Fio = (_dr["fio"]).ToString();
                        _user.GroupID = Convert.ToInt32(_dr["groupID"]);
                        _user.Group = (_dr["group"]).ToString();
                        _user.IsActive = Convert.ToBoolean(_dr["isActive"]);
                        _user.NeedChangPassword = Convert.ToBoolean(_dr["needChangPassword"]);
                        result.Add(_user);
                    }
            }
            catch (SqlException e)
            {
                result = null;
                MessageBox.Show(e.ToString(), "ошибка GetListAllUsers error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
            return result;
        }

        public bool CreateNewUser()
        {
            DataBase db = new DataBase();
            var request = String.Format(@"INSERT  INTO  users (login, password, fio, groupID, isActive, needChangPassword)
VALUES     ('{0}','{1}','{2}','{3}','{4}','{5}')", utils.SetStringDB(Login), utils.SetStringDB(Password), utils.SetStringDB(Fio), GroupID, utils.SetBitDB(IsActive), utils.SetBitDB(NeedChangPassword));
            return db.ExecuteSql(request);
        }
            
 }
    
}
