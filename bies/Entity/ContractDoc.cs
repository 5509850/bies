using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace bies.Entity
{
    public class ContractDoc
    {
        private int contractDocsID;
        private int contractID;
        private Int64 amnountS;
        private int currencyID;
        private int avanceA;
        private int retentionU;
        private Int64 s1;
        private int typedocID;
        private bool isActive;
        private bool isNew;


        public int ContractDocsID
        {
            get { return contractDocsID; }
            set { contractDocsID = value; }
        }

        public int ContractID
        {
            get { return contractID; }
            set { contractID = value; }
        }

        public Int64 AmnountS
        {
            get { return amnountS; }
            set { amnountS = value; }
        }

        public int CurrencyID
        {
            get { return currencyID; }
            set { currencyID = value; }
        }

        public int AvanceA
        {
            get { return avanceA; }
            set { avanceA = value; }
        }

        public int RetentionU
        {
            get { return retentionU; }
            set { retentionU = value; }
        }

        public Int64 S1
        {
            get { return s1; }
            set { s1 = value; }
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

        public bool CreateContractDocGetID()
        {
            DataBase db = new DataBase();
            string request;

            try
            {
                SqlConnection con = db.Connect();
                request =
                String.Format(
                    @"INSERT INTO  contractDocs 
(contractID, amnountS, currencyID, avanceA, retentionU, s1, typedocID, isActive)
VALUES     ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}');
SELECT @@IDENTITY AS 'ID';", ContractID,
                           AmnountS,
                           CurrencyID,
                           AvanceA,
                           RetentionU, S1, TypedocID, utils.SetBitDB(IsActive));
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
                    ContractDocsID = Convert.ToInt32(_dr["ID"]);

                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ошибка CreateContractDocGetID error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            /*
             */
            string request = String.Format(
                    @"UPDATE    contractDocs
SET           contractID = '{1}', amnountS = '{2}', currencyID = '{3}', avanceA = '{4}', retentionU = '{5}', s1 = '{6}', typedocID = '{7}', isActive = '{8}'

WHERE     (contractDocsID = '{0}')", ContractDocsID, ContractID, AmnountS, CurrencyID, AvanceA, RetentionU, S1, TypedocID, utils.SetBitDB(IsActive));
            return db.ExecuteSql(request);
        }

        public bool Delete()
        {
            DataBase db = new DataBase();
            string request = String.Format(@"UPDATE   contractDocs  SET isActive = '0' WHERE  (contractDocsID = '{0}');
UPDATE    filedoc SET isActive = '0' WHERE     (docID = '{0}') AND (typedocID = '{1}')", ContractDocsID, TypedocID);
            return db.ExecuteSql(request);
        }
    }
}
