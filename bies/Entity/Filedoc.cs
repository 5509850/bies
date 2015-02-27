using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace bies.Entity
{
    public class Filedoc
    {
        private int filedocID;
        private int docID;
        private int fileID;
        private int typedocID;
        private int tradeID;
        private bool isActive;

        public int FiledocID
        {
            get { return filedocID; }
            set { filedocID = value; }
        }

        public int DocID
        {
            get { return docID; }
            set { docID = value; }
        }

        public int FileID
        {
            get { return fileID; }
            set { fileID = value; }
        }

        public int TypedocID
        {
            get { return typedocID; }
            set { typedocID = value; }
        }

        public int TradeID
        {
            get { return tradeID; }
            set { tradeID = value; }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public bool Create()
        {
            DataBase db = new DataBase();
            string request = String.Format(
                    @"INSERT INTO [filedoc]
           ([docID] ,[fileID],[typedocID],[tradeID],[isActive])     
VALUES     ('{0}','{1}','{2}','{3}','{4}'); SELECT @@IDENTITY AS 'ID';",
                    DocID, fileID, TypedocID, TradeID, IsActive);
            return db.ExecuteSql(request);
        }

        public void GetListAllSignedByID(DevExpress.XtraGrid.GridControl gc)
        {
            DataBase db = new DataBase();
            //List<TenderDocs> result = new List<TenderDocs>();

            try
            {
                SqlConnection con = db.Connect();
                var request = String.Format(@"SELECT     approve.approveDate AS дата, approve.title AS название, approve.approve AS одобрен, users.fio AS ФИО, approve.remark AS резолюция
FROM         approve INNER JOIN users ON approve.approveUserID = users.userID
WHERE     (approve.filedocID = '{0}') AND (approve.isActive = '1')", FiledocID);
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 4a5");
                    return; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                gc.DataSource = _ds.Tables[0];
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "ошибка GetListAllSignedByID error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
        }

        public bool Update()
        {

            DataBase db = new DataBase();
            string request = String.Format(@"UPDATE    filedoc
SET              tradeID = '{0}'
WHERE     (docID = '{1}') AND (isActive = '1') AND (fileID = '{2}')", TradeID, DocID, FileID);
            /*
         * 
         */
            return db.ExecuteSql(request);
        }


    }
}
