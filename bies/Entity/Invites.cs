using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace bies.Entity
{
    public class Invites
    {
        private int inviteID;
        private int tradeID;
        private DateTime dateCreate;
        private bool sendtobank;
        private int inviteStatusID;
        private DateTime inviteStatusDate;
        private int typedocID;
        private bool isActive;
        private bool isNew;

        public int InviteID
        {
            get { return inviteID; }
            set { inviteID = value; }
        }

        public int TradeID
        {
            get { return tradeID; }
            set { tradeID = value; }
        }

        public DateTime DateCreate
        {
            get { return dateCreate; }
            set { dateCreate = value; }
        }

        public bool Sendtobank
        {
            get { return sendtobank; }
            set { sendtobank = value; }
        }

        public int InviteStatusID
        {
            get { return inviteStatusID; }
            set { inviteStatusID = value; }
        }

        public DateTime InviteStatusDate
        {
            get { return inviteStatusDate; }
            set { inviteStatusDate = value; }
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

        public void CreateInviteGetID()
        {
            DataBase db = new DataBase();
            string request;

            try
            {
                SqlConnection con = db.Connect();
                request =
                String.Format(
                    @"INSERT INTO [invites] 
            ([tradeID]
           ,[dateCreate]
           ,[sendtobank]
           ,[inviteStatusID]
           ,[inviteStatusDate]
           ,[typedocID]
           ,[isActive])     
VALUES     ('{0}', CONVERT(DATETIME, GETDATE(), 102), '{1}',  '{2}', CONVERT(DATETIME, GETDATE(), 102), '{3}', '{4}');
SELECT @@IDENTITY AS 'ID';", TradeID, utils.SetBitDB(Sendtobank), InviteStatusID, TypedocID, utils.SetBitDB(IsActive));
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 595");
                    return;
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                DataTable dt = _ds.Tables[0];
                foreach (DataRow _dr in dt.Rows)
                {
                    InviteID = Convert.ToInt32(_dr["ID"]);

                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ошибка CreateInviteGetID error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    @"UPDATE    invites
SET              tradeID = '{1}', dateCreate = '{3}', sendtobank = '{2}', inviteStatusID = '{4}', inviteStatusDate = '{5}', isActive = '{6}'
WHERE     (inviteID = '{0}')", InviteID, TradeID, utils.SetBitDB(Sendtobank), utils.DateToSqlString(DateCreate), InviteStatusID, utils.DateToSqlString(InviteStatusDate), utils.SetBitDB(IsActive));
            return db.ExecuteSql(request);
        }

        public void GetListAllInviteByTradeID(DevExpress.XtraGrid.GridControl gc)
        {
            DataBase db = new DataBase();
            //List<TenderDocs> result = new List<TenderDocs>();

            try
            {
                SqlConnection con = db.Connect();
                var request = String.Format(@"SELECT     
invites.inviteID, filedoc.filedocID, files.fileID, files.filename, files.isFileInBaseOrInShare, files.title, invites.dateCreate, invites.sendtobank, 
                      files.signed, inviteStatus.name, invites.inviteStatusDate, invites.inviteStatusID
                      FROM invites 
                      INNER JOIN filedoc 
							ON invites.inviteID = filedoc.docID 
                      INNER JOIN files 
							ON filedoc.fileID = files.fileID
                      INNER JOIN inviteStatus	
					         ON inviteStatus.inviteStatusID = invites.inviteStatusID
WHERE     (invites.tradeID = '{0}') AND (invites.typedocID = '{1}') AND (invites.isActive = '1') AND (filedoc.isActive = '1') AND (filedoc.typedocID = '{1}') AND (filedoc.tradeID = '{0}')",
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

        public bool Delete()
        {
            DataBase db = new DataBase();
            string request = String.Format(@"UPDATE   invites  SET isActive = '0' WHERE  (inviteID = '{0}');
UPDATE    filedoc SET isActive = '0' WHERE     (docID = '{0}') AND (typedocID = '{1}')", InviteID, TypedocID);
            return db.ExecuteSql(request);
        }

       
    }

}
