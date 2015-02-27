using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace bies.Entity
{
    public class TenderDocs
    {

        private int tenderDocID;
        private int tradeID;
        private bool sendtobank;
        private DateTime dateCreate;
        private int typedocID;
        private bool isActive;
        private bool isNew;

        public int TenderDocID
        {
            get { return tenderDocID; }
            set { tenderDocID = value; }
        }

        public int TradeID
        {
            get { return tradeID; }
            set { tradeID = value; }
        }

        public bool Sendtobank
        {
            get { return sendtobank; }
            set { sendtobank = value; }
        }

        public DateTime DateCreate
        {
            get { return dateCreate; }
            set { dateCreate = value; }
        }

        public int TypedocID
        {
            get { return typedocID; }
            set { typedocID = value; }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public bool IsNew
        {
            get { return isNew; }
            set { isNew = value; }
        }

        public void GetListAllTenderDocsByTradeID(DevExpress.XtraGrid.GridControl gc)
        {
            DataBase db = new DataBase();
            //List<TenderDocs> result = new List<TenderDocs>();

            try
            {
                SqlConnection con = db.Connect();
                var request = String.Format(@"SELECT     tenderDocs.tenderDocID, filedoc.filedocID, files.fileID, files.filename, files.isFileInBaseOrInShare, files.title, tenderDocs.dateCreate, tenderDocs.sendtobank, 
                      files.signed FROM tenderDocs INNER JOIN
                      filedoc ON tenderDocs.tenderDocID = filedoc.docID INNER JOIN files ON filedoc.fileID = files.fileID
WHERE     (tenderDocs.tradeID = '{0}') AND (tenderDocs.typedocID = '{1}') AND (tenderDocs.isActive = '1') AND (filedoc.isActive = '1') AND (filedoc.typedocID = '{1}') AND (filedoc.tradeID = '{0}')", 
TradeID, TypedocID);
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 45");
                    return; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                gc.DataSource = _ds.Tables[0];
                //foreach (DataRow _dr in dt.Rows)
                //{
                //    TenderDocs tenderDocs = new TenderDocs();
                //    tenderDocs.TradeID = Convert.ToInt32(_dr["tradeID"]);
                //    //tenderDocs.Name = (_dr["name"]).ToString();
                //    //tenderDocs.UserIDrespons = Convert.ToInt32(_dr["userIDrespons"]);
                //    //tenderDocs.Projectname = (_dr["projectname"]).ToString();
                //    //tenderDocs.Number = (_dr["number"]).ToString();
                //    //trade.TradetypeID = Convert.ToInt32(_dr["tradetypeID"]);
                //    //trade.Datebegin = Convert.ToDateTime(_dr["datebegin"]);
                //    //trade.IsActive = Convert.ToBoolean(_dr["isActive"]);
                //    //trade.UserNameRespons = (_dr["userNameRespons"]).ToString();
                //    //trade.Tradetype = (_dr["tradetype"]).ToString();
                //    result.Add(tenderDocs);
                //}
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "ошибка GetListAllTenderDocsByTradeID error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
        }

        public void CreateTenderDocGetID()
        {
            DataBase db = new DataBase();
            string request;
            
                 try
            {
                SqlConnection con = db.Connect();
                request =
                String.Format(
                    @"INSERT INTO tenderDocs
                      (tradeID, sendtobank, dateCreate, typedocID, isActive)
VALUES     ('{0}', '{1}', CONVERT(DATETIME, GETDATE(), 102), '{2}', '{3}');
SELECT @@IDENTITY AS 'ID';", TradeID, utils.SetBitDB(Sendtobank), TypedocID, utils.SetBitDB(IsActive));
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 55");
                    return;
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                DataTable dt = _ds.Tables[0];
                foreach (DataRow _dr in dt.Rows)
                {
                    TenderDocID = Convert.ToInt32(_dr["ID"]);
                    
                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ошибка CreateTenderDocGetID error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
        }

        public bool Update()
        {
            DataBase db = new DataBase();
            string request = String.Format(
                    @"UPDATE    tenderDocs
SET              tradeID = '{1}', sendtobank = '{2}', dateCreate = '{3}', typedocID = '{4}', isActive = '{5}'
WHERE     (tenderDocID = '{0}')", TenderDocID, TradeID, utils.SetBitDB(Sendtobank), utils.DateToSqlString(DateCreate), TypedocID, utils.SetBitDB(IsActive));
            return db.ExecuteSql(request);
        }

        public bool Delete()
        {
            DataBase db = new DataBase();
            string request = String.Format(@"UPDATE    tenderDocs SET isActive = '0' WHERE  (tenderDocID = '{0}');
UPDATE    filedoc SET isActive = '0' WHERE     (docID = '{0}') AND (typedocID = '{1}')", TenderDocID, TypedocID);
            return db.ExecuteSql(request);
        }
    }
}
