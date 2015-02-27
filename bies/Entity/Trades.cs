using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace bies.Entity
{
    public class Trades
    {
        private int tradeID;
        private string name;
        private int userIDrespons;
        private string userNameRespons;
        private string projectname;
        private string number;
        private int tradetypeID;
        private string tradetype;
        private DateTime datebegin;
        private bool isActive;

        #region filds
        public int TradeID
        {
            get { return tradeID; }
            set { tradeID = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int UserIDrespons
        {
            get { return userIDrespons; }
            set { userIDrespons = value; }
        }

        public string Projectname
        {
            get { return projectname; }
            set { projectname = value; }
        }

        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        public int TradetypeID
        {
            get { return tradetypeID; }
            set { tradetypeID = value; }
        }

        public DateTime Datebegin
        {
            get { return datebegin; }
            set { datebegin = value; }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public string Tradetype
        {
            get { return tradetype; }
            set { tradetype = value; }
        }

        public string UserNameRespons
        {
            get { return userNameRespons; }
            set { userNameRespons = value; }
        }

        public bool IsNew
        {
            get { return TradeID <= 0; }
        }

        #endregion fields

        public List<Trades> GetListAllTradesByUserID(DevExpress.XtraGrid.GridControl gc, DateTime from, int userID)
        {
            DataBase db = new DataBase();
            List<Trades> result = new List<Trades>();

            try
            {
                SqlConnection con = db.Connect();
                var request = String.Format(@"SELECT trades.tradeID, trades.name, trades.userIDrespons, trades.projectname, trades.number, trades.tradetypeID, trades.datebegin, trades.isActive, 
                      users.fio AS 'userNameRespons', tradetype.name AS 'tradetype'
FROM         trades INNER JOIN
                      users ON trades.userIDrespons = users.userID INNER JOIN
                      tradetype ON trades.tradetypeID = tradetype.tradetypeID
WHERE     (trades.datebegin >= CONVERT(DATETIME, '{0}', 102)) AND (trades.userIDrespons = '{1}') AND (trades.isActive = 1)", utils.DateToSqlString(from), userID);
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 21");
                    return null; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                DataTable dt = _ds.Tables[0];
                gc.DataSource = dt;
                foreach (DataRow _dr in dt.Rows)
                {
                    Trades trade = new Trades();
                    trade.TradeID = Convert.ToInt32(_dr["tradeID"]); 
                    trade.Name = (_dr["name"]).ToString();
                    trade.UserIDrespons = Convert.ToInt32(_dr["userIDrespons"]); 
                    trade.Projectname = (_dr["projectname"]).ToString();
                    trade.Number = (_dr["number"]).ToString();
                    trade.TradetypeID = Convert.ToInt32(_dr["tradetypeID"]);
                    trade.Datebegin = Convert.ToDateTime(_dr["datebegin"]);
                    trade.IsActive = Convert.ToBoolean(_dr["isActive"]);
                    trade.UserNameRespons = (_dr["userNameRespons"]).ToString();
                    trade.Tradetype = (_dr["tradetype"]).ToString();
                    result.Add(trade);
                }
            }
            catch (SqlException e)
            {
                result = null;
                MessageBox.Show(e.ToString(), "ошибка GetListAllTradesByUserID error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
            return result;
        }

        public void FillProjectNameList(DevExpress.XtraEditors.ComboBoxEdit cbox)
        {
            DataBase db = new DataBase();

            try
            {
                SqlConnection con = db.Connect();
                var request = @"SELECT     projectname FROM  projectname WHERE     (isActive = '1')"; 
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 22");
                    return; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                DataTable dt = _ds.Tables[0];
                cbox.Properties.Items.Clear();
                foreach (DataRow _dr in dt.Rows)
                {
                    cbox.Properties.Items.Add((_dr["projectname"]).ToString());
                }
            }
            catch (SqlException e)
            {

                MessageBox.Show(e.ToString(), "ошибка FillProjectNameList error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
            
        }

        public void FillProjectNameGrid(DevExpress.XtraGrid.GridControl gc)
        {
            DataBase db = new DataBase();

            try
            {
                SqlConnection con = db.Connect();
                var request = @"SELECT  * FROM  projectname WHERE     (isActive = '1')";
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 22");
                    return; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                gc.DataSource = _ds.Tables[0]; 
            }
            catch (SqlException e)
            {

                MessageBox.Show(e.ToString(), "ошибка FillProjectNameGrid error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
        }

        public void fillDocsForTradeTab(DevExpress.XtraGrid.GridControl gc)
        {
            DataBase db = new DataBase();
            //документы подписаны нет кем по торгам
            try
            {
                SqlConnection con = db.Connect();
                var request = String.Format(@"SELECT filedoc.filedocID, filedoc.typedocID, typedoc.name, files.title, files.datecreate, files.signed
FROM         filedoc INNER JOIN
                      typedoc ON filedoc.typedocID = typedoc.typedocID INNER JOIN
                      files ON filedoc.fileID = files.fileID 
WHERE     (filedoc.tradeID = '{0}') AND (filedoc.isActive = '1')", TradeID);
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 22");
                    return; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                gc.DataSource = _ds.Tables[0];
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ошибка fillDocsForTradeTab error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
        }

        public bool CreateOrUpdate()
        {
            DataBase db = new DataBase();
            string request = String.Empty;
            request = IsNew ? 
                            String.Format(@"INSERT INTO trades (name, userIDrespons, projectname, number, tradetypeID, datebegin, isActive)
VALUES     ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", utils.SetStringDB(Name), UserIDrespons, utils.SetStringDB(Projectname), utils.SetStringDB(Number), TradetypeID, utils.DateToSqlString(Datebegin), 1)
                            : 
                            String.Format(@"UPDATE trades SET name = '{0}', userIDrespons = '{1}', projectname = '{2}', number = '{3}', tradetypeID = '{4}', datebegin = '{5}', isActive = '{6}'
WHERE     (tradeID = '{7}')", utils.SetStringDB(Name), UserIDrespons, utils.SetStringDB(Projectname), utils.SetStringDB(Number), TradetypeID, utils.DateToSqlString(Datebegin), utils.SetBitDB(IsActive), TradeID);
            return db.ExecuteSql(request);
        }

        public List<Trades> GetListAllTrades(DevExpress.XtraGrid.GridControl gc, DateTime from)
        {
            DataBase db = new DataBase();
            List<Trades> result = new List<Trades>();

            try
            {
                SqlConnection con = db.Connect();
                var request = String.Format(@"SELECT trades.tradeID, trades.name, trades.userIDrespons, trades.projectname, trades.number, trades.tradetypeID, trades.datebegin, trades.isActive, 
                      users.fio AS 'userNameRespons', tradetype.name AS 'tradetype'
FROM         trades INNER JOIN
                      users ON trades.userIDrespons = users.userID INNER JOIN
                      tradetype ON trades.tradetypeID = tradetype.tradetypeID
WHERE     (trades.datebegin >= CONVERT(DATETIME, '{0}', 102)) AND (trades.isActive = 1) ORDER BY trades.tradeID DESC", utils.DateToSqlString(from));
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 22");
                    return null; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                DataTable dt = _ds.Tables[0];
                gc.DataSource = dt;
                foreach (DataRow _dr in dt.Rows)
                {
                    Trades trade = new Trades();
                    trade.TradeID = Convert.ToInt32(_dr["tradeID"]);
                    trade.Name = (_dr["name"]).ToString();
                    trade.UserIDrespons = Convert.ToInt32(_dr["userIDrespons"]);
                    trade.Projectname = (_dr["projectname"]).ToString();
                    trade.Number = (_dr["number"]).ToString();
                    trade.TradetypeID = Convert.ToInt32(_dr["tradetypeID"]);
                    trade.Datebegin = Convert.ToDateTime(_dr["datebegin"]);
                    trade.IsActive = Convert.ToBoolean(_dr["isActive"]);
                    trade.UserNameRespons = (_dr["userNameRespons"]).ToString();
                    trade.Tradetype = (_dr["tradetype"]).ToString();
                    result.Add(trade);
                }
            }
            catch (SqlException e)
            {
                result = null;
                MessageBox.Show(e.ToString(), "ошибка GetListAllTradesByUserID error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
            return result;
        }


        public bool UpdateProjectName(int projectnameID, string textEdit_edit)
        {
            DataBase db = new DataBase();
            string request = String.Format(@"UPDATE projectname SET projectname  = '{0}' WHERE     (projectnameID  = '{1}')", utils.SetStringDB(textEdit_edit), projectnameID);
            return db.ExecuteSql(request);
        }

        public bool CreateProjectName(string textEdit_edit)
        {
            DataBase db = new DataBase();
            string request = String.Format(
                @"INSERT INTO projectname (projectname, isActive) VALUES     ('{0}','{1}')",
                utils.SetStringDB(textEdit_edit), 1);
            return db.ExecuteSql(request);
        }

        public bool DeleteProjectName(int projectnameID)
        {
            DataBase db = new DataBase();
            string request = String.Format(@"UPDATE projectname SET isActive = '0' WHERE     (projectnameID  = '{0}')", projectnameID);
            return db.ExecuteSql(request);
        }
        
    }
}
