using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace bies.Entity
{
    public class RatingReport
    {
        private int ratingReportID;
        private int tradeID;
        private bool approveRatingCommission;
        private DateTime approveRatingDate;
        private bool approveTenderCommission;
        private DateTime approveTenderDate;
        private bool sendtobank;
        private bool approveBank;
        private DateTime approveBankDate;
        private DateTime dateCreate;
        private int typedocID;
        private bool isActive;
        private bool isNew;

        public int RatingReportID
        {
            get { return ratingReportID; }
            set { ratingReportID = value; }
        }

        public int TradeID
        {
            get { return tradeID; }
            set { tradeID = value; }
        }

        public bool ApproveRatingCommission
        {
            get { return approveRatingCommission; }
            set { approveRatingCommission = value; }
        }

        public DateTime ApproveRatingDate
        {
            get { return approveRatingDate; }
            set { approveRatingDate = value; }
        }

        public bool ApproveTenderCommission
        {
            get { return approveTenderCommission; }
            set { approveTenderCommission = value; }
        }

        public DateTime ApproveTenderDate
        {
            get { return approveTenderDate; }
            set { approveTenderDate = value; }
        }

        public bool Sendtobank
        {
            get { return sendtobank; }
            set { sendtobank = value; }
        }

        public bool ApproveBank
        {
            get { return approveBank; }
            set { approveBank = value; }
        }

        public DateTime ApproveBankDate
        {
            get { return approveBankDate; }
            set { approveBankDate = value; }
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

        public bool CreateRatingReportGetID()
        {
            DataBase db = new DataBase();
            string request;

            try
            {
                SqlConnection con = db.Connect();
                request =
                String.Format(
                    @"INSERT INTO   ratingReports (tradeID, dateCreate,  approveRatingDate, approveTenderDate, approveBankDate, approveRatingCommission, approveTenderCommission, sendtobank, approveBank, typedocID, isActive)
VALUES     ('{0}', {1},{1},{1},{1}, '{2}', '{3}', '{4}' , '{5}', '{6}', '{7}');
SELECT @@IDENTITY AS 'ID';", TradeID, 
                           "CONVERT(DATETIME, GETDATE(), 102)",
                           utils.SetBitDB(ApproveRatingCommission), 
                           utils.SetBitDB(ApproveTenderCommission),
                           utils.SetBitDB(Sendtobank),
                           utils.SetBitDB(ApproveBank), 
                           TypedocID, 
                           utils.SetBitDB(IsActive));
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 595");
                    return false;
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                DataTable dt = _ds.Tables[0];
                foreach (DataRow _dr in dt.Rows)
                {
                    RatingReportID = Convert.ToInt32(_dr["ID"]);

                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ошибка CreateRatingReportGetID error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            finally
            {
                db.Disconnect();
            }
            return true;
        }

        public bool Update()
        {
            DataBase db = new DataBase();
            string request = String.Format(
                    @"UPDATE    ratingReports
SET              tradeID = '{1}', dateCreate = '{3}', sendtobank = '{2}', isActive = '{4}', approveRatingCommission = '{5}', approveRatingDate = '{6}',  approveTenderDate = '{7}',  approveBankDate = '{8}',  approveTenderCommission = '{9}', approveBank = '{10}'
WHERE     (ratingReportID = '{0}')", RatingReportID, TradeID, utils.SetBitDB(Sendtobank), utils.DateToSqlString(DateCreate), utils.SetBitDB(IsActive), utils.SetBitDB(ApproveRatingCommission), utils.DateToSqlString(ApproveRatingDate), utils.DateToSqlString(ApproveTenderDate), utils.DateToSqlString(ApproveBankDate), utils.SetBitDB(ApproveTenderCommission), utils.SetBitDB(ApproveBank));
            return db.ExecuteSql(request);
        }

        public void GetListAllRatingReportByTradeID(DevExpress.XtraGrid.GridControl gc)
        {
            DataBase db = new DataBase();
            //List<TenderDocs> result = new List<TenderDocs>();

            try
            {
                SqlConnection con = db.Connect();
                var request = String.Format(@"SELECT  ratingReports.ratingReportID, filedoc.filedocID, files.fileID, files.filename, files.isFileInBaseOrInShare, files.title, 
ratingReports.dateCreate, ratingReports.sendtobank, ratingReports.approveRatingCommission, ratingReports.approveRatingDate,
ratingReports.approveTenderCommission, ratingReports.approveTenderDate, ratingReports.approveBank, ratingReports.approveBankDate, files.signed
                      FROM ratingReports
                      INNER JOIN filedoc ON ratingReports.ratingReportID = filedoc.docID 
                      INNER JOIN files ON filedoc.fileID = files.fileID                      
WHERE     (ratingReports.tradeID = '{0}') AND (ratingReports.typedocID = '{1}') AND (ratingReports.isActive = '1') AND (filedoc.isActive = '1') AND (filedoc.typedocID = '{1}') AND (filedoc.tradeID = '{0}')",
TradeID, TypedocID);
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 1435");
                    return; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                gc.DataSource = _ds.Tables[0];
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "ошибка GetListAllRatingReportByTradeID error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
        }

        public bool Delete()
        {
            DataBase db = new DataBase();
            string request = String.Format(@"UPDATE   ratingReports  SET isActive = '0' WHERE  (ratingReportID = '{0}');
UPDATE    filedoc SET isActive = '0' WHERE     (docID = '{0}') AND (typedocID = '{1}')", RatingReportID, TypedocID);
            return db.ExecuteSql(request);
        }
    }
}
