using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace bies.Entity
{
    public class OpenProtolol
    {
        private int openProtocolID;
        private int tradeID;
        private DateTime dateCreate;
        private bool sendtobank;
        private int typedocID;
        private bool isActive;
        private bool isNew;


        public int OpenProtocolID
        {
            get { return openProtocolID; }
            set { openProtocolID = value; }
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

        
        public bool CreateopenProtololGetID()
        {
            DataBase db = new DataBase();
            string request;

            try
            {
                SqlConnection con = db.Connect();
                request =
                String.Format(
                    @"INSERT INTO [openProtocol] 
            ([tradeID]
           ,[dateCreate]
           ,[sendtobank]           
           ,[typedocID]
           ,[isActive])     
VALUES     ('{0}', CONVERT(DATETIME, GETDATE(), 102), '{1}',  '{2}', '{3}');
SELECT @@IDENTITY AS 'ID';", TradeID, utils.SetBitDB(Sendtobank), TypedocID, utils.SetBitDB(IsActive));
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
                    OpenProtocolID = Convert.ToInt32(_dr["ID"]);

                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ошибка CreateopenProtololGetID error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    @"UPDATE    openProtocol
SET              tradeID = '{1}', dateCreate = '{3}', sendtobank = '{2}', isActive = '{4}'
WHERE     (openProtocolID = '{0}')", OpenProtocolID, TradeID, utils.SetBitDB(Sendtobank), utils.DateToSqlString(DateCreate), utils.SetBitDB(IsActive));
            return db.ExecuteSql(request);
        }


        public void GetListAllOpenProtokolByTradeID(DevExpress.XtraGrid.GridControl gc)
        {
            DataBase db = new DataBase();
            //List<TenderDocs> result = new List<TenderDocs>();

            try
            {
                SqlConnection con = db.Connect();
                var request = String.Format(@"SELECT     
openProtocol.openProtocolID, filedoc.filedocID, files.fileID, files.filename, files.isFileInBaseOrInShare, files.title, openProtocol.dateCreate, openProtocol.sendtobank, 
                      files.signed
                      FROM openProtocol
                      INNER JOIN filedoc 
							ON openProtocol.openProtocolID = filedoc.docID 
                      INNER JOIN files 
							ON filedoc.fileID = files.fileID                      
WHERE     (openProtocol.tradeID = '{0}') AND (openProtocol.typedocID = '{1}') AND (openProtocol.isActive = '1') AND (filedoc.isActive = '1') AND (filedoc.typedocID = '{1}') AND (filedoc.tradeID = '{0}')",
TradeID, TypedocID);
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 435");
                    return; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                gc.DataSource = _ds.Tables[0];
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "ошибка GetListAllOpenProtokolByTradeID error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
        }

        public bool Delete()
        {
            DataBase db = new DataBase();
            string request = String.Format(@"UPDATE   openProtocol  SET isActive = '0' WHERE  (openProtocolID = '{0}');
UPDATE    filedoc SET isActive = '0' WHERE     (docID = '{0}') AND (typedocID = '{1}')", OpenProtocolID, TypedocID);
            return db.ExecuteSql(request);
        }


    }
}
